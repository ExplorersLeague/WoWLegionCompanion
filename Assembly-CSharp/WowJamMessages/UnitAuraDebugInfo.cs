using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "UnitAuraDebugInfo", Version = 28333852u)]
	public class UnitAuraDebugInfo
	{
		[FlexJamMember(Name = "spellID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spellID")]
		public int SpellID { get; set; }

		[FlexJamMember(Name = "fromItemSet", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "fromItemSet")]
		public bool FromItemSet { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "serverOnly")]
		[FlexJamMember(Name = "serverOnly", Type = FlexJamType.Bool)]
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

		[FlexJamMember(Name = "paused", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "paused")]
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
