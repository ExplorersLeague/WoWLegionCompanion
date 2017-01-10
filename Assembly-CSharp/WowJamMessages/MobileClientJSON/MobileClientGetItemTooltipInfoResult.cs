using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4870, Name = "MobileClientGetItemTooltipInfoResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGetItemTooltipInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		public int ItemContext { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "stats")]
		[FlexJamMember(Name = "stats", Type = FlexJamType.Struct)]
		public MobileItemStats Stats { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }
	}
}
