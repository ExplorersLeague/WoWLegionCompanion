using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamMessage(Id = 15030, Name = "JSONRealmCharacterCountList", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JSONRealmCharacterCountList
	{
		[System.Runtime.Serialization.DataMember(Name = "counts")]
		[FlexJamMember(ArrayDimensions = 1, Name = "counts", Type = FlexJamType.Struct)]
		public JamJSONRealmCharacterCount[] Counts { get; set; }
	}
}
