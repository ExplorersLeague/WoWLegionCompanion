using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowJamMessages.MobilePlayerJSON;

public class TroopsPanel : MonoBehaviour
{
	private void Awake()
	{
		this.m_noRecruitsYetMessage.font = GeneralHelpers.LoadStandardFont();
		this.m_noRecruitsYetMessage.text = StaticDB.GetString("NO_RECRUITS_AVAILABLE_YET", "You have no recruits available yet.");
		this.InitList();
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
		instance4.StartLogOutAction = (Action)Delegate.Combine(instance4.StartLogOutAction, new Action(this.HandleStartLogout));
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
		instance4.StartLogOutAction = (Action)Delegate.Remove(instance4.StartLogOutAction, new Action(this.HandleStartLogout));
	}

	private void Update()
	{
		if (this.m_panelViewRT.sizeDelta.x != this.m_parentViewRT.rect.width)
		{
			this.m_multiPanelViewSizeDelta = this.m_panelViewRT.sizeDelta;
			this.m_multiPanelViewSizeDelta.x = this.m_parentViewRT.rect.width;
			this.m_panelViewRT.sizeDelta = this.m_multiPanelViewSizeDelta;
		}
	}

	private void HandleFollowerDataChanged()
	{
		this.InitList();
		TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>();
		foreach (TroopsListItem troopsListItem in componentsInChildren)
		{
			troopsListItem.HandleFollowerDataChanged();
		}
	}

	private void HandleStartLogout()
	{
		TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>();
		foreach (TroopsListItem troopsListItem in componentsInChildren)
		{
			Object.DestroyImmediate(troopsListItem.gameObject);
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
		TroopsListItem[] componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>();
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
		componentsInChildren = this.m_troopsListContents.GetComponentsInChildren<TroopsListItem>();
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
				component.SetCharShipment(availableShipmentTypes[k]);
				FancyEntrance component2 = component.GetComponent<FancyEntrance>();
				component2.m_timeToDelayEntrance = this.m_listItemInitialEntranceDelay + this.m_listItemEntranceDelay * (float)k;
				component2.Activate();
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

	public GameObject m_troopsListItemPrefab;

	public GameObject m_troopsListContents;

	public float m_listItemInitialEntranceDelay;

	public float m_listItemEntranceDelay;

	public Text m_noRecruitsYetMessage;

	public RectTransform m_panelViewRT;

	public RectTransform m_parentViewRT;

	private Vector2 m_multiPanelViewSizeDelta;
}
