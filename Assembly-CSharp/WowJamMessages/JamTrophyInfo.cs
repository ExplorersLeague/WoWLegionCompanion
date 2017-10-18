using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamTrophyInfo", Version = 28333852u)]
	public class JamTrophyInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "canUseReason")]
		[FlexJamMember(Name = "canUseReason", Type = FlexJamType.Int32)]
		public int CanUseReason { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "canUseData")]
		[FlexJamMember(Name = "canUseData", Type = FlexJamType.Int32)]
		public int CanUseData { get; set; }

		[FlexJamMember(Name = "trophyID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "trophyID")]
		public int TrophyID { get; set; }
	}
}
