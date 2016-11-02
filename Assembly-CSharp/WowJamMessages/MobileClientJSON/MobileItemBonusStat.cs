using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileItemBonusStat", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileItemBonusStat
	{
		[FlexJamMember(Name = "statID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "statID")]
		public int StatID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusAmount")]
		[FlexJamMember(Name = "bonusAmount", Type = FlexJamType.Int32)]
		public int BonusAmount { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "color")]
		[FlexJamMember(Name = "color", Type = FlexJamType.Enum)]
		public MobileStatColor Color { get; set; }
	}
}
