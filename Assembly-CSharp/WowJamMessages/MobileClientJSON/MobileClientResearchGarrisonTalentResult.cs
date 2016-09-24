using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4859, Name = "MobileClientResearchGarrisonTalentResult", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientResearchGarrisonTalentResult
	{
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		public int GarrTalentID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }
	}
}
