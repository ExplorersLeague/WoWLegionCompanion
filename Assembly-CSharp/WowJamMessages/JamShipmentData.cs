using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamShipmentData", Version = 28333852u)]
	public class JamShipmentData
	{
		public JamShipmentData()
		{
			this.ResetPending = false;
		}

		[FlexJamMember(ArrayDimensions = 1, Name = "shipment", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "shipment")]
		public JamCharacterShipment[] Shipment { get; set; }

		[FlexJamMember(Name = "resetPending", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "resetPending")]
		public bool ResetPending { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "pendingShipment", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "pendingShipment")]
		public int[] PendingShipment { get; set; }
	}
}
