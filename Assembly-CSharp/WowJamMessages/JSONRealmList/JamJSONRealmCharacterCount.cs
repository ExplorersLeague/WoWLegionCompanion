using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONRealmCharacterCount", Version = 47212487u)]
	public class JamJSONRealmCharacterCount
	{
		[DataMember(Name = "wowRealmAddress")]
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		public uint WowRealmAddress { get; set; }

		[DataMember(Name = "count")]
		[FlexJamMember(Name = "count", Type = FlexJamType.UInt8)]
		public byte Count { get; set; }
	}
}
