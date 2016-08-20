using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4837, Name = "MobileClientClaimMissionBonusResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientClaimMissionBonusResult
	{
		[System.Runtime.Serialization.DataMember(Name = "awardOvermax")]
		[FlexJamMember(Name = "awardOvermax", Type = FlexJamType.Bool)]
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
