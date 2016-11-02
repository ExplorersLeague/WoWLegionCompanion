using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4837, Name = "MobileClientClaimMissionBonusResult", Version = 33577221u)]
	public class MobileClientClaimMissionBonusResult
	{
		[FlexJamMember(Name = "awardOvermax", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "awardOvermax")]
		public bool AwardOvermax { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }
	}
}
