using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileWorldQuestReward", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileWorldQuestReward
	{
		public MobileWorldQuestReward()
		{
			this.FileDataID = 0;
			this.ItemContext = 0;
		}

		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		public int ItemContext { get; set; }

		[FlexJamMember(Name = "recordID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "recordID")]
		public int RecordID { get; set; }

		[FlexJamMember(Name = "fileDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "fileDataID")]
		public int FileDataID { get; set; }

		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		public int Quantity { get; set; }
	}
}
