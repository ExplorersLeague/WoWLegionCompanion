using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4809, Name = "MobilePlayerGetItemTooltipInfo", Version = 38820897u)]
	public class MobilePlayerGetItemTooltipInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		public int ItemContext { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }
	}
}
