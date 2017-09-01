using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4868, Name = "MobileClientSetMissionDurationCheatResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientSetMissionDurationCheatResult
	{
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "success")]
		public bool Success { get; set; }
	}
}
