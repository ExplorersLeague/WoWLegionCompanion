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
				gameObject.GetComponentInChildren<Text>().text = StaticDB.GetString("COMMUNITIES_PENDING_INVITES", "[PH] PENDING INVITATIONS");
				GameObject gameObject2 = this.m_scrollContent.AddAsChildObject(this.m_inviteButtonPrefab);
				string text = StaticDB.GetString("COMMUNITIES_MULTIPLE_INVITATIONS", null);
				text = GeneralHelpers.QuantityRule(text, pendingInvites.Count);
				gameObject2.GetComponentInChildren<Text>().text = text;
				gameObject2.GetComponentInChildren<Button>().onClick.AddListener(new UnityAction(this.OpenPendingInvitesDialog));
			}
		}

		private void AddCommunitiesToContent()
		{
			if (CommunityData.Instance.HasGuild())
			{
				GameObject gameObject = this.m_scrollContent.AddAsChildObject(this.m_scrollSectionHeaderPrefab);
				string @string = StaticDB.GetString("SOCIAL_CHARACTERS_GUILD", "[PH] %s'S GUILD");
				string text = MobileClient.FormatString(@string, Singleton<CharacterData>.Instance.CharacterName);
				gameObject.GetComponentInChildren<Text>().text = text;
				CommunityData.Instance.ForGuild(delegate(Community guild)
				{
					GameObject gameObject3 = this.m_scrollContent.AddAsChildObject(this.m_communityButtonPrefab);
					gameObject3.GetComponent<CommunityButton>().SetCommunity(guild);
				});
			}
			if (CommunityData.Instance.HasCommunities())
			{
				GameObject gameObject2 = this.m_scrollContent.AddAsChildObject(this.m_scrollSectionHeaderPrefab);
				string string2 = StaticDB.GetString("SOCIAL_CHARACTERS_COMMUNITIES", "[PH] %s'S COMMUNITIES");
				string text2 = MobileClient.FormatString(string2, Singleton<CharacterData>.Instance.CharacterName);
				gameObject2.GetComponentInChildren<Text>().text = text2;
				CommunityData.Instance.ForEachCommunity(delegate(Community community)
				{
					GameObject gameObject3 = this.m_scrollContent.AddAsChildObject(this.m_communityButtonPrefab);
					gameObject3.GetComponent<CommunityButton>().SetCommunity(community);
				});
			}
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
