using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4849, Name = "MobileGarrisonRemoveMissionArchive", Version = 39869590u)]
	public class MobileGarrisonRemoveMissionArchive
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionID")]
		[FlexJamMember(Name = "missionID", Type = FlexJamType.Int32)]
		public int MissionID { get; set; }
	}
}
