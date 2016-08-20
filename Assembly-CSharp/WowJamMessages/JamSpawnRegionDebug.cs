﻿using System;
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

		[FlexJamMember(Name = "isFarmed", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isFarmed")]
		public bool IsFarmed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "actual")]
		[FlexJamMember(Name = "actual", Type = FlexJamType.Int32)]
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
