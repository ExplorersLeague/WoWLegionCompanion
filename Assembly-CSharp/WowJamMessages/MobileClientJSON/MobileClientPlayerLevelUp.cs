using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4884, Name = "MobileClientPlayerLevelUp", Version = 39869590u)]
	public class MobileClientPlayerLevelUp
	{
		[FlexJamMember(Name = "newLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "newLevel")]
		public int NewLevel { get; set; }
	}
}
