using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4862, Name = "MobileClientShipmentTypes", Version = 39869590u)]
	public class MobileClientShipmentTypes
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		public MobileClientShipmentType[] Shipment { get; set; }
	}
}
