using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileFollowerArmamentExt", Version = 39869590u)]
	public class MobileFollowerArmamentExt
	{
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		public int SpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minItemLevel")]
		[FlexJamMember(Name = "minItemLevel", Type = FlexJamType.Int32)]
		public int MinItemLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxItemLevel")]
		[FlexJamMember(Name = "maxItemLevel", Type = FlexJamType.Int32)]
		public int MaxItemLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		public int Quantity { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "operation")]
		[FlexJamMember(Name = "operation", Type = FlexJamType.Int32)]
		public int Operation { get; set; }
	}
}
