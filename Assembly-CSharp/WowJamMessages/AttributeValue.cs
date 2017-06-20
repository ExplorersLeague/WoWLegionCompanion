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

		[FlexJamMember(Name = "intValue", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "intValue")]
		public int IntValue { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "vector3Value")]
		[FlexJamMember(Name = "vector3Value", Type = FlexJamType.Struct)]
		public Vector3 Vector3Value { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "type")]
		[FlexJamMember(Name = "type", Type = FlexJamType.Enum)]
		public AttributeValueType Type { get; set; }

		[FlexJamMember(Name = "floatValue", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "floatValue")]
		public float FloatValue { get; set; }

		[FlexJamMember(Name = "stringValue", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "stringValue")]
		public string StringValue { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guidValue")]
		[FlexJamMember(Name = "guidValue", Type = FlexJamType.WowGuid)]
		public string GuidValue { get; set; }
	}
}
