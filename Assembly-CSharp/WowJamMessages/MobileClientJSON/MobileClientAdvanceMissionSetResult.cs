using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4843, Name = "MobileClientAdvanceMissionSetResult", Version = 28333852u)]
	public class MobileClientAdvanceMissionSetResult
	{
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		public int MissionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "success")]
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		public bool Success { get; set; }
	}
}
