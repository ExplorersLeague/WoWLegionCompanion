using System;

namespace bgs
{
	public class PartyInfo
	{
		public PartyInfo()
		{
		}

		public PartyInfo(PartyId partyId, PartyType type)
		{
			this.Id = partyId;
			this.Type = type;
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", this.Type.ToString(), this.Id.ToString());
		}

		public PartyId Id;

		public PartyType Type;
	}
}
