using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonMissionFollowerInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonMissionFollowerInfo
	{
		[FlexJamMember(Name = "followerDBID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "followerDBID")]
		public ulong FollowerDBID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionCompleteState")]
		[FlexJamMember(Name = "missionCompleteState", Type = FlexJamType.UInt32)]
		public uint MissionCompleteState { get; set; }
	}
}
