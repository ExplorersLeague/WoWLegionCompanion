using System;
using UnityEngine;
using UnityEngine.Events;

namespace WoWCompanionApp
{
	public class SocialPanel : MonoBehaviour
	{
		private void Awake()
		{
			CommunityData.Instance.ClearData();
			CommunityData.Instance.RefreshCommunities();
			CommunityData.Instance.RefreshInvitations();
			CommunityData.OnCommunityRefresh += this.RefreshScrollingContent;
			CommunityData.OnInviteRefresh += this.RefreshScrollingContent;
		}

		private void OnDestroy()
		{
			CommunityData.OnCommunityRefresh -= this.RefreshScrollingContent;
			CommunityData.OnInviteRefresh -= this.RefreshScrollingContent;
		}

		private void OnEnable()
		{
			CommunityData.Instance.RefreshCommunities();
			CommunityData.Instance.RefreshInvitations();
			this.RefreshScrollingContent();
		}

		public void RefreshScrollingContent()
		{
			this.m_communitiesListPanel.GetComponent<CommunityListPanel>().RefreshPanelContent();
		}

		public void SelectCommunityButton(CommunityButton button)
		{
			ulong streamId = 0UL;
			string key = "DefaultChannel_" + button.m_community.ClubId.ToString();
			if (SecurePlayerPrefs.HasKey(key))
			{
				streamId = Convert.ToUInt64(SecurePlayerPrefs.GetString(key, Main.uniqueIdentifier));
			}
			CommunityStream defaultStream = button.m_community.GetDefaultStream(streamId);
			if (defaultStream == null)
			{
				Main.instance.AddChildToLevel2Canvas(this.m_noChannelsAvailableDialogPrefab);
			}
			else
			{
				SecurePlayerPrefs.SetString(key, defaultStream.StreamId.ToString(), Main.uniqueIdentifier);
				this.OpenChatPanel(button.m_community, defaultStream);
			}
		}

		public void OpenChannelSelect(CommunityButton button)
		{
			GameObject gameObject = Main.instance.AddChildToLevel2Canvas(this.m_channelSelectDialogPrefab);
			CommunityChannelDialog component = gameObject.GetComponent<CommunityChannelDialog>();
			component.InitializeContentPane(button.m_community, new UnityAction<CommunityChannelButton>(this.SelectChannelButton), new UnityAction(gameObject.GetComponent<BaseDialog>().CloseDialog));
		}

		public void SelectChannelButton(CommunityChannelButton button)
		{
			SecurePlayerPrefs.SetString("DefaultChannel_" + button.Community.ClubId.ToString(), button.StreamId.ToString(), Main.uniqueIdentifier);
			this.OpenChatPanel(button.Community, button.Stream);
		}

		public void CloseChatPanel()
		{
			this.m_communitiesChatPanel.SetActive(false);
			this.m_communitiesListPanel.SetActive(true);
			this.RefreshScrollingContent();
		}

		private void OpenChatPanel(Community community, CommunityStream stream)
		{
			this.m_communitiesChatPanel.SetActive(true);
			this.m_communitiesChatPanel.GetComponent<CommunityChatPanel>().InitializeChatContent(community, stream);
			this.m_communitiesListPanel.SetActive(false);
		}

		public void OpenInviteLinkDialog()
		{
			Main.instance.AddChildToLevel2Canvas(this.m_inviteLinkDialogPrefab);
		}

		public GameObject m_communitiesChatPanel;

		public GameObject m_communitiesListPanel;

		public GameObject m_channelSelectDialogPrefab;

		public GameObject m_inviteLinkDialogPrefab;

		public GameObject m_pendingInvitationDialogPrefab;

		public GameObject m_noChannelsAvailableDialogPrefab;

		private const string LOOKUP_PREFIX = "DefaultChannel_";
	}
}
