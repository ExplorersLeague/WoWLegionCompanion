using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStaticData;

public class MiniMissionListPanel : MonoBehaviour
{
	private void Awake()
	{
		this.m_availableMissionsTabLabel.font = GeneralHelpers.LoadFancyFont();
		this.m_inProgressMissionsTabLabel.font = GeneralHelpers.LoadFancyFont();
		this.m_noMissionsAvailableLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_noMissionsAvailableLabel.text = StaticDB.GetString("NO_MISSIONS_AVAILABLE", "No missions are currently available.");
		this.m_noMissionsInProgressLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_noMissionsInProgressLabel.text = StaticDB.GetString("NO_MISSIONS_IN_PROGRESS", "No missions are currently in progress.");
	}

	public void OnEnable()
	{
		Main instance = Main.instance;
		instance.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(instance.GarrisonDataResetFinishedAction, new Action(this.HandleGarrisonDataResetFinished));
		Main instance2 = Main.instance;
		instance2.MissionAddedAction = (Action<int, int>)Delegate.Combine(instance2.MissionAddedAction, new Action<int, int>(this.HandleMissionAdded));
		this.InitMissionList();
		this.ShowAvailableMissionList();
	}

	private void OnDisable()
	{
		Main instance = Main.instance;
		instance.GarrisonDataResetFinishedAction = (Action)Delegate.Remove(instance.GarrisonDataResetFinishedAction, new Action(this.HandleGarrisonDataResetFinished));
		Main instance2 = Main.instance;
		instance2.MissionAddedAction = (Action<int, int>)Delegate.Remove(instance2.MissionAddedAction, new Action<int, int>(this.HandleMissionAdded));
	}

	public void ShowAvailableMissionList()
	{
		this.m_availableMissionListScrollView.SetActive(true);
		this.m_inProgressMissionListScrollView.SetActive(false);
		this.m_availableMissionsTabSelectedImage.gameObject.SetActive(true);
		this.m_inProgressMissionsTabSelectedImage.gameObject.SetActive(false);
	}

	public void ShowInProgressMissionList()
	{
		this.m_availableMissionListScrollView.SetActive(false);
		this.m_inProgressMissionListScrollView.SetActive(true);
		this.m_availableMissionsTabSelectedImage.gameObject.SetActive(false);
		this.m_inProgressMissionsTabSelectedImage.gameObject.SetActive(true);
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

	private void HandleGarrisonDataResetFinished()
	{
		this.InitMissionList();
	}

	private void HandleMissionAdded(int garrMissionID, int result)
	{
		this.InitMissionList();
	}

	public void InitMissionList()
	{
		this.m_combatAllyListItem.gameObject.SetActive(false);
		MiniMissionListItem[] componentsInChildren = this.m_availableMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
		foreach (MiniMissionListItem miniMissionListItem in componentsInChildren)
		{
			bool flag = true;
			if (PersistentMissionData.missionDictionary.ContainsKey(miniMissionListItem.GetMissionID()))
			{
				JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[miniMissionListItem.GetMissionID()];
				if (jamGarrisonMobileMission.MissionState == 0)
				{
					flag = false;
					miniMissionListItem.UpdateMechanicPreview(false, jamGarrisonMobileMission);
				}
			}
			if (flag)
			{
				Object.DestroyImmediate(miniMissionListItem.gameObject);
			}
		}
		componentsInChildren = this.m_inProgressMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
		foreach (MiniMissionListItem miniMissionListItem2 in componentsInChildren)
		{
			bool flag2 = true;
			if (PersistentMissionData.missionDictionary.ContainsKey(miniMissionListItem2.GetMissionID()))
			{
				JamGarrisonMobileMission jamGarrisonMobileMission2 = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[miniMissionListItem2.GetMissionID()];
				if (jamGarrisonMobileMission2.MissionState != 0)
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				Object.DestroyImmediate(miniMissionListItem2.gameObject);
			}
		}
		MiniMissionListItem[] componentsInChildren2 = this.m_availableMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
		MiniMissionListItem[] componentsInChildren3 = this.m_inProgressMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
		foreach (object obj in PersistentMissionData.missionDictionary.Values)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission3 = (JamGarrisonMobileMission)obj;
			bool flag3 = false;
			foreach (MiniMissionListItem miniMissionListItem3 in componentsInChildren2)
			{
				if (miniMissionListItem3.GetMissionID() == jamGarrisonMobileMission3.MissionRecID)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				foreach (MiniMissionListItem miniMissionListItem4 in componentsInChildren3)
				{
					if (miniMissionListItem4.GetMissionID() == jamGarrisonMobileMission3.MissionRecID)
					{
						flag3 = true;
						break;
					}
				}
			}
			if (!flag3)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(jamGarrisonMobileMission3.MissionRecID);
				if (record == null)
				{
					Debug.LogWarning("Mission Not Found: ID " + jamGarrisonMobileMission3.MissionRecID);
				}
				else if (record.GarrFollowerTypeID == 4u)
				{
					if ((record.Flags & 16u) != 0u)
					{
						this.m_combatAllyListItem.gameObject.SetActive(true);
					}
					else
					{
						GameObject gameObject = Object.Instantiate<GameObject>(this.m_miniMissionListItemPrefab);
						if (jamGarrisonMobileMission3.MissionState == 0)
						{
							gameObject.transform.SetParent(this.m_availableMission_listContents.transform, false);
						}
						else
						{
							gameObject.transform.SetParent(this.m_inProgressMission_listContents.transform, false);
						}
						MiniMissionListItem component = gameObject.GetComponent<MiniMissionListItem>();
						component.SetMission(jamGarrisonMobileMission3);
						AutoHide autoHide = gameObject.AddComponent<AutoHide>();
						autoHide.m_clipRT = base.gameObject.GetComponent<RectTransform>();
					}
				}
			}
		}
		componentsInChildren2 = this.m_availableMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
		componentsInChildren3 = this.m_inProgressMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
		int num = componentsInChildren2.Length;
		int num2 = componentsInChildren3.Length;
		this.m_availableMissionsTabLabel.text = StaticDB.GetString("AVAILABLE", null) + " - " + num;
		this.m_inProgressMissionsTabLabel.text = StaticDB.GetString("IN_PROGRESS", null) + " - " + num2;
		this.m_noMissionsAvailableLabel.gameObject.SetActive(num == 0);
		this.m_noMissionsInProgressLabel.gameObject.SetActive(num2 == 0);
	}

	public RectTransform m_parentViewRT;

	public RectTransform m_panelViewRT;

	public GameObject m_miniMissionListItemPrefab;

	public GameObject m_availableMissionListScrollView;

	public GameObject m_availableMission_listContents;

	public GameObject m_inProgressMissionListScrollView;

	public GameObject m_inProgressMission_listContents;

	public Button m_availableMissionsTabButton;

	public Text m_availableMissionsTabLabel;

	public Image m_availableMissionsTabSelectedImage;

	public Button m_inProgressMissionsTabButton;

	public Text m_inProgressMissionsTabLabel;

	public Image m_inProgressMissionsTabSelectedImage;

	public Text m_noMissionsAvailableLabel;

	public Text m_noMissionsInProgressLabel;

	public CombatAllyListItem m_combatAllyListItem;

	private Vector2 m_multiPanelViewSizeDelta;
}
