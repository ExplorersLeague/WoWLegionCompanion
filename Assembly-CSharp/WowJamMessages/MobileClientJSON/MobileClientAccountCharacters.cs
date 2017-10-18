using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4858, Name = "MobileClientAccountCharacters", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientAccountCharacters
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "characters", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "characters")]
		public MobilePlayerCharacter[] Characters { get; set; }
	}
}
