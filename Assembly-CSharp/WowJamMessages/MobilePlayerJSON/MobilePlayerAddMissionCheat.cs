using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4786, Name = "MobilePlayerAddMissionCheat", Version = 28333852u)]
	public class MobilePlayerAddMissionCheat
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }
	}
}
