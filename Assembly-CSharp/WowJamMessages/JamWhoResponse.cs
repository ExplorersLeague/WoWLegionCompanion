using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamWhoResponse", Version = 28333852u)]
	public class JamWhoResponse
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "entries", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "entries")]
		public JamWhoEntry[] Entries { get; set; }
	}
}
