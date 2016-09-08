using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4783, Name = "MobilePlayerGarrisonCompleteMission", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonCompleteMission
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }
	}
}
