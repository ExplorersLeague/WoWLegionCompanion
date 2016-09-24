using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarbageFreeStringBuilder;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class FollowerListItem : MonoBehaviour
{
	private void Awake()
	{
		if (PersistentFollowerData.followerDictionary.Count == 0)
		{
			return;
		}
		if (AssetBundleManager.instance.IsInitialized())
		{
			this.OnAssetBundleManagerInitialized();
		}
		else
		{
			AssetBundleManager instance = AssetBundleManager.instance;
			instance.InitializedAction = (Action)Delegate.Combine(instance.InitializedAction, new Action(this.OnAssetBundleManagerInitialized));
		}
		if (this.m_useArmamentsButton != null)
		{
			this.m_useArmamentsButton.SetActive(false);
			this.m_statusText.gameObject.SetActive(true);
		}
		this.followerIDText.font = GeneralHelpers.LoadStandardFont();
		this.portraitErrorText.font = GeneralHelpers.LoadStandardFont();
		this.nameText.font = GeneralHelpers.LoadStandardFont();
		this.m_levelText.font = GeneralHelpers.LoadStandardFont();
		this.m_statusText.font = GeneralHelpers.LoadStandardFont();
		this.m_xpAmountText.font = GeneralHelpers.LoadStandardFont();
		if (this.m_useArmamentsButtonText != null)
		{
			this.m_useArmamentsButtonText.font = GeneralHelpers.LoadStandardFont();
		}
		this.m_missionTimeRemaining = new Duration(0, false);
		this.m_statusTextSB = new StringBuilder(16);
	}

	private void OnEnable()
	{
		if (Main.instance == null)
		{
			return;
		}
		if (this.m_followerID > 0)
		{
			if (!PersistentFollowerData.followerDictionary.ContainsKey(this.m_followerID))
			{
				base.transform.SetParent(Main.instance.transform);
				base.gameObject.SetActive(false);
				return;
			}
			this.SetAvailabilityStatus(PersistentFollowerData.followerDictionary[this.m_followerID]);
		}
		Main instance = Main.instance;
		instance.FollowerDataChangedAction = (Action)Delegate.Combine(instance.FollowerDataChangedAction, new Action(this.FollowerDataChanged));
		Main instance2 = Main.instance;
		instance2.UseArmamentResultAction = (Action<int, JamGarrisonFollower, JamGarrisonFollower>)Delegate.Combine(instance2.UseArmamentResultAction, new Action<int, JamGarrisonFollower, JamGarrisonFollower>(this.HandleUseArmamentResult));
		if (AdventureMapPanel.instance != null)
		{
			AdventureMapPanel instance3 = AdventureMapPanel.instance;
			instance3.OnMissionFollowerSlotChanged = (Action<int, bool>)Delegate.Combine(instance3.OnMissionFollowerSlotChanged, new Action<int, bool>(this.OnMissionFollowerSlotChanged));
			AdventureMapPanel instance4 = AdventureMapPanel.instance;
			instance4.DeselectAllFollowerListItemsAction = (Action)Delegate.Combine(instance4.DeselectAllFollowerListItemsAction, new Action(this.DeselectMe));
		}
		if (this.m_followerDetailView != null && OrderHallFollowersPanel.instance != null)
		{
			OrderHallFollowersPanel instance5 = OrderHallFollowersPanel.instance;
			instance5.FollowerDetailListItemSelectedAction = (Action<int>)Delegate.Combine(instance5.FollowerDetailListItemSelectedAction, new Action<int>(this.DetailFollowerListItem_ManageFollowerDetailViewSize));
		}
	}

	private void OnDisable()
	{
		Main instance = Main.instance;
		instance.FollowerDataChangedAction = (Action)Delegate.Remove(instance.FollowerDataChangedAction, new Action(this.FollowerDataChanged));
		Main instance2 = Main.instance;
		instance2.UseArmamentResultAction = (Action<int, JamGarrisonFollower, JamGarrisonFollower>)Delegate.Remove(instance2.UseArmamentResultAction, new Action<int, JamGarrisonFollower, JamGarrisonFollower>(this.HandleUseArmamentResult));
		if (AdventureMapPanel.instance != null)
		{
			AdventureMapPanel instance3 = AdventureMapPanel.instance;
			instance3.OnMissionFollowerSlotChanged = (Action<int, bool>)Delegate.Remove(instance3.OnMissionFollowerSlotChanged, new Action<int, bool>(this.OnMissionFollowerSlotChanged));
			AdventureMapPanel instance4 = AdventureMapPanel.instance;
			instance4.DeselectAllFollowerListItemsAction = (Action)Delegate.Remove(instance4.DeselectAllFollowerListItemsAction, new Action(this.DeselectMe));
		}
		if (this.m_followerDetailView != null && OrderHallFollowersPanel.instance != null)
		{
			OrderHallFollowersPanel instance5 = OrderHallFollowersPanel.instance;
			instance5.FollowerDetailListItemSelectedAction = (Action<int>)Delegate.Remove(instance5.FollowerDetailListItemSelectedAction, new Action<int>(this.DetailFollowerListItem_ManageFollowerDetailViewSize));
		}
	}

	public void OnAssetBundleManagerInitialized()
	{
		if (FollowerListItem.m_iLvlString == null)
		{
			FollowerListItem.m_iLvlString = StaticDB.GetString("ITEM_LEVEL_ABBREVIATION", null);
		}
		if (FollowerListItem.m_inactiveString == null)
		{
			FollowerListItem.m_inactiveString = StaticDB.GetString("INACTIVE", null);
		}
		if (FollowerListItem.m_onMissionString == null)
		{
			FollowerListItem.m_onMissionString = StaticDB.GetString("ON_MISSION", null);
		}
		if (FollowerListItem.m_fatiguedString == null)
		{
			FollowerListItem.m_fatiguedString = StaticDB.GetString("FATIGUED", null);
		}
		if (FollowerListItem.m_inBuildingString == null)
		{
			FollowerListItem.m_inBuildingString = StaticDB.GetString("IN_BUILDING", null);
		}
		if (FollowerListItem.m_inPartyString == null)
		{
			FollowerListItem.m_inPartyString = StaticDB.GetString("IN_PARTY", null);
		}
		if (FollowerListItem.m_missionCompleteString == null)
		{
			FollowerListItem.m_missionCompleteString = StaticDB.GetString("MISSION_COMPLETE", null);
		}
		if (FollowerListItem.m_combatAllyString == null)
		{
			FollowerListItem.m_combatAllyString = StaticDB.GetString("COMBAT_ALLY", null);
		}
	}

	public void SetFollower(JamGarrisonFollower follower)
	{
		this.m_followerID = follower.GarrFollowerID;
		this.followerIDText.text = string.Concat(new object[]
		{
			"ID:",
			follower.GarrFollowerID,
			" Q:",
			follower.Quality
		});
		this.m_inParty = false;
		this.SetAvailabilityStatus(follower);
		GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
		if (record == null)
		{
			return;
		}
		if (record.GarrFollowerTypeID != 4u)
		{
			return;
		}
		if (record != null)
		{
			CreatureRec record2 = StaticDB.creatureDB.GetRecord((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceCreatureID : record.HordeCreatureID);
			this.nameText.text = record2.Name;
		}
		this.m_levelText.text = string.Empty + follower.FollowerLevel;
		int num = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceIconFileDataID : record.HordeIconFileDataID;
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.PortraitIcons, num);
		if (sprite != null)
		{
			this.followerPortrait.sprite = sprite;
			this.portraitErrorText.gameObject.SetActive(false);
		}
		else
		{
			this.portraitErrorText.text = string.Empty + num;
			this.portraitErrorText.gameObject.SetActive(true);
		}
		GarrClassSpecRec record3 = StaticDB.garrClassSpecDB.GetRecord((int)((GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record.AllianceGarrClassSpecID : record.HordeGarrClassSpecID));
		Sprite atlasSprite = TextureAtlas.instance.GetAtlasSprite((int)record3.UiTextureAtlasMemberID);
		if (atlasSprite != null)
		{
			this.m_classIcon.sprite = atlasSprite;
		}
		Transform[] componentsInChildren = this.m_troopHeartContainer.GetComponentsInChildren<Transform>(true);
		foreach (Transform transform in componentsInChildren)
		{
			if (transform != this.m_troopHeartContainer.transform)
			{
				Object.DestroyImmediate(transform.gameObject);
			}
		}
		bool flag = (follower.Flags & 8) != 0;
		if (flag)
		{
			this.qualityBorder.color = Color.white;
			this.levelBorder.color = Color.white;
			this.nameText.color = Color.white;
			int j;
			for (j = 0; j < follower.Durability; j++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_troopHeartPrefab);
				gameObject.transform.SetParent(this.m_troopHeartContainer.transform, false);
			}
			for (int k = j; k < record.Vitality; k++)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_troopEmptyHeartPrefab);
				gameObject2.transform.SetParent(this.m_troopHeartContainer.transform, false);
			}
			this.qualityBorder.gameObject.SetActive(false);
			this.levelBorder.gameObject.SetActive(false);
			this.followerPortraitFrame.enabled = false;
			this.m_progressBarObj.SetActive(false);
		}
		else
		{
			Color qualityColor = GeneralHelpers.GetQualityColor(follower.Quality);
			this.qualityBorder.color = qualityColor;
			this.levelBorder.color = qualityColor;
			if (follower.Quality <= 1)
			{
				this.nameText.color = Color.white;
			}
			else
			{
				this.nameText.color = qualityColor;
			}
			uint num2;
			bool flag2;
			bool flag3;
			GeneralHelpers.GetXpCapInfo(follower.FollowerLevel, follower.Quality, out num2, out flag2, out flag3);
			if (flag3)
			{
				this.m_progressBarObj.SetActive(false);
			}
			else
			{
				this.m_progressBarObj.SetActive(true);
				float fillAmount = Mathf.Clamp01((float)follower.Xp / num2);
				this.m_progressBarFillImage.fillAmount = fillAmount;
				this.m_xpAmountText.text = string.Concat(new object[]
				{
					string.Empty,
					follower.Xp,
					"/",
					num2
				});
			}
		}
	}

	private void Update()
	{
		if (!this.m_onMission)
		{
			return;
		}
		if (this.m_isCombatAlly)
		{
			return;
		}
		long num = GarrisonStatus.CurrentTime() - this.m_missionStartedTime;
		long num2 = this.m_missionDurationInSeconds - num;
		num2 = ((num2 <= 0L) ? 0L : num2);
		if (num2 > 0L)
		{
			this.m_missionTimeRemaining.FormatDurationString((int)num2, false);
			this.m_statusTextSB.Length = 0;
			this.m_statusTextSB.ConcatFormat("{0} {1} - {2} - {3}", FollowerListItem.m_iLvlString, this.m_itemLevel, FollowerListItem.m_onMissionString, this.m_missionTimeRemaining.DurationString);
			this.m_statusText.text = this.m_statusTextSB.ToString();
		}
		else
		{
			this.m_statusTextSB.Length = 0;
			this.m_statusTextSB.ConcatFormat("{0} {1} - {2} - {3}", FollowerListItem.m_iLvlString, this.m_itemLevel, FollowerListItem.m_onMissionString, FollowerListItem.m_missionCompleteString);
			this.m_statusText.text = this.m_statusTextSB.ToString();
		}
	}

	private void SelectMe()
	{
		if (this.selectedImage != null)
		{
			this.selectedImage.SetActive(true);
		}
	}

	public void DeselectMe()
	{
		if (this.selectedImage != null)
		{
			this.selectedImage.SetActive(false);
		}
	}

	public void SelectAndReplaceExistingCombatAlly()
	{
		AdventureMapPanel.instance.DeselectAllFollowerListItems();
		this.SelectMe();
		this.AddToParty(true);
	}

	public void SelectAndInspect()
	{
		AdventureMapPanel.instance.DeselectAllFollowerListItems();
		this.SelectMe();
		AdventureMapPanel.instance.SetFollowerToInspect(this.m_followerID);
	}

	public void SelectAndAddToParty()
	{
		AdventureMapPanel.instance.DeselectAllFollowerListItems();
		this.SelectMe();
		this.AddToParty(false);
	}

	private void AddToParty(bool forceReplaceFirstSlot = false)
	{
		if (this.m_availableForMission)
		{
			MissionFollowerSlotGroup componentInChildren = base.gameObject.transform.parent.parent.parent.gameObject.GetComponentInChildren<MissionFollowerSlotGroup>();
			if (componentInChildren != null && componentInChildren.gameObject.activeSelf)
			{
				this.m_inParty = componentInChildren.SetFollower(this.m_followerID, this.followerPortrait.overrideSprite, this.qualityBorder.color, forceReplaceFirstSlot);
				if (this.m_inParty)
				{
					JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[this.m_followerID];
					bool flag = (jamGarrisonFollower.Flags & 8) != 0;
					if (flag)
					{
						Main.instance.m_UISound.Play_SlotTroop();
					}
					else
					{
						Main.instance.m_UISound.Play_SlotChampion();
					}
				}
				else
				{
					Main.instance.m_UISound.Play_DefaultNavClick();
					this.DeselectMe();
				}
				this.SetAvailabilityStatus(PersistentFollowerData.followerDictionary[this.m_followerID]);
			}
		}
	}

	public void RemoveFromParty(int garrFollowerID)
	{
		if (this.m_followerID == garrFollowerID)
		{
			this.m_inParty = false;
			this.SetAvailabilityStatus(PersistentFollowerData.followerDictionary[this.m_followerID]);
		}
	}

	public void RemoveFromParty()
	{
		this.m_inParty = false;
		this.SetAvailabilityStatus(PersistentFollowerData.followerDictionary[this.m_followerID]);
	}

	private void OnMissionFollowerSlotChanged(int garrFollowerID, bool inParty)
	{
		if (this.m_followerID == garrFollowerID)
		{
			this.m_inParty = inParty;
			this.FollowerDataChanged();
		}
	}

	private void FollowerDataChanged()
	{
		if (this.m_followerID > 0)
		{
			if (PersistentFollowerData.followerDictionary.ContainsKey(this.m_followerID))
			{
				this.SetAvailabilityStatus(PersistentFollowerData.followerDictionary[this.m_followerID]);
				if (this.m_followerDetailView != null)
				{
					this.m_followerDetailView.HandleFollowerDataChanged();
				}
			}
			else
			{
				Object.DestroyImmediate(base.gameObject);
			}
		}
	}

	public void SetAvailabilityStatus(JamGarrisonFollower follower)
	{
		this.m_isCombatAlly = false;
		if (follower.CurrentMissionID != 0)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[follower.CurrentMissionID];
			this.m_missionStartedTime = jamGarrisonMobileMission.StartTime;
			this.m_missionDurationInSeconds = jamGarrisonMobileMission.MissionDuration;
		}
		this.m_itemLevel = (follower.ItemLevelWeapon + follower.ItemLevelArmor) / 2;
		bool flag = (follower.Flags & 4) != 0;
		bool flag2 = (follower.Flags & 2) != 0;
		this.m_onMission = (follower.CurrentMissionID != 0);
		bool flag3 = follower.CurrentBuildingID != 0;
		bool flag4 = (follower.Flags & 8) != 0;
		GarrMissionRec garrMissionRec = (!this.m_onMission) ? null : StaticDB.garrMissionDB.GetRecord(follower.CurrentMissionID);
		if (garrMissionRec != null && (garrMissionRec.Flags & 16u) != 0u)
		{
			this.m_isCombatAlly = true;
		}
		this.darkeningImage.gameObject.SetActive(true);
		this.darkeningImage.color = new Color(0f, 0f, 0.28f, 0.3f);
		this.m_statusText.color = Color.white;
		this.m_troopHeartContainer.SetActive(false);
		if (flag)
		{
			this.m_statusText.color = Color.red;
			if (follower.FollowerLevel == 110)
			{
				this.m_statusText.text = string.Concat(new object[]
				{
					FollowerListItem.m_iLvlString,
					" ",
					this.m_itemLevel,
					" - ",
					FollowerListItem.m_inactiveString
				});
			}
			else
			{
				this.m_statusText.text = FollowerListItem.m_inactiveString;
			}
			this.darkeningImage.color = new Color(0.28f, 0f, 0f, 0.196f);
		}
		else if (flag2)
		{
			this.m_statusText.text = string.Concat(new object[]
			{
				FollowerListItem.m_iLvlString,
				" ",
				this.m_itemLevel,
				" - ",
				FollowerListItem.m_fatiguedString
			});
		}
		else if (this.m_isCombatAlly)
		{
			this.m_statusText.text = string.Concat(new object[]
			{
				FollowerListItem.m_iLvlString,
				" ",
				this.m_itemLevel,
				" - ",
				FollowerListItem.m_combatAllyString
			});
		}
		else if (this.m_onMission)
		{
			this.m_statusText.text = string.Concat(new object[]
			{
				FollowerListItem.m_iLvlString,
				" ",
				this.m_itemLevel,
				" - ",
				FollowerListItem.m_onMissionString
			});
		}
		else if (flag3)
		{
			this.m_statusText.text = string.Concat(new object[]
			{
				FollowerListItem.m_iLvlString,
				" ",
				this.m_itemLevel,
				" - ",
				FollowerListItem.m_inBuildingString
			});
		}
		else if (this.m_inParty)
		{
			this.m_statusText.text = FollowerListItem.m_inPartyString;
			this.darkeningImage.color = new Color(0.1f, 0.6f, 0.1f, 0.3f);
		}
		else
		{
			if (!flag4 && follower.FollowerLevel == 110)
			{
				this.m_statusText.text = FollowerListItem.m_iLvlString + " " + this.m_itemLevel;
			}
			else
			{
				this.m_statusText.text = string.Empty;
			}
			this.darkeningImage.gameObject.SetActive(false);
			this.m_troopHeartContainer.SetActive(true);
		}
		if (this.m_useArmamentsButtonText != null)
		{
			this.m_useArmamentsButtonText.text = FollowerListItem.m_iLvlString + " " + this.m_itemLevel;
		}
		this.m_availableForMission = (!flag && !flag2 && !this.m_onMission && !flag3);
	}

	public void UpdateUsefulAbilitiesDisplay(int currentGarrMissionID)
	{
		AbilityDisplay[] componentsInChildren = this.usefulAbilitiesGroup.GetComponentsInChildren<AbilityDisplay>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.DestroyImmediate(componentsInChildren[i].gameObject);
		}
		List<int> list = new List<int>();
		JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[currentGarrMissionID];
		for (int j = 0; j < jamGarrisonMobileMission.Encounter.Length; j++)
		{
			int num = (jamGarrisonMobileMission.Encounter[j].MechanicID.Length <= 0) ? 0 : jamGarrisonMobileMission.Encounter[j].MechanicID[0];
			GarrMechanicRec record = StaticDB.garrMechanicDB.GetRecord(num);
			if (record == null)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"INVALID garrMechanic ID ",
					num,
					" in mission ",
					jamGarrisonMobileMission.MissionRecID
				}));
			}
			else
			{
				int abilityToCounterMechanicType = MissionMechanic.GetAbilityToCounterMechanicType((int)record.GarrMechanicTypeID);
				if (!list.Contains(abilityToCounterMechanicType))
				{
					list.Add(abilityToCounterMechanicType);
				}
			}
		}
		List<int> usefulBuffAbilitiesForFollower = MissionMechanic.GetUsefulBuffAbilitiesForFollower(this.m_followerID);
		List<int> list2 = list.Union(usefulBuffAbilitiesForFollower).ToList<int>();
		JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[this.m_followerID];
		foreach (int num2 in jamGarrisonFollower.AbilityID)
		{
			foreach (int num3 in list2)
			{
				if (num2 == num3)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_abilityDisplayPrefab);
					gameObject.transform.SetParent(this.usefulAbilitiesGroup.transform, false);
					AbilityDisplay component = gameObject.GetComponent<AbilityDisplay>();
					component.SetAbility(num2, true, false, null);
					component.m_abilityNameText.gameObject.SetActive(false);
				}
			}
		}
	}

	private void HandleUseArmamentResult(int result, JamGarrisonFollower oldFollower, JamGarrisonFollower newFollower)
	{
		if (result != 0)
		{
			return;
		}
		if (newFollower.GarrFollowerID != this.m_followerID)
		{
			return;
		}
		this.SetAvailabilityStatus(newFollower);
		UiAnimMgr.instance.PlayAnim("FlameGlowPulse", this.followerPortraitFrame.transform, Vector3.zero, 1.5f, 0f);
		Main.instance.m_UISound.Play_UpgradeArmament();
	}

	public void OnTapFollowerDetailListItem()
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AllPopups.instance.SetCurrentFollowerDetailView(this.m_followerDetailView);
		AllPopups.instance.HideChampionUpgradeDialogs();
		OrderHallFollowersPanel.instance.FollowerDetailListItemSelected(this.m_followerID);
	}

	public void SetDetailViewHeight(float value)
	{
		this.m_followerDetailViewLayoutElement.minHeight = value;
	}

	public void ExpandDetailViewComplete()
	{
		RectTransform component = this.m_followerDetailView.traitsAndAbilitiesRootObject.GetComponent<RectTransform>();
		this.m_followerDetailViewLayoutElement.minHeight = component.rect.height + (float)this.m_followerDetailViewExtraHeight;
		JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[this.m_followerID];
		int num = (jamGarrisonFollower.ItemLevelWeapon + jamGarrisonFollower.ItemLevelArmor) / 2;
		bool flag = (jamGarrisonFollower.Flags & 8) != 0;
		GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(jamGarrisonFollower.GarrFollowerID);
		if (record != null && num < 850 && jamGarrisonFollower.FollowerLevel == MissionDetailView.GarrisonFollower_GetMaxFollowerLevel((int)record.GarrFollowerTypeID) && !flag)
		{
			this.m_useArmamentsButton.SetActive(true);
			this.m_statusText.gameObject.SetActive(false);
		}
	}

	public void ContractDetailViewComplete()
	{
		this.m_followerDetailViewLayoutElement.minHeight = 0f;
		this.m_followerDetailViewCanvasGroup.alpha = 0f;
		this.m_followerDetailView.SetFollower(0);
	}

	public void SetExpandArrowRotation(float zrot)
	{
		Vector3 localEulerAngles = this.m_expandArrow.transform.localEulerAngles;
		localEulerAngles.z = zrot;
		this.m_expandArrow.transform.localEulerAngles = localEulerAngles;
	}

	public void ExpandArrowRotationComplete()
	{
		this.m_expandArrow.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
	}

	public void ContractArrowRotationComplete()
	{
		this.m_expandArrow.transform.localEulerAngles = Vector3.zero;
	}

	private void DetailFollowerListItem_ManageFollowerDetailViewSize(int garrFollowerID)
	{
		bool flag = garrFollowerID == this.m_followerID && this.m_followerDetailViewLayoutElement.minHeight == 0f;
		if (flag)
		{
			if (this.m_followerDetailView.GetCurrentFollower() != this.m_followerID)
			{
				this.m_followerDetailView.SetFollower(this.m_followerID);
			}
			iTween.StopByName(base.gameObject, "FollowerDetailExpand");
			iTween.StopByName(base.gameObject, "FollowerDetailExpandArrow");
			iTween.StopByName(base.gameObject, "FollowerDetailContract");
			iTween.StopByName(base.gameObject, "FollowerDetailContractArrow");
			this.SelectMe();
			bool flag2 = false;
			float num = 0f;
			FollowerListItem[] componentsInChildren = OrderHallFollowersPanel.instance.m_followerDetailListContent.GetComponentsInChildren<FollowerListItem>(true);
			foreach (FollowerListItem followerListItem in componentsInChildren)
			{
				if (followerListItem == this)
				{
					break;
				}
				if (followerListItem.m_followerDetailViewLayoutElement.minHeight > 0f)
				{
					num = followerListItem.m_followerDetailViewLayoutElement.minHeight;
					flag2 = true;
					break;
				}
			}
			RectTransform component = this.m_followerDetailView.traitsAndAbilitiesRootObject.GetComponent<RectTransform>();
			OrderHallFollowersPanel.instance.ScrollListTo(-base.transform.localPosition.y - ((!flag2) ? 0f : num) - 56f);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FollowerDetailExpand",
				"from",
				this.m_followerDetailViewLayoutElement.minHeight,
				"to",
				component.rect.height + (float)this.m_followerDetailViewExtraHeight,
				"time",
				0.25f,
				"easetype",
				iTween.EaseType.easeOutCubic,
				"onupdate",
				"SetDetailViewHeight",
				"oncomplete",
				"ExpandDetailViewComplete"
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FollowerDetailExpandArrow",
				"from",
				0,
				"to",
				-90f,
				"time",
				0.25f,
				"easetype",
				iTween.EaseType.easeOutCubic,
				"onupdate",
				"SetExpandArrowRotation",
				"oncomplete",
				"ExpandArrowRotationComplete"
			}));
			this.m_followerDetailViewCanvasGroup.alpha = 1f;
		}
		else if (this.m_followerDetailViewLayoutElement.minHeight > 0f)
		{
			iTween.StopByName(base.gameObject, "FollowerDetailExpand");
			iTween.StopByName(base.gameObject, "FollowerDetailExpandArrow");
			iTween.StopByName(base.gameObject, "FollowerDetailContract");
			iTween.StopByName(base.gameObject, "FollowerDetailContractArrow");
			this.DeselectMe();
			this.m_useArmamentsButton.SetActive(false);
			this.m_statusText.gameObject.SetActive(true);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FollowerDetailContract",
				"from",
				this.m_followerDetailViewLayoutElement.minHeight,
				"to",
				0f,
				"time",
				0.25f,
				"easetype",
				iTween.EaseType.easeOutCubic,
				"onupdate",
				"SetDetailViewHeight",
				"oncomplete",
				"ContractDetailViewComplete"
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FollowerDetailContractArrow",
				"from",
				this.m_expandArrow.transform.localEulerAngles.z,
				"to",
				360f,
				"time",
				0.25f,
				"easetype",
				iTween.EaseType.easeOutCubic,
				"onupdate",
				"SetExpandArrowRotation",
				"oncomplete",
				"ContractArrowRotationComplete"
			}));
		}
	}

	public void ShowArmamentDialog()
	{
		if (AllPopups.instance.GetCurrentFollowerDetailView() != this.m_followerDetailView)
		{
			return;
		}
		AllPopups.instance.ShowArmamentDialog(this.m_followerDetailView, true);
	}

	public int m_followerID;

	public Text followerIDText;

	public Text portraitErrorText;

	public Image followerPortrait;

	public Image followerPortraitFrame;

	public Text nameText;

	public Text m_levelText;

	public Text m_statusText;

	public GameObject selectedImage;

	public Image qualityBorder;

	public Image levelBorder;

	public Image darkeningImage;

	public Image m_classIcon;

	public GameObject usefulAbilitiesGroup;

	public GameObject m_abilityDisplayPrefab;

	public GameObject missionMechanicCounterPrefab;

	public GameObject m_troopHeartContainer;

	public GameObject m_troopHeartPrefab;

	public GameObject m_troopEmptyHeartPrefab;

	public bool m_inParty;

	private bool m_availableForMission;

	private bool m_onMission;

	public long m_missionStartedTime;

	public long m_missionDurationInSeconds;

	[Header("XP Bar")]
	public GameObject m_progressBarObj;

	public Image m_progressBarFillImage;

	public Text m_xpAmountText;

	[Header("Detail Follower List Items Only")]
	public FollowerDetailView m_followerDetailView;

	public CanvasGroup m_followerDetailViewCanvasGroup;

	public LayoutElement m_followerDetailViewLayoutElement;

	public int m_followerDetailViewExtraHeight;

	public RectTransform m_listItemArea;

	public Image m_expandArrow;

	public GameObject m_selectionGlowRoot;

	public GameObject m_useArmamentsButton;

	public Text m_useArmamentsButtonText;

	public Image m_useArmamentsButtonUpArrowGreen;

	private bool m_isCombatAlly;

	private int m_itemLevel;

	private Duration m_missionTimeRemaining;

	private StringBuilder m_statusTextSB;

	private static string m_iLvlString;

	private static string m_inactiveString;

	private static string m_onMissionString;

	private static string m_fatiguedString;

	private static string m_inBuildingString;

	private static string m_inPartyString;

	private static string m_missionCompleteString;

	private static string m_combatAllyString;
}
