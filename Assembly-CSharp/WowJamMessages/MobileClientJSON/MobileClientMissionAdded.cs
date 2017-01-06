using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4838, Name = "MobileClientMissionAdded", Version = 33577221u)]
	public class MobileClientMissionAdded
	{
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "mission")]
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

		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }
	}
}
