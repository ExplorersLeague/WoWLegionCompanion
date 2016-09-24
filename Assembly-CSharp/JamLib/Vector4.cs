using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "vector4")]
	public struct Vector4
	{
		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		public float X { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "y")]
		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		public float Y { get; set; }

		[FlexJamMember(Name = "z", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "z")]
		public float Z { get; set; }

		[FlexJamMember(Name = "w", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "w")]
		public float W { get; set; }
	}
}
