using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONCharacterEntry", Version = 28333852u)]
	public class JamJSONCharacterEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "raceID")]
		[FlexJamMember(Name = "raceID", Type = FlexJamType.UInt8)]
		public byte RaceID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "hasMobileAccess")]
		[FlexJamMember(Name = "hasMobileAccess", Type = FlexJamType.Bool)]
		public bool HasMobileAccess { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "experienceLevel")]
		[FlexJamMember(Name = "experienceLevel", Type = FlexJamType.UInt8)]
		public byte ExperienceLevel { get; set; }

		[FlexJamMember(Name = "playerGuid", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "playerGuid")]
		public string PlayerGuid { get; set; }

		[FlexJamMember(Name = "lastActiveTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "lastActiveTime")]
		public int LastActiveTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint VirtualRealmAddress { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(Name = "classID", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "classID")]
		public byte ClassID { get; set; }

		[FlexJamMember(Name = "sexID", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "sexID")]
		public byte SexID { get; set; }
	}
}
