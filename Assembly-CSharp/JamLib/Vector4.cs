using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "vector4")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Vector4
	{
		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		public float X { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "y")]
		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		public float Y { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "z")]
		[FlexJamMember(Name = "z", Type = FlexJamType.Float)]
		public float Z { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "w")]
		[FlexJamMember(Name = "w", Type = FlexJamType.Float)]
		public float W { get; set; }
	}
}
