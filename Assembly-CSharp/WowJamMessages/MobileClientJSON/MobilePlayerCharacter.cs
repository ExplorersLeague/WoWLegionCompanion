using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobilePlayerCharacter", Version = 39869590u)]
	public class MobilePlayerCharacter
	{
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charLevel")]
		[FlexJamMember(Name = "charLevel", Type = FlexJamType.UInt8)]
		public byte CharLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "status")]
		[FlexJamMember(Name = "status", Type = FlexJamType.Int32)]
		public int Status { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charClass")]
		[FlexJamMember(Name = "charClass", Type = FlexJamType.UInt8)]
		public byte CharClass { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "charRace")]
		[FlexJamMember(Name = "charRace", Type = FlexJamType.UInt8)]
		public byte CharRace { get; set; }
	}
}
