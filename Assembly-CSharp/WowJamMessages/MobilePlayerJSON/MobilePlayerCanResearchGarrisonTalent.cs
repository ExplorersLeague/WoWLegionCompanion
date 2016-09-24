using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4799, Name = "MobilePlayerCanResearchGarrisonTalent", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCanResearchGarrisonTalent
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		public int GarrTalentID { get; set; }
	}
}
