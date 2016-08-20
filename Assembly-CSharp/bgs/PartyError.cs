using System;

namespace bgs
{
	public struct PartyError
	{
		public PartyType PartyType
		{
			get
			{
				return BnetParty.GetPartyTypeFromString(this.szPartyType);
			}
		}

		public bool IsOperationCallback;

		public string DebugContext;

		public Error ErrorCode;

		public BnetFeature Feature;

		public BnetFeatureEvent FeatureEvent;

		public PartyId PartyId;

		public string szPartyType;

		public string StringData;
	}
}
