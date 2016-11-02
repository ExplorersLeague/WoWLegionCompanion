using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4841, Name = "MobileClientFollowerChangedXP", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
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

		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
