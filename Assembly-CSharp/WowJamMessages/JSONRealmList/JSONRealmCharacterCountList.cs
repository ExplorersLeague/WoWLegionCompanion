using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15030, Name = "JSONRealmCharacterCountList", Version = 47212487u)]
	public class JSONRealmCharacterCountList
	{
		[DataMember(Name = "counts")]
		[FlexJamMember(ArrayDimensions = 1, Name = "counts", Type = FlexJamType.Struct)]
		public JamJSONRealmCharacterCount[] Counts { get; set; }
	}
}
