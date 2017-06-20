using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[FlexJamStruct(Name = "vector4")]
	[System.Runtime.Serialization.DataContract]
	public struct Vector4
	{
		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		public float X { get; set; }

		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "y")]
		public float Y { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "z")]
		[FlexJamMember(Name = "z", Type = FlexJamType.Float)]
		public float Z { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "w")]
		[FlexJamMember(Name = "w", Type = FlexJamType.Float)]
		public float W { get; set; }
	}
}
