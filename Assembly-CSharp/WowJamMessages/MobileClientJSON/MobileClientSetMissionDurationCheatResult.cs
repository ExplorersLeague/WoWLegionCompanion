using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4868, Name = "MobileClientSetMissionDurationCheatResult", Version = 39869590u)]
	public class MobileClientSetMissionDurationCheatResult
	{
		[System.Runtime.Serialization.DataMember(Name = "success")]
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		public bool Success { get; set; }
	}
}
