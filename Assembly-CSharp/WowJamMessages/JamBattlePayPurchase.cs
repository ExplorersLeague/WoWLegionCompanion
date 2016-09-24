using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayPurchase", Version = 28333852u)]
	public class JamBattlePayPurchase
	{
		[System.Runtime.Serialization.DataMember(Name = "purchaseID")]
		[FlexJamMember(Name = "purchaseID", Type = FlexJamType.UInt64)]
		public ulong PurchaseID { get; set; }

		[FlexJamMember(Name = "status", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "status")]
		public uint Status { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "resultCode")]
		[FlexJamMember(Name = "resultCode", Type = FlexJamType.UInt32)]
		public uint ResultCode { get; set; }

		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "productID")]
		public uint ProductID { get; set; }

		[FlexJamMember(Name = "walletName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "walletName")]
		public string WalletName { get; set; }
	}
}
