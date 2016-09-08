using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileWorldQuest", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileWorldQuest
	{
		[System.Runtime.Serialization.DataMember(Name = "item")]
		[FlexJamMember(ArrayDimensions = 1, Name = "item", Type = FlexJamType.Struct)]
		public MobileWorldQuestReward[] Item { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "endTime")]
		[FlexJamMember(Name = "endTime", Type = FlexJamType.Int32)]
		public int EndTime { get; set; }

		[FlexJamMember(Name = "worldMapAreaID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "worldMapAreaID")]
		public int WorldMapAreaID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "experience")]
		[FlexJamMember(Name = "experience", Type = FlexJamType.Int32)]
		public int Experience { get; set; }

		[FlexJamMember(Name = "startLocationMapID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "startLocationMapID")]
		public int StartLocationMapID { get; set; }

		[FlexJamMember(Name = "questID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		public int QuestID { get; set; }

		[FlexJamMember(Name = "questInfoID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questInfoID")]
		public int QuestInfoID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "money")]
		[FlexJamMember(Name = "money", Type = FlexJamType.Int32)]
		public int Money { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "startTime")]
		[FlexJamMember(Name = "startTime", Type = FlexJamType.Int32)]
		public int StartTime { get; set; }

		[FlexJamMember(Name = "startLocationY", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "startLocationY")]
		public int StartLocationY { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "currency", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "currency")]
		public MobileWorldQuestReward[] Currency { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "startLocationX")]
		[FlexJamMember(Name = "startLocationX", Type = FlexJamType.Int32)]
		public int StartLocationX { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "faction")]
		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		public MobileWorldQuestReward[] Faction { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "objective")]
		[FlexJamMember(ArrayDimensions = 1, Name = "objective", Type = FlexJamType.Struct)]
		public MobileWorldQuestObjective[] Objective { get; set; }

		[FlexJamMember(Name = "questTitle", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "questTitle")]
		public string QuestTitle { get; set; }
	}
}
