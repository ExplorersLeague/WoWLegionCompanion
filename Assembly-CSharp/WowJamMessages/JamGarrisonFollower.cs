using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonFollower", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonFollower
	{
		public JamGarrisonFollower()
		{
			this.CustomName = string.Empty;
		}

		[FlexJamMember(Name = "customName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "customName")]
		public string CustomName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemLevelWeapon")]
		[FlexJamMember(Name = "itemLevelWeapon", Type = FlexJamType.Int32)]
		public int ItemLevelWeapon { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currentBuildingID")]
		[FlexJamMember(Name = "currentBuildingID", Type = FlexJamType.Int32)]
		public int CurrentBuildingID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "zoneSupportSpellID")]
		[FlexJamMember(Name = "zoneSupportSpellID", Type = FlexJamType.Int32)]
		public int ZoneSupportSpellID { get; set; }

		[FlexJamMember(Name = "currentMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currentMissionID")]
		public int CurrentMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "followerLevel")]
		[FlexJamMember(Name = "followerLevel", Type = FlexJamType.Int32)]
		public int FollowerLevel { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public int Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemLevelArmor")]
		[FlexJamMember(Name = "itemLevelArmor", Type = FlexJamType.Int32)]
		public int ItemLevelArmor { get; set; }

		[FlexJamMember(Name = "xp", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "xp")]
		public int Xp { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "dbID")]
		[FlexJamMember(Name = "dbID", Type = FlexJamType.UInt64)]
		public ulong DbID { get; set; }

		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		public int GarrFollowerID { get; set; }

		[FlexJamMember(Name = "quality", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "quality")]
		public int Quality { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "durability")]
		[FlexJamMember(Name = "durability", Type = FlexJamType.Int32)]
		public int Durability { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "abilityID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "abilityID")]
		public int[] AbilityID { get; set; }
	}
}
