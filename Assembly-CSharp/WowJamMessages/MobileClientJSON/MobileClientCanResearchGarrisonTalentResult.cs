using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4870, Name = "MobileClientCanResearchGarrisonTalentResult", Version = 39869590u)]
	public class MobileClientCanResearchGarrisonTalentResult
	{
		[System.Runtime.Serialization.DataMember(Name = "conditionText")]
		[FlexJamMember(Name = "conditionText", Type = FlexJamType.String)]
		public string ConditionText { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTalentID")]
		[FlexJamMember(Name = "garrTalentID", Type = FlexJamType.Int32)]
		public int GarrTalentID { get; set; }
	}
}
