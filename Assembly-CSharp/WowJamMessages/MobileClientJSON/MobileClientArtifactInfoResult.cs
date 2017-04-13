using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4882, Name = "MobileClientArtifactInfoResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientArtifactInfoResult
	{
		[FlexJamMember(Name = "knowledgeLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "knowledgeLevel")]
		public int KnowledgeLevel { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "xpMultiplier")]
		[FlexJamMember(Name = "xpMultiplier", Type = FlexJamType.Float)]
		public float XpMultiplier { get; set; }
	}
}
