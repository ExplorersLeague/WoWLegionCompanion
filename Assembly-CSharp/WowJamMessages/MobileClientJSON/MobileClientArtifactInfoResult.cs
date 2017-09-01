using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4882, Name = "MobileClientArtifactInfoResult", Version = 39869590u)]
	public class MobileClientArtifactInfoResult
	{
		[FlexJamMember(Name = "knowledgeLevel", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "knowledgeLevel")]
		public int KnowledgeLevel { get; set; }

		[FlexJamMember(Name = "xpMultiplier", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "xpMultiplier")]
		public float XpMultiplier { get; set; }
	}
}
