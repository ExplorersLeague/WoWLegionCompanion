using System;
using System.Collections;
using WowJamMessages.MobileClientJSON;

public class PersistentEquipmentData
{
	private static PersistentEquipmentData instance
	{
		get
		{
			if (PersistentEquipmentData.s_instance == null)
			{
				PersistentEquipmentData.s_instance = new PersistentEquipmentData();
				PersistentEquipmentData.s_instance.m_equipmentDictionary = new Hashtable();
			}
			return PersistentEquipmentData.s_instance;
		}
	}

	public static Hashtable equipmentDictionary
	{
		get
		{
			return PersistentEquipmentData.instance.m_equipmentDictionary;
		}
	}

	public static void AddOrUpdateEquipment(MobileFollowerEquipment equipment)
	{
		if (PersistentEquipmentData.instance.m_equipmentDictionary.ContainsKey(equipment.ItemID))
		{
			PersistentEquipmentData.instance.m_equipmentDictionary.Remove(equipment.ItemID);
		}
		PersistentEquipmentData.instance.m_equipmentDictionary.Add(equipment.ItemID, equipment);
	}

	public static void ClearData()
	{
		PersistentEquipmentData.instance.m_equipmentDictionary.Clear();
	}

	private static PersistentEquipmentData s_instance;

	private Hashtable m_equipmentDictionary;
}
