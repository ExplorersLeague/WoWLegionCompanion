using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4742, Name = "MobileServerSetCharacter", Version = 28333852u)]
	public class MobileServerSetCharacter
	{
		[System.Runtime.Serialization.DataMember(Name = "characterGUID")]
		[FlexJamMember(Name = "characterGUID", Type = FlexJamType.WowGuid)]
		public string CharacterGUID { get; set; }
	}
}
