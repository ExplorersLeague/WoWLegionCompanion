using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonMissionSet", Version = 28333852u)]
	public class JamGarrisonMissionSet
	{
		[FlexJamMember(Name = "garrMissionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionSetID")]
		public int GarrMissionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lastUpdateTime")]
		[FlexJamMember(Name = "lastUpdateTime", Type = FlexJamType.Int32)]
		public int LastUpdateTime { get; set; }
	}
}
