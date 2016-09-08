using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[FlexJamMessage(Id = 4742, Name = "MobileServerSetCharacter", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileServerSetCharacter
	{
		[FlexJamMember(Name = "characterGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "characterGUID")]
		public string CharacterGUID { get; set; }
	}
}
