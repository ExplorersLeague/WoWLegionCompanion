using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4812, Name = "MobilePlayerMakeContribution", Version = 38820897u)]
	public class MobilePlayerMakeContribution
	{
		[FlexJamMember(Name = "contributionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "contributionID")]
		public int ContributionID { get; set; }
	}
}
