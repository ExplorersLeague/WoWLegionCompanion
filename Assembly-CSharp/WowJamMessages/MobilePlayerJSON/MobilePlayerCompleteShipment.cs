using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4793, Name = "MobilePlayerCompleteShipment", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCompleteShipment
	{
		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		public ulong ShipmentID { get; set; }
	}
}
