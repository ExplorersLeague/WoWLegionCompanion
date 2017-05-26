using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4782, Name = "MobilePlayerGarrisonCompleteMission", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonCompleteMission
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }
	}
}
