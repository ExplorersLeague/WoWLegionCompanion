using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4846, Name = "MobileClientCompleteMissionResult", Version = 39869590u)]
	public class MobileClientCompleteMissionResult
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionSuccessChance")]
		[FlexJamMember(Name = "missionSuccessChance", Type = FlexJamType.UInt8)]
		public byte MissionSuccessChance { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusRollSucceeded")]
		[FlexJamMember(Name = "bonusRollSucceeded", Type = FlexJamType.Bool)]
		public bool BonusRollSucceeded { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "followerInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "followerInfo", Type = FlexJamType.Struct)]
		public JamGarrisonMissionFollowerInfo[] FollowerInfo { get; set; }
	}
}
