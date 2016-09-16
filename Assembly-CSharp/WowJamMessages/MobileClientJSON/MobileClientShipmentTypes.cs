using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4852, Name = "MobileClientShipmentTypes", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentTypes
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		public MobileClientShipmentType[] Shipment { get; set; }
	}
}
