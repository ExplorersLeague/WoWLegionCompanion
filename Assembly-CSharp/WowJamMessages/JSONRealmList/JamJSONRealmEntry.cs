using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONRealmEntry", Version = 47212487u)]
	public class JamJSONRealmEntry
	{
		[DataMember(Name = "wowRealmAddress")]
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		public uint WowRealmAddress { get; set; }

		[DataMember(Name = "cfgTimezonesID")]
		[FlexJamMember(Name = "cfgTimezonesID", Type = FlexJamType.Int32)]
		public int CfgTimezonesID { get; set; }

		[DataMember(Name = "populationState")]
		[FlexJamMember(Name = "populationState", Type = FlexJamType.Int32)]
		public int PopulationState { get; set; }

		[DataMember(Name = "cfgCategoriesID")]
		[FlexJamMember(Name = "cfgCategoriesID", Type = FlexJamType.Int32)]
		public int CfgCategoriesID { get; set; }

		[DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Struct)]
		public JamJSONGameVersion Version { get; set; }

		[DataMember(Name = "cfgRealmsID")]
		[FlexJamMember(Name = "cfgRealmsID", Type = FlexJamType.Int32)]
		public int CfgRealmsID { get; set; }

		[DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[DataMember(Name = "cfgConfigsID")]
		[FlexJamMember(Name = "cfgConfigsID", Type = FlexJamType.Int32)]
		public int CfgConfigsID { get; set; }

		[DataMember(Name = "cfgLanguagesID")]
		[FlexJamMember(Name = "cfgLanguagesID", Type = FlexJamType.Int32)]
		public int CfgLanguagesID { get; set; }
	}
}
