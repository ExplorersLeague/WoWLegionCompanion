using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class TroopsListItem : MonoBehaviour
	{
		public void HandleFollowerDataChanged()
		{
			if (this.m_charShipmentRec == null)
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
			bool flag = GarrisonStatus.WarResources() >= this.m_shipmentCost;
			this.m_itemResourceCostText.color = ((!flag) ? Color.red : Color.white);
			bool flag2 = true;
			if (this.m_charShipmentRec != null && !PersistentShipmentData.CanOrderShipmentType(this.m_charShipmentRec.ID))
			{
				flag2 = false;
			}
			TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
			if (componentsInChildren.Length < 6)
			{
				this.m_troopSlotsRootObject.GetComponent<GridLayoutGroup>().constraintCount = 2;
			}
			else
			{
				this.m_troopSlotsRootObject.GetComponent<GridLayoutGroup>().constraintCount = 3;
			}
			bool flag3 = false;
			foreach (TroopSlot troopSlot in componentsInChildren)
			{
				if (troopSlot.IsEmpty())
				{
					flag3 = true;
					break;
				}
			}
			this.m_recruitButtonText.color = new Color(1f, 0.82f, 0f, 1f);
			if (!flag3)
			{
				this.m_recruitButtonText.text = StaticDB.GetString("SLOTS_FULL", null);
				this.m_recruitButtonText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
			else if (!flag)
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
			if (flag3 && flag && flag2)
			{
				this.m_recruitTroopsButton.interactable = true;
			}
			else
			{
				this.m_recruitTroopsButton.interactable = false;
				this.m_recruitButtonText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		private void Awake()
		{
			this.ClearAndHideLootArea();
			this.m_akLevelBefore = 0;
		}

		private void Start()
		{
			if (Main.instance.IsNarrowScreen())
			{
				if (this.m_troopSpecificArea != null)
				{
					Main.instance.NudgeX(ref this.m_troopSpecificArea, 30f);
				}
				if (this.m_itemSpecificArea != null)
				{
					Main.instance.NudgeX(ref this.m_itemSpecificArea, 30f);
				}
				if (this.m_troopSlotsRootObject != null)
				{
					GridLayoutGroup component = this.m_troopSlotsRootObject.GetComponent<GridLayoutGroup>();
					if (component != null)
					{
						Vector2 spacing = component.spacing;
						spacing.x = 20f;
						component.spacing = spacing;
					}
				}
			}
			if (GarrisonStatus.Faction() == PVP_FACTION.HORDE)
			{
				this.m_hordeBanner.SetActive(true);
				this.m_allianceBanner.SetActive(false);
			}
			else if (GarrisonStatus.Faction() == PVP_FACTION.ALLIANCE)
			{
				this.m_hordeBanner.SetActive(false);
				this.m_allianceBanner.SetActive(true);
			}
			this.m_troopResourceCostText.font = GeneralHelpers.LoadStandardFont();
			this.m_itemResourceCostText.font = GeneralHelpers.LoadStandardFont();
			this.m_recruitButtonText.font = GeneralHelpers.LoadStandardFont();
			this.m_itemName.font = GeneralHelpers.LoadStandardFont();
			this.m_youReceivedLoot.font = GeneralHelpers.LoadStandardFont();
			this.m_youReceivedLoot.text = StaticDB.GetString("YOU_RECEIVED_LOOT", "You received loot:");
			this.m_akHintText.font = FontLoader.LoadStandardFont();
			this.m_artifactKnowledgeLevelIncreasedLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_artifactKnowledgeLevelIncreasedLabel.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			this.HandleFollowerDataChanged();
			Singleton<GarrisonWrapper>.Instance.ShipmentAddedAction += this.HandleShipmentAdded;
		}

		private void OnDisable()
		{
			Singleton<GarrisonWrapper>.Instance.ShipmentAddedAction -= this.HandleShipmentAdded;
		}

		public void SetCharShipment(WrapperShipmentType? shipmentType, bool isSealOfFateHack = false, CharShipmentRec sealOfFateHackCharShipmentRec = null)
		{
			this.m_akHintText.gameObject.SetActive(false);
			if (isSealOfFateHack)
			{
				this.m_shipmentCost = 0;
			}
			else
			{
				this.m_shipmentCost = shipmentType.Value.CurrencyCost;
			}
			Transform[] componentsInChildren = this.m_troopHeartContainer.GetComponentsInChildren<Transform>(true);
			foreach (Transform transform in componentsInChildren)
			{
				if (transform != this.m_troopHeartContainer.transform)
				{
					transform.transform.SetParent(null);
					Object.Destroy(transform.gameObject);
				}
			}
			AbilityDisplay[] componentsInChildren2 = this.m_traitsAndAbilitiesRootObject.GetComponentsInChildren<AbilityDisplay>(true);
			foreach (AbilityDisplay abilityDisplay in componentsInChildren2)
			{
				abilityDisplay.transform.SetParent(null);
				Object.Destroy(abilityDisplay.gameObject);
			}
			TroopSlot[] componentsInChildren3 = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
			foreach (TroopSlot troopSlot in componentsInChildren3)
			{
				troopSlot.transform.SetParent(null);
				Object.Destroy(troopSlot.gameObject);
			}
			CharShipmentRec charShipmentRec = (!isSealOfFateHack) ? StaticDB.charShipmentDB.GetRecord(shipmentType.Value.CharShipmentID) : sealOfFateHackCharShipmentRec;
			if (charShipmentRec == null)
			{
				Debug.LogError("Invalid Shipment ID: " + shipmentType.Value.CharShipmentID);
				this.m_troopName.text = "Invalid Shipment ID: " + shipmentType.Value.CharShipmentID;
				return;
			}
			if (charShipmentRec.GarrFollowerID > 0u)
			{
				this.SetCharShipmentTroop(shipmentType.Value, charShipmentRec);
			}
			else if (charShipmentRec.DummyItemID > 0)
			{
				this.SetCharShipmentItem(shipmentType.Value, (!isSealOfFateHack) ? charShipmentRec : sealOfFateHackCharShipmentRec, isSealOfFateHack);
			}
		}

		private void UpdateItemSlots()
		{
			if (this.m_isArtifactResearch && this.m_akResearchDisabled)
			{
				TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Object.Destroy(componentsInChildren[i].gameObject);
				}
				return;
			}
			bool flag = true;
			if (this.m_charShipmentRec != null && !PersistentShipmentData.CanPickupShipmentType(this.m_charShipmentRec.ID))
			{
				flag = false;
			}
			int num = 0;
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				if (wrapperCharacterShipment.ShipmentRecID == this.m_charShipmentRec.ID)
				{
					num++;
					break;
				}
			}
			if (num > 0 && !flag)
			{
				this.m_troopSlotsCanvasGroup.alpha = 0f;
				this.m_troopSlotsCanvasGroup.interactable = false;
				this.m_troopSlotsCanvasGroup.blocksRaycasts = false;
			}
			else
			{
				this.m_troopSlotsCanvasGroup.alpha = 1f;
				this.m_troopSlotsCanvasGroup.interactable = true;
				this.m_troopSlotsCanvasGroup.blocksRaycasts = true;
			}
			int maxShipments = (int)this.m_charShipmentRec.MaxShipments;
			TroopSlot[] componentsInChildren2 = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
			if (componentsInChildren2.Length < maxShipments)
			{
				for (int j = componentsInChildren2.Length; j < maxShipments; j++)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopSlotPrefab);
					gameObject.transform.SetParent(this.m_troopSlotsRootObject.transform, false);
					TroopSlot component = gameObject.GetComponent<TroopSlot>();
					component.SetCharShipment(this.m_charShipmentRec.ID, 0UL, 0, false, 0);
				}
			}
			if (componentsInChildren2.Length > maxShipments)
			{
				for (int k = maxShipments; k < componentsInChildren2.Length; k++)
				{
					Object.Destroy(componentsInChildren2[k].gameObject);
				}
			}
			componentsInChildren2 = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
			foreach (TroopSlot troopSlot in componentsInChildren2)
			{
				if (troopSlot.GetDBID() != 0UL && !PersistentShipmentData.shipmentDictionary.ContainsKey(troopSlot.GetDBID()))
				{
					troopSlot.SetCharShipment(this.m_charShipmentRec.ID, 0UL, 0, false, 0);
				}
			}
			foreach (WrapperCharacterShipment wrapperCharacterShipment2 in PersistentShipmentData.shipmentDictionary.Values)
			{
				if (wrapperCharacterShipment2.ShipmentRecID == this.m_charShipmentRec.ID)
				{
					this.SetTroopSlotForPendingShipment(componentsInChildren2, wrapperCharacterShipment2.ShipmentID);
				}
			}
		}

		private void SetCharShipmentItem(WrapperShipmentType shipmentType, CharShipmentRec charShipmentRec, bool isSealOfFateHack = false)
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
			this.m_itemResourceCostText.gameObject.SetActive(!isSealOfFateHack);
			this.m_itemResourceIcon.gameObject.SetActive(!isSealOfFateHack);
			if (!isSealOfFateHack)
			{
				this.m_itemResourceCostText.text = string.Empty + shipmentType.CurrencyCost;
				Sprite sprite2 = GeneralHelpers.LoadCurrencyIcon(shipmentType.CurrencyTypeID);
				if (sprite2 != null)
				{
					this.m_itemResourceIcon.sprite = sprite2;
				}
			}
			this.UpdateItemSlots();
			this.UpdateRecruitButtonState();
		}

		private void SetTroopSlotForExistingFollower(TroopSlot[] troopSlots, WrapperGarrisonFollower follower)
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
					troopSlot2.SetCharShipment(this.m_charShipmentRec.ID, 0UL, follower.GarrFollowerID, false, iconFileDataID);
					return;
				}
			}
			foreach (TroopSlot troopSlot3 in troopSlots)
			{
				if (troopSlot3.IsPendingCreate())
				{
					GarrFollowerRec record2 = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
					int iconFileDataID2 = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record2.AllianceIconFileDataID : record2.HordeIconFileDataID;
					troopSlot3.SetCharShipment(this.m_charShipmentRec.ID, 0UL, follower.GarrFollowerID, false, iconFileDataID2);
					return;
				}
			}
			foreach (TroopSlot troopSlot4 in troopSlots)
			{
				if (troopSlot4.IsEmpty())
				{
					GarrFollowerRec record3 = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
					int iconFileDataID3 = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record3.AllianceIconFileDataID : record3.HordeIconFileDataID;
					troopSlot4.SetCharShipment(this.m_charShipmentRec.ID, 0UL, follower.GarrFollowerID, false, iconFileDataID3);
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
				if (troopSlot2.IsPendingCreate())
				{
					troopSlot2.SetCharShipment(this.m_charShipmentRec.ID, shipmentDBID, 0, true, 0);
					return;
				}
			}
			foreach (TroopSlot troopSlot3 in troopSlots)
			{
				if (troopSlot3.IsEmpty())
				{
					troopSlot3.SetCharShipment(this.m_charShipmentRec.ID, shipmentDBID, 0, true, 0);
					return;
				}
			}
		}

		private void UpdateTroopSlots()
		{
			if (this.m_followerRec == null || this.m_charShipmentRec == null)
			{
				return;
			}
			int maxTroops = this.GetMaxTroops((int)((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? this.m_followerRec.AllianceGarrClassSpecID : this.m_followerRec.HordeGarrClassSpecID));
			TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
			if (componentsInChildren.Length < maxTroops)
			{
				for (int i = componentsInChildren.Length; i < maxTroops; i++)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopSlotPrefab);
					gameObject.transform.SetParent(this.m_troopSlotsRootObject.transform, false);
					TroopSlot component = gameObject.GetComponent<TroopSlot>();
					component.SetCharShipment(this.m_charShipmentRec.ID, 0UL, 0, false, 0);
				}
			}
			if (componentsInChildren.Length > maxTroops)
			{
				for (int j = maxTroops; j < componentsInChildren.Length; j++)
				{
					Object.Destroy(componentsInChildren[j].gameObject);
				}
			}
			componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
			foreach (TroopSlot troopSlot in componentsInChildren)
			{
				int ownedFollowerID = troopSlot.GetOwnedFollowerID();
				if (ownedFollowerID != 0 && (!PersistentFollowerData.followerDictionary.ContainsKey(ownedFollowerID) || PersistentFollowerData.followerDictionary[ownedFollowerID].Durability == 0))
				{
					troopSlot.SetCharShipment(this.m_charShipmentRec.ID, 0UL, 0, false, 0);
				}
			}
			uint num = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? this.m_followerRec.AllianceGarrClassSpecID : this.m_followerRec.HordeGarrClassSpecID;
			foreach (WrapperGarrisonFollower follower in PersistentFollowerData.followerDictionary.Values)
			{
				GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
				uint num2 = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceGarrClassSpecID : record.HordeGarrClassSpecID;
				if (num2 == num && follower.Durability > 0)
				{
					this.SetTroopSlotForExistingFollower(componentsInChildren, follower);
				}
			}
			CharShipmentRec record2 = StaticDB.charShipmentDB.GetRecord(this.m_charShipmentRec.ID);
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				if (wrapperCharacterShipment.ShipmentRecID == this.m_charShipmentRec.ID)
				{
					this.SetTroopSlotForPendingShipment(componentsInChildren, wrapperCharacterShipment.ShipmentID);
				}
				else
				{
					CharShipmentRec record3 = StaticDB.charShipmentDB.GetRecord(wrapperCharacterShipment.ShipmentRecID);
					if (record3.ContainerID == record2.ContainerID)
					{
						this.SetTroopSlotForPendingShipment(componentsInChildren, wrapperCharacterShipment.ShipmentID);
					}
				}
			}
		}

		private void SetCharShipmentTroop(WrapperShipmentType shipmentType, CharShipmentRec charShipmentRec)
		{
			this.m_isTroop = true;
			this.m_charShipmentRec = charShipmentRec;
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
			Sprite sprite = AssetBundleManager.PortraitIcons.LoadAsset<Sprite>(text);
			for (int i = 0; i < record.Vitality; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopHeartPrefab);
				gameObject.transform.SetParent(this.m_troopHeartContainer.transform, false);
			}
			this.m_troopName.text = record2.Name;
			foreach (GarrFollowerXAbilityRec garrFollowerXAbilityRec in from rec in StaticDB.garrFollowerXAbilityDB.GetRecordsByParentID((int)charShipmentRec.GarrFollowerID)
			where rec.FactionIndex == (int)GarrisonStatus.Faction()
			select rec)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_abilityDisplayPrefab);
				gameObject2.transform.SetParent(this.m_traitsAndAbilitiesRootObject.transform, false);
				AbilityDisplay component = gameObject2.GetComponent<AbilityDisplay>();
				component.SetAbility(garrFollowerXAbilityRec.GarrAbilityID, true, true, null);
			}
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
			if (this.m_charShipmentRec.GarrFollowerID > 0u)
			{
				TroopSlot troopSlot = null;
				TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
				foreach (TroopSlot troopSlot2 in componentsInChildren)
				{
					if (troopSlot2.IsEmpty())
					{
						troopSlot = troopSlot2;
						break;
					}
				}
				if (troopSlot == null)
				{
					return;
				}
				troopSlot.SetPendingCreate();
				this.UpdateRecruitButtonState();
			}
			LegionCompanionWrapper.CreateShipment(this.m_charShipmentRec.ID, 1);
			Main.instance.m_UISound.Play_RecruitTroop();
		}

		private void HandleShipmentAdded(int charShipmentID, ulong shipmentDBID)
		{
			if (charShipmentID == this.m_charShipmentRec.ID)
			{
				TroopSlot[] componentsInChildren = this.m_troopSlotsRootObject.GetComponentsInChildren<TroopSlot>(true);
				foreach (TroopSlot troopSlot in componentsInChildren)
				{
					if (troopSlot.GetDBID() == shipmentDBID)
					{
						return;
					}
				}
				foreach (TroopSlot troopSlot2 in componentsInChildren)
				{
					if (troopSlot2.IsPendingCreate())
					{
						troopSlot2.SetCharShipment(charShipmentID, shipmentDBID, 0, true, 0);
						this.UpdateRecruitButtonState();
						return;
					}
				}
				foreach (TroopSlot troopSlot3 in componentsInChildren)
				{
					if (troopSlot3.IsEmpty())
					{
						troopSlot3.SetCharShipment(charShipmentID, shipmentDBID, 0, true, 0);
						this.UpdateRecruitButtonState();
						return;
					}
				}
			}
		}

		private int GetMaxTroops(int garrClassSpecID)
		{
			GarrClassSpecRec record = StaticDB.garrClassSpecDB.GetRecord(garrClassSpecID);
			int num = 0;
			if (record != null)
			{
				num = (int)record.FollowerClassLimit;
			}
			foreach (WrapperGarrisonTalent wrapperGarrisonTalent in PersistentTalentData.talentDictionary.Values)
			{
				if ((wrapperGarrisonTalent.Flags & 1) != 0)
				{
					GarrTalentRec record2 = StaticDB.garrTalentDB.GetRecord(wrapperGarrisonTalent.GarrTalentID);
					if (record2 != null)
					{
						foreach (GarrAbilityEffectRec garrAbilityEffectRec in from rec in StaticDB.garrAbilityEffectDB.GetRecordsByParentID((int)record2.GarrAbilityID)
						where rec.AbilityAction == 34u && (ulong)rec.ActionRecordID == (ulong)((long)garrClassSpecID)
						select rec)
						{
							num += (int)garrAbilityEffectRec.ActionValueFlat;
						}
					}
				}
			}
			return num;
		}

		public void PlayClickSound()
		{
			Main.instance.m_UISound.Play_ButtonRedClick();
		}

		public int GetCharShipmentTypeID()
		{
			if (this.m_charShipmentRec == null)
			{
				return 0;
			}
			return this.m_charShipmentRec.ID;
		}

		public void HandleShipmentItemPushed(WrapperShipmentItem item)
		{
			if (!this.m_itemResourceCostText.gameObject.activeSelf)
			{
				return;
			}
			if (!this.m_lootDisplayArea.activeSelf)
			{
				this.m_lootDisplayArea.SetActive(true);
			}
			int charShipmentTypeID = this.GetCharShipmentTypeID();
			MissionRewardDisplay missionRewardDisplay;
			if (charShipmentTypeID >= 372 && charShipmentTypeID <= 383)
			{
				missionRewardDisplay = Object.Instantiate<MissionRewardDisplay>(this.m_rewardDisplayPrefab);
				missionRewardDisplay.transform.SetParent(this.m_lootItemArea.transform, false);
				missionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.currency, item.ItemID, item.Count, 0, 0);
			}
			else
			{
				missionRewardDisplay = Object.Instantiate<MissionRewardDisplay>(this.m_rewardDisplayPrefab);
				missionRewardDisplay.transform.SetParent(this.m_lootItemArea.transform, false);
				missionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.item, item.ItemID, item.Count, item.Context, item.IconFileDataID);
			}
			if (missionRewardDisplay != null)
			{
				UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", missionRewardDisplay.transform, Vector3.zero, 1.5f, 0f);
			}
		}

		public void ClearAndHideLootArea()
		{
			MissionRewardDisplay[] componentsInChildren = this.m_lootItemArea.GetComponentsInChildren<MissionRewardDisplay>(true);
			foreach (MissionRewardDisplay missionRewardDisplay in componentsInChildren)
			{
				Object.DestroyObject(missionRewardDisplay.gameObject);
			}
			this.m_lootDisplayArea.SetActive(false);
		}

		public GameObject m_troopSpecificArea;

		public GameObject m_itemSpecificArea;

		public Text m_akHintText;

		public GameObject m_troopHeartContainer;

		public GameObject m_troopHeartPrefab;

		public Text m_troopName;

		public GameObject m_traitsAndAbilitiesRootObject;

		public GameObject m_abilityDisplayPrefab;

		public GameObject m_troopSlotsRootObject;

		public CanvasGroup m_troopSlotsCanvasGroup;

		public GameObject m_troopSlotPrefab;

		public Image m_troopResourceIcon;

		public Text m_troopResourceCostText;

		public Image m_itemResourceIcon;

		public Text m_itemResourceCostText;

		public Button m_recruitTroopsButton;

		public Text m_recruitButtonText;

		public Text m_itemName;

		public MissionRewardDisplay m_itemDisplay;

		public GameObject m_lootDisplayArea;

		public GameObject m_lootItemArea;

		public MissionRewardDisplay m_rewardDisplayPrefab;

		public Text m_youReceivedLoot;

		public Text m_artifactKnowledgeLevelIncreasedLabel;

		public MissionRewardDisplay m_artifactResearchNotesDisplayPrefab;

		public GameObject m_allianceBanner;

		public GameObject m_hordeBanner;

		private bool m_isTroop;

		private int m_shipmentCost;

		private GarrFollowerRec m_followerRec;

		private CharShipmentRec m_charShipmentRec;

		private bool m_isArtifactResearch;

		private int m_akLevelBefore;

		private bool m_akResearchDisabled;
	}
}
