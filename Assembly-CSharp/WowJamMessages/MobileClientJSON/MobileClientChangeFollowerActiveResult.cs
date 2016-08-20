using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4866, Name = "MobileClientChangeFollowerActiveResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientChangeFollowerActiveResult
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[FlexJamMember(Name = "activationsRemaining", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "activationsRemaining")]
		public int ActivationsRemaining { get; set; }

		[FlexJamMember(Name = "follower", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "follower")]
		public JamGarrisonFollower Follower { get; set; }
	}
}
