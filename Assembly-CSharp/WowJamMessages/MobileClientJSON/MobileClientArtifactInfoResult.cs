using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4882, Name = "MobileClientArtifactInfoResult", Version = 39869590u)]
	public class MobileClientArtifactInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "knowledgeLevel")]
		[FlexJamMember(Name = "knowledgeLevel", Type = FlexJamType.Int32)]
		public int KnowledgeLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "xpMultiplier")]
		[FlexJamMember(Name = "xpMultiplier", Type = FlexJamType.Float)]
		public float XpMultiplier { get; set; }
	}
}
