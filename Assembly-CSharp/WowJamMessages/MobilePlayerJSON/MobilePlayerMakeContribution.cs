using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4812, Name = "MobilePlayerMakeContribution", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerMakeContribution
	{
		[System.Runtime.Serialization.DataMember(Name = "contributionID")]
		[FlexJamMember(Name = "contributionID", Type = FlexJamType.Int32)]
		public int ContributionID { get; set; }
	}
}
