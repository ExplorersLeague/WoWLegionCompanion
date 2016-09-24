using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmCharacterCount", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmCharacterCount
	{
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "wowRealmAddress")]
		public uint WowRealmAddress { get; set; }

		[FlexJamMember(Name = "count", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "count")]
		public byte Count { get; set; }
	}
}
