using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4873, Name = "MobileClientPlayerLevelUp", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientPlayerLevelUp
	{
		[FlexJamMember(Name = "newLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "newLevel")]
		public int NewLevel { get; set; }
	}
}
