using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4867, Name = "MobileClientWorldQuestInactiveBountiesResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientWorldQuestInactiveBountiesResult
	{
		[System.Runtime.Serialization.DataMember(Name = "bounty")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bounty", Type = FlexJamType.Struct)]
		public MobileWorldQuestBounty[] Bounty { get; set; }
	}
}
