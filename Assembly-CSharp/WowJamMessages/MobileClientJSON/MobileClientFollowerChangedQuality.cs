using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4863, Name = "MobileClientFollowerChangedQuality", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerChangedQuality
	{
		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower OldFollower { get; set; }

		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		public JamGarrisonFollower Follower { get; set; }
	}
}
