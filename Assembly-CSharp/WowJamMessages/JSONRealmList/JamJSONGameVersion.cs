using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONGameVersion", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONGameVersion
	{
		[System.Runtime.Serialization.DataMember(Name = "versionMajor")]
		[FlexJamMember(Name = "versionMajor", Type = FlexJamType.UInt32)]
		public uint VersionMajor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionBuild")]
		[FlexJamMember(Name = "versionBuild", Type = FlexJamType.UInt32)]
		public uint VersionBuild { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionMinor")]
		[FlexJamMember(Name = "versionMinor", Type = FlexJamType.UInt32)]
		public uint VersionMinor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionRevision")]
		[FlexJamMember(Name = "versionRevision", Type = FlexJamType.UInt32)]
		public uint VersionRevision { get; set; }
	}
}
