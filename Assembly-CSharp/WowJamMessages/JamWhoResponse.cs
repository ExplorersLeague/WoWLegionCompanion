using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamWhoResponse", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamWhoResponse
	{
		[System.Runtime.Serialization.DataMember(Name = "entries")]
		[FlexJamMember(ArrayDimensions = 1, Name = "entries", Type = FlexJamType.Struct)]
		public JamWhoEntry[] Entries { get; set; }
	}
}
