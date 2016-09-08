using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayShopEntry", Version = 28333852u)]
	public class JamBattlePayShopEntry
	{
		[FlexJamMember(Name = "entryID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "entryID")]
		public uint EntryID { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public uint Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ordering")]
		[FlexJamMember(Name = "ordering", Type = FlexJamType.Int32)]
		public int Ordering { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bannerType")]
		[FlexJamMember(Name = "bannerType", Type = FlexJamType.UInt8)]
		public byte BannerType { get; set; }

		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "productID")]
		public uint ProductID { get; set; }

		[FlexJamMember(Name = "groupID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "groupID")]
		public uint GroupID { get; set; }
	}
}
