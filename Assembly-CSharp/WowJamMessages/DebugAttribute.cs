using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "DebugAttribute", Version = 28333852u)]
	public class DebugAttribute
	{
		[FlexJamMember(Name = "key", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "key")]
		public string Key { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "value")]
		[FlexJamMember(Name = "value", Type = FlexJamType.Struct)]
		public AttributeValue Value { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "param")]
		[FlexJamMember(Name = "param", Type = FlexJamType.Int32)]
		public int Param { get; set; }
	}
}
