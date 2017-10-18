using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileBountiesByWorldQuest", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileBountiesByWorldQuest
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "bountyQuestID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "bountyQuestID")]
		public int[] BountyQuestID { get; set; }

		[FlexJamMember(Name = "questID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		public int QuestID { get; set; }
	}
}
