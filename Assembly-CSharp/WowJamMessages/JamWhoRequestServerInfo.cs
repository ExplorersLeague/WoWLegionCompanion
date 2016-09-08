using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamWhoRequestServerInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamWhoRequestServerInfo
	{
		[FlexJamMember(Name = "factionGroup", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "factionGroup")]
		public int FactionGroup { get; set; }

		[FlexJamMember(Name = "locale", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		public int Locale { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "requesterVirtualRealmAddress")]
		[FlexJamMember(Name = "requesterVirtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint RequesterVirtualRealmAddress { get; set; }
	}
}
