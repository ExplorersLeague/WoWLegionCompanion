using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamWhoEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamWhoEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "guildGUID")]
		[FlexJamMember(Name = "guildGUID", Type = FlexJamType.WowGuid)]
		public string GuildGUID { get; set; }

		[FlexJamMember(Name = "areaID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "areaID")]
		public int AreaID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guildVirtualRealmAddress")]
		[FlexJamMember(Name = "guildVirtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint GuildVirtualRealmAddress { get; set; }

		[FlexJamMember(Name = "guildName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "guildName")]
		public string GuildName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerData")]
		[FlexJamMember(Name = "playerData", Type = FlexJamType.Struct)]
		public JamPlayerGuidLookupData PlayerData { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "isGM")]
		[FlexJamMember(Name = "isGM", Type = FlexJamType.Bool)]
		public bool IsGM { get; set; }
	}
}
