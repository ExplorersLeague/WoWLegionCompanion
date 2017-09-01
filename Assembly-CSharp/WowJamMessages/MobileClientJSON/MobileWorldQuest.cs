using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileWorldQuest", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileWorldQuest
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "item", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "item")]
		public MobileWorldQuestReward[] Item { get; set; }

		[FlexJamMember(Name = "endTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "endTime")]
		public int EndTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "worldMapAreaID")]
		[FlexJamMember(Name = "worldMapAreaID", Type = FlexJamType.Int32)]
		public int WorldMapAreaID { get; set; }

		[FlexJamMember(Name = "experience", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "experience")]
		public int Experience { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "startLocationMapID")]
		[FlexJamMember(Name = "startLocationMapID", Type = FlexJamType.Int32)]
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

		[System.Runtime.Serialization.DataMember(Name = "startLocationY")]
		[FlexJamMember(Name = "startLocationY", Type = FlexJamType.Int32)]
		public int StartLocationY { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "currency", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "currency")]
		public MobileWorldQuestReward[] Currency { get; set; }

		[FlexJamMember(Name = "startLocationX", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "startLocationX")]
		public int StartLocationX { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "faction")]
		public MobileWorldQuestReward[] Faction { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "objective", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "objective")]
		public MobileWorldQuestObjective[] Objective { get; set; }

		[FlexJamMember(Name = "questTitle", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "questTitle")]
		public string QuestTitle { get; set; }
	}
}
