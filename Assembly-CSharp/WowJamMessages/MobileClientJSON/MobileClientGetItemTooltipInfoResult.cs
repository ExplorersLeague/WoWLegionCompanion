﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4881, Name = "MobileClientGetItemTooltipInfoResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGetItemTooltipInfoResult
	{
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		public int ItemContext { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "stats")]
		[FlexJamMember(Name = "stats", Type = FlexJamType.Struct)]
		public MobileItemStats Stats { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }
	}
}
