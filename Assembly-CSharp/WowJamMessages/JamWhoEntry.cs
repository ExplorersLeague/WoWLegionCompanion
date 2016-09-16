using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamWhoEntry", Version = 28333852u)]
	public class JamWhoEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "guildGUID")]
		[FlexJamMember(Name = "guildGUID", Type = FlexJamType.WowGuid)]
		public string GuildGUID { get; set; }

		[FlexJamMember(Name = "areaID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "areaID")]
		public int AreaID { get; set; }

		[FlexJamMember(Name = "guildVirtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "guildVirtualRealmAddress")]
		public uint GuildVirtualRealmAddress { get; set; }

		[FlexJamMember(Name = "guildName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "guildName")]
		public string GuildName { get; set; }

		[FlexJamMember(Name = "playerData", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "playerData")]
		public JamPlayerGuidLookupData PlayerData { get; set; }

		[FlexJamMember(Name = "isGM", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isGM")]
		public bool IsGM { get; set; }
	}
}
