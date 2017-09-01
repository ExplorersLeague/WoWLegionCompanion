﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamCharacterShipment", Version = 28333852u)]
	public class JamCharacterShipment
	{
		[System.Runtime.Serialization.DataMember(Name = "shipmentDuration")]
		[FlexJamMember(Name = "shipmentDuration", Type = FlexJamType.Int32)]
		public int ShipmentDuration { get; set; }

		[FlexJamMember(Name = "assignedFollowerDBID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "assignedFollowerDBID")]
		public ulong AssignedFollowerDBID { get; set; }

		[FlexJamMember(Name = "shipmentID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentID")]
		public ulong ShipmentID { get; set; }

		[FlexJamMember(Name = "shipmentRecID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "shipmentRecID")]
		public int ShipmentRecID { get; set; }

		[FlexJamMember(Name = "creationTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "creationTime")]
		public int CreationTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "buildingType")]
		[FlexJamMember(Name = "buildingType", Type = FlexJamType.Int32)]
		public int BuildingType { get; set; }
	}
}
