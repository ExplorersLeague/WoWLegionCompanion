using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONRealmCharacterCount", Version = 28333852u)]
	public class JamJSONRealmCharacterCount
	{
		[System.Runtime.Serialization.DataMember(Name = "wowRealmAddress")]
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		public uint WowRealmAddress { get; set; }

		[FlexJamMember(Name = "count", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "count")]
		public byte Count { get; set; }
	}
}
