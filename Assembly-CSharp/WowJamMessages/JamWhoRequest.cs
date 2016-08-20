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

		[System.Runtime.Serialization.DataMember(Name = "serverInfo")]
		[FlexJamMember(Optional = true, Name = "serverInfo", Type = FlexJamType.Struct)]
		public JamWhoRequestServerInfo[] ServerInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minLevel")]
		[FlexJamMember(Name = "minLevel", Type = FlexJamType.Int32)]
		public int MinLevel { get; set; }

		[FlexJamMember(Name = "classFilter", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "classFilter")]
		public int ClassFilter { get; set; }

		[FlexJamMember(Name = "showEnemies", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "showEnemies")]
		public bool ShowEnemies { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guildVirtualRealmName")]
		[FlexJamMember(Name = "guildVirtualRealmName", Type = FlexJamType.String)]
		public string GuildVirtualRealmName { get; set; }

		[FlexJamMember(Name = "showArenaPlayers", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "showArenaPlayers")]
		public bool ShowArenaPlayers { get; set; }

		[FlexJamMember(Name = "maxLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "maxLevel")]
		public int MaxLevel { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guild")]
		[FlexJamMember(Name = "guild", Type = FlexJamType.String)]
		public string Guild { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "raceFilter")]
		[FlexJamMember(Name = "raceFilter", Type = FlexJamType.Int32)]
		public int RaceFilter { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "virtualRealmName")]
		[FlexJamMember(Name = "virtualRealmName", Type = FlexJamType.String)]
		public string VirtualRealmName { get; set; }

		[FlexJamMember(Name = "exactName", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "exactName")]
		public bool ExactName { get; set; }
	}
}
