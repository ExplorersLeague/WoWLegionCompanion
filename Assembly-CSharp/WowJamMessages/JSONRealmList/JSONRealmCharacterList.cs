using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15036, Name = "JSONRealmCharacterList", Version = 47212487u)]
	public class JSONRealmCharacterList
	{
		[DataMember(Name = "characterList")]
		[FlexJamMember(ArrayDimensions = 1, Name = "characterList", Type = FlexJamType.Struct)]
		public JamJSONCharacterEntry[] CharacterList { get; set; }
	}
}
