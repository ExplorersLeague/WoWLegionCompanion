using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using bgs;
using Blizzard;
using bnet.protocol;
using bnet.protocol.account;
using bnet.protocol.attribute;
using bnet.protocol.game_utilities;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using JamLib;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using WowJamMessages;
using WowJamMessages.JSONRealmList;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class Login : Singleton<Login>
	{
		public bool ReturnToRecentCharacter { get; set; }

		public bool ReturnToCharacterList { get; set; }

		public LoginUI LoginUI
		{
			get
			{
				if (this.m_loginUI == null)
				{
					this.m_loginUI = Object.FindObjectOfType<LoginUI>();
				}
				return this.m_loginUI;
			}
		}

		public bool CanCancelNow()
		{
			switch (this.GetLoginState())
			{
			case Login.eLoginState.IDLE:
			case Login.eLoginState.WAIT_FOR_ASSET_BUNDLES:
			case Login.eLoginState.WEB_AUTH_START:
			case Login.eLoginState.WEB_AUTH_LOADING:
			case Login.eLoginState.WEB_AUTH_IN_PROGRESS:
			case Login.eLoginState.WEB_AUTH_FAILED:
			case Login.eLoginState.BN_LOGIN_START:
			case Login.eLoginState.BN_LOGIN_WAIT_FOR_LOGON:
			case Login.eLoginState.BN_LOGIN_PROVIDE_TOKEN:
			case Login.eLoginState.BN_LOGGING_IN:
			case Login.eLoginState.BN_ACCOUNT_NAME_WAIT:
			case Login.eLoginState.BN_TICKET_WAIT:
			case Login.eLoginState.BN_SUBREGION_LIST_WAIT:
			case Login.eLoginState.BN_CHARACTER_LIST_WAIT:
			case Login.eLoginState.BN_LOGIN_UNKNOWN:
			case Login.eLoginState.UPDATE_REQUIRED_START:
			case Login.eLoginState.UPDATE_REQUIRED_IDLE:
				return true;
			}
			return false;
		}

		public void OnConnectionLost(Network.ConnectionLostEvent eventArgs)
		{
			if (this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN || this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_IDLE || this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE)
			{
				this.MobileDisconnect(DisconnectReason.ConnectionLost);
			}
		}

		private void OnReturnToTitleScene(Scene scene, LoadSceneMode loadSceneMode)
		{
			this.OnLogoutConfirmed(false);
			SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnReturnToTitleScene);
		}

		public void OnClickTitleResume()
		{
			this.StartCachedLogin(true, false);
		}

		public void OnClickTitleConnect()
		{
			if (this.HaveCachedWebToken())
			{
				this.LoginUI.ShowLogoutConfirmationPopup(true);
			}
			else
			{
				this.StartNewLogin();
			}
		}

		public void OnClickCharacterSelectCancel()
		{
			this.LoginUI.ShowLogoutConfirmationPopup(false);
		}

		public void OnClickBackToAccountSelect()
		{
			this.BackToAccountSelect();
		}

		public void OnClickConnectingCancel()
		{
			if (this.ReturnToRecentCharacter)
			{
				this.StartCachedLogin(true, true);
			}
			else
			{
				this.CancelLogin();
			}
		}

		public void OnLogoutConfirmed(bool goToWebAuth)
		{
			this.LoginUI.HideAllPopups();
			if (this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_IDLE || this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE)
			{
				this.InitiateMobileDisconnect();
			}
			if (goToWebAuth)
			{
				this.StartNewLogin();
			}
			else if (this.ReturnToCharacterList)
			{
				this.StartCachedLogin(true, false);
				this.ReturnToCharacterList = false;
			}
			else
			{
				this.BackToAccountSelect();
			}
		}

		public void OnLogoutCancel()
		{
			this.LoginUI.HideAllPopups();
		}

		public void SetRegionIndex()
		{
			this.LoginUI.SetRegionIndex();
			this.LoginUI.HideAllPopups();
		}

		public void CancelRegionIndex()
		{
			this.LoginUI.CancelRegionIndex();
			this.LoginUI.HideAllPopups();
		}

		public void ShowCreateNewLoginPanel()
		{
			this.LoginUI.ShowCreateNewLoginPanel();
		}

		public void ShowRealmListPanel()
		{
			this.LoginUI.ShowRealmListPanel();
		}

		private void OnApplicationPause(bool paused)
		{
			this.LoginLog("OnApplicationPause: " + paused.ToString());
			if (Main.instance != null)
			{
				if (paused)
				{
					Main.instance.ScheduleNotifications();
				}
				else
				{
					Main.instance.ClearPendingNotifications();
				}
			}
			if (paused)
			{
				BattleNet.ApplicationWasPaused();
				this.m_pauseTimestamp = GeneralHelpers.CurrentUnixTime();
			}
			else
			{
				if (this.m_loginState == Login.eLoginState.UPDATE_REQUIRED_START || this.m_loginState == Login.eLoginState.UPDATE_REQUIRED_IDLE)
				{
					this.LoginLog("OnApplicationPause: Update required, exiting early.");
					return;
				}
				int num = GeneralHelpers.CurrentUnixTime();
				int num2 = num - this.m_pauseTimestamp;
				bool flag = num2 > 30;
				if (flag)
				{
					if (this.LoginUI != null)
					{
						this.LoginUI.HideAllPopups();
					}
					if (AdventureMapPanel.instance != null)
					{
						AdventureMapPanel.instance.SelectMissionFromList(0);
					}
					switch (this.m_loginState)
					{
					case Login.eLoginState.IDLE:
					case Login.eLoginState.WAIT_FOR_ASSET_BUNDLES:
					case Login.eLoginState.WEB_AUTH_START:
					case Login.eLoginState.WEB_AUTH_LOADING:
					case Login.eLoginState.WEB_AUTH_IN_PROGRESS:
						goto IL_11E;
					}
					this.LoginUI.ShowConnectingPanel();
				}
				IL_11E:
				BattleNet.ApplicationWasUnpaused();
				if (!this.m_initialUnpause)
				{
					if (flag)
					{
						this.LoginLog("reconnect update test");
						Singleton<AssetBundleManager>.instance.UpdateVersion();
						if (this.IsUpdateAvailable())
						{
							this.SetLoginState(Login.eLoginState.UPDATE_REQUIRED_START);
							this.CancelWebAuth();
							this.LoginLog("updateFound = true");
						}
						else
						{
							this.LoginLog("updateFound = false");
						}
					}
				}
				Login.eLoginState loginState = this.m_loginState;
				switch (loginState)
				{
				case Login.eLoginState.MOBILE_LOGGING_IN:
					this.LoginLog("OnApplicationPause: Reconnecting: mobile login states");
					this.m_battlenetFailures = 0;
					this.ReturnToTitleScene();
					break;
				case Login.eLoginState.MOBILE_LOGGED_IN:
				case Login.eLoginState.MOBILE_LOGGED_IN_IDLE:
				case Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE:
					if (flag)
					{
						this.LoginLog("OnApplicationPause: Reconnecting: mobile idle state");
						this.m_battlenetFailures = 0;
						this.ReturnToRecentCharacter = true;
						Network.Logout();
						Network.Disconnect();
						this.ReturnToTitleScene();
					}
					else if (!Network.IsConnected())
					{
						this.LoginLog("OnApplicationPause: Reconnecting, not connected after short pause time of " + num2);
						this.m_battlenetFailures = 0;
						this.ReturnToTitleScene();
					}
					else
					{
						this.LoginLog("OnApplicationPause: Still connected. Not reconnecting after short pause time of " + num2);
					}
					break;
				case Login.eLoginState.UPDATE_REQUIRED_START:
				case Login.eLoginState.UPDATE_REQUIRED_IDLE:
					break;
				default:
					switch (loginState)
					{
					case Login.eLoginState.WAIT_FOR_ASSET_BUNDLES:
						this.LoginLog("OnApplicationPause: Wait for asset bundles");
						break;
					case Login.eLoginState.WEB_AUTH_START:
					case Login.eLoginState.WEB_AUTH_LOADING:
					case Login.eLoginState.WEB_AUTH_IN_PROGRESS:
						this.LoginUI.HideConnectingPanel();
						this.LoginLog("OnApplicationPause: Hiding all panels");
						break;
					default:
						if (loginState != Login.eLoginState.BN_CHARACTER_LIST_WAIT)
						{
							if (this.m_loginState != Login.eLoginState.IDLE && this.m_loginState != Login.eLoginState.NO_NETWORK)
							{
								this.LoginLog("OnApplicationPause: Back to Title");
								this.ReturnToTitleScene();
							}
						}
						else if (flag)
						{
							this.m_battlenetFailures = 0;
							this.LoginLog("OnApplicationPause: Reconnecting: character list wait state");
							this.ReturnToTitleScene();
						}
						else
						{
							this.LoginLog("OnApplicationPause: Not reconnecting after short pause time of " + num2);
						}
						break;
					}
					break;
				}
				this.m_initialUnpause = false;
			}
		}

		private void Start()
		{
			this.LoginLog("-----START-----");
			this.m_urlDownloader = new DotNetUrlDownloader();
			this.SetInitialPortal();
			if (this.LoginUI == null)
			{
				return;
			}
			this.TryStartConnection();
		}

		private void TryStartConnection()
		{
			this.LoginLog("Attempting to start connection");
			if (Application.internetReachability == null)
			{
				this.LoginLog("No network connectivity");
				this.SetLoginState(Login.eLoginState.NO_NETWORK);
				GenericPopup.DisabledAction = delegate
				{
					this.SetLoginState(Login.eLoginState.IDLE);
				};
				this.LoginUI.ShowGenericPopupFull(this.GetNoInternetErrorText());
				return;
			}
			this.LoginLog("Starting connection");
			this.LoginUI.ShowConnectingPanel();
			this.SetLoginState(Login.eLoginState.WAIT_FOR_ASSET_BUNDLES);
			this.InitRecentCharacters();
			this.LoadRecentCharacters();
			this.ReturnToRecentCharacter = false;
			if (this.IsDevRegionList())
			{
				this.m_mobileLoginTimeout = 120f;
			}
			this.SubscribeToEvents();
			Network.Initialize();
			Singleton<AssetBundleManager>.Instance.InitAssetBundleManager();
		}

		private void OnDestroy()
		{
			if (!base.IsCloneGettingRemoved)
			{
				Network.Shutdown();
				this.UnsubscribeFromEvents();
			}
		}

		private void SubscribeToEvents()
		{
			Network.OnLoginFailedDuplicateCharacter += new Network.LoginFailedDuplicateCharacterHandler(this.LoginFailedDuplicateCharacterHandler);
			Network.OnLoginFailedUnknownReason += new Network.LoginFailedUnknownReasonHandler(this.LoginFailedUnknownReasonHandler);
			Network.OnLoginFailedVeteranAccount += new Network.LoginFailedVeteranAccountHandler(this.LoginFailedVeteranAccountHandler);
			Network.OnLoginFailedConsumptionTimeNotAllowed += new Network.LoginFailedConsumptionTimeNotAllowedHandler(this.LoginFailedConsumptionTimeNotAllowedHandler);
			Network.OnLoginFailedTrialNotAllowed += new Network.LoginFailedTrialNotAllowedHandler(this.LoginFailedTrialNotAllowedHandler);
			Network.OnConnectionLost += new Network.ConnectionLostHandler(Singleton<Login>.Instance.OnConnectionLost);
		}

		private void UnsubscribeFromEvents()
		{
			Network.OnLoginFailedDuplicateCharacter -= new Network.LoginFailedDuplicateCharacterHandler(this.LoginFailedDuplicateCharacterHandler);
			Network.OnLoginFailedUnknownReason -= new Network.LoginFailedUnknownReasonHandler(this.LoginFailedUnknownReasonHandler);
			Network.OnLoginFailedVeteranAccount -= new Network.LoginFailedVeteranAccountHandler(this.LoginFailedVeteranAccountHandler);
			Network.OnLoginFailedConsumptionTimeNotAllowed -= new Network.LoginFailedConsumptionTimeNotAllowedHandler(this.LoginFailedConsumptionTimeNotAllowedHandler);
			Network.OnLoginFailedTrialNotAllowed -= new Network.LoginFailedTrialNotAllowedHandler(this.LoginFailedTrialNotAllowedHandler);
			Network.OnConnectionLost -= new Network.ConnectionLostHandler(Singleton<Login>.Instance.OnConnectionLost);
		}

		private void LoginFailedDuplicateCharacterHandler(Network.LoginFailedDuplicateCharacterEvent eventArgs)
		{
			this.LoginLog("LoginFailedDuplicateCharacterHandler called");
			this.MobileDisconnect(DisconnectReason.CharacterInWorld);
		}

		private void LoginFailedUnknownReasonHandler(Network.LoginFailedUnknownReasonEvent eventArgs)
		{
			this.LoginLog("LoginFailedUnknownReasonHandler called, reason: " + eventArgs.Reason);
			this.MobileDisconnect(DisconnectReason.Generic);
		}

		private void LoginFailedVeteranAccountHandler(Network.LoginFailedVeteranAccountEvent eventArgs)
		{
			this.LoginLog("LoginFailedVeteranAccountHandler called");
			this.MobileDisconnect(DisconnectReason.ConsumptionTimeNotAllowed);
		}

		private void LoginFailedConsumptionTimeNotAllowedHandler(Network.LoginFailedConsumptionTimeNotAllowedEvent eventArgs)
		{
			this.LoginLog("LoginFailedConsumptionTimeNotAllowedHandler called");
			this.MobileDisconnect(DisconnectReason.ConsumptionTimeNotAllowed);
		}

		private void LoginFailedTrialNotAllowedHandler(Network.LoginFailedTrialNotAllowedEvent eventArgs)
		{
			this.LoginLog("LoginFailedTrialNotAllowedHandler called");
			this.MobileDisconnect(DisconnectReason.TrialNotAllowed);
		}

		private void SetLoginState(Login.eLoginState newState)
		{
			if ((this.m_loginState == Login.eLoginState.UPDATE_REQUIRED_START || this.m_loginState == Login.eLoginState.UPDATE_REQUIRED_IDLE) && newState != Login.eLoginState.UPDATE_REQUIRED_START && newState != Login.eLoginState.UPDATE_REQUIRED_IDLE && (Singleton<AssetBundleManager>.instance.ForceUpgrade || newState != Login.eLoginState.IDLE))
			{
				this.LoginLog("SetLoginState(): Update required, igoring state change to " + newState.ToString() + ".");
				return;
			}
			this.LoginLog(string.Concat(new string[]
			{
				"SetLoginState(): from ",
				this.m_loginState.ToString(),
				" to ",
				newState.ToString(),
				"."
			}));
			this.m_loginState = newState;
		}

		public Login.eLoginState GetLoginState()
		{
			return this.m_loginState;
		}

		private void Update()
		{
			if (this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE && Application.internetReachability == null)
			{
				this.LoginLog("No network connectivity");
				this.SetLoginState(Login.eLoginState.NO_NETWORK);
				GenericPopup.DisabledAction = new Action(this.ReturnToTitleScene);
				this.LoginUI.ShowGenericPopupFull(this.GetNoInternetErrorText());
				return;
			}
			this.UpdateLoginState();
			this.BnErrorsUpdate();
			if (this.m_urlDownloader != null)
			{
				this.m_urlDownloader.Process();
			}
			if (this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE)
			{
				MobileClient.SetServerTime(GarrisonStatus.CurrentTime());
			}
			Network.Update();
		}

		private void UpdateLoginState()
		{
			switch (this.m_loginState)
			{
			case Login.eLoginState.IDLE:
				if (this.ReturnToRecentCharacter || this.ReturnToCharacterList)
				{
					this.SetLoginState(Login.eLoginState.BN_LOGIN_START);
					this.ReturnToRecentCharacter = false;
					this.ReturnToCharacterList = false;
					this.TryStartConnection();
				}
				break;
			case Login.eLoginState.WAIT_FOR_ASSET_BUNDLES:
				this.WaitForAssetBundles();
				break;
			case Login.eLoginState.WEB_AUTH_START:
				this.WebAuthStart();
				break;
			case Login.eLoginState.WEB_AUTH_LOADING:
				this.WebAuthUpdate();
				break;
			case Login.eLoginState.WEB_AUTH_IN_PROGRESS:
				this.WebAuthUpdate();
				break;
			case Login.eLoginState.BN_LOGIN_START:
				this.BnLoginStart(true, true, true, false);
				break;
			case Login.eLoginState.BN_LOGIN_WAIT_FOR_LOGON:
				this.BnLoginWaitForLogon();
				break;
			case Login.eLoginState.BN_LOGIN_PROVIDE_TOKEN:
				this.BnLoginProvideToken();
				break;
			case Login.eLoginState.BN_LOGGING_IN:
			case Login.eLoginState.BN_LOGIN_UNKNOWN:
				this.BnLoginUpdate();
				break;
			case Login.eLoginState.BN_ACCOUNT_NAME_WAIT:
				this.BnAccountNameWait();
				break;
			case Login.eLoginState.BN_TICKET_WAIT:
				this.BnTicketWait();
				break;
			case Login.eLoginState.BN_SUBREGION_LIST_WAIT:
				this.BnSubRegionListWait();
				break;
			case Login.eLoginState.BN_CHARACTER_LIST_WAIT:
				this.BnCharacterListWait();
				break;
			case Login.eLoginState.BN_REALM_JOIN_WAIT:
				this.BnRealmJoinWait();
				break;
			case Login.eLoginState.MOBILE_CONNECT:
				this.MobileConnect();
				break;
			case Login.eLoginState.MOBILE_CONNECTING:
				this.MobileConnecting();
				break;
			case Login.eLoginState.MOBILE_CONNECT_FAILED:
				this.MobileConnectFailed();
				break;
			case Login.eLoginState.MOBILE_DISCONNECTING:
				this.MobileDisconnecting();
				break;
			case Login.eLoginState.MOBILE_DISCONNECTED:
				this.MobileDisconnected();
				break;
			case Login.eLoginState.MOBILE_DISCONNECTED_BY_SERVER:
				this.MobileDisconnectedByServer();
				break;
			case Login.eLoginState.MOBILE_LOGGING_IN:
				this.MobileLoggingIn();
				break;
			case Login.eLoginState.MOBILE_LOGGED_IN:
				this.MobileLoggedIn();
				break;
			case Login.eLoginState.MOBILE_LOGGED_IN_IDLE:
				this.MobileLoggedInIdle();
				break;
			case Login.eLoginState.UPDATE_REQUIRED_START:
				this.UpdateRequiredStart();
				break;
			case Login.eLoginState.UPDATE_REQUIRED_IDLE:
				this.UpdateRequiredIdle();
				break;
			}
		}

		private void WaitForAssetBundles()
		{
			if (!Singleton<AssetBundleManager>.instance.IsInitialized())
			{
				return;
			}
			this.GetBnServerString();
			this.LoginLog("Latest version is " + Singleton<AssetBundleManager>.instance.LatestVersion);
			this.LoginLog("Force upgrade is " + Singleton<AssetBundleManager>.instance.ForceUpgrade);
			string @string = SecurePlayerPrefs.GetString("WebToken", Main.uniqueIdentifier);
			if (this.IsUpdateAvailable())
			{
				this.SetLoginState(Login.eLoginState.UPDATE_REQUIRED_START);
			}
			else if (@string != null && @string != string.Empty)
			{
				if (this.LoginUI != null)
				{
					this.LoginUI.ShowConnectingPanel();
					this.SetLoginState(Login.eLoginState.BN_LOGIN_START);
				}
			}
			else if (this.LoginUI != null)
			{
				this.LoginUI.ShowTitlePanel();
				this.SetLoginState(Login.eLoginState.IDLE);
			}
		}

		private bool IsUpdateAvailable()
		{
			return Singleton<AssetBundleManager>.instance.LatestVersion > new Version(Application.version);
		}

		private void UpdateRequiredStart()
		{
			this.LoginLog("UpdateRequiredStart()");
			this.LoginUI.ShowTitlePanel();
			GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.UpdateAppPopupDisabledAction));
			this.LoginUI.ShowGenericPopup(StaticDB.GetString("UPDATE_REQUIRED", null), StaticDB.GetString("UPDATE_REQUIRED_DESCRIPTION", null));
			if (Singleton<AssetBundleManager>.instance.ForceUpgrade)
			{
				this.LoginLog("UpdateRequiredStart() force upgrade");
				this.SetLoginState(Login.eLoginState.UPDATE_REQUIRED_IDLE);
			}
			else
			{
				this.LoginLog("UpdateRequiredStart() do not force upgrade");
				this.SetLoginState(Login.eLoginState.IDLE);
			}
		}

		private void UpdateRequiredIdle()
		{
			if (Singleton<AssetBundleManager>.instance.ForceUpgrade && !this.LoginUI.IsGenericPopupShowing())
			{
				this.LoginLog("UpdateRequiredIdle() opening popup");
				this.LoginUI.ShowTitlePanel();
				GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.UpdateAppPopupDisabledAction));
				this.LoginUI.ShowGenericPopup(StaticDB.GetString("UPDATE_REQUIRED", null), StaticDB.GetString("UPDATE_REQUIRED_DESCRIPTION", null));
			}
		}

		private void UpdateAppPopupDisabledAction()
		{
			GenericPopup.DisabledAction = (Action)Delegate.Remove(GenericPopup.DisabledAction, new Action(this.UpdateAppPopupDisabledAction));
			string text;
			if (Singleton<Login>.instance.GetBnPortal() == "cn")
			{
				text = Singleton<AssetBundleManager>.instance.AppStoreUrl_CN;
			}
			else
			{
				text = Singleton<AssetBundleManager>.instance.AppStoreUrl;
			}
			if (text != null)
			{
				Application.OpenURL(text);
			}
		}

		public bool IsDevRegionList()
		{
			return false;
		}

		public bool IsDevPortal(string portal)
		{
			foreach (string b in Login.m_devPortals)
			{
				if (portal == b)
				{
					return true;
				}
			}
			return false;
		}

		public string GetBnServerString()
		{
			string @string = SecurePlayerPrefs.GetString("Portal", Main.uniqueIdentifier);
			if (@string != null && @string != string.Empty)
			{
				Login.m_portal = @string;
			}
			string text;
			if (this.IsDevPortal(Login.m_portal))
			{
				text = Login.m_portal + ".bgs.battle.net";
			}
			else
			{
				text = Login.m_portal + ".actual.battle.net";
			}
			this.LoginLog("BnServerString is " + text);
			return text;
		}

		public string GetBnPortal()
		{
			string @string = SecurePlayerPrefs.GetString("Portal", Main.uniqueIdentifier);
			if (@string != null && @string != string.Empty)
			{
				Login.m_portal = @string;
			}
			this.LoginLog("Portal is " + Login.m_portal);
			return Login.m_portal;
		}

		public void SetPortal(string newValue)
		{
			Login.m_portal = newValue.ToLower();
			SecurePlayerPrefs.SetString("Portal", Login.m_portal, Main.uniqueIdentifier);
			PlayerPrefs.Save();
			this.LoginLog("Setting portal to " + Login.m_portal);
		}

		private void SetInitialPortal()
		{
			string @string = SecurePlayerPrefs.GetString("Portal", Main.uniqueIdentifier);
			if (@string != null && @string != string.Empty)
			{
				Login.m_portal = @string;
			}
			else if (!this.IsDevRegionList())
			{
				string locale = Main.instance.GetLocale();
				string text;
				switch (locale)
				{
				case "frFR":
				case "deDE":
				case "ruRU":
				case "itIT":
					text = "eu";
					goto IL_112;
				case "koKR":
				case "zhTW":
					text = "kr";
					goto IL_112;
				case "zhCN":
					text = "cn";
					goto IL_112;
				}
				text = "us";
				IL_112:
				this.LoginLog("Setting initial portal to " + text);
				this.SetPortal(text);
			}
		}

		private void BnErrorsUpdate()
		{
			int errorsCount = BattleNet.GetErrorsCount();
			if (errorsCount > 0)
			{
				BnetErrorInfo[] array = new BnetErrorInfo[errorsCount];
				BattleNet.GetErrors(array);
				BattleNet.ClearErrors();
				foreach (BnetErrorInfo bnetErrorInfo in array)
				{
					this.LoginLog("Battle.net error: Name: " + bnetErrorInfo.GetName());
					this.LoginLog("Battle.net error: Feature: " + bnetErrorInfo.GetFeature());
					this.LoginLog("Battle.net error: FeatureEvent: " + bnetErrorInfo.GetFeatureEvent());
					this.LoginLog("Battle.net error: Context: " + bnetErrorInfo.GetContext());
					if (bnetErrorInfo.GetError() != bgs.BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED)
					{
						this.BnLoginFailed(null, null);
						return;
					}
					Login.eLoginState loginState = this.m_loginState;
					if (loginState == Login.eLoginState.BN_CHARACTER_LIST_WAIT)
					{
						this.BnLoginStart(true, true, false, false);
					}
				}
			}
			List<string> logEvents = Log.BattleNet.GetLogEvents();
			if (logEvents.Count > 0)
			{
				Log.BattleNet.ClearLogEvents();
			}
		}

		public void LoginLog(string message)
		{
			DebugPrinter.Log(">>>>>> " + message, null, DebugPrinter.LogLevel.Info);
		}

		public void StartNewLogin()
		{
			this.BnLoginStart(false, false, false, false);
		}

		public void StartCachedLogin(bool useCachedRealm, bool useCachedCharacter)
		{
			this.BnLoginStart(true, useCachedRealm, useCachedCharacter, false);
		}

		public void ClearAllCachedTokens()
		{
			SecurePlayerPrefs.DeleteKey("WebToken");
			SecurePlayerPrefs.DeleteKey("CharacterID");
			SecurePlayerPrefs.DeleteKey("CharacterName");
			SecurePlayerPrefs.DeleteKey("GameAccountHigh");
			SecurePlayerPrefs.DeleteKey("GameAccountLow");
			this.LoginLog("Clearing cached BN token, game account, and character");
		}

		private void ClearCachedTokens()
		{
			SecurePlayerPrefs.DeleteKey("WebToken");
			SecurePlayerPrefs.DeleteKey("CharacterID");
			this.LoginLog("Clearing cached BN token, realm, and character");
		}

		public void ClearRealmAndCharacterTokens()
		{
			SecurePlayerPrefs.DeleteKey("CharacterID");
			this.LoginLog("Clearing cached realm and character");
		}

		public bool HaveCachedWebToken()
		{
			string @string = SecurePlayerPrefs.GetString("WebToken", Main.uniqueIdentifier);
			return @string != null && @string != string.Empty;
		}

		public bool UseCachedCharacter()
		{
			return this.m_useCachedCharacter;
		}

		private void BnLoginStart(bool cachedLogin, bool cachedRealm, bool cachedCharacter, bool returnToRecentCharacter = false)
		{
			Main.instance.OnBeginConnection();
			BattleNet.RequestCloseAurora();
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			this.m_useCachedLogin = cachedLogin;
			this.m_useCachedRealm = cachedRealm;
			this.m_useCachedCharacter = cachedCharacter;
			this.ReturnToRecentCharacter = returnToRecentCharacter;
			this.m_bnLoginStartTime = Time.timeSinceLevelLoad;
			if (this.LoginUI != null)
			{
				this.LoginUI.ShowConnectingPanel();
			}
			string bnServerString = this.GetBnServerString();
			int port = 1119;
			ClientInterface ci = new MyClientInterface();
			SslParameters sslParams = new SslParameters();
			this.LoginLog(string.Concat(new string[]
			{
				"BnLoginStart(",
				this.m_useCachedLogin.ToString(),
				",",
				this.m_useCachedRealm.ToString(),
				",",
				this.m_useCachedCharacter.ToString(),
				"): server = ",
				bnServerString
			}));
			bool flag = BattleNet.Init(true, string.Empty, bnServerString, port, sslParams, ci);
			if (flag)
			{
				BattleNet.ProcessAurora();
				this.SetLoginState(Login.eLoginState.BN_LOGIN_WAIT_FOR_LOGON);
			}
			else
			{
				this.LoginLog("BattleNet.Init() failed.");
				this.BnLoginFailed(null, null);
			}
		}

		private void BnLoginWaitForLogon()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			if (BattleNet.CheckWebAuth(out this.m_webAuthUrl))
			{
				this.LoginLog("Received WebAuth challenge URL: " + this.m_webAuthUrl);
				this.m_webToken = SecurePlayerPrefs.GetString("WebToken", Main.uniqueIdentifier);
				if (this.m_useCachedLogin && this.m_webToken != null && this.m_webToken != string.Empty)
				{
					this.SetLoginState(Login.eLoginState.BN_LOGIN_PROVIDE_TOKEN);
				}
				else
				{
					this.SetLoginState(Login.eLoginState.WEB_AUTH_START);
				}
			}
			if (Time.timeSinceLevelLoad > this.m_bnLoginStartTime + 20f)
			{
				this.LoginLog("BnLoginWaitForLogon(): timed out.");
				this.BnLoginFailed(null, null);
			}
		}

		private void WebAuthStart()
		{
			this.LoginUI.HidePanelsForWebAuth();
			if (this.m_webAuth == null)
			{
				GameObject gameObject = GameObject.Find("MainCanvas");
				if (gameObject == null)
				{
					throw new Exception("Canvas game obj was null in WebAuthStart");
				}
				Canvas component = gameObject.GetComponent<Canvas>();
				if (component == null)
				{
					throw new Exception("webCanvas was null in WebAuthStart");
				}
				if (this.m_webAuthUrl == null)
				{
					throw new Exception("m_webAuthUrl was null in WebAuthStart");
				}
				float x = 0f;
				float y = 0f;
				float width = component.pixelRect.width;
				float height = component.pixelRect.height;
				this.m_webAuth = new WebAuth(this.m_webAuthUrl, x, y, width, height, "MainScriptObj");
				this.LoginLog("Created WebAuth, url: " + this.m_webAuthUrl);
			}
			if (this.m_webAuth != null)
			{
				this.SetLoginState(Login.eLoginState.WEB_AUTH_IN_PROGRESS);
				string locale = Main.instance.GetLocale();
				string text = locale.Substring(2);
				this.LoginLog("Setting web auth country code to " + text);
				this.m_webAuth.SetCountryCodeCookie(text, ".blizzard.net");
				WebAuth.ClearLoginData();
				this.m_webAuth.Load();
				this.LoginUI.ShowConnectingPanel();
				this.LoginLog("------------------------------ Loading WebAuth ----------------------------");
				return;
			}
			throw new Exception("m_webAuth was null in WebAuthStart");
		}

		public void ShowWebAuthView()
		{
			if (this.m_webAuth != null && this.m_loginState == Login.eLoginState.WEB_AUTH_IN_PROGRESS)
			{
				this.LoginLog("============================= Showing WebAuth View ========================");
				this.m_webAuth.Show();
				this.LoginUI.ShowWebAuthPanel();
			}
		}

		public void CancelWebAuth()
		{
			this.LoginUI.HideWebAuthPanel();
			if (this.m_webAuth != null)
			{
				this.m_webAuth.Close();
				this.m_webAuth = null;
			}
		}

		private void WebAuthUpdate()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			if (this.m_webAuth == null)
			{
				this.SetLoginState(Login.eLoginState.WEB_AUTH_FAILED);
				this.LoginLog("m_webAuth was null in WebAuthUpdate");
				return;
			}
			WebAuth.Status status = this.m_webAuth.GetStatus();
			if (status != WebAuth.Status.Success)
			{
				if (status == WebAuth.Status.Error)
				{
					this.LoginLog("Web auth status: Error.");
					this.SetLoginState(Login.eLoginState.WEB_AUTH_FAILED);
				}
			}
			else
			{
				this.m_webToken = this.m_webAuth.GetLoginCode();
				this.LoginLog("Received web auth token: " + this.m_webToken);
				this.SetLoginState(Login.eLoginState.BN_LOGIN_PROVIDE_TOKEN);
				this.m_webAuth.Close();
				this.m_webAuth = null;
				this.LoginUI.ShowConnectingPanel();
			}
		}

		public bool IsWebAuthState()
		{
			switch (this.m_loginState)
			{
			case Login.eLoginState.WEB_AUTH_START:
			case Login.eLoginState.WEB_AUTH_LOADING:
			case Login.eLoginState.WEB_AUTH_IN_PROGRESS:
			case Login.eLoginState.WEB_AUTH_FAILED:
				return true;
			default:
				return false;
			}
		}

		private void BnLoginProvideToken()
		{
			BattleNet.ProcessAurora();
			BattleNet.ProvideWebAuthToken(this.m_webToken);
			this.LoginLog("BnLoginProvideToken(): Provided web auth token " + this.m_webToken);
			this.SetLoginState(Login.eLoginState.BN_LOGGING_IN);
		}

		private void BnLoginUpdate()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			if (BattleNet.CheckWebAuth(out this.m_webAuthUrl))
			{
				if (!this.m_retryWebAuthOnce)
				{
					this.LoginLog("CheckWebAuth was true in BnLoginUpdate, starting WebAuth.");
					this.SetLoginState(Login.eLoginState.WEB_AUTH_START);
				}
				else
				{
					this.LoginLog("Retrying cached web token!");
					this.m_retryWebAuthOnce = false;
				}
				return;
			}
			switch (BattleNet.BattleNetStatus())
			{
			case 0:
				if (this.m_loginState != Login.eLoginState.BN_LOGIN_UNKNOWN)
				{
					this.SetLoginState(Login.eLoginState.BN_LOGIN_UNKNOWN);
				}
				break;
			case 1:
				if (this.m_loginState != Login.eLoginState.BN_LOGGING_IN)
				{
					this.SetLoginState(Login.eLoginState.BN_LOGGING_IN);
				}
				break;
			case 2:
				this.LoginLog("BnLoginUpdate(): timed out.");
				this.BnLoginFailed(null, null);
				break;
			case 3:
				this.LoginLog("BnLoginUpdate(): login failed.");
				this.BnLoginFailed(null, null);
				break;
			case 4:
				this.LoginLog("-------------------BnLoginUpdate(): Success! Logged in.--------------------------");
				this.BnLoginSucceeded();
				break;
			}
		}

		private void BnLoginSucceeded()
		{
			this.m_battlenetFailures = 0;
			SecurePlayerPrefs.SetString("WebToken", this.m_webToken, Main.uniqueIdentifier);
			PlayerPrefs.Save();
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			if (this.m_loginGameAccounts == null)
			{
				this.m_loginGameAccounts = new List<Login.LoginGameAccount>();
			}
			this.m_loginGameAccounts.Clear();
			this.RequestGameAccountNames();
		}

		private void RegisterPushManager(string token, string locale, string bnetAccountID)
		{
			string text = locale.Substring(2);
			BLPushManagerBuilder blpushManagerBuilder = ScriptableObject.CreateInstance<BLPushManagerBuilder>();
			if (Login.m_portal.ToLower() == "wow-dev")
			{
				blpushManagerBuilder.isDebug = true;
				blpushManagerBuilder.applicationName = "test.wowcompanion";
			}
			else
			{
				blpushManagerBuilder.isDebug = false;
				blpushManagerBuilder.applicationName = "wowcompanion";
			}
			blpushManagerBuilder.shouldRegisterwithBPNS = true;
			blpushManagerBuilder.region = text;
			blpushManagerBuilder.locale = locale;
			blpushManagerBuilder.authToken = token;
			blpushManagerBuilder.authRegion = text;
			blpushManagerBuilder.appAccountID = bnetAccountID;
			blpushManagerBuilder.senderId = "952133414280";
			blpushManagerBuilder.didReceiveRegistrationTokenDelegate = new DidReceiveRegistrationTokenDelegate(this.DidReceiveRegistrationTokenHandler);
			blpushManagerBuilder.didReceiveDeeplinkURLDelegate = new DidReceiveDeeplinkURLDelegate(this.DidReceiveDeeplinkURLDelegateHandler);
			BLPushManager.instance.InitWithBuilder(blpushManagerBuilder);
			BLPushManager.instance.RegisterForPushNotifications();
			this.LoginLog("Registered for push using game account " + bnetAccountID + ", region " + text);
		}

		public void DidReceiveRegistrationTokenHandler(string deviceToken)
		{
			this.LoginLog("DidReceiveRegistrationTokenHandler: device token " + deviceToken);
		}

		public void DidReceiveDeeplinkURLDelegateHandler(string url)
		{
			this.LoginLog("DidReceiveDeeplinkURLDelegateHandler: url " + url);
		}

		public void AddGameAccountButton(EntityId gameAccount, string name, bool isBanned, bool isSuspended)
		{
			if (this.m_loginState != Login.eLoginState.BN_ACCOUNT_NAME_WAIT)
			{
				this.LoginLog("AddGameAccountButton(): Ignored because in state " + this.m_loginState);
				return;
			}
			RealmListView realmListView = this.LoginUI.RealmListView;
			if (this.m_loginGameAccounts.Count == 0)
			{
				realmListView.ClearBnRealmList();
			}
			Login.LoginGameAccount loginGameAccount = new Login.LoginGameAccount();
			loginGameAccount.gameAccount = gameAccount;
			loginGameAccount.name = name;
			loginGameAccount.isBanned = isBanned;
			loginGameAccount.isSuspended = isSuspended;
			this.m_loginGameAccounts.Add(loginGameAccount);
			List<EntityId> gameAccountList = BattleNet.GetGameAccountList();
			if (gameAccountList.Count > 1)
			{
				string @string = SecurePlayerPrefs.GetString("GameAccountHigh", Main.uniqueIdentifier);
				string string2 = SecurePlayerPrefs.GetString("GameAccountLow", Main.uniqueIdentifier);
				ulong num = 0UL;
				ulong num2 = 0UL;
				if (@string != null && @string != string.Empty)
				{
					num = Convert.ToUInt64(@string);
				}
				if (string2 != null && string2 != string.Empty)
				{
					num2 = Convert.ToUInt64(string2);
				}
				if (this.m_useCachedWoWAccount)
				{
					if (num == gameAccount.High && num2 == gameAccount.Low)
					{
						if (this.m_useCachedLogin && !isBanned && !isSuspended)
						{
							if (this.m_gameAccount == null)
							{
								this.m_gameAccount = new EntityId();
							}
							this.m_gameAccount.High = num;
							this.m_gameAccount.Low = num2;
							this.SetLoginState(Login.eLoginState.BN_TICKET_WAIT);
							this.SendRealmListTicketRequest();
						}
					}
					else if (this.m_loginGameAccounts.Count >= gameAccountList.Count && !this.LoginUI.IsShowingRealmListPanel())
					{
						this.LoginLog(string.Concat(new object[]
						{
							"Called ShowingRealmListPanel() because all accounts received. ",
							this.m_loginGameAccounts.Count,
							" > ",
							gameAccountList.Count
						}));
						this.LoginUI.ShowRealmListPanel();
					}
				}
				else if (!this.LoginUI.IsShowingRealmListPanel())
				{
					this.LoginLog("Called ShowingRealmListPanel() because m_useCachedWoWAccount is false.");
					this.LoginUI.ShowRealmListPanel();
				}
			}
			else
			{
				if (isBanned)
				{
					this.BnLoginFailed(StaticDB.GetString("SUSPENDED", null), StaticDB.GetString("SUSPENDED_LONG", null));
					return;
				}
				if (isSuspended)
				{
					this.BnLoginFailed(StaticDB.GetString("SUSPENDED", null), StaticDB.GetString("SUSPENDED_LONG", null));
					return;
				}
				this.m_gameAccount = gameAccountList[0];
				this.SetLoginState(Login.eLoginState.BN_TICKET_WAIT);
				this.SendRealmListTicketRequest();
			}
			realmListView.AddGameAccountButton(gameAccount, name, isBanned, isSuspended);
		}

		public void SelectGameAccount(EntityId gameAccount)
		{
			this.m_useCachedWoWAccount = true;
			Login.LoginGameAccount loginGameAccount = null;
			foreach (Login.LoginGameAccount loginGameAccount2 in this.m_loginGameAccounts)
			{
				if (loginGameAccount2.gameAccount == gameAccount)
				{
					loginGameAccount = loginGameAccount2;
				}
			}
			if (loginGameAccount == null)
			{
				this.LoginLog("SelectGameAccount: Could not find account " + gameAccount.ToString());
				this.BnLoginFailed(null, null);
				return;
			}
			if (loginGameAccount.isBanned)
			{
				List<EntityId> gameAccountList = BattleNet.GetGameAccountList();
				if (gameAccountList.Count > 1)
				{
					this.LoginUI.ShowGenericPopup(StaticDB.GetString("BANNED", null), StaticDB.GetString("BANNED_LONG", null));
				}
				else
				{
					this.BnLoginFailed(StaticDB.GetString("BANNED", null), StaticDB.GetString("BANNED_LONG", null));
				}
				return;
			}
			if (loginGameAccount.isSuspended)
			{
				List<EntityId> gameAccountList2 = BattleNet.GetGameAccountList();
				if (gameAccountList2.Count > 1)
				{
					this.LoginUI.ShowGenericPopup(StaticDB.GetString("SUSPENDED", null), StaticDB.GetString("SUSPENDED_LONG", null));
				}
				else
				{
					this.BnLoginFailed(StaticDB.GetString("SUSPENDED", null), StaticDB.GetString("SUSPENDED_LONG", null));
				}
				return;
			}
			SecurePlayerPrefs.SetString("GameAccountHigh", gameAccount.High.ToString(), Main.uniqueIdentifier);
			SecurePlayerPrefs.SetString("GameAccountLow", gameAccount.Low.ToString(), Main.uniqueIdentifier);
			PlayerPrefs.Save();
			this.m_gameAccount = gameAccount;
			this.SetLoginState(Login.eLoginState.BN_TICKET_WAIT);
			this.SendRealmListTicketRequest();
			RealmListView realmListView = this.LoginUI.RealmListView;
			realmListView.ClearBnRealmList();
			realmListView.SetRealmListTitle();
		}

		private void RequestGameAccountNames()
		{
			this.SetLoginState(Login.eLoginState.BN_ACCOUNT_NAME_WAIT);
			List<EntityId> gameAccountList = BattleNet.GetGameAccountList();
			foreach (EntityId entityId in gameAccountList)
			{
				Login.GameAccountStateCallback gameAccountStateCallback = new Login.GameAccountStateCallback();
				gameAccountStateCallback.EntityID = entityId;
				GetGameAccountStateRequest getGameAccountStateRequest = new GetGameAccountStateRequest();
				getGameAccountStateRequest.GameAccountId = entityId;
				getGameAccountStateRequest.Options = new GameAccountFieldOptions();
				getGameAccountStateRequest.Options.SetFieldGameLevelInfo(true);
				getGameAccountStateRequest.Options.SetFieldGameStatus(true);
				BattleNet.GetGameAccountState(getGameAccountStateRequest, new RPCContextDelegate(gameAccountStateCallback.Callback));
			}
		}

		private void BnAccountNameWait()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			RealmListView realmListView = this.LoginUI.RealmListView;
			realmListView.SetGameAccountTitle();
		}

		private void SendRealmListTicketRequest()
		{
			this.LoginLog("Sending realm list ticket ClientRequest...");
			ClientRequest clientRequest = new ClientRequest();
			bnet.protocol.attribute.Attribute attribute = new bnet.protocol.attribute.Attribute();
			attribute.SetName(MobileBuild.GetRealmListTicketRequestName());
			Variant variant = new Variant();
			variant.SetIntValue(0L);
			attribute.SetValue(variant);
			clientRequest.AddAttribute(attribute);
			bnet.protocol.attribute.Attribute attribute2 = new bnet.protocol.attribute.Attribute();
			attribute2.SetName("Param_Identity");
			JSONRealmListTicketIdentity jsonrealmListTicketIdentity = new JSONRealmListTicketIdentity();
			byte b = (byte)((this.m_gameAccount.High & 1095216660480UL) >> 32);
			jsonrealmListTicketIdentity.GameAccountRegion = b;
			jsonrealmListTicketIdentity.GameAccountID = this.m_gameAccount.Low;
			this.LoginLog("Region is " + b);
			this.LoginLog("GameAccountRegion: " + jsonrealmListTicketIdentity.GameAccountRegion);
			this.LoginLog("GameAccountID: " + jsonrealmListTicketIdentity.GameAccountID);
			this.LoginLog(string.Format("BNetAccount-0-{0:X12}", BattleNet.GetMyAccountId().lo));
			this.LoginLog(string.Format("WowAccount-0-{0:X12}", this.m_gameAccount.Low));
			string text = jsonrealmListTicketIdentity.ToString().Substring(jsonrealmListTicketIdentity.ToString().LastIndexOf(".") + 1) + ":";
			text += JsonConvert.SerializeObject(jsonrealmListTicketIdentity, Formatting.None);
			text += '\0';
			Variant variant2 = new Variant();
			variant2.SetBlobValue(Encoding.UTF8.GetBytes(text));
			this.LoginLog("Identity: <" + Encoding.UTF8.GetString(variant2.BlobValue) + ">");
			attribute2.SetValue(variant2);
			clientRequest.AddAttribute(attribute2);
			bnet.protocol.attribute.Attribute attribute3 = new bnet.protocol.attribute.Attribute();
			attribute3.SetName("Param_ClientInfo");
			FourCC fourCC = new FourCC();
			fourCC.SetValue((uint)MobileBuild.GetClientId());
			FourCC fourCC2 = new FourCC();
			fourCC2.SetString(BattleNet.Client().GetPlatformName());
			FourCC fourCC3 = new FourCC();
			fourCC3.SetString(Main.instance.GetLocale());
			JSONRealmListTicketClientInformation jsonrealmListTicketClientInformation = new JSONRealmListTicketClientInformation();
			jsonrealmListTicketClientInformation.Info = new JamJSONRealmListTicketClientInformation();
			jsonrealmListTicketClientInformation.Info.Type = fourCC.GetValue();
			jsonrealmListTicketClientInformation.Info.Platform = fourCC2.GetValue();
			jsonrealmListTicketClientInformation.Info.BuildVariant = MobileBuild.GetBuildVariant();
			jsonrealmListTicketClientInformation.Info.TextLocale = fourCC3.GetValue();
			jsonrealmListTicketClientInformation.Info.AudioLocale = fourCC3.GetValue();
			jsonrealmListTicketClientInformation.Info.Version = new JamJSONGameVersion();
			jsonrealmListTicketClientInformation.Info.Version.VersionMajor = (uint)MobileBuild.GetVersionMajor();
			jsonrealmListTicketClientInformation.Info.Version.VersionMinor = (uint)MobileBuild.GetVersionMinor();
			jsonrealmListTicketClientInformation.Info.Version.VersionRevision = (uint)MobileBuild.GetVersionRevision();
			jsonrealmListTicketClientInformation.Info.Version.VersionBuild = (uint)MobileBuild.GetBuildNum();
			jsonrealmListTicketClientInformation.Info.VersionDataBuild = (uint)MobileBuild.GetDataBuildNum();
			jsonrealmListTicketClientInformation.Info.CurrentTime = (int)BattleNet.Get().CurrentUTCTime();
			jsonrealmListTicketClientInformation.Info.TimeZone = "Etc/UTC";
			this.m_clientSecret = WowAuthCrypto.GenerateSecret();
			jsonrealmListTicketClientInformation.Info.Secret = this.m_clientSecret;
			jsonrealmListTicketClientInformation.Info.PlatformType = MobileBuild.GetPlatformType();
			jsonrealmListTicketClientInformation.Info.ClientArch = MobileBuild.GetClientArchitecture();
			jsonrealmListTicketClientInformation.Info.SystemArch = MobileBuild.GetSystemArchitecture();
			jsonrealmListTicketClientInformation.Info.SystemVersion = MobileBuild.GetOSVersion();
			this.LoginLog("clientInfoJSON.Info.Type is " + jsonrealmListTicketClientInformation.Info.Type);
			this.LoginLog("clientInfoJSON.Info.Textlocale is " + jsonrealmListTicketClientInformation.Info.TextLocale);
			this.LoginLog("clientInfoJSON.Info.Version.VersionBuild is " + jsonrealmListTicketClientInformation.Info.Version.VersionBuild);
			string text2 = jsonrealmListTicketClientInformation.ToString().Substring(jsonrealmListTicketClientInformation.ToString().LastIndexOf(".") + 1) + ":";
			text2 += JsonConvert.SerializeObject(jsonrealmListTicketClientInformation, Formatting.None, MessageFactory.SerializerSettings);
			text2 += '\0';
			Variant variant3 = new Variant();
			variant3.SetBlobValue(Encoding.UTF8.GetBytes(text2));
			this.LoginLog("info: <" + Encoding.UTF8.GetString(variant3.BlobValue) + ">");
			attribute3.SetValue(variant3);
			clientRequest.AddAttribute(attribute3);
			BattleNet.Get().SendClientRequest(clientRequest, null);
			this.LoginLog("-------------------------Sent realm list TICKET request--------------------------");
		}

		private void BnTicketWait()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			bool flag = false;
			GamesAPI.UtilResponse utilResponse = BattleNet.NextUtilPacket();
			if (utilResponse != null)
			{
				this.LoginLog("BnTicketWait(): Received ClientResponse: <" + utilResponse.GetType().ToString() + ">");
				foreach (bnet.protocol.attribute.Attribute attribute in utilResponse.m_response.AttributeList)
				{
					this.LoginLog("Attrib: <" + attribute.Name + ">");
					if (attribute.Name.StartsWith("Param_MobileErrorVersion"))
					{
						flag = false;
						this.BnLoginFailed(StaticDB.GetString("UPDATE_REQUIRED", null), StaticDB.GetString("UPDATE_REQUIRED_DESCRIPTION", null));
						break;
					}
					if (attribute.Name.StartsWith("Param_MobileError"))
					{
						flag = false;
						this.BnLoginFailed(StaticDB.GetString("LOGIN_UNAVAILABLE", null), null);
						break;
					}
					if (attribute.Name.StartsWith("Param_CustomErrorMessage"))
					{
						if (attribute.Value.HasStringValue)
						{
							flag = false;
							this.BnLoginFailed(attribute.Value.StringValue, null);
							break;
						}
						flag = false;
						this.BnLoginFailed(StaticDB.GetString("LOGIN_UNAVAILABLE", null), null);
						break;
					}
					else if (attribute.Name.StartsWith("Param_RealmListTicket"))
					{
						this.m_realmListTicket = attribute.Value.BlobValue;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.SetLoginState(Login.eLoginState.BN_SUBREGION_LIST_WAIT);
				this.SendSubRegionListRequest();
			}
		}

		private void SendSubRegionListRequest()
		{
			BattleNet.GetAllValuesForAttribute(MobileBuild.GetCharacterListRequestName(), 1);
		}

		private void BnSubRegionListWait()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			GamesAPI.GetAllValuesForAttributeResult getAllValuesForAttributeResult = BattleNet.NextGetAllValuesForAttributeResult();
			if (getAllValuesForAttributeResult != null)
			{
				this.LoginUI.ClearCharacterList();
				this.m_selectedCharacterEntry = null;
				int num = 0;
				this.m_subRegions = new string[getAllValuesForAttributeResult.m_response.AttributeValue.Count];
				foreach (Variant variant in getAllValuesForAttributeResult.m_response.AttributeValue)
				{
					this.m_subRegions[num++] = variant.StringValue;
				}
				foreach (string text in this.m_subRegions)
				{
					this.LoginLog("Received sub region " + text);
					this.SendCharacterListRequest(text);
				}
				this.SetLoginState(Login.eLoginState.BN_CHARACTER_LIST_WAIT);
				this.LoginUI.ShowConnectingPanel();
				RealmListView realmListView = this.LoginUI.RealmListView;
				realmListView.ClearBnRealmList();
				this.m_characterListStartTime = Time.timeSinceLevelLoad;
			}
		}

		private void SendCharacterListRequest(string subRegion)
		{
			this.LoginLog("Sending realm list ClientRequest for subregion " + subRegion);
			ClientRequest clientRequest = new ClientRequest();
			bnet.protocol.attribute.Attribute attribute = new bnet.protocol.attribute.Attribute();
			attribute.SetName(MobileBuild.GetCharacterListRequestName());
			Variant variant = new Variant();
			variant.SetStringValue(subRegion);
			attribute.SetValue(variant);
			clientRequest.AddAttribute(attribute);
			bnet.protocol.attribute.Attribute attribute2 = new bnet.protocol.attribute.Attribute();
			attribute2.SetName("Param_RealmListTicket");
			Variant variant2 = new Variant();
			variant2.SetBlobValue(this.m_realmListTicket);
			attribute2.SetValue(variant2);
			clientRequest.AddAttribute(attribute2);
			Login.CharacterListCallback characterListCallback = new Login.CharacterListCallback();
			characterListCallback.SubRegion = subRegion;
			BattleNet.Get().SendClientRequest(clientRequest, new RPCContextDelegate(characterListCallback.Callback));
			this.LoginLog("---------------------------Sent Character List request for subregion " + subRegion + " -------------------------------");
		}

		private void BnCharacterListWait()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			if (this.LoginUI.IsShowingCharacterListPanel())
			{
				if (Time.timeSinceLevelLoad > this.m_characterListStartTime + 30f)
				{
					this.m_characterListStartTime = Time.timeSinceLevelLoad;
					this.m_clearCharacterListOnReply = true;
					foreach (string subRegion in this.m_subRegions)
					{
						this.SendCharacterListRequest(subRegion);
					}
					this.LoginLog("Refreshing character list.");
				}
			}
			else if (!Singleton<Login>.instance.UseCachedCharacter() || Time.timeSinceLevelLoad > this.m_characterListStartTime + 10f)
			{
				this.LoginLog("Displayed CharacterList panel after timeout.");
				this.LoginUI.ShowCharacterListPanel();
			}
		}

		public void SelectCharacterNew(JamJSONCharacterEntry characterEntry, string subRegion)
		{
			if (this.GetLoginState() == Login.eLoginState.MOBILE_LOGGED_IN_IDLE)
			{
				this.LoginUI.CloseCharacterListDialog();
			}
			this.m_selectedCharacterEntry = characterEntry;
			SecurePlayerPrefs.SetString("CharacterID", characterEntry.PlayerGuid, Main.uniqueIdentifier);
			SecurePlayerPrefs.SetString("CharacterName", characterEntry.Name, Main.uniqueIdentifier);
			PlayerPrefs.Save();
			Login.eLoginState loginState = Singleton<Login>.instance.GetLoginState();
			if (loginState == Login.eLoginState.MOBILE_LOGGED_IN_IDLE || loginState == Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE)
			{
				this.ReconnectToMobileServerCachedCharacter();
			}
			else
			{
				this.SendRealmJoinRequest("dummyRealmName", (ulong)characterEntry.VirtualRealmAddress, subRegion);
				Singleton<Login>.instance.SetLoginState(Login.eLoginState.BN_REALM_JOIN_WAIT);
				this.LoginUI.ShowConnectingPanel();
			}
			Singleton<CharacterData>.Instance.CopyCharacterEntry(this.m_selectedCharacterEntry);
		}

		public void SelectRecentCharacter(RecentCharacter recentChar, string subRegion)
		{
			SecurePlayerPrefs.SetString("WebToken", recentChar.WebToken, Main.uniqueIdentifier);
			SecurePlayerPrefs.SetString("GameAccountHigh", recentChar.GameAccount.High.ToString(), Main.uniqueIdentifier);
			SecurePlayerPrefs.SetString("GameAccountLow", recentChar.GameAccount.Low.ToString(), Main.uniqueIdentifier);
			SecurePlayerPrefs.SetString("CharacterID", recentChar.Entry.PlayerGuid, Main.uniqueIdentifier);
			SecurePlayerPrefs.SetString("CharacterName", recentChar.Entry.Name, Main.uniqueIdentifier);
			PlayerPrefs.Save();
			if (Network.IsConnected())
			{
				Network.Logout();
				Network.Disconnect();
			}
			this.BnLoginStart(true, true, true, false);
		}

		private void RemoveWebTokenFromCaches()
		{
			RecentCharacter recentCharacter = null;
			foreach (RecentCharacter recentCharacter2 in this.m_recentCharacters)
			{
				if (recentCharacter2.WebToken == this.m_webToken)
				{
					recentCharacter = recentCharacter2;
				}
			}
			if (recentCharacter != null)
			{
				this.m_recentCharacters.Remove(recentCharacter);
				this.SaveRecentCharacters();
			}
		}

		public void SendRealmJoinRequest(string realmName, ulong realmAddress, string subRegion)
		{
			if (this.m_realmJoinInfo != null && !string.IsNullOrEmpty(this.m_realmJoinInfo.IpAddress))
			{
				return;
			}
			this.m_subRegion = subRegion;
			ClientRequest clientRequest = new ClientRequest();
			bnet.protocol.attribute.Attribute attribute = new bnet.protocol.attribute.Attribute();
			attribute.SetName(MobileBuild.GetRealmJoinRequestName());
			Variant variant = new Variant();
			variant.SetStringValue(subRegion);
			attribute.SetValue(variant);
			clientRequest.AddAttribute(attribute);
			bnet.protocol.attribute.Attribute attribute2 = new bnet.protocol.attribute.Attribute();
			attribute2.SetName("Param_RealmAddress");
			Variant variant2 = new Variant();
			variant2.SetUintValue(realmAddress);
			attribute2.SetValue(variant2);
			clientRequest.AddAttribute(attribute2);
			bnet.protocol.attribute.Attribute attribute3 = new bnet.protocol.attribute.Attribute();
			attribute3.SetName("Param_RealmListTicket");
			Variant variant3 = new Variant();
			variant3.SetBlobValue(this.m_realmListTicket);
			attribute3.SetValue(variant3);
			clientRequest.AddAttribute(attribute3);
			bnet.protocol.attribute.Attribute attribute4 = new bnet.protocol.attribute.Attribute();
			attribute4.SetName("Param_BnetSessionKey");
			Variant variant4 = new Variant();
			variant4.SetBlobValue(BattleNet.GetSessionKey());
			attribute4.SetValue(variant4);
			clientRequest.AddAttribute(attribute4);
			this.m_realmJoinInfo = new Login.RealmJoinInfo();
			this.m_realmJoinInfo.RealmAddress = realmAddress;
			BattleNet.Get().SendClientRequest(clientRequest, new RPCContextDelegate(this.m_realmJoinInfo.ClientResponseCallback));
			PlayerPrefs.Save();
			this.LoginLog("----------------------------Sent realm join info request------------------------------");
		}

		private void BnRealmJoinWait()
		{
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
		}

		public static string DecompressJsonAttribBlob(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream(data);
			byte[] array = new byte[4];
			memoryStream.Read(array, 0, 4);
			int num = BitConverter.ToInt32(array, 0);
			Inflater inflater = new Inflater(false);
			InflaterInputStream inflaterInputStream = new InflaterInputStream(memoryStream, inflater);
			byte[] array2 = new byte[num];
			int num2 = 0;
			int num3 = array2.Length;
			try
			{
				for (;;)
				{
					int num4 = inflaterInputStream.Read(array2, num2, num3);
					if (num4 <= 0)
					{
						break;
					}
					num2 += num4;
					num3 -= num4;
				}
			}
			catch (Exception)
			{
				return null;
			}
			if (num2 != num)
			{
				return null;
			}
			string @string = Encoding.UTF8.GetString(array2);
			string text = @string.Substring(@string.IndexOf(':') + 1);
			return text.Substring(0, text.Length - 1);
		}

		private void BnLoginFailed(string popupTitle = null, string popupDescription = null)
		{
			this.LoginUI.HideAllPopups();
			BattleNet.Get().RequestCloseAurora();
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			this.LoginUI.ShowTitlePanel();
			if (popupTitle != null && popupDescription != null)
			{
				this.LoginUI.ShowGenericPopup(popupTitle, popupDescription);
			}
			else if (popupTitle != null)
			{
				this.LoginUI.ShowGenericPopupFull(popupTitle);
			}
			else if (this.m_battlenetFailures == 0)
			{
				GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.ReconnectPopupDisabledAction));
				if (Main.instance.GetLocale() == "enUS")
				{
					this.LoginUI.ShowGenericPopup("Battle.net Error", "Unable to contact Battle.net, tap anywhere to retry.");
				}
				else
				{
					this.LoginUI.ShowGenericPopup(StaticDB.GetString("NETWORK_ERROR", null), StaticDB.GetString("CANT_CONNECT", null));
				}
			}
			else
			{
				this.LoginUI.ShowGenericPopup(StaticDB.GetString("NETWORK_ERROR", null), StaticDB.GetString("CANT_CONNECT", null));
			}
			this.m_battlenetFailures++;
			this.SetLoginState(Login.eLoginState.IDLE);
			this.LoginLog("=================== BN Login Failed. " + this.m_battlenetFailures + " ===================");
		}

		private void ReconnectPopupDisabledAction()
		{
			GenericPopup.DisabledAction = (Action)Delegate.Remove(GenericPopup.DisabledAction, new Action(this.ReconnectPopupDisabledAction));
			if (Network.IsConnected())
			{
				Network.Logout();
				Network.Disconnect();
			}
			this.BnLoginStart(true, true, true, false);
		}

		public void BnQuit()
		{
			BattleNet.AppQuit();
		}

		public void ConnectToMobileServer(string mobileServerAddress, int mobileServerPort, string bnetAccount, ulong virtualRealmAddress, string wowAccount, byte[] realmJoinTicket, bool showConnectingPanel = true)
		{
			this.m_mobileServerAddress = mobileServerAddress;
			this.m_mobileServerPort = mobileServerPort;
			this.m_bnetAccount = bnetAccount;
			this.m_virtualRealmAddress = virtualRealmAddress;
			this.m_wowAccount = wowAccount;
			this.m_realmJoinTicket = realmJoinTicket;
			if (showConnectingPanel)
			{
				this.LoginUI.ShowConnectingPanel();
			}
			this.SetLoginState(Login.eLoginState.MOBILE_CONNECT);
			JamJSONRealmEntry jamJSONRealmEntry = this.m_realmEntries.FirstOrDefault((JamJSONRealmEntry entry) => (ulong)entry.WowRealmAddress == virtualRealmAddress);
			if (jamJSONRealmEntry != null)
			{
				MobileClient.SetCfgLanguagesID(jamJSONRealmEntry.CfgLanguagesID);
				MobileClient.SetCfgRealmsID(jamJSONRealmEntry.CfgRealmsID);
				MobileClient.SetPlayerRace((int)this.m_selectedCharacterEntry.RaceID);
			}
			BattleNet.Get().RequestCloseAurora();
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
		}

		public void ReconnectToMobileServerCharacterSelect()
		{
			if (Network.IsConnected())
			{
				Network.Logout();
				Network.Disconnect();
			}
			this.BnLoginStart(true, true, false, false);
		}

		public void ReconnectToMobileServerCachedCharacter()
		{
			if (Network.IsConnected())
			{
				Network.Logout();
				Network.Disconnect();
			}
			this.BnLoginStart(true, true, true, false);
		}

		private void MobileConnect()
		{
			this.LoginLog("Connecting to: " + this.m_mobileServerAddress);
			Network.SetLoginData(this.m_realmJoinTicket.ToList<byte>(), this.m_joinSecret.ToList<byte>(), this.m_clientSecret.ToList<byte>(), BattleNet.GetSessionKey().ToList<byte>(), BattleNet.GetMyAccountId().hi, BattleNet.GetMyAccountId().lo, BattleNet.GetMyBattleTag());
			Network.SetSelectedRealm(this.m_virtualRealmAddress);
			Network.Connect(this.m_mobileServerAddress, this.m_mobileServerPort);
			this.m_mobileLoginTime = Time.timeSinceLevelLoad;
			this.SetLoginState(Login.eLoginState.MOBILE_CONNECTING);
		}

		private void MobileConnecting()
		{
			if (Time.timeSinceLevelLoad > this.m_mobileLoginTime + this.m_mobileLoginTimeout)
			{
				this.LoginLog("MobileConnecting(): timeout exceeded while connecting");
				this.SetLoginState(Login.eLoginState.MOBILE_CONNECT_FAILED);
				return;
			}
			if (Network.IsConnected())
			{
				this.MobileConnected();
			}
		}

		private void MobileConnected()
		{
			this.LoginLog("============= Mobile Connected ==================");
			this.SetLoginState(Login.eLoginState.MOBILE_LOGGING_IN);
			this.m_loggingIn = false;
		}

		private void MobileConnectFailed()
		{
			this.LoginUI.ShowTitlePanel();
			this.SetLoginState(Login.eLoginState.IDLE);
		}

		public void SetJoinSecret(byte[] secret)
		{
			this.m_joinSecret = secret;
		}

		private void MobileLoggingIn()
		{
			if (!Network.IsConnected())
			{
				this.LoginLog("Mobile network disconnected.");
				this.MobileDisconnect(DisconnectReason.ConnectionLost);
				return;
			}
			if (Time.timeSinceLevelLoad > this.m_mobileLoginTime + this.m_mobileLoginTimeout)
			{
				this.LoginLog("Mobile login attempt timed out.");
				this.MobileDisconnect(DisconnectReason.TimeoutContactingServer);
				return;
			}
			if (Network.IsAuthorized())
			{
				if (!this.m_loggingIn)
				{
					this.LoginLog("Attempting to log in");
					Network.Login(Singleton<CharacterData>.Instance.PlayerGuid);
					this.m_loggingIn = true;
				}
				else if (Network.IsLoggedIn())
				{
					this.SetLoginState(Login.eLoginState.MOBILE_LOGGED_IN);
				}
			}
		}

		private void MobileLoggedIn()
		{
			Main.instance.MobileLoggedIn();
			this.SetLoginState(Login.eLoginState.MOBILE_LOGGED_IN_IDLE);
			this.LoginUI.HideAllPopups();
			this.m_loggingIn = false;
			this.m_mobileLoginTime = Time.timeSinceLevelLoad;
		}

		private void MobileLoggedInIdle()
		{
			if (Time.timeSinceLevelLoad > this.m_mobileLoginTime + this.m_mobileLoginTimeout)
			{
				this.LoginLog("Initial data request attempt timed out.");
				this.MobileDisconnect(DisconnectReason.TimeoutContactingServer);
			}
		}

		public void MobileLoginDataRequestComplete()
		{
			this.SetLoginState(Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE);
		}

		public void InitiateMobileDisconnect()
		{
			this.LoginLog("InitiateMobileDisconnect called");
			this.MobileDisconnect(DisconnectReason.Generic);
		}

		private void MobileDisconnect(DisconnectReason reason)
		{
			this.m_recentDisconnectReason = reason;
			this.LoginLog(string.Concat(new object[]
			{
				"Disconnecting for reason: ",
				reason,
				". Network connected: ",
				Network.IsConnected(),
				", authorized: ",
				Network.IsAuthorized(),
				", logged in: ",
				Network.IsLoggedIn()
			}));
			if (Network.IsConnected())
			{
				this.SetLoginState(Login.eLoginState.MOBILE_DISCONNECTING);
				Network.Logout();
				Network.Disconnect();
			}
			else
			{
				this.SetLoginState(Login.eLoginState.MOBILE_DISCONNECTED);
			}
		}

		private void MobileDisconnecting()
		{
			if (!Network.IsConnected())
			{
				this.SetLoginState(Login.eLoginState.MOBILE_DISCONNECTED);
			}
		}

		private void MobileDisconnected()
		{
			this.LoginLog("Disconnected");
			this.LoginUI.HideAllPopups();
			this.LoginUI.ShowTitlePanel();
			this.SetLoginState(Login.eLoginState.MOBILE_DISCONNECTED_IDLE);
			string text = null;
			string text2 = null;
			switch (this.m_recentDisconnectReason)
			{
			case DisconnectReason.ConnectionLost:
			case DisconnectReason.PingTimeout:
				text2 = StaticDB.GetString("DISCONNECTED_BY_SERVER", null);
				break;
			case DisconnectReason.TimeoutContactingServer:
			case DisconnectReason.Generic:
				text2 = StaticDB.GetString("CANT_CONNECT", null);
				break;
			case DisconnectReason.AppVersionOld:
				text = StaticDB.GetString("UPDATE_REQUIRED", null);
				text2 = StaticDB.GetString("UPDATE_REQUIRED_DESCRIPTION", null);
				break;
			case DisconnectReason.AppVersionNew:
				text = StaticDB.GetString("INCOMPATIBLE_REALM", null);
				text2 = StaticDB.GetString("INCOMPATIBLE_REALM_DESCRIPTION", null);
				break;
			case DisconnectReason.CharacterInWorld:
				text2 = StaticDB.GetString("ALREADY_LOGGED_IN", null);
				break;
			case DisconnectReason.CantEnterWorld:
				text2 = StaticDB.GetString("CHARACTER_UNAVAILABLE", null);
				break;
			case DisconnectReason.LoginDisabled:
				text2 = StaticDB.GetString("LOGIN_UNAVAILABLE", null);
				break;
			case DisconnectReason.TrialNotAllowed:
				text2 = StaticDB.GetString("TRIAL_LOGIN_UNAVAILABLE", null);
				break;
			case DisconnectReason.ConsumptionTimeNotAllowed:
				text2 = StaticDB.GetString("SUBSCRIPTION_REQUIRED", null);
				break;
			}
			if (text != null && text2 != null)
			{
				this.LoginUI.ShowGenericPopup(text, text2);
				if (SceneManager.GetActiveScene().name != Scenes.TitleSceneName)
				{
					GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.ReturnToTitleScene));
				}
			}
			else if (text2 != null)
			{
				this.LoginUI.ShowGenericPopupFull(text2);
				if (SceneManager.GetActiveScene().name != Scenes.TitleSceneName)
				{
					GenericPopup.DisabledAction = (Action)Delegate.Combine(GenericPopup.DisabledAction, new Action(this.ReturnToTitleScene));
				}
			}
			this.m_recentDisconnectReason = DisconnectReason.None;
		}

		public void ReturnToTitleScene()
		{
			GenericPopup.DisabledAction = (Action)Delegate.Remove(GenericPopup.DisabledAction, new Action(this.ReturnToTitleScene));
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(Singleton<Login>.instance.OnReturnToTitleScene);
			SceneManager.LoadSceneAsync(Scenes.TitleSceneName);
		}

		private void MobileDisconnectedByServer()
		{
			this.LoginLog("DisconnectedByServer");
			this.LoginUI.HideAllPopups();
			this.LoginUI.ShowTitlePanel();
			this.SetLoginState(Login.eLoginState.MOBILE_DISCONNECTED_IDLE);
			if (this.m_currentServerProtocolVersion >= 0)
			{
				if (this.m_currentServerProtocolVersion > this.m_expectedServerProtocolVersion)
				{
					this.LoginUI.ShowGenericPopup(StaticDB.GetString("UPDATE_REQUIRED", null), StaticDB.GetString("UPDATE_REQUIRED_DESCRIPTION", null));
				}
				else if (this.m_currentServerProtocolVersion < this.m_expectedServerProtocolVersion)
				{
					this.LoginUI.ShowGenericPopup(StaticDB.GetString("INCOMPATIBLE_REALM", null), StaticDB.GetString("INCOMPATIBLE_REALM_DESCRIPTION", null));
				}
				else
				{
					this.LoginUI.ShowGenericPopupFull(StaticDB.GetString("DISCONNECTED_BY_SERVER", null));
				}
			}
			else
			{
				this.LoginUI.ShowGenericPopupFull(StaticDB.GetString("DISCONNECTED_BY_SERVER", null));
			}
		}

		public void MobileConnectDestroy()
		{
			if (Network.IsConnected())
			{
				this.LoginLog("MobileConnectDestroy(): Disconnecting.");
				Network.Disconnect();
			}
		}

		public void BackToAccountSelect()
		{
			List<EntityId> gameAccountList = BattleNet.GetGameAccountList();
			if (gameAccountList == null)
			{
				this.MobileDisconnect(DisconnectReason.Generic);
				return;
			}
			if (gameAccountList.Count > 1 && this.m_loginState == Login.eLoginState.BN_CHARACTER_LIST_WAIT)
			{
				this.m_useCachedWoWAccount = false;
				this.BnLoginStart(true, false, false, false);
			}
			else
			{
				BattleNet.Get().RequestCloseAurora();
				BattleNet.ProcessAurora();
				this.BnErrorsUpdate();
				this.LoginUI.ShowTitlePanel();
				this.SetLoginState(Login.eLoginState.IDLE);
				this.m_useCachedWoWAccount = true;
			}
		}

		public void BackToTitle()
		{
			BattleNet.Get().RequestCloseAurora();
			BattleNet.ProcessAurora();
			this.BnErrorsUpdate();
			this.SetLoginState(Login.eLoginState.IDLE);
			this.LoginUI.ShowTitlePanel();
		}

		public void CancelLogin()
		{
			switch (this.m_loginState)
			{
			case Login.eLoginState.WEB_AUTH_START:
			case Login.eLoginState.WEB_AUTH_LOADING:
			case Login.eLoginState.WEB_AUTH_IN_PROGRESS:
			case Login.eLoginState.WEB_AUTH_FAILED:
			case Login.eLoginState.BN_LOGIN_START:
			case Login.eLoginState.BN_LOGIN_WAIT_FOR_LOGON:
			case Login.eLoginState.BN_LOGIN_PROVIDE_TOKEN:
			case Login.eLoginState.BN_LOGGING_IN:
			case Login.eLoginState.BN_TICKET_WAIT:
			case Login.eLoginState.BN_SUBREGION_LIST_WAIT:
			case Login.eLoginState.BN_CHARACTER_LIST_WAIT:
			case Login.eLoginState.BN_REALM_JOIN_WAIT:
			case Login.eLoginState.BN_LOGIN_UNKNOWN:
				BattleNet.Get().RequestCloseAurora();
				BattleNet.ProcessAurora();
				this.BnErrorsUpdate();
				this.SetLoginState(Login.eLoginState.IDLE);
				this.LoginUI.ShowTitlePanel();
				break;
			case Login.eLoginState.MOBILE_CONNECT:
			case Login.eLoginState.MOBILE_CONNECTING:
			case Login.eLoginState.MOBILE_CONNECT_FAILED:
			case Login.eLoginState.MOBILE_DISCONNECTING:
			case Login.eLoginState.MOBILE_DISCONNECTED:
			case Login.eLoginState.MOBILE_DISCONNECTED_BY_SERVER:
			case Login.eLoginState.MOBILE_DISCONNECTED_IDLE:
			case Login.eLoginState.MOBILE_LOGGING_IN:
			case Login.eLoginState.MOBILE_LOGGED_IN:
			case Login.eLoginState.MOBILE_LOGGED_IN_IDLE:
			case Login.eLoginState.MOBILE_LOGGED_IN_DATA_COMPLETE:
				Singleton<Login>.instance.ReconnectToMobileServerCharacterSelect();
				break;
			}
		}

		private void InitRecentCharacters()
		{
			this.m_recentCharacters = new List<RecentCharacter>();
		}

		private void LoadRecentCharacters()
		{
			for (int i = 0; i < 3; i++)
			{
				string @string = SecurePlayerPrefs.GetString("RecentCharacter" + i, Main.uniqueIdentifier);
				if (@string != null && @string != string.Empty)
				{
					object obj = MessageFactory.Deserialize(@string);
					if (obj is RecentCharacter)
					{
						RecentCharacter recentCharacter = (RecentCharacter)obj;
						if (recentCharacter.Version == 2)
						{
							this.m_recentCharacters.Add(recentCharacter);
						}
						else
						{
							PlayerPrefs.DeleteKey("RecentCharacter" + i);
						}
					}
				}
			}
		}

		private void SaveRecentCharacters()
		{
			int i = 0;
			while (i < this.m_recentCharacters.Count && i < 3)
			{
				string value = MessageFactory.Serialize(this.m_recentCharacters[i]);
				SecurePlayerPrefs.SetString("RecentCharacter" + i, value, Main.uniqueIdentifier);
				i++;
			}
			while (i < 3)
			{
				SecurePlayerPrefs.DeleteKey("RecentCharacter" + i);
				i++;
			}
			PlayerPrefs.Save();
		}

		public void UpdateRecentCharacters()
		{
			RecentCharacter recentCharacter = new RecentCharacter();
			recentCharacter.Entry = this.m_selectedCharacterEntry;
			recentCharacter.GameAccount = this.m_gameAccount;
			recentCharacter.UnixTime = GeneralHelpers.CurrentUnixTime();
			recentCharacter.WebToken = this.m_webToken;
			recentCharacter.SubRegion = this.m_subRegion;
			recentCharacter.Version = 2;
			RecentCharacter recentCharacter2 = null;
			foreach (RecentCharacter recentCharacter3 in this.m_recentCharacters)
			{
				if (recentCharacter3.Entry.PlayerGuid == recentCharacter.Entry.PlayerGuid)
				{
					recentCharacter2 = recentCharacter3;
				}
			}
			if (recentCharacter2 != null)
			{
				this.m_recentCharacters.Remove(recentCharacter2);
				this.m_recentCharacters.Add(recentCharacter);
			}
			else
			{
				RecentCharacter recentCharacter4 = null;
				int num = GeneralHelpers.CurrentUnixTime();
				foreach (RecentCharacter recentCharacter5 in this.m_recentCharacters)
				{
					if (recentCharacter5.UnixTime < num)
					{
						num = recentCharacter5.UnixTime;
						recentCharacter4 = recentCharacter5;
					}
				}
				if (recentCharacter4 != null && this.m_recentCharacters.Count >= 3)
				{
					this.m_recentCharacters.Remove(recentCharacter4);
				}
				this.m_recentCharacters.Add(recentCharacter);
			}
			this.SaveRecentCharacters();
			int i = 0;
			int num2 = 0;
			while (num2 < this.m_recentCharacters.Count && num2 < 3)
			{
				if (this.m_recentCharacters[num2].Entry.PlayerGuid != this.m_selectedCharacterEntry.PlayerGuid)
				{
					this.LoginUI.SetRecentCharacter(i++, this.m_recentCharacters[num2]);
				}
				num2++;
			}
			while (i < 3)
			{
				this.LoginUI.SetRecentCharacter(i, null);
				i++;
			}
		}

		private void CheckRecentCharacters(JamJSONCharacterEntry[] characters, string subRegion)
		{
			List<RecentCharacter> list = new List<RecentCharacter>();
			foreach (RecentCharacter recentCharacter in this.m_recentCharacters)
			{
				if (recentCharacter.SubRegion == subRegion)
				{
					JamJSONCharacterEntry jamJSONCharacterEntry = null;
					foreach (JamJSONCharacterEntry jamJSONCharacterEntry2 in characters)
					{
						if (jamJSONCharacterEntry2.PlayerGuid == recentCharacter.Entry.PlayerGuid)
						{
							jamJSONCharacterEntry = jamJSONCharacterEntry2;
							break;
						}
					}
					if (jamJSONCharacterEntry == null)
					{
						this.LoginLog("Removing recent character " + recentCharacter.Entry.Name + ", not found in character list.");
						list.Add(recentCharacter);
					}
					else if (jamJSONCharacterEntry.Name != recentCharacter.Entry.Name)
					{
						this.LoginLog("Removing recent character " + recentCharacter.Entry.Name + ", name doesn't match " + jamJSONCharacterEntry.Name);
						list.Add(recentCharacter);
					}
					else if (jamJSONCharacterEntry.VirtualRealmAddress != recentCharacter.Entry.VirtualRealmAddress)
					{
						this.LoginLog(string.Concat(new object[]
						{
							"Removing recent character ",
							recentCharacter.Entry.Name,
							", realm address changed from ",
							recentCharacter.Entry.VirtualRealmAddress,
							" to ",
							jamJSONCharacterEntry.VirtualRealmAddress
						}));
						list.Add(recentCharacter);
					}
				}
			}
			foreach (RecentCharacter item in list)
			{
				this.m_recentCharacters.Remove(item);
			}
			this.SaveRecentCharacters();
		}

		public void InvalidateRecentTokens()
		{
			int num = 0;
			while (num < this.m_recentCharacters.Count && num < 3)
			{
				StringBuilder stringBuilder = new StringBuilder(this.m_recentCharacters[num].WebToken);
				if (stringBuilder.Length > 10)
				{
					stringBuilder[6] = '0';
					stringBuilder[7] = '0';
					stringBuilder[8] = '0';
					stringBuilder[9] = '0';
					this.m_recentCharacters[num].WebToken = stringBuilder.ToString();
				}
				num++;
			}
			this.SaveRecentCharacters();
			StringBuilder stringBuilder2 = new StringBuilder(SecurePlayerPrefs.GetString("WebToken", Main.uniqueIdentifier));
			if (stringBuilder2.Length > 10)
			{
				stringBuilder2[6] = '0';
				stringBuilder2[7] = '0';
				stringBuilder2[8] = '0';
				stringBuilder2[9] = '0';
				SecurePlayerPrefs.SetString("WebToken", stringBuilder2.ToString(), Main.uniqueIdentifier);
			}
		}

		public string GetRealmName(uint virtualAddress)
		{
			JamJSONRealmEntry jamJSONRealmEntry = this.m_realmEntries.FirstOrDefault((JamJSONRealmEntry entry) => entry.WowRealmAddress == virtualAddress);
			return (jamJSONRealmEntry == null) ? string.Empty : jamJSONRealmEntry.Name;
		}

		private string GetNoInternetErrorText()
		{
			if (StaticDB.StringsAvailable())
			{
				return StaticDB.GetString("NO_INTERNET_CONNECTION", "[PH] No internet connection");
			}
			string bestGuessForLocale = MobileDeviceLocale.GetBestGuessForLocale();
			switch (bestGuessForLocale)
			{
			case "koKR":
				return "인터넷 연결이 끊겼습니다";
			case "frFR":
				return "Aucune connexion Internet";
			case "deDE":
				return "Keine Internetverbindung";
			case "zhCN":
				return "没有网络连接";
			case "zhTW":
				return "沒有網路連線";
			case "esES":
				return "No hay conexión a Internet";
			case "esMX":
				return "Sin conexión a internet";
			case "ruRU":
				return "Отсутствует подключение к Интернету";
			case "ptBR":
				return "Sem conexão com a internet";
			case "itIT":
				return "Connessione a internet assente.";
			}
			return "No internet connection";
		}

		private string m_webToken;

		private Login.eLoginState m_loginState = Login.eLoginState.WAIT_FOR_ASSET_BUNDLES;

		private WebAuth m_webAuth;

		private string m_webAuthUrl;

		private string m_mobileServerAddress;

		private int m_mobileServerPort;

		private string m_bnetAccount;

		private ulong m_virtualRealmAddress;

		private string m_wowAccount;

		private byte[] m_realmListTicket;

		private byte[] m_realmJoinTicket;

		private string[] m_subRegions;

		private string m_subRegion;

		private List<Login.LoginGameAccount> m_loginGameAccounts;

		private float m_characterListStartTime;

		private const float m_characterListRefreshWaitTime = 30f;

		private const float m_characterListDisplayTimeout = 10f;

		private Login.RealmJoinInfo m_realmJoinInfo;

		private float m_mobileLoginTime;

		private float m_mobileLoginTimeout = 25f;

		public DotNetUrlDownloader m_urlDownloader;

		private int m_expectedServerProtocolVersion = 4;

		private int m_currentServerProtocolVersion = -1;

		private bool m_useCachedLogin;

		private bool m_useCachedRealm;

		private bool m_useCachedCharacter;

		private bool m_useCachedWoWAccount = true;

		public const int m_numRecentChars = 3;

		public EntityId m_gameAccount;

		private JamJSONCharacterEntry m_selectedCharacterEntry;

		private int m_pauseTimestamp;

		private const int m_unpauseReconnectTime = 30;

		private List<RecentCharacter> m_recentCharacters;

		private const int m_recentCharacterVersion = 2;

		private byte[] m_joinSecret;

		private byte[] m_clientSecret;

		private DisconnectReason m_recentDisconnectReason;

		public bool m_clearCharacterListOnReply;

		private const float m_bnLoginTimeout = 20f;

		private float m_bnLoginStartTime;

		private bool m_retryWebAuthOnce;

		private int m_battlenetFailures;

		private bool m_initialUnpause = true;

		private bool m_loggingIn;

		private HashSet<JamJSONRealmEntry> m_realmEntries = new HashSet<JamJSONRealmEntry>();

		private LoginUI m_loginUI;

		public static string m_portal = "wow-dev";

		public static string[] m_devPortals = new string[]
		{
			"wow-dev",
			"st1",
			"st-us",
			"st2",
			"st-eu",
			"st3",
			"st-kr",
			"st5",
			"st-cn",
			"st21",
			"st22",
			"st23",
			"st25"
		};

		private class LoginGameAccount
		{
			public EntityId gameAccount;

			public string name;

			public bool isBanned;

			public bool isSuspended;
		}

		public enum eLoginState
		{
			IDLE,
			NO_NETWORK,
			WAIT_FOR_ASSET_BUNDLES,
			WEB_AUTH_START,
			WEB_AUTH_LOADING,
			WEB_AUTH_IN_PROGRESS,
			WEB_AUTH_FAILED,
			BN_LOGIN_START,
			BN_LOGIN_WAIT_FOR_LOGON,
			BN_LOGIN_PROVIDE_TOKEN,
			BN_LOGGING_IN,
			BN_ACCOUNT_NAME_WAIT,
			BN_TICKET_WAIT,
			BN_SUBREGION_LIST_WAIT,
			BN_CHARACTER_LIST_WAIT,
			BN_REALM_JOIN_WAIT,
			BN_LOGIN_UNKNOWN,
			MOBILE_CONNECT,
			MOBILE_CONNECTING,
			MOBILE_CONNECT_FAILED,
			MOBILE_DISCONNECTING,
			MOBILE_DISCONNECTED,
			MOBILE_DISCONNECTED_BY_SERVER,
			MOBILE_DISCONNECTED_IDLE,
			MOBILE_LOGGING_IN,
			MOBILE_LOGGED_IN,
			MOBILE_LOGGED_IN_IDLE,
			MOBILE_LOGGED_IN_DATA_COMPLETE,
			UPDATE_REQUIRED_START,
			UPDATE_REQUIRED_IDLE
		}

		private class GameAccountStateCallback
		{
			public EntityId EntityID
			{
				get
				{
					return this.m_entityID;
				}
				set
				{
					this.m_entityID = value;
				}
			}

			public void Callback(RPCContext context)
			{
				if (Singleton<Login>.instance == null || context == null || context.Payload == null || this.EntityID == null)
				{
					return;
				}
				Singleton<Login>.instance.LoginLog("GameAccountStateCallback called");
				GetGameAccountStateResponse getGameAccountStateResponse = GetGameAccountStateResponse.ParseFrom(context.Payload);
				if (getGameAccountStateResponse == null || getGameAccountStateResponse.State == null || getGameAccountStateResponse.State.GameLevelInfo == null || getGameAccountStateResponse.State.GameStatus == null || getGameAccountStateResponse.State.GameLevelInfo.Name == null)
				{
					return;
				}
				Singleton<Login>.instance.LoginLog(string.Concat(new object[]
				{
					"GameAccountStateCallback: Received name ",
					getGameAccountStateResponse.State.GameLevelInfo.Name,
					" for game account ",
					this.EntityID.Low
				}));
				if (getGameAccountStateResponse.State.GameStatus.IsBanned)
				{
					Singleton<Login>.instance.LoginLog(string.Concat(new object[]
					{
						"GameAccountStateCallback: Account ",
						getGameAccountStateResponse.State.GameLevelInfo.Name,
						", (",
						this.EntityID.Low,
						") is Banned!"
					}));
				}
				if (getGameAccountStateResponse.State.GameStatus.IsSuspended)
				{
					Singleton<Login>.instance.LoginLog(string.Concat(new object[]
					{
						"GameAccountStateCallback: Account ",
						getGameAccountStateResponse.State.GameLevelInfo.Name,
						", (",
						this.EntityID.Low,
						") is Suspended!"
					}));
				}
				Singleton<Login>.instance.AddGameAccountButton(this.EntityID, getGameAccountStateResponse.State.GameLevelInfo.Name, getGameAccountStateResponse.State.GameStatus.IsBanned, getGameAccountStateResponse.State.GameStatus.IsSuspended);
			}

			private EntityId m_entityID;
		}

		private class CharacterListCallback
		{
			public string SubRegion
			{
				get
				{
					return this.m_subRegion;
				}
				set
				{
					this.m_subRegion = value;
				}
			}

			public void Callback(RPCContext context)
			{
				Singleton<Login>.instance.LoginLog("--------- CharacterListCallback received for subregion " + this.SubRegion + " ----------");
				Singleton<Login>.instance.m_realmJoinInfo = null;
				ClientResponse clientResponseFromContext = GamesAPI.GetClientResponseFromContext(context);
				if (clientResponseFromContext != null)
				{
					if (Singleton<Login>.instance.m_clearCharacterListOnReply)
					{
						Singleton<Login>.instance.m_clearCharacterListOnReply = false;
						Singleton<Login>.instance.LoginUI.ClearCharacterList();
					}
					foreach (bnet.protocol.attribute.Attribute attribute in clientResponseFromContext.AttributeList)
					{
						if (attribute.Name == "Param_RealmList")
						{
							string value = Login.DecompressJsonAttribBlob(attribute.Value.BlobValue);
							this.m_updates = JsonConvert.DeserializeObject<JSONRealmListUpdates>(value);
						}
						else if (attribute.Name == "Param_CharacterList")
						{
							string value2 = Login.DecompressJsonAttribBlob(attribute.Value.BlobValue);
							this.m_characters = JsonConvert.DeserializeObject<JSONRealmCharacterList>(value2);
						}
					}
					string @string = SecurePlayerPrefs.GetString("CharacterID", Main.uniqueIdentifier);
					if (this.m_characters != null && this.m_updates != null)
					{
						foreach (JamJSONRealmEntry item in from update in this.m_updates.Updates
						select update.Update)
						{
							Singleton<Login>.instance.m_realmEntries.Add(item);
						}
						foreach (JamJSONCharacterEntry jamJSONCharacterEntry in this.m_characters.CharacterList)
						{
							bool flag = false;
							foreach (JamJSONRealmListUpdatePart jamJSONRealmListUpdatePart in this.m_updates.Updates)
							{
								if (jamJSONRealmListUpdatePart.Update.WowRealmAddress == jamJSONCharacterEntry.VirtualRealmAddress)
								{
									string name = jamJSONRealmListUpdatePart.Update.Name;
									bool online = jamJSONRealmListUpdatePart.Update.PopulationState != 0;
									Singleton<Login>.instance.LoginUI.AddCharacterButton(jamJSONCharacterEntry, this.SubRegion, name, online);
									flag = true;
									break;
								}
							}
							if (!flag)
							{
							}
							if (Singleton<Login>.instance.UseCachedCharacter() && @string != null && jamJSONCharacterEntry.PlayerGuid == @string && Singleton<Login>.Instance.GetLoginState() != Login.eLoginState.BN_REALM_JOIN_WAIT)
							{
								Singleton<Login>.instance.m_selectedCharacterEntry = jamJSONCharacterEntry;
								Singleton<Login>.instance.SendRealmJoinRequest("dummyRealmName", (ulong)jamJSONCharacterEntry.VirtualRealmAddress, this.SubRegion);
								Singleton<Login>.instance.SetLoginState(Login.eLoginState.BN_REALM_JOIN_WAIT);
								Singleton<CharacterData>.Instance.CopyCharacterEntry(jamJSONCharacterEntry);
							}
						}
						Singleton<Login>.instance.CheckRecentCharacters(this.m_characters.CharacterList, this.SubRegion);
					}
					Singleton<Login>.instance.LoginUI.SortCharacterList();
					return;
				}
				throw new Exception("ClientResponse was null in CharacterListCallback for sub region: " + this.SubRegion);
			}

			private string m_subRegion;

			private JSONRealmListUpdates m_updates;

			private JSONRealmCharacterList m_characters;
		}

		private class RealmJoinInfo
		{
			public ulong RealmAddress
			{
				get
				{
					return this.m_realmAddress;
				}
				set
				{
					this.m_realmAddress = value;
				}
			}

			public string IpAddress
			{
				get
				{
					return this.m_ipAddress;
				}
				set
				{
					this.m_ipAddress = value;
				}
			}

			public byte[] JoinTicket
			{
				get
				{
					return this.m_joinTicket;
				}
				set
				{
					this.m_joinTicket = value;
				}
			}

			public byte[] JoinSecret
			{
				get
				{
					return this.m_joinSecret;
				}
				set
				{
					this.m_joinSecret = value;
				}
			}

			public int Port { get; set; }

			public void ClientResponseCallback(RPCContext context)
			{
				ClientResponse clientResponseFromContext = GamesAPI.GetClientResponseFromContext(context);
				if (clientResponseFromContext != null)
				{
					foreach (bnet.protocol.attribute.Attribute attribute in clientResponseFromContext.AttributeList)
					{
						if (attribute.Name == "Param_RealmJoinTicket")
						{
							this.JoinTicket = attribute.Value.BlobValue;
						}
						else if (attribute.Name == "Param_ServerAddresses")
						{
							string value = Login.DecompressJsonAttribBlob(attribute.Value.BlobValue);
							JSONRealmListServerIPAddresses jsonrealmListServerIPAddresses = JsonConvert.DeserializeObject<JSONRealmListServerIPAddresses>(value);
							IEnumerable<JamJSONRealmListServerIPAddress> source = (from family in jsonrealmListServerIPAddresses.Families
							where (int)family.Family == (int)((sbyte)MobileBuild.GetSockAddrFamilyIpV4Enum())
							select family).SelectMany((JamJSONRealmListServerIPFamily family) => family.Addresses);
							JamJSONRealmListServerIPAddress jamJSONRealmListServerIPAddress = source.ElementAt(Random.Range(0, source.Count<JamJSONRealmListServerIPAddress>()));
							this.IpAddress = jamJSONRealmListServerIPAddress.Ip;
							this.Port = (int)jamJSONRealmListServerIPAddress.Port;
							Singleton<Login>.instance.LoginLog(string.Concat(new object[]
							{
								"RealmJoinInfo: Found ip ",
								this.IpAddress,
								", port ",
								this.Port
							}));
						}
						else if (attribute.Name == "Param_JoinSecret")
						{
							this.JoinSecret = attribute.Value.BlobValue;
							Singleton<Login>.instance.SetJoinSecret(attribute.Value.BlobValue);
						}
					}
					this.RealmJoinConnectToMobileServer();
					return;
				}
				throw new Exception("ClientResponse was null in RealmJoinCallback");
			}

			public void RealmJoinConnectToMobileServer()
			{
				if (this.IpAddress == null || this.IpAddress == string.Empty)
				{
					Singleton<Login>.instance.LoginLog("Couldn't connect to mobile server, ip address was blank.");
					Singleton<Login>.instance.SetLoginState(Login.eLoginState.MOBILE_CONNECT_FAILED);
					Singleton<Login>.instance.LoginUI.ShowGenericPopup(StaticDB.GetString("NETWORK_ERROR", null), StaticDB.GetString("CANT_CONNECT", null));
					return;
				}
				int num = this.Port;
				if (num == 0)
				{
					num = 6012;
				}
				string bnetAccount = string.Format("BNetAccount-0-{0:X12}", BattleNet.GetMyAccountId().lo);
				string wowAccount = string.Format("WowAccount-0-{0:X12}", Singleton<Login>.instance.m_gameAccount.Low);
				Singleton<Login>.instance.ConnectToMobileServer(this.IpAddress, num, bnetAccount, this.RealmAddress, wowAccount, this.JoinTicket, true);
			}

			private ulong m_realmAddress;

			private string m_ipAddress;

			private byte[] m_joinTicket;

			private byte[] m_joinSecret;
		}
	}
}
