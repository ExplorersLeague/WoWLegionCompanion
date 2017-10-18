using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobilePlayerCharacter", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCharacter
	{
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[FlexJamMember(Name = "charLevel", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "charLevel")]
		public byte CharLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[FlexJamMember(Name = "status", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "status")]
		public int Status { get; set; }

		[FlexJamMember(Name = "charClass", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "charClass")]
		public byte CharClass { get; set; }

		[FlexJamMember(Name = "charRace", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "charRace")]
		public byte CharRace { get; set; }
	}
}
