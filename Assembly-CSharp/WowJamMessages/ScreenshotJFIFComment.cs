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

		[System.Runtime.Serialization.DataMember(Name = "level")]
		[FlexJamMember(Name = "level", Type = FlexJamType.Int32)]
		public int Level { get; set; }

		[FlexJamMember(Name = "raceID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "raceID")]
		public uint RaceID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "worldport")]
		[FlexJamMember(Name = "worldport", Type = FlexJamType.String)]
		public string Worldport { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "isInGame")]
		[FlexJamMember(Name = "isInGame", Type = FlexJamType.Bool)]
		public bool IsInGame { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "realmName")]
		[FlexJamMember(Name = "realmName", Type = FlexJamType.String)]
		public string RealmName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "zoneName")]
		[FlexJamMember(Name = "zoneName", Type = FlexJamType.String)]
		public string ZoneName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "facing")]
		[FlexJamMember(Name = "facing", Type = FlexJamType.Float)]
		public float Facing { get; set; }

		[FlexJamMember(Name = "mapID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "mapID")]
		public uint MapID { get; set; }

		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "position")]
		public Vector3 Position { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "classID")]
		[FlexJamMember(Name = "classID", Type = FlexJamType.UInt32)]
		public uint ClassID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "sex")]
		[FlexJamMember(Name = "sex", Type = FlexJamType.UInt32)]
		public uint Sex { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mapName")]
		[FlexJamMember(Name = "mapName", Type = FlexJamType.String)]
		public string MapName { get; set; }
	}
}
