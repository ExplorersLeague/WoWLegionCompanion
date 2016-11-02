using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamShortVec3", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamShortVec3
	{
		[System.Runtime.Serialization.DataMember(Name = "z")]
		[FlexJamMember(Name = "z", Type = FlexJamType.Int16)]
		public short Z { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Int16)]
		public short X { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "y")]
		[FlexJamMember(Name = "y", Type = FlexJamType.Int16)]
		public short Y { get; set; }
	}
}
