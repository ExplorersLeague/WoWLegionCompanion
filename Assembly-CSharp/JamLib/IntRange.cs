using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "CiRange")]
	public struct IntRange
	{
		[System.Runtime.Serialization.DataMember(Name = "l")]
		[FlexJamMember(Name = "l", Type = FlexJamType.Int32)]
		public int Low { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "h")]
		[FlexJamMember(Name = "h", Type = FlexJamType.Int32)]
		public int High { get; set; }
	}
}
