using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonMissionReward", Version = 28333852u)]
	public class JamGarrisonMissionReward
	{
		public JamGarrisonMissionReward()
		{
			this.ItemFileDataID = 0;
		}

		[FlexJamMember(Name = "itemFileDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemFileDataID")]
		public int ItemFileDataID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currencyType")]
		[FlexJamMember(Name = "currencyType", Type = FlexJamType.Int32)]
		public int CurrencyType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "followerXP")]
		[FlexJamMember(Name = "followerXP", Type = FlexJamType.UInt32)]
		public uint FollowerXP { get; set; }

		[FlexJamMember(Name = "currencyQuantity", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "currencyQuantity")]
		public uint CurrencyQuantity { get; set; }

		[FlexJamMember(Name = "itemQuantity", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "itemQuantity")]
		public uint ItemQuantity { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrMssnBonusAbilityID")]
		[FlexJamMember(Name = "garrMssnBonusAbilityID", Type = FlexJamType.UInt32)]
		public uint GarrMssnBonusAbilityID { get; set; }
	}
}
