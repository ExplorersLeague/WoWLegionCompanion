using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4881, Name = "MobileClientGetItemTooltipInfoResult", Version = 39869590u)]
	public class MobileClientGetItemTooltipInfoResult
	{
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		public int ItemContext { get; set; }

		[FlexJamMember(Name = "stats", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "stats")]
		public MobileItemStats Stats { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }
	}
}
