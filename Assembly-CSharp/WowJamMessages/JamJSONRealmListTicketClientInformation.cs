using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONRealmListTicketClientInformation", Version = 47212487u)]
	public class JamJSONRealmListTicketClientInformation
	{
		public JamJSONRealmListTicketClientInformation()
		{
			this.Secret = new byte[32];
		}

		[DataMember(Name = "platform")]
		[FlexJamMember(Name = "platform", Type = FlexJamType.UInt32)]
		public uint Platform { get; set; }

		[DataMember(Name = "currentTime")]
		[FlexJamMember(Name = "currentTime", Type = FlexJamType.Int32)]
		public int CurrentTime { get; set; }

		[DataMember(Name = "clientArch")]
		[FlexJamMember(Name = "clientArch", Type = FlexJamType.UInt32)]
		public uint ClientArch { get; set; }

		[DataMember(Name = "systemVersion")]
		[FlexJamMember(Name = "systemVersion", Type = FlexJamType.String)]
		public string SystemVersion { get; set; }

		[DataMember(Name = "buildVariant")]
		[FlexJamMember(Name = "buildVariant", Type = FlexJamType.String)]
		public string BuildVariant { get; set; }

		[DataMember(Name = "timeZone")]
		[FlexJamMember(Name = "timeZone", Type = FlexJamType.String)]
		public string TimeZone { get; set; }

		[DataMember(Name = "versionDataBuild")]
		[FlexJamMember(Name = "versionDataBuild", Type = FlexJamType.UInt32)]
		public uint VersionDataBuild { get; set; }

		[DataMember(Name = "audioLocale")]
		[FlexJamMember(Name = "audioLocale", Type = FlexJamType.UInt32)]
		public uint AudioLocale { get; set; }

		[DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Struct)]
		public JamJSONGameVersion Version { get; set; }

		[DataMember(Name = "secret")]
		[FlexJamMember(ArrayDimensions = 1, Name = "secret", Type = FlexJamType.UInt8)]
		public byte[] Secret { get; set; }

		[DataMember(Name = "type")]
		[FlexJamMember(Name = "type", Type = FlexJamType.UInt32)]
		public uint Type { get; set; }

		[DataMember(Name = "textLocale")]
		[FlexJamMember(Name = "textLocale", Type = FlexJamType.UInt32)]
		public uint TextLocale { get; set; }

		[DataMember(Name = "platformType")]
		[FlexJamMember(Name = "platformType", Type = FlexJamType.UInt32)]
		public uint PlatformType { get; set; }

		[DataMember(Name = "systemArch")]
		[FlexJamMember(Name = "systemArch", Type = FlexJamType.UInt32)]
		public uint SystemArch { get; set; }
	}
}
