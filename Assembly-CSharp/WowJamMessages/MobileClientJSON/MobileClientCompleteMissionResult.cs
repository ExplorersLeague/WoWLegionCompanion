using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4836, Name = "MobileClientCompleteMissionResult", Version = 33577221u)]
	public class MobileClientCompleteMissionResult
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionSuccessChance")]
		[FlexJamMember(Name = "missionSuccessChance", Type = FlexJamType.UInt8)]
		public byte MissionSuccessChance { get; set; }

		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		public JamGarrisonMobileMission Mission { get; set; }

		[FlexJamMember(Name = "bonusRollSucceeded", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "bonusRollSucceeded")]
		public bool BonusRollSucceeded { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "followerInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "followerInfo", Type = FlexJamType.Struct)]
		public JamGarrisonMissionFollowerInfo[] FollowerInfo { get; set; }
	}
}
