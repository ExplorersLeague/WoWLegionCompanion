using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4888, Name = "MobileClientArtifactKnowledgeInfoResult", Version = 39869590u)]
	public class MobileClientArtifactKnowledgeInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "itemsInBags")]
		[FlexJamMember(Name = "itemsInBags", Type = FlexJamType.Int32)]
		public int ItemsInBags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemsInMail")]
		[FlexJamMember(Name = "itemsInMail", Type = FlexJamType.Int32)]
		public int ItemsInMail { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "activeShipments")]
		[FlexJamMember(Name = "activeShipments", Type = FlexJamType.Int32)]
		public int ActiveShipments { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "maxLevel")]
		[FlexJamMember(Name = "maxLevel", Type = FlexJamType.Int32)]
		public int MaxLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemsInBank")]
		[FlexJamMember(Name = "itemsInBank", Type = FlexJamType.Int32)]
		public int ItemsInBank { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currentLevel")]
		[FlexJamMember(Name = "currentLevel", Type = FlexJamType.Int32)]
		public int CurrentLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemsInLoot")]
		[FlexJamMember(Name = "itemsInLoot", Type = FlexJamType.Int32)]
		public int ItemsInLoot { get; set; }
	}
}
