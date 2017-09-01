using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4888, Name = "MobileClientArtifactKnowledgeInfoResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientArtifactKnowledgeInfoResult
	{
		[FlexJamMember(Name = "itemsInBags", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemsInBags")]
		public int ItemsInBags { get; set; }

		[FlexJamMember(Name = "itemsInMail", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemsInMail")]
		public int ItemsInMail { get; set; }

		[FlexJamMember(Name = "activeShipments", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "activeShipments")]
		public int ActiveShipments { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxLevel")]
		[FlexJamMember(Name = "maxLevel", Type = FlexJamType.Int32)]
		public int MaxLevel { get; set; }

		[FlexJamMember(Name = "itemsInBank", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemsInBank")]
		public int ItemsInBank { get; set; }

		[FlexJamMember(Name = "currentLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "currentLevel")]
		public int CurrentLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemsInLoot")]
		[FlexJamMember(Name = "itemsInLoot", Type = FlexJamType.Int32)]
		public int ItemsInLoot { get; set; }
	}
}
