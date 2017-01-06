using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "vector4")]
	public struct Vector4
	{
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "x")]
		public float X { get; set; }

		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "y")]
		public float Y { get; set; }

		[FlexJamMember(Name = "z", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "z")]
		public float Z { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "w")]
		[FlexJamMember(Name = "w", Type = FlexJamType.Float)]
		public float W { get; set; }
	}
}
