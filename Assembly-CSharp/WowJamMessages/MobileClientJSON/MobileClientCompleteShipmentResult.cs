using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4863, Name = "MobileClientCompleteShipmentResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientCompleteShipmentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		public ulong ShipmentID { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }
	}
}
