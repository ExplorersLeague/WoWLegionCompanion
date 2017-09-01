﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileItemStats", Version = 39869590u)]
	public class MobileItemStats
	{
		[FlexJamMember(Name = "itemDelay", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemDelay")]
		public int ItemDelay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dpr")]
		[FlexJamMember(Name = "dpr", Type = FlexJamType.Float)]
		public float Dpr { get; set; }

		[FlexJamMember(Name = "effectiveArmor", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "effectiveArmor")]
		public int EffectiveArmor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "weaponSpeed")]
		[FlexJamMember(Name = "weaponSpeed", Type = FlexJamType.Float)]
		public float WeaponSpeed { get; set; }

		[FlexJamMember(Name = "itemLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemLevel")]
		public int ItemLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusStat")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bonusStat", Type = FlexJamType.Struct)]
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

		[FlexJamMember(Name = "quality", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "quality")]
		public int Quality { get; set; }
	}
}
