using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class Main : MonoBehaviour
	{
		private void Awake()
		{
			Main.instance = this;
			this.m_enableNotifications = true;
			this.GenerateUniqueIdentifier();
			this.canvasAnimator = this.mainCanvas.GetComponent<Animator>();
			this.SetNarrowScreen();
		}

		private void Start()
		{
			Application.targetFrameRate = 30;
			GarrisonStatus.ArtifactKnowledgeLevel = 0;
			GarrisonStatus.ArtifactXpMultiplier = 1f;
			MobileClient.RegisterHandlers();
			this.SubscribeToEvents();
		}

		private void Update()
		{
			this.UpdateDebugText();
			this.UpdateCanvasOrientation();
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

		private void UpdateCanvasOrientation()
		{
			if (Screen.width > Screen.height)
			{
				this.canvasAnimator.SetBool("isLandscape", true);
			}
			else
			{
				this.canvasAnimator.SetBool("isLandscape", false);
			}
		}

		private void PrecacheMissionChances()
		{
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && record.GarrFollowerTypeID == 4u)
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

		private void HandleEnterWorld()
		{
			Main main = Main.instance;
			main.GarrisonDataResetFinishedAction = (Action)Delegate.Remove(main.GarrisonDataResetFinishedAction, new Action(this.HandleEnterWorld));
			this.PrecacheMissionChances();
			LocalNotifications.RegisterForNotifications();
			SceneManager.LoadSceneAsync(Scenes.MainSceneName);
		}

		public void MobileLoggedIn()
		{
			PersistentArmamentData.ClearData();
			PersistentBountyData.ClearData();
			PersistentEquipmentData.ClearData();
			PersistentFollowerData.ClearData();
			PersistentFollowerData.ClearPreMissionFollowerData();
			PersistentMissionData.ClearData();
			PersistentShipmentData.ClearData();
			PersistentTalentData.ClearData();
			MissionDataCache.ClearData();
			WorldQuestData.ClearData();
			ItemStatCache.instance.ClearItemStats();
			GarrisonStatus.Initialized = false;
			Club.SetCommunityID(Singleton<CharacterData>.Instance.CommunityID);
			MobileClient.Initialize();
			this.MobileRequestData();
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
			GarrisonStatus.SetCurrencies(eventArgs.Data.GoldCurrency, 0, eventArgs.Data.OrderhallResourcesCurrency);
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
				Main main = Main.instance;
				main.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(main.GarrisonDataResetFinishedAction, new Action(this.HandleEnterWorld));
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
			componentsInChildren = AdventureMapPanel.instance.m_missionAndWorldQuestArea_Argus.GetComponentsInChildren<AdventureMapMissionSite>(true);
			foreach (AdventureMapMissionSite adventureMapMissionSite2 in componentsInChildren)
			{
				if (!adventureMapMissionSite2.m_isStackablePreview)
				{
					if (adventureMapMissionSite2.GetGarrMissionID() == eventArgs.Result.GarrMissionID)
					{
						if (!adventureMapMissionSite2.gameObject.activeSelf)
						{
							adventureMapMissionSite2.gameObject.SetActive(true);
						}
						adventureMapMissionSite2.HandleCompleteMissionResult(eventArgs.Result.GarrMissionID, eventArgs.Result.BonusRollSucceeded);
						break;
					}
				}
			}
			LegionCompanionWrapper.RequestShipmentTypes((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestFollowerEquipment(4);
			LegionCompanionWrapper.RequestGarrisonData(3);
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
			componentsInChildren = AdventureMapPanel.instance.m_missionAndWorldQuestArea_Argus.GetComponentsInChildren<AdventureMapMissionSite>(true);
			foreach (AdventureMapMissionSite adventureMapMissionSite2 in componentsInChildren)
			{
				if (!adventureMapMissionSite2.m_isStackablePreview)
				{
					if (adventureMapMissionSite2.GetGarrMissionID() == msg.Result.GarrMissionID)
					{
						if (!adventureMapMissionSite2.gameObject.activeSelf)
						{
							adventureMapMissionSite2.gameObject.SetActive(true);
						}
						adventureMapMissionSite2.HandleClaimMissionBonusResult(msg.Result.GarrMissionID, msg.Result.AwardOvermax, msg.Result.Result);
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
				LegionCompanionWrapper.RequestGarrisonData(3);
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

		public void MobileRequestData()
		{
			LegionCompanionWrapper.RequestShipmentTypes((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
			LegionCompanionWrapper.RequestWorldQuestBounties(4);
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

		private void UpdateDebugText()
		{
			this.m_frameCount++;
		}

		public void OnQuitButton()
		{
			Application.Quit();
		}

		private void OnApplicationQuit()
		{
			if (Network.IsConnected() && Network.IsLoggedIn())
			{
				Network.Logout();
			}
			Singleton<Login>.instance.BnQuit();
			MobileClient.Shutdown();
		}

		private void OnDestroy()
		{
			this.UnsubscribeFromEvents();
			if (SceneManager.GetActiveScene().name != Scenes.TitleSceneName)
			{
				CommunityData.Instance.Shutdown();
				MobileClient.Disconnect();
			}
		}

		public void ClearPendingNotifications()
		{
			LocalNotifications.ClearPending();
		}

		public void ScheduleNotifications()
		{
			this.ClearPendingNotifications();
			if (!Main.instance.m_enableNotifications)
			{
				return;
			}
			List<NotificationData> list = new List<NotificationData>();
			ICollection<WrapperGarrisonMission> values = PersistentMissionData.missionDictionary.Values;
			foreach (WrapperGarrisonMission wrapperGarrisonMission in values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
				{
					if (wrapperGarrisonMission.MissionState == 1)
					{
						if ((record.Flags & 16u) == 0u)
						{
							TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
							TimeSpan timeRemaining = wrapperGarrisonMission.MissionDuration - t;
							list.Add(new NotificationData
							{
								notificationText = record.Name,
								timeRemaining = timeRemaining,
								notificationType = NotificationType.missionCompete
							});
						}
					}
				}
			}
			foreach (WrapperCharacterShipment wrapperCharacterShipment in PersistentShipmentData.shipmentDictionary.Values)
			{
				CharShipmentRec record2 = StaticDB.charShipmentDB.GetRecord(wrapperCharacterShipment.ShipmentRecID);
				if (record2 == null)
				{
					Debug.LogError("Invalid Shipment ID: " + wrapperCharacterShipment.ShipmentRecID);
				}
				else
				{
					string notificationText = "Invalid";
					if (record2.GarrFollowerID > 0u)
					{
						GarrFollowerRec record3 = StaticDB.garrFollowerDB.GetRecord((int)record2.GarrFollowerID);
						if (record3 == null)
						{
							Debug.LogError("Invalid Follower ID: " + record2.GarrFollowerID);
							continue;
						}
						int num = (GarrisonStatus.Faction() != PVP_FACTION.HORDE) ? record3.AllianceCreatureID : record3.HordeCreatureID;
						CreatureRec record4 = StaticDB.creatureDB.GetRecord(num);
						if (record4 == null)
						{
							Debug.LogError("Invalid Creature ID: " + num);
							continue;
						}
						notificationText = record4.Name;
					}
					if (record2.DummyItemID > 0)
					{
						ItemRec record5 = StaticDB.itemDB.GetRecord(record2.DummyItemID);
						if (record5 == null)
						{
							Debug.LogError("Invalid Item ID: " + record2.DummyItemID);
							continue;
						}
						notificationText = record5.Display;
					}
					TimeSpan t2 = GarrisonStatus.CurrentTime() - wrapperCharacterShipment.CreationTime;
					TimeSpan timeRemaining2 = wrapperCharacterShipment.ShipmentDuration - t2;
					list.Add(new NotificationData
					{
						notificationText = notificationText,
						timeRemaining = timeRemaining2,
						notificationType = NotificationType.workOrderReady
					});
				}
			}
			foreach (WrapperGarrisonTalent wrapperGarrisonTalent in PersistentTalentData.talentDictionary.Values)
			{
				if ((wrapperGarrisonTalent.Flags & 1) == 0)
				{
					if (!(wrapperGarrisonTalent.StartTime <= DateTime.UtcNow))
					{
						GarrTalentRec record6 = StaticDB.garrTalentDB.GetRecord(wrapperGarrisonTalent.GarrTalentID);
						if (record6 != null)
						{
							TimeSpan timeRemaining3 = TimeSpan.Zero;
							if ((wrapperGarrisonTalent.Flags & 2) == 0)
							{
								timeRemaining3 = TimeSpan.FromSeconds((double)record6.ResearchDurationSecs) - (GarrisonStatus.CurrentTime() - wrapperGarrisonTalent.StartTime);
							}
							else
							{
								timeRemaining3 = TimeSpan.FromSeconds((double)record6.RespecDurationSecs) - (GarrisonStatus.CurrentTime() - wrapperGarrisonTalent.StartTime);
							}
							list.Add(new NotificationData
							{
								notificationText = record6.Name,
								timeRemaining = timeRemaining3,
								notificationType = NotificationType.talentReady
							});
						}
					}
				}
			}
			int num2 = 0;
			foreach (NotificationData notificationData in from n in list
			orderby n.timeRemaining
			select n)
			{
				if (notificationData.notificationType == NotificationType.missionCompete)
				{
					LocalNotifications.ScheduleMissionCompleteNotification(notificationData.notificationText, ++num2, Convert.ToInt64(notificationData.timeRemaining.TotalSeconds));
				}
				if (notificationData.notificationType == NotificationType.workOrderReady)
				{
					LocalNotifications.ScheduleWorkOrderReadyNotification(notificationData.notificationText, ++num2, Convert.ToInt64(notificationData.timeRemaining.TotalSeconds));
				}
				if (notificationData.notificationType == NotificationType.talentReady)
				{
					LocalNotifications.ScheduleTalentResearchCompleteNotification(notificationData.notificationText, ++num2, Convert.ToInt64(notificationData.timeRemaining.TotalSeconds));
				}
				Debug.Log(string.Concat(new object[]
				{
					"Scheduling Notification for [",
					notificationData.notificationType,
					"] ",
					notificationData.notificationText,
					" (",
					num2,
					") in ",
					notificationData.timeRemaining.TotalSeconds,
					" seconds"
				}));
			}
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
			Debug.Log("Main.CompleteAllMissions()");
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && record.GarrFollowerTypeID == 4u)
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

		public string GetLocale()
		{
			if (this.m_locale == null || this.m_locale == string.Empty)
			{
				this.m_locale = MobileDeviceLocale.GetBestGuessForLocale();
			}
			return this.m_locale;
		}

		public void RequestEmissaryFactions()
		{
			LegionCompanionWrapper.RequestEmissaryFactions(1895);
		}

		public void RequestWorldQuests()
		{
			LegionCompanionWrapper.RequestWorldQuests();
		}

		private void EmissaryFactionUpdateHandler(LegionCompanionWrapper.EmissaryFactionsUpdateEvent eventArgs)
		{
			this.allPopups.EmissaryFactionUpdate(eventArgs.Factions);
		}

		private void CreateShipmentResultHandler(LegionCompanionWrapper.CreateShipmentResultEvent eventArgs)
		{
			GARRISON_RESULT result = (GARRISON_RESULT)eventArgs.Result;
			if (result != GARRISON_RESULT.SUCCESS)
			{
			}
			if (this.CreateShipmentResultAction != null)
			{
				this.CreateShipmentResultAction(eventArgs.Result);
			}
			if (result == GARRISON_RESULT.SUCCESS)
			{
				LegionCompanionWrapper.RequestShipmentTypes((int)GarrisonStatus.GarrisonType);
				LegionCompanionWrapper.RequestShipments((int)GarrisonStatus.GarrisonType);
				LegionCompanionWrapper.RequestGarrisonData(3);
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
			return mapID == 1220 || mapID == 1669 || mapID == 1642 || mapID == 1643;
		}

		private void WorldQuestUpdateHandler(LegionCompanionWrapper.RequestWorldQuestsResultEvent eventArgs)
		{
			WorldQuestData.ClearData();
			foreach (WrapperWorldQuest worldQuest in eventArgs.Quests)
			{
				bool flag = Main.DoesMapIDSupportWorldQuests(worldQuest.StartLocationMapID);
				if (flag)
				{
					WorldQuestData.AddWorldQuest(worldQuest);
					for (int i = 0; i < worldQuest.Items.Count; i++)
					{
						ItemStatCache.instance.GetItemStats(worldQuest.Items[i].RecordID, worldQuest.Items[i].ItemContext);
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
			else
			{
				Debug.Log("MobileClientEvaluateMissionResult failed with error " + ((GARRISON_RESULT)eventArgs.Result).ToString());
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
			LegionCompanionWrapper.RequestFollowerEquipment(4);
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
				LegionCompanionWrapper.RequestFollowerArmamentsExtended(4);
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
			if (eventArgs.Visible)
			{
			}
			if (eventArgs.LockedQuestID != 0)
			{
			}
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
			ItemStatCache.instance.AddMobileItemStats(eventArgs.ItemID, eventArgs.ItemContext, eventArgs.Stats);
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

		public void SetDebugText(string newText)
		{
			this.m_debugText.text = newText;
			this.m_debugButton.SetActive(true);
		}

		public void HideDebugText()
		{
			this.m_debugButton.SetActive(false);
		}

		private void OnApplicationPause(bool pauseStatus)
		{
		}

		public void Logout()
		{
			MissionDataCache.ClearData();
			AllPopups.instance.HideAllPopups();
			AllPanels.instance.ShowOrderHallMultiPanel(false);
			AllPanels.instance.ShowCompanionMultiPanel(false);
			Singleton<Login>.instance.ReconnectToMobileServerCharacterSelect();
		}

		public void SelectCompanionNavButton(CompanionNavButton navButton)
		{
			if (this.CompanionNavButtonSelectionAction != null)
			{
				this.CompanionNavButtonSelectionAction(navButton);
			}
		}

		public void SelectOrderHallFilterOptionsButton(OrderHallFilterOptionsButton filterOptionsButton)
		{
			if (this.OrderHallfilterOptionsButtonSelectedAction != null)
			{
				this.OrderHallfilterOptionsButtonSelectedAction(filterOptionsButton);
			}
		}

		private static string getMd5Hash(string input)
		{
			if (input == string.Empty)
			{
				return string.Empty;
			}
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = md5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private void GenerateUniqueIdentifier()
		{
			bool flag = false;
			string text = string.Empty;
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.content.Context");
			string static2 = androidJavaClass2.GetStatic<string>("TELEPHONY_SERVICE");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getSystemService", new object[]
			{
				static2
			});
			bool flag2 = false;
			try
			{
				text = androidJavaObject.Call<string>("getDeviceId", new object[0]);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
				flag2 = true;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if ((flag2 && !flag) || (!flag2 && text == string.Empty && flag))
			{
				AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("android.provider.Settings$Secure");
				string static3 = androidJavaClass3.GetStatic<string>("ANDROID_ID");
				AndroidJavaObject androidJavaObject2 = @static.Call<AndroidJavaObject>("getContentResolver", new object[0]);
				text = androidJavaClass3.CallStatic<string>("getString", new object[]
				{
					androidJavaObject2,
					static3
				});
				if (text == null)
				{
					text = string.Empty;
				}
			}
			if (text == string.Empty)
			{
				string text2 = "00000000000000000000000000000000";
				try
				{
					StreamReader streamReader = new StreamReader("/sys/class/net/wlan0/address");
					text2 = streamReader.ReadLine();
					streamReader.Close();
				}
				catch (Exception ex2)
				{
					Debug.Log(ex2.ToString());
				}
				text = text2.Replace(":", string.Empty);
			}
			Main.uniqueIdentifier = Main.getMd5Hash(text);
		}

		public void CheatFastForwardOneHour()
		{
			GarrisonStatus.CheatFastForwardOneHour();
		}

		private void SetNarrowScreen()
		{
			float num = 1f;
			float num2 = (float)Screen.currentResolution.height;
			float num3 = (float)Screen.currentResolution.width;
			if (num3 > 0f && num2 > 0f)
			{
				if (num2 > num3)
				{
					num = num2 / num3;
				}
				else
				{
					num = num3 / num2;
				}
			}
			if (num > 1.9f)
			{
				this.m_narrowScreen = true;
			}
		}

		public bool IsNarrowScreen()
		{
			return this.m_narrowScreen;
		}

		public void NudgeX(ref GameObject gameObj, float amount)
		{
			if (gameObj != null)
			{
				RectTransform component = gameObj.GetComponent<RectTransform>();
				if (component)
				{
					Vector2 anchoredPosition = component.anchoredPosition;
					anchoredPosition.x += amount;
					component.anchoredPosition = anchoredPosition;
				}
			}
		}

		public void NudgeY(ref GameObject gameObj, float amount)
		{
			if (gameObj != null)
			{
				RectTransform component = gameObj.GetComponent<RectTransform>();
				if (component)
				{
					Vector2 anchoredPosition = component.anchoredPosition;
					anchoredPosition.y += amount;
					component.anchoredPosition = anchoredPosition;
				}
			}
		}

		public GameObject AddChildToLevel2Canvas(GameObject prefab)
		{
			GameObject gameObject = this.InstantiateBasedOnPrefab(prefab);
			this.AddChildToObject(this.m_level2Canvas.transform, gameObject.transform);
			return gameObject;
		}

		public GameObject AddChildToLevel3Canvas(GameObject prefab)
		{
			GameObject gameObject = this.InstantiateBasedOnPrefab(prefab);
			this.AddChildToObject(this.m_level3Canvas.transform, gameObject.transform);
			return gameObject;
		}

		private GameObject InstantiateBasedOnPrefab(GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			RectTransform rectTransform = gameObject.transform as RectTransform;
			RectTransform rectTransform2 = prefab.transform as RectTransform;
			rectTransform.anchorMin = rectTransform2.anchorMin;
			rectTransform.anchorMax = rectTransform2.anchorMax;
			RectTransform rectTransform3 = rectTransform;
			Vector2 zero = Vector2.zero;
			rectTransform.offsetMax = zero;
			rectTransform3.offsetMin = zero;
			return gameObject;
		}

		private void AddChildToObject(Transform parent, Transform child)
		{
			child.SetParent(parent, false);
		}

		private int m_frameCount;

		public Canvas mainCanvas;

		public Canvas m_level2Canvas;

		public Canvas m_level3Canvas;

		private Animator canvasAnimator;

		public AllPanels allPanels;

		public AllPopups allPopups;

		private string m_unknownMsg;

		public UISound m_UISound;

		public bool m_enableNotifications;

		public static Main instance;

		public Action FollowerDataChangedAction;

		public Action<int> MissionSuccessChanceChangedAction;

		public Action GarrisonDataResetStartedAction;

		public Action GarrisonDataResetFinishedAction;

		public Action ShipmentTypesUpdatedAction;

		public Action<int> CreateShipmentResultAction;

		public Action<int, ulong> ShipmentAddedAction;

		public Action<SHIPMENT_RESULT, ulong> CompleteShipmentResultAction;

		public Action<int, int> MissionAddedAction;

		public Action<WrapperGarrisonFollower, WrapperGarrisonFollower> FollowerChangedXPAction;

		public Action<WrapperGarrisonFollower> TroopExpiredAction;

		public Action<int, int, string> CanResearchGarrisonTalentResultAction;

		public Action<int, int> ResearchGarrisonTalentResultAction;

		public Action BountyInfoUpdatedAction;

		public Action<int> UseEquipmentStartAction;

		public Action<WrapperGarrisonFollower, WrapperGarrisonFollower> UseEquipmentResultAction;

		public Action EquipmentInventoryChangedAction;

		public Action<int> UseArmamentStartAction;

		public Action<int, WrapperGarrisonFollower, WrapperGarrisonFollower> UseArmamentResultAction;

		public Action ArmamentInventoryChangedAction;

		public Action<OrderHallFilterOptionsButton> OrderHallfilterOptionsButtonSelectedAction;

		public Action<CompanionNavButton> CompanionNavButtonSelectionAction;

		public Action<int, WrapperShipmentItem> ShipmentItemPushedAction;

		public Action<int> PlayerLeveledUpAction;

		public Action MakeContributionRequestInitiatedAction;

		public Action ContributionInfoChangedAction;

		public Action InvasionPOIChangedAction;

		public Action ArtifactKnowledgeInfoChangedAction;

		public Action MaxActiveFollowersChangedAction;

		public Action<int, int, int> GotItemFromQuestCompletionAction;

		public GameObject m_debugButton;

		public Text m_debugText;

		private string m_locale;

		public GameClient m_gameClientScript;

		public static string uniqueIdentifier;

		public CanvasBlurManager m_canvasBlurManager;

		public BackButtonManager m_backButtonManager;

		private bool m_narrowScreen;

		public interface SetCache
		{
			void SetCachePath(string cachePath);
		}
	}
}
