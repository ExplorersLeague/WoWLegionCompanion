using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmListLoginHistoryEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmListLoginHistoryEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "characterGUID")]
		[FlexJamMember(Name = "characterGUID", Type = FlexJamType.WowGuid)]
		public string CharacterGUID { get; set; }

		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		public uint VirtualRealmAddress { get; set; }

		[FlexJamMember(Name = "lastPlayedTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "lastPlayedTime")]
		public int LastPlayedTime { get; set; }
	}
}
