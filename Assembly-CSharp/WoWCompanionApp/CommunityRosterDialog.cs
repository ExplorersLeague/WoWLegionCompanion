using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityRosterDialog : MonoBehaviour
	{
		public void Start()
		{
			this.m_searchInput.onValueChanged.AddListener(delegate(string input)
			{
				this.FilterRosterByString(input);
			});
			CommunityData.OnRosterRefresh += this.OnRosterRefresh;
			Club.OnClubRemoved += new Club.ClubRemovedHandler(this.OnClubRemoved);
		}

		private void OnDestroy()
		{
			CommunityData.OnRosterRefresh -= this.OnRosterRefresh;
			Club.OnClubRemoved -= new Club.ClubRemovedHandler(this.OnClubRemoved);
		}

		private void OnRosterRefresh(ulong clubID)
		{
			if (clubID == this.m_community.ClubId)
			{
				this.m_memberList = this.m_community.GetMemberList();
				this.RefreshRoster();
			}
		}

		private void OnClubRemoved(Club.ClubRemovedEvent clubRemovedEvent)
		{
			if (clubRemovedEvent.ClubID == this.m_community.ClubId)
			{
				base.GetComponent<BaseDialog>().CloseDialog();
			}
		}

		public void SetRosterData(Community community)
		{
			this.m_community = community;
			this.m_memberList = this.m_community.GetMemberList();
			this.m_headerText.text = this.m_community.Name.ToUpper();
			this.RefreshRoster();
		}

		public void ToggleShowOffline()
		{
			this.m_showOffline = !this.m_showOffline;
			this.RefreshRoster();
		}

		public void ToggleAlphabetical()
		{
			this.m_alphabeticalOrdering = !this.m_alphabeticalOrdering;
			this.RefreshRoster();
		}

		private void RefreshRoster()
		{
			this.AddMemberListToPanel(new List<CommunityMember>(this.m_memberList));
		}

		private void AddMemberListToPanel(List<CommunityMember> memberList)
		{
			this.ClearContentPanel();
			if (!this.m_showOffline)
			{
				memberList = this.FilterByPresence(memberList);
			}
			if (this.m_alphabeticalOrdering)
			{
				memberList = this.SortListAlphabetically(memberList);
			}
			GameObject gameObject = this.AddChildToObject(this.m_contentPanel, this.m_rosterItemPagePrefab);
			CommunityRosterPage component = gameObject.GetComponent<CommunityRosterPage>();
			foreach (CommunityMember member in memberList)
			{
				if (component.AtCapacity())
				{
					gameObject = this.AddChildToObject(this.m_contentPanel, this.m_rosterItemPagePrefab);
					component = gameObject.GetComponent<CommunityRosterPage>();
				}
				component.AddMemberToRoster(member);
			}
		}

		private void FilterRosterByString(string filterString)
		{
			if (string.IsNullOrEmpty(filterString))
			{
				this.RefreshRoster();
				return;
			}
			List<CommunityMember> list = new List<CommunityMember>(this.m_memberList);
			foreach (CommunityMember communityMember in this.m_memberList)
			{
				if (communityMember.Name.IndexOf(filterString, StringComparison.OrdinalIgnoreCase) < 0)
				{
					list.Remove(communityMember);
				}
			}
			this.AddMemberListToPanel(list);
		}

		private List<CommunityMember> SortListAlphabetically(List<CommunityMember> listToSort)
		{
			return (from member in listToSort
			orderby member.Name
			select member).ToList<CommunityMember>();
		}

		private List<CommunityMember> FilterByPresence(List<CommunityMember> listToSort)
		{
			List<CommunityMember> list = new List<CommunityMember>(listToSort);
			foreach (CommunityMember communityMember in listToSort)
			{
				if (communityMember.Presence == 3)
				{
					list.Remove(communityMember);
				}
			}
			return list;
		}

		private GameObject AddChildToObject(GameObject parentobj, GameObject prefabToCreate)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefabToCreate);
			gameObject.transform.SetParent(parentobj.transform);
			gameObject.transform.SetAsLastSibling();
			gameObject.transform.localScale = Vector3.one;
			return gameObject;
		}

		private void ClearContentPanel()
		{
			for (int i = this.m_contentPanel.transform.childCount - 1; i >= 0; i--)
			{
				Object.Destroy(this.m_contentPanel.transform.GetChild(i).gameObject);
			}
			this.m_contentPanel.transform.DetachChildren();
		}

		public GameObject m_rosterItemPagePrefab;

		public GameObject m_contentPanel;

		public InputField m_searchInput;

		public Text m_headerText;

		public bool m_showOffline;

		public bool m_alphabeticalOrdering;

		private ReadOnlyCollection<CommunityMember> m_memberList;

		private Community m_community;
	}
}
