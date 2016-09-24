using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4853, Name = "MobileClientCompleteShipmentResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientCompleteShipmentResult
	{
		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		public ulong ShipmentID { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }
	}
}
