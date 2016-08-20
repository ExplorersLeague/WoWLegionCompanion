using System;
using bgs.types;
using bnet.protocol;

namespace bgs
{
	public class BnetEntityId
	{
		public static BnetEntityId CreateFromEntityId(bgs.types.EntityId src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.CopyFrom(src);
			return bnetEntityId;
		}

		public static BnetEntityId CreateFromProtocol(bnet.protocol.EntityId src)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			bnetEntityId.SetLo(src.Low);
			bnetEntityId.SetHi(src.High);
			return bnetEntityId;
		}

		public static bgs.types.EntityId CreateEntityId(BnetEntityId src)
		{
			return new bgs.types.EntityId
			{
				hi = src.m_hi,
				lo = src.m_lo
			};
		}

		public static bnet.protocol.EntityId CreateForProtocol(BnetEntityId src)
		{
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(src.GetLo());
			entityId.SetHigh(src.GetHi());
			return entityId;
		}

		public BnetEntityId Clone()
		{
			return (BnetEntityId)base.MemberwiseClone();
		}

		public ulong GetHi()
		{
			return this.m_hi;
		}

		public void SetHi(ulong hi)
		{
			this.m_hi = hi;
		}

		public ulong GetLo()
		{
			return this.m_lo;
		}

		public void SetLo(ulong lo)
		{
			this.m_lo = lo;
		}

		public bool IsEmpty()
		{
			return (this.m_hi | this.m_lo) == 0UL;
		}

		public bool IsValid()
		{
			return this.m_lo != 0UL;
		}

		public void CopyFrom(bgs.types.EntityId id)
		{
			this.m_hi = id.hi;
			this.m_lo = id.lo;
		}

		public void CopyFrom(BnetEntityId id)
		{
			this.m_hi = id.m_hi;
			this.m_lo = id.m_lo;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			BnetEntityId bnetEntityId = obj as BnetEntityId;
			return bnetEntityId != null && this.m_hi == bnetEntityId.m_hi && this.m_lo == bnetEntityId.m_lo;
		}

		public bool Equals(BnetEntityId other)
		{
			return other != null && this.m_hi == other.m_hi && this.m_lo == other.m_lo;
		}

		public override int GetHashCode()
		{
			int num = 17;
			num = num * 11 + this.m_hi.GetHashCode();
			return num * 11 + this.m_lo.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("[hi={0} lo={1}]", this.m_hi, this.m_lo);
		}

		public static bool operator ==(BnetEntityId a, BnetEntityId b)
		{
			return object.ReferenceEquals(a, b) || (a != null && b != null && a.m_hi == b.m_hi && a.m_lo == b.m_lo);
		}

		public static bool operator !=(BnetEntityId a, BnetEntityId b)
		{
			return !(a == b);
		}

		protected ulong m_hi;

		protected ulong m_lo;
	}
}
