using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class PersistentArmamentData
	{
		private static PersistentArmamentData instance
		{
			get
			{
				if (PersistentArmamentData.s_instance == null)
				{
					PersistentArmamentData.s_instance = new PersistentArmamentData();
				}
				return PersistentArmamentData.s_instance;
			}
		}

		public static IDictionary<int, WrapperFollowerArmamentExt> armamentDictionary
		{
			get
			{
				return PersistentArmamentData.instance.m_armamentDictionary;
			}
		}

		public static void AddOrUpdateArmament(WrapperFollowerArmamentExt armament)
		{
			if (PersistentArmamentData.instance.m_armamentDictionary.ContainsKey(armament.ItemID))
			{
				PersistentArmamentData.instance.m_armamentDictionary.Remove(armament.ItemID);
			}
			PersistentArmamentData.instance.m_armamentDictionary.Add(armament.ItemID, armament);
		}

		public static void ClearData()
		{
			PersistentArmamentData.instance.m_armamentDictionary.Clear();
		}

		private static PersistentArmamentData s_instance;

		private Dictionary<int, WrapperFollowerArmamentExt> m_armamentDictionary = new Dictionary<int, WrapperFollowerArmamentExt>();
	}
}
