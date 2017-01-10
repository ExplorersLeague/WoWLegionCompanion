using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4857, Name = "MobileClientEvaluateMissionResult", Version = 33577221u)]
	public class MobileClientEvaluateMissionResult
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[FlexJamMember(Name = "successChance", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "successChance")]
		public int SuccessChance { get; set; }

		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }
	}
}
