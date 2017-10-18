using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4869, Name = "MobileClientSetShipmentDurationCheatResult", Version = 39869590u)]
	public class MobileClientSetShipmentDurationCheatResult
	{
		[System.Runtime.Serialization.DataMember(Name = "success")]
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		public bool Success { get; set; }
	}
}
