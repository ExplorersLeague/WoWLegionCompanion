using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4849, Name = "MobileGarrisonRemoveMissionArchive", Version = 39869590u)]
	public class MobileGarrisonRemoveMissionArchive
	{
		[System.Runtime.Serialization.DataMember(Name = "result")]
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }

		[FlexJamMember(Name = "missionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "missionID")]
		public int MissionID { get; set; }
	}
}
