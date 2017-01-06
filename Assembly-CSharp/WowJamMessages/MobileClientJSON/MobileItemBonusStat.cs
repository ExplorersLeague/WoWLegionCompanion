using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileItemBonusStat", Version = 33577221u)]
	public class MobileItemBonusStat
	{
		[System.Runtime.Serialization.DataMember(Name = "statID")]
		[FlexJamMember(Name = "statID", Type = FlexJamType.Int32)]
		public int StatID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusAmount")]
		[FlexJamMember(Name = "bonusAmount", Type = FlexJamType.Int32)]
		public int BonusAmount { get; set; }

		[FlexJamMember(Name = "color", Type = FlexJamType.Enum)]
		[System.Runtime.Serialization.DataMember(Name = "color")]
		public MobileStatColor Color { get; set; }
	}
}
