using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4837, Name = "MobileClientClaimMissionBonusResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientClaimMissionBonusResult
	{
		[FlexJamMember(Name = "awardOvermax", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "awardOvermax")]
		public bool AwardOvermax { get; set; }

		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		public JamGarrisonMobileMission Mission { get; set; }
	}
}
