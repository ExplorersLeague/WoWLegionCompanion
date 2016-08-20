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

		[FlexJamMember(Name = "itemContext", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemContext")]
		public int ItemContext { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "recordID")]
		[FlexJamMember(Name = "recordID", Type = FlexJamType.Int32)]
		public int RecordID { get; set; }

		[FlexJamMember(Name = "fileDataID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "fileDataID")]
		public int FileDataID { get; set; }

		[FlexJamMember(Name = "quantity", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "quantity")]
		public int Quantity { get; set; }
	}
}
