using System;
using bgs.types;
using bnet.protocol;

namespace bgs
{
	public class BnetGameAccountId : BnetEntityId
	{
		public new static BnetGameAccountId CreateFromEntityId(bgs.types.EntityId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.CopyFrom(src);
			return bnetGameAccountId;
		}

		public new static BnetGameAccountId CreateFromProtocol(bnet.protocol.EntityId src)
		{
			BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
			bnetGameAccountId.SetLo(src.Low);
			bnetGameAccountId.SetHi(src.High);
			return bnetGameAccountId;
		}

		public new BnetGameAccountId Clone()
		{
			return (BnetGameAccountId)base.Clone();
		}
	}
}
