using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonFollower", Version = 28333852u)]
	public class JamGarrisonFollower
	{
		public JamGarrisonFollower()
		{
			this.CustomName = string.Empty;
		}

		[System.Runtime.Serialization.DataMember(Name = "customName")]
		[FlexJamMember(Name = "customName", Type = FlexJamType.String)]
		public string CustomName { get; set; }

		[FlexJamMember(Name = "itemLevelWeapon", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemLevelWeapon")]
		public int ItemLevelWeapon { get; set; }

		[FlexJamMember(Name = "currentBuildingID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currentBuildingID")]
		public int CurrentBuildingID { get; set; }

		[FlexJamMember(Name = "zoneSupportSpellID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "zoneSupportSpellID")]
		public int ZoneSupportSpellID { get; set; }

		[FlexJamMember(Name = "currentMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currentMissionID")]
		public int CurrentMissionID { get; set; }

		[FlexJamMember(Name = "followerLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "followerLevel")]
		public int FollowerLevel { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public int Flags { get; set; }

		[FlexJamMember(Name = "itemLevelArmor", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemLevelArmor")]
		public int ItemLevelArmor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "xp")]
		[FlexJamMember(Name = "xp", Type = FlexJamType.Int32)]
		public int Xp { get; set; }

		[FlexJamMember(Name = "dbID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "dbID")]
		public ulong DbID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		public int GarrFollowerID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quality")]
		[FlexJamMember(Name = "quality", Type = FlexJamType.Int32)]
		public int Quality { get; set; }

		[FlexJamMember(Name = "durability", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "durability")]
		public int Durability { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "abilityID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "abilityID", Type = FlexJamType.Int32)]
		public int[] AbilityID { get; set; }
	}
}
