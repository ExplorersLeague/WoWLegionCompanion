using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4886, Name = "MobileClientMakeContributionResult", Version = 39869590u)]
	public class MobileClientMakeContributionResult
	{
		[System.Runtime.Serialization.DataMember(Name = "contributionID")]
		[FlexJamMember(Name = "contributionID", Type = FlexJamType.Int32)]
		public int ContributionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Bool)]
		public bool Result { get; set; }
	}
}
