using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlePayProduct", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlePayProduct
	{
		[FlexJamMember(Name = "currentPriceFixedPoint", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "currentPriceFixedPoint")]
		public ulong CurrentPriceFixedPoint { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "normalPriceFixedPoint")]
		[FlexJamMember(Name = "normalPriceFixedPoint", Type = FlexJamType.UInt64)]
		public ulong NormalPriceFixedPoint { get; set; }

		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "productID")]
		public uint ProductID { get; set; }

		[FlexJamMember(Name = "type", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "type")]
		public byte Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "deliverables")]
		[FlexJamMember(ArrayDimensions = 1, Name = "deliverables", Type = FlexJamType.UInt32)]
		public uint[] Deliverables { get; set; }
	}
}
