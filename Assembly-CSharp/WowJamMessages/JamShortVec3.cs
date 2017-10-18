using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamShortVec3", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamShortVec3
	{
		[FlexJamMember(Name = "z", Type = FlexJamType.Int16)]
		[System.Runtime.Serialization.DataMember(Name = "z")]
		public short Z { get; set; }

		[FlexJamMember(Name = "x", Type = FlexJamType.Int16)]
		[System.Runtime.Serialization.DataMember(Name = "x")]
		public short X { get; set; }

		[FlexJamMember(Name = "y", Type = FlexJamType.Int16)]
		[System.Runtime.Serialization.DataMember(Name = "y")]
		public short Y { get; set; }
	}
}
