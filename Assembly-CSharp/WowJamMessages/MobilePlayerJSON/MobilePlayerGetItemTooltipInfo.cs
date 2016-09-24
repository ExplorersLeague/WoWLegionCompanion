using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4810, Name = "MobilePlayerGetItemTooltipInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGetItemTooltipInfo
	{
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		public int ItemContext { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }
	}
}
