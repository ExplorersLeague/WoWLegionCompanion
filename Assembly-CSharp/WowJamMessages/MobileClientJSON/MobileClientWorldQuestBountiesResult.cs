using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4877, Name = "MobileClientWorldQuestBountiesResult", Version = 39869590u)]
	public class MobileClientWorldQuestBountiesResult
	{
		[System.Runtime.Serialization.DataMember(Name = "bounty")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bounty", Type = FlexJamType.Struct)]
		public MobileWorldQuestBounty[] Bounty { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "visible")]
		[FlexJamMember(Name = "visible", Type = FlexJamType.Bool)]
		public bool Visible { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockedQuestID")]
		[FlexJamMember(Name = "lockedQuestID", Type = FlexJamType.Int32)]
		public int LockedQuestID { get; set; }
	}
}
