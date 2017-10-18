using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4799, Name = "MobilePlayerResearchGarrisonTalent", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerResearchGarrisonTalent
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		public int GarrTalentID { get; set; }
	}
}
