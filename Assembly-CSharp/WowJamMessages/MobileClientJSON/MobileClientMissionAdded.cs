using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4848, Name = "MobileClientMissionAdded", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientMissionAdded
	{
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionSource")]
		[FlexJamMember(Name = "missionSource", Type = FlexJamType.UInt8)]
		public byte MissionSource { get; set; }

		[FlexJamMember(Name = "canStartMission", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "canStartMission")]
		public bool CanStartMission { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}
