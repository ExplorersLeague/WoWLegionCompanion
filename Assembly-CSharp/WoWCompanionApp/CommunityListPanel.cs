using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityListPanel : MonoBehaviour
	{
		public void RefreshPanelContent()
		{
			this.m_scrollContent.DetachAllChildren();
			this.AddInvitationsToContent();
			this.AddCommunitiesToContent();
		}

		private void AddInvitationsToContent()
		{
			ReadOnlyCollection<CommunityPendingInvite> pendingInvites = CommunityData.Instance.GetPendingInvites();
			if (pendingInvites.Count > 0)
			{
				GameObject gameObject = this.m_scrollContent.AddAsChildObject(this.m_scrollSectionHeaderPrefab);
				gameObject.GetComponentInChildren<Text>().text = "PENDING INVITATIONS";
				GameObject gameObject2 = this.m_scrollContent.AddAsChildObject(this.m_inviteButtonPrefab);
				string text = (pendingInvites.Count <= 1) ? "1 INVITE" : "%s INVITATIONS";
				text = MobileClient.FormatString(text, pendingInvites.Count.ToString());
				gameObject2.GetComponentInChildren<Text>().text = text;
				gameObject2.GetComponentInChildren<Button>().onClick.AddListener(new UnityAction(this.OpenPendingInvitesDialog));
			}
		}

		private void AddCommunitiesToContent()
		{
			GameObject gameObject = this.m_scrollContent.AddAsChildObject(this.m_scrollSectionHeaderPrefab);
			string text = MobileClient.FormatString("%s'S COMMUNITIES", Singleton<CharacterData>.Instance.CharacterName.ToUpper());
			gameObject.GetComponentInChildren<Text>().text = text;
			CommunityData.Instance.ForEachCommunity(delegate(Community community)
			{
				GameObject gameObject2 = this.m_scrollContent.AddAsChildObject(this.m_communityButtonPrefab);
				gameObject2.GetComponent<CommunityButton>().SetCommunity(community);
			});
		}

		private void OpenPendingInvitesDialog()
		{
			Main.instance.AddChildToLevel2Canvas(this.m_pendingInvitesDialogPrefab);
		}

		public GameObject m_scrollContent;

		public GameObject m_communityButtonPrefab;

		public GameObject m_inviteButtonPrefab;

		public GameObject m_scrollSectionHeaderPrefab;

		public GameObject m_pendingInvitesDialogPrefab;
	}
}
