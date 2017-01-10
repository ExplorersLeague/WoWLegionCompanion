using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4868, Name = "MobileClientFollowerActivationDataResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
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
