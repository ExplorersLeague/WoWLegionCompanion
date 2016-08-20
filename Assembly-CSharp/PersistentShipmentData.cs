using System;
using System.Collections;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;

public class PersistentShipmentData
{
	private static PersistentShipmentData instance
	{
		get
		{
			if (PersistentShipmentData.s_instance == null)
			{
				PersistentShipmentData.s_instance = new PersistentShipmentData();
				PersistentShipmentData.s_instance.m_shipmentDictionary = new Hashtable();
			}
			return PersistentShipmentData.s_instance;
		}
	}

	public static Hashtable shipmentDictionary
	{
		get
		{
			return PersistentShipmentData.instance.m_shipmentDictionary;
		}
	}

	public static void AddOrUpdateShipment(JamCharacterShipment shipment)
	{
		if (PersistentShipmentData.instance.m_shipmentDictionary.ContainsKey(shipment.ShipmentID))
		{
			PersistentShipmentData.instance.m_shipmentDictionary.Remove(shipment.ShipmentID);
		}
		PersistentShipmentData.instance.m_shipmentDictionary.Add(shipment.ShipmentID, shipment);
	}

	public static void SetAvailableShipmentTypes(MobileClientShipmentType[] availableShipmentTypes)
	{
		PersistentShipmentData.instance.m_availableShipmentTypes = availableShipmentTypes;
	}

	public static MobileClientShipmentType[] GetAvailableShipmentTypes()
	{
		return PersistentShipmentData.instance.m_availableShipmentTypes;
	}

	public static void ClearData()
	{
		PersistentShipmentData.instance.m_shipmentDictionary.Clear();
	}

	private static PersistentShipmentData s_instance;

	private Hashtable m_shipmentDictionary;

	private MobileClientShipmentType[] m_availableShipmentTypes;
}
