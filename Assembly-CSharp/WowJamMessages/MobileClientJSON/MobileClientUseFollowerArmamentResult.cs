using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4865, Name = "MobileClientUseFollowerArmamentResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientUseFollowerArmamentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		public JamGarrisonFollower Follower { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
