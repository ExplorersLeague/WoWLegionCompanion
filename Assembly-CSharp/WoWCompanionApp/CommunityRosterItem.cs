using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityRosterItem : MonoBehaviour
	{
		public void PopulateMemberInfo(CommunityMember member)
		{
			this.m_characterName.text = member.Name;
			this.m_classImage.sprite = GeneralHelpers.LoadClassIcon((int)member.Class);
			this.m_memberInfo = member;
		}

		public Text m_characterName;

		public Image m_classImage;

		private CommunityMember m_memberInfo;
	}
}
