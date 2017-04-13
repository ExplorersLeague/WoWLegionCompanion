using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "UnitDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
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

		[FlexJamMember(Name = "level", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "level")]
		public int Level { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "aiTriggerActionDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "aiTriggerActionDebugInfo", Type = FlexJamType.Struct)]
		public AITriggerActionDebugInfo[] AiTriggerActionDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spellDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "spellDebugInfo", Type = FlexJamType.Struct)]
		public CreatureSpellDebugInfo[] SpellDebugInfo { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "effectiveStatValues", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "effectiveStatValues")]
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

		[FlexJamMember(Name = "classID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "classID")]
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

		[FlexJamMember(Name = "spawnGroupID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "spawnGroupID")]
		public int SpawnGroupID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "spawnEventDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "spawnEventDebugInfo", Type = FlexJamType.Struct)]
		public SpawnEventDebugInfo[] SpawnEventDebugInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "auraDebugInfo")]
		[FlexJamMember(ArrayDimensions = 1, Name = "auraDebugInfo", Type = FlexJamType.Struct)]
		public UnitAuraDebugInfo[] AuraDebugInfo { get; set; }

		[FlexJamMember(Name = "creatureSpellDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "creatureSpellDataID")]
		public int CreatureSpellDataID { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "zoneFlags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "zoneFlags")]
		public uint[] ZoneFlags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "percentRangedAttack")]
		[FlexJamMember(Name = "percentRangedAttack", Type = FlexJamType.Int32)]
		public int PercentRangedAttack { get; set; }
	}
}
