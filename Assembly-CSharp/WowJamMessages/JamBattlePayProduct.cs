using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayProduct", Version = 28333852u)]
	public class JamBattlePayProduct
	{
		[System.Runtime.Serialization.DataMember(Name = "currentPriceFixedPoint")]
		[FlexJamMember(Name = "currentPriceFixedPoint", Type = FlexJamType.UInt64)]
		public ulong CurrentPriceFixedPoint { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "normalPriceFixedPoint")]
		[FlexJamMember(Name = "normalPriceFixedPoint", Type = FlexJamType.UInt64)]
		public ulong NormalPriceFixedPoint { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "productID")]
		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		public uint ProductID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "type")]
		[FlexJamMember(Name = "type", Type = FlexJamType.UInt8)]
		public byte Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "deliverables")]
		[FlexJamMember(ArrayDimensions = 1, Name = "deliverables", Type = FlexJamType.UInt32)]
		public uint[] Deliverables { get; set; }
	}
}
