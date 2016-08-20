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

		[FlexJamMember(Name = "serverOnly", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "serverOnly")]
		public bool ServerOnly { get; set; }

		[FlexJamMember(Name = "fromEnchantment", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "fromEnchantment")]
		public bool FromEnchantment { get; set; }

		[FlexJamMember(Name = "enchantmentSlot", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "enchantmentSlot")]
		public int EnchantmentSlot { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "effectDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "effectDebugInfo")]
		public UnitAuraEffectDebugInfo[] EffectDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "paused")]
		[FlexJamMember(Name = "paused", Type = FlexJamType.Bool)]
		public bool Paused { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemName")]
		[FlexJamMember(Name = "itemName", Type = FlexJamType.String)]
		public string ItemName { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "fromItem")]
		[FlexJamMember(Name = "fromItem", Type = FlexJamType.Bool)]
		public bool FromItem { get; set; }
	}
}
