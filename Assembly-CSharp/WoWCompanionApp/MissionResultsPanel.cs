using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionResultsPanel : MonoBehaviour
	{
		private void Awake()
		{
			this.m_bonusLootInitialLocalPosition = this.m_bonusMissionRewardDisplay.m_rewardIcon.transform.localPosition;
			this.m_attemptedAutoComplete = false;
			this.m_darknessBG.SetActive(false);
			this.m_popupView.SetActive(false);
			if (this.m_partyBuffsText != null)
			{
				this.m_partyBuffsText.font = GeneralHelpers.LoadStandardFont();
				this.m_partyBuffsText.text = StaticDB.GetString("PARTY_BUFFS", null);
			}
			if (this.m_bonusLootChanceText != null)
			{
				this.m_bonusLootChanceText.font = GeneralHelpers.LoadStandardFont();
			}
		}

		private void Start()
		{
			if (Main.instance.IsNarrowScreen())
			{
				this.NarrowScreenAdjust();
			}
		}

		private void OnEnable()
		{
			this.m_missionSuccessMessage.SetActive(false);
			this.m_missionFailMessage.SetActive(false);
			this.m_missionInProgressMessage.SetActive(false);
			this.m_bonusMissionRewardDisplay.m_rewardIcon.transform.localPosition = this.m_bonusLootInitialLocalPosition;
			if (AdventureMapPanel.instance != null)
			{
				AdventureMapPanel instance = AdventureMapPanel.instance;
				instance.ShowMissionResultAction = (Action<int, int, bool>)Delegate.Combine(instance.ShowMissionResultAction, new Action<int, int, bool>(this.ShowMissionResults));
			}
			Singleton<GarrisonWrapper>.Instance.MissionSuccessChanceChangedAction += this.OnMissionSuccessChanceChanged;
			Singleton<GarrisonWrapper>.Instance.FollowerDataChangedAction += this.HandleFollowerDataChanged;
			this.m_okButtonText.text = StaticDB.GetString("OK", null);
			this.m_inProgressText.text = StaticDB.GetString("IN_PROGRESS", null);
			this.m_successText.text = StaticDB.GetString("MISSION_SUCCESS", null);
			this.m_failureText.text = StaticDB.GetString("MISSION_FAILED", null);
		}

		private void OnDisable()
		{
			Singleton<GarrisonWrapper>.Instance.MissionSuccessChanceChangedAction -= this.OnMissionSuccessChanceChanged;
			Singleton<GarrisonWrapper>.Instance.FollowerDataChangedAction -= this.HandleFollowerDataChanged;
			if (AdventureMapPanel.instance != null)
			{
				AdventureMapPanel instance = AdventureMapPanel.instance;
				instance.ShowMissionResultAction = (Action<int, int, bool>)Delegate.Remove(instance.ShowMissionResultAction, new Action<int, int, bool>(this.ShowMissionResults));
			}
		}

		private void UpdateMissionRemainingTimeDisplay()
		{
			if (!this.m_missionInProgressMessage.activeSelf)
			{
				return;
			}
			TimeSpan t = GarrisonStatus.CurrentTime() - this.m_missionStartedTime;
			TimeSpan timeSpan = this.m_missionDurationInSeconds - t;
			bool flag = timeSpan.TotalSeconds < 0.0 && this.m_popupView.gameObject.activeSelf;
			timeSpan = ((timeSpan.TotalSeconds <= 0.0) ? TimeSpan.Zero : timeSpan);
			this.m_missionTimeRemainingText.text = timeSpan.GetDurationString(false, TimeUnit.Second);
			if (flag && !this.m_attemptedAutoComplete)
			{
				if (AdventureMapPanel.instance.ShowMissionResultAction != null)
				{
					AdventureMapPanel.instance.ShowMissionResultAction(this.m_garrMissionID, 1, false);
				}
				Singleton<GarrisonWrapper>.Instance.CompleteMission(this.m_garrMissionID);
				this.m_attemptedAutoComplete = true;
			}
		}

		private void Update()
		{
			this.UpdateMissionRemainingTimeDisplay();
			if (!this.m_followerExperienceDisplayArea.activeSelf && (this.m_currentResultType == MissionResultType.success || this.m_currentResultType == MissionResultType.failure))
			{
				this.m_timeUntilFadeOutMissionDetailsDisplay -= Time.deltaTime;
				if (this.m_timeUntilFadeOutMissionDetailsDisplay < 0f)
				{
					this.m_missionResultsDisplayCanvasGroupAutoFadeOut.EnableFadeOut();
				}
				this.m_timeUntilShowFollowerExperienceDisplays -= Time.deltaTime;
				if (this.m_timeUntilShowFollowerExperienceDisplays < 0f)
				{
					this.m_followerExperienceDisplayArea.SetActive(true);
				}
			}
		}

		public void UpdateMissionStatus(int garrMissionID)
		{
			MissionMechanic[] componentsInChildren = this.enemyPortraitsGroup.GetComponentsInChildren<MissionMechanic>(true);
			if (componentsInChildren == null)
			{
				return;
			}
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetCountered(false, false, true);
			}
			AbilityDisplay[] componentsInChildren2 = this.enemyPortraitsGroup.GetComponentsInChildren<AbilityDisplay>(true);
			if (componentsInChildren2 == null)
			{
				return;
			}
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].SetCountered(false, true);
			}
			MissionMechanicTypeCounter[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<MissionMechanicTypeCounter>(true);
			if (componentsInChildren3 == null)
			{
				return;
			}
			for (int k = 0; k < componentsInChildren3.Length; k++)
			{
				componentsInChildren3[k].usedIcon.gameObject.SetActive(false);
				for (int l = 0; l < componentsInChildren.Length; l++)
				{
					if (componentsInChildren3[k].countersMissionMechanicTypeID == componentsInChildren[l].m_missionMechanicTypeID && !componentsInChildren[l].IsCountered())
					{
						componentsInChildren[l].SetCountered(true, false, false);
						if (l < componentsInChildren2.Length)
						{
							componentsInChildren2[l].SetCountered(true, false);
						}
						break;
					}
				}
			}
			MissionFollowerSlot[] componentsInChildren4 = base.gameObject.GetComponentsInChildren<MissionFollowerSlot>(true);
			List<WrapperGarrisonFollower> list = new List<WrapperGarrisonFollower>();
			for (int m = 0; m < componentsInChildren4.Length; m++)
			{
				int currentGarrFollowerID = componentsInChildren4[m].GetCurrentGarrFollowerID();
				if (PersistentFollowerData.followerDictionary.ContainsKey(currentGarrFollowerID))
				{
					WrapperGarrisonFollower item = PersistentFollowerData.followerDictionary[currentGarrFollowerID];
					list.Add(item);
				}
			}
			int chance = -1000;
			if (MissionDataCache.missionDataDictionary.ContainsKey(this.m_garrMissionID))
			{
				chance = MissionDataCache.missionDataDictionary[this.m_garrMissionID];
			}
			else
			{
				LegionCompanionWrapper.EvaluateMission(garrMissionID, (from f in list
				select f.GarrFollowerID).ToList<int>());
			}
			this.OnMissionSuccessChanceChanged(chance);
		}

		private void OnMissionSuccessChanceChanged(int chance)
		{
			if (this.m_garrMissionID == 0)
			{
				return;
			}
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			this.m_bonusLootDisplay.SetActive(false);
			if (chance <= -1000)
			{
				this.missionPercentChanceText.text = "%";
				this.m_missionChanceSpinner.SetActive(true);
			}
			else
			{
				this.missionPercentChanceText.text = chance + "%";
				this.m_missionChanceSpinner.SetActive(false);
			}
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(this.m_garrMissionID);
			if (record == null)
			{
				Debug.LogError("Invalid Mission ID:" + this.m_garrMissionID);
				return;
			}
			if (StaticDB.rewardPackDB.GetRecord(record.OvermaxRewardPackID) == null)
			{
				return;
			}
			if (PersistentMissionData.missionDictionary.ContainsKey(this.m_garrMissionID))
			{
				WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[this.m_garrMissionID];
				if (record.OvermaxRewardPackID > 0 && wrapperGarrisonMission.OvermaxRewards.Count > 0)
				{
					this.m_bonusLootDisplay.SetActive(true);
					this.m_bonusLootChanceText.text = "<color=#ff9600ff>" + Mathf.Max(0, chance - 100) + "%</color>";
					this.m_bonusLootChance = Mathf.Max(0, chance - 100);
				}
			}
		}

		public void ShowMissionResults(int garrMissionID, int missionResultType, bool awardOvermax)
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
			if (record == null)
			{
				return;
			}
			this.m_missionResultsDisplayCanvasGroupAutoFadeOut.Reset();
			this.m_currentResultType = (MissionResultType)missionResultType;
			this.m_followerExperienceDisplayArea.SetActive(false);
			this.m_attemptedAutoComplete = false;
			this.m_garrMissionID = garrMissionID;
			this.m_darknessBG.SetActive(true);
			this.m_popupView.SetActive(true);
			this.m_bonusLootDisplay.SetActive(false);
			if (this.missionFollowerSlotGroup != null)
			{
				MissionFollowerSlot[] componentsInChildren = this.missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] != null && componentsInChildren[i] != this.missionFollowerSlotGroup.transform)
					{
						componentsInChildren[i].gameObject.transform.SetParent(null);
						Object.Destroy(componentsInChildren[i].gameObject);
					}
				}
			}
			MissionEncounter[] componentsInChildren2 = this.enemyPortraitsGroup.GetComponentsInChildren<MissionEncounter>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				if (componentsInChildren2[j] != null && componentsInChildren2[j] != this.enemyPortraitsGroup.transform)
				{
					componentsInChildren2[j].gameObject.transform.SetParent(null);
					Object.Destroy(componentsInChildren2[j].gameObject);
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
			this.m_missionStartedTime = wrapperGarrisonMission.StartTime;
			this.m_missionDurationInSeconds = wrapperGarrisonMission.MissionDuration;
			for (int k = 0; k < wrapperGarrisonMission.Encounters.Count; k++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.missionEncounterPrefab);
				gameObject.transform.SetParent(this.enemyPortraitsGroup.transform, false);
				MissionEncounter component = gameObject.GetComponent<MissionEncounter>();
				int garrMechanicID = (wrapperGarrisonMission.Encounters[k].MechanicIDs.Count <= 0) ? 0 : wrapperGarrisonMission.Encounters[k].MechanicIDs[0];
				component.SetEncounter(wrapperGarrisonMission.Encounters[k].EncounterID, garrMechanicID);
			}
			this.missionNameText.text = record.Name;
			this.missionLocationText.text = record.Location;
			GarrMissionTypeRec record2 = StaticDB.garrMissionTypeDB.GetRecord((int)record.GarrMissionTypeID);
			this.missionTypeImage.overrideSprite = TextureAtlas.instance.GetAtlasSprite((int)record2.UiTextureAtlasMemberID);
			if (this.missionFollowerSlotGroup != null)
			{
				for (int l = 0; l < (int)record.MaxFollowers; l++)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.missionFollowerSlotPrefab);
					gameObject2.transform.SetParent(this.missionFollowerSlotGroup.transform, false);
					MissionFollowerSlot component2 = gameObject2.GetComponent<MissionFollowerSlot>();
					component2.m_enemyPortraitsGroup = this.enemyPortraitsGroup;
				}
			}
			if (record.UiTextureKitID > 0)
			{
				UiTextureKitRec record3 = StaticDB.uiTextureKitDB.GetRecord((int)record.UiTextureKitID);
				this.m_scrollingEnvironment_Back.enabled = false;
				this.m_scrollingEnvironment_Mid.enabled = false;
				this.m_scrollingEnvironment_Fore.enabled = false;
				int uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("_" + record3.KitPrefix + "-Back");
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
				int uitextureAtlasMemberID2 = TextureAtlas.GetUITextureAtlasMemberID("_" + record3.KitPrefix + "-Mid");
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
				int uitextureAtlasMemberID3 = TextureAtlas.GetUITextureAtlasMemberID("_" + record3.KitPrefix + "-Fore");
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
			else
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"DATA ERROR: Mission UITextureKit Not Set for mission ID:",
					record.ID,
					" - ",
					record.Name
				}));
				Debug.LogWarning("This means the scrolling background images will show the wrong location");
			}
			if (this.m_lootGroupObj == null || this.m_missionRewardDisplayPrefab == null)
			{
				return;
			}
			MissionRewardDisplay[] componentsInChildren3 = this.m_lootGroupObj.GetComponentsInChildren<MissionRewardDisplay>(true);
			for (int m = 0; m < componentsInChildren3.Length; m++)
			{
				if (componentsInChildren3[m] != null)
				{
					Object.Destroy(componentsInChildren3[m].gameObject);
				}
			}
			if (missionResultType == 1)
			{
				PersistentFollowerData.ClearPreMissionFollowerData();
			}
			List<WrapperGarrisonFollower> list = new List<WrapperGarrisonFollower>();
			MissionFollowerSlot[] componentsInChildren4 = this.missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
			int num = 0;
			foreach (WrapperGarrisonFollower wrapperGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
			{
				if (wrapperGarrisonFollower.CurrentMissionID == garrMissionID)
				{
					componentsInChildren4[num++].SetFollower(wrapperGarrisonFollower.GarrFollowerID);
					if (missionResultType == 1)
					{
						PersistentFollowerData.CachePreMissionFollower(wrapperGarrisonFollower);
					}
					list.Add(wrapperGarrisonFollower);
				}
			}
			this.UpdateMissionStatus(garrMissionID);
			foreach (MissionFollowerSlot missionFollowerSlot in componentsInChildren4)
			{
				missionFollowerSlot.InitHeartPanel();
			}
			MissionRewardDisplay.InitMissionRewards(this.m_missionRewardDisplayPrefab.gameObject, this.m_lootGroupObj.transform, wrapperGarrisonMission.Rewards);
			if (record.OvermaxRewardPackID > 0 && wrapperGarrisonMission.OvermaxRewards.Count > 0)
			{
				this.m_bonusLootDisplay.SetActive(true);
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
						CurrencyTypesRec record4 = StaticDB.currencyTypesDB.GetRecord(wrapperGarrisonMissionReward.CurrencyType);
						int rewardQuantity = (int)((ulong)wrapperGarrisonMissionReward.CurrencyQuantity / (ulong)(((record4.Flags & 8u) == 0u) ? 1L : 100L));
						this.m_bonusMissionRewardDisplay.InitReward(MissionRewardDisplay.RewardType.currency, wrapperGarrisonMissionReward.CurrencyType, rewardQuantity, 0, 0);
					}
				}
			}
			this.m_timeUntilFadeOutMissionDetailsDisplay = this.m_missionDetailsFadeOutDelay;
			this.m_timeUntilShowFollowerExperienceDisplays = this.m_experienceDisplayInitialEntranceDelay;
			if (missionResultType == 2)
			{
				this.InitFollowerExperienceDisplays();
				this.m_missionInProgressMessage.SetActive(false);
				this.m_missionSuccessMessage.SetActive(true);
				this.m_missionFailMessage.SetActive(false);
				if (this.m_fancyEntrance != null)
				{
					Object.Destroy(this.m_fancyEntrance);
					iTween.Stop(this.m_missionSuccessMessage);
					this.m_missionSuccessMessage.transform.localScale = Vector3.one;
					iTween.Stop(this.m_missionFailMessage);
					this.m_missionFailMessage.transform.localScale = Vector3.one;
				}
				this.m_missionSuccessMessage.SetActive(false);
				this.m_fancyEntrance = this.m_missionSuccessMessage.AddComponent<FancyEntrance>();
				this.m_fancyEntrance.m_fadeInCanvasGroup = this.m_missionSuccessMessage.GetComponent<CanvasGroup>();
				this.m_fancyEntrance.m_fadeInTime = this.m_messageFadeInTime;
				this.m_fancyEntrance.m_punchScale = this.m_messagePunchScale;
				this.m_fancyEntrance.m_punchScaleAmount = this.m_messagePunchScaleAmount;
				this.m_fancyEntrance.m_punchScaleDuration = this.m_messagePunchScaleDuration;
				this.m_fancyEntrance.m_timeToDelayEntrance = this.m_messageTimeToDelayEntrance;
				this.m_fancyEntrance.m_activateOnEnable = true;
				this.m_fancyEntrance.m_objectToNotifyOnBegin = base.gameObject;
				this.m_fancyEntrance.m_notifyOnBeginCallbackName = "OnShowSuccessMessage";
				this.m_missionSuccessMessage.SetActive(true);
				MissionRewardDisplay[] componentsInChildren5 = this.m_lootGroupObj.GetComponentsInChildren<MissionRewardDisplay>(true);
				for (int num2 = 0; num2 < componentsInChildren5.Length; num2++)
				{
					componentsInChildren5[num2].ShowResultSuccess(this.m_lootEffectInitialDelay + this.m_lootEffectDelay * (float)num2);
				}
				if (this.m_bonusLootChance > 0)
				{
					iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
					{
						"name",
						"ShakeIt",
						"from",
						0.3f,
						"to",
						1f,
						"delay",
						this.m_bonusLootShakeInitialDelay,
						"time",
						this.m_bonusLootShakeDuration,
						"onupdate",
						"OnBonusLootShakeUpdate",
						"oncomplete",
						"OnBonusLootShakeComplete"
					}));
				}
				if (awardOvermax)
				{
					this.m_bonusMissionRewardDisplay.ShowResultSuccess(this.m_lootEffectInitialDelay + this.m_lootEffectDelay * (float)componentsInChildren5.Length);
				}
				else
				{
					this.m_bonusMissionRewardDisplay.ShowResultFail(this.m_lootEffectInitialDelay + this.m_lootEffectDelay * (float)componentsInChildren5.Length);
				}
			}
			if (missionResultType == 3)
			{
				this.InitFollowerExperienceDisplays();
				this.m_missionInProgressMessage.SetActive(false);
				this.m_missionSuccessMessage.SetActive(false);
				this.m_missionFailMessage.SetActive(true);
				if (this.m_fancyEntrance != null)
				{
					Object.Destroy(this.m_fancyEntrance);
					iTween.Stop(this.m_missionSuccessMessage);
					this.m_missionSuccessMessage.transform.localScale = Vector3.one;
					iTween.Stop(this.m_missionFailMessage);
					this.m_missionFailMessage.transform.localScale = Vector3.one;
				}
				this.m_missionFailMessage.SetActive(false);
				this.m_fancyEntrance = this.m_missionFailMessage.AddComponent<FancyEntrance>();
				this.m_fancyEntrance.m_fadeInCanvasGroup = this.m_missionFailMessage.GetComponent<CanvasGroup>();
				this.m_fancyEntrance.m_fadeInTime = this.m_messageFadeInTime;
				this.m_fancyEntrance.m_punchScale = this.m_messagePunchScale;
				this.m_fancyEntrance.m_punchScaleAmount = this.m_messagePunchScaleAmount;
				this.m_fancyEntrance.m_punchScaleDuration = this.m_messagePunchScaleDuration;
				this.m_fancyEntrance.m_timeToDelayEntrance = this.m_messageTimeToDelayEntrance;
				this.m_fancyEntrance.m_activateOnEnable = true;
				this.m_fancyEntrance.m_objectToNotifyOnBegin = base.gameObject;
				this.m_fancyEntrance.m_notifyOnBeginCallbackName = "OnShowFailMessage";
				this.m_missionFailMessage.SetActive(true);
				MissionRewardDisplay[] componentsInChildren6 = this.m_lootGroupObj.GetComponentsInChildren<MissionRewardDisplay>(true);
				for (int num3 = 0; num3 < componentsInChildren6.Length; num3++)
				{
					componentsInChildren6[num3].ShowResultFail(this.m_lootEffectInitialDelay);
				}
				this.m_bonusMissionRewardDisplay.ShowResultFail(this.m_lootEffectInitialDelay);
			}
			if (missionResultType == 0)
			{
				this.m_missionInProgressMessage.SetActive(true);
				this.m_missionSuccessMessage.SetActive(false);
				this.m_missionFailMessage.SetActive(false);
				this.m_bonusMissionRewardDisplay.ClearResults();
			}
			if (missionResultType == 1)
			{
				this.m_missionInProgressMessage.SetActive(false);
				this.m_missionSuccessMessage.SetActive(false);
				this.m_missionFailMessage.SetActive(false);
				FollowerExperienceDisplay[] componentsInChildren7 = this.m_followerExperienceDisplayArea.GetComponentsInChildren<FollowerExperienceDisplay>(true);
				foreach (FollowerExperienceDisplay followerExperienceDisplay in componentsInChildren7)
				{
					Object.Destroy(followerExperienceDisplay.gameObject);
				}
			}
			if (this.m_partyBuffGroup == null)
			{
				return;
			}
			AbilityDisplay[] componentsInChildren8 = this.m_partyBuffGroup.GetComponentsInChildren<AbilityDisplay>(true);
			foreach (AbilityDisplay abilityDisplay in componentsInChildren8)
			{
				Object.Destroy(abilityDisplay.gameObject);
			}
			int adjustedMissionDuration = GeneralHelpers.GetAdjustedMissionDuration(record, list, this.enemyPortraitsGroup);
			int num6 = 0;
			foreach (WrapperGarrisonFollower wrapperGarrisonFollower2 in PersistentFollowerData.followerDictionary.Values)
			{
				if (wrapperGarrisonFollower2.CurrentMissionID == garrMissionID)
				{
					int[] buffsForCurrentMission = GeneralHelpers.GetBuffsForCurrentMission(wrapperGarrisonFollower2.GarrFollowerID, garrMissionID, this.missionFollowerSlotGroup, adjustedMissionDuration);
					num6 += buffsForCurrentMission.Length;
					foreach (int garrAbilityID in buffsForCurrentMission)
					{
						GameObject gameObject3 = Object.Instantiate<GameObject>(this.m_mechanicEffectDisplayPrefab);
						gameObject3.transform.SetParent(this.m_partyBuffGroup.transform, false);
						AbilityDisplay component3 = gameObject3.GetComponent<AbilityDisplay>();
						component3.SetAbility(garrAbilityID, false, false, null);
					}
				}
			}
			if (num6 > 8)
			{
				this.m_partyBuffsText.text = string.Empty;
			}
			else
			{
				this.m_partyBuffsText.text = StaticDB.GetString("PARTY_BUFFS", null);
			}
			HorizontalLayoutGroup component4 = this.m_partyBuffGroup.GetComponent<HorizontalLayoutGroup>();
			if (component4 != null)
			{
				if (num6 > 10 && Main.instance.IsNarrowScreen())
				{
					component4.spacing = 3f;
				}
				else
				{
					component4.spacing = 6f;
				}
			}
			this.m_partyBuffGroup.SetActive(num6 > 0);
		}

		private void OnShowSuccessMessage()
		{
			Main.instance.m_UISound.Play_MissionSuccess();
		}

		private void OnShowFailMessage()
		{
			Main.instance.m_UISound.Play_MissionFailure();
		}

		private void OnBonusLootShakeUpdate(float val)
		{
			this.m_bonusMissionRewardDisplay.m_rewardIcon.transform.localPosition = new Vector3(this.m_bonusLootInitialLocalPosition.x + Random.Range(-this.m_bonusLootShakeAmountX * val, this.m_bonusLootShakeAmountX * val), this.m_bonusLootInitialLocalPosition.y + Random.Range(-this.m_bonusLootShakeAmountY * val, this.m_bonusLootShakeAmountY * val), this.m_bonusLootInitialLocalPosition.z);
		}

		private void OnBonusLootShakeComplete()
		{
			this.m_bonusMissionRewardDisplay.m_rewardIcon.transform.localPosition = this.m_bonusLootInitialLocalPosition;
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
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(this.m_garrMissionID);
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
					int[] buffsForCurrentMission = GeneralHelpers.GetBuffsForCurrentMission(currentGarrFollowerID2, this.m_garrMissionID, this.missionFollowerSlotGroup, adjustedMissionDuration);
					foreach (int item2 in buffsForCurrentMission)
					{
						list.Add(item2);
					}
				}
			}
			AllPopups.instance.ShowPartyBuffsPopup(list.ToArray());
		}

		private void InitFollowerExperienceDisplays()
		{
			int num = 0;
			foreach (WrapperGarrisonFollower wrapperGarrisonFollower in PersistentFollowerData.preMissionFollowerDictionary.Values)
			{
				if (wrapperGarrisonFollower.CurrentMissionID == this.m_garrMissionID)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.m_followerExperienceDisplayPrefab);
					FollowerExperienceDisplay component = gameObject.GetComponent<FollowerExperienceDisplay>();
					FancyEntrance component2 = gameObject.GetComponent<FancyEntrance>();
					float num2 = (float)num * this.m_experienceDisplayEntranceDelay;
					component2.m_timeToDelayEntrance = num2;
					component2.Activate();
					component.SetFollower(wrapperGarrisonFollower, wrapperGarrisonFollower, num2);
					component.transform.SetParent(this.m_followerExperienceDisplayArea.transform, false);
					num++;
				}
			}
		}

		public void HandleFollowerDataChanged()
		{
			if (!this.m_popupView.activeSelf)
			{
				return;
			}
			FollowerExperienceDisplay[] componentsInChildren = this.m_followerExperienceDisplayArea.GetComponentsInChildren<FollowerExperienceDisplay>(true);
			int num = 0;
			foreach (FollowerExperienceDisplay followerExperienceDisplay in componentsInChildren)
			{
				if (PersistentFollowerData.preMissionFollowerDictionary.ContainsKey(followerExperienceDisplay.GetFollowerID()))
				{
					WrapperGarrisonFollower oldFollower = PersistentFollowerData.preMissionFollowerDictionary[followerExperienceDisplay.GetFollowerID()];
					bool flag = PersistentFollowerData.followerDictionary.ContainsKey(followerExperienceDisplay.GetFollowerID());
					WrapperGarrisonFollower newFollower;
					if (flag)
					{
						newFollower = PersistentFollowerData.followerDictionary[followerExperienceDisplay.GetFollowerID()];
					}
					else
					{
						newFollower = default(WrapperGarrisonFollower);
						newFollower.GarrFollowerID = oldFollower.GarrFollowerID;
						newFollower.Quality = oldFollower.Quality;
						newFollower.Durability = 0;
					}
					float initialEffectDelay = (float)num * this.m_experienceDisplayEntranceDelay;
					followerExperienceDisplay.SetFollower(oldFollower, newFollower, initialEffectDelay);
					num++;
				}
			}
		}

		public void HideMissionResults()
		{
			this.m_darknessBG.SetActive(false);
			this.m_popupView.SetActive(false);
		}

		public void NarrowScreenAdjust()
		{
			GridLayoutGroup component = this.m_EnemiesGroup.GetComponent<GridLayoutGroup>();
			if (component != null)
			{
				Vector2 spacing = component.spacing;
				spacing.x = 30f;
				component.spacing = spacing;
			}
			component = this.m_FollowerSlotGroup.GetComponent<GridLayoutGroup>();
			if (component != null)
			{
				Vector2 spacing2 = component.spacing;
				spacing2.x = 30f;
				component.spacing = spacing2;
			}
		}

		public GameObject m_darknessBG;

		public GameObject m_popupView;

		public GameObject missionFollowerSlotGroup;

		public GameObject enemyPortraitsGroup;

		public GameObject treasureChestHorde;

		public GameObject treasureChestAlliance;

		public GameObject missionEncounterPrefab;

		public Text missionNameText;

		public Text missionLocationText;

		public Image missionTypeImage;

		public GameObject missionFollowerSlotPrefab;

		public Image m_scrollingEnvironment_Back;

		public Image m_scrollingEnvironment_Mid;

		public Image m_scrollingEnvironment_Fore;

		public GameObject m_lootGroupObj;

		public MissionRewardDisplay m_missionRewardDisplayPrefab;

		public GameObject m_mechanicEffectDisplayPrefab;

		public Text missionPercentChanceText;

		public GameObject m_missionChanceSpinner;

		public GameObject m_partyBuffGroup;

		public Text m_partyBuffsText;

		public GameObject m_missionSuccessMessage;

		public GameObject m_missionFailMessage;

		public GameObject m_missionInProgressMessage;

		public Text m_missionTimeRemainingText;

		public float m_messageTimeToDelayEntrance;

		public float m_messageFadeInTime;

		public bool m_messagePunchScale;

		public float m_messagePunchScaleAmount;

		public float m_messagePunchScaleDuration;

		public float m_lootEffectInitialDelay;

		public float m_lootEffectDelay;

		public Text m_okButtonText;

		public Text m_inProgressText;

		public Text m_successText;

		public Text m_failureText;

		[Header("Bonus Loot")]
		public GameObject m_bonusLootDisplay;

		private int m_bonusLootChance;

		public Text m_bonusLootChanceText;

		public MissionRewardDisplay m_bonusMissionRewardDisplay;

		private Vector3 m_bonusLootInitialLocalPosition;

		public float m_bonusLootShakeInitialDelay;

		public float m_bonusLootShakeDuration;

		public float m_bonusLootShakeAmountX;

		public float m_bonusLootShakeAmountY;

		[Header("XP Display")]
		public GameObject m_followerExperienceDisplayArea;

		public GameObject m_followerExperienceDisplayPrefab;

		public float m_missionDetailsFadeOutDelay;

		public float m_experienceDisplayInitialEntranceDelay;

		public float m_experienceDisplayEntranceDelay;

		public AutoFadeOut m_missionResultsDisplayCanvasGroupAutoFadeOut;

		private MissionResultType m_currentResultType;

		private FancyEntrance m_fancyEntrance;

		private DateTime m_missionStartedTime;

		private TimeSpan m_missionDurationInSeconds;

		private int m_garrMissionID;

		private bool m_attemptedAutoComplete;

		private float m_timeUntilFadeOutMissionDetailsDisplay;

		private float m_timeUntilShowFollowerExperienceDisplays;

		[Header("Narrow Screen")]
		public GameObject m_EnemiesGroup;

		public GameObject m_FollowerSlotGroup;
	}
}
