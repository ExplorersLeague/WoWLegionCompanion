using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "SpawnTrackerData", Version = 28333852u)]
	public class SpawnTrackerData
	{
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "questID", Type = FlexJamType.Int32)]
		public int[] QuestID { get; set; }
	}
}
