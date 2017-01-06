using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamGarrisonMissionReward", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamGarrisonMissionReward
	{
		public JamGarrisonMissionReward()
		{
			this.ItemFileDataID = 0;
		}

		[System.Runtime.Serialization.DataMember(Name = "itemFileDataID")]
		[FlexJamMember(Name = "itemFileDataID", Type = FlexJamType.Int32)]
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

		[System.Runtime.Serialization.DataMember(Name = "currencyQuantity")]
		[FlexJamMember(Name = "currencyQuantity", Type = FlexJamType.UInt32)]
		public uint CurrencyQuantity { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemQuantity")]
		[FlexJamMember(Name = "itemQuantity", Type = FlexJamType.UInt32)]
		public uint ItemQuantity { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrMssnBonusAbilityID")]
		[FlexJamMember(Name = "garrMssnBonusAbilityID", Type = FlexJamType.UInt32)]
		public uint GarrMssnBonusAbilityID { get; set; }
	}
}
