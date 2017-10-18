using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonMissionFollowerInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonMissionFollowerInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "followerDBID")]
		[FlexJamMember(Name = "followerDBID", Type = FlexJamType.UInt64)]
		public ulong FollowerDBID { get; set; }

		[FlexJamMember(Name = "missionCompleteState", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "missionCompleteState")]
		public uint MissionCompleteState { get; set; }
	}
}
