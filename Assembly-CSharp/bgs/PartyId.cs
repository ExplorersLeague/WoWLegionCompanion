using System;
using bgs.types;
using bnet.protocol;

namespace bgs
{
	public class PartyId
	{
		public PartyId()
		{
			ulong num = 0UL;
			this.Lo = num;
			this.Hi = num;
		}

		public PartyId(ulong highBits, ulong lowBits)
		{
			this.Set(highBits, lowBits);
		}

		public PartyId(bgs.types.EntityId partyEntityId)
		{
			this.Set(partyEntityId.hi, partyEntityId.lo);
		}

		public ulong Hi { get; private set; }

		public ulong Lo { get; private set; }

		public bool IsEmpty
		{
			get
			{
				return this.Hi == 0UL && this.Lo == 0UL;
			}
		}

		public void Set(ulong highBits, ulong lowBits)
		{
			this.Hi = highBits;
			this.Lo = lowBits;
		}

		public static implicit operator PartyId(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return null;
			}
			return new PartyId(entityId.GetHi(), entityId.GetLo());
		}

		public static bool operator ==(PartyId a, PartyId b)
		{
			if (a == null)
			{
				return b == null;
			}
			return b != null && a.Hi == b.Hi && a.Lo == b.Lo;
		}

		public static bool operator !=(PartyId a, PartyId b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj is PartyId)
			{
				return this == (PartyId)obj;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.Hi.GetHashCode() ^ this.Lo.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}-{1}", this.Hi, this.Lo);
		}

		public static PartyId FromEntityId(bgs.types.EntityId entityId)
		{
			return new PartyId(entityId);
		}

		public static PartyId FromBnetEntityId(BnetEntityId entityId)
		{
			return new PartyId(entityId.GetHi(), entityId.GetLo());
		}

		public static PartyId FromProtocol(bnet.protocol.EntityId protoEntityId)
		{
			return new PartyId(protoEntityId.High, protoEntityId.Low);
		}

		public bgs.types.EntityId ToEntityId()
		{
			return new bgs.types.EntityId
			{
				hi = this.Hi,
				lo = this.Lo
			};
		}

		public static readonly PartyId Empty = new PartyId(0UL, 0UL);
	}
}
