using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4841, Name = "MobileClientFollowerChangedXP", Version = 28333852u)]
	public class MobileClientFollowerChangedXP
	{
		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		public JamGarrisonFollower Follower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "xpChange")]
		[FlexJamMember(Name = "xpChange", Type = FlexJamType.Int32)]
		public int XpChange { get; set; }

		[FlexJamMember(Name = "source", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "source")]
		public int Source { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
