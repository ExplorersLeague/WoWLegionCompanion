using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "vector2")]
	public struct Vector2
	{
		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		public float X { get; set; }

		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "y")]
		public float Y { get; set; }
	}
}
