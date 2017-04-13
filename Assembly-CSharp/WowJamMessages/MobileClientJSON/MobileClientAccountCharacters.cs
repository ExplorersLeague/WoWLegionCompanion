using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4858, Name = "MobileClientAccountCharacters", Version = 39869590u)]
	public class MobileClientAccountCharacters
	{
		[System.Runtime.Serialization.DataMember(Name = "characters")]
		[FlexJamMember(ArrayDimensions = 1, Name = "characters", Type = FlexJamType.Struct)]
		public MobilePlayerCharacter[] Characters { get; set; }
	}
}
