﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4838, Name = "MobileClientMissionAdded", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientMissionAdded
	{
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[FlexJamMember(Name = "missionSource", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "missionSource")]
		public byte MissionSource { get; set; }

		[FlexJamMember(Name = "canStartMission", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "canStartMission")]
		public bool CanStartMission { get; set; }

		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }
	}
}
