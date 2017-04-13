using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4861, Name = "MobileClientShipmentsUpdate", Version = 39869590u)]
	public class MobileClientShipmentsUpdate
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		public JamCharacterShipment[] Shipment { get; set; }
	}
}
