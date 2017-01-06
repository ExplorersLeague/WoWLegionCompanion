using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "DebugAttribute", Version = 28333852u)]
	public class DebugAttribute
	{
		[System.Runtime.Serialization.DataMember(Name = "key")]
		[FlexJamMember(Name = "key", Type = FlexJamType.String)]
		public string Key { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "value")]
		[FlexJamMember(Name = "value", Type = FlexJamType.Struct)]
		public AttributeValue Value { get; set; }

		[FlexJamMember(Name = "param", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "param")]
		public int Param { get; set; }
	}
}
