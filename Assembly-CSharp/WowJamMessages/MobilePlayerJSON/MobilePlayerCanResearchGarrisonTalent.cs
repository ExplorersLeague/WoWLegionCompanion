using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4799, Name = "MobilePlayerCanResearchGarrisonTalent", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerCanResearchGarrisonTalent
	{
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		public int GarrTalentID { get; set; }
	}
}
