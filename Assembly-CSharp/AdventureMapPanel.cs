using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class AdventureMapPanel : MonoBehaviour
{
	private void OnEnable()
	{
		this.MapFiltersChanged = (Action)Delegate.Combine(this.MapFiltersChanged, new Action(this.UpdateWorldQuests));
		this.HandleInvasionPOIChanged();
		Main main = Main.instance;
		main.InvasionPOIChangedAction = (Action)Delegate.Combine(main.InvasionPOIChangedAction, new Action(this.HandleInvasionPOIChanged));
	}

	private void OnDisable()
	{
		this.MapFiltersChanged = (Action)Delegate.Remove(this.MapFiltersChanged, new Action(this.UpdateWorldQuests));
		Main main = Main.instance;
		main.InvasionPOIChangedAction = (Action)Delegate.Remove(main.InvasionPOIChangedAction, new Action(this.HandleInvasionPOIChanged));
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
		this.m_invasionTimeRemainingDuration = new Duration(0, false);
		AdventureMapPanel.instance = this;
		this.m_zoneID = AdventureMapPanel.eZone.None;
		this.m_testMissionIconScale = 1f;
		this.m_mapFilters = new bool[15];
		for (int i = 0; i < this.m_mapFilters.Length; i++)
		{
			this.m_mapFilters[i] = false;
		}
		this.EnableMapFilter(MapFilterType.All, true);
		AllPanels.instance.m_missionResultsPanel.gameObject.SetActive(true);
	}

	private void Start()
	{
		this.m_pinchZoomContentManager.SetZoom(1f, false);
		StackableMapIconContainer[] componentsInChildren = this.m_mapViewContentsRT.GetComponentsInChildren<StackableMapIconContainer>(true);
		foreach (StackableMapIconContainer stackableMapIconContainer in componentsInChildren)
		{
			Object.DestroyImmediate(stackableMapIconContainer.gameObject);
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
		AllPopups.instance.EnableMissionDialog();
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
		if (record.GarrFollowerTypeID != 4u)
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
		JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[garrMissionID];
		if (jamGarrisonMobileMission.MissionState == 0)
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
		float width = this.m_worldMapLowDetail_BrokenIsles.sprite.textureRect.width;
		float height = this.m_worldMapLowDetail_BrokenIsles.sprite.textureRect.height;
		Vector2 vector = new Vector3(num2 / width, num3 / height);
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
					Object.DestroyImmediate(adventureMapMissionSite.gameObject);
				}
			}
		}
		componentsInChildren = this.m_missionAndWorldQuestArea_Argus.transform.GetComponentsInChildren<AdventureMapMissionSite>(true);
		foreach (AdventureMapMissionSite adventureMapMissionSite2 in componentsInChildren)
		{
			if (adventureMapMissionSite2 != null)
			{
				StackableMapIcon component2 = adventureMapMissionSite2.GetComponent<StackableMapIcon>();
				GameObject gameObject2 = adventureMapMissionSite2.gameObject;
				if (component2 != null)
				{
					component2.RemoveFromContainer();
				}
				if (gameObject2 != null)
				{
					Object.DestroyImmediate(adventureMapMissionSite2.gameObject);
				}
			}
		}
		foreach (object obj in PersistentMissionData.missionDictionary.Values)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)obj;
			this.CreateMissionSite(jamGarrisonMobileMission.MissionRecID);
		}
	}

	public void UpdateWorldQuests()
	{
		AdventureMapWorldQuest[] componentsInChildren = this.m_missionAndWorldQuestArea_BrokenIsles.GetComponentsInChildren<AdventureMapWorldQuest>(true);
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
				Object.DestroyImmediate(adventureMapWorldQuest.gameObject);
			}
		}
		componentsInChildren = this.m_missionAndWorldQuestArea_Argus.GetComponentsInChildren<AdventureMapWorldQuest>(true);
		foreach (AdventureMapWorldQuest adventureMapWorldQuest2 in componentsInChildren)
		{
			StackableMapIcon component2 = adventureMapWorldQuest2.GetComponent<StackableMapIcon>();
			GameObject gameObject2 = adventureMapWorldQuest2.gameObject;
			if (component2 != null)
			{
				component2.RemoveFromContainer();
			}
			if (gameObject2 != null)
			{
				Object.DestroyImmediate(adventureMapWorldQuest2.gameObject);
			}
		}
		foreach (object obj in WorldQuestData.worldQuestDictionary.Values)
		{
			MobileWorldQuest mobileWorldQuest = (MobileWorldQuest)obj;
			if (!this.IsFilterEnabled(MapFilterType.All))
			{
				bool matchesFilter = false;
				if (this.IsFilterEnabled(MapFilterType.ArtifactPower))
				{
					foreach (MobileWorldQuestReward mobileWorldQuestReward in mobileWorldQuest.Item)
					{
						StaticDB.itemEffectDB.EnumRecordsByParentID(mobileWorldQuestReward.RecordID, delegate(ItemEffectRec itemEffectRec)
						{
							StaticDB.spellEffectDB.EnumRecordsByParentID(itemEffectRec.SpellID, delegate(SpellEffectRec spellEffectRec)
							{
								if (spellEffectRec.Effect == 240)
								{
									matchesFilter = true;
									return false;
								}
								return true;
							});
							return !matchesFilter;
						});
					}
				}
				if (this.IsFilterEnabled(MapFilterType.OrderResources))
				{
					foreach (MobileWorldQuestReward mobileWorldQuestReward2 in mobileWorldQuest.Currency)
					{
						if (mobileWorldQuestReward2.RecordID == 1220)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Gold) && mobileWorldQuest.Money > 0)
				{
					matchesFilter = true;
				}
				if (this.IsFilterEnabled(MapFilterType.Gear))
				{
					foreach (MobileWorldQuestReward mobileWorldQuestReward3 in mobileWorldQuest.Item)
					{
						ItemRec record = StaticDB.itemDB.GetRecord(mobileWorldQuestReward3.RecordID);
						if (record != null && (record.ClassID == 2 || record.ClassID == 3 || record.ClassID == 4 || record.ClassID == 6))
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.ProfessionMats))
				{
					foreach (MobileWorldQuestReward mobileWorldQuestReward4 in mobileWorldQuest.Item)
					{
						ItemRec record2 = StaticDB.itemDB.GetRecord(mobileWorldQuestReward4.RecordID);
						if (record2 != null && record2.ClassID == 7)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.PetCharms))
				{
					foreach (MobileWorldQuestReward mobileWorldQuestReward5 in mobileWorldQuest.Item)
					{
						if (mobileWorldQuestReward5.RecordID == 116415)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_HighmountainTribes) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num2 = 0; num2 < mobileBountiesByWorldQuest.BountyQuestID.Length; num2++)
					{
						if (mobileBountiesByWorldQuest.BountyQuestID[num2] == 42233)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_CourtOfFarondis) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest2 = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num3 = 0; num3 < mobileBountiesByWorldQuest2.BountyQuestID.Length; num3++)
					{
						if (mobileBountiesByWorldQuest2.BountyQuestID[num3] == 42420)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_Dreamweavers) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest3 = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num4 = 0; num4 < mobileBountiesByWorldQuest3.BountyQuestID.Length; num4++)
					{
						if (mobileBountiesByWorldQuest3.BountyQuestID[num4] == 42170)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_Wardens) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest4 = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num5 = 0; num5 < mobileBountiesByWorldQuest4.BountyQuestID.Length; num5++)
					{
						if (mobileBountiesByWorldQuest4.BountyQuestID[num5] == 42422)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_Nightfallen) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest5 = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num6 = 0; num6 < mobileBountiesByWorldQuest5.BountyQuestID.Length; num6++)
					{
						if (mobileBountiesByWorldQuest5.BountyQuestID[num6] == 42421)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_Valarjar) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest6 = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num7 = 0; num7 < mobileBountiesByWorldQuest6.BountyQuestID.Length; num7++)
					{
						if (mobileBountiesByWorldQuest6.BountyQuestID[num7] == 42234)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Bounty_KirinTor) && PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(mobileWorldQuest.QuestID))
				{
					MobileBountiesByWorldQuest mobileBountiesByWorldQuest7 = (MobileBountiesByWorldQuest)PersistentBountyData.bountiesByWorldQuestDictionary[mobileWorldQuest.QuestID];
					for (int num8 = 0; num8 < mobileBountiesByWorldQuest7.BountyQuestID.Length; num8++)
					{
						if (mobileBountiesByWorldQuest7.BountyQuestID[num8] == 43179)
						{
							matchesFilter = true;
							break;
						}
					}
				}
				if (this.IsFilterEnabled(MapFilterType.Invasion))
				{
					QuestInfoRec record3 = StaticDB.questInfoDB.GetRecord(mobileWorldQuest.QuestInfoID);
					if (record3 == null)
					{
						break;
					}
					bool flag = record3.Type == 7;
					if (flag)
					{
						matchesFilter = true;
					}
				}
				if (!matchesFilter)
				{
					continue;
				}
			}
			GameObject gameObject3 = Object.Instantiate<GameObject>(AdventureMapPanel.instance.m_AdvMapWorldQuestPrefab);
			if (mobileWorldQuest.StartLocationMapID == 1220)
			{
				gameObject3.transform.SetParent(this.m_missionAndWorldQuestArea_BrokenIsles.transform, false);
				float mapScale = 0.10271506f;
				float mapOffsetX = 1036.88037f;
				float mapOffsetY = 597.2115f;
				this.SetupWorldQuestIcon(mobileWorldQuest, gameObject3, mapOffsetX, mapOffsetY, mapScale);
			}
			else
			{
				gameObject3.transform.localScale = new Vector3(1.33f, 1.33f, 1.33f);
				gameObject3.transform.SetParent(this.m_missionAndWorldQuestArea_Argus.transform, false);
				int worldMapAreaID = mobileWorldQuest.WorldMapAreaID;
				if (worldMapAreaID != 1170)
				{
					if (worldMapAreaID != 1171)
					{
						if (worldMapAreaID != 1135)
						{
							Debug.LogError(string.Concat(new object[]
							{
								"UNHANDLED WORLD QUEST AREA ID ",
								mobileWorldQuest.QuestTitle,
								" ",
								mobileWorldQuest.WorldMapAreaID
							}));
						}
						else
						{
							float mapScale2 = 0.3132809f;
							float mapOffsetX2 = 2115.88f;
							float mapOffsetY2 = -7.788513f;
							this.SetupWorldQuestIcon(mobileWorldQuest, gameObject3, mapOffsetX2, mapOffsetY2, mapScale2);
						}
					}
					else
					{
						float mapScale2 = 0.38f;
						float mapOffsetX2 = 3981f;
						float mapOffsetY2 = 1520f;
						this.SetupWorldQuestIcon(mobileWorldQuest, gameObject3, mapOffsetX2, mapOffsetY2, mapScale2);
					}
				}
				else
				{
					float mapScale2 = 0.39f;
					float mapOffsetX2 = 4832.76f;
					float mapOffsetY2 = -1232f;
					this.SetupWorldQuestIcon(mobileWorldQuest, gameObject3, mapOffsetX2, mapOffsetY2, mapScale2);
				}
			}
			AdventureMapWorldQuest component3 = gameObject3.GetComponent<AdventureMapWorldQuest>();
			component3.SetQuestID(mobileWorldQuest.QuestID);
			StackableMapIcon component4 = gameObject3.GetComponent<StackableMapIcon>();
			if (component4 != null)
			{
				component4.RegisterWithManager(mobileWorldQuest.StartLocationMapID);
			}
		}
		this.m_pinchZoomContentManager.ForceZoomFactorChanged();
	}

	private void SetupWorldQuestIcon(MobileWorldQuest worldQuest, GameObject worldQuestObj, float mapOffsetX, float mapOffsetY, float mapScale)
	{
		float num = (float)worldQuest.StartLocationY * -mapScale;
		float num2 = (float)worldQuest.StartLocationX * mapScale;
		num += mapOffsetX;
		num2 += mapOffsetY;
		float num3 = (float)((worldQuest.StartLocationMapID != 1220) ? 2001 : 1201);
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
		if (this.m_invasionNotification.gameObject.activeSelf)
		{
			long num = LegionfallData.GetCurrentInvasionExpirationTime() - GarrisonStatus.CurrentTime();
			num = ((num <= 0L) ? 0L : num);
			if (num > 0L)
			{
				this.m_invasionTimeRemainingDuration.FormatDurationString((int)num, false);
				this.m_invasionTimeRemaining.text = this.m_invasionTimeRemainingDuration.DurationString;
			}
			else
			{
				this.m_invasionNotification.gameObject.SetActive(false);
				if (this.m_mapInfo_BrokenIsles.gameObject.activeSelf)
				{
					this.SetMapViewSize_BrokenIsles();
				}
				else
				{
					this.SetMapViewSize_Argus();
				}
				Main.instance.RequestWorldQuests();
			}
		}
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
		if (this.m_adventureMapOrderHallNavButton.IsSelected())
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
						Object.DestroyImmediate(gameObject);
					}
				}
			}
		}
		if (PersistentBountyData.bountyDictionary == null)
		{
			return;
		}
		foreach (object obj in PersistentBountyData.bountyDictionary.Values)
		{
			MobileWorldQuestBounty mobileWorldQuestBounty = (MobileWorldQuestBounty)obj;
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_bountySitePrefab);
			if (!(gameObject2 == null))
			{
				BountySite component2 = gameObject2.GetComponent<BountySite>();
				if (!(component2 == null))
				{
					component2.SetBounty(mobileWorldQuestBounty);
					gameObject2.name = "BountySite " + mobileWorldQuestBounty.QuestID;
					RectTransform component3 = gameObject2.GetComponent<RectTransform>();
					if (!(component3 == null))
					{
						component3.anchorMin = new Vector2(0.5f, 0.5f);
						component3.anchorMax = new Vector2(0.5f, 0.5f);
						QuestV2Rec record = StaticDB.questDB.GetRecord(mobileWorldQuestBounty.QuestID);
						int num = (record == null) ? 0 : record.QuestSortID;
						bool flag = true;
						ZoneMissionOverview zoneMissionOverview = null;
						int startLocationMapID = 1220;
						int num2 = num;
						switch (num2)
						{
						case 7541:
							zoneMissionOverview = this.m_allZoneMissionOverviews[3];
							break;
						default:
							if (num2 != 7502)
							{
								if (num2 == 7503)
								{
									zoneMissionOverview = this.m_allZoneMissionOverviews[2];
									break;
								}
								if (num2 == 7334)
								{
									zoneMissionOverview = this.m_allZoneMissionOverviews[0];
									break;
								}
								if (num2 == 7558)
								{
									zoneMissionOverview = this.m_allZoneMissionOverviews[5];
									break;
								}
								if (num2 == 7637)
								{
									zoneMissionOverview = this.m_allZoneMissionOverviews[4];
									break;
								}
								if (num2 != 8147)
								{
									if (num2 == 8574)
									{
										zoneMissionOverview = this.m_allZoneMissionOverviews[7];
										break;
									}
									if (num2 != 8701)
									{
										Debug.LogError(string.Concat(new object[]
										{
											"INVALID QUESTSORTID ",
											num,
											" for quest ID:",
											mobileWorldQuestBounty.QuestID
										}));
										flag = false;
										break;
									}
									zoneMissionOverview = this.m_allZoneMissionOverviews[9];
									break;
								}
							}
							zoneMissionOverview = this.m_allZoneMissionOverviews[6];
							break;
						case 7543:
							zoneMissionOverview = this.m_allZoneMissionOverviews[1];
							break;
						}
						if (flag)
						{
							if (zoneMissionOverview != null)
							{
								if (zoneMissionOverview.zoneNameTag != null && zoneMissionOverview.zoneNameTag.Length > 0)
								{
									if (zoneMissionOverview.m_bountyButtonRoot != null)
									{
										gameObject2.transform.SetParent(zoneMissionOverview.m_bountyButtonRoot.transform, false);
									}
								}
								else if (zoneMissionOverview.m_anonymousBountyButtonRoot != null)
								{
									gameObject2.transform.SetParent(zoneMissionOverview.m_anonymousBountyButtonRoot.transform, false);
								}
							}
							gameObject2.transform.localPosition = Vector3.zero;
							if (component2.m_errorImage != null)
							{
								component2.m_errorImage.gameObject.SetActive(false);
							}
						}
						else
						{
							gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
							if (component2.m_errorImage != null)
							{
								component2.m_errorImage.gameObject.SetActive(true);
							}
						}
						StackableMapIcon component4 = gameObject2.GetComponent<StackableMapIcon>();
						if (component4 != null)
						{
							component4.RegisterWithManager(startLocationMapID);
						}
					}
				}
			}
		}
	}

	public void HideRecentCharacterPanel()
	{
		this.m_playerInfoDisplay.HideRecentCharacterPanel();
	}

	private void HandleInvasionPOIChanged()
	{
		JamMobileAreaPOI currentInvasionPOI = LegionfallData.GetCurrentInvasionPOI();
		if (currentInvasionPOI == null)
		{
			this.m_invasionNotification.gameObject.SetActive(false);
		}
		else
		{
			this.m_invasionNotification.gameObject.SetActive(true);
			this.m_invasionTitle.text = currentInvasionPOI.Description;
			long num = LegionfallData.GetCurrentInvasionExpirationTime() - GarrisonStatus.CurrentTime();
			num = ((num <= 0L) ? 0L : num);
			this.m_invasionTimeRemainingDuration.FormatDurationString((int)num, false);
			this.m_invasionTimeRemaining.text = this.m_invasionTimeRemainingDuration.DurationString;
		}
		if (this.m_mapInfo_BrokenIsles.gameObject.activeSelf)
		{
			this.SetMapViewSize_BrokenIsles();
		}
		else
		{
			this.SetMapViewSize_Argus();
		}
	}

	public void SetMapViewSize_BrokenIsles()
	{
		this.m_mapViewRT.sizeDelta = new Vector2(this.m_mapViewRT.sizeDelta.x, 820f + ((!this.m_invasionNotification.gameObject.activeSelf) ? 0f : -60f));
		this.m_mapViewRT.anchoredPosition = new Vector2(0f, 0f);
		this.m_pinchZoomContentManager.SetZoom(1f, false);
		this.CenterAndZoomOut();
	}

	public void SetMapViewSize_Argus()
	{
		this.m_mapViewRT.sizeDelta = new Vector2(this.m_mapViewRT.sizeDelta.x, 720f + ((!this.m_invasionNotification.gameObject.activeSelf) ? 0f : -60f));
		this.m_mapViewRT.anchoredPosition = new Vector2(0f, 100f);
		this.m_pinchZoomContentManager.SetZoom(1f, false);
		this.CenterAndZoomOut();
	}

	public void CenterMapInstantly()
	{
		this.m_mapViewContentsRT.anchoredPosition = Vector2.zero;
		this.m_mapViewRT.GetComponent<ScrollRect>().StopMovement();
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

	public Image m_worldMapLowDetail_BrokenIsles;

	public Image m_worldMapLowDetail_Argus;

	public MapInfo m_mainMapInfo;

	public GameObject m_AdvMapMissionSitePrefab;

	public GameObject m_AdvMapWorldQuestPrefab;

	public GameObject m_bountySitePrefab;

	public MapInfo m_mapInfo_BrokenIsles;

	public MapInfo m_mapInfo_Argus;

	public GameObject m_missionAndWorldQuestArea_BrokenIsles;

	public GameObject m_missionAndWorldQuestArea_Argus;

	public ZoneMissionOverview[] m_allZoneMissionOverviews;

	public GameObject m_missionRewardResultsDisplayPrefab;

	public GameObject m_zoneLabel;

	public Text m_zoneLabelText;

	public AdventureMapPanel.eZone m_zoneID;

	public static AdventureMapPanel instance;

	public ZoneButton m_lastTappedZoneButton;

	public ZoneButton m_currentVisibleZone;

	public OrderHallNavButton m_adventureMapOrderHallNavButton;

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

	public RectTransform m_invasionNotification;

	public Text m_invasionTitle;

	public Text m_invasionTimeRemaining;

	private Duration m_invasionTimeRemainingDuration;

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
		None,
		NumZones
	}
}
