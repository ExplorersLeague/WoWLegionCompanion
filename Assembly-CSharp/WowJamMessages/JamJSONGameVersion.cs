using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONGameVersion", Version = 47212487u)]
	public class JamJSONGameVersion
	{
		[DataMember(Name = "versionMajor")]
		[FlexJamMember(Name = "versionMajor", Type = FlexJamType.UInt32)]
		public uint VersionMajor { get; set; }

		[DataMember(Name = "versionBuild")]
		[FlexJamMember(Name = "versionBuild", Type = FlexJamType.UInt32)]
		public uint VersionBuild { get; set; }

		[DataMember(Name = "versionMinor")]
		[FlexJamMember(Name = "versionMinor", Type = FlexJamType.UInt32)]
		public uint VersionMinor { get; set; }

		[DataMember(Name = "versionRevision")]
		[FlexJamMember(Name = "versionRevision", Type = FlexJamType.UInt32)]
		public uint VersionRevision { get; set; }
	}
}
