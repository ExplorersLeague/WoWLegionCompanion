using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4864, Name = "MobileClientFollowerArmamentsResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientFollowerArmamentsResult
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "armament", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "armament")]
		public MobileFollowerArmament[] Armament { get; set; }
	}
}
