using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamJSONRealmEntry", Version = 28333852u)]
	public class JamJSONRealmEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "wowRealmAddress")]
		[FlexJamMember(Name = "wowRealmAddress", Type = FlexJamType.UInt32)]
		public uint WowRealmAddress { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgTimezonesID")]
		[FlexJamMember(Name = "cfgTimezonesID", Type = FlexJamType.Int32)]
		public int CfgTimezonesID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "populationState")]
		[FlexJamMember(Name = "populationState", Type = FlexJamType.Int32)]
		public int PopulationState { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgCategoriesID")]
		[FlexJamMember(Name = "cfgCategoriesID", Type = FlexJamType.Int32)]
		public int CfgCategoriesID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Struct)]
		public JamJSONGameVersion Version { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgRealmsID")]
		[FlexJamMember(Name = "cfgRealmsID", Type = FlexJamType.Int32)]
		public int CfgRealmsID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgConfigsID")]
		[FlexJamMember(Name = "cfgConfigsID", Type = FlexJamType.Int32)]
		public int CfgConfigsID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "cfgLanguagesID")]
		[FlexJamMember(Name = "cfgLanguagesID", Type = FlexJamType.Int32)]
		public int CfgLanguagesID { get; set; }
	}
}
