using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileItemStats", Version = 39869590u)]
	public class MobileItemStats
	{
		[System.Runtime.Serialization.DataMember(Name = "itemDelay")]
		[FlexJamMember(Name = "itemDelay", Type = FlexJamType.Int32)]
		public int ItemDelay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dpr")]
		[FlexJamMember(Name = "dpr", Type = FlexJamType.Float)]
		public float Dpr { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "effectiveArmor")]
		[FlexJamMember(Name = "effectiveArmor", Type = FlexJamType.Int32)]
		public int EffectiveArmor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "weaponSpeed")]
		[FlexJamMember(Name = "weaponSpeed", Type = FlexJamType.Float)]
		public float WeaponSpeed { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemLevel")]
		[FlexJamMember(Name = "itemLevel", Type = FlexJamType.Int32)]
		public int ItemLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusStat")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bonusStat", Type = FlexJamType.Struct)]
		public MobileItemBonusStat[] BonusStat { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "requiredLevel")]
		[FlexJamMember(Name = "requiredLevel", Type = FlexJamType.Int32)]
		public int RequiredLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxDamage")]
		[FlexJamMember(Name = "maxDamage", Type = FlexJamType.Int32)]
		public int MaxDamage { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minDamage")]
		[FlexJamMember(Name = "minDamage", Type = FlexJamType.Int32)]
		public int MinDamage { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quality")]
		[FlexJamMember(Name = "quality", Type = FlexJamType.Int32)]
		public int Quality { get; set; }
	}
}
