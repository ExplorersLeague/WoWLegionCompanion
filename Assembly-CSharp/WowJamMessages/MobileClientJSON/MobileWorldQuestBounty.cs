﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileWorldQuestBounty", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileWorldQuestBounty
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "item", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "item")]
		public MobileWorldQuestReward[] Item { get; set; }

		[FlexJamMember(Name = "endTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "endTime")]
		public int EndTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numNeeded")]
		[FlexJamMember(Name = "numNeeded", Type = FlexJamType.Int32)]
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

		[System.Runtime.Serialization.DataMember(Name = "money")]
		[FlexJamMember(Name = "money", Type = FlexJamType.Int32)]
		public int Money { get; set; }

		[FlexJamMember(Name = "startTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "startTime")]
		public int StartTime { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "currency", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "currency")]
		public MobileWorldQuestReward[] Currency { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "faction", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "faction")]
		public MobileWorldQuestReward[] Faction { get; set; }
	}
}
