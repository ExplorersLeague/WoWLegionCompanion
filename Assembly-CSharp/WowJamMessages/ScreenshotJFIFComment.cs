using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "ScreenshotJFIFComment", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class ScreenshotJFIFComment
	{
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "level")]
		[FlexJamMember(Name = "level", Type = FlexJamType.Int32)]
		public int Level { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "raceID")]
		[FlexJamMember(Name = "raceID", Type = FlexJamType.UInt32)]
		public uint RaceID { get; set; }

		[FlexJamMember(Name = "worldport", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "worldport")]
		public string Worldport { get; set; }

		[FlexJamMember(Name = "isInGame", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "isInGame")]
		public bool IsInGame { get; set; }

		[FlexJamMember(Name = "realmName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "realmName")]
		public string RealmName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "zoneName")]
		[FlexJamMember(Name = "zoneName", Type = FlexJamType.String)]
		public string ZoneName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "facing")]
		[FlexJamMember(Name = "facing", Type = FlexJamType.Float)]
		public float Facing { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mapID")]
		[FlexJamMember(Name = "mapID", Type = FlexJamType.UInt32)]
		public uint MapID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "position")]
		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		public Vector3 Position { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(Name = "classID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "classID")]
		public uint ClassID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "sex")]
		[FlexJamMember(Name = "sex", Type = FlexJamType.UInt32)]
		public uint Sex { get; set; }

		[FlexJamMember(Name = "mapName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "mapName")]
		public string MapName { get; set; }
	}
}
