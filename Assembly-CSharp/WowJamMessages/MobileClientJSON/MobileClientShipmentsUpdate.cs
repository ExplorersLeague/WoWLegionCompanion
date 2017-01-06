using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4851, Name = "MobileClientShipmentsUpdate", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentsUpdate
	{
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		public JamCharacterShipment[] Shipment { get; set; }
	}
}
