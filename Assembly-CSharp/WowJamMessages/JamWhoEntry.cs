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

		[System.Runtime.Serialization.DataMember(Name = "areaID")]
		[FlexJamMember(Name = "areaID", Type = FlexJamType.Int32)]
		public int AreaID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guildVirtualRealmAddress")]
		[FlexJamMember(Name = "guildVirtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint GuildVirtualRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guildName")]
		[FlexJamMember(Name = "guildName", Type = FlexJamType.String)]
		public string GuildName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "playerData")]
		[FlexJamMember(Name = "playerData", Type = FlexJamType.Struct)]
		public JamPlayerGuidLookupData PlayerData { get; set; }

		[FlexJamMember(Name = "isGM", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isGM")]
		public bool IsGM { get; set; }
	}
}
