using System;

namespace WoWCompanionApp
{
	public class CommunityPendingInvite
	{
		public CommunityPendingInvite(ClubSelfInvitationInfo inviteInfo)
		{
			this.m_clubId = inviteInfo.club.clubId;
			this.m_clubName = inviteInfo.club.name;
			this.m_clubDescription = inviteInfo.club.description;
			this.m_inviter = inviteInfo.inviter.name;
		}

		public string CommunityName
		{
			get
			{
				return this.m_clubName;
			}
		}

		public string CommunityDescription
		{
			get
			{
				return this.m_clubDescription;
			}
		}

		public string Inviter
		{
			get
			{
				return this.m_inviter;
			}
		}

		public void AcceptInvite()
		{
			Club.AcceptInvitation(this.m_clubId);
		}

		public void DeclineInvite()
		{
			Club.DeclineInvitation(this.m_clubId);
		}

		private ulong m_clubId;

		private string m_clubName;

		private string m_clubDescription;

		private string m_inviter;
	}
}
