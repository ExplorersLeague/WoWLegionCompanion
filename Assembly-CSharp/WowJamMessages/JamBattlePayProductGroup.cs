using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlePayProductGroup", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlePayProductGroup
	{
		[FlexJamMember(Name = "iconFileDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "iconFileDataID")]
		public int IconFileDataID { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayType")]
		[FlexJamMember(Name = "displayType", Type = FlexJamType.UInt8)]
		public byte DisplayType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ordering")]
		[FlexJamMember(Name = "ordering", Type = FlexJamType.Int32)]
		public int Ordering { get; set; }

		[FlexJamMember(Name = "groupID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "groupID")]
		public uint GroupID { get; set; }
	}
}
