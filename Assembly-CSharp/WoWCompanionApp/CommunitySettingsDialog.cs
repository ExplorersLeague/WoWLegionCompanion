using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunitySettingsDialog : MonoBehaviour
	{
		public void InitializeCommunitySettings(CommunityButton communityButton)
		{
			this.m_community = communityButton.m_community;
			this.m_headerText.text = this.m_community.Name;
			this.m_markReadCallback = new Action(communityButton.UpdateNotifications);
			this.m_community.RefreshMemberList();
			this.m_community.RefreshStreams();
		}

		public void OpenRoleAndRanksMenu()
		{
			GameObject gameObject = Main.instance.AddChildToLevel2Canvas(this.m_communityRosterPrefab);
			CommunityMemberSettingsDialog component = gameObject.GetComponent<CommunityMemberSettingsDialog>();
			component.SetCommunityAndPopulateContent(this.m_community);
		}

		public void OpenNotificationSettingsDialog()
		{
			GameObject gameObject = Main.instance.AddChildToLevel2Canvas(this.m_notificationSettingsPrefab);
			gameObject.GetComponent<CommunityNotificationsDialog>().SetCommunity(this.m_community);
		}

		public void MarkAllAsRead()
		{
			if (this.m_community != null)
			{
				this.m_community.MarkAllAsRead();
				this.m_markReadCallback();
			}
		}

		public void LeaveCommunity()
		{
			this.m_community.LeaveClub();
		}

		public GameObject m_notificationSettingsPrefab;

		public GameObject m_communityRosterPrefab;

		public Text m_headerText;

		private Community m_community;

		private GameObject m_communityButton;

		private Action m_markReadCallback;
	}
}
