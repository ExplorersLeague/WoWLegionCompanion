using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4787, Name = "MobilePlayerGarrisonAdvanceMissionSet", Version = 38820897u)]
	public class MobilePlayerGarrisonAdvanceMissionSet
	{
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		public int MissionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}
