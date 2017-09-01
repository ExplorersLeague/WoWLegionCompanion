using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4877, Name = "MobileClientWorldQuestBountiesResult", Version = 39869590u)]
	public class MobileClientWorldQuestBountiesResult
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "bounty", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "bounty")]
		public MobileWorldQuestBounty[] Bounty { get; set; }

		[FlexJamMember(Name = "visible", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "visible")]
		public bool Visible { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockedQuestID")]
		[FlexJamMember(Name = "lockedQuestID", Type = FlexJamType.Int32)]
		public int LockedQuestID { get; set; }
	}
}
