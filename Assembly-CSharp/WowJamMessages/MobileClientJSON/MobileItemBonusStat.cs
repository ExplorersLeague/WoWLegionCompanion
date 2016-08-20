using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileItemBonusStat", Version = 28333852u)]
	public class MobileItemBonusStat
	{
		[FlexJamMember(Name = "statID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "statID")]
		public int StatID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusAmount")]
		[FlexJamMember(Name = "bonusAmount", Type = FlexJamType.Int32)]
		public int BonusAmount { get; set; }

		[FlexJamMember(Name = "color", Type = FlexJamType.Enum)]
		[System.Runtime.Serialization.DataMember(Name = "color")]
		public MobileStatColor Color { get; set; }
	}
}
