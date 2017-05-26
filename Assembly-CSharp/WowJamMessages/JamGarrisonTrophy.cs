using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonTrophy", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonTrophy
	{
		[FlexJamMember(Name = "trophyInstanceID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "trophyInstanceID")]
		public int TrophyInstanceID { get; set; }

		[FlexJamMember(Name = "trophyID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "trophyID")]
		public int TrophyID { get; set; }
	}
}
