using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlePayDistributionObject", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlePayDistributionObject
	{
		[FlexJamMember(Name = "revoked", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "revoked")]
		public bool Revoked { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "deliverableID")]
		[FlexJamMember(Name = "deliverableID", Type = FlexJamType.UInt32)]
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

		[System.Runtime.Serialization.DataMember(Name = "status")]
		[FlexJamMember(Name = "status", Type = FlexJamType.UInt32)]
		public uint Status { get; set; }

		[FlexJamMember(Name = "targetNativeRealm", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "targetNativeRealm")]
		public uint TargetNativeRealm { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "distributionID")]
		[FlexJamMember(Name = "distributionID", Type = FlexJamType.UInt64)]
		public ulong DistributionID { get; set; }

		[FlexJamMember(Name = "targetVirtualRealm", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "targetVirtualRealm")]
		public uint TargetVirtualRealm { get; set; }
	}
}
