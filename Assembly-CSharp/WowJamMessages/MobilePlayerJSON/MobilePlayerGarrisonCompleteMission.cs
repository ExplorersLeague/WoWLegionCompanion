using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4783, Name = "MobilePlayerGarrisonCompleteMission", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonCompleteMission
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }
	}
}
