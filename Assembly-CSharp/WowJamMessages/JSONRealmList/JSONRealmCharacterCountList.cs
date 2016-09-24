using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15030, Name = "JSONRealmCharacterCountList", Version = 28333852u)]
	public class JSONRealmCharacterCountList
	{
		[System.Runtime.Serialization.DataMember(Name = "counts")]
		[FlexJamMember(ArrayDimensions = 1, Name = "counts", Type = FlexJamType.Struct)]
		public JamJSONRealmCharacterCount[] Counts { get; set; }
	}
}
