using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonTrophy", Version = 28333852u)]
	public class JamGarrisonTrophy
	{
		[FlexJamMember(Name = "trophyInstanceID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "trophyInstanceID")]
		public int TrophyInstanceID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "trophyID")]
		[FlexJamMember(Name = "trophyID", Type = FlexJamType.Int32)]
		public int TrophyID { get; set; }
	}
}
