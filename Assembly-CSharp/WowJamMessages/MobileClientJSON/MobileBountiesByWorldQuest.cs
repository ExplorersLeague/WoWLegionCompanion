using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileBountiesByWorldQuest", Version = 39869590u)]
	public class MobileBountiesByWorldQuest
	{
		[System.Runtime.Serialization.DataMember(Name = "bountyQuestID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bountyQuestID", Type = FlexJamType.Int32)]
		public int[] BountyQuestID { get; set; }

		[FlexJamMember(Name = "questID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		public int QuestID { get; set; }
	}
}
