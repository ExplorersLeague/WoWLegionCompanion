using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONRealmListUpdatePart", Version = 47212487u)]
	public class JamJSONRealmListUpdatePart
	{
		[DataMember(Name = "wowRealmAddress")]
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		public uint WowRealmAddress { get; set; }

		[DataMember(Name = "update")]
		[FlexJamMember(Name = "update", Type = FlexJamType.Struct)]
		public JamJSONRealmEntry Update { get; set; }

		[DataMember(Name = "deleting")]
		[FlexJamMember(Name = "deleting", Type = FlexJamType.Bool)]
		public bool Deleting { get; set; }
	}
}
