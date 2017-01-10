using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamCharacterShipment", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamCharacterShipment
	{
		[FlexJamMember(Name = "shipmentDuration", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentDuration")]
		public int ShipmentDuration { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "assignedFollowerDBID")]
		[FlexJamMember(Name = "assignedFollowerDBID", Type = FlexJamType.UInt64)]
		public ulong AssignedFollowerDBID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		public ulong ShipmentID { get; set; }

		[FlexJamMember(Name = "shipmentRecID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentRecID")]
		public int ShipmentRecID { get; set; }

		[FlexJamMember(Name = "creationTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "creationTime")]
		public int CreationTime { get; set; }

		[FlexJamMember(Name = "buildingType", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "buildingType")]
		public int BuildingType { get; set; }
	}
}
