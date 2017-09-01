using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4795, Name = "MobilePlayerEvaluateMission", Version = 38820897u)]
	public class MobilePlayerEvaluateMission
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "garrFollowerID", Type = FlexJamType.Int32)]
		public int[] GarrFollowerID { get; set; }
	}
}
