using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4868, Name = "MobileClientArtifactInfoResult", Version = 28333852u)]
	public class MobileClientArtifactInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "knowledgeLevel")]
		[FlexJamMember(Name = "knowledgeLevel", Type = FlexJamType.Int32)]
		public int KnowledgeLevel { get; set; }

		[FlexJamMember(Name = "xpMultiplier", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "xpMultiplier")]
		public float XpMultiplier { get; set; }
	}
}
