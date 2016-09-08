using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "DebugAttributeDescription", Version = 28333852u)]
	public class DebugAttributeDescription
	{
		[FlexJamMember(Name = "key", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "key")]
		public string Key { get; set; }

		[FlexJamMember(Name = "descriptionData", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "descriptionData")]
		public string DescriptionData { get; set; }

		[FlexJamMember(Name = "type", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "type")]
		public int Type { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public int Flags { get; set; }
	}
}
