using System;
using System.Collections.Generic;
using System.Linq;

namespace WoWCompanionApp
{
	public class PersistentShipmentData
	{
		private static PersistentShipmentData instance
		{
			get
			{
				if (PersistentShipmentData.s_instance == null)
				{
					PersistentShipmentData.s_instance = new PersistentShipmentData();
				}
				return PersistentShipmentData.s_instance;
			}
		}

		public static IDictionary<ulong, WrapperCharacterShipment> shipmentDictionary
		{
			get
			{
				return PersistentShipmentData.instance.m_shipmentDictionary;
			}
		}

		public static void AddOrUpdateShipment(WrapperCharacterShipment shipment)
		{
			if (PersistentShipmentData.instance.m_shipmentDictionary.ContainsKey(shipment.ShipmentID))
			{
				PersistentShipmentData.instance.m_shipmentDictionary.Remove(shipment.ShipmentID);
			}
			PersistentShipmentData.instance.m_shipmentDictionary.Add(shipment.ShipmentID, shipment);
		}

		public static void SetAvailableShipmentTypes(IEnumerable<WrapperShipmentType> availableShipmentTypes)
		{
			PersistentShipmentData.instance.m_availableShipmentTypes = availableShipmentTypes.ToList<WrapperShipmentType>();
		}

		public static List<WrapperShipmentType> GetAvailableShipmentTypes()
		{
			return PersistentShipmentData.instance.m_availableShipmentTypes;
		}

		public static bool ShipmentTypeForShipmentIsAvailable(int charShipmentID)
		{
			foreach (WrapperShipmentType wrapperShipmentType in PersistentShipmentData.instance.m_availableShipmentTypes)
			{
				if (wrapperShipmentType.CharShipmentID == charShipmentID)
				{
					return true;
				}
			}
			return false;
		}

		public static void ClearData()
		{
			PersistentShipmentData.instance.m_shipmentDictionary.Clear();
		}

		public static int GetNumReadyShipments()
		{
			int num = 0;
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				if (PersistentShipmentData.ShipmentTypeForShipmentIsAvailable(wrapperCharacterShipment.ShipmentRecID))
				{
					TimeSpan t = GarrisonStatus.CurrentTime() - wrapperCharacterShipment.CreationTime;
					if ((wrapperCharacterShipment.ShipmentDuration - t).TotalSeconds <= 0.0)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static bool CanOrderShipmentType(int charShipmentID)
		{
			foreach (WrapperShipmentType wrapperShipmentType in PersistentShipmentData.instance.m_availableShipmentTypes)
			{
				if (wrapperShipmentType.CharShipmentID == charShipmentID)
				{
					return wrapperShipmentType.CanOrder;
				}
			}
			return false;
		}

		public static bool CanPickupShipmentType(int charShipmentID)
		{
			foreach (WrapperShipmentType wrapperShipmentType in PersistentShipmentData.instance.m_availableShipmentTypes)
			{
				if (wrapperShipmentType.CharShipmentID == charShipmentID)
				{
					return wrapperShipmentType.CanPickup;
				}
			}
			return false;
		}

		private static PersistentShipmentData s_instance;

		private Dictionary<ulong, WrapperCharacterShipment> m_shipmentDictionary = new Dictionary<ulong, WrapperCharacterShipment>();

		private List<WrapperShipmentType> m_availableShipmentTypes;
	}
}
