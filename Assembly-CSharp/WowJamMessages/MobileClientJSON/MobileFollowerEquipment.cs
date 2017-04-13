using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileFollowerEquipment", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileFollowerEquipment
	{
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		public int SpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrAbilityID")]
		[FlexJamMember(Name = "garrAbilityID", Type = FlexJamType.Int32)]
		public int GarrAbilityID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }

		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		public int Quantity { get; set; }
	}
}
