using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4842, Name = "MobileClientExpediteMissionCheatResult", Version = 28333852u)]
	public class MobileClientExpediteMissionCheatResult
	{
		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "missionRecID")]
		[FlexJamMember(Name = "missionRecID", Type = FlexJamType.Int32)]
		public int MissionRecID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "mission")]
		[FlexJamMember(Name = "mission", Type = FlexJamType.Struct)]
		public JamGarrisonMobileMission Mission { get; set; }
	}
}
