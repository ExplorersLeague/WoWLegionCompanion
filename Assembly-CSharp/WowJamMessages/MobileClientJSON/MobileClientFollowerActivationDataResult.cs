using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4879, Name = "MobileClientFollowerActivationDataResult", Version = 39869590u)]
	public class MobileClientFollowerActivationDataResult
	{
		[FlexJamMember(Name = "goldCost", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "goldCost")]
		public int GoldCost { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "activationsRemaining")]
		[FlexJamMember(Name = "activationsRemaining", Type = FlexJamType.Int32)]
		public int ActivationsRemaining { get; set; }
	}
}
