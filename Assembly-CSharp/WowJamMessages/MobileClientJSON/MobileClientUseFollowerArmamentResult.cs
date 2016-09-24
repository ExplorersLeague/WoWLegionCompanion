using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4863, Name = "MobileClientUseFollowerArmamentResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientUseFollowerArmamentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "follower")]
		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		public JamGarrisonFollower Follower { get; set; }

		[FlexJamMember(Name = "oldFollower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "oldFollower")]
		public JamGarrisonFollower OldFollower { get; set; }
	}
}
