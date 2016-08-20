using System;
using System.Collections;
using WowJamMessages.MobileClientJSON;

public class PersistentArmamentData
{
	private static PersistentArmamentData instance
	{
		get
		{
			if (PersistentArmamentData.s_instance == null)
			{
				PersistentArmamentData.s_instance = new PersistentArmamentData();
				PersistentArmamentData.s_instance.m_armamentDictionary = new Hashtable();
			}
			return PersistentArmamentData.s_instance;
		}
	}

	public static Hashtable armamentDictionary
	{
		get
		{
			return PersistentArmamentData.instance.m_armamentDictionary;
		}
	}

	public static void AddOrUpdateArmament(MobileFollowerArmament armament)
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

	private Hashtable m_armamentDictionary;
}
