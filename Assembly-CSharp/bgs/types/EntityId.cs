using System;
using bnet.protocol;

namespace bgs.types
{
	public struct EntityId
	{
		public EntityId(EntityId copyFrom)
		{
			this.hi = copyFrom.hi;
			this.lo = copyFrom.lo;
		}

		public EntityId(EntityId protoEntityId)
		{
			this.hi = protoEntityId.High;
			this.lo = protoEntityId.Low;
		}

		public EntityId ToProtocol()
		{
			EntityId entityId = new EntityId();
			entityId.SetHigh(this.hi);
			entityId.SetLow(this.lo);
			return entityId;
		}

		public ulong hi;

		public ulong lo;
	}
}
