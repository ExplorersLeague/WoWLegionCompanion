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
			if (this.m_community.IsGuild())
			{
				this.m_leaveCommunityButton.SetActive(false);
			}
			this.m_rolesAndRanksButton.SetActive(false);
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
			if (this.m_community.CanLeaveClub())
			{
				this.OpenLeaveCommunityDialog();
			}
			else if (this.m_community.CanDeleteClub())
			{
				this.OpenDeleteClubDialog();
			}
			else
			{
				this.OpenTransferOwnershipDialog();
			}
		}

		public void OpenLeaveCommunityDialog()
		{
			string messageKey = MobileClient.FormatString(StaticDB.GetString("COMMUNITIES_CONFIRM_LEAVE_COMMUNITY_DESCRIPTION", "COMMUNITIES_CONFIRM_LEAVE_COMMUNITY_DESCRIPTION"), this.m_community.Name);
			Singleton<DialogFactory>.Instance.CreateOKCancelDialog("COMMUNITIES_CONFIRM_LEAVE_COMMUNITY", messageKey, delegate
			{
				this.m_community.LeaveOrDestroyClub();
			}, null);
		}

		public void OpenDeleteClubDialog()
		{
			Singleton<DialogFactory>.Instance.CreateOKCancelDialog("COMMUNITIES_CONFIRM_LEAVE_AND_DESTROY_COMMUNITY", "COMMUNITIES_CONFIRM_LEAVE_AND_DESTROY_COMMUNITY_DESCRIPTION", delegate
			{
				this.m_community.LeaveOrDestroyClub();
			}, null);
		}

		public void OpenTransferOwnershipDialog()
		{
			AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("COMMUNITIES_LIST_TRANSFER_OWNERSHIP_FIRST", "COMMUNITIES_LIST_TRANSFER_OWNERSHIP_FIRST"));
		}

		public GameObject m_notificationSettingsPrefab;

		public GameObject m_communityRosterPrefab;

		public Text m_headerText;

		public GameObject m_rolesAndRanksButton;

		public GameObject m_leaveCommunityButton;

		private Community m_community;

		private GameObject m_communityButton;

		private Action m_markReadCallback;
	}
}
