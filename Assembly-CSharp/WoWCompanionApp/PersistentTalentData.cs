using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class PersistentTalentData
	{
		private static PersistentTalentData instance
		{
			get
			{
				if (PersistentTalentData.s_instance == null)
				{
					PersistentTalentData.s_instance = new PersistentTalentData();
				}
				return PersistentTalentData.s_instance;
			}
		}

		public static IDictionary<int, WrapperGarrisonTalent> talentDictionary
		{
			get
			{
				return PersistentTalentData.instance.m_talentDictionary;
			}
		}

		public static void AddOrUpdateTalent(WrapperGarrisonTalent talent)
		{
			if (PersistentTalentData.instance.m_talentDictionary.ContainsKey(talent.GarrTalentID))
			{
				PersistentTalentData.instance.m_talentDictionary.Remove(talent.GarrTalentID);
			}
			PersistentTalentData.instance.m_talentDictionary.Add(talent.GarrTalentID, talent);
		}

		public static void ClearData()
		{
			PersistentTalentData.instance.m_talentDictionary.Clear();
		}

		private static PersistentTalentData s_instance;

		private Dictionary<int, WrapperGarrisonTalent> m_talentDictionary = new Dictionary<int, WrapperGarrisonTalent>();
	}
}
