using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4860, Name = "MobileClientCanResearchGarrisonTalentResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientCanResearchGarrisonTalentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "conditionText")]
		[FlexJamMember(Name = "conditionText", Type = FlexJamType.String)]
		public string ConditionText { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		public int GarrTalentID { get; set; }
	}
}
