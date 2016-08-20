using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4839, Name = "MobileGarrisonRemoveMissionArchive", Version = 28333852u)]
	public class MobileGarrisonRemoveMissionArchive
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }

		[FlexJamMember(Name = "missionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "missionID")]
		public int MissionID { get; set; }
	}
}
