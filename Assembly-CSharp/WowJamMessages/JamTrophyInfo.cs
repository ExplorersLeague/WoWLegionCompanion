using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamTrophyInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamTrophyInfo
	{
		[FlexJamMember(Name = "canUseReason", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "canUseReason")]
		public int CanUseReason { get; set; }

		[FlexJamMember(Name = "canUseData", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "canUseData")]
		public int CanUseData { get; set; }

		[FlexJamMember(Name = "trophyID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "trophyID")]
		public int TrophyID { get; set; }
	}
}
