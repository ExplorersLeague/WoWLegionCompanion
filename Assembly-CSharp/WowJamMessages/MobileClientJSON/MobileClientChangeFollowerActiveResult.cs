using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4880, Name = "MobileClientChangeFollowerActiveResult", Version = 39869590u)]
	public class MobileClientChangeFollowerActiveResult
	{
		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "activationsRemaining")]
		[FlexJamMember(Name = "activationsRemaining", Type = FlexJamType.Int32)]
		public int ActivationsRemaining { get; set; }

		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		public JamGarrisonFollower Follower { get; set; }
	}
}
