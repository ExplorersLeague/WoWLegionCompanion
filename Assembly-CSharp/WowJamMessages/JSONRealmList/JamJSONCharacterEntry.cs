using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONCharacterEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONCharacterEntry
	{
		[FlexJamMember(Name = "raceID", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "raceID")]
		public byte RaceID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "hasMobileAccess")]
		[FlexJamMember(Name = "hasMobileAccess", Type = FlexJamType.Bool)]
		public bool HasMobileAccess { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "experienceLevel")]
		[FlexJamMember(Name = "experienceLevel", Type = FlexJamType.UInt8)]
		public byte ExperienceLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerGuid")]
		[FlexJamMember(Name = "playerGuid", Type = FlexJamType.WowGuid)]
		public string PlayerGuid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lastActiveTime")]
		[FlexJamMember(Name = "lastActiveTime", Type = FlexJamType.Int32)]
		public int LastActiveTime { get; set; }

		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
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
