using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "UnitAuraDebugInfo", Version = 28333852u)]
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

		[System.Runtime.Serialization.DataMember(Name = "fromEnchantment")]
		[FlexJamMember(Name = "fromEnchantment", Type = FlexJamType.Bool)]
		public bool FromEnchantment { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "enchantmentSlot")]
		[FlexJamMember(Name = "enchantmentSlot", Type = FlexJamType.Int32)]
		public int EnchantmentSlot { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "effectDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "effectDebugInfo", Type = FlexJamType.Struct)]
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

		[FlexJamMember(Name = "fromItem", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "fromItem")]
		public bool FromItem { get; set; }
	}
}
