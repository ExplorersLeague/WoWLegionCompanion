using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class MissionDataCache
	{
		private static MissionDataCache instance
		{
			get
			{
				if (MissionDataCache.s_instance == null)
				{
					MissionDataCache.s_instance = new MissionDataCache();
				}
				return MissionDataCache.s_instance;
			}
		}

		public static IDictionary<int, int> missionDataDictionary
		{
			get
			{
				return MissionDataCache.instance.m_missionDataDictionary;
			}
		}

		public static void AddOrUpdateMissionData(int garrMissionID, int successChance)
		{
			if (!MissionDataCache.instance.m_missionDataDictionary.ContainsKey(garrMissionID))
			{
				MissionDataCache.instance.m_missionDataDictionary.Add(garrMissionID, successChance);
			}
			else
			{
				MissionDataCache.instance.m_missionDataDictionary.Remove(garrMissionID);
				MissionDataCache.instance.m_missionDataDictionary.Add(garrMissionID, successChance);
			}
		}

		public static void ClearData()
		{
			MissionDataCache.instance.m_missionDataDictionary.Clear();
		}

		private static MissionDataCache s_instance;

		private Dictionary<int, int> m_missionDataDictionary = new Dictionary<int, int>();
	}
}
