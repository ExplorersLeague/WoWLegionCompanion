using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "SystemDebugInfo", Version = 28333852u)]
	public class SystemDebugInfo
	{
		public SystemDebugInfo()
		{
			this.RequestParameter = string.Empty;
		}

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "attributeDescriptions", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "attributeDescriptions")]
		public DebugAttributeDescription[] AttributeDescriptions { get; set; }

		[FlexJamMember(Name = "updateTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "updateTime")]
		public int UpdateTime { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "attributes", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "attributes")]
		public DebugAttribute[] Attributes { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "requestParameter")]
		[FlexJamMember(Name = "requestParameter", Type = FlexJamType.String)]
		public string RequestParameter { get; set; }
	}
}
