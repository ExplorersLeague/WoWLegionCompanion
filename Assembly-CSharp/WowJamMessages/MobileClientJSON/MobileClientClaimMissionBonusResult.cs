using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4847, Name = "MobileClientClaimMissionBonusResult", Version = 39869590u)]
	public class MobileClientClaimMissionBonusResult
	{
		[System.Runtime.Serialization.DataMember(Name = "awardOvermax")]
		[FlexJamMember(Name = "awardOvermax", Type = FlexJamType.Bool)]
		public bool AwardOvermax { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }
	}
}
