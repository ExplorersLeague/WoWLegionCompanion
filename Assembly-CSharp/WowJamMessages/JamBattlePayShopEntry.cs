﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlePayShopEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlePayShopEntry
	{
		[FlexJamMember(Name = "entryID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "entryID")]
		public uint EntryID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		public uint Flags { get; set; }

		[FlexJamMember(Optional = true, Name = "displayInfo", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "displayInfo")]
		public JamBattlepayDisplayInfo[] DisplayInfo { get; set; }

		[FlexJamMember(Name = "ordering", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "ordering")]
		public int Ordering { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bannerType")]
		[FlexJamMember(Name = "bannerType", Type = FlexJamType.UInt8)]
		public byte BannerType { get; set; }

		[FlexJamMember(Name = "productID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "productID")]
		public uint ProductID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "groupID")]
		[FlexJamMember(Name = "groupID", Type = FlexJamType.UInt32)]
		public uint GroupID { get; set; }
	}
}
