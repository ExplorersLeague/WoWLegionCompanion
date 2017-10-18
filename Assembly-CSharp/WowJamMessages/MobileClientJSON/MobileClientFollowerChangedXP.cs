using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4851, Name = "MobileClientFollowerChangedXP", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerChangedXP
	{
		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		public JamGarrisonFollower Follower { get; set; }

		[FlexJamMember(Name = "xpChange", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "xpChange")]
		public int XpChange { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "source")]
		[FlexJamMember(Name = "source", Type = FlexJamType.Int32)]
		public int Source { get; set; }

		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
