using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "SpawnTrackerData", Version = 28333852u)]
	public class SpawnTrackerData
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "questID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		public int[] QuestID { get; set; }
	}
}
