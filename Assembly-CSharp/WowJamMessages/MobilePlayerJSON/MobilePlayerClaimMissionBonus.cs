using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4784, Name = "MobilePlayerClaimMissionBonus", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerClaimMissionBonus
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }
	}
}
