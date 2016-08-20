using System;

namespace bgs.types
{
	public struct PartyListenerEvent
	{
		public PartyError ToPartyError()
		{
			return new PartyError
			{
				IsOperationCallback = (this.Type == PartyListenerEventType.OPERATION_CALLBACK),
				DebugContext = this.StringData,
				ErrorCode = (BattleNetErrors)this.UintData,
				Feature = (BnetFeature)(this.UlongData >> 32),
				FeatureEvent = (BnetFeatureEvent)((uint)(this.UlongData & (ulong)-1)),
				PartyId = this.PartyId,
				szPartyType = this.StringData2,
				StringData = this.StringData
			};
		}

		public PartyListenerEventType Type;

		public PartyId PartyId;

		public BnetGameAccountId SubjectMemberId;

		public BnetGameAccountId TargetMemberId;

		public uint UintData;

		public ulong UlongData;

		public string StringData;

		public string StringData2;

		public byte[] BlobData;

		public enum AttributeChangeEvent_AttrType
		{
			ATTR_TYPE_NULL,
			ATTR_TYPE_LONG,
			ATTR_TYPE_STRING,
			ATTR_TYPE_BLOB
		}
	}
}
