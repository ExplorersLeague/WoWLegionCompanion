using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4890, Name = "MobileClientQuestCompleted", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientQuestCompleted
	{
		[System.Runtime.Serialization.DataMember(Name = "item")]
		[FlexJamMember(ArrayDimensions = 1, Name = "item", Type = FlexJamType.Struct)]
		public MobileQuestItem[] Item { get; set; }

		[FlexJamMember(Name = "questID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "questID")]
		public int QuestID { get; set; }
	}
}
