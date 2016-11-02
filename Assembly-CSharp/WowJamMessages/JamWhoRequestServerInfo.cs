using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamWhoRequestServerInfo", Version = 28333852u)]
	public class JamWhoRequestServerInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "factionGroup")]
		[FlexJamMember(Name = "factionGroup", Type = FlexJamType.Int32)]
		public int FactionGroup { get; set; }

		[FlexJamMember(Name = "locale", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		public int Locale { get; set; }

		[FlexJamMember(Name = "requesterVirtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "requesterVirtualRealmAddress")]
		public uint RequesterVirtualRealmAddress { get; set; }
	}
}
