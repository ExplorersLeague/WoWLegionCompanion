using System;
using UnityEngine;
using WowJamMessages;
using WowJamMessages.JSONRealmList;

namespace WoWCompanionApp
{
	public class LoginUI : MonoBehaviour
	{
		public RealmListView RealmListView
		{
			get
			{
				return this.m_realmListPanel.m_realmListView;
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
			if (this.shouldShowConnectingPanel && StaticDB.StringsAvailable())
			{
				this.ShowConnectingPanel();
			}
		}

		private void HideAllPanels(bool hideConnecting = true)
		{
			if (this.m_titlePanel != null)
			{
				this.m_titlePanel.gameObject.SetActive(false);
			}
			if (this.m_realmListPanel != null)
			{
				this.m_realmListPanel.gameObject.SetActive(false);
			}
			if (this.m_createNewLoginPanel != null)
			{
				this.m_createNewLoginPanel.gameObject.SetActive(false);
			}
			if (this.m_downloadingPanel != null)
			{
				this.m_downloadingPanel.gameObject.SetActive(false);
			}
			if (this.m_characterListPanel != null)
			{
				this.m_characterListPanel.gameObject.SetActive(false);
			}
			if (this.m_webAuthPanel != null)
			{
				this.m_webAuthPanel.gameObject.SetActive(false);
			}
			if (this.m_regionConfirmation != null)
			{
				this.m_regionConfirmation.gameObject.SetActive(false);
			}
			if (hideConnecting)
			{
				if (this.m_connectingPanel != null)
				{
					this.m_connectingPanel.gameObject.SetActive(false);
				}
				if (this.m_commonLegionWallpaper != null)
				{
					this.m_commonLegionWallpaper.SetActive(false);
				}
				this.shouldShowConnectingPanel = false;
			}
		}

		public void HidePanelsForWebAuth()
		{
			this.HideAllPanels(false);
		}

		public void ShowWebAuthPanel()
		{
			this.HideAllPanels(true);
			this.m_commonLegionWallpaper.SetActive(true);
			this.m_webAuthPanel.gameObject.SetActive(true);
		}

		public void HideWebAuthPanel()
		{
			this.m_webAuthPanel.gameObject.SetActive(false);
		}

		public void SetRegionIndex()
		{
			this.m_titlePanel.SetRegionIndex();
		}

		public void CancelRegionIndex()
		{
			this.m_titlePanel.CancelRegionIndex();
		}

		public void ShowRegionConfirmationPopup(int index)
		{
			this.m_regionConfirmation.gameObject.SetActive(true);
		}

		public void ShowTitlePanel()
		{
			this.HideAllPanels(true);
			if (this.m_titlePanel != null)
			{
				this.m_titlePanel.gameObject.SetActive(true);
			}
			this.ShowLegionBackground();
			if (this.m_titlePanel != null)
			{
				this.m_titlePanel.UpdateResumeButtonVisiblity();
			}
		}

		public void HideAllPopups()
		{
			if (this.m_characterListPopup && this.m_characterListPopup.gameObject.activeInHierarchy)
			{
				this.CloseCharacterListDialog();
			}
			this.m_genericPopup.gameObject.SetActive(false);
			this.m_logoutConfirmation.gameObject.SetActive(false);
		}

		public bool IsShowingCharacterListPanel()
		{
			return this.m_characterListPanel == null || this.m_characterListPanel.gameObject.activeSelf;
		}

		public void ClearCharacterList()
		{
			if (this.m_characterListPanel != null)
			{
				this.m_characterListPanel.m_characterListView.ClearList();
			}
		}

		public void AddCharacterButton(JamJSONCharacterEntry charData, string subRegion, string realmName, bool online)
		{
			if (this.m_characterListPanel != null)
			{
				this.m_characterListPanel.m_characterListView.AddCharacterButton(charData, subRegion, realmName, online);
			}
			if (this.m_characterListPopup != null)
			{
				this.m_characterListPopup.m_characterListView.AddCharacterButton(charData, subRegion, realmName, online);
			}
		}

		public void SortCharacterList()
		{
			if (this.m_characterListPanel != null)
			{
				this.m_characterListPanel.m_characterListView.SortCharacterList();
			}
		}

		public void ShowConnectingPanel()
		{
			if (this.m_connectingPanel.gameObject.activeSelf)
			{
				return;
			}
			this.HideAllPanels(true);
			this.ShowLegionBackground();
			if (!StaticDB.StringsAvailable())
			{
				this.shouldShowConnectingPanel = true;
				return;
			}
			this.m_connectingPanel.gameObject.SetActive(true);
			this.shouldShowConnectingPanel = false;
		}

		public void HideConnectingPanel()
		{
			this.m_connectingPanel.gameObject.SetActive(false);
			this.shouldShowConnectingPanel = false;
		}

		public void ShowRealmListPanel()
		{
			this.HideAllPanels(true);
			this.m_realmListPanel.gameObject.SetActive(true);
			this.ShowLegionBackground();
		}

		public bool IsShowingRealmListPanel()
		{
			return this.m_realmListPanel.gameObject.activeSelf;
		}

		public void ShowCharacterListPanel()
		{
			this.m_connectingPanel.Hide();
			this.m_realmListPanel.gameObject.SetActive(false);
			this.m_characterListPanel.gameObject.SetActive(true);
			this.ShowLegionBackground();
		}

		public void SetRecentCharacter(int index, RecentCharacter recentChar)
		{
			if (this.m_recentCharacterArea != null)
			{
				this.m_recentCharacterArea.SetRecentCharacter(index, recentChar);
			}
		}

		public void CloseCharacterListDialog()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
			this.m_characterListPopup.gameObject.SetActive(false);
		}

		public void ShowGenericPopup(string headerText, string descriptionText)
		{
			this.HideAllPopups();
			this.m_genericPopup.SetText(headerText, descriptionText);
			this.m_genericPopup.gameObject.SetActive(true);
		}

		public bool IsGenericPopupShowing()
		{
			return this.m_genericPopup.gameObject.activeSelf;
		}

		public void ShowGenericPopupFull(string fullText)
		{
			this.HideAllPopups();
			this.m_genericPopup.SetFullText(fullText);
			this.m_genericPopup.gameObject.SetActive(true);
		}

		public void ShowLogoutConfirmationPopup(bool goToWebAuth)
		{
			this.m_logoutConfirmation.GoToWebAuth = goToWebAuth;
			this.m_logoutConfirmation.gameObject.SetActive(true);
		}

		public void ShowCreateNewLoginPanel()
		{
		}

		public void ShowDownloadingPanel(bool show)
		{
			if (show == this.m_downloadingPanel.gameObject.activeSelf)
			{
				return;
			}
			if (show)
			{
				this.HideAllPanels(true);
				this.ShowLegionBackground();
			}
			this.m_downloadingPanel.gameObject.SetActive(show);
		}

		private void ShowLegionBackground()
		{
			if (this.m_commonLegionWallpaper != null)
			{
				this.m_commonLegionWallpaper.SetActive(true);
			}
		}

		public void OnLoginButtonClicked()
		{
			Singleton<Login>.Instance.OnClickTitleResume();
		}

		public void OnAccountSelectButtonClicked()
		{
			Singleton<Login>.Instance.OnClickTitleConnect();
		}

		public void OnClickConnectingCancel()
		{
			Singleton<Login>.Instance.OnClickConnectingCancel();
		}

		public void OnClickCharacterSelectCancel()
		{
			Singleton<Login>.instance.OnClickCharacterSelectCancel();
		}

		public void ReturnToTitleScene()
		{
			Singleton<Login>.Instance.ReturnToTitleScene();
		}

		public ConnectingPanel m_connectingPanel;

		public RealmListPanel m_realmListPanel;

		public CharacterListPanel m_characterListPanel;

		public CharacterListPanel m_characterListPopup;

		public DownloadingPanel m_downloadingPanel;

		public CreateNewLoginPanel m_createNewLoginPanel;

		public TitlePanel m_titlePanel;

		public WebAuthPanel m_webAuthPanel;

		public RecentCharacterArea m_recentCharacterArea;

		public GenericPopup m_genericPopup;

		public LogoutConfirmation m_logoutConfirmation;

		public RegionConfirmation m_regionConfirmation;

		public GameObject m_commonLegionWallpaper;

		private bool shouldShowConnectingPanel;
	}
}
