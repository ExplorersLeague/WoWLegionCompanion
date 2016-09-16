using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamWhoResponse", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamWhoResponse
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "entries", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "entries")]
		public JamWhoEntry[] Entries { get; set; }
	}
}
