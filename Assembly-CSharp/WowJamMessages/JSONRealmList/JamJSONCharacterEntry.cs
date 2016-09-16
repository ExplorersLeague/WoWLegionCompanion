using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONCharacterEntry", Version = 28333852u)]
	public class JamJSONCharacterEntry
	{
		[FlexJamMember(Name = "raceID", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "raceID")]
		public byte RaceID { get; set; }

		[FlexJamMember(Name = "hasMobileAccess", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "hasMobileAccess")]
		public bool HasMobileAccess { get; set; }

		[FlexJamMember(Name = "experienceLevel", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "experienceLevel")]
		public byte ExperienceLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerGuid")]
		[FlexJamMember(Name = "playerGuid", Type = FlexJamType.WowGuid)]
		public string PlayerGuid { get; set; }

		[FlexJamMember(Name = "lastActiveTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "lastActiveTime")]
		public int LastActiveTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint VirtualRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[FlexJamMember(Name = "classID", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "classID")]
		public byte ClassID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "sexID")]
		[FlexJamMember(Name = "sexID", Type = FlexJamType.UInt8)]
		public byte SexID { get; set; }
	}
}
