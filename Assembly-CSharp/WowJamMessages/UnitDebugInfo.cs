﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "UnitDebugInfo", Version = 28333852u)]
	public class UnitDebugInfo
	{
		public UnitDebugInfo()
		{
			this.ZoneFlags = new uint[3];
			this.EffectiveStatValues = new int[5];
			this.CreatureSpellDataID = 0;
			this.PercentSupportAction = 0;
			this.PercentRangedAttack = 0;
			this.SpawnGroupID = 0;
			this.SpawnGroupName = string.Empty;
			this.SpawnRegionID = 0;
			this.SpawnRegionName = string.Empty;
		}

		[System.Runtime.Serialization.DataMember(Name = "level")]
		[FlexJamMember(Name = "level", Type = FlexJamType.Int32)]
		public int Level { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "aiTriggerActionDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "aiTriggerActionDebugInfo")]
		public AITriggerActionDebugInfo[] AiTriggerActionDebugInfo { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "spellDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "spellDebugInfo")]
		public CreatureSpellDebugInfo[] SpellDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "effectiveStatValues")]
		[FlexJamMember(ArrayDimensions = 1, Name = "effectiveStatValues", Type = FlexJamType.Int32)]
		public int[] EffectiveStatValues { get; set; }

		[FlexJamMember(Name = "spawnRegionName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "spawnRegionName")]
		public string SpawnRegionName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spawnGroupName")]
		[FlexJamMember(Name = "spawnGroupName", Type = FlexJamType.String)]
		public string SpawnGroupName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spawnRegionID")]
		[FlexJamMember(Name = "spawnRegionID", Type = FlexJamType.Int32)]
		public int SpawnRegionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "classID")]
		[FlexJamMember(Name = "classID", Type = FlexJamType.Int32)]
		public int ClassID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "aiTriggerActionSetDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "aiTriggerActionSetDebugInfo", Type = FlexJamType.Struct)]
		public AITriggerActionSetDebugInfo[] AiTriggerActionSetDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerClassID")]
		[FlexJamMember(Name = "playerClassID", Type = FlexJamType.Int32)]
		public int PlayerClassID { get; set; }

		[FlexJamMember(Name = "percentSupportAction", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "percentSupportAction")]
		public int PercentSupportAction { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spawnGroupID")]
		[FlexJamMember(Name = "spawnGroupID", Type = FlexJamType.Int32)]
		public int SpawnGroupID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "spawnEventDebugInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "spawnEventDebugInfo")]
		public SpawnEventDebugInfo[] SpawnEventDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "auraDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "auraDebugInfo", Type = FlexJamType.Struct)]
		public UnitAuraDebugInfo[] AuraDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "creatureSpellDataID")]
		[FlexJamMember(Name = "creatureSpellDataID", Type = FlexJamType.Int32)]
		public int CreatureSpellDataID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "zoneFlags")]
		[FlexJamMember(ArrayDimensions = 1, Name = "zoneFlags", Type = FlexJamType.UInt32)]
		public uint[] ZoneFlags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "percentRangedAttack")]
		[FlexJamMember(Name = "percentRangedAttack", Type = FlexJamType.Int32)]
		public int PercentRangedAttack { get; set; }
	}
}
