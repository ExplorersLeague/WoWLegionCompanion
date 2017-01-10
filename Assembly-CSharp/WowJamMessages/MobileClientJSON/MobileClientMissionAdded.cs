using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4838, Name = "MobileClientMissionAdded", Version = 33577221u)]
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

		[System.Runtime.Serialization.DataMember(Name = "canStartMission")]
		[FlexJamMember(Name = "canStartMission", Type = FlexJamType.Bool)]
		public bool CanStartMission { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}
