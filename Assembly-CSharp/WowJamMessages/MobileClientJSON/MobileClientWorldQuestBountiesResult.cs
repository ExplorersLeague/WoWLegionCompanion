using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4866, Name = "MobileClientWorldQuestBountiesResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientWorldQuestBountiesResult
	{
		[System.Runtime.Serialization.DataMember(Name = "bounty")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bounty", Type = FlexJamType.Struct)]
		public MobileWorldQuestBounty[] Bounty { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "visible")]
		[FlexJamMember(Name = "visible", Type = FlexJamType.Bool)]
		public bool Visible { get; set; }

		[FlexJamMember(Name = "lockedQuestID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "lockedQuestID")]
		public int LockedQuestID { get; set; }
	}
}
