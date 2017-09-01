using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4873, Name = "MobileClientFollowerChangedQuality", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerChangedQuality
	{
		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower OldFollower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "follower")]
		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower Follower { get; set; }
	}
}
