using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class EventInviteListItem : RosterPageItem<CalendarEventInviteInfo>
	{
		public override void PopulateMemberInfo(CalendarEventInviteInfo invite)
		{
			this.m_memberInfo = invite;
			this.m_nameText.text = invite.name;
			ChrClassesRec recordFirstOrDefault = StaticDB.chrClassesDB.GetRecordFirstOrDefault((ChrClassesRec rec) => rec.Name.Equals(invite.className, StringComparison.OrdinalIgnoreCase));
			if (recordFirstOrDefault != null)
			{
				this.m_classIcon.sprite = GeneralHelpers.LoadClassIcon(new uint?((uint)recordFirstOrDefault.ID));
			}
			if (invite.inviteIsMine)
			{
				this.m_classIconRing.sprite = this.m_goldRingSprite;
			}
			if (invite.modStatus == "CREATOR")
			{
				this.m_ownerIcon.gameObject.SetActive(true);
			}
			else if (invite.modStatus == "MODERATOR")
			{
				this.m_moderatorIcon.gameObject.SetActive(true);
			}
			this.m_checkMark.gameObject.SetActive(CalendarStatusExtensions.IsAttending(invite.inviteStatus));
			this.m_questionMark.gameObject.SetActive(invite.inviteStatus == 8u);
			this.m_xMark.gameObject.SetActive(invite.inviteStatus == 2u);
		}

		public Text m_nameText;

		public Image m_classIcon;

		public Image m_classIconRing;

		public Image m_ownerIcon;

		public Image m_moderatorIcon;

		public Image m_checkMark;

		public Image m_questionMark;

		public Image m_xMark;

		public Sprite m_goldRingSprite;
	}
}
