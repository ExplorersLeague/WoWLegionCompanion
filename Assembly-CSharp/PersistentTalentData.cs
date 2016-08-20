using System;
using System.Collections;
using WowJamMessages;

public class PersistentTalentData
{
	private static PersistentTalentData instance
	{
		get
		{
			if (PersistentTalentData.s_instance == null)
			{
				PersistentTalentData.s_instance = new PersistentTalentData();
				PersistentTalentData.s_instance.m_talentDictionary = new Hashtable();
			}
			return PersistentTalentData.s_instance;
		}
	}

	public static Hashtable talentDictionary
	{
		get
		{
			return PersistentTalentData.instance.m_talentDictionary;
		}
	}

	public static void AddOrUpdateTalent(JamGarrisonTalent talent)
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

	private Hashtable m_talentDictionary;
}
