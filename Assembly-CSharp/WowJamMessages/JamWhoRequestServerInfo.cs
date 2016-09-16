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

		[System.Runtime.Serialization.DataMember(Name = "locale")]
		[FlexJamMember(Name = "locale", Type = FlexJamType.Int32)]
		public int Locale { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "requesterVirtualRealmAddress")]
		[FlexJamMember(Name = "requesterVirtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint RequesterVirtualRealmAddress { get; set; }
	}
}
