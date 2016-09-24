using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileWorldQuestReward", Version = 28333852u)]
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

		[System.Runtime.Serialization.DataMember(Name = "fileDataID")]
		[FlexJamMember(Name = "fileDataID", Type = FlexJamType.Int32)]
		public int FileDataID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		public int Quantity { get; set; }
	}
}
