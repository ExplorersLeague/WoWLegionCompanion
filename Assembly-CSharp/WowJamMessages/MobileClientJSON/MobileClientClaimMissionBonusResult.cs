﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4847, Name = "MobileClientClaimMissionBonusResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientClaimMissionBonusResult
	{
		[FlexJamMember(Name = "awardOvermax", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "awardOvermax")]
		public bool AwardOvermax { get; set; }

		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "mission")]
		public JamGarrisonMobileMission Mission { get; set; }
	}
}
