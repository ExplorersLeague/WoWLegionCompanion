using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlepayDisplayInfo", Version = 28333852u)]
	public class JamBattlepayDisplayInfo
	{
		[FlexJamMember(Name = "name1", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name1")]
		public string Name1 { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "overrideTextColor")]
		[FlexJamMember(Optional = true, Name = "overrideTextColor", Type = FlexJamType.UInt32)]
		public uint[] OverrideTextColor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name2")]
		[FlexJamMember(Name = "name2", Type = FlexJamType.String)]
		public string Name2 { get; set; }

		[FlexJamMember(Name = "name3", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name3")]
		public string Name3 { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "overrideBackground")]
		[FlexJamMember(Optional = true, Name = "overrideBackground", Type = FlexJamType.UInt32)]
		public uint[] OverrideBackground { get; set; }

		[FlexJamMember(Optional = true, Name = "overrideTexture", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "overrideTexture")]
		public uint[] OverrideTexture { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Optional = true, Name = "flags", Type = FlexJamType.UInt32)]
		public uint[] Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "creatureDisplayInfoID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "creatureDisplayInfoID")]
		public uint[] CreatureDisplayInfoID { get; set; }

		[FlexJamMember(Optional = true, Name = "fileDataID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "fileDataID")]
		public uint[] FileDataID { get; set; }
	}
}
