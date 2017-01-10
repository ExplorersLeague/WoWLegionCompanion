using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4800, Name = "MobilePlayerResearchGarrisonTalent", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerResearchGarrisonTalent
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		public int GarrTalentID { get; set; }
	}
}
