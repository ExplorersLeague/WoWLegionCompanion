using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileWorldQuestBounty", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileWorldQuestBounty
	{
		[System.Runtime.Serialization.DataMember(Name = "item")]
		[FlexJamMember(ArrayDimensions = 1, Name = "item", Type = FlexJamType.Struct)]
		public MobileWorldQuestReward[] Item { get; set; }

		[FlexJamMember(Name = "endTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "endTime")]
		public int EndTime { get; set; }

		[FlexJamMember(Name = "numNeeded", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "numNeeded")]
		public int NumNeeded { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "experience")]
		[FlexJamMember(Name = "experience", Type = FlexJamType.Int32)]
		public int Experience { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "questID")]
		[FlexJamMember(Name = "questID", Type = FlexJamType.Int32)]
		public int QuestID { get; set; }

		[FlexJamMember(Name = "iconFileDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "iconFileDataID")]
		public int IconFileDataID { get; set; }

		[FlexJamMember(Name = "numCompleted", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "numCompleted")]
		public int NumCompleted { get; set; }

		[FlexJamMember(Name = "money", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "money")]
		public int Money { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "startTime")]
		[FlexJamMember(Name = "startTime", Type = FlexJamType.Int32)]
		public int StartTime { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "currency", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "currency")]
		public MobileWorldQuestReward[] Currency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "faction")]
		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		public MobileWorldQuestReward[] Faction { get; set; }
	}
}
