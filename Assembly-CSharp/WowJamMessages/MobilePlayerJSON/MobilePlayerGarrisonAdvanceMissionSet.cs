using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4788, Name = "MobilePlayerGarrisonAdvanceMissionSet", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
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
