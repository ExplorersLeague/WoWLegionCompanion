using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class Main : MonoBehaviour
	{
		private void Awake()
		{
			Main.instance = this;
			this.m_enableNotifications = true;
			this.GenerateUniqueIdentifier();
			this.canvasAnimator = this.mainCanvas.GetComponent<Animator>();
			this.SetNarrowScreen();
		}

		private void Start()
		{
			Application.targetFrameRate = 30;
			GarrisonStatus.ArtifactKnowledgeLevel = 0;
			GarrisonStatus.ArtifactXpMultiplier = 1f;
			this.LoadPreferences();
			if (Singleton<Login>.Instance.GetLoginState() != Login.eLoginState.IDLE && Singleton<Login>.Instance.GetLoginState() != Login.eLoginState.NO_NETWORK)
			{
				this.OnBeginConnection();
			}
		}

		public void OnBeginConnection()
		{
			MobileClient.RegisterHandlers();
		}

		private void Update()
		{
			this.UpdateDebugText();
			this.UpdateCanvasOrientation();
		}

		private void LoadPreferences()
		{
			bool enable = this.m_UISound.IsSFXEnabled();
			bool.TryParse(SecurePlayerPrefs.GetString("EnableSFX", Main.uniqueIdentifier), out enable);
			this.m_UISound.EnableSFX(enable);
			bool.TryParse(SecurePlayerPrefs.GetString("EnableNotifications", Main.uniqueIdentifier), out this.m_enableNotifications);
		}

		private void UpdateCanvasOrientation()
		{
			if (Screen.width > Screen.height)
			{
				this.canvasAnimator.SetBool("isLandscape", true);
			}
			else
			{
				this.canvasAnimator.SetBool("isLandscape", false);
			}
		}

		public void MobileLoggedIn()
		{
			PersistentArmamentData.ClearData();
			PersistentBountyData.ClearData();
			PersistentEquipmentData.ClearData();
			PersistentFollowerData.ClearData();
			PersistentFollowerData.ClearPreMissionFollowerData();
			PersistentMissionData.ClearData();
			PersistentShipmentData.ClearData();
			PersistentTalentData.ClearData();
			MissionDataCache.ClearData();
			WorldQuestData.ClearData();
			ItemStatCache.instance.ClearItemStats();
			GarrisonStatus.Initialized = false;
			MobileClient.SetCommunityID(Singleton<CharacterData>.Instance.CommunityID);
			MobileClient.Initialize();
			Singleton<GarrisonWrapper>.Instance.MobileRequestData();
		}

		private void UpdateDebugText()
		{
			this.m_frameCount++;
		}

		public void OnQuitButton()
		{
			Application.Quit();
		}

		private void OnApplicationQuit()
		{
			if (Network.IsConnected() && Network.IsLoggedIn())
			{
				Network.Logout();
			}
			Singleton<Login>.instance.BnQuit();
			MobileClient.Shutdown();
		}

		private void OnDestroy()
		{
			if (SceneManager.GetActiveScene().name != Scenes.TitleSceneName)
			{
				CommunityData.Instance.Shutdown();
				MobileClient.Disconnect();
			}
		}

		public void ClearPendingNotifications()
		{
			LocalNotifications.ClearPending();
		}

		public void ScheduleNotifications()
		{
			this.ClearPendingNotifications();
			if (!Main.instance.m_enableNotifications)
			{
				return;
			}
			List<NotificationData> list = new List<NotificationData>();
			ICollection<WrapperGarrisonMission> values = PersistentMissionData.missionDictionary.Values;
			foreach (WrapperGarrisonMission wrapperGarrisonMission in values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && (GARR_FOLLOWER_TYPE)record.GarrFollowerTypeID == GarrisonStatus.GarrisonFollowerType)
				{
					if (wrapperGarrisonMission.MissionState == 1)
					{
						if ((record.Flags & 16u) == 0u)
						{
							TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
							TimeSpan timeRemaining = wrapperGarrisonMission.MissionDuration - t;
							list.Add(new NotificationData
							{
								notificationText = record.Name,
								timeRemaining = timeRemaining,
								notificationType = NotificationType.missionCompete
							});
						}
					}
				}
			}
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				CharShipmentRec record2 = StaticDB.charShipmentDB.GetRecord(wrapperCharacterShipment.ShipmentRecID);
				if (record2 == null)
				{
					Debug.LogError("Invalid Shipment ID: " + wrapperCharacterShipment.ShipmentRecID);
				}
				else
				{
					string notificationText = "Invalid";
					if (record2.GarrFollowerID > 0)
					{
						GarrFollowerRec record3 = StaticDB.garrFollowerDB.GetRecord((int)record2.GarrFollowerID);
						if (record3 == null)
						{
							Debug.LogError("Invalid Follower ID: " + record2.GarrFollowerID);
							continue;
						}
						int num = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record3.AllianceCreatureID : record3.HordeCreatureID;
						CreatureRec record4 = StaticDB.creatureDB.GetRecord(num);
						if (record4 == null)
						{
							Debug.LogError("Invalid Creature ID: " + num);
							continue;
						}
						notificationText = record4.Name;
					}
					if (record2.DummyItemID > 0)
					{
						ItemRec record5 = StaticDB.itemDB.GetRecord(record2.DummyItemID);
						if (record5 == null)
						{
							Debug.LogError("Invalid Item ID: " + record2.DummyItemID);
							continue;
						}
						notificationText = record5.Display;
					}
					TimeSpan t2 = GarrisonStatus.CurrentTime() - wrapperCharacterShipment.CreationTime;
					TimeSpan timeRemaining2 = wrapperCharacterShipment.ShipmentDuration - t2;
					list.Add(new NotificationData
					{
						notificationText = notificationText,
						timeRemaining = timeRemaining2,
						notificationType = NotificationType.workOrderReady
					});
				}
			}
			foreach (WrapperGarrisonTalent wrapperGarrisonTalent in PersistentTalentData.talentDictionary.Values)
			{
				if ((wrapperGarrisonTalent.Flags & 1) == 0)
				{
					if (!(wrapperGarrisonTalent.StartTime <= DateTime.UtcNow))
					{
						GarrTalentRec record6 = StaticDB.garrTalentDB.GetRecord(wrapperGarrisonTalent.GarrTalentID);
						if (record6 != null)
						{
							TimeSpan timeRemaining3 = TimeSpan.Zero;
							if ((wrapperGarrisonTalent.Flags & 2) == 0)
							{
								timeRemaining3 = TimeSpan.FromSeconds((double)record6.ResearchDurationSecs) - (GarrisonStatus.CurrentTime() - wrapperGarrisonTalent.StartTime);
							}
							else
							{
								timeRemaining3 = TimeSpan.FromSeconds((double)record6.RespecDurationSecs) - (GarrisonStatus.CurrentTime() - wrapperGarrisonTalent.StartTime);
							}
							list.Add(new NotificationData
							{
								notificationText = record6.Name,
								timeRemaining = timeRemaining3,
								notificationType = NotificationType.talentReady
							});
						}
					}
				}
			}
			int num2 = 0;
			foreach (NotificationData notificationData in from n in list
			orderby n.timeRemaining
			select n)
			{
				if (notificationData.notificationType == NotificationType.missionCompete)
				{
					LocalNotifications.ScheduleMissionCompleteNotification(notificationData.notificationText, ++num2, Convert.ToInt64(notificationData.timeRemaining.TotalSeconds));
				}
				if (notificationData.notificationType == NotificationType.workOrderReady)
				{
					LocalNotifications.ScheduleWorkOrderReadyNotification(notificationData.notificationText, ++num2, Convert.ToInt64(notificationData.timeRemaining.TotalSeconds));
				}
				if (notificationData.notificationType == NotificationType.talentReady)
				{
					LocalNotifications.ScheduleTalentResearchCompleteNotification(notificationData.notificationText, ++num2, Convert.ToInt64(notificationData.timeRemaining.TotalSeconds));
				}
				Debug.Log(string.Concat(new object[]
				{
					"Scheduling Notification for [",
					notificationData.notificationType,
					"] ",
					notificationData.notificationText,
					" (",
					num2,
					") in ",
					notificationData.timeRemaining.TotalSeconds,
					" seconds"
				}));
			}
		}

		public string GetLocale()
		{
			if (string.IsNullOrEmpty(this.m_locale))
			{
				this.m_locale = MobileDeviceLocale.GetBestGuessForLocale();
			}
			return this.m_locale;
		}

		public void SetDebugText(string newText)
		{
			this.m_debugText.text = newText;
			this.m_debugButton.SetActive(true);
		}

		public void HideDebugText()
		{
			this.m_debugButton.SetActive(false);
		}

		private void OnApplicationPause(bool pauseStatus)
		{
		}

		public void Logout()
		{
			MissionDataCache.ClearData();
			AllPopups.instance.HideAllPopups();
			AllPanels.instance.ShowOrderHallMultiPanel(false);
			AllPanels.instance.ShowCompanionMultiPanel(false);
			Singleton<Login>.instance.ReconnectToMobileServerCharacterSelect();
		}

		public void SelectCompanionNavButton(CompanionNavButton navButton)
		{
			if (this.CompanionNavButtonSelectionAction != null)
			{
				this.CompanionNavButtonSelectionAction(navButton);
			}
		}

		public void SelectOrderHallFilterOptionsButton(OrderHallFilterOptionsButton filterOptionsButton)
		{
			if (this.OrderHallfilterOptionsButtonSelectedAction != null)
			{
				this.OrderHallfilterOptionsButtonSelectedAction(filterOptionsButton);
			}
		}

		private static string getMd5Hash(string input)
		{
			if (input == string.Empty)
			{
				return string.Empty;
			}
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = md5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private void GenerateUniqueIdentifier()
		{
			bool flag = false;
			string text = string.Empty;
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.content.Context");
			string static2 = androidJavaClass2.GetStatic<string>("TELEPHONY_SERVICE");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getSystemService", new object[]
			{
				static2
			});
			bool flag2 = false;
			try
			{
				text = androidJavaObject.Call<string>("getDeviceId", new object[0]);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
				flag2 = true;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if ((flag2 && !flag) || (!flag2 && text == string.Empty && flag))
			{
				AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("android.provider.Settings$Secure");
				string static3 = androidJavaClass3.GetStatic<string>("ANDROID_ID");
				AndroidJavaObject androidJavaObject2 = @static.Call<AndroidJavaObject>("getContentResolver", new object[0]);
				text = androidJavaClass3.CallStatic<string>("getString", new object[]
				{
					androidJavaObject2,
					static3
				});
				if (text == null)
				{
					text = string.Empty;
				}
			}
			if (text == string.Empty)
			{
				string text2 = "00000000000000000000000000000000";
				try
				{
					StreamReader streamReader = new StreamReader("/sys/class/net/wlan0/address");
					text2 = streamReader.ReadLine();
					streamReader.Close();
				}
				catch (Exception ex2)
				{
					Debug.Log(ex2.ToString());
				}
				text = text2.Replace(":", string.Empty);
			}
			Main.uniqueIdentifier = Main.getMd5Hash(text);
		}

		public void CheatFastForwardOneHour()
		{
			GarrisonStatus.CheatFastForwardOneHour();
		}

		private void SetNarrowScreen()
		{
			float num = 1f;
			float num2 = (float)Screen.height;
			float num3 = (float)Screen.width;
			if (num3 > 0f && num2 > 0f)
			{
				if (num2 > num3)
				{
					num = num2 / num3;
				}
				else
				{
					num = num3 / num2;
				}
			}
			if (num > 1.9f)
			{
				this.m_narrowScreen = true;
			}
		}

		public bool IsNarrowScreen()
		{
			return this.m_narrowScreen;
		}

		public bool HasNotch()
		{
			return SystemInfo.deviceModel.StartsWith("Pixel 3", StringComparison.OrdinalIgnoreCase);
		}

		public void NudgeX(ref GameObject gameObj, float amount)
		{
			if (gameObj != null)
			{
				RectTransform component = gameObj.GetComponent<RectTransform>();
				if (component)
				{
					Vector2 anchoredPosition = component.anchoredPosition;
					anchoredPosition.x += amount;
					component.anchoredPosition = anchoredPosition;
				}
			}
		}

		public void NudgeY(ref GameObject gameObj, float amount)
		{
			if (gameObj != null)
			{
				RectTransform component = gameObj.GetComponent<RectTransform>();
				if (component)
				{
					Vector2 anchoredPosition = component.anchoredPosition;
					anchoredPosition.y += amount;
					component.anchoredPosition = anchoredPosition;
				}
			}
		}

		public GameObject AddChildToLevel2Canvas(GameObject prefab)
		{
			GameObject gameObject = this.InstantiateBasedOnPrefab(prefab);
			this.AddChildToObject(this.m_level2Canvas.transform, gameObject.transform);
			return gameObject;
		}

		public GameObject AddChildToLevel3Canvas(GameObject prefab)
		{
			GameObject gameObject = this.InstantiateBasedOnPrefab(prefab);
			this.AddChildToObject(this.m_level3Canvas.transform, gameObject.transform);
			return gameObject;
		}

		private GameObject InstantiateBasedOnPrefab(GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			RectTransform rectTransform = gameObject.transform as RectTransform;
			RectTransform rectTransform2 = prefab.transform as RectTransform;
			rectTransform.anchorMin = rectTransform2.anchorMin;
			rectTransform.anchorMax = rectTransform2.anchorMax;
			RectTransform rectTransform3 = rectTransform;
			Vector2 zero = Vector2.zero;
			rectTransform.offsetMax = zero;
			rectTransform3.offsetMin = zero;
			return gameObject;
		}

		private void AddChildToObject(Transform parent, Transform child)
		{
			child.SetParent(parent, false);
		}

		private int m_frameCount;

		public Canvas mainCanvas;

		public Canvas m_level2Canvas;

		public Canvas m_level3Canvas;

		private Animator canvasAnimator;

		public AllPanels allPanels;

		public AllPopups allPopups;

		private string m_unknownMsg;

		public UISound m_UISound;

		public bool m_enableNotifications;

		public static Main instance;

		public Action<OrderHallFilterOptionsButton> OrderHallfilterOptionsButtonSelectedAction;

		public Action<CompanionNavButton> CompanionNavButtonSelectionAction;

		public GameObject m_debugButton;

		public Text m_debugText;

		private string m_locale;

		public GameClient m_gameClientScript;

		public static string uniqueIdentifier;

		public CanvasBlurManager m_canvasBlurManager;

		public BackButtonManager m_backButtonManager;

		private bool m_narrowScreen;

		public interface SetCache
		{
			void SetCachePath(string cachePath);
		}
	}
}
