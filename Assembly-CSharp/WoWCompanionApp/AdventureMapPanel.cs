using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class AdventureMapPanel : MonoBehaviour
	{
		private void OnEnable()
		{
			AdventureMapPanel.instance = this;
			this.MapFiltersChanged = (Action)Delegate.Combine(this.MapFiltersChanged, new Action(this.UpdateWorldQuests));
		}

		private void OnDisable()
		{
			AdventureMapPanel.instance = this;
			this.MapFiltersChanged = (Action)Delegate.Remove(this.MapFiltersChanged, new Action(this.UpdateWorldQuests));
		}

		public void DeselectAllFollowerListItems()
		{
			if (this.DeselectAllFollowerListItemsAction != null)
			{
				this.DeselectAllFollowerListItemsAction();
			}
		}

		public void ShowFollowerDetails(bool show)
		{
			if (this.OnShowFollowerDetails != null)
			{
				this.OnShowFollowerDetails(show);
			}
		}

		public void SetFollowerToInspect(int garrFollowerID)
		{
			this.m_followerToInspect = garrFollowerID;
			if (this.FollowerToInspectChangedAction != null)
			{
				this.FollowerToInspectChangedAction(garrFollowerID);
			}
		}

		public int GetFollowerToInspect()
		{
			return this.m_followerToInspect;
		}

		public void EnableMapFilter(MapFilterType mapFilterType, bool enable)
		{
			for (int i = 0; i < this.m_mapFilters.Length; i++)
			{
				this.m_mapFilters[i] = false;
			}
			this.m_mapFilters[(int)mapFilterType] = true;
			AllPopups.instance.m_optionsDialog.SyncWithOptions();
			if (this.MapFiltersChanged != null)
			{
				this.MapFiltersChanged();
			}
		}

		public bool IsFilterEnabled(MapFilterType mapFilterType)
		{
			return this.m_mapFilters[(int)mapFilterType];
		}

		public void AddMissionLootToRewardPanel(int garrMissionID)
		{
			if (this.OnAddMissionLootToRewardPanel != null)
			{
				this.OnAddMissionLootToRewardPanel(garrMissionID);
			}
		}

		public void ShowRewardPanel(bool show)
		{
			if (this.OnShowMissionRewardPanel != null)
			{
				this.OnShowMissionRewardPanel(show);
			}
		}

		public int GetCurrentMapMission()
		{
			return this.m_currentMapMission;
		}

		public void SelectMissionFromMap(int garrMissionID)
		{
			if (this.m_currentMapMission != garrMissionID)
			{
				this.m_secondsMissionHasBeenSelected = 0f;
				this.m_currentMapMission = garrMissionID;
				if (this.MissionMapSelectionChangedAction != null)
				{
					this.MissionMapSelectionChangedAction(this.m_currentMapMission);
				}
			}
			if (this.MissionSelectedFromMapAction != null)
			{
				this.MissionSelectedFromMapAction(this.m_currentMapMission);
			}
			if (garrMissionID > 0)
			{
				this.SelectWorldQuest(0);
			}
		}

		public int GetCurrentListMission()
		{
			return this.m_currentListMission;
		}

		public void SelectMissionFromList(int garrMissionID)
		{
			this.m_currentListMission = garrMissionID;
			if (this.MissionSelectedFromListAction != null)
			{
				this.MissionSelectedFromListAction(garrMissionID);
			}
		}

		public int GetCurrentWorldQuest()
		{
			return this.m_currentWorldQuest;
		}

		public void SelectWorldQuest(int worldQuestID)
		{
			this.m_currentWorldQuest = worldQuestID;
			if (this.WorldQuestChangedAction != null)
			{
				this.WorldQuestChangedAction(this.m_currentWorldQuest);
			}
			if (worldQuestID > 0)
			{
				this.SelectMissionFromMap(0);
			}
		}

		public void SetMissionIconScale(float val)
		{
			this.m_testMissionIconScale = val;
			if (this.TestIconSizeChanged != null)
			{
				this.TestIconSizeChanged(this.m_testMissionIconScale);
			}
		}

		public void SetSelectedIconContainer(StackableMapIconContainer container)
		{
			this.m_iconContainer = container;
			if (this.SelectedIconContainerChanged != null)
			{
				this.SelectedIconContainerChanged(container);
			}
		}

		public StackableMapIconContainer GetSelectedIconContainer()
		{
			return this.m_iconContainer;
		}

		private void Awake()
		{
			AdventureMapPanel.instance = this;
			this.m_zoneID = AdventureMapPanel.eZone.None;
			this.m_testMissionIconScale = 1f;
			this.m_mapFilters = new bool[18];
			for (int i = 0; i < this.m_mapFilters.Length; i++)
			{
				this.m_mapFilters[i] = false;
			}
			this.EnableMapFilter(MapFilterType.All, true);
			if (this.m_missionResultsPanel)
			{
				this.m_missionResultsPanel.gameObject.SetActive(true);
			}
		}

		private void Start()
		{
			this.m_pinchZoomContentManager.SetZoom(1f, false);
			StackableMapIconContainer[] componentsInChildren = this.m_mapViewContentsRT.GetComponentsInChildren<StackableMapIconContainer>(true);
			foreach (StackableMapIconContainer stackableMapIconContainer in componentsInChildren)
			{
				Object.Destroy(stackableMapIconContainer.gameObject);
			}
			this.InitMissionSites();
			this.UpdateWorldQuests();
			this.HandleBountyInfoUpdated();
			Main main = Main.instance;
			main.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(main.GarrisonDataResetFinishedAction, new Action(this.InitMissionSites));
			Main main2 = Main.instance;
			main2.MissionAddedAction = (Action<int, int>)Delegate.Combine(main2.MissionAddedAction, new Action<int, int>(this.HandleMissionAdded));
			Main main3 = Main.instance;
			main3.BountyInfoUpdatedAction = (Action)Delegate.Combine(main3.BountyInfoUpdatedAction, new Action(this.HandleBountyInfoUpdated));
		}

		private void HandleMissionAdded(int garrMissionID, int result)
		{
		}

		private void CreateMissionSite(int garrMissionID)
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
			if (record == null)
			{
				Debug.LogWarning("Mission Not Found: ID " + garrMissionID);
				return;
			}
			if (record.GarrFollowerTypeID != (uint)GarrisonStatus.GarrisonFollowerType)
			{
				return;
			}
			if ((record.Flags & 16u) != 0u)
			{
				return;
			}
			if (!PersistentMissionData.missionDictionary.ContainsKey(garrMissionID))
			{
				return;
			}
			if (PersistentMissionData.missionDictionary[garrMissionID].MissionState == 0)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(AdventureMapPanel.instance.m_AdvMapMissionSitePrefab);
			gameObject.transform.SetParent(this.m_mapViewContentsRT, false);
			float num = 1.84887111f;
			float num2 = record.Mappos_x * num;
			float num3 = record.Mappos_y * -num;
			float num4 = -272.5694f;
			float num5 = 1318.388f;
			num2 += num4;
			num3 += num5;
			float num6 = 1f;
			float num7 = 1f;
			Vector2 vector = new Vector3(num2 / num6, num3 / num7);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = vector;
			component.anchorMax = vector;
			component.anchoredPosition = Vector2.zero;
			AdventureMapMissionSite component2 = gameObject.GetComponent<AdventureMapMissionSite>();
			component2.SetMission(record.ID);
			StackableMapIcon component3 = gameObject.GetComponent<StackableMapIcon>();
			if (component3 != null)
			{
				component3.RegisterWithManager(record.AreaID);
			}
		}

		private void InitMissionSites()
		{
			if (this.OnInitMissionSites != null)
			{
				this.OnInitMissionSites();
			}
			AdventureMapMissionSite[] componentsInChildren = this.m_mapViewContentsRT.GetComponentsInChildren<AdventureMapMissionSite>(true);
			foreach (AdventureMapMissionSite adventureMapMissionSite in componentsInChildren)
			{
				if (adventureMapMissionSite != null)
				{
					StackableMapIcon component = adventureMapMissionSite.GetComponent<StackableMapIcon>();
					GameObject gameObject = adventureMapMissionSite.gameObject;
					if (component != null)
					{
						component.RemoveFromContainer();
					}
					if (gameObject != null)
					{
						Object.Destroy(adventureMapMissionSite.gameObject);
					}
				}
			}
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				this.CreateMissionSite(wrapperGarrisonMission.MissionRecID);
			}
		}

		private static void ClearWorldQuestArea(GameObject questArea)
		{
			AdventureMapWorldQuest[] componentsInChildren = questArea.GetComponentsInChildren<AdventureMapWorldQuest>(true);
			foreach (AdventureMapWorldQuest adventureMapWorldQuest in componentsInChildren)
			{
				StackableMapIcon component = adventureMapWorldQuest.GetComponent<StackableMapIcon>();
				GameObject gameObject = adventureMapWorldQuest.gameObject;
				if (component != null)
				{
					component.RemoveFromContainer();
				}
				if (gameObject != null)
				{
					Object.Destroy(adventureMapWorldQuest.gameObject);
				}
			}
		}

		public void UpdateWorldQuests()
		{
			AdventureMapPanel.ClearWorldQuestArea(this.m_missionAndWorldQuestArea_KulTiras);
			AdventureMapPanel.ClearWorldQuestArea(this.m_missionAndWorldQuestArea_Zandalar);
			foreach (WrapperWorldQuest worldQuest in WorldQuestData.WorldQuestDictionary.Values)
			{
				if (worldQuest.StartLocationMapID != 1220 && worldQuest.StartLocationMapID != 1669)
				{
					if (!this.IsFilterEnabled(MapFilterType.All))
					{
						bool flag = false;
						if (!flag && this.IsFilterEnabled(MapFilterType.Azerite))
						{
							flag |= worldQuest.Currencies.Any((WrapperWorldQuestReward reward) => reward.RecordID == 1553);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.OrderResources))
						{
							flag |= worldQuest.Currencies.Any((WrapperWorldQuestReward reward) => reward.RecordID == 1560);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Gold) && worldQuest.Money > 0)
						{
							flag = true;
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Gear))
						{
							flag |= worldQuest.Items.Any(delegate(WrapperWorldQuestReward reward)
							{
								ItemRec record = StaticDB.itemDB.GetRecord(reward.RecordID);
								return record != null && (record.ClassID == 2 || record.ClassID == 3 || record.ClassID == 4 || record.ClassID == 6);
							});
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.ProfessionMats))
						{
							flag |= worldQuest.Items.Any(delegate(WrapperWorldQuestReward reward)
							{
								ItemRec record = StaticDB.itemDB.GetRecord(reward.RecordID);
								return record != null && record.ClassID == 7;
							});
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.PetBattles))
						{
							flag |= (worldQuest.QuestInfoID == 115);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Reputation))
						{
							flag |= worldQuest.Currencies.Any(delegate(WrapperWorldQuestReward reward)
							{
								CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(reward.RecordID);
								return record != null && record.FactionID != 0u;
							});
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_ChampionsOfAzeroth))
						{
							bool flag2 = flag;
							bool flag3;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag3 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50562);
							}
							else
							{
								flag3 = false;
							}
							flag = (flag2 || flag3);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_ZandalariEmpire))
						{
							bool flag4 = flag;
							bool flag5;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag5 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50598);
							}
							else
							{
								flag5 = false;
							}
							flag = (flag4 || flag5);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_ProudmooreAdmiralty))
						{
							bool flag6 = flag;
							bool flag7;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag7 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50599);
							}
							else
							{
								flag7 = false;
							}
							flag = (flag6 || flag7);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_OrderOfEmbers))
						{
							bool flag8 = flag;
							bool flag9;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag9 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50600);
							}
							else
							{
								flag9 = false;
							}
							flag = (flag8 || flag9);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_StormsWake))
						{
							bool flag10 = flag;
							bool flag11;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag11 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50601);
							}
							else
							{
								flag11 = false;
							}
							flag = (flag10 || flag11);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_TalanjisExpedition))
						{
							bool flag12 = flag;
							bool flag13;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag13 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50602);
							}
							else
							{
								flag13 = false;
							}
							flag = (flag12 || flag13);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_Voldunai))
						{
							bool flag14 = flag;
							bool flag15;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag15 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50603);
							}
							else
							{
								flag15 = false;
							}
							flag = (flag14 || flag15);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_TortollanSeekers))
						{
							bool flag16 = flag;
							bool flag17;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag17 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50604);
							}
							else
							{
								flag17 = false;
							}
							flag = (flag16 || flag17);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_AllianceWarEffort))
						{
							bool flag18 = flag;
							bool flag19;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag19 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50605);
							}
							else
							{
								flag19 = false;
							}
							flag = (flag18 || flag19);
						}
						if (!flag && this.IsFilterEnabled(MapFilterType.Bounty_HordeWarEffort))
						{
							bool flag20 = flag;
							bool flag21;
							if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
							{
								flag21 = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID].BountyQuestIDs.Any((int questID) => questID == 50606);
							}
							else
							{
								flag21 = false;
							}
							flag = (flag20 || flag21);
						}
						if (!flag)
						{
							continue;
						}
					}
					GameObject gameObject = Object.Instantiate<GameObject>(AdventureMapPanel.instance.m_AdvMapWorldQuestPrefab);
					if (worldQuest.StartLocationMapID == 1642)
					{
						gameObject.transform.SetParent(this.m_missionAndWorldQuestArea_Zandalar.transform, false);
						float num = 0.152715057f;
						float num2 = 1250.88025f;
						float num3 = 697.2115f;
						if (worldQuest.WorldMapAreaID == 863)
						{
							num -= 0.02f;
						}
						else if (worldQuest.WorldMapAreaID == 864)
						{
							num2 += 60f;
							num3 -= 20f;
						}
						this.SetupWorldQuestIcon(worldQuest, gameObject, num2, num3, num);
					}
					else if (worldQuest.StartLocationMapID == 1643)
					{
						gameObject.transform.SetParent(this.m_missionAndWorldQuestArea_KulTiras.transform, false);
						float mapScale = 0.152715057f;
						float mapOffsetX = 1150.88025f;
						float mapOffsetY = 497.2115f;
						this.SetupWorldQuestIcon(worldQuest, gameObject, mapOffsetX, mapOffsetY, mapScale);
					}
					AdventureMapWorldQuest component = gameObject.GetComponent<AdventureMapWorldQuest>();
					component.SetQuestID(worldQuest.QuestID);
					StackableMapIcon component2 = gameObject.GetComponent<StackableMapIcon>();
					if (component2 != null)
					{
						component2.RegisterWithManager(worldQuest.StartLocationMapID);
					}
				}
			}
			this.m_pinchZoomContentManager.ForceZoomFactorChanged();
		}

		private static int GetImageWByMapID(int startLocationMapId)
		{
			if (startLocationMapId == 1642)
			{
				return 1985;
			}
			if (startLocationMapId != 1643)
			{
				return 0;
			}
			return 1985;
		}

		private void SetupWorldQuestIcon(WrapperWorldQuest worldQuest, GameObject worldQuestObj, float mapOffsetX, float mapOffsetY, float mapScale)
		{
			float num = (float)worldQuest.StartLocationY * -mapScale;
			float num2 = (float)worldQuest.StartLocationX * mapScale;
			num += mapOffsetX;
			num2 += mapOffsetY;
			float num3 = (float)AdventureMapPanel.GetImageWByMapID(worldQuest.StartLocationMapID);
			float num4 = 1334f;
			Vector2 vector = new Vector3(num / num3, num2 / num4);
			RectTransform component = worldQuestObj.GetComponent<RectTransform>();
			component.anchorMin = vector;
			component.anchorMax = vector;
			component.anchoredPosition = Vector2.zero;
		}

		private void Update()
		{
			this.m_currentVisibleZone = null;
			if (this.m_currentMapMission > 0)
			{
				this.m_secondsMissionHasBeenSelected += Time.deltaTime;
			}
			this.UpdateCompletedMissionsDisplay();
		}

		private void ZoomOutTweenCallback(float newZoomFactor)
		{
			this.m_pinchZoomContentManager.SetZoom(newZoomFactor, true);
		}

		private void ZoomInTweenCallback(float newZoomFactor)
		{
			this.m_pinchZoomContentManager.SetZoom(newZoomFactor, false);
		}

		public void CenterAndZoomOut()
		{
			if (this.m_adventureMapOrderHallNavButton && this.m_adventureMapOrderHallNavButton.IsSelected())
			{
				this.CenterAndZoom(Vector2.zero, null, false);
			}
		}

		public void CenterAndZoomIn()
		{
			if (Input.touchCount != 1)
			{
				return;
			}
			Vector2 position = Input.GetTouch(0).position;
			this.CenterAndZoom(position, null, true);
		}

		public void ShowWorldMap(bool show)
		{
			this.m_mainMapInfo.gameObject.SetActive(show);
		}

		public void CenterAndZoom(Vector2 tapPos, ZoneButton zoneButton, bool zoomIn)
		{
			iTween.Stop(this.m_mapViewContentsRT.gameObject);
			iTween.Stop(base.gameObject);
			this.m_lastTappedZoneButton = zoneButton;
			Vector3[] array = new Vector3[4];
			this.m_mapViewRT.GetWorldCorners(array);
			float num = array[2].x - array[0].x;
			float num2 = array[2].y - array[0].y;
			Vector2 vector;
			vector.x = array[0].x + num * 0.5f;
			vector.y = array[0].y + num2 * 0.5f;
			Vector3[] array2 = new Vector3[4];
			this.m_mapViewContentsRT.GetWorldCorners(array2);
			float num3 = array2[2].x - array2[0].x;
			float num4 = array2[2].y - array2[0].y;
			Vector2 vector2;
			vector2.x = array2[0].x + num3 * 0.5f;
			vector2.y = array2[0].y + num4 * 0.5f;
			MapInfo componentInChildren = base.GetComponentInChildren<MapInfo>();
			if (componentInChildren == null)
			{
				return;
			}
			if (zoomIn)
			{
				if (this.m_pinchZoomContentManager.m_zoomFactor < 1.001f)
				{
					Main.instance.m_UISound.Play_MapZoomIn();
				}
				Vector2 vector3 = tapPos - vector2;
				vector3 *= componentInChildren.m_maxZoomFactor / this.m_pinchZoomContentManager.m_zoomFactor;
				Vector2 vector4 = vector2 + vector3;
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"name",
					"Zoom View In",
					"from",
					this.m_pinchZoomContentManager.m_zoomFactor,
					"to",
					componentInChildren.m_maxZoomFactor,
					"easeType",
					"easeOutCubic",
					"time",
					0.8f,
					"onupdate",
					"ZoomInTweenCallback"
				}));
				iTween.MoveBy(this.m_mapViewContentsRT.gameObject, iTween.Hash(new object[]
				{
					"name",
					"Pan View To Point (in)",
					"x",
					vector.x - vector4.x,
					"y",
					vector.y - vector4.y,
					"easeType",
					"easeOutQuad",
					"time",
					0.8f
				}));
			}
			else
			{
				if (this.OnZoomOutMap != null)
				{
					Main.instance.m_UISound.Play_MapZoomOut();
					this.OnZoomOutMap();
				}
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"name",
					"Zoom View Out",
					"from",
					this.m_pinchZoomContentManager.m_zoomFactor,
					"to",
					componentInChildren.m_minZoomFactor,
					"easeType",
					"easeOutCubic",
					"time",
					0.8f,
					"onupdate",
					"ZoomOutTweenCallback"
				}));
				iTween.MoveTo(this.m_mapViewContentsRT.gameObject, iTween.Hash(new object[]
				{
					"name",
					"Pan View To Point (out)",
					"x",
					vector.x,
					"y",
					vector.y,
					"easeType",
					"easeOutQuad",
					"time",
					0.8f
				}));
			}
		}

		public Vector2 ScreenPointToLocalPointInMapViewRT(Vector2 screenPoint)
		{
			Vector2 result;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_mapViewRT, screenPoint, this.m_mainCamera, ref result);
			return result;
		}

		public void MissionFollowerSlotChanged(int garrFollowerID, bool inParty)
		{
			if (this.OnMissionFollowerSlotChanged != null)
			{
				this.OnMissionFollowerSlotChanged(garrFollowerID, inParty);
			}
		}

		public void HandleBountyInfoUpdated()
		{
			if (this.m_mapViewContentsRT != null)
			{
				BountySite[] componentsInChildren = this.m_mapViewContentsRT.GetComponentsInChildren<BountySite>(true);
				if (componentsInChildren != null)
				{
					foreach (BountySite bountySite in componentsInChildren)
					{
						StackableMapIcon component = bountySite.GetComponent<StackableMapIcon>();
						GameObject gameObject = bountySite.gameObject;
						if (component != null)
						{
							component.RemoveFromContainer();
						}
						if (gameObject != null)
						{
							Object.Destroy(gameObject);
						}
					}
				}
			}
			if (PersistentBountyData.bountyDictionary == null || this.m_emissaryCollection == null)
			{
				return;
			}
			this.m_emissaryCollection.ClearCollection();
			foreach (WrapperWorldQuestBounty bounty in PersistentBountyData.bountyDictionary.Values)
			{
				QuestV2Rec record = StaticDB.questDB.GetRecord(bounty.QuestID);
				int num = (record == null) ? 0 : record.QuestSortID;
				if (record == null)
				{
					Debug.LogWarning("HandleBountyInfoUpdated Warning: Failed to get Bounty quest with ID " + bounty.QuestID.ToString());
				}
				else if (num != 7502 && num != 7503)
				{
					switch (num)
					{
					case 7541:
					case 7543:
						break;
					default:
						if (num != 7334 && num != 7558 && num != 7637 && num != 8147 && num != 8574 && num != 8701)
						{
							GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_bountySitePrefab);
							if (!(gameObject2 == null))
							{
								BountySite component2 = gameObject2.GetComponent<BountySite>();
								if (!(component2 == null))
								{
									component2.SetBounty(bounty);
									gameObject2.name = "BountySite " + bounty.QuestID;
									RectTransform component3 = gameObject2.GetComponent<RectTransform>();
									if (!(component3 == null))
									{
										component3.anchorMin = new Vector2(0.5f, 0.5f);
										component3.anchorMax = new Vector2(0.5f, 0.5f);
										this.m_emissaryCollection.AddBountyObjectToCollection(gameObject2);
									}
								}
							}
						}
						break;
					}
				}
			}
		}

		public void HideRecentCharacterPanel()
		{
			this.m_playerInfoDisplay.HideRecentCharacterPanel();
		}

		private void SetActiveMapViewSize()
		{
			if (this.m_zoneID == AdventureMapPanel.eZone.Kultiras)
			{
				this.SetMapViewSize_KulTiras();
			}
			else if (this.m_zoneID == AdventureMapPanel.eZone.Zandalar)
			{
				this.SetMapViewSize_Zandalar();
			}
		}

		private void SetMapViewSize_BrokenIsles()
		{
			this.m_mapViewRT.sizeDelta = new Vector2(this.m_mapViewRT.sizeDelta.x, 720f);
			this.m_mapViewRT.anchoredPosition = new Vector2(0f, 0f);
			this.m_pinchZoomContentManager.SetZoom(1f, false);
			this.CenterAndZoomOut();
		}

		private void SetMapViewSize_KulTiras()
		{
			this.m_mapViewRT.sizeDelta = new Vector2(this.m_mapViewRT.sizeDelta.x, 660f);
			this.m_mapViewRT.anchoredPosition = new Vector2(0f, 110f);
			this.m_pinchZoomContentManager.SetZoom(1f, false);
			this.CenterAndZoomOut();
		}

		private void SetMapViewSize_Zandalar()
		{
			this.m_mapViewRT.sizeDelta = new Vector2(this.m_mapViewRT.sizeDelta.x, 660f);
			this.m_mapViewRT.anchoredPosition = new Vector2(0f, 110f);
			this.m_pinchZoomContentManager.SetZoom(1f, false);
			this.CenterAndZoomOut();
		}

		private void SetMapViewSize_Argus()
		{
			this.m_mapViewRT.sizeDelta = new Vector2(this.m_mapViewRT.sizeDelta.x, 720f);
			this.m_mapViewRT.anchoredPosition = new Vector2(0f, 100f);
			this.m_pinchZoomContentManager.SetZoom(1f, false);
			this.CenterAndZoomOut();
		}

		public void CenterMapInstantly()
		{
			this.m_mapViewContentsRT.anchoredPosition = Vector2.zero;
			this.m_mapViewRT.GetComponent<ScrollRect>().StopMovement();
		}

		private void UpdateCompletedMissionsDisplay()
		{
			if (PersistentMissionData.GetNumCompletedMissions(false) > 0)
			{
				this.m_nextCompletedMissionButton.gameObject.SetActive(true);
			}
		}

		public void SetStartingMapByFaction()
		{
			if (GarrisonStatus.Faction() == PVP_FACTION.HORDE)
			{
				this.m_zoneID = AdventureMapPanel.eZone.Zandalar;
			}
			else if (GarrisonStatus.Faction() == PVP_FACTION.ALLIANCE)
			{
				this.m_zoneID = AdventureMapPanel.eZone.Kultiras;
			}
			this.SetMapByActiveZoneID();
		}

		public void SwapMaps()
		{
			this.m_zoneID = ((this.m_zoneID != AdventureMapPanel.eZone.Zandalar) ? AdventureMapPanel.eZone.Zandalar : AdventureMapPanel.eZone.Kultiras);
			this.SetMapByActiveZoneID();
		}

		private void SetMapByActiveZoneID()
		{
			if (this.m_zoneID == AdventureMapPanel.eZone.Zandalar)
			{
				this.m_mapInfo_Zandalar.gameObject.SetActive(true);
				this.m_mapInfo_KulTiras.gameObject.SetActive(false);
				this.m_selectedMapImage.sprite = this.m_zandalarNavButtonImage;
				this.m_notSelectedMapImage.sprite = this.m_zandalarNavButtonImage;
			}
			if (this.m_zoneID == AdventureMapPanel.eZone.Kultiras)
			{
				this.m_mapInfo_Zandalar.gameObject.SetActive(false);
				this.m_mapInfo_KulTiras.gameObject.SetActive(true);
				this.m_selectedMapImage.sprite = this.m_kultirasNavButtonImage;
				this.m_notSelectedMapImage.sprite = this.m_kultirasNavButtonImage;
			}
			string text = (this.m_zoneID != AdventureMapPanel.eZone.Zandalar) ? "KUL_TIRAS_CAPS" : "ZANDALAR_CAPS";
			string str = text;
			this.m_zoneLabelText.text = StaticDB.GetString(text, "[PH] " + str);
			this.SetActiveMapViewSize();
		}

		public bool m_testEnableDetailedZoneMaps;

		public bool m_testEnableAutoZoomInOut;

		public bool m_testEnableTapToZoomOut;

		public float m_testMissionIconScale;

		public Action<float> TestIconSizeChanged;

		public Camera m_mainCamera;

		public PinchZoomContentManager m_pinchZoomContentManager;

		public RectTransform m_mapViewRT;

		public RectTransform m_mapAndRewardParentViewRT;

		public RectTransform m_mapViewContentsRT;

		public MapInfo m_mainMapInfo;

		public GameObject m_AdvMapMissionSitePrefab;

		public GameObject m_AdvMapWorldQuestPrefab;

		public GameObject m_bountySitePrefab;

		public MapInfo m_mapInfo_KulTiras;

		public MapInfo m_mapInfo_Zandalar;

		public GameObject m_missionAndWorldQuestArea_KulTiras;

		public GameObject m_missionAndWorldQuestArea_Zandalar;

		public EmissaryCollection m_emissaryCollection;

		public ZoneMissionOverview[] m_allZoneMissionOverviews;

		public GameObject m_missionRewardResultsDisplayPrefab;

		public GameObject m_zoneLabel;

		public Text m_zoneLabelText;

		public AdventureMapPanel.eZone m_zoneID;

		public static AdventureMapPanel instance;

		public ZoneButton m_lastTappedZoneButton;

		public ZoneButton m_currentVisibleZone;

		public OrderHallNavButton m_adventureMapOrderHallNavButton;

		public Image m_selectedMapImage;

		public Image m_notSelectedMapImage;

		public Sprite m_kultirasNavButtonImage;

		public Sprite m_zandalarNavButtonImage;

		private int m_currentMapMission;

		public Action<int> MissionSelectedFromMapAction;

		public Action<int> MissionMapSelectionChangedAction;

		private int m_currentListMission;

		public Action<int> MissionSelectedFromListAction;

		private int m_currentWorldQuest;

		public Action<int> WorldQuestChangedAction;

		public Action OnZoomOutMap;

		public Action<int> OnAddMissionLootToRewardPanel;

		public Action<bool> OnShowMissionRewardPanel;

		public Action OnInitMissionSites;

		public Action MapFiltersChanged;

		private int m_followerToInspect;

		public Action<int> FollowerToInspectChangedAction;

		public Action DeselectAllFollowerListItemsAction;

		public Action<bool> OnShowFollowerDetails;

		public Action<int, bool> OnMissionFollowerSlotChanged;

		public Action<int, int, bool> ShowMissionResultAction;

		private StackableMapIconContainer m_iconContainer;

		public Action<StackableMapIconContainer> SelectedIconContainerChanged;

		public float m_secondsMissionHasBeenSelected;

		public CanvasGroup m_topLevelMapCanvasGroup;

		public PlayerInfoDisplay m_playerInfoDisplay;

		private bool[] m_mapFilters;

		public MissionResultsPanel m_missionResultsPanel;

		public NextCompletedMissionButton m_nextCompletedMissionButton;

		public enum eZone
		{
			Azsuna,
			BrokenShore,
			HighMountain,
			Stormheim,
			Suramar,
			ValShara,
			Argus,
			MacAree,
			StygianWake,
			Drustvar,
			TiragardeSound,
			StormsongValley,
			Zuldazar,
			VolDun,
			Nazmir,
			Zandalar,
			Kultiras,
			None,
			NumZones
		}
	}
}
