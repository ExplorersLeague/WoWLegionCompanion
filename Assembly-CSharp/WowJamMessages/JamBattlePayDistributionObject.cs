using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayDistributionObject", Version = 28333852u)]
	public class JamBattlePayDistributionObject
	{
		[FlexJamMember(Name = "revoked", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "revoked")]
		public bool Revoked { get; set; }

		[FlexJamMember(Name = "deliverableID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "deliverableID")]
		public uint DeliverableID { get; set; }

		[FlexJamMember(Name = "targetPlayer", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "targetPlayer")]
		public string TargetPlayer { get; set; }

		[FlexJamMember(Optional = true, Name = "deliverable", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "deliverable")]
		public JamBattlePayDeliverable[] Deliverable { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "purchaseID")]
		[FlexJamMember(Name = "purchaseID", Type = FlexJamType.UInt64)]
		public ulong PurchaseID { get; set; }

		[FlexJamMember(Name = "status", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "status")]
		public uint Status { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "targetNativeRealm")]
		[FlexJamMember(Name = "targetNativeRealm", Type = FlexJamType.UInt32)]
		public uint TargetNativeRealm { get; set; }

		[FlexJamMember(Name = "distributionID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "distributionID")]
		public ulong DistributionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "targetVirtualRealm")]
		[FlexJamMember(Name = "targetVirtualRealm", Type = FlexJamType.UInt32)]
		public uint TargetVirtualRealm { get; set; }
	}
}
