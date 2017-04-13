using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4878, Name = "MobileClientWorldQuestInactiveBountiesResult", Version = 39869590u)]
	public class MobileClientWorldQuestInactiveBountiesResult
	{
		[System.Runtime.Serialization.DataMember(Name = "bounty")]
		[FlexJamMember(ArrayDimensions = 1, Name = "bounty", Type = FlexJamType.Struct)]
		public MobileWorldQuestBounty[] Bounty { get; set; }
	}
}
