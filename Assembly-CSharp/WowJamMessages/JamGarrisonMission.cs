using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonMission", Version = 28333852u)]
	public class JamGarrisonMission
	{
		public JamGarrisonMission()
		{
			this.Flags = 0u;
		}

		[System.Runtime.Serialization.DataMember(Name = "offerTime")]
		[FlexJamMember(Name = "offerTime", Type = FlexJamType.Int32)]
		public int OfferTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "successChance")]
		[FlexJamMember(Name = "successChance", Type = FlexJamType.Int32)]
		public int SuccessChance { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "travelDuration")]
		[FlexJamMember(Name = "travelDuration", Type = FlexJamType.Int32)]
		public int TravelDuration { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionRecID")]
		[FlexJamMember(Name = "missionRecID", Type = FlexJamType.Int32)]
		public int MissionRecID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionState")]
		[FlexJamMember(Name = "missionState", Type = FlexJamType.Int32)]
		public int MissionState { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "startTime")]
		[FlexJamMember(Name = "startTime", Type = FlexJamType.Int32)]
		public int StartTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dbID")]
		[FlexJamMember(Name = "dbID", Type = FlexJamType.UInt64)]
		public ulong DbID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "offerDuration")]
		[FlexJamMember(Name = "offerDuration", Type = FlexJamType.Int32)]
		public int OfferDuration { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionDuration")]
		[FlexJamMember(Name = "missionDuration", Type = FlexJamType.Int32)]
		public int MissionDuration { get; set; }
	}
}
