using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmListUpdatePart", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmListUpdatePart
	{
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "wowRealmAddress")]
		public uint WowRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "update")]
		[FlexJamMember(Name = "update", Type = FlexJamType.Struct)]
		public JamJSONRealmEntry Update { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "deleting")]
		[FlexJamMember(Name = "deleting", Type = FlexJamType.Bool)]
		public bool Deleting { get; set; }
	}
}
