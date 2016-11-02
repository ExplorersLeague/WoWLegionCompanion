﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamBattlePayShopEntry", Version = 28333852u)]
	public class JamBattlePayShopEntry
	{
		[System.Runtime.Serialization.DataMember(Name = "entryID")]
		[FlexJamMember(Name = "entryID", Type = FlexJamType.UInt32)]
		public uint EntryID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[FlexJamMember(Name = "ordering", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "ordering")]
		public int Ordering { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bannerType")]
		[FlexJamMember(Name = "bannerType", Type = FlexJamType.UInt8)]
		public byte BannerType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "productID")]
		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		public uint ProductID { get; set; }

		[FlexJamMember(Name = "groupID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "groupID")]
		public uint GroupID { get; set; }
	}
}
