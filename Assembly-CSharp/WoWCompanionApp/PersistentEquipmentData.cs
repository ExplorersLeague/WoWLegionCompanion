using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class PersistentEquipmentData
	{
		private static PersistentEquipmentData instance
		{
			get
			{
				if (PersistentEquipmentData.s_instance == null)
				{
					PersistentEquipmentData.s_instance = new PersistentEquipmentData();
				}
				return PersistentEquipmentData.s_instance;
			}
		}

		public static IDictionary<int, WrapperFollowerEquipment> equipmentDictionary
		{
			get
			{
				return PersistentEquipmentData.instance.m_equipmentDictionary;
			}
		}

		public static void AddOrUpdateEquipment(WrapperFollowerEquipment equipment)
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

		private Dictionary<int, WrapperFollowerEquipment> m_equipmentDictionary = new Dictionary<int, WrapperFollowerEquipment>();
	}
}
