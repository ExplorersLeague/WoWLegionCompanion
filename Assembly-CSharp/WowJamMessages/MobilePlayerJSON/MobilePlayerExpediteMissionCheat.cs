using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4787, Name = "MobilePlayerExpediteMissionCheat", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerExpediteMissionCheat
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }
	}
}
