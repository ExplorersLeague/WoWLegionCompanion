using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class WorldQuestData
	{
		private static WorldQuestData instance
		{
			get
			{
				if (WorldQuestData.s_instance == null)
				{
					WorldQuestData.s_instance = new WorldQuestData();
				}
				return WorldQuestData.s_instance;
			}
		}

		public static IDictionary<int, WrapperWorldQuest> WorldQuestDictionary
		{
			get
			{
				return WorldQuestData.instance.m_worldQuestDictionary;
			}
		}

		public static void AddWorldQuest(WrapperWorldQuest worldQuest)
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

		private Dictionary<int, WrapperWorldQuest> m_worldQuestDictionary = new Dictionary<int, WrapperWorldQuest>();
	}
}
