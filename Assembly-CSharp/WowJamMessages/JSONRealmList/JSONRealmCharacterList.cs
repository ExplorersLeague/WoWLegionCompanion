using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamMessage(Id = 15036, Name = "JSONRealmCharacterList", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JSONRealmCharacterList
	{
		[System.Runtime.Serialization.DataMember(Name = "characterList")]
		[FlexJamMember(ArrayDimensions = 1, Name = "characterList", Type = FlexJamType.Struct)]
		public JamJSONCharacterEntry[] CharacterList { get; set; }
	}
}
