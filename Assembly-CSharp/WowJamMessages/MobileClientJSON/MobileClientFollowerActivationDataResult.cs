using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4868, Name = "MobileClientFollowerActivationDataResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerActivationDataResult
	{
		[System.Runtime.Serialization.DataMember(Name = "goldCost")]
		[FlexJamMember(Name = "goldCost", Type = FlexJamType.Int32)]
		public int GoldCost { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "activationsRemaining")]
		[FlexJamMember(Name = "activationsRemaining", Type = FlexJamType.Int32)]
		public int ActivationsRemaining { get; set; }
	}
}
