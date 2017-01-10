using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayProduct", Version = 28333852u)]
	public class JamBattlePayProduct
	{
		[FlexJamMember(Name = "currentPriceFixedPoint", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "currentPriceFixedPoint")]
		public ulong CurrentPriceFixedPoint { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public uint Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[FlexJamMember(Name = "normalPriceFixedPoint", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "normalPriceFixedPoint")]
		public ulong NormalPriceFixedPoint { get; set; }

		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "productID")]
		public uint ProductID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "type")]
		[FlexJamMember(Name = "type", Type = FlexJamType.UInt8)]
		public byte Type { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "deliverables")]
		[FlexJamMember(ArrayDimensions = 1, Name = "deliverables", Type = FlexJamType.UInt32)]
		public uint[] Deliverables { get; set; }
	}
}
