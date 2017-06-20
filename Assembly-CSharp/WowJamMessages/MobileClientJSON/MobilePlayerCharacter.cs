using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobilePlayerCharacter", Version = 39869590u)]
	public class MobilePlayerCharacter
	{
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		public string Guid { get; set; }

		[FlexJamMember(Name = "charLevel", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "charLevel")]
		public byte CharLevel { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "status")]
		[FlexJamMember(Name = "status", Type = FlexJamType.Int32)]
		public int Status { get; set; }

		[FlexJamMember(Name = "charClass", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "charClass")]
		public byte CharClass { get; set; }

		[FlexJamMember(Name = "charRace", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "charRace")]
		public byte CharRace { get; set; }
	}
}
