using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4851, Name = "MobileClientFollowerChangedXP", Version = 39869590u)]
	public class MobileClientFollowerChangedXP
	{
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower Follower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "xpChange")]
		[FlexJamMember(Name = "xpChange", Type = FlexJamType.Int32)]
		public int XpChange { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "source")]
		[FlexJamMember(Name = "source", Type = FlexJamType.Int32)]
		public int Source { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
