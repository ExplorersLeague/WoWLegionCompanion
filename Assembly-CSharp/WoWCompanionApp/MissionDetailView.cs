using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionDetailView : MonoBehaviour
	{
		public void Awake()
		{
			this.m_percentChance = 0;
			if (Singleton<AssetBundleManager>.instance.IsInitialized())
			{
				this.OnAssetBundleManagerInitialized();
			}
			else
			{
				AssetBundleManager instance = Singleton<AssetBundleManager>.instance;
				instance.InitializedAction = (Action)Delegate.Combine(instance.InitializedAction, new Action(this.OnAssetBundleManagerInitialized));
			}
			if (this.m_previewView != null)
			{
				this.m_previewView.SetActive(true);
			}
			Material material = new Material(this.m_grayscaleShader);
			this.m_startMissionButton.material = material;
			this.m_startMissionButton.material.SetFloat("_GrayscaleAmount", 0f);
			UiAnimMgr.instance.PlayAnim("ItemReadyToUseGlowLoop", this.m_missionTypePulseRoot.transform, Vector3.zero, 0.6f, 0f);
		}

		private void Start()
		{
			if (Main.instance.IsNarrowScreen())
			{
				this.NarrowScreenAdjust();
			}
		}

		public void OnAssetBundleManagerInitialized()
		{
			this.m_currentGarrMissionID = 0;
			if (MissionDetailView.m_timeText == null)
			{
				MissionDetailView.m_timeText = StaticDB.GetString("TIME", null);
			}
			if (MissionDetailView.m_typeText == null)
			{
				MissionDetailView.m_typeText = StaticDB.GetString("TYPE", null);
			}
			MissionDetailView.m_iLevelText = StaticDB.GetString("ITEM_LEVEL_ABBREVIATION", null);
		}

		public void HideMissionDetailView()
		{
			Main.instance.m_UISound.Play_CloseButton();
			base.GetComponentInParent<MissionDialog>().gameObject.SetActive(false);
		}

		private void OnApplicationPause(bool paused)
		{
			this.HideMissionDetailView();
		}

		private void OnEnable()
		{
			PersistentFollowerData.ClearPreMissionFollowerData();
			if (!this.m_isCombatAlly)
			{
				if (this.m_usedForMissionList)
				{
					AdventureMapPanel instance = AdventureMapPanel.instance;
					instance.MissionSelectedFromListAction = (Action<int>)Delegate.Combine(instance.MissionSelectedFromListAction, new Action<int>(this.HandleMissionSelected));
				}
				else
				{
					AdventureMapPanel instance2 = AdventureMapPanel.instance;
					instance2.MissionSelectedFromMapAction = (Action<int>)Delegate.Combine(instance2.MissionSelectedFromMapAction, new Action<int>(this.HandleMissionSelected));
				}
				if (Main.instance != null)
				{
					Singleton<GarrisonWrapper>.Instance.MissionSuccessChanceChangedAction += this.OnMissionSuccessChanceChanged;
				}
			}
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideMissionDialog, null);
		}

		private void OnDisable()
		{
			if (!this.m_isCombatAlly)
			{
				if (this.m_usedForMissionList)
				{
					AdventureMapPanel instance = AdventureMapPanel.instance;
					instance.MissionSelectedFromListAction = (Action<int>)Delegate.Remove(instance.MissionSelectedFromListAction, new Action<int>(this.HandleMissionSelected));
				}
				else
				{
					AdventureMapPanel instance2 = AdventureMapPanel.instance;
					instance2.MissionSelectedFromMapAction = (Action<int>)Delegate.Remove(instance2.MissionSelectedFromMapAction, new Action<int>(this.HandleMissionSelected));
				}
				if (Main.instance != null)
				{
					Singleton<GarrisonWrapper>.Instance.MissionSuccessChanceChangedAction -= this.OnMissionSuccessChanceChanged;
				}
			}
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void SetupInputForPreviewSlider(Button button)
		{
			if (this.m_missionPanelSlider == null)
			{
				return;
			}
			EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = 13;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.m_missionPanelSlider.m_sliderPanel.OnBeginDrag(eventData);
			});
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.m_missionPanelSlider.StopTheBounce();
			});
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				button.enabled = false;
			});
			eventTrigger.triggers.Add(entry);
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = 5;
			entry2.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.m_missionPanelSlider.m_sliderPanel.OnDrag(eventData);
			});
			eventTrigger.triggers.Add(entry2);
			EventTrigger.Entry entry3 = new EventTrigger.Entry();
			entry3.eventID = 14;
			entry3.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.m_missionPanelSlider.m_sliderPanel.MissionPanelSlider_HandleAutopositioning_Bottom();
			});
			entry3.callback.AddListener(delegate(BaseEventData eventData)
			{
				button.enabled = true;
			});
			eventTrigger.triggers.Add(entry3);
		}

		private int GetTrueMissionCost(GarrMissionRec garrMissionRec, List<int> followerBuffAbilityIDs)
		{
			float num = garrMissionRec.MissionCost;
			if (this.enemyPortraitsGroup != null)
			{
				MissionMechanic[] componentsInChildren = this.enemyPortraitsGroup.GetComponentsInChildren<MissionMechanic>(true);
				foreach (MissionMechanic missionMechanic in componentsInChildren)
				{
					if (!missionMechanic.IsCountered())
					{
						if (missionMechanic.AbilityID() != 0)
						{
							foreach (GarrAbilityEffectRec garrAbilityEffectRec in from rec in StaticDB.garrAbilityEffectDB.GetRecordsByParentID(missionMechanic.AbilityID())
							where rec.AbilityAction == 39
							select rec)
							{
								num *= garrAbilityEffectRec.ActionValueFlat;
							}
						}
					}
				}
			}
			foreach (int parentID in followerBuffAbilityIDs)
			{
				foreach (GarrAbilityEffectRec garrAbilityEffectRec2 in from rec in StaticDB.garrAbilityEffectDB.GetRecordsByParentID(parentID)
				where rec.AbilityAction == 39
				select rec)
				{
					num *= garrAbilityEffectRec2.ActionValueFlat;
				}
			}
			return (int)num;
		}

		public void HandleMissionSelected(int garrMissionID)
		{
			if (garrMissionID <= 0)
			{
				return;
			}
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
			if (record == null)
			{
				return;
			}
			this.m_currentGarrMissionID = garrMissionID;
			if (this.missionFollowerSlotGroup != null)
			{
				MissionFollowerSlot[] componentsInChildren = this.missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
				foreach (MissionFollowerSlot missionFollowerSlot in componentsInChildren)
				{
					missionFollowerSlot.ClearFollower();
				}
				RectTransform[] componentsInChildren2 = this.missionFollowerSlotGroup.GetComponentsInChildren<RectTransform>(true);
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					if (componentsInChildren2[j] != null && componentsInChildren2[j] != this.missionFollowerSlotGroup.transform)
					{
						componentsInChildren2[j].gameObject.transform.SetParent(null);
						Object.Destroy(componentsInChildren2[j].gameObject);
					}
				}
			}
			if (this.enemyPortraitsGroup != null)
			{
				RectTransform[] componentsInChildren3 = this.enemyPortraitsGroup.GetComponentsInChildren<RectTransform>(true);
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					if (componentsInChildren3[k] != null && componentsInChildren3[k] != this.enemyPortraitsGroup.transform)
					{
						componentsInChildren3[k].gameObject.transform.SetParent(null);
						Object.Destroy(componentsInChildren3[k].gameObject);
					}
				}
			}
			if (this.m_previewMechanicsGroup != null)
			{
				AbilityDisplay[] componentsInChildren4 = this.m_previewMechanicsGroup.GetComponentsInChildren<AbilityDisplay>(true);
				for (int l = 0; l < componentsInChildren4.Length; l++)
				{
					if (componentsInChildren4[l] != null)
					{
						componentsInChildren4[l].gameObject.transform.SetParent(null);
						Object.Destroy(componentsInChildren4[l].gameObject);
					}
				}
			}
			if (this.treasureChestHorde != null && this.treasureChestAlliance != null)
			{
				if (GarrisonStatus.Faction() == PVP_FACTION.HORDE)
				{
					this.treasureChestHorde.SetActive(true);
					this.treasureChestAlliance.SetActive(false);
				}
				else
				{
					this.treasureChestHorde.SetActive(false);
					this.treasureChestAlliance.SetActive(true);
				}
			}
			WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[garrMissionID];
			if (this.enemyPortraitsGroup != null)
			{
				for (int m = 0; m < wrapperGarrisonMission.Encounters.Count; m++)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.missionEncounterPrefab);
					gameObject.transform.SetParent(this.enemyPortraitsGroup.transform, false);
					MissionEncounter component = gameObject.GetComponent<MissionEncounter>();
					int num = (wrapperGarrisonMission.Encounters[m].MechanicIDs.Count <= 0) ? 0 : wrapperGarrisonMission.Encounters[m].MechanicIDs[0];
					component.SetEncounter(wrapperGarrisonMission.Encounters[m].EncounterID, num);
					if (this.m_previewMechanicsGroup != null)
					{
						GarrMechanicRec record2 = StaticDB.garrMechanicDB.GetRecord(num);
						if (record2 != null && record2.GarrAbilityID != 0)
						{
							GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_previewMechanicEffectPrefab);
							gameObject2.transform.SetParent(this.m_previewMechanicsGroup.transform, false);
							AbilityDisplay component2 = gameObject2.GetComponent<AbilityDisplay>();
							component2.SetAbility(record2.GarrAbilityID, false, false, null);
							this.SetupInputForPreviewSlider(component2.m_mainButton);
							FollowerCanCounterMechanic canCounterStatus = GeneralHelpers.HasFollowerWhoCanCounter((int)record2.GarrMechanicTypeID);
							component2.SetCanCounterStatus(canCounterStatus);
						}
					}
				}
			}
			this.missionNameText.text = record.Name;
			if (this.m_previewMissionNameText != null)
			{
				this.m_previewMissionNameText.text = record.Name;
			}
			if (this.m_previewMissionLocationText != null)
			{
				this.m_previewMissionLocationText.text = record.Location;
			}
			if (this.missionDescriptionText != null)
			{
				this.missionDescriptionText.text = record.Description;
			}
			if (this.missioniLevelText != null)
			{
				if ((int)record.TargetLevel < 110)
				{
					this.missioniLevelText.text = string.Empty + record.TargetLevel;
				}
				else
				{
					this.missioniLevelText.text = string.Concat(new object[]
					{
						string.Empty,
						record.TargetLevel,
						"\n(",
						record.TargetItemLevel,
						")"
					});
				}
			}
			if (this.m_previewMissioniLevelText != null)
			{
				this.m_previewMissioniLevelText.text = MissionDetailView.m_iLevelText + " " + record.TargetItemLevel;
			}
			if (this.missionTypeImage != null)
			{
				GarrMissionTypeRec record3 = StaticDB.garrMissionTypeDB.GetRecord((int)record.GarrMissionTypeID);
				this.missionTypeImage.overrideSprite = TextureAtlas.instance.GetAtlasSprite((int)record3.UiTextureAtlasMemberID);
				if (this.m_previewMissionTypeImage != null)
				{
					this.m_previewMissionTypeImage.overrideSprite = TextureAtlas.instance.GetAtlasSprite((int)record3.UiTextureAtlasMemberID);
				}
			}
			this.missionTypeText.gameObject.SetActive(false);
			if (this.missionTypeText != null)
			{
				GarrMechanicRec record4 = StaticDB.garrMechanicDB.GetRecord(record.EnvGarrMechanicID);
				if (record4 != null)
				{
					GarrAbilityRec record5 = StaticDB.garrAbilityDB.GetRecord(record4.GarrAbilityID);
					if (record5 != null)
					{
						this.missionTypeText.gameObject.SetActive(true);
						this.m_missionTypePulseRoot.gameObject.SetActive(record.EnvGarrMechanicID == 139);
						this.m_missionTypeIcon.sprite = Singleton<MissionTypeIconLoader>.Instance.GetMissionTypeIconByMissionID(garrMissionID);
						this.missionTypeText.text = string.Concat(new string[]
						{
							"<color=#ffff00ff>",
							MissionDetailView.m_typeText,
							": </color><color=#ffffffff>",
							record5.Name,
							"</color>"
						});
					}
				}
			}
			Sprite sprite = GeneralHelpers.LoadCurrencyIcon(1560);
			if (sprite != null)
			{
				this.m_resourceIcon_MissionCost.sprite = sprite;
			}
			if (this.missionFollowerSlotGroup != null)
			{
				for (int n = 0; n < (int)record.MaxFollowers; n++)
				{
					GameObject gameObject3 = Object.Instantiate<GameObject>(this.missionFollowerSlotPrefab);
					gameObject3.transform.SetParent(this.missionFollowerSlotGroup.transform, false);
					MissionFollowerSlot component3 = gameObject3.GetComponent<MissionFollowerSlot>();
					component3.m_missionDetailView = this;
					component3.m_enemyPortraitsGroup = this.enemyPortraitsGroup;
				}
			}
			if (!this.m_isCombatAlly && record.UiTextureKitID > 0)
			{
				UiTextureKitRec record6 = StaticDB.uiTextureKitDB.GetRecord((int)record.UiTextureKitID);
				this.m_scrollingEnvironment_Back.enabled = false;
				this.m_scrollingEnvironment_Mid.enabled = false;
				this.m_scrollingEnvironment_Fore.enabled = false;
				int uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("_" + record6.KitPrefix + "-Back");
				if (uitextureAtlasMemberID > 0)
				{
					Sprite atlasSprite = TextureAtlas.instance.GetAtlasSprite(uitextureAtlasMemberID);
					if (atlasSprite != null)
					{
						this.m_scrollingEnvironment_Back.enabled = true;
						this.m_scrollingEnvironment_Back.sprite = atlasSprite;
					}
					else
					{
						Debug.Log("Missing expected Back sprite from UiTextureKitID: [" + record.UiTextureKitID.ToString() + "]");
					}
				}
				int uitextureAtlasMemberID2 = TextureAtlas.GetUITextureAtlasMemberID("_" + record6.KitPrefix + "-Mid");
				if (uitextureAtlasMemberID2 > 0)
				{
					Sprite atlasSprite2 = TextureAtlas.instance.GetAtlasSprite(uitextureAtlasMemberID2);
					if (atlasSprite2 != null)
					{
						this.m_scrollingEnvironment_Mid.enabled = true;
						this.m_scrollingEnvironment_Mid.sprite = atlasSprite2;
					}
					else
					{
						Debug.Log("Missing expected Mid sprite from UiTextureKitID: [" + record.UiTextureKitID.ToString() + "]");
					}
				}
				int uitextureAtlasMemberID3 = TextureAtlas.GetUITextureAtlasMemberID("_" + record6.KitPrefix + "-Fore");
				if (uitextureAtlasMemberID3 > 0)
				{
					Sprite atlasSprite3 = TextureAtlas.instance.GetAtlasSprite(uitextureAtlasMemberID3);
					if (atlasSprite3 != null)
					{
						this.m_scrollingEnvironment_Fore.enabled = true;
						this.m_scrollingEnvironment_Fore.sprite = atlasSprite3;
					}
					else
					{
						Debug.Log("Missing expected Fore sprite from UiTextureKitID: [" + record.UiTextureKitID.ToString() + "]");
					}
				}
			}
			else if ((record.Flags & 16u) == 0u)
			{
			}
			this.UpdateMissionStatus();
			if (this.m_lootGroupObj == null || this.m_missionRewardDisplayPrefab == null)
			{
				return;
			}
			MissionRewardDisplay[] componentsInChildren5 = this.m_lootGroupObj.GetComponentsInChildren<MissionRewardDisplay>(true);
			for (int num2 = 0; num2 < componentsInChildren5.Length; num2++)
			{
				if (componentsInChildren5[num2] != null)
				{
					Object.Destroy(componentsInChildren5[num2].gameObject);
				}
			}
			if (this.m_previewLootGroup != null)
			{
				MissionRewardDisplay[] componentsInChildren6 = this.m_previewLootGroup.GetComponentsInChildren<MissionRewardDisplay>(true);
				for (int num3 = 0; num3 < componentsInChildren6.Length; num3++)
				{
					if (componentsInChildren6[num3] != null)
					{
						Object.Destroy(componentsInChildren6[num3].gameObject);
					}
				}
			}
			if (!PersistentMissionData.missionDictionary.ContainsKey(this.m_currentGarrMissionID))
			{
				return;
			}
			MissionRewardDisplay.InitMissionRewards(this.m_missionRewardDisplayPrefab.gameObject, this.m_lootGroupObj.transform, wrapperGarrisonMission.Rewards);
			if (this.m_previewLootGroup != null)
			{
				MissionRewardDisplay.InitMissionRewards(this.m_missionRewardDisplayPrefab.gameObject, this.m_previewLootGroup.transform, wrapperGarrisonMission.Rewards);
			}
			this.InitBonusRewardDisplay(record, this.m_percentChance);
		}

		public void UpdateMissionStatus()
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(this.m_currentGarrMissionID);
			if (record == null)
			{
				return;
			}
			if ((record.Flags & 16u) == 0u)
			{
				if (this.enemyPortraitsGroup != null)
				{
					MissionMechanic[] componentsInChildren = this.enemyPortraitsGroup.GetComponentsInChildren<MissionMechanic>(true);
					if (componentsInChildren == null)
					{
						return;
					}
					AbilityDisplay[] componentsInChildren2 = this.enemyPortraitsGroup.GetComponentsInChildren<AbilityDisplay>(true);
					if (componentsInChildren2 == null)
					{
						return;
					}
					MissionMechanicTypeCounter[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<MissionMechanicTypeCounter>(true);
					if (componentsInChildren3 == null)
					{
						return;
					}
					int num = (componentsInChildren3.Length <= 0) ? 1 : componentsInChildren3.Length;
					bool[] array = new bool[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = false;
					}
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						bool isCountered = false;
						for (int k = 0; k < componentsInChildren3.Length; k++)
						{
							componentsInChildren3[k].usedIcon.gameObject.SetActive(false);
							if (componentsInChildren3[k].countersMissionMechanicTypeID == componentsInChildren[j].m_missionMechanicTypeID && !array[k])
							{
								isCountered = true;
								array[k] = true;
								break;
							}
						}
						componentsInChildren[j].SetCountered(isCountered, false, true);
						if (j < componentsInChildren2.Length)
						{
							componentsInChildren2[j].SetCountered(isCountered, true);
						}
					}
					if (record.EnvGarrMechanicID == 139)
					{
						bool flag = false;
						MissionFollowerSlot[] componentsInChildren4 = base.gameObject.GetComponentsInChildren<MissionFollowerSlot>();
						foreach (MissionFollowerSlot missionFollowerSlot in componentsInChildren4)
						{
							if (missionFollowerSlot.CountersStealth())
							{
								if (!this.m_missionTypeCounteredObject.activeSelf && this.missionTypeText.gameObject.activeSelf)
								{
									UiAnimMgr.instance.PlayAnim("RedFailX", this.m_missionTypeCounteredObject.transform, Vector3.zero, 0.8f, 0f);
								}
								flag = true;
							}
						}
						this.m_missionTypeCounteredObject.SetActive(flag);
						this.m_missionTypePulseRoot.SetActive(!flag);
						if (!flag)
						{
							float alpha = 0.6f;
							Color color = Color.white;
							bool flag2 = false;
							MissionFollowerSlot[] componentsInChildren5 = this.missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
							foreach (MissionFollowerSlot missionFollowerSlot2 in componentsInChildren5)
							{
								int currentGarrFollowerID = missionFollowerSlot2.GetCurrentGarrFollowerID();
								if (currentGarrFollowerID != 0)
								{
									if (PersistentFollowerData.followerDictionary.ContainsKey(currentGarrFollowerID) && (PersistentFollowerData.followerDictionary[currentGarrFollowerID].Flags & 8) != 0)
									{
										flag2 = true;
									}
								}
							}
							if (flag2)
							{
								alpha = 1f;
								color = Color.red;
							}
							this.m_missionTypePulseRoot.GetComponentInChildren<Image>().color = color;
							this.m_missionTypePulseRoot.GetComponentInChildren<CanvasGroup>().alpha = alpha;
						}
					}
				}
				MissionFollowerSlot[] componentsInChildren6 = base.gameObject.GetComponentsInChildren<MissionFollowerSlot>(true);
				List<WrapperGarrisonFollower> list = new List<WrapperGarrisonFollower>();
				for (int n = 0; n < componentsInChildren6.Length; n++)
				{
					int currentGarrFollowerID2 = componentsInChildren6[n].GetCurrentGarrFollowerID();
					if (PersistentFollowerData.followerDictionary.ContainsKey(currentGarrFollowerID2))
					{
						WrapperGarrisonFollower item = PersistentFollowerData.followerDictionary[currentGarrFollowerID2];
						list.Add(item);
					}
				}
				LegionCompanionWrapper.EvaluateMission(this.m_currentGarrMissionID, (from f in list
				select f.GarrFollowerID).ToList<int>());
				if (this.missionPercentChanceText != null)
				{
					this.missionPercentChanceText.text = "%";
					this.m_missionChanceSpinner.SetActive(true);
				}
				if (this.m_partyBuffGroup != null)
				{
					AbilityDisplay[] componentsInChildren7 = this.m_partyBuffGroup.GetComponentsInChildren<AbilityDisplay>(true);
					foreach (AbilityDisplay abilityDisplay in componentsInChildren7)
					{
						Object.Destroy(abilityDisplay.gameObject);
					}
				}
				if (this.m_partyDebuffGroup != null)
				{
					AbilityDisplay[] componentsInChildren8 = this.m_partyDebuffGroup.GetComponentsInChildren<AbilityDisplay>(true);
					foreach (AbilityDisplay abilityDisplay2 in componentsInChildren8)
					{
						Object.Destroy(abilityDisplay2.gameObject);
					}
				}
				int adjustedMissionDuration = GeneralHelpers.GetAdjustedMissionDuration(record, list, this.enemyPortraitsGroup);
				List<int> list2 = new List<int>();
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				MissionFollowerSlot[] componentsInChildren9 = this.missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
				foreach (MissionFollowerSlot missionFollowerSlot3 in componentsInChildren9)
				{
					int currentGarrFollowerID3 = missionFollowerSlot3.GetCurrentGarrFollowerID();
					if (currentGarrFollowerID3 != 0)
					{
						int[] buffsForCurrentMission = GeneralHelpers.GetBuffsForCurrentMission(currentGarrFollowerID3, this.m_currentGarrMissionID, this.missionFollowerSlotGroup, adjustedMissionDuration);
						num4 += buffsForCurrentMission.Length;
						foreach (int num9 in buffsForCurrentMission)
						{
							list2.Add(num9);
							GameObject gameObject = Object.Instantiate<GameObject>(this.m_mechanicEffectDisplayPrefab);
							gameObject.transform.SetParent(this.m_partyBuffGroup.transform, false);
							AbilityDisplay component = gameObject.GetComponent<AbilityDisplay>();
							component.SetAbility(num9, false, false, null);
						}
						if (PersistentFollowerData.followerDictionary.ContainsKey(currentGarrFollowerID3) && (PersistentFollowerData.followerDictionary[currentGarrFollowerID3].Flags & 8) == 0)
						{
							num6++;
						}
					}
				}
				if (num4 > 8)
				{
					this.m_partyBuffsText.text = string.Empty;
				}
				else
				{
					this.m_partyBuffsText.text = StaticDB.GetString("PARTY_BUFFS", null);
				}
				if (this.m_partyBuffGroup != null)
				{
					HorizontalLayoutGroup component2 = this.m_partyBuffGroup.GetComponent<HorizontalLayoutGroup>();
					if (component2 != null)
					{
						if (num4 > 10 && Main.instance.IsNarrowScreen())
						{
							component2.spacing = 3f;
						}
						else
						{
							component2.spacing = 6f;
						}
					}
					this.m_partyBuffGroup.SetActive(num4 > 0);
				}
				if (this.m_partyDebuffGroup != null)
				{
					this.m_partyDebuffGroup.SetActive(num5 > 0);
				}
				int trueMissionCost = this.GetTrueMissionCost(record, list2);
				this.missionCostText.text = GarrisonStatus.WarResources().ToString("N0", MobileDeviceLocale.GetCultureInfoLocale()) + " / " + trueMissionCost.ToString("N0", MobileDeviceLocale.GetCultureInfoLocale());
				int numActiveChampions = GeneralHelpers.GetNumActiveChampions();
				int maxActiveFollowers = GarrisonStatus.GetMaxActiveFollowers();
				this.m_isOverMaxChampionSoftCap = false;
				this.m_needMoreResources = false;
				this.m_needAtLeastOneChampion = false;
				if (numActiveChampions > maxActiveFollowers)
				{
					this.m_isOverMaxChampionSoftCap = true;
				}
				if (GarrisonStatus.WarResources() < trueMissionCost)
				{
					this.m_needMoreResources = true;
				}
				if (num6 < 1)
				{
					this.m_needAtLeastOneChampion = true;
				}
				if (this.m_needMoreResources)
				{
					this.missionCostText.color = Color.red;
				}
				else
				{
					this.missionCostText.color = Color.white;
				}
				bool flag3 = !this.m_isOverMaxChampionSoftCap && !this.m_needMoreResources && !this.m_needAtLeastOneChampion;
				if (flag3)
				{
					this.m_startMissionButton.material.SetFloat("_GrayscaleAmount", 0f);
					this.m_startMissionButtonText.color = new Color(1f, 0.8588f, 0f, 1f);
					MeshGradient component3 = this.m_startMissionButtonText.GetComponent<MeshGradient>();
					component3.enabled = true;
				}
				else
				{
					this.m_startMissionButton.material.SetFloat("_GrayscaleAmount", 1f);
					this.m_startMissionButtonText.color = Color.gray;
					MeshGradient component4 = this.m_startMissionButtonText.GetComponent<MeshGradient>();
					component4.enabled = false;
				}
				TimeSpan timeSpan = TimeSpan.FromSeconds((double)adjustedMissionDuration);
				if (this.missionLocationText != null)
				{
					this.missionLocationText.text = string.Concat(new object[]
					{
						StaticDB.GetString("XP", "XP:"),
						" ",
						record.BaseFollowerXP,
						" (<color=#ff8600ff>",
						timeSpan.GetDurationString(false, TimeUnit.Second),
						"</color>)"
					});
				}
				if (this.m_previewMissionTimeText != null)
				{
					this.m_previewMissionTimeText.text = string.Concat(new string[]
					{
						"Hoopy <color=#ffff00ff>",
						MissionDetailView.m_timeText,
						": </color><color=#ff8600ff>",
						timeSpan.GetDurationString(false, TimeUnit.Second),
						"</color>"
					});
				}
				return;
			}
			MissionFollowerSlot componentInChildren = base.gameObject.GetComponentInChildren<MissionFollowerSlot>(true);
			if (componentInChildren == null)
			{
				return;
			}
			if (componentInChildren.IsOccupied())
			{
				if (this.missionDescriptionText != null)
				{
					this.missionDescriptionText.gameObject.SetActive(false);
				}
				this.m_combatAllyThisIsWhatYouGetText.gameObject.SetActive(true);
				this.m_combatAllySupportSpellDisplay.gameObject.SetActive(true);
				int currentGarrFollowerID4 = componentInChildren.GetCurrentGarrFollowerID();
				int zoneSupportSpellID = PersistentFollowerData.followerDictionary[currentGarrFollowerID4].ZoneSupportSpellID;
				this.m_combatAllySupportSpellDisplay.SetSpell(zoneSupportSpellID);
			}
			else
			{
				if (this.missionDescriptionText != null)
				{
					this.missionDescriptionText.gameObject.SetActive(true);
				}
				this.m_combatAllyThisIsWhatYouGetText.gameObject.SetActive(false);
				this.m_combatAllySupportSpellDisplay.gameObject.SetActive(false);
			}
		}

		public void OnPartyBuffSectionTapped()
		{
			List<int> list = new List<int>();
			MissionFollowerSlot[] componentsInChildren = this.missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
			List<WrapperGarrisonFollower> list2 = new List<WrapperGarrisonFollower>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				int currentGarrFollowerID = componentsInChildren[i].GetCurrentGarrFollowerID();
				if (PersistentFollowerData.followerDictionary.ContainsKey(currentGarrFollowerID))
				{
					WrapperGarrisonFollower item = PersistentFollowerData.followerDictionary[currentGarrFollowerID];
					list2.Add(item);
				}
			}
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(this.m_currentGarrMissionID);
			if (record == null)
			{
				return;
			}
			int adjustedMissionDuration = GeneralHelpers.GetAdjustedMissionDuration(record, list2, this.enemyPortraitsGroup);
			foreach (MissionFollowerSlot missionFollowerSlot in componentsInChildren)
			{
				int currentGarrFollowerID2 = missionFollowerSlot.GetCurrentGarrFollowerID();
				if (currentGarrFollowerID2 != 0)
				{
					int[] buffsForCurrentMission = GeneralHelpers.GetBuffsForCurrentMission(currentGarrFollowerID2, this.m_currentGarrMissionID, this.missionFollowerSlotGroup, adjustedMissionDuration);
					foreach (int item2 in buffsForCurrentMission)
					{
						list.Add(item2);
					}
				}
			}
			AllPopups.instance.ShowPartyBuffsPopup(list.ToArray());
		}

		private void OnMissionSuccessChanceChanged(int newChance)
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(this.m_currentGarrMissionID);
			if (record == null)
			{
				return;
			}
			if ((record.Flags & 16u) != 0u)
			{
				return;
			}
			this.m_missionChanceSpinner.SetActive(false);
			this.missionPercentChanceText.text = newChance + "%";
			if (newChance < 0)
			{
				this.missionPercentChanceText.color = Color.red;
				this.missionPercentChanceText.GetComponentInParent<MeshGradient>().enabled = false;
			}
			else
			{
				this.missionPercentChanceText.color = Color.green;
				this.missionPercentChanceText.GetComponentInParent<MeshGradient>().enabled = true;
			}
			if (this.m_percentChance < 100 && newChance >= 100)
			{
				Main.instance.m_UISound.Play_100Percent();
			}
			else if (this.m_percentChance < 200 && newChance >= 200)
			{
				Main.instance.m_UISound.Play_200Percent();
			}
			else if (newChance > this.m_percentChance)
			{
			}
			this.m_bonusLootChanceText.text = "<color=#ff9600ff>" + Math.Max(0, newChance - 100) + "%</color>";
			this.m_percentChance = newChance;
		}

		private void InitBonusRewardDisplay(GarrMissionRec garrMissionRec, int missionSuccessChance)
		{
			this.m_bonusLootDisplay.SetActive(false);
			if (garrMissionRec == null)
			{
				return;
			}
			if (StaticDB.rewardPackDB.GetRecord(garrMissionRec.OvermaxRewardPackID) == null)
			{
				return;
			}
			if (PersistentMissionData.missionDictionary.ContainsKey(garrMissionRec.ID))
			{
				WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[garrMissionRec.ID];
				if (garrMissionRec.OvermaxRewardPackID > 0 && wrapperGarrisonMission.OvermaxRewards.Count > 0)
				{
					this.m_bonusLootDisplay.SetActive(true);
					this.m_bonusLootChanceText.text = string.Concat(new object[]
					{
						"<color=#ffff00ff>",
						StaticDB.GetString("BONUS", "Bonus:"),
						" </color>\n<color=#ff8600ff>",
						Math.Max(0, missionSuccessChance - 100),
						"%</color>"
					});
					if (PersistentMissionData.missionDictionary.ContainsKey(this.m_currentGarrMissionID) && wrapperGarrisonMission.OvermaxRewards.Count > 0)
					{
						WrapperGarrisonMissionReward wrapperGarrisonMissionReward = wrapperGarrisonMission.OvermaxRewards[0];
						if (wrapperGarrisonMissionReward.ItemID > 0)
						{
							this.m_bonusMissionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.item, wrapperGarrisonMissionReward.ItemID, (int)wrapperGarrisonMissionReward.ItemQuantity, 0, wrapperGarrisonMissionReward.ItemFileDataID);
						}
						else if (wrapperGarrisonMissionReward.FollowerXP > 0u)
						{
							this.m_bonusMissionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.followerXP, 0, (int)wrapperGarrisonMissionReward.FollowerXP, 0, 0);
						}
						else if (wrapperGarrisonMissionReward.CurrencyQuantity > 0u)
						{
							if (wrapperGarrisonMissionReward.CurrencyType == 0)
							{
								this.m_bonusMissionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.gold, 0, (int)(wrapperGarrisonMissionReward.CurrencyQuantity / 10000u), 0, 0);
							}
							else
							{
								CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(wrapperGarrisonMissionReward.CurrencyType);
								int rewardQuantity = (int)((ulong)wrapperGarrisonMissionReward.CurrencyQuantity / (ulong)(((record.Flags & 8u) == 0u) ? 1L : 100L));
								this.m_bonusMissionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.currency, wrapperGarrisonMissionReward.CurrencyType, rewardQuantity, 0, 0);
							}
						}
					}
				}
			}
		}

		public void AssignCombatAlly()
		{
			this.StartMission();
		}

		public void StartMission()
		{
			if (this.m_isOverMaxChampionSoftCap || this.m_needMoreResources || this.m_needAtLeastOneChampion)
			{
				if (this.m_isOverMaxChampionSoftCap)
				{
					AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("TOO_MANY_CHAMPIONS", null));
				}
				else if (this.m_needAtLeastOneChampion)
				{
					AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("NEED_A_CHAMPION", null));
				}
				else if (this.m_needMoreResources)
				{
					AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("NEED_MORE_RESOURCES", null));
				}
				return;
			}
			Main.instance.m_UISound.Play_StartMission();
			IEnumerable<ulong> followerDBIDs = from slot in base.gameObject.GetComponentsInChildren<MissionFollowerSlot>(true)
			where PersistentFollowerData.followerDictionary.ContainsKey(slot.GetCurrentGarrFollowerID())
			select PersistentFollowerData.followerDictionary[slot.GetCurrentGarrFollowerID()].DbID;
			Singleton<GarrisonWrapper>.Instance.StartMission(this.m_currentGarrMissionID, followerDBIDs);
			AdventureMapPanel.instance.SelectMissionFromMap(0);
			AdventureMapPanel.instance.SelectMissionFromList(0);
			AdventureMapPanel.instance.SetSelectedIconContainer(null);
			base.GetComponentInParent<MissionDialog>().gameObject.SetActive(false);
		}

		public CombatAllyMissionState GetCombatAllyMissionState()
		{
			return this.m_combatAllyMissionState;
		}

		public void SetCombatAllyMissionState(CombatAllyMissionState state)
		{
			this.m_combatAllyMissionState = state;
			switch (state)
			{
			case CombatAllyMissionState.notAvailable:
			{
				Text[] componentsInChildren = this.m_combatAllyNotAvailableStuff.GetComponentsInChildren<Text>(true);
				if (componentsInChildren[0] != null)
				{
					componentsInChildren[0].text = StaticDB.GetString("COMBAT_ALLY_UNAVAILABLE", null);
				}
				this.m_combatAllyNotAvailableStuff.SetActive(true);
				this.m_combatAllyAvailableStuff.SetActive(false);
				break;
			}
			case CombatAllyMissionState.available:
				this.m_combatAllyNotAvailableStuff.SetActive(false);
				this.m_combatAllyAvailableStuff.SetActive(true);
				this.m_assignCombatAllyStuff.SetActive(true);
				this.m_unassignCombatAllyStuff.SetActive(false);
				break;
			case CombatAllyMissionState.inProgress:
			{
				this.m_combatAllyNotAvailableStuff.SetActive(false);
				this.m_combatAllyAvailableStuff.SetActive(true);
				int follower = 0;
				foreach (WrapperGarrisonFollower wrapperGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
				{
					if (wrapperGarrisonFollower.CurrentMissionID == this.m_currentGarrMissionID)
					{
						follower = wrapperGarrisonFollower.GarrFollowerID;
						break;
					}
				}
				MissionFollowerSlot[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<MissionFollowerSlot>(true);
				componentsInChildren2[0].SetFollower(follower);
				this.m_assignCombatAllyStuff.SetActive(false);
				this.m_unassignCombatAllyStuff.SetActive(true);
				break;
			}
			}
		}

		public void UnassignCombatAlly()
		{
			Singleton<GarrisonWrapper>.Instance.CompleteMission(this.m_currentGarrMissionID);
			this.SetCombatAllyMissionState(CombatAllyMissionState.available);
			MissionFollowerSlot[] componentsInChildren = base.gameObject.GetComponentsInChildren<MissionFollowerSlot>(true);
			componentsInChildren[0].SetFollower(0);
		}

		public int GetCurrentMissionID()
		{
			return this.m_currentGarrMissionID;
		}

		public static float ComputeFollowerBias(WrapperGarrisonFollower follower, int followerLevel, int followerItemLevel, int garrMissionID)
		{
			bool flag = (follower.Flags & 8) != 0;
			if (flag)
			{
				return 0f;
			}
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
			if (record == null)
			{
				return 0f;
			}
			float num = MissionDetailView.ComputeFollowerLevelBias(follower, followerLevel, (int)record.TargetLevel);
			num += MissionDetailView.ComputeFollowerItemLevelBias(follower, followerItemLevel, (int)record.GarrFollowerTypeID, (int)record.TargetItemLevel);
			return Mathf.Clamp(num, -1f, 1f);
		}

		public static int GarrisonFollower_GetMaxFollowerLevel(int garrFollowerTypeID)
		{
			return (int)StaticDB.garrFollowerLevelXPDB.GetRecordsWhere((GarrFollowerLevelXPRec rec) => (int)rec.GarrFollowerTypeID == garrFollowerTypeID).Max((GarrFollowerLevelXPRec rec) => rec.FollowerLevel);
		}

		private static float ComputeFollowerItemLevelBias(WrapperGarrisonFollower follower, int followerItemLevel, int garrFollowerTypeID, int targetItemLevel)
		{
			float num = 0f;
			if (follower.FollowerLevel == MissionDetailView.GarrisonFollower_GetMaxFollowerLevel(garrFollowerTypeID))
			{
				int num2;
				int num3;
				MissionDetailView.GarrisonFollower_GetFollowerBiasConstants(follower, out num2, out num3);
				int num4 = (targetItemLevel <= 0) ? 600 : targetItemLevel;
				float num5 = (float)(followerItemLevel - num4);
				num5 /= (float)num3;
				num += num5;
			}
			return num;
		}

		private static float ComputeFollowerLevelBias(WrapperGarrisonFollower follower, int followerLevel, int targetLevel)
		{
			int num;
			int num2;
			MissionDetailView.GarrisonFollower_GetFollowerBiasConstants(follower, out num, out num2);
			float num3 = (float)(followerLevel - targetLevel);
			return num3 / (float)num;
		}

		private static float ComputeBiasValue(float par, float max, float bias)
		{
			if (bias < 0f)
			{
				return par + bias * par;
			}
			return par + bias * (max - par);
		}

		private static void GarrisonFollower_GetFollowerBiasConstants(WrapperGarrisonFollower follower, out int out_levelRangeBias, out int out_itemLevelRangeBias)
		{
			out_levelRangeBias = 1;
			out_itemLevelRangeBias = 1;
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
			if (record == null)
			{
				return;
			}
			GarrFollowerTypeRec record2 = StaticDB.garrFollowerTypeDB.GetRecord((int)record.GarrFollowerTypeID);
			if (record2 == null)
			{
				return;
			}
			out_levelRangeBias = (int)((record2.LevelRangeBias >= 1) ? record2.LevelRangeBias : 1);
			out_itemLevelRangeBias = (int)((record2.ItemLevelRangeBias >= 1) ? record2.ItemLevelRangeBias : 1);
		}

		public void NotifyFollowerSlotsChanged()
		{
			if (this.FollowerSlotsChangedAction != null)
			{
				this.FollowerSlotsChangedAction();
			}
		}

		public void NarrowScreenAdjust()
		{
			GridLayoutGroup component = this.m_EnemiesGroup.GetComponent<GridLayoutGroup>();
			if (component != null)
			{
				Vector2 spacing = component.spacing;
				spacing.x = 20f;
				component.spacing = spacing;
			}
			component = this.m_FollowerSlotGroup.GetComponent<GridLayoutGroup>();
			if (component != null)
			{
				Vector2 spacing2 = component.spacing;
				spacing2.x = 20f;
				component.spacing = spacing2;
			}
		}

		public void OpenMissionTypeDialog()
		{
			MissionTypeDialog missionTypeDialog = Singleton<DialogFactory>.instance.CreateMissionTypeDialog();
			missionTypeDialog.InitializeMissionDialog(this.m_currentGarrMissionID, Singleton<MissionTypeIconLoader>.Instance.GetMissionTypeIconByMissionID(this.m_currentGarrMissionID));
		}

		public void OpenMissionInfoDialog()
		{
			MissionInfoDialog missionInfoDialog = Singleton<DialogFactory>.instance.CreateMissionInfoDialog();
			missionInfoDialog.InitializeDialog(this.m_currentGarrMissionID);
		}

		[Header("Main Mission Display")]
		public Text missionNameText;

		public Text missionLocationText;

		public Image missionTypeImage;

		public Text missioniLevelText;

		public Text missionTypeText;

		public Image m_missionTypeIcon;

		public GameObject m_missionTypeCounteredObject;

		public GameObject m_missionTypePulseRoot;

		public Text missionDescriptionText;

		public GameObject missionFollowerSlotGroup;

		public GameObject enemyPortraitsGroup;

		public GameObject missionFollowerSlotPrefab;

		public GameObject missionEncounterPrefab;

		public GameObject missionMechanicPrefab;

		public MissionMechanic missionEnvironmentMechanic;

		public GameObject treasureChestHorde;

		public GameObject treasureChestAlliance;

		public Text missionPercentChanceText;

		public GameObject m_missionChanceSpinner;

		public Text missionCostText;

		public Image m_scrollingEnvironment_Back;

		public Image m_scrollingEnvironment_Mid;

		public Image m_scrollingEnvironment_Fore;

		public GameObject m_lootGroupObj;

		public MissionRewardDisplay m_missionRewardDisplayPrefab;

		public GameObject m_mechanicEffectDisplayPrefab;

		public Text m_resourcesDescText;

		public Image m_startMissionButton;

		public Text m_startMissionButtonText;

		public Text m_costDescText;

		public GameObject m_partyBuffGroup;

		public GameObject m_partyDebuffGroup;

		public Image m_resourceIcon_MissionCost;

		[Header("Combat Ally")]
		public bool m_isCombatAlly;

		public GameObject m_combatAllyAvailableStuff;

		public GameObject m_assignCombatAllyStuff;

		public GameObject m_unassignCombatAllyStuff;

		public GameObject m_combatAllyNotAvailableStuff;

		public Text m_combatAllyThisIsWhatYouGetText;

		public SpellDisplay m_combatAllySupportSpellDisplay;

		[Header("Bonus Loot")]
		public GameObject m_bonusLootDisplay;

		public Text m_bonusLootChanceText;

		public MissionRewardDisplay m_bonusMissionRewardDisplay;

		[Header("Mission Preview")]
		public GameObject m_previewView;

		public Text m_previewMissionNameText;

		public Text m_previewMissionLocationText;

		public Image m_previewMissionTypeImage;

		public Text m_previewMissioniLevelText;

		public Text m_previewMissionTimeText;

		public GameObject m_previewMechanicsGroup;

		public GameObject m_previewMechanicEffectPrefab;

		public GameObject m_previewLootGroup;

		public Transform m_previewSlideUpHintEffectRoot;

		[Header("Misc")]
		public CanvasGroup m_topLevelDetailViewCanvasGroup;

		public MissionPanelSlider m_missionPanelSlider;

		public Shader m_grayscaleShader;

		public bool m_usedForMissionList;

		private int m_currentGarrMissionID;

		private static string m_timeText;

		private static string m_typeText;

		private static string m_iLevelText;

		public Text m_partyBuffsText;

		public Text m_partyDebuffsText;

		private CombatAllyMissionState m_combatAllyMissionState;

		private bool m_isOverMaxChampionSoftCap;

		private bool m_needMoreResources;

		private bool m_needAtLeastOneChampion;

		private int m_percentChance;

		public Action FollowerSlotsChangedAction;

		[Header("Notched Screen")]
		public RectTransform m_DarkBG;

		public GameObject m_LevelBG;

		public GameObject m_MissionTypeImage;

		public GameObject m_MissionNameText;

		public GameObject m_MissionLocationText;

		public GameObject m_CloseButton;

		public GameObject m_bottomBar;

		[Header("Narrow Screen")]
		public GameObject m_EnemiesGroup;

		public GameObject m_FollowerSlotGroup;

		private const int HidePartyBuffTextCount = 8;
	}
}
