using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONGameVersion", Version = 28333852u)]
	public class JamJSONGameVersion
	{
		[FlexJamMember(Name = "versionMajor", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "versionMajor")]
		public uint VersionMajor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionBuild")]
		[FlexJamMember(Name = "versionBuild", Type = FlexJamType.UInt32)]
		public uint VersionBuild { get; set; }

		[FlexJamMember(Name = "versionMinor", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "versionMinor")]
		public uint VersionMinor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionRevision")]
		[FlexJamMember(Name = "versionRevision", Type = FlexJamType.UInt32)]
		public uint VersionRevision { get; set; }
	}
}
