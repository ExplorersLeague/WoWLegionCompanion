using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15036, Name = "JSONRealmCharacterList", Version = 28333852u)]
	public class JSONRealmCharacterList
	{
		[System.Runtime.Serialization.DataMember(Name = "characterList")]
		[FlexJamMember(ArrayDimensions = 1, Name = "characterList", Type = FlexJamType.Struct)]
		public JamJSONCharacterEntry[] CharacterList { get; set; }
	}
}
