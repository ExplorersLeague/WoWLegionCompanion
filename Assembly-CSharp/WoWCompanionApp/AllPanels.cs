using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class AllPanels : MonoBehaviour
	{
		private void Awake()
		{
			AllPanels.instance = this;
			if (Singleton<AssetBundleManager>.instance.IsInitialized())
			{
				this.OnAssetBundleManagerInitialized();
			}
			else
			{
				AssetBundleManager assetBundleManager = Singleton<AssetBundleManager>.instance;
				assetBundleManager.InitializedAction = (Action)Delegate.Combine(assetBundleManager.InitializedAction, new Action(this.OnAssetBundleManagerInitialized));
			}
		}

		public void OnAssetBundleManagerInitialized()
		{
		}

		private void HideAllPanels(bool hideConnecting = true)
		{
			this.createNewLoginPanel.gameObject.SetActive(false);
			this.commonWoodBackground.SetActive(false);
			this.HideRecentCharacterPanel();
			if (hideConnecting)
			{
				this.commonLegionWallpaper.SetActive(false);
			}
			if (this.m_companionMultiPanel)
			{
				this.m_companionMultiPanel.gameObject.SetActive(false);
			}
			if (this.m_orderHallMultiPanel)
			{
				this.m_orderHallMultiPanel.gameObject.SetActive(false);
			}
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

		public void ShowOrderHallMultiPanel(bool show)
		{
			if (this.m_orderHallMultiPanel)
			{
				this.m_orderHallMultiPanel.gameObject.SetActive(show);
				this.commonLegionWallpaper.SetActive(!show);
			}
		}

		public bool ShowCompanionMultiPanel(bool show)
		{
			if (this.m_companionMultiPanel)
			{
				this.m_companionMultiPanel.gameObject.SetActive(show);
				this.commonLegionWallpaper.SetActive(!show);
				return true;
			}
			return false;
		}

		private void HideRecentCharacterPanel()
		{
			if (AdventureMapPanel.instance != null)
			{
				AdventureMapPanel.instance.HideRecentCharacterPanel();
			}
		}

		public static AllPanels instance;

		public CreateNewLoginPanel createNewLoginPanel;

		public DownloadingPanel downloadingPanel;

		public FollowerListView followerListView_SlideIn_Bottom;

		public FollowerListView followerListView_SlideIn_Left;

		public AdventureMapPanel adventureMapPanel;

		public GameObject commonWoodBackground;

		public GameObject commonLegionWallpaper;

		public OrderHallMultiPanel m_orderHallMultiPanel;

		public RecentCharacterArea m_recentCharacterArea;

		public TroopsPanel m_troopsPanel;

		public CharacterViewPanel m_characterViewPanel;

		public CompanionMultiPanel m_companionMultiPanel;
	}
}
