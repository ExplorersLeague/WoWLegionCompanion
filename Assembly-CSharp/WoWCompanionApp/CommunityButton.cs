using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class CommunityButton : MonoBehaviour
	{
		private void Awake()
		{
			Club.OnStreamViewMarkerUpdated += new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
		}

		private void OnDestroy()
		{
			Club.OnStreamViewMarkerUpdated -= new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
		}

		public void SetCommunity(Community community)
		{
			this.m_community = community;
			this.m_communityName.text = community.Name;
			this.m_guildIcon.SetActive(this.m_community.IsGuild());
			this.m_communityIcon.gameObject.SetActive(!this.m_community.IsGuild());
			if (this.m_community.IsGuild())
			{
				this.m_guildIcon.GetComponent<Image>().sprite = ((GarrisonStatus.Faction() != PVP_FACTION.ALLIANCE) ? this.m_hordeGuildSprite : this.m_allianceGuildSprite);
			}
			else
			{
				this.m_communityIcon.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, (int)((this.m_community.AvatarId != 0u) ? this.m_community.AvatarId : ((uint)StaticDB.communityIconDB.GetRecord(1).IconFileID)));
			}
			this.m_communityBorder.SetActive(!this.m_community.IsGuild());
			this.m_guildBorder.SetActive(this.m_community.IsGuild());
			this.UpdateNotifications();
		}

		public void SelectCommunity()
		{
			this.m_community.PopulateCommunityInfo();
			base.gameObject.GetComponentInParent<SocialPanel>().SelectCommunityButton(this);
		}

		public void OpenChannelSelect()
		{
			this.m_community.PopulateCommunityInfo();
			base.gameObject.GetComponentInParent<SocialPanel>().OpenChannelSelect(this);
		}

		public ReadOnlyCollection<CommunityStream> GetStreamList()
		{
			return this.m_community.GetAllStreams();
		}

		public void OpenCommunitySettings()
		{
			GameObject gameObject = Main.instance.AddChildToLevel2Canvas(this.m_communitySettingsDialogPrefab);
			CommunitySettingsDialog component = gameObject.GetComponent<CommunitySettingsDialog>();
			component.InitializeCommunitySettings(this);
		}

		private void OnViewMarkerUpdated(Club.StreamViewMarkerUpdatedEvent markerEvent)
		{
			if (markerEvent.ClubID == this.m_community.ClubId)
			{
				this.UpdateNotifications();
			}
		}

		public void UpdateNotifications()
		{
			this.m_notificationImage.SetActive(this.m_community.HasUnreadMessages(null));
		}

		private void Update()
		{
			RectTransform rectTransform = base.GetComponentInChildren<TiledRandomTexture>().transform as RectTransform;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
		}

		public Text m_communityName;

		public Community m_community;

		public GameObject m_communitySettingsDialogPrefab;

		public GameObject m_notificationImage;

		public Image m_communityIcon;

		public GameObject m_communityBorder;

		public GameObject m_guildBorder;

		public Sprite m_allianceGuildSprite;

		public Sprite m_hordeGuildSprite;

		public GameObject m_guildIcon;
	}
}
