using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileClientShipmentItem", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientShipmentItem
	{
		[System.Runtime.Serialization.DataMember(Name = "context")]
		[FlexJamMember(Name = "context", Type = FlexJamType.Int32)]
		public int Context { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "iconFileDataID")]
		[FlexJamMember(Name = "iconFileDataID", Type = FlexJamType.Int32)]
		public int IconFileDataID { get; set; }

		[FlexJamMember(Name = "mailed", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "mailed")]
		public bool Mailed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "count")]
		[FlexJamMember(Name = "count", Type = FlexJamType.Int32)]
		public int Count { get; set; }
	}
}
