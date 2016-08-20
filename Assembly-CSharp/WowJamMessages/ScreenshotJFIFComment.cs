using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "ScreenshotJFIFComment", Version = 28333852u)]
	public class ScreenshotJFIFComment
	{
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		public string Guid { get; set; }

		[FlexJamMember(Name = "level", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "level")]
		public int Level { get; set; }

		[FlexJamMember(Name = "raceID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "raceID")]
		public uint RaceID { get; set; }

		[FlexJamMember(Name = "worldport", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "worldport")]
		public string Worldport { get; set; }

		[FlexJamMember(Name = "isInGame", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isInGame")]
		public bool IsInGame { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "realmName")]
		[FlexJamMember(Name = "realmName", Type = FlexJamType.String)]
		public string RealmName { get; set; }

		[FlexJamMember(Name = "zoneName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "zoneName")]
		public string ZoneName { get; set; }

		[FlexJamMember(Name = "facing", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "facing")]
		public float Facing { get; set; }

		[FlexJamMember(Name = "mapID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "mapID")]
		public uint MapID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "position")]
		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		public Vector3 Position { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "classID")]
		[FlexJamMember(Name = "classID", Type = FlexJamType.UInt32)]
		public uint ClassID { get; set; }

		[FlexJamMember(Name = "sex", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "sex")]
		public uint Sex { get; set; }

		[FlexJamMember(Name = "mapName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "mapName")]
		public string MapName { get; set; }
	}
}
