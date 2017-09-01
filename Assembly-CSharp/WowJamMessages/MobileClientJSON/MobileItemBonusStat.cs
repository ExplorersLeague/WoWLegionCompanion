using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileItemBonusStat", Version = 39869590u)]
	public class MobileItemBonusStat
	{
		[FlexJamMember(Name = "statID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "statID")]
		public int StatID { get; set; }

		[FlexJamMember(Name = "bonusAmount", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "bonusAmount")]
		public int BonusAmount { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "color")]
		[FlexJamMember(Name = "color", Type = FlexJamType.Enum)]
		public MobileStatColor Color { get; set; }
	}
}
