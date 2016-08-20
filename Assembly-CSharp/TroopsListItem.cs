using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;
using WowJamMessages.MobilePlayerJSON;
using WowStatConstants;
using WowStaticData;

public class TroopsListItem : MonoBehaviour
{
	public void HandleFollowerDataChanged()
	{
		if (this.m_shipmentType == null)
		{
			return;
		}
		if (this.m_isTroop)
		{
			this.UpdateTroopSlots();
		}
		else
		{
			this.UpdateItemSlots();
		}
		this.UpdateRecruitButtonState();
	}

	private void UpdateRecruitButtonState()
	{
		TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>();
		bool flag = false;
		foreach (TroopSlot troopSlot in componentsInChildren)
		{
			if (troopSlot.IsEmpty())
			{
				flag = true;
				break;
			}
		}
		bool flag2 = GarrisonStatus.Resources() >= this.m_shipmentCost;
		this.m_itemResourceCostText.color = ((!flag2) ? Color.red : Color.white);
		this.m_recruitButtonText.color = new Color(1f, 0.82f, 0f, 1f);
		if (!flag)
		{
			this.m_recruitButtonText.text = StaticDB.GetString("SLOTS_FULL", null);
			this.m_recruitButtonText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		}
		else if (!flag2)
		{
			this.m_recruitButtonText.text = StaticDB.GetString("CANT_AFFORD", "Can't Afford");
			this.m_recruitButtonText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		}
		else if (this.m_isTroop)
		{
			this.m_recruitButtonText.text = StaticDB.GetString("RECRUIT", null);
		}
		else
		{
			this.m_recruitButtonText.text = StaticDB.GetString("PLACE_ORDER", null);
		}
		if (flag && flag2)
		{
			this.m_recruitTroopsButton.interactable = true;
		}
		else
		{
			this.m_recruitTroopsButton.interactable = false;
		}
	}

	private void Start()
	{
		Main instance = Main.instance;
		instance.ShipmentAddedAction = (Action<int, ulong>)Delegate.Combine(instance.ShipmentAddedAction, new Action<int, ulong>(this.HandleShipmentAdded));
		this.m_troopName.font = GeneralHelpers.LoadStandardFont();
		this.m_troopResourceCostText.font = GeneralHelpers.LoadStandardFont();
		this.m_itemResourceCostText.font = GeneralHelpers.LoadStandardFont();
		this.m_recruitButtonText.font = GeneralHelpers.LoadStandardFont();
		this.m_itemName.font = GeneralHelpers.LoadStandardFont();
	}

	private void OnDestroy()
	{
		Main instance = Main.instance;
		instance.ShipmentAddedAction = (Action<int, ulong>)Delegate.Remove(instance.ShipmentAddedAction, new Action<int, ulong>(this.HandleShipmentAdded));
	}

	public void SetCharShipment(MobileClientShipmentType shipmentType)
	{
		this.m_shipmentCost = shipmentType.CurrencyCost;
		this.m_shipmentType = shipmentType;
		Transform[] componentsInChildren = this.m_troopHeartContainer.GetComponentsInChildren<Transform>();
		foreach (Transform transform in componentsInChildren)
		{
			if (transform != this.m_troopHeartContainer.transform)
			{
				Object.DestroyImmediate(transform.gameObject);
			}
		}
		AbilityDisplay[] componentsInChildren2 = this.m_traitsAndAbilitiesRootObject.GetComponentsInChildren<AbilityDisplay>();
		foreach (AbilityDisplay abilityDisplay in componentsInChildren2)
		{
			Object.DestroyImmediate(abilityDisplay.gameObject);
		}
		TroopSlot[] componentsInChildren3 = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>();
		foreach (TroopSlot troopSlot in componentsInChildren3)
		{
			Object.DestroyImmediate(troopSlot.gameObject);
		}
		CharShipmentRec record = StaticDB.charShipmentDB.GetRecord(shipmentType.CharShipmentID);
		if (record == null)
		{
			Debug.LogError("Invalid Shipment ID: " + shipmentType.CharShipmentID);
			this.m_troopName.text = "Invalid Shipment ID: " + shipmentType.CharShipmentID;
			return;
		}
		if (record.GarrFollowerID > 0u)
		{
			this.SetCharShipmentTroop(shipmentType, record);
		}
		else if (record.DummyItemID > 0)
		{
			this.SetCharShipmentItem(shipmentType, record);
		}
	}

	private void UpdateItemSlots()
	{
		int maxShipments = (int)this.m_charShipmentRec.MaxShipments;
		TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
		if (componentsInChildren.Length < maxShipments)
		{
			for (int i = componentsInChildren.Length; i < maxShipments; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopSlotPrefab);
				gameObject.transform.SetParent(this.m_troopSlotsRootObject.transform, false);
				TroopSlot component = gameObject.GetComponent<TroopSlot>();
				component.SetCharShipment(this.m_shipmentType.CharShipmentID, 0UL, 0, false, 0);
			}
		}
		if (componentsInChildren.Length > maxShipments)
		{
			for (int j = maxShipments; j < componentsInChildren.Length; j++)
			{
				Object.DestroyImmediate(componentsInChildren[j].gameObject);
			}
		}
		componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
		foreach (TroopSlot troopSlot in componentsInChildren)
		{
			if (troopSlot.GetDBID() != 0UL && !PersistentShipmentData.shipmentDictionary.ContainsKey(troopSlot.GetDBID()))
			{
				troopSlot.SetCharShipment(this.m_shipmentType.CharShipmentID, 0UL, 0, false, 0);
			}
		}
		foreach (object obj in PersistentShipmentData.shipmentDictionary.Values)
		{
			JamCharacterShipment jamCharacterShipment = (JamCharacterShipment)obj;
			if (jamCharacterShipment.ShipmentRecID == this.m_shipmentType.CharShipmentID)
			{
				this.SetTroopSlotForPendingShipment(componentsInChildren, jamCharacterShipment.ShipmentID);
			}
		}
	}

	private void SetCharShipmentItem(MobileClientShipmentType shipmentType, CharShipmentRec charShipmentRec)
	{
		this.m_isTroop = false;
		this.m_charShipmentRec = charShipmentRec;
		this.m_troopSpecificArea.SetActive(false);
		this.m_itemSpecificArea.SetActive(true);
		this.m_troopName.gameObject.SetActive(false);
		this.m_itemName.gameObject.SetActive(true);
		ItemRec record = StaticDB.itemDB.GetRecord(charShipmentRec.DummyItemID);
		if (record == null)
		{
			Debug.LogError("Invalid Item ID: " + charShipmentRec.DummyItemID);
			this.m_troopName.text = "Invalid Item ID: " + charShipmentRec.DummyItemID;
			return;
		}
		this.m_itemDisplay.InitReward(MissionRewardDisplay.RewardType.item, charShipmentRec.DummyItemID, 1, 0, record.IconFileDataID);
		this.m_itemName.text = record.Display;
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
		if (sprite != null)
		{
			this.m_troopSnapshotImage.sprite = sprite;
		}
		this.m_itemResourceCostText.text = string.Empty + shipmentType.CurrencyCost;
		Sprite sprite2 = GeneralHelpers.LoadCurrencyIcon(shipmentType.CurrencyTypeID);
		if (sprite2 != null)
		{
			this.m_itemResourceIcon.sprite = sprite2;
		}
		this.UpdateItemSlots();
		this.UpdateRecruitButtonState();
	}

	private void SetTroopSlotForExistingFollower(TroopSlot[] troopSlots, JamGarrisonFollower follower)
	{
		if (follower.Durability <= 0)
		{
			return;
		}
		foreach (TroopSlot troopSlot in troopSlots)
		{
			int ownedFollowerID = troopSlot.GetOwnedFollowerID();
			if (ownedFollowerID != 0 && ownedFollowerID == follower.GarrFollowerID)
			{
				return;
			}
		}
		foreach (TroopSlot troopSlot2 in troopSlots)
		{
			if (troopSlot2.IsCollected())
			{
				GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
				int iconFileDataID = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceIconFileDataID : record.HordeIconFileDataID;
				troopSlot2.SetCharShipment(this.m_shipmentType.CharShipmentID, 0UL, follower.GarrFollowerID, false, iconFileDataID);
				return;
			}
		}
		foreach (TroopSlot troopSlot3 in troopSlots)
		{
			if (troopSlot3.IsEmpty())
			{
				GarrFollowerRec record2 = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
				int iconFileDataID2 = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record2.AllianceIconFileDataID : record2.HordeIconFileDataID;
				troopSlot3.SetCharShipment(this.m_shipmentType.CharShipmentID, 0UL, follower.GarrFollowerID, false, iconFileDataID2);
				return;
			}
		}
	}

	private void SetTroopSlotForPendingShipment(TroopSlot[] troopSlots, ulong shipmentDBID)
	{
		foreach (TroopSlot troopSlot in troopSlots)
		{
			if (troopSlot.GetDBID() == shipmentDBID)
			{
				return;
			}
		}
		foreach (TroopSlot troopSlot2 in troopSlots)
		{
			if (troopSlot2.IsEmpty())
			{
				troopSlot2.SetCharShipment(this.m_shipmentType.CharShipmentID, shipmentDBID, 0, true, 0);
				return;
			}
		}
	}

	private void UpdateTroopSlots()
	{
		int maxTroops = this.GetMaxTroops((int)((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? this.m_followerRec.AllianceGarrClassSpecID : this.m_followerRec.HordeGarrClassSpecID));
		TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
		if (componentsInChildren.Length < maxTroops)
		{
			for (int i = componentsInChildren.Length; i < maxTroops; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopSlotPrefab);
				gameObject.transform.SetParent(this.m_troopSlotsRootObject.transform, false);
				TroopSlot component = gameObject.GetComponent<TroopSlot>();
				component.SetCharShipment(this.m_shipmentType.CharShipmentID, 0UL, 0, false, 0);
			}
		}
		if (componentsInChildren.Length > maxTroops)
		{
			for (int j = maxTroops; j < componentsInChildren.Length; j++)
			{
				Object.DestroyImmediate(componentsInChildren[j].gameObject);
			}
		}
		componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
		foreach (TroopSlot troopSlot in componentsInChildren)
		{
			int ownedFollowerID = troopSlot.GetOwnedFollowerID();
			if (ownedFollowerID != 0 && (!PersistentFollowerData.followerDictionary.ContainsKey(ownedFollowerID) || PersistentFollowerData.followerDictionary[ownedFollowerID].Durability == 0))
			{
				troopSlot.SetCharShipment(this.m_shipmentType.CharShipmentID, 0UL, 0, false, 0);
			}
		}
		uint num = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? this.m_followerRec.AllianceGarrClassSpecID : this.m_followerRec.HordeGarrClassSpecID;
		foreach (JamGarrisonFollower jamGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
		{
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(jamGarrisonFollower.GarrFollowerID);
			uint num2 = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceGarrClassSpecID : record.HordeGarrClassSpecID;
			if (num2 == num && jamGarrisonFollower.Durability > 0)
			{
				this.SetTroopSlotForExistingFollower(componentsInChildren, jamGarrisonFollower);
			}
		}
		CharShipmentRec record2 = StaticDB.charShipmentDB.GetRecord(this.m_shipmentType.CharShipmentID);
		foreach (object obj in PersistentShipmentData.shipmentDictionary.Values)
		{
			JamCharacterShipment jamCharacterShipment = (JamCharacterShipment)obj;
			if (jamCharacterShipment.ShipmentRecID == this.m_shipmentType.CharShipmentID)
			{
				this.SetTroopSlotForPendingShipment(componentsInChildren, jamCharacterShipment.ShipmentID);
			}
			else
			{
				CharShipmentRec record3 = StaticDB.charShipmentDB.GetRecord(jamCharacterShipment.ShipmentRecID);
				if (record3.ContainerID == record2.ContainerID)
				{
					this.SetTroopSlotForPendingShipment(componentsInChildren, jamCharacterShipment.ShipmentID);
				}
			}
		}
	}

	private void SetCharShipmentTroop(MobileClientShipmentType shipmentType, CharShipmentRec charShipmentRec)
	{
		this.m_isTroop = true;
		this.m_shipmentType = shipmentType;
		this.m_troopSpecificArea.SetActive(true);
		this.m_itemSpecificArea.SetActive(false);
		this.m_troopName.gameObject.SetActive(true);
		this.m_itemName.gameObject.SetActive(false);
		GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord((int)charShipmentRec.GarrFollowerID);
		if (record == null)
		{
			Debug.LogError("Invalid Follower ID: " + charShipmentRec.GarrFollowerID);
			this.m_troopName.text = "Invalid Follower ID: " + charShipmentRec.GarrFollowerID;
			return;
		}
		this.m_followerRec = record;
		int num = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceCreatureID : record.HordeCreatureID;
		CreatureRec record2 = StaticDB.creatureDB.GetRecord(num);
		if (record2 == null)
		{
			Debug.LogError("Invalid Creature ID: " + num);
			this.m_troopName.text = "Invalid Creature ID: " + num;
			return;
		}
		string text = "Assets/BundleAssets/PortraitIcons/cid_" + record2.ID.ToString("D8") + ".png";
		Sprite sprite = AssetBundleManager.portraitIcons.LoadAsset<Sprite>(text);
		if (sprite != null)
		{
			this.m_troopSnapshotImage.sprite = sprite;
		}
		for (int i = 0; i < record.Vitality; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopHeartPrefab);
			gameObject.transform.SetParent(this.m_troopHeartContainer.transform, false);
		}
		this.m_troopName.text = record2.Name;
		StaticDB.garrFollowerXAbilityDB.EnumRecordsByParentID((int)charShipmentRec.GarrFollowerID, delegate(GarrFollowerXAbilityRec xAbilityRec)
		{
			if (xAbilityRec.FactionIndex == (int)GarrisonStatus.Faction())
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_abilityDisplayPrefab);
				gameObject2.transform.SetParent(this.m_traitsAndAbilitiesRootObject.transform, false);
				AbilityDisplay component = gameObject2.GetComponent<AbilityDisplay>();
				component.SetAbility(xAbilityRec.GarrAbilityID, true, true, null);
			}
			return true;
		});
		this.UpdateTroopSlots();
		this.m_troopResourceCostText.text = string.Empty + shipmentType.CurrencyCost;
		Sprite sprite2 = GeneralHelpers.LoadCurrencyIcon(shipmentType.CurrencyTypeID);
		if (sprite2 != null)
		{
			this.m_troopResourceIcon.sprite = sprite2;
		}
		this.UpdateRecruitButtonState();
	}

	public void Recruit()
	{
		Debug.Log("Attempting To Recruit! CharShipment ID = " + this.m_shipmentType.CharShipmentID);
		MobilePlayerCreateShipment mobilePlayerCreateShipment = new MobilePlayerCreateShipment();
		mobilePlayerCreateShipment.CharShipmentID = this.m_shipmentType.CharShipmentID;
		mobilePlayerCreateShipment.NumShipments = 1;
		Login.instance.SendToMobileServer(mobilePlayerCreateShipment);
		Main.instance.m_UISound.Play_RecruitTroop();
	}

	private void HandleShipmentAdded(int charShipmentID, ulong shipmentDBID)
	{
		if (charShipmentID == this.m_shipmentType.CharShipmentID)
		{
			TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>();
			foreach (TroopSlot troopSlot in componentsInChildren)
			{
				if (troopSlot.GetDBID() == shipmentDBID)
				{
					return;
				}
			}
			foreach (TroopSlot troopSlot2 in componentsInChildren)
			{
				if (troopSlot2.IsEmpty())
				{
					troopSlot2.SetCharShipment(charShipmentID, shipmentDBID, 0, true, 0);
					break;
				}
			}
			this.UpdateRecruitButtonState();
		}
	}

	private int GetMaxTroops(int garrClassSpecID)
	{
		GarrClassSpecRec record = StaticDB.garrClassSpecDB.GetRecord(garrClassSpecID);
		int maxTroops = 0;
		if (record != null)
		{
			maxTroops = (int)record.FollowerClassLimit;
		}
		foreach (object obj in PersistentTalentData.talentDictionary.Values)
		{
			JamGarrisonTalent jamGarrisonTalent = (JamGarrisonTalent)obj;
			if ((jamGarrisonTalent.Flags & 1) != 0)
			{
				GarrTalentRec record2 = StaticDB.garrTalentDB.GetRecord(jamGarrisonTalent.GarrTalentID);
				if (record2 != null)
				{
					StaticDB.garrAbilityEffectDB.EnumRecordsByParentID((int)record2.GarrAbilityID, delegate(GarrAbilityEffectRec effectRec)
					{
						if (effectRec.AbilityAction == 34u && (ulong)effectRec.ActionRecordID == (ulong)((long)garrClassSpecID))
						{
							maxTroops += (int)effectRec.ActionValueFlat;
						}
						return true;
					});
				}
			}
		}
		return maxTroops;
	}

	public void PlayClickSound()
	{
		Main.instance.m_UISound.Play_ButtonRedClick();
	}

	public int GetCharShipmentTypeID()
	{
		if (this.m_shipmentType == null)
		{
			return 0;
		}
		return this.m_shipmentType.CharShipmentID;
	}

	public GameObject m_troopSpecificArea;

	public GameObject m_itemSpecificArea;

	public Image m_troopSnapshotImage;

	public GameObject m_troopHeartContainer;

	public GameObject m_troopHeartPrefab;

	public Text m_troopName;

	public GameObject m_traitsAndAbilitiesRootObject;

	public GameObject m_abilityDisplayPrefab;

	public GameObject m_troopSlotsRootObject;

	public GameObject m_troopSlotPrefab;

	public Image m_troopResourceIcon;

	public Text m_troopResourceCostText;

	public Image m_itemResourceIcon;

	public Text m_itemResourceCostText;

	public Button m_recruitTroopsButton;

	public Text m_recruitButtonText;

	public Text m_itemName;

	public MissionRewardDisplay m_itemDisplay;

	private MobileClientShipmentType m_shipmentType;

	private bool m_isTroop;

	private int m_shipmentCost;

	private GarrFollowerRec m_followerRec;

	private CharShipmentRec m_charShipmentRec;
}
