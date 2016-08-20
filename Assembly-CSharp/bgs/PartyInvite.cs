using System;

namespace bgs
{
	public class PartyInvite
	{
		public PartyInvite()
		{
		}

		public PartyInvite(ulong inviteId, PartyId partyId, PartyType type)
		{
			this.InviteId = inviteId;
			this.PartyId = partyId;
			this.PartyType = type;
		}

		public uint GetFlags()
		{
			return (uint)this.InviteFlags;
		}

		public void SetFlags(uint flagsValue)
		{
			this.InviteFlags = (PartyInvite.Flags)flagsValue;
		}

		public bool IsRejoin
		{
			get
			{
				return (this.InviteFlags & PartyInvite.Flags.RESERVATION) == PartyInvite.Flags.RESERVATION;
			}
		}

		public bool IsReservation
		{
			get
			{
				return (this.InviteFlags & PartyInvite.Flags.RESERVATION) == PartyInvite.Flags.RESERVATION;
			}
		}

		public ulong InviteId;

		public PartyId PartyId;

		public PartyType PartyType;

		public string InviterName;

		public BnetGameAccountId InviterId;

		public BnetGameAccountId InviteeId;

		private PartyInvite.Flags InviteFlags;

		[Flags]
		public enum Flags
		{
			RESERVATION = 1,
			REJOIN = 1
		}
	}
}
