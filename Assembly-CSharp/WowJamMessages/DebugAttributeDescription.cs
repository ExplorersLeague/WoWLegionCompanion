using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "DebugAttributeDescription", Version = 28333852u)]
	public class DebugAttributeDescription
	{
		[System.Runtime.Serialization.DataMember(Name = "key")]
		[FlexJamMember(Name = "key", Type = FlexJamType.String)]
		public string Key { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "descriptionData")]
		[FlexJamMember(Name = "descriptionData", Type = FlexJamType.String)]
		public string DescriptionData { get; set; }

		[FlexJamMember(Name = "type", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "type")]
		public int Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		public int Flags { get; set; }
	}
}
