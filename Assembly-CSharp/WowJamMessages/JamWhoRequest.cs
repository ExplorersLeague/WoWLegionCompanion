using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamWhoRequest", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamWhoRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "words")]
		[FlexJamMember(ArrayDimensions = 1, Name = "words", Type = FlexJamType.Struct)]
		public JamWhoWord[] Words { get; set; }

		[FlexJamMember(Optional = true, Name = "serverInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "serverInfo")]
		public JamWhoRequestServerInfo[] ServerInfo { get; set; }

		[FlexJamMember(Name = "minLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "minLevel")]
		public int MinLevel { get; set; }

		[FlexJamMember(Name = "classFilter", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "classFilter")]
		public int ClassFilter { get; set; }

		[FlexJamMember(Name = "showEnemies", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "showEnemies")]
		public bool ShowEnemies { get; set; }

		[FlexJamMember(Name = "guildVirtualRealmName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "guildVirtualRealmName")]
		public string GuildVirtualRealmName { get; set; }

		[FlexJamMember(Name = "showArenaPlayers", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "showArenaPlayers")]
		public bool ShowArenaPlayers { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxLevel")]
		[FlexJamMember(Name = "maxLevel", Type = FlexJamType.Int32)]
		public int MaxLevel { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(Name = "guild", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "guild")]
		public string Guild { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "raceFilter")]
		[FlexJamMember(Name = "raceFilter", Type = FlexJamType.Int32)]
		public int RaceFilter { get; set; }

		[FlexJamMember(Name = "virtualRealmName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "virtualRealmName")]
		public string VirtualRealmName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "exactName")]
		[FlexJamMember(Name = "exactName", Type = FlexJamType.Bool)]
		public bool ExactName { get; set; }
	}
}
