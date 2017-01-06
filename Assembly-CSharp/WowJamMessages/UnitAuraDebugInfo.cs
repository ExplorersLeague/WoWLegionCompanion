using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "UnitAuraDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class UnitAuraDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		public int SpellID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "fromItemSet")]
		[FlexJamMember(Name = "fromItemSet", Type = FlexJamType.Bool)]
		public bool FromItemSet { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "serverOnly")]
		[FlexJamMember(Name = "serverOnly", Type = FlexJamType.Bool)]
		public bool ServerOnly { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "fromEnchantment")]
		[FlexJamMember(Name = "fromEnchantment", Type = FlexJamType.Bool)]
		public bool FromEnchantment { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "enchantmentSlot")]
		[FlexJamMember(Name = "enchantmentSlot", Type = FlexJamType.Int32)]
		public int EnchantmentSlot { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "effectDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "effectDebugInfo")]
		public UnitAuraEffectDebugInfo[] EffectDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "paused")]
		[FlexJamMember(Name = "paused", Type = FlexJamType.Bool)]
		public bool Paused { get; set; }

		[FlexJamMember(Name = "itemName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "itemName")]
		public string ItemName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }

		[FlexJamMember(Name = "fromItem", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "fromItem")]
		public bool FromItem { get; set; }
	}
}
