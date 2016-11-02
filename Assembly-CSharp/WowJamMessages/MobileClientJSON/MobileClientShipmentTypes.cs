using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4852, Name = "MobileClientShipmentTypes", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentTypes
	{
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		public MobileClientShipmentType[] Shipment { get; set; }
	}
}
