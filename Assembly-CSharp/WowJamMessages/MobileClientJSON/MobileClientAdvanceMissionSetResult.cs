using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4853, Name = "MobileClientAdvanceMissionSetResult", Version = 39869590u)]
	public class MobileClientAdvanceMissionSetResult
	{
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		public int MissionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "success")]
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		public bool Success { get; set; }
	}
}
