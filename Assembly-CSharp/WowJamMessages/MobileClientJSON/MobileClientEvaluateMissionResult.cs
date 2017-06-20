using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4867, Name = "MobileClientEvaluateMissionResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientEvaluateMissionResult
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "successChance")]
		[FlexJamMember(Name = "successChance", Type = FlexJamType.Int32)]
		public int SuccessChance { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }
	}
}
