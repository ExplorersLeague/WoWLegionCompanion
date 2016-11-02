using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamSpawnRegionDebug", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamSpawnRegionDebug
	{
		[System.Runtime.Serialization.DataMember(Name = "pending")]
		[FlexJamMember(Name = "pending", Type = FlexJamType.Int32)]
		public int Pending { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numThresholdsHit")]
		[FlexJamMember(Name = "numThresholdsHit", Type = FlexJamType.Int32)]
		public int NumThresholdsHit { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxThreshold")]
		[FlexJamMember(Name = "maxThreshold", Type = FlexJamType.Float)]
		public float MaxThreshold { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numGroups")]
		[FlexJamMember(Name = "numGroups", Type = FlexJamType.Int32)]
		public int NumGroups { get; set; }

		[FlexJamMember(Name = "checkingThreshold", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "checkingThreshold")]
		public bool CheckingThreshold { get; set; }

		[FlexJamMember(Name = "isFarmed", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isFarmed")]
		public bool IsFarmed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "actual")]
		[FlexJamMember(Name = "actual", Type = FlexJamType.Int32)]
		public int Actual { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minThreshold")]
		[FlexJamMember(Name = "minThreshold", Type = FlexJamType.Float)]
		public float MinThreshold { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "regionID")]
		[FlexJamMember(Name = "regionID", Type = FlexJamType.Int32)]
		public int RegionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "players")]
		[FlexJamMember(ArrayDimensions = 1, Name = "players", Type = FlexJamType.Struct)]
		public JamSpawnRegionPlayerActivity[] Players { get; set; }
	}
}
