using System;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityDescriptionDialog : BaseDialog
	{
		public void PopulateFromInvite(CommunityPendingInvite pendingInvite)
		{
			this.m_headerText.text = pendingInvite.CommunityName;
			this.m_membersText.text = StaticDB.GetString(this.COMMUNITIES_DESCRIPTION_MEMBERS_KEY, "[PH] Members: %d").Replace("%d", pendingInvite.CommunityMemberCount.ToString());
			this.m_leaderText.text = MobileClient.FormatString(StaticDB.GetString(this.COMMUNITIES_DESCRIPTION_INVITED_BY_KEY, "[PH] Invited by: %s"), pendingInvite.Inviter);
			this.m_descriptionText.text = pendingInvite.CommunityDescription;
		}

		public Text m_headerText;

		public Text m_membersText;

		public Text m_leaderText;

		public Text m_descriptionText;

		private readonly string COMMUNITIES_DESCRIPTION_MEMBERS_KEY = "COMMUNITIES_DESCRIPTION_MEMBERS";

		private readonly string COMMUNITIES_DESCRIPTION_INVITED_BY_KEY = "COMMUNITIES_DESCRIPTION_INVITED_BY";
	}
}
