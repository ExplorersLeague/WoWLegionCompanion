using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4876, Name = "MobileClientUseFollowerArmamentResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientUseFollowerArmamentResult
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "follower")]
		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower Follower { get; set; }

		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
