using System;
using UnityEngine;
using WowJamMessages;
using WowJamMessages.JSONRealmList;

public class AllPanels : MonoBehaviour
{
	private void Awake()
	{
		AllPanels.instance = this;
		if (AssetBundleManager.instance.IsInitialized())
		{
			this.OnAssetBundleManagerInitialized();
		}
		else
		{
			AssetBundleManager assetBundleManager = AssetBundleManager.instance;
			assetBundleManager.InitializedAction = (Action)Delegate.Combine(assetBundleManager.InitializedAction, new Action(this.OnAssetBundleManagerInitialized));
		}
	}

	public void OnAssetBundleManagerInitialized()
	{
		this.SetConnectingPanelStatus(StaticDB.GetString("CONNECTING", null));
	}

	private void HideAllPanels(bool hideConnecting = true)
	{
		this.titlePanel.gameObject.SetActive(false);
		this.realmListPanel.gameObject.SetActive(false);
		this.createNewLoginPanel.gameObject.SetActive(false);
		if (hideConnecting)
		{
			this.connectingPanel.gameObject.SetActive(false);
		}
		this.downloadingPanel.gameObject.SetActive(false);
		this.characterListPanel.gameObject.SetActive(false);
		this.commonWoodBackground.SetActive(false);
		if (hideConnecting)
		{
			this.commonLegionWallpaper.SetActive(false);
		}
		this.m_orderHallMultiPanel.gameObject.SetActive(false);
		this.HideRecentCharacterPanel();
		this.m_webAuthPanel.gameObject.SetActive(false);
		this.m_missionResultsPanel.HideMissionResults();
	}

	public void ShowTitlePanel()
	{
		this.HideAllPanels(true);
		this.titlePanel.gameObject.SetActive(true);
		this.commonWoodBackground.SetActive(false);
		this.commonLegionWallpaper.SetActive(true);
		this.titlePanel.UpdateResumeButtonVisiblity();
	}

	public void OnClickTitleConnect()
	{
		if (Login.instance.HaveCachedWebToken())
		{
			AllPopups.instance.ShowLogoutConfirmationPopup(true);
		}
		else
		{
			Login.instance.StartNewLogin();
		}
	}

	public void OnClickTitleResume()
	{
		Login.instance.StartCachedLogin(true, false);
	}

	public void ShowAdventureMap()
	{
		this.HideAllPanels(true);
		this.ShowOrderHallMultiPanel(true);
	}

	public void ShowMissionList()
	{
		this.m_orderHallMultiPanel.ShowMissionListPanel();
	}

	public void ShowRealmListPanel()
	{
		this.HideAllPanels(true);
		this.realmListPanel.gameObject.SetActive(true);
		this.commonWoodBackground.SetActive(false);
		this.commonLegionWallpaper.SetActive(true);
	}

	public bool IsShowingRealmListPanel()
	{
		return this.realmListPanel.gameObject.activeSelf;
	}

	public void ShowCharacterListPanel()
	{
		this.connectingPanel.Hide();
		this.realmListPanel.gameObject.SetActive(false);
		this.characterListPanel.gameObject.SetActive(true);
		this.commonWoodBackground.SetActive(false);
		this.commonLegionWallpaper.SetActive(true);
	}

	public bool IsShowingCharacterListPanel()
	{
		return this.characterListPanel.gameObject.activeSelf;
	}

	public void ClearCharacterList()
	{
		this.characterListPanel.m_characterListView.ClearList();
	}

	public void AddCharacterButton(JamJSONCharacterEntry charData, string subRegion, string realmName, bool online)
	{
		this.characterListPanel.m_characterListView.AddCharacterButton(charData, subRegion, realmName, online);
	}

	public void SortCharacterList()
	{
		this.characterListPanel.m_characterListView.SortCharacterList();
	}

	public void ShowCreateNewLoginPanel()
	{
	}

	public void ShowConnectingPanel()
	{
		if (this.connectingPanel.gameObject.activeSelf)
		{
			return;
		}
		this.HideAllPanels(true);
		this.connectingPanel.gameObject.SetActive(true);
		this.commonWoodBackground.SetActive(false);
		this.commonLegionWallpaper.SetActive(true);
	}

	public void HideConnectingPanel()
	{
		this.HideAllPanels(true);
		this.connectingPanel.gameObject.SetActive(false);
	}

	public void SetConnectingPanelStatus(string statusText)
	{
		this.connectingPanel.m_statusText.text = statusText;
	}

	public void ShowConnectingPanelCancelButton(bool show)
	{
		this.connectingPanel.m_cancelButton.gameObject.SetActive(show);
	}

	public void ShowDownloadingPanel(bool show)
	{
		if (show)
		{
			this.HideAllPanels(true);
			this.commonLegionWallpaper.SetActive(true);
		}
		this.downloadingPanel.gameObject.SetActive(show);
	}

	public bool IsShowingDownloadingPanel()
	{
		return this.downloadingPanel.gameObject.activeSelf;
	}

	public void HidePanelsForWebAuth()
	{
		this.HideAllPanels(false);
	}

	public void OnClickBackToTitle()
	{
		Login.instance.BackToTitle();
	}

	public void OnClickBackToAccountSelect()
	{
		Login.instance.BackToAccountSelect();
	}

	public void OnClickCharacterSelectCancel()
	{
		AllPopups.instance.ShowLogoutConfirmationPopup(false);
	}

	public void OnClickConnectingCancel()
	{
		if (Login.instance.ReturnToRecentCharacter)
		{
			Login.instance.StartCachedLogin(true, true);
		}
		else
		{
			Login.instance.CancelLogin();
		}
	}

	public void ShowOrderHallMultiPanel(bool show)
	{
		this.m_orderHallMultiPanel.gameObject.SetActive(show);
		this.commonLegionWallpaper.SetActive(!show);
	}

	public void SetRecentCharacter(int index, RecentCharacter recentChar)
	{
		this.m_recentCharacterArea.SetRecentCharacter(index, recentChar);
	}

	private void HideRecentCharacterPanel()
	{
		if (AdventureMapPanel.instance != null)
		{
			AdventureMapPanel.instance.HideRecentCharacterPanel();
		}
	}

	public void ShowWebAuthPanel()
	{
		this.HideAllPanels(true);
		this.commonLegionWallpaper.SetActive(true);
		this.m_webAuthPanel.gameObject.SetActive(true);
	}

	public void HideWebAuthPanel()
	{
		this.m_webAuthPanel.gameObject.SetActive(false);
	}

	public void SetRegionIndex()
	{
		this.titlePanel.SetRegionIndex();
	}

	public void CancelRegionIndex()
	{
		this.titlePanel.CancelRegionIndex();
	}

	public static AllPanels instance;

	public TitlePanel titlePanel;

	public RealmListPanel realmListPanel;

	public CreateNewLoginPanel createNewLoginPanel;

	public ConnectingPanel connectingPanel;

	public DownloadingPanel downloadingPanel;

	public FollowerListView followerListView_SlideIn_Bottom;

	public FollowerListView followerListView_SlideIn_Left;

	public AdventureMapPanel adventureMapPanel;

	public CharacterListPanel characterListPanel;

	public GameObject commonWoodBackground;

	public GameObject commonLegionWallpaper;

	public OrderHallMultiPanel m_orderHallMultiPanel;

	public RecentCharacterArea m_recentCharacterArea;

	public WebAuthPanel m_webAuthPanel;

	public TroopsPanel m_troopsPanel;

	public TalentTreePanel m_talentTreePanel;

	public MissionResultsPanel m_missionResultsPanel;
}
