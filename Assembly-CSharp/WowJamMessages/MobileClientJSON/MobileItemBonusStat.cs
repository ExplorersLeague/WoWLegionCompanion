using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileItemBonusStat", Version = 39869590u)]
	public class MobileItemBonusStat
	{
		[System.Runtime.Serialization.DataMember(Name = "statID")]
		[FlexJamMember(Name = "statID", Type = FlexJamType.Int32)]
		public int StatID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bonusAmount")]
		[FlexJamMember(Name = "bonusAmount", Type = FlexJamType.Int32)]
		public int BonusAmount { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "color")]
		[FlexJamMember(Name = "color", Type = FlexJamType.Enum)]
		public MobileStatColor Color { get; set; }
	}
}
