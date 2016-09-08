using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[FlexJamStruct(Name = "CiRange")]
	[System.Runtime.Serialization.DataContract]
	public struct IntRange
	{
		[FlexJamMember(Name = "l", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "l")]
		public int Low { get; set; }

		[FlexJamMember(Name = "h", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "h")]
		public int High { get; set; }
	}
}
