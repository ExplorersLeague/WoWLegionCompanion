using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4848, Name = "MobileClientAccountCharacters", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientAccountCharacters
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "characters", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "characters")]
		public MobilePlayerCharacter[] Characters { get; set; }
	}
}
