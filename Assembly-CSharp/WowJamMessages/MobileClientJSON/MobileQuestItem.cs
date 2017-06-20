using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileQuestItem", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileQuestItem
	{
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		public int Quantity { get; set; }
	}
}
