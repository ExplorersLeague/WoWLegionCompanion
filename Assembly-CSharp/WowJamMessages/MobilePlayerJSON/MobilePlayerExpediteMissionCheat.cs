using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4787, Name = "MobilePlayerExpediteMissionCheat", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerExpediteMissionCheat
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }
	}
}
