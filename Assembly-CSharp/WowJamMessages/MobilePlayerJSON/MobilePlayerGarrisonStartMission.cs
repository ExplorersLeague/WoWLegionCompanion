using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4781, Name = "MobilePlayerGarrisonStartMission", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonStartMission
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "followerDBIDs", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "followerDBIDs")]
		public ulong[] FollowerDBIDs { get; set; }
	}
}
