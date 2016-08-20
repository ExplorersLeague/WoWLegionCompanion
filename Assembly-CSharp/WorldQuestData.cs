using System;
using System.Collections;
using WowJamMessages.MobileClientJSON;

public class WorldQuestData
{
	private static WorldQuestData instance
	{
		get
		{
			if (WorldQuestData.s_instance == null)
			{
				WorldQuestData.s_instance = new WorldQuestData();
				WorldQuestData.s_instance.m_worldQuestDictionary = new Hashtable();
			}
			return WorldQuestData.s_instance;
		}
	}

	public static Hashtable worldQuestDictionary
	{
		get
		{
			return WorldQuestData.instance.m_worldQuestDictionary;
		}
	}

	public static void AddWorldQuest(MobileWorldQuest worldQuest)
	{
		if (!WorldQuestData.instance.m_worldQuestDictionary.ContainsKey(worldQuest.QuestID))
		{
			WorldQuestData.instance.m_worldQuestDictionary.Add(worldQuest.QuestID, worldQuest);
		}
	}

	public static void ClearData()
	{
		WorldQuestData.instance.m_worldQuestDictionary.Clear();
	}

	private static WorldQuestData s_instance;

	private Hashtable m_worldQuestDictionary;
}
