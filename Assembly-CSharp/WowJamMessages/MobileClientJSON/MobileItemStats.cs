using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileItemStats", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileItemStats
	{
		[FlexJamMember(Name = "itemDelay", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemDelay")]
		public int ItemDelay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dpr")]
		[FlexJamMember(Name = "dpr", Type = FlexJamType.Float)]
		public float Dpr { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "effectiveArmor")]
		[FlexJamMember(Name = "effectiveArmor", Type = FlexJamType.Int32)]
		public int EffectiveArmor { get; set; }

		[FlexJamMember(Name = "weaponSpeed", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "weaponSpeed")]
		public float WeaponSpeed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemLevel")]
		[FlexJamMember(Name = "itemLevel", Type = FlexJamType.Int32)]
		public int ItemLevel { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "bonusStat", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "bonusStat")]
		public MobileItemBonusStat[] BonusStat { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "requiredLevel")]
		[FlexJamMember(Name = "requiredLevel", Type = FlexJamType.Int32)]
		public int RequiredLevel { get; set; }

		[FlexJamMember(Name = "maxDamage", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "maxDamage")]
		public int MaxDamage { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minDamage")]
		[FlexJamMember(Name = "minDamage", Type = FlexJamType.Int32)]
		public int MinDamage { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quality")]
		[FlexJamMember(Name = "quality", Type = FlexJamType.Int32)]
		public int Quality { get; set; }
	}
}
