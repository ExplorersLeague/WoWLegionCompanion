using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "DebugAttribute", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class DebugAttribute
	{
		[System.Runtime.Serialization.DataMember(Name = "key")]
		[FlexJamMember(Name = "key", Type = FlexJamType.String)]
		public string Key { get; set; }

		[FlexJamMember(Name = "value", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "value")]
		public AttributeValue Value { get; set; }

		[FlexJamMember(Name = "param", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "param")]
		public int Param { get; set; }
	}
}
