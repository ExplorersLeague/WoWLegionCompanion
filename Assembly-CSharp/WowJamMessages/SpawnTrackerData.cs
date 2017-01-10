using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "SpawnTrackerData", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class SpawnTrackerData
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "questID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		public int[] QuestID { get; set; }
	}
}
