using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class TroopsPanel : MonoBehaviour
	{
		private void Awake()
		{
			this.m_gamePanel = base.GetComponentInParent<GamePanel>();
			this.m_noRecruitsYetMessage.font = FontLoader.LoadStandardFont();
			this.m_noRecruitsYetMessage.text = StaticDB.GetString("NO_RECRUITS_AVAILABLE_YET", "You have no recruits available yet.");
			this.InitList();
		}

		private void Start()
		{
		}

		public void OnEnable()
		{
			Main instance = Main.instance;
			instance.CreateShipmentResultAction = (Action<int>)Delegate.Combine(instance.CreateShipmentResultAction, new Action<int>(this.HandleRecruitResult));
			Main instance2 = Main.instance;
			instance2.FollowerDataChangedAction = (Action)Delegate.Combine(instance2.FollowerDataChangedAction, new Action(this.HandleFollowerDataChanged));
			Main instance3 = Main.instance;
			instance3.ShipmentTypesUpdatedAction = (Action)Delegate.Combine(instance3.ShipmentTypesUpdatedAction, new Action(this.InitList));
			Main instance4 = Main.instance;
			instance4.ShipmentItemPushedAction = (Action<int, WrapperShipmentItem>)Delegate.Combine(instance4.ShipmentItemPushedAction, new Action<int, WrapperShipmentItem>(this.HandleShipmentItemPushed));
			GamePanel gamePanel = this.m_gamePanel;
			gamePanel.OrderHallNavButtonSelectedAction = (Action<OrderHallNavButton>)Delegate.Combine(gamePanel.OrderHallNavButtonSelectedAction, new Action<OrderHallNavButton>(this.HandleOrderHallNavButtonSelected));
			this.InitList();
		}

		private void OnDisable()
		{
			Main instance = Main.instance;
			instance.CreateShipmentResultAction = (Action<int>)Delegate.Remove(instance.CreateShipmentResultAction, new Action<int>(this.HandleRecruitResult));
			Main instance2 = Main.instance;
			instance2.FollowerDataChangedAction = (Action)Delegate.Remove(instance2.FollowerDataChangedAction, new Action(this.HandleFollowerDataChanged));
			Main instance3 = Main.instance;
			instance3.ShipmentTypesUpdatedAction = (Action)Delegate.Remove(instance3.ShipmentTypesUpdatedAction, new Action(this.InitList));
			Main instance4 = Main.instance;
			instance4.ShipmentItemPushedAction = (Action<int, WrapperShipmentItem>)Delegate.Remove(instance4.ShipmentItemPushedAction, new Action<int, WrapperShipmentItem>(this.HandleShipmentItemPushed));
			GamePanel gamePanel = this.m_gamePanel;
			gamePanel.OrderHallNavButtonSelectedAction = (Action<OrderHallNavButton>)Delegate.Remove(gamePanel.OrderHallNavButtonSelectedAction, new Action<OrderHallNavButton>(this.HandleOrderHallNavButtonSelected));
		}

		public void HandleOrderHallNavButtonSelected(OrderHallNavButton navButton)
		{
			TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			foreach (TroopsListItem troopsListItem in componentsInChildren)
			{
				troopsListItem.ClearAndHideLootArea();
			}
		}

		private void Update()
		{
		}

		private void HandleFollowerDataChanged()
		{
			this.InitList();
			TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			foreach (TroopsListItem troopsListItem in componentsInChildren)
			{
				troopsListItem.HandleFollowerDataChanged();
			}
		}

		private void HandleEnteredWorld()
		{
			TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			foreach (TroopsListItem troopsListItem in componentsInChildren)
			{
				Object.Destroy(troopsListItem.gameObject);
			}
		}

		private void HandleShipmentItemPushed(int charShipmentID, WrapperShipmentItem item)
		{
			TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			foreach (TroopsListItem troopsListItem in componentsInChildren)
			{
				if (troopsListItem.GetCharShipmentTypeID() == charShipmentID)
				{
					troopsListItem.HandleShipmentItemPushed(item);
				}
			}
		}

		private void InitList()
		{
			List<WrapperShipmentType> availableShipmentTypes = PersistentShipmentData.GetAvailableShipmentTypes();
			if (availableShipmentTypes == null || availableShipmentTypes.Count == 0)
			{
				this.m_noRecruitsYetMessage.gameObject.SetActive(true);
			}
			else
			{
				this.m_noRecruitsYetMessage.gameObject.SetActive(false);
			}
			TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			foreach (TroopsListItem troopsListItem in componentsInChildren)
			{
				bool flag = true;
				if (availableShipmentTypes != null)
				{
					foreach (WrapperShipmentType wrapperShipmentType in availableShipmentTypes)
					{
						if (troopsListItem.GetCharShipmentTypeID() == wrapperShipmentType.CharShipmentID)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					Object.Destroy(troopsListItem.gameObject);
				}
			}
			if (availableShipmentTypes == null)
			{
				return;
			}
			componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			for (int j = 0; j < availableShipmentTypes.Count; j++)
			{
				bool flag2 = false;
				foreach (TroopsListItem troopsListItem2 in componentsInChildren)
				{
					if (troopsListItem2.GetCharShipmentTypeID() == availableShipmentTypes[j].CharShipmentID)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopsListItemPrefab);
					gameObject.transform.SetParent(this.m_troopsListContents.transform, false);
					TroopsListItem component = gameObject.GetComponent<TroopsListItem>();
					component.SetCharShipment(new WrapperShipmentType?(availableShipmentTypes[j]), false, null);
					FancyEntrance component2 = component.GetComponent<FancyEntrance>();
					component2.m_timeToDelayEntrance = this.m_listItemInitialEntranceDelay + this.m_listItemEntranceDelay * (float)j;
					component2.Activate();
				}
			}
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				if (!PersistentShipmentData.ShipmentTypeForShipmentIsAvailable(wrapperCharacterShipment.ShipmentRecID))
				{
					bool flag3 = true;
					bool flag4 = false;
					if (wrapperCharacterShipment.ShipmentRecID < 372 || wrapperCharacterShipment.ShipmentRecID > 383)
					{
						flag3 = false;
					}
					if (wrapperCharacterShipment.ShipmentRecID == 178 || wrapperCharacterShipment.ShipmentRecID == 179 || wrapperCharacterShipment.ShipmentRecID == 180 || wrapperCharacterShipment.ShipmentRecID == 192 || wrapperCharacterShipment.ShipmentRecID == 194 || wrapperCharacterShipment.ShipmentRecID == 195)
					{
						flag4 = true;
					}
					if (flag3 || flag4)
					{
						CharShipmentRec record = StaticDB.charShipmentDB.GetRecord(wrapperCharacterShipment.ShipmentRecID);
						if (record != null)
						{
							GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_troopsListItemPrefab);
							gameObject2.transform.SetParent(this.m_troopsListContents.transform, false);
							TroopsListItem component3 = gameObject2.GetComponent<TroopsListItem>();
							component3.SetCharShipment(null, true, record);
						}
					}
				}
			}
		}

		private void HandleRecruitResult(int result)
		{
			if (result == 0)
			{
				LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
			}
		}

		public void PurgeList()
		{
			TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
			foreach (TroopsListItem troopsListItem in componentsInChildren)
			{
				Object.Destroy(troopsListItem.gameObject);
			}
		}

		public GameObject m_troopsListItemPrefab;

		public GameObject m_troopsListContents;

		public float m_listItemInitialEntranceDelay;

		public float m_listItemEntranceDelay;

		public Text m_noRecruitsYetMessage;

		public RectTransform m_panelViewRT;

		public GameObject m_resourcesDisplay;

		private GamePanel m_gamePanel;
	}
}
