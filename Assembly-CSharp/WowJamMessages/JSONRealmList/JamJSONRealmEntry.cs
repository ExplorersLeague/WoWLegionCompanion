using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmEntry
	{
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "wowRealmAddress")]
		public uint WowRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgTimezonesID")]
		[FlexJamMember(Name = "cfgTimezonesID", Type = FlexJamType.Int32)]
		public int CfgTimezonesID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "populationState")]
		[FlexJamMember(Name = "populationState", Type = FlexJamType.Int32)]
		public int PopulationState { get; set; }

		[FlexJamMember(Name = "cfgCategoriesID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "cfgCategoriesID")]
		public int CfgCategoriesID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Struct)]
		public JamJSONGameVersion Version { get; set; }

		[FlexJamMember(Name = "cfgRealmsID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "cfgRealmsID")]
		public int CfgRealmsID { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[FlexJamMember(Name = "cfgConfigsID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "cfgConfigsID")]
		public int CfgConfigsID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgLanguagesID")]
		[FlexJamMember(Name = "cfgLanguagesID", Type = FlexJamType.Int32)]
		public int CfgLanguagesID { get; set; }
	}
}
