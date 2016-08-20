using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4857, Name = "MobileClientSetShipmentDurationCheatResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientSetShipmentDurationCheatResult
	{
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "success")]
		public bool Success { get; set; }
	}
}
