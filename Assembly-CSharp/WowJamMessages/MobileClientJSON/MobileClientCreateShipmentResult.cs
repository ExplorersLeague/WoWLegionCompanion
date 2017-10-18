using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4860, Name = "MobileClientCreateShipmentResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientCreateShipmentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "charShipmentID")]
		[FlexJamMember(Name = "charShipmentID", Type = FlexJamType.Int32)]
		public int CharShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }
	}
}
