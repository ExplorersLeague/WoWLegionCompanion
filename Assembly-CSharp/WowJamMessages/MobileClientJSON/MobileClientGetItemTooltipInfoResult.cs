using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4881, Name = "MobileClientGetItemTooltipInfoResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGetItemTooltipInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		public int ItemContext { get; set; }

		[FlexJamMember(Name = "stats", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "stats")]
		public MobileItemStats Stats { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }
	}
}
