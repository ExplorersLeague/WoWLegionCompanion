using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4788, Name = "MobilePlayerGarrisonAdvanceMissionSet", Version = 28333852u)]
	public class MobilePlayerGarrisonAdvanceMissionSet
	{
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		public int MissionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}
