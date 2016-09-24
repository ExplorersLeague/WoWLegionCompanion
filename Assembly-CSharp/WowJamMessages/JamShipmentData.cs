using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamShipmentData", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamShipmentData
	{
		public JamShipmentData()
		{
			this.ResetPending = false;
		}

		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		public JamCharacterShipment[] Shipment { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "resetPending")]
		[FlexJamMember(Name = "resetPending", Type = FlexJamType.Bool)]
		public bool ResetPending { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "pendingShipment")]
		[FlexJamMember(ArrayDimensions = 1, Name = "pendingShipment", Type = FlexJamType.Int32)]
		public int[] PendingShipment { get; set; }
	}
}
