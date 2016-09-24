using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4867, Name = "MobileClientGetItemTooltipInfoResult", Version = 28333852u)]
	public class MobileClientGetItemTooltipInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		public int ItemContext { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "stats")]
		[FlexJamMember(Name = "stats", Type = FlexJamType.Struct)]
		public MobileItemStats Stats { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }
	}
}
