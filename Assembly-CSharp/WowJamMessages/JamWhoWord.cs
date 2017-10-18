using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamWhoWord", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamWhoWord
	{
		[System.Runtime.Serialization.DataMember(Name = "word")]
		[FlexJamMember(Name = "word", Type = FlexJamType.String)]
		public string Word { get; set; }
	}
}
