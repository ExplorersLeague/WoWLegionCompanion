using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4836, Name = "MobileClientCompleteMissionResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientCompleteMissionResult
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[FlexJamMember(Name = "missionSuccessChance", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "missionSuccessChance")]
		public byte MissionSuccessChance { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }

		[FlexJamMember(Name = "bonusRollSucceeded", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "bonusRollSucceeded")]
		public bool BonusRollSucceeded { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "followerInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "followerInfo", Type = FlexJamType.Struct)]
		public JamGarrisonMissionFollowerInfo[] FollowerInfo { get; set; }
	}
}
