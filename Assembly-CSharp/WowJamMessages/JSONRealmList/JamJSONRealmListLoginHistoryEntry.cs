using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONRealmListLoginHistoryEntry", Version = 28333852u)]
	public class JamJSONRealmListLoginHistoryEntry
	{
		[FlexJamMember(Name = "characterGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "characterGUID")]
		public string CharacterGUID { get; set; }

		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		public uint VirtualRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lastPlayedTime")]
		[FlexJamMember(Name = "lastPlayedTime", Type = FlexJamType.Int32)]
		public int LastPlayedTime { get; set; }
	}
}
