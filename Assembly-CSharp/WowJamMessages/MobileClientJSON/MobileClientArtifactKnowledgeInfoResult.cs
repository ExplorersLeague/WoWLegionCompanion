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

		[FlexJamMember(Name = "itemsInMail", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemsInMail")]
		public int ItemsInMail { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "activeShipments")]
		[FlexJamMember(Name = "activeShipments", Type = FlexJamType.Int32)]
		public int ActiveShipments { get; set; }

		[FlexJamMember(Name = "maxLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "maxLevel")]
		public int MaxLevel { get; set; }

		[FlexJamMember(Name = "itemsInBank", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemsInBank")]
		public int ItemsInBank { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "currentLevel")]
		[FlexJamMember(Name = "currentLevel", Type = FlexJamType.Int32)]
		public int CurrentLevel { get; set; }

		[FlexJamMember(Name = "itemsInLoot", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemsInLoot")]
		public int ItemsInLoot { get; set; }
	}
}
