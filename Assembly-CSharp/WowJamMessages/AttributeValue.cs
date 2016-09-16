using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "AttributeValue", Version = 28333852u)]
	public class AttributeValue
	{
		public AttributeValue()
		{
			this.IntValue = 0;
			this.FloatValue = 0f;
			this.StringValue = string.Empty;
			this.GuidValue = "0000000000000000";
		}

		[System.Runtime.Serialization.DataMember(Name = "intValue")]
		[FlexJamMember(Name = "intValue", Type = FlexJamType.Int32)]
		public int IntValue { get; set; }

		[FlexJamMember(Name = "vector3Value", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "vector3Value")]
		public Vector3 Vector3Value { get; set; }

		[FlexJamMember(Name = "type", Type = FlexJamType.Enum)]
		[System.Runtime.Serialization.DataMember(Name = "type")]
		public AttributeValueType Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "floatValue")]
		[FlexJamMember(Name = "floatValue", Type = FlexJamType.Float)]
		public float FloatValue { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "stringValue")]
		[FlexJamMember(Name = "stringValue", Type = FlexJamType.String)]
		public string StringValue { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guidValue")]
		[FlexJamMember(Name = "guidValue", Type = FlexJamType.WowGuid)]
		public string GuidValue { get; set; }
	}
}
