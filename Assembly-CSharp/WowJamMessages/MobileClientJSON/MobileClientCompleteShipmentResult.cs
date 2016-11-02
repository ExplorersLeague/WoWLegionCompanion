using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4853, Name = "MobileClientCompleteShipmentResult", Version = 33577221u)]
	public class MobileClientCompleteShipmentResult
	{
		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		public ulong ShipmentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }
	}
}
