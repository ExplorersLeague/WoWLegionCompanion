using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamWhoRequest", Version = 28333852u)]
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

		[System.Runtime.Serialization.DataMember(Name = "classFilter")]
		[FlexJamMember(Name = "classFilter", Type = FlexJamType.Int32)]
		public int ClassFilter { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "showEnemies")]
		[FlexJamMember(Name = "showEnemies", Type = FlexJamType.Bool)]
		public bool ShowEnemies { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guildVirtualRealmName")]
		[FlexJamMember(Name = "guildVirtualRealmName", Type = FlexJamType.String)]
		public string GuildVirtualRealmName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "showArenaPlayers")]
		[FlexJamMember(Name = "showArenaPlayers", Type = FlexJamType.Bool)]
		public bool ShowArenaPlayers { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxLevel")]
		[FlexJamMember(Name = "maxLevel", Type = FlexJamType.Int32)]
		public int MaxLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
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

		[System.Runtime.Serialization.DataMember(Name = "exactName")]
		[FlexJamMember(Name = "exactName", Type = FlexJamType.Bool)]
		public bool ExactName { get; set; }
	}
}
