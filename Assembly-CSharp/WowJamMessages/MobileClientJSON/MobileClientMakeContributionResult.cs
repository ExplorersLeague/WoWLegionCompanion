using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4886, Name = "MobileClientMakeContributionResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientMakeContributionResult
	{
		[FlexJamMember(Name = "contributionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "contributionID")]
		public int ContributionID { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public bool Result { get; set; }
	}
}
