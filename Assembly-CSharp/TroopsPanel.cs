using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;
using WowJamMessages.MobilePlayerJSON;
using WowStaticData;

public class TroopsPanel : MonoBehaviour
{
	private void Awake()
	{
		this.m_noRecruitsYetMessage.font = GeneralHelpers.LoadStandardFont();
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
		instance4.ShipmentItemPushedAction = (Action<int, MobileClientShipmentItem>)Delegate.Combine(instance4.ShipmentItemPushedAction, new Action<int, MobileClientShipmentItem>(this.HandleShipmentItemPushed));
		Main instance5 = Main.instance;
		instance5.OrderHallNavButtonSelectedAction = (Action<OrderHallNavButton>)Delegate.Combine(instance5.OrderHallNavButtonSelectedAction, new Action<OrderHallNavButton>(this.HandleOrderHallNavButtonSelected));
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
		instance4.ShipmentItemPushedAction = (Action<int, MobileClientShipmentItem>)Delegate.Remove(instance4.ShipmentItemPushedAction, new Action<int, MobileClientShipmentItem>(this.HandleShipmentItemPushed));
		Main instance5 = Main.instance;
		instance5.OrderHallNavButtonSelectedAction = (Action<OrderHallNavButton>)Delegate.Remove(instance5.OrderHallNavButtonSelectedAction, new Action<OrderHallNavButton>(this.HandleOrderHallNavButtonSelected));
	}

	public void HandleOrderHallNavButtonSelected(OrderHallNavButton navButton)
	{
		TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
		foreach (TroopsListItem troopsListItem in componentsInChildren)
		{
			troopsListItem.ClearAndHideLootArea();
			troopsListItem.AddInventoryItems();
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
			Object.DestroyImmediate(troopsListItem.gameObject);
		}
	}

	private void HandleShipmentItemPushed(int charShipmentID, MobileClientShipmentItem item)
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
		MobileClientShipmentType[] availableShipmentTypes = PersistentShipmentData.GetAvailableShipmentTypes();
		if (availableShipmentTypes == null || availableShipmentTypes.Length == 0)
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
				foreach (MobileClientShipmentType mobileClientShipmentType in availableShipmentTypes)
				{
					if (troopsListItem.GetCharShipmentTypeID() == mobileClientShipmentType.CharShipmentID)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				Object.DestroyImmediate(troopsListItem.gameObject);
			}
		}
		if (availableShipmentTypes == null)
		{
			return;
		}
		componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
		for (int k = 0; k < availableShipmentTypes.Length; k++)
		{
			bool flag2 = false;
			foreach (TroopsListItem troopsListItem2 in componentsInChildren)
			{
				if (troopsListItem2.GetCharShipmentTypeID() == availableShipmentTypes[k].CharShipmentID)
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
				component.SetCharShipment(availableShipmentTypes[k], false, null);
				FancyEntrance component2 = component.GetComponent<FancyEntrance>();
				component2.m_timeToDelayEntrance = this.m_listItemInitialEntranceDelay + this.m_listItemEntranceDelay * (float)k;
				component2.Activate();
			}
		}
		IEnumerator enumerator = PersistentShipmentData.shipmentDictionary.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				JamCharacterShipment jamCharacterShipment = (JamCharacterShipment)obj;
				if (!PersistentShipmentData.ShipmentTypeForShipmentIsAvailable(jamCharacterShipment.ShipmentRecID))
				{
					bool flag3 = true;
					bool flag4 = false;
					if (jamCharacterShipment.ShipmentRecID < 372 || jamCharacterShipment.ShipmentRecID > 383)
					{
						flag3 = false;
					}
					if (jamCharacterShipment.ShipmentRecID == 178 || jamCharacterShipment.ShipmentRecID == 179 || jamCharacterShipment.ShipmentRecID == 180 || jamCharacterShipment.ShipmentRecID == 192 || jamCharacterShipment.ShipmentRecID == 194 || jamCharacterShipment.ShipmentRecID == 195)
					{
						flag4 = true;
					}
					if (flag3 || flag4)
					{
						CharShipmentRec record = StaticDB.charShipmentDB.GetRecord(jamCharacterShipment.ShipmentRecID);
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
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	private void HandleRecruitResult(int result)
	{
		if (result == 0)
		{
			MobilePlayerRequestShipments obj = new MobilePlayerRequestShipments();
			Login.instance.SendToMobileServer(obj);
		}
	}

	public void PurgeList()
	{
		TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>(true);
		foreach (TroopsListItem troopsListItem in componentsInChildren)
		{
			Object.DestroyImmediate(troopsListItem.gameObject);
		}
	}

	public GameObject m_troopsListItemPrefab;

	public GameObject m_troopsListContents;

	public float m_listItemInitialEntranceDelay;

	public float m_listItemEntranceDelay;

	public Text m_noRecruitsYetMessage;

	public RectTransform m_panelViewRT;

	public GameObject m_resourcesDisplay;
}
