using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONGameVersion", Version = 28333852u)]
	public class JamJSONGameVersion
	{
		[System.Runtime.Serialization.DataMember(Name = "versionMajor")]
		[FlexJamMember(Name = "versionMajor", Type = FlexJamType.UInt32)]
		public uint VersionMajor { get; set; }

		[FlexJamMember(Name = "versionBuild", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "versionBuild")]
		public uint VersionBuild { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionMinor")]
		[FlexJamMember(Name = "versionMinor", Type = FlexJamType.UInt32)]
		public uint VersionMinor { get; set; }

		[FlexJamMember(Name = "versionRevision", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "versionRevision")]
		public uint VersionRevision { get; set; }
	}
}
