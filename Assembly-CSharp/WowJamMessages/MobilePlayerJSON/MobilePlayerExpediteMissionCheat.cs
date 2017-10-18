using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4786, Name = "MobilePlayerExpediteMissionCheat", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerExpediteMissionCheat
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }
	}
}
