using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4786, Name = "MobilePlayerAddMissionCheat", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerAddMissionCheat
	{
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		public int GarrMissionID { get; set; }
	}
}
