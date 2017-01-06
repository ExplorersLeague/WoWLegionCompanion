using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4788, Name = "MobilePlayerGarrisonAdvanceMissionSet", Version = 33577221u)]
	public class MobilePlayerGarrisonAdvanceMissionSet
	{
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		public int MissionSetID { get; set; }

		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }
	}
}
