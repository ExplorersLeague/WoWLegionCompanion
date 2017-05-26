using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileClientShipmentItem", Version = 39869590u)]
	public class MobileClientShipmentItem
	{
		[FlexJamMember(Name = "context", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "context")]
		public int Context { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "iconFileDataID")]
		[FlexJamMember(Name = "iconFileDataID", Type = FlexJamType.Int32)]
		public int IconFileDataID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mailed")]
		[FlexJamMember(Name = "mailed", Type = FlexJamType.Bool)]
		public bool Mailed { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "count")]
		[FlexJamMember(Name = "count", Type = FlexJamType.Int32)]
		public int Count { get; set; }
	}
}
