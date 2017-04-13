using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4781, Name = "MobilePlayerGarrisonStartMission", Version = 38820897u)]
	public class MobilePlayerGarrisonStartMission
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "followerDBIDs")]
		[FlexJamMember(ArrayDimensions = 1, Name = "followerDBIDs", Type = FlexJamType.UInt64)]
		public ulong[] FollowerDBIDs { get; set; }
	}
}
