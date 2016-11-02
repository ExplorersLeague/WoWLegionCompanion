using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileBountiesByWorldQuest", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileBountiesByWorldQuest
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "bountyQuestID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "bountyQuestID")]
		public int[] BountyQuestID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "questID")]
		[FlexJamMember(Name = "questID", Type = FlexJamType.Int32)]
		public int QuestID { get; set; }
	}
}
