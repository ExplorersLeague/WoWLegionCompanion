using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerHello
	{
		[System.Runtime.Serialization.DataMember(Name = "token")]
		public ulong Token { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "reply")]
		public bool Reply { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionMajor")]
		public uint VersionMajor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionMinor")]
		public uint VersionMinor { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionRevision")]
		public uint VersionRevision { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "versionBuild")]
		public uint VersionBuild { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "protocolVersion")]
		public uint ProtocolVersion { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "appName")]
		public string AppName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "appPath")]
		public string AppPath { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "machineName")]
		public string MachineName { get; set; }
	}
}
