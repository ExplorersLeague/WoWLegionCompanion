using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4782, Name = "MobilePlayerGarrisonStartMission", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonStartMission
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "followerDBIDs", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "followerDBIDs")]
		public ulong[] FollowerDBIDs { get; set; }
	}
}
