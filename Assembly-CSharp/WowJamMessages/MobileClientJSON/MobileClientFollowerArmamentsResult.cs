using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4874, Name = "MobileClientFollowerArmamentsResult", Version = 39869590u)]
	public class MobileClientFollowerArmamentsResult
	{
		[System.Runtime.Serialization.DataMember(Name = "armament")]
		[FlexJamMember(ArrayDimensions = 1, Name = "armament", Type = FlexJamType.Struct)]
		public MobileFollowerArmament[] Armament { get; set; }
	}
}
