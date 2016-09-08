using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamShortVec3", Version = 28333852u)]
	public class JamShortVec3
	{
		[FlexJamMember(Name = "z", Type = FlexJamType.Int16)]
		[System.Runtime.Serialization.DataMember(Name = "z")]
		public short Z { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Int16)]
		public short X { get; set; }

		[FlexJamMember(Name = "y", Type = FlexJamType.Int16)]
		[System.Runtime.Serialization.DataMember(Name = "y")]
		public short Y { get; set; }
	}
}
