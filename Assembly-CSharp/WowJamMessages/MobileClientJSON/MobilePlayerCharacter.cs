using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobilePlayerCharacter", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCharacter
	{
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charLevel")]
		[FlexJamMember(Name = "charLevel", Type = FlexJamType.UInt8)]
		public byte CharLevel { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(Name = "status", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "status")]
		public int Status { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charClass")]
		[FlexJamMember(Name = "charClass", Type = FlexJamType.UInt8)]
		public byte CharClass { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charRace")]
		[FlexJamMember(Name = "charRace", Type = FlexJamType.UInt8)]
		public byte CharRace { get; set; }
	}
}
