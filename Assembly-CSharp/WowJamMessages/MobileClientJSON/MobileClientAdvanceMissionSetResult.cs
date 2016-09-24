using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4843, Name = "MobileClientAdvanceMissionSetResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientAdvanceMissionSetResult
	{
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		public int MissionSetID { get; set; }

		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "success")]
		public bool Success { get; set; }
	}
}
