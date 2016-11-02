using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4870, Name = "MobileClientGetItemTooltipInfoResult", Version = 33577221u)]
	public class MobileClientGetItemTooltipInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		public int ItemContext { get; set; }

		[FlexJamMember(Name = "stats", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "stats")]
		public MobileItemStats Stats { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }
	}
}
