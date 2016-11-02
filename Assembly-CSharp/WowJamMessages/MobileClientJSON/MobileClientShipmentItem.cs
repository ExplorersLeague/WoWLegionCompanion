using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileClientShipmentItem", Version = 33577221u)]
	public class MobileClientShipmentItem
	{
		[System.Runtime.Serialization.DataMember(Name = "context")]
		[FlexJamMember(Name = "context", Type = FlexJamType.Int32)]
		public int Context { get; set; }

		[FlexJamMember(Name = "iconFileDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "iconFileDataID")]
		public int IconFileDataID { get; set; }

		[FlexJamMember(Name = "mailed", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "mailed")]
		public bool Mailed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }

		[FlexJamMember(Name = "count", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "count")]
		public int Count { get; set; }
	}
}
