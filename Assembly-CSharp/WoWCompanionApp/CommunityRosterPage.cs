using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class CommunityRosterPage : MonoBehaviour
	{
		public void AddMemberToRoster(CommunityMember member)
		{
			CommunityRosterItem communityRosterItem = this.m_contentPane.AddAsChildObject(this.m_memberButtonPrefab);
			communityRosterItem.PopulateMemberInfo(member);
		}

		public bool AtCapacity()
		{
			return this.m_contentPane.transform.childCount == this.m_pageCapacity;
		}

		public CommunityRosterItem m_memberButtonPrefab;

		public GameObject m_contentPane;

		public int m_pageCapacity;
	}
}
