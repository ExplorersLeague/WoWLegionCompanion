﻿using System;
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

		[FlexJamMember(Name = "numThresholdsHit", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "numThresholdsHit")]
		public int NumThresholdsHit { get; set; }

		[FlexJamMember(Name = "maxThreshold", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "maxThreshold")]
		public float MaxThreshold { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "numGroups")]
		[FlexJamMember(Name = "numGroups", Type = FlexJamType.Int32)]
		public int NumGroups { get; set; }

		[FlexJamMember(Name = "checkingThreshold", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "checkingThreshold")]
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

		[System.Runtime.Serialization.DataMember(Name = "regionID")]
		[FlexJamMember(Name = "regionID", Type = FlexJamType.Int32)]
		public int RegionID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "players", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "players")]
		public JamSpawnRegionPlayerActivity[] Players { get; set; }
	}
}
