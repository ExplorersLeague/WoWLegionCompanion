using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonMissionBonusAbility", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonMissionBonusAbility
	{
		[FlexJamMember(Name = "garrMssnBonusAbilityID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMssnBonusAbilityID")]
		public int GarrMssnBonusAbilityID { get; set; }

		[FlexJamMember(Name = "startTime", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "startTime")]
		public int StartTime { get; set; }
	}
}
