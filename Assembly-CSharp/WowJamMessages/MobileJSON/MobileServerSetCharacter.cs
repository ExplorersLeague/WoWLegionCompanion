using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4742, Name = "MobileServerSetCharacter", Version = 28333852u)]
	public class MobileServerSetCharacter
	{
		[FlexJamMember(Name = "characterGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "characterGUID")]
		public string CharacterGUID { get; set; }
	}
}
