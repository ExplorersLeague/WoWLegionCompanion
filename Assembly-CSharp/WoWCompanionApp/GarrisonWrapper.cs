using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class GarrisonWrapper : Singleton<GarrisonWrapper>
	{
		public event Action FollowerDataChangedAction;

		public event Action<int> MissionSuccessChanceChangedAction;

		public event Action GarrisonDataResetStartedAction;

		public event Action GarrisonDataResetFinishedAction;

		public event Action ShipmentTypesUpdatedAction;

		public event Action<int> CreateShipmentResultAction;

		public event Action<int, ulong> ShipmentAddedAction;

		public event Action<SHIPMENT_RESULT, ulong> CompleteShipmentResultAction;

		public event Action<int, int> MissionAddedAction;

		public event Action<WrapperGarrisonFollower, WrapperGarrisonFollower> FollowerChangedXPAction;

		public event Action<WrapperGarrisonFollower> TroopExpiredAction;

		public event Action<int, int, string> CanResearchGarrisonTalentResultAction;

		public event Action<int, int> ResearchGarrisonTalentResultAction;

		public event Action BountyInfoUpdatedAction;

		public event Action<int> UseEquipmentStartAction;

		public event Action<WrapperGarrisonFollower, WrapperGarrisonFollower> UseEquipmentResultAction;

		public event Action EquipmentInventoryChangedAction;

		public event Action<int> UseArmamentStartAction;

		public event Action<int, WrapperGarrisonFollower, WrapperGarrisonFollower> UseArmamentResultAction;

		public event Action ArmamentInventoryChangedAction;

		public event Action<int, WrapperShipmentItem> ShipmentItemPushedAction;

		public event Action<int> PlayerLeveledUpAction;

		public event Action MakeContributionRequestInitiatedAction;

		public event Action ContributionInfoChangedAction;

		public event Action InvasionPOIChangedAction;

		public event Action ArtifactKnowledgeInfoChangedAction;

		public event Action MaxActiveFollowersChangedAction;

		public event Action<int, int, int> GotItemFromQuestCompletionAction;

		private void OnEnable()
		{
			if (!base.IsCloneGettingRemoved)
			{
				this.SubscribeToEvents();
			}
		}

		private void OnDestroy()
		{
			if (!base.IsCloneGettingRemoved)
			{
				this.UnsubscribeFromEvents();
			}
		}

		private void Update()
		{
		}

		private void SubscribeToEvents()
		{
			LegionCompanionWrapper.OnRequestWorldQuestsResult += new LegionCompanionWrapper.RequestWorldQuestsResultHandler(this.WorldQuestUpdateHandler);
			LegionCompanionWrapper.OnBountiesByWorldQuestUpdate += new LegionCompanionWrapper.BountiesByWorldQuestUpdateHandler(this.BountiesByWorldQuestUpdateHandler);
			LegionCompanionWrapper.OnRequestWorldQuestBountiesResult += new LegionCompanionWrapper.RequestWorldQuestBountiesResultHandler(this.WorldQuestBountiesResultHandler);
			LegionCompanionWrapper.OnRequestWorldQuestInactiveBountiesResult += new LegionCompanionWrapper.RequestWorldQuestInactiveBountiesResultHandler(this.WorldQuestInactiveBountiesResultHandler);
			LegionCompanionWrapper.OnGarrisonDataRequestResult += new LegionCompanionWrapper.GarrisonDataRequestResultHandler(this.GarrisonDataResultHandler);
			LegionCompanionWrapper.OnGarrisonStartMissionResult += new LegionCompanionWrapper.GarrisonStartMissionResultHandler(this.StartMissionResultHandler);
			LegionCompanionWrapper.OnGarrisonCompleteMissionResult += new LegionCompanionWrapper.GarrisonCompleteMissionResultHandler(this.CompleteMissionResultHandler);
			LegionCompanionWrapper.OnClaimMissionBonusResult += new LegionCompanionWrapper.ClaimMissionBonusResultHandler(this.ClaimMissionBonusResultHandler);
			LegionCompanionWrapper.OnMissionAdded += new LegionCompanionWrapper.MissionAddedHandler(this.MissionAddedHandler);
			LegionCompanionWrapper.OnFollowerChangedXp += new LegionCompanionWrapper.FollowerChangedXpHandler(this.FollowerChangedXPHandler);
			LegionCompanionWrapper.OnFollowerChangedQuality += new LegionCompanionWrapper.FollowerChangedQualityHandler(this.FollowerChangedQualityHandler);
			LegionCompanionWrapper.OnShipmentsUpdate += new LegionCompanionWrapper.ShipmentsUpdateHandler(this.ShipmentsUpdateHandler);
			LegionCompanionWrapper.OnUseFollowerArmamentResult += new LegionCompanionWrapper.UseFollowerArmamentResultHandler(this.UseFollowerArmamentResultHandler);
			LegionCompanionWrapper.OnChangeFollowerActiveResult += new LegionCompanionWrapper.ChangeFollowerActiveResultHandler(this.ChangeFollowerActiveResultHandler);
			LegionCompanionWrapper.OnRequestMaxFollowersResult += new LegionCompanionWrapper.RequestMaxFollowersResultHandler(this.RequestMaxFollowersResultHandler);
			LegionCompanionWrapper.OnFollowerArmamentsExtendedResult += new LegionCompanionWrapper.FollowerArmamentsExtendedResultHandler(this.FollowerArmamentsExtendedResultHandler);
			LegionCompanionWrapper.OnFollowerEquipmentResult += new LegionCompanionWrapper.FollowerEquipmentResultHandler(this.FollowerEquipmentResultHandler);
			LegionCompanionWrapper.OnEmissaryFactionsUpdate += new LegionCompanionWrapper.EmissaryFactionsUpdateHandler(this.EmissaryFactionUpdateHandler);
			LegionCompanionWrapper.OnCreateShipmentResult += new LegionCompanionWrapper.CreateShipmentResultHandler(this.CreateShipmentResultHandler);
			LegionCompanionWrapper.OnCompleteShipmentResult += new LegionCompanionWrapper.CompleteShipmentResultHandler(this.CompleteShipmentResultHandler);
			LegionCompanionWrapper.OnEvaluateMissionResult += new LegionCompanionWrapper.EvaluateMissionResultHandler(this.EvaluateMissionResultHandler);
			LegionCompanionWrapper.OnShipmentTypesResult += new LegionCompanionWrapper.ShipmentTypesResultHandler(this.ShipmentTypesHandler);
			LegionCompanionWrapper.OnCanResearchGarrisonTalentResult += new LegionCompanionWrapper.CanResearchGarrisonTalentResultHandler(this.CanResearchGarrisonTalentResultHandler);
			LegionCompanionWrapper.OnResearchGarrisonTalentResult += new LegionCompanionWrapper.ResearchGarrisonTalentResultHandler(this.ResearchGarrisonTalentResultHandler);
			LegionCompanionWrapper.OnFollowerActivationDataResult += new LegionCompanionWrapper.FollowerActivationDataResultHandler(this.FollowerActivationDataResultHandler);
			LegionCompanionWrapper.OnShipmentPushResult += new LegionCompanionWrapper.ShipmentPushResultHandler(this.ShipmentPushResultHandler);
			LegionCompanionWrapper.OnAreaPoiInfoResult += new LegionCompanionWrapper.AreaPoiInfoResultHandler(this.RequestAreaPoiInfoResultHandler);
			LegionCompanionWrapper.OnContributionInfoResult += new LegionCompanionWrapper.ContributionInfoResultHandler(this.RequestContributionInfoResultHandler);
			LegionCompanionWrapper.OnMakeContributionResult += new LegionCompanionWrapper.MakeContributionResultHandler(this.MakeContributionResultHandler);
			LegionCompanionWrapper.OnItemTooltipInfoResult += new LegionCompanionWrapper.ItemTooltipInfoResultHandler(this.GetItemTooltipInfoResultHandler);
			LegionCompanionWrapper.OnArtifactInfoResult += new LegionCompanionWrapper.ArtifactInfoResultHandler(this.ArtifactInfoResultHandler);
			LegionCompanionWrapper.OnPlayerLevelUp += new LegionCompanionWrapper.PlayerLevelUpHandler(this.PlayerLevelUpHandler);
			LegionCompanionWrapper.OnQuestCompleted += new LegionCompanionWrapper.QuestCompletedHandler(this.QuestCompletedHandler);
		}

		private void UnsubscribeFromEvents()
		{
			LegionCompanionWrapper.OnRequestWorldQuestsResult -= new LegionCompanionWrapper.RequestWorldQuestsResultHandler(this.WorldQuestUpdateHandler);
			LegionCompanionWrapper.OnBountiesByWorldQuestUpdate -= new LegionCompanionWrapper.BountiesByWorldQuestUpdateHandler(this.BountiesByWorldQuestUpdateHandler);
			LegionCompanionWrapper.OnRequestWorldQuestBountiesResult -= new LegionCompanionWrapper.RequestWorldQuestBountiesResultHandler(this.WorldQuestBountiesResultHandler);
			LegionCompanionWrapper.OnRequestWorldQuestInactiveBountiesResult -= new LegionCompanionWrapper.RequestWorldQuestInactiveBountiesResultHandler(this.WorldQuestInactiveBountiesResultHandler);
			LegionCompanionWrapper.OnGarrisonDataRequestResult -= new LegionCompanionWrapper.GarrisonDataRequestResultHandler(this.GarrisonDataResultHandler);
			LegionCompanionWrapper.OnGarrisonStartMissionResult -= new LegionCompanionWrapper.GarrisonStartMissionResultHandler(this.StartMissionResultHandler);
			LegionCompanionWrapper.OnGarrisonCompleteMissionResult -= new LegionCompanionWrapper.GarrisonCompleteMissionResultHandler(this.CompleteMissionResultHandler);
			LegionCompanionWrapper.OnClaimMissionBonusResult -= new LegionCompanionWrapper.ClaimMissionBonusResultHandler(this.ClaimMissionBonusResultHandler);
			LegionCompanionWrapper.OnMissionAdded -= new LegionCompanionWrapper.MissionAddedHandler(this.MissionAddedHandler);
			LegionCompanionWrapper.OnFollowerChangedXp -= new LegionCompanionWrapper.FollowerChangedXpHandler(this.FollowerChangedXPHandler);
			LegionCompanionWrapper.OnFollowerChangedQuality -= new LegionCompanionWrapper.FollowerChangedQualityHandler(this.FollowerChangedQualityHandler);
			LegionCompanionWrapper.OnShipmentsUpdate -= new LegionCompanionWrapper.ShipmentsUpdateHandler(this.ShipmentsUpdateHandler);
			LegionCompanionWrapper.OnUseFollowerArmamentResult -= new LegionCompanionWrapper.UseFollowerArmamentResultHandler(this.UseFollowerArmamentResultHandler);
			LegionCompanionWrapper.OnChangeFollowerActiveResult -= new LegionCompanionWrapper.ChangeFollowerActiveResultHandler(this.ChangeFollowerActiveResultHandler);
			LegionCompanionWrapper.OnRequestMaxFollowersResult -= new LegionCompanionWrapper.RequestMaxFollowersResultHandler(this.RequestMaxFollowersResultHandler);
			LegionCompanionWrapper.OnFollowerArmamentsExtendedResult -= new LegionCompanionWrapper.FollowerArmamentsExtendedResultHandler(this.FollowerArmamentsExtendedResultHandler);
			LegionCompanionWrapper.OnFollowerEquipmentResult -= new LegionCompanionWrapper.FollowerEquipmentResultHandler(this.FollowerEquipmentResultHandler);
			LegionCompanionWrapper.OnEmissaryFactionsUpdate -= new LegionCompanionWrapper.EmissaryFactionsUpdateHandler(this.EmissaryFactionUpdateHandler);
			LegionCompanionWrapper.OnCreateShipmentResult -= new LegionCompanionWrapper.CreateShipmentResultHandler(this.CreateShipmentResultHandler);
			LegionCompanionWrapper.OnCompleteShipmentResult -= new LegionCompanionWrapper.CompleteShipmentResultHandler(this.CompleteShipmentResultHandler);
			LegionCompanionWrapper.OnEvaluateMissionResult -= new LegionCompanionWrapper.EvaluateMissionResultHandler(this.EvaluateMissionResultHandler);
			LegionCompanionWrapper.OnShipmentTypesResult -= new LegionCompanionWrapper.ShipmentTypesResultHandler(this.ShipmentTypesHandler);
			LegionCompanionWrapper.OnCanResearchGarrisonTalentResult -= new LegionCompanionWrapper.CanResearchGarrisonTalentResultHandler(this.CanResearchGarrisonTalentResultHandler);
			LegionCompanionWrapper.OnResearchGarrisonTalentResult -= new LegionCompanionWrapper.ResearchGarrisonTalentResultHandler(this.ResearchGarrisonTalentResultHandler);
			LegionCompanionWrapper.OnFollowerActivationDataResult -= new LegionCompanionWrapper.FollowerActivationDataResultHandler(this.FollowerActivationDataResultHandler);
			LegionCompanionWrapper.OnShipmentPushResult -= new LegionCompanionWrapper.ShipmentPushResultHandler(this.ShipmentPushResultHandler);
			LegionCompanionWrapper.OnAreaPoiInfoResult -= new LegionCompanionWrapper.AreaPoiInfoResultHandler(this.RequestAreaPoiInfoResultHandler);
			LegionCompanionWrapper.OnContributionInfoResult -= new LegionCompanionWrapper.ContributionInfoResultHandler(this.RequestContributionInfoResultHandler);
			LegionCompanionWrapper.OnMakeContributionResult -= new LegionCompanionWrapper.MakeContributionResultHandler(this.MakeContributionResultHandler);
			LegionCompanionWrapper.OnItemTooltipInfoResult -= new LegionCompanionWrapper.ItemTooltipInfoResultHandler(this.GetItemTooltipInfoResultHandler);
			LegionCompanionWrapper.OnArtifactInfoResult -= new LegionCompanionWrapper.ArtifactInfoResultHandler(this.ArtifactInfoResultHandler);
			LegionCompanionWrapper.OnPlayerLevelUp -= new LegionCompanionWrapper.PlayerLevelUpHandler(this.PlayerLevelUpHandler);
			LegionCompanionWrapper.OnQuestCompleted -= new LegionCompanionWrapper.QuestCompletedHandler(this.QuestCompletedHandler);
		}

		public void MobileRequestData()
		{
			LegionCompanionWrapper.RequestShipmentTypes((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestWorldQuestBounties(10);
			this.RequestWorldQuests();
			LegionCompanionWrapper.RequestFollowerEquipment((int)GarrisonStatus.GarrisonFollowerType);
			LegionCompanionWrapper.RequestFollowerArmamentsExtended((int)GarrisonStatus.GarrisonFollowerType);
			LegionCompanionWrapper.RequestFollowerActivationData((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.GetArtifactInfo();
			LegionCompanionWrapper.RequestContributionInfo();
			LegionCompanionWrapper.RequestAreaPoiInfo();
			LegionCompanionWrapper.RequestMaxFollowers((int)GarrisonStatus.GarrisonFollowerType);
			LegionCompanionWrapper.RequestGarrisonData((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestAreaPoiInfo();
			if (this.GarrisonDataResetFinishedAction != null)
			{
				this.GarrisonDataResetFinishedAction();
			}
		}

		private void PrecacheMissionChances()
		{
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
				{
					if (wrapperGarrisonMission.MissionState == 1)
					{
						List<WrapperGarrisonFollower> list = new List<WrapperGarrisonFollower>();
						foreach (WrapperGarrisonFollower item in PersistentFollowerData.followerDictionary.Values)
						{
							if (item.CurrentMissionID == wrapperGarrisonMission.MissionRecID)
							{
								list.Add(item);
							}
						}
						LegionCompanionWrapper.EvaluateMission(wrapperGarrisonMission.MissionRecID, (from f in list
						select f.GarrFollowerID).ToList<int>());
					}
				}
			}
		}

		public void HandleEnterWorld()
		{
			this.GarrisonDataResetFinishedAction -= this.HandleEnterWorld;
			this.PrecacheMissionChances();
			LocalNotifications.RegisterForNotifications();
			SceneManager.LoadSceneAsync(Scenes.MainSceneName);
		}

		public void StartMission(int garrMissionID, IEnumerable<ulong> followerDBIDs)
		{
			LegionCompanionWrapper.GarrisonStartMission(garrMissionID, followerDBIDs.ToList<ulong>());
		}

		public void CompleteMission(int garrMissionID)
		{
			LegionCompanionWrapper.GarrisonCompleteMission(garrMissionID);
		}

		public void ClaimMissionBonus(int garrMissionID)
		{
			LegionCompanionWrapper.ClaimMissionBonus(garrMissionID);
		}

		public void CompleteAllMissions()
		{
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
				{
					if (wrapperGarrisonMission.MissionState == 1)
					{
						TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
						if ((wrapperGarrisonMission.MissionDuration - t).TotalSeconds <= 0.0)
						{
							this.CompleteMission(wrapperGarrisonMission.MissionRecID);
						}
					}
				}
			}
		}

		public void RequestEmissaryFactions(int factionID)
		{
			LegionCompanionWrapper.RequestEmissaryFactions(factionID);
		}

		public void RequestWorldQuests()
		{
			LegionCompanionWrapper.RequestWorldQuests();
		}

		private void EmissaryFactionUpdateHandler(LegionCompanionWrapper.EmissaryFactionsUpdateEvent eventArgs)
		{
			AllPopups.instance.EmissaryFactionUpdate(eventArgs.Factions);
		}

		private void CreateShipmentResultHandler(LegionCompanionWrapper.CreateShipmentResultEvent eventArgs)
		{
			if (eventArgs.Result == 0)
			{
				LegionCompanionWrapper.RequestShipmentTypes((int)GarrisonStatus.GarrisonType);
				LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
				LegionCompanionWrapper.RequestGarrisonData((int)GarrisonStatus.GarrisonType);
			}
			if (this.CreateShipmentResultAction != null)
			{
				this.CreateShipmentResultAction(eventArgs.Result);
			}
		}

		private void ShipmentTypesHandler(LegionCompanionWrapper.ShipmentTypesResultEvent eventArgs)
		{
			PersistentShipmentData.SetAvailableShipmentTypes(eventArgs.Shipments);
			if (this.ShipmentTypesUpdatedAction != null)
			{
				this.ShipmentTypesUpdatedAction();
			}
		}

		private void CompleteShipmentResultHandler(LegionCompanionWrapper.CompleteShipmentResultEvent eventArgs)
		{
			SHIPMENT_RESULT result = (SHIPMENT_RESULT)eventArgs.Result;
			if (this.CompleteShipmentResultAction != null)
			{
				this.CompleteShipmentResultAction(result, eventArgs.ShipmentID);
			}
		}

		private void ShipmentsUpdateHandler(LegionCompanionWrapper.ShipmentsUpdateEvent eventArgs)
		{
			PersistentShipmentData.ClearData();
			foreach (WrapperCharacterShipment shipment in eventArgs.Shipments)
			{
				PersistentShipmentData.AddOrUpdateShipment(shipment);
				if (this.ShipmentAddedAction != null)
				{
					this.ShipmentAddedAction(shipment.ShipmentRecID, shipment.ShipmentID);
				}
			}
		}

		private static bool DoesMapIDSupportWorldQuests(int mapID)
		{
			return mapID == 1642 || mapID == 1643 || mapID == 0 || mapID == 1;
		}

		private void WorldQuestUpdateHandler(LegionCompanionWrapper.RequestWorldQuestsResultEvent eventArgs)
		{
			WorldQuestData.ClearData();
			foreach (WrapperWorldQuest worldQuest in eventArgs.Quests)
			{
				bool flag = GarrisonWrapper.DoesMapIDSupportWorldQuests(worldQuest.StartLocationMapID);
				if (flag)
				{
					WorldQuestData.AddWorldQuest(worldQuest);
					for (int i = 0; i < worldQuest.Items.Count; i++)
					{
						ItemStatCache.instance.GetItemStats(worldQuest.Items[i].RecordID, worldQuest.Items[i].ItemContext, worldQuest.Items[i].ItemInstance);
					}
				}
			}
		}

		private void BountiesByWorldQuestUpdateHandler(LegionCompanionWrapper.BountiesByWorldQuestUpdateEvent eventArgs)
		{
			foreach (WrapperBountiesByWorldQuest bountiesByWorldQuest in eventArgs.Quests)
			{
				PersistentBountyData.AddOrUpdateBountiesByWorldQuest(bountiesByWorldQuest);
			}
			if (AdventureMapPanel.instance != null)
			{
				AdventureMapPanel.instance.UpdateWorldQuests();
			}
		}

		private void EvaluateMissionResultHandler(LegionCompanionWrapper.EvaluateMissionResultEvent eventArgs)
		{
			if (eventArgs.Result == 0)
			{
				MissionDataCache.AddOrUpdateMissionData(eventArgs.GarrMissionID, eventArgs.SuccessChance);
				if (this.MissionSuccessChanceChangedAction != null)
				{
					this.MissionSuccessChanceChangedAction(eventArgs.SuccessChance);
				}
			}
		}

		private void ShipmentPushResultHandler(LegionCompanionWrapper.ShipmentPushResultEvent eventArgs)
		{
			foreach (WrapperShipmentItem arg in eventArgs.Items)
			{
				if (this.ShipmentItemPushedAction != null)
				{
					this.ShipmentItemPushedAction(eventArgs.CharShipmentID, arg);
				}
			}
		}

		private void SetMissionDurationCheatResultHandler(LegionCompanionWrapper.SetMissionDurationCheatResultEvent eventArgs)
		{
			AllPopups.instance.HideAllPopups();
		}

		private void CanResearchGarrisonTalentResultHandler(LegionCompanionWrapper.CanResearchGarrisonTalentResultEvent eventArgs)
		{
			if (this.CanResearchGarrisonTalentResultAction != null)
			{
				this.CanResearchGarrisonTalentResultAction(eventArgs.GarrTalentID, eventArgs.Result, eventArgs.ConditionText);
			}
		}

		private void ResearchGarrisonTalentResultHandler(LegionCompanionWrapper.ResearchGarrisonTalentResultEvent eventArgs)
		{
			GARRISON_RESULT result = (GARRISON_RESULT)eventArgs.Result;
			if (result != GARRISON_RESULT.SUCCESS)
			{
				AllPopups.instance.ShowGenericPopup(StaticDB.GetString("TALENT_RESEARCH_FAILED", null), result.ToString());
			}
			if (this.ResearchGarrisonTalentResultAction != null)
			{
				this.ResearchGarrisonTalentResultAction(eventArgs.GarrTalentID, eventArgs.Result);
			}
		}

		private void FollowerEquipmentResultHandler(LegionCompanionWrapper.FollowerEquipmentResultEvent eventArgs)
		{
			PersistentEquipmentData.ClearData();
			for (int i = 0; i < eventArgs.Equipments.Count; i++)
			{
				PersistentEquipmentData.AddOrUpdateEquipment(eventArgs.Equipments[i]);
			}
			if (this.EquipmentInventoryChangedAction != null)
			{
				this.EquipmentInventoryChangedAction();
			}
		}

		private void FollowerChangedQualityHandler(LegionCompanionWrapper.FollowerChangedQualityEvent eventArgs)
		{
			PersistentFollowerData.AddOrUpdateFollower(eventArgs.Follower);
			if (this.UseEquipmentResultAction != null)
			{
				this.UseEquipmentResultAction(eventArgs.OldFollower, eventArgs.Follower);
			}
			LegionCompanionWrapper.RequestFollowerEquipment((int)GarrisonStatus.GarrisonFollowerType);
		}

		private void FollowerArmamentsExtendedResultHandler(LegionCompanionWrapper.FollowerArmamentsExtendedResultEvent eventArgs)
		{
			PersistentArmamentData.ClearData();
			for (int i = 0; i < eventArgs.Armaments.Count; i++)
			{
				PersistentArmamentData.AddOrUpdateArmament(eventArgs.Armaments[i]);
			}
			if (this.ArmamentInventoryChangedAction != null)
			{
				this.ArmamentInventoryChangedAction();
			}
		}

		private void UseFollowerArmamentResultHandler(LegionCompanionWrapper.UseFollowerArmamentResultEvent eventArgs)
		{
			if (eventArgs.Result == 0)
			{
				PersistentFollowerData.AddOrUpdateFollower(eventArgs.Follower);
				LegionCompanionWrapper.RequestFollowerArmamentsExtended((int)GarrisonStatus.GarrisonFollowerType);
			}
			else
			{
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("USE_ARMAMENT_FAILED", null));
			}
			if (this.UseArmamentResultAction != null)
			{
				this.UseArmamentResultAction(eventArgs.Result, eventArgs.OldFollower, eventArgs.Follower);
			}
		}

		private void WorldQuestBountiesResultHandler(LegionCompanionWrapper.RequestWorldQuestBountiesResultEvent eventArgs)
		{
			PersistentBountyData.ClearData();
			PersistentBountyData.SetBountiesVisible(eventArgs.Visible);
			for (int i = 0; i < eventArgs.Bounties.Count; i++)
			{
				PersistentBountyData.AddOrUpdateBounty(eventArgs.Bounties[i]);
			}
			if (this.BountyInfoUpdatedAction != null)
			{
				this.BountyInfoUpdatedAction();
			}
		}

		private void WorldQuestInactiveBountiesResultHandler(LegionCompanionWrapper.RequestWorldQuestInactiveBountiesResultEvent eventArgs)
		{
		}

		private void FollowerActivationDataResultHandler(LegionCompanionWrapper.FollowerActivationDataResultEvent eventArgs)
		{
			GarrisonStatus.SetFollowerActivationInfo(eventArgs.ActivationsRemaining, eventArgs.GoldCost);
		}

		private void ChangeFollowerActiveResultHandler(LegionCompanionWrapper.ChangeFollowerActiveResultEvent eventArgs)
		{
			if (eventArgs.Result == 0)
			{
				PersistentFollowerData.AddOrUpdateFollower(eventArgs.Follower);
				FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(eventArgs.Follower);
				if (followerStatus == FollowerStatus.inactive)
				{
					Debug.Log("Follower is now inactive. " + eventArgs.ActivationsRemaining + " activations remain for the day.");
				}
				else
				{
					Debug.Log("Follower is now active. " + eventArgs.ActivationsRemaining + " activations remain for the day.");
				}
				if (this.FollowerDataChangedAction != null)
				{
					this.FollowerDataChangedAction();
				}
				LegionCompanionWrapper.RequestFollowerActivationData((int)GarrisonStatus.GarrisonType);
			}
		}

		private void GetItemTooltipInfoResultHandler(LegionCompanionWrapper.ItemTooltipInfoResultEvent eventArgs)
		{
			ItemStatCache.instance.AddMobileItemStats(eventArgs.ItemID, eventArgs.ItemContext, eventArgs.Stats, null);
		}

		private void ArtifactInfoResultHandler(LegionCompanionWrapper.ArtifactInfoResultEvent eventArgs)
		{
			GarrisonStatus.ArtifactKnowledgeLevel = eventArgs.KnowledgeLevel;
			GarrisonStatus.ArtifactXpMultiplier = eventArgs.XpMultiplier;
		}

		private void PlayerLevelUpHandler(LegionCompanionWrapper.PlayerLevelUpEvent eventArgs)
		{
			Debug.Log("Congrats, your character is now level " + eventArgs.NewLevel);
			AllPopups.instance.ShowLevelUpToast(eventArgs.NewLevel);
			if (this.PlayerLeveledUpAction != null)
			{
				this.PlayerLeveledUpAction(eventArgs.NewLevel);
			}
		}

		private void RequestContributionInfoResultHandler(LegionCompanionWrapper.ContributionInfoResultEvent eventArgs)
		{
			LegionfallData.ClearData();
			LegionfallData.SetLegionfallWarResources(eventArgs.LegionfallWarResources);
			LegionfallData.SetHasAccess(eventArgs.HasAccess);
			foreach (WrapperContribution contribution in eventArgs.Contributions)
			{
				LegionfallData.AddOrUpdateLegionfallBuilding(contribution);
			}
			if (this.ContributionInfoChangedAction != null)
			{
				this.ContributionInfoChangedAction();
			}
		}

		private void MakeContributionResultHandler(LegionCompanionWrapper.MakeContributionResultEvent eventArgs)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Make Contribution Result for ID ",
				eventArgs.ContributionID,
				" is ",
				eventArgs.Result
			}));
			LegionCompanionWrapper.RequestContributionInfo();
			if (this.ContributionInfoChangedAction != null)
			{
				this.ContributionInfoChangedAction();
			}
		}

		private void RequestAreaPoiInfoResultHandler(LegionCompanionWrapper.AreaPoiInfoResultEvent eventArgs)
		{
			LegionfallData.SetCurrentInvasionPOI(null);
			if (eventArgs.POIData != null && eventArgs.POIData.Count > 0)
			{
				LegionfallData.SetCurrentInvasionPOI(new WrapperAreaPoi?(eventArgs.POIData[0]));
				LegionfallData.SetCurrentInvasionExpirationTime(eventArgs.POIData[0].TimeRemaining);
			}
			if (this.InvasionPOIChangedAction != null)
			{
				this.InvasionPOIChangedAction();
			}
		}

		private void RequestMaxFollowersResultHandler(LegionCompanionWrapper.RequestMaxFollowersResultEvent eventArgs)
		{
			GarrisonStatus.SetMaxActiveFollowers(eventArgs.MaxFollowers);
			if (this.MaxActiveFollowersChangedAction != null)
			{
				this.MaxActiveFollowersChangedAction();
			}
		}

		private void QuestCompletedHandler(LegionCompanionWrapper.QuestCompletedEvent eventArgs)
		{
			foreach (WrapperQuestItem wrapperQuestItem in eventArgs.Items)
			{
				if (this.GotItemFromQuestCompletionAction != null)
				{
					this.GotItemFromQuestCompletionAction(wrapperQuestItem.ItemID, wrapperQuestItem.Quantity, eventArgs.QuestID);
				}
			}
		}

		private void GarrisonDataResultHandler(LegionCompanionWrapper.GarrisonDataRequestResultEvent eventArgs)
		{
			PersistentFollowerData.ClearData();
			PersistentMissionData.ClearData();
			PersistentTalentData.ClearData();
			if (this.GarrisonDataResetStartedAction != null)
			{
				this.GarrisonDataResetStartedAction();
			}
			GarrisonStatus.SetFaction(eventArgs.Data.PvpFaction);
			GarrisonStatus.SetGarrisonServerConnectTime(eventArgs.Data.ServerTime);
			GarrisonStatus.SetCurrencies(eventArgs.Data.GoldCurrency, eventArgs.Data.OrderhallResourcesCurrency, eventArgs.Data.WarResourcesCurrency);
			GarrisonStatus.SetCharacterName(eventArgs.Data.CharacterName);
			GarrisonStatus.SetCharacterLevel(eventArgs.Data.CharacterLevel);
			GarrisonStatus.SetCharacterClass(eventArgs.Data.CharacterClassID);
			for (int i = 0; i < eventArgs.Data.Followers.Count; i++)
			{
				WrapperGarrisonFollower wrapperGarrisonFollower = eventArgs.Data.Followers[i];
				if (StaticDB.garrFollowerDB.GetRecord(wrapperGarrisonFollower.GarrFollowerID) != null)
				{
					PersistentFollowerData.AddOrUpdateFollower(wrapperGarrisonFollower);
					bool flag = (wrapperGarrisonFollower.Flags & 8) != 0;
					if (flag && wrapperGarrisonFollower.Durability <= 0 && this.TroopExpiredAction != null)
					{
						this.TroopExpiredAction(wrapperGarrisonFollower);
					}
				}
			}
			for (int j = 0; j < eventArgs.Data.Missions.Count; j++)
			{
				PersistentMissionData.AddMission(eventArgs.Data.Missions[j]);
			}
			for (int k = 0; k < eventArgs.Data.Talents.Count; k++)
			{
				PersistentTalentData.AddOrUpdateTalent(eventArgs.Data.Talents[k]);
			}
			if (!GarrisonStatus.Initialized)
			{
				this.GarrisonDataResetFinishedAction += this.HandleEnterWorld;
				GarrisonStatus.Initialized = true;
			}
			if (this.GarrisonDataResetFinishedAction != null)
			{
				this.GarrisonDataResetFinishedAction();
			}
			if (this.FollowerDataChangedAction != null)
			{
				this.FollowerDataChangedAction();
			}
			Singleton<Login>.Instance.MobileLoginDataRequestComplete();
		}

		private void StartMissionResultHandler(LegionCompanionWrapper.GarrisonStartMissionResultEvent eventArgs)
		{
			if (eventArgs.Result.Result != 0)
			{
				GARRISON_RESULT result = (GARRISON_RESULT)eventArgs.Result.Result;
				string text = result.ToString();
				if (result == GARRISON_RESULT.FOLLOWER_SOFT_CAP_EXCEEDED)
				{
					text = StaticDB.GetString("TOO_MANY_ACTIVE_CHAMPIONS", null);
					AllPopups.instance.ShowGenericPopup(StaticDB.GetString("MISSION_START_FAILED", null), text);
				}
				else if (result == GARRISON_RESULT.MISSION_MISSING_REQUIRED_FOLLOWER)
				{
					text = StaticDB.GetString("MISSING_REQUIRED_FOLLOWER", null);
					AllPopups.instance.ShowGenericPopup(StaticDB.GetString("MISSION_START_FAILED", null), text);
				}
				else
				{
					AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("MISSION_START_FAILED", null));
					Debug.Log("Mission start result: " + text);
				}
			}
			this.MobileRequestData();
		}

		private void CompleteMissionResultHandler(LegionCompanionWrapper.GarrisonCompleteMissionResultEvent eventArgs)
		{
			PersistentMissionData.UpdateMission(eventArgs.Result.Mission);
			AdventureMapMissionSite[] componentsInChildren = AdventureMapPanel.instance.m_mapViewContentsRT.GetComponentsInChildren<AdventureMapMissionSite>(true);
			foreach (AdventureMapMissionSite adventureMapMissionSite in componentsInChildren)
			{
				if (!adventureMapMissionSite.m_isStackablePreview)
				{
					if (adventureMapMissionSite.GetGarrMissionID() == eventArgs.Result.GarrMissionID)
					{
						if (!adventureMapMissionSite.gameObject.activeSelf)
						{
							adventureMapMissionSite.gameObject.SetActive(true);
						}
						adventureMapMissionSite.HandleCompleteMissionResult(eventArgs.Result.GarrMissionID, eventArgs.Result.BonusRollSucceeded);
						break;
					}
				}
			}
			LegionCompanionWrapper.RequestShipmentTypes((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestFollowerEquipment((int)GarrisonStatus.GarrisonFollowerType);
			LegionCompanionWrapper.RequestGarrisonData((int)GarrisonStatus.GarrisonType);
		}

		private void ClaimMissionBonusResultHandler(LegionCompanionWrapper.ClaimMissionBonusResultEvent msg)
		{
			PersistentMissionData.UpdateMission(msg.Result.Mission);
			AdventureMapMissionSite[] componentsInChildren = AdventureMapPanel.instance.m_mapViewContentsRT.GetComponentsInChildren<AdventureMapMissionSite>(true);
			foreach (AdventureMapMissionSite adventureMapMissionSite in componentsInChildren)
			{
				if (!adventureMapMissionSite.m_isStackablePreview)
				{
					if (adventureMapMissionSite.GetGarrMissionID() == msg.Result.GarrMissionID)
					{
						if (!adventureMapMissionSite.gameObject.activeSelf)
						{
							adventureMapMissionSite.gameObject.SetActive(true);
						}
						adventureMapMissionSite.HandleClaimMissionBonusResult(msg.Result.GarrMissionID, msg.Result.AwardOvermax, msg.Result.Result);
						break;
					}
				}
			}
		}

		private void MissionAddedHandler(LegionCompanionWrapper.MissionAddedEvent eventArgs)
		{
			if (eventArgs.Result == 0 && eventArgs.Mission.MissionRecID != 0)
			{
				PersistentMissionData.AddMission(eventArgs.Mission);
			}
			else
			{
				Debug.Log(string.Concat(new object[]
				{
					"Error adding mission: ",
					((GARRISON_RESULT)eventArgs.Result).ToString(),
					" Mission ID:",
					eventArgs.Mission.MissionRecID
				}));
			}
			if (this.MissionAddedAction != null)
			{
				this.MissionAddedAction(eventArgs.Mission.MissionRecID, eventArgs.Result);
			}
		}

		private void FollowerChangedXPHandler(LegionCompanionWrapper.FollowerChangedXpEvent eventArgs)
		{
			if (this.FollowerChangedXPAction != null)
			{
				this.FollowerChangedXPAction(eventArgs.OldFollower, eventArgs.Follower);
			}
		}

		private void ExpediteMissionCheatResultHandler(LegionCompanionWrapper.ExpediteMissionCheatResultEvent eventArgs)
		{
			if (eventArgs.Result == 0)
			{
				Debug.Log("Expedited completion of mission " + eventArgs.MissionRecID);
				LegionCompanionWrapper.RequestGarrisonData((int)GarrisonStatus.GarrisonType);
			}
			else
			{
				Debug.Log(string.Concat(new object[]
				{
					"MobileClientExpediteMissionCheatResult: Mission ID ",
					eventArgs.MissionRecID,
					" failed with error ",
					eventArgs.Result
				}));
			}
		}

		private void AdvanceMissionSetResultHandler(LegionCompanionWrapper.AdvanceMissionSetCheatResultEvent msg)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Advance mission set ",
				msg.MissionSetID,
				" success: ",
				msg.Success
			}));
		}

		public void UseEquipment(int garrFollowerID, int itemID, int replaceThisAbilityID)
		{
			if (this.UseEquipmentStartAction != null)
			{
				this.UseEquipmentStartAction(replaceThisAbilityID);
			}
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(garrFollowerID);
			LegionCompanionWrapper.UseFollowerEquipment((int)record.GarrFollowerTypeID, garrFollowerID, itemID, replaceThisAbilityID);
		}

		public void UseArmament(int garrFollowerID, int itemID)
		{
			if (this.UseArmamentStartAction != null)
			{
				this.UseArmamentStartAction(garrFollowerID);
			}
			GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(garrFollowerID);
			LegionCompanionWrapper.UseFollowerArmament((int)record.GarrFollowerTypeID, garrFollowerID, itemID);
		}
	}
}
