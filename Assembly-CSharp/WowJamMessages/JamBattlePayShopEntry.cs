using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlePayShopEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlePayShopEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "entryID")]
		[FlexJamMember(Name = "entryID", Type = FlexJamType.UInt32)]
		public uint EntryID { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[FlexJamMember(Name = "ordering", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "ordering")]
		public int Ordering { get; set; }

		[FlexJamMember(Name = "bannerType", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "bannerType")]
		public byte BannerType { get; set; }

		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "productID")]
		public uint ProductID { get; set; }

		[FlexJamMember(Name = "groupID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "groupID")]
		public uint GroupID { get; set; }
	}
}
