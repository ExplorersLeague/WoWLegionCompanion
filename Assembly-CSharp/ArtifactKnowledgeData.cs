using System;
using WowJamMessages.MobileClientJSON;

public class ArtifactKnowledgeData
{
	public static void SetArtifactKnowledgeInfo(MobileClientArtifactKnowledgeInfoResult artifactKnowledgeInfo)
	{
		ArtifactKnowledgeData.s_artifactKnowledgeInfo = artifactKnowledgeInfo;
	}

	public static void ClearData()
	{
		ArtifactKnowledgeData.s_artifactKnowledgeInfo = null;
	}

	public static MobileClientArtifactKnowledgeInfoResult s_artifactKnowledgeInfo;
}
