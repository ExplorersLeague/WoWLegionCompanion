using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4879, Name = "MobileClientFollowerActivationDataResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerActivationDataResult
	{
		[FlexJamMember(Name = "goldCost", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "goldCost")]
		public int GoldCost { get; set; }

		[FlexJamMember(Name = "activationsRemaining", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "activationsRemaining")]
		public int ActivationsRemaining { get; set; }
	}
}
