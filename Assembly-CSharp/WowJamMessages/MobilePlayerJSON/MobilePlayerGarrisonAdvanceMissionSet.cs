using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4788, Name = "MobilePlayerGarrisonAdvanceMissionSet", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonAdvanceMissionSet
	{
		[FlexJamMember(Name = "missionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "missionSetID")]
		public int MissionSetID { get; set; }

		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }
	}
}
