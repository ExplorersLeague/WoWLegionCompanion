using System;

namespace WoWCompanionApp
{
	public class CommunityPendingInvite
	{
		public CommunityPendingInvite(ClubSelfInvitationInfo inviteInfo)
		{
			this.m_clubId = inviteInfo.club.clubId;
			this.CommunityName = inviteInfo.club.name;
			this.CommunityDescription = inviteInfo.club.description;
			this.CommunityMemberCount = inviteInfo.club.memberCount.GetValueOrDefault();
			this.Inviter = inviteInfo.inviter.name;
			this.AvatarId = inviteInfo.club.avatarId;
		}

		public string CommunityName { get; private set; }

		public string CommunityDescription { get; private set; }

		public uint CommunityMemberCount { get; private set; }

		public string Inviter { get; private set; }

		public uint AvatarId { get; private set; }

		public void AcceptInvite()
		{
			Club.AcceptInvitation(this.m_clubId);
		}

		public void DeclineInvite()
		{
			Club.DeclineInvitation(this.m_clubId);
		}

		private ulong m_clubId;
	}
}
