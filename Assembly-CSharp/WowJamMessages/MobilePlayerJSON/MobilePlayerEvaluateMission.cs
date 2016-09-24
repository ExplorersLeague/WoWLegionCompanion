using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4796, Name = "MobilePlayerEvaluateMission", Version = 28333852u)]
	public class MobilePlayerEvaluateMission
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		[FlexJamMember(ArrayDimensions = 1, Name = "garrFollowerID", Type = FlexJamType.Int32)]
		public int[] GarrFollowerID { get; set; }
	}
}
