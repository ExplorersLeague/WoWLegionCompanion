using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4794, Name = "MobilePlayerCompleteShipment", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCompleteShipment
	{
		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		public ulong ShipmentID { get; set; }
	}
}
