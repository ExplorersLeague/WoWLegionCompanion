using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4862, Name = "MobileClientFollowerArmamentsResult", Version = 28333852u)]
	public class MobileClientFollowerArmamentsResult
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "armament", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "armament")]
		public MobileFollowerArmament[] Armament { get; set; }
	}
}
