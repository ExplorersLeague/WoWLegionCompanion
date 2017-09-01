using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4799, Name = "MobilePlayerResearchGarrisonTalent", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerResearchGarrisonTalent
	{
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		public int GarrTalentID { get; set; }
	}
}
