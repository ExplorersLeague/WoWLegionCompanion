using System;

namespace bgs
{
	public class PartyMember : OnlinePlayer
	{
		public bool HasRole(Enum role)
		{
			uint roleId = Convert.ToUInt32(role);
			return this.HasRole(roleId);
		}

		public bool HasRole(uint roleId)
		{
			if (this.RoleIds == null)
			{
				return false;
			}
			for (int i = 0; i < this.RoleIds.Length; i++)
			{
				if (this.RoleIds[i] == roleId)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsLeader(PartyType partyType)
		{
			uint leaderRoleId = PartyMember.GetLeaderRoleId(partyType);
			return this.HasRole(leaderRoleId);
		}

		public static uint GetLeaderRoleId(PartyType partyType)
		{
			if (partyType != PartyType.SPECTATOR_PARTY)
			{
				if (partyType != PartyType.FRIENDLY_CHALLENGE)
				{
				}
				return Convert.ToUInt32(BnetParty.FriendlyGameRoleSet.Inviter);
			}
			return Convert.ToUInt32(BnetParty.SpectatorPartyRoleSet.Leader);
		}

		public uint[] RoleIds = new uint[0];
	}
}
