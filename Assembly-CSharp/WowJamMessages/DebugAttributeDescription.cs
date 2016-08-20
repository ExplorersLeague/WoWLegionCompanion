using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "DebugAttributeDescription", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
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

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		public int Flags { get; set; }
	}
}
