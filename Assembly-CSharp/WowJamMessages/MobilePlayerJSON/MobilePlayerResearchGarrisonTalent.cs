﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4800, Name = "MobilePlayerResearchGarrisonTalent", Version = 33577221u)]
	public class MobilePlayerResearchGarrisonTalent
	{
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		public int GarrTalentID { get; set; }
	}
}
