using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityRosterItem : RosterPageItem<CommunityMember>
	{
		public override void PopulateMemberInfo(CommunityMember member)
		{
			this.m_characterName.text = member.Name;
			this.m_classImage.sprite = GeneralHelpers.LoadClassIcon(member.Class);
			this.m_memberInfo = member;
			this.m_classImage.color = ((this.m_memberInfo.Presence != 1 && this.m_memberInfo.Presence != 2) ? CommunityRosterItem.OfflineColor : CommunityRosterItem.OnlineColor);
			this.m_characterName.color = ((this.m_memberInfo.Presence != 1 && this.m_memberInfo.Presence != 2) ? CommunityRosterItem.OfflineColor : CommunityRosterItem.OnlineColor);
			MeshGradient component = this.m_characterName.gameObject.GetComponent<MeshGradient>();
			if (component != null)
			{
				component.enabled = (this.m_memberInfo.Presence == 1);
			}
			if (this.m_ringIcon != null)
			{
				this.m_ringIcon.color = ((this.m_memberInfo.Presence != 1 && this.m_memberInfo.Presence != 2) ? CommunityRosterItem.OfflineColor : CommunityRosterItem.OnlineColor);
			}
			if (this.m_ownerIcon != null)
			{
				this.m_ownerIcon.gameObject.SetActive(this.m_memberInfo.Role == 1 || this.m_memberInfo.Role == 2);
			}
			if (this.m_moderatorIcon != null)
			{
				this.m_moderatorIcon.gameObject.SetActive(this.m_memberInfo.Role == 3);
			}
		}

		public Image m_ownerIcon;

		public Image m_moderatorIcon;

		public Image m_ringIcon;

		private static Color OfflineColor = new Color(0.5019608f, 0.5019608f, 0.5019608f, 1f);

		private static Color OnlineColor = Color.white;
	}
}
