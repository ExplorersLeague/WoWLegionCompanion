using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4852, Name = "MobileClientShipmentTypes", Version = 28333852u)]
	public class MobileClientShipmentTypes
	{
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		public MobileClientShipmentType[] Shipment { get; set; }
	}
}
