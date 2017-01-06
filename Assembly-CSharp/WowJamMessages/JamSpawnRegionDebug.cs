using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamSpawnRegionDebug", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamSpawnRegionDebug
	{
		[FlexJamMember(Name = "pending", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "pending")]
		public int Pending { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numThresholdsHit")]
		[FlexJamMember(Name = "numThresholdsHit", Type = FlexJamType.Int32)]
		public int NumThresholdsHit { get; set; }

		[FlexJamMember(Name = "maxThreshold", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "maxThreshold")]
		public float MaxThreshold { get; set; }

		[FlexJamMember(Name = "numGroups", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "numGroups")]
		public int NumGroups { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "checkingThreshold")]
		[FlexJamMember(Name = "checkingThreshold", Type = FlexJamType.Bool)]
		public bool CheckingThreshold { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "isFarmed")]
		[FlexJamMember(Name = "isFarmed", Type = FlexJamType.Bool)]
		public bool IsFarmed { get; set; }

		[FlexJamMember(Name = "actual", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "actual")]
		public int Actual { get; set; }

		[FlexJamMember(Name = "minThreshold", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "minThreshold")]
		public float MinThreshold { get; set; }

		[FlexJamMember(Name = "regionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "regionID")]
		public int RegionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "players")]
		[FlexJamMember(ArrayDimensions = 1, Name = "players", Type = FlexJamType.Struct)]
		public JamSpawnRegionPlayerActivity[] Players { get; set; }
	}
}
