using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MiniMissionListPanel : MonoBehaviour
	{
		private void Awake()
		{
			this.m_noMissionsAvailableLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_noMissionsAvailableLabel.text = StaticDB.GetString("NO_MISSIONS_AVAILABLE", "No missions are currently available.");
			this.m_noMissionsInProgressLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_noMissionsInProgressLabel.text = StaticDB.GetString("NO_MISSIONS_IN_PROGRESS", "No missions are currently in progress.");
		}

		public void OnEnable()
		{
			Singleton<GarrisonWrapper>.Instance.GarrisonDataResetFinishedAction += this.HandleGarrisonDataResetFinished;
			Singleton<GarrisonWrapper>.Instance.MissionAddedAction += this.HandleMissionAdded;
			this.InitMissionList();
			this.ShowAvailableMissionList();
			Singleton<DialogFactory>.Instance.CloseMissionDialog();
		}

		private void OnDisable()
		{
			Singleton<GarrisonWrapper>.Instance.GarrisonDataResetFinishedAction -= this.HandleGarrisonDataResetFinished;
			Singleton<GarrisonWrapper>.Instance.MissionAddedAction -= this.HandleMissionAdded;
			this.ClearMissionStartedEffect();
		}

		public void ShowAvailableMissionList()
		{
			this.m_availableMissionListScrollView.SetActive(true);
			this.m_inProgressMissionListScrollView.SetActive(false);
			this.m_availableMissionNotSelectedImage.gameObject.SetActive(false);
			this.m_inProgressMissionNotSelectedImage.gameObject.SetActive(true);
		}

		public void ShowInProgressMissionList()
		{
			this.m_availableMissionListScrollView.SetActive(false);
			this.m_inProgressMissionListScrollView.SetActive(true);
			this.m_availableMissionNotSelectedImage.gameObject.SetActive(true);
			this.m_inProgressMissionNotSelectedImage.gameObject.SetActive(false);
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
					WrapperGarrisonMission mission = PersistentMissionData.missionDictionary[miniMissionListItem.GetMissionID()];
					if (mission.MissionState == 0)
					{
						flag = false;
						miniMissionListItem.UpdateMechanicPreview(false, mission);
					}
				}
				if (flag)
				{
					miniMissionListItem.gameObject.transform.SetParent(null);
					Object.Destroy(miniMissionListItem.gameObject);
				}
			}
			componentsInChildren = this.m_inProgressMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
			foreach (MiniMissionListItem miniMissionListItem2 in componentsInChildren)
			{
				bool flag2 = true;
				if (PersistentMissionData.missionDictionary.ContainsKey(miniMissionListItem2.GetMissionID()) && PersistentMissionData.missionDictionary[miniMissionListItem2.GetMissionID()].MissionState != 0)
				{
					flag2 = false;
				}
				if (flag2)
				{
					miniMissionListItem2.gameObject.transform.SetParent(null);
					Object.Destroy(miniMissionListItem2.gameObject);
				}
			}
			MiniMissionListItem[] componentsInChildren2 = this.m_availableMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
			MiniMissionListItem[] componentsInChildren3 = this.m_inProgressMission_listContents.GetComponentsInChildren<MiniMissionListItem>(true);
			foreach (WrapperGarrisonMission mission2 in PersistentMissionData.missionDictionary.Values)
			{
				bool flag3 = false;
				foreach (MiniMissionListItem miniMissionListItem3 in componentsInChildren2)
				{
					if (miniMissionListItem3.GetMissionID() == mission2.MissionRecID)
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					foreach (MiniMissionListItem miniMissionListItem4 in componentsInChildren3)
					{
						if (miniMissionListItem4.GetMissionID() == mission2.MissionRecID)
						{
							flag3 = true;
							break;
						}
					}
				}
				if (!flag3)
				{
					GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(mission2.MissionRecID);
					if (record == null)
					{
						Debug.LogWarning("Mission Not Found: ID " + mission2.MissionRecID);
					}
					else if (record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
					{
						if ((record.Flags & 16u) != 0u)
						{
							this.m_combatAllyListItem.gameObject.SetActive(true);
							this.m_combatAllyListItem.UpdateVisuals();
						}
						else
						{
							MiniMissionListItem miniMissionListItem5 = Object.Instantiate<MiniMissionListItem>(this.m_miniMissionListItemPrefab);
							if (mission2.MissionState == 0)
							{
								miniMissionListItem5.transform.SetParent(this.m_availableMission_listContents.transform, false);
							}
							else
							{
								miniMissionListItem5.transform.SetParent(this.m_inProgressMission_listContents.transform, false);
								this.ShowMissionStartedAnim();
							}
							miniMissionListItem5.SetMission(mission2);
						}
					}
				}
			}
			int num = 0;
			int num2 = 0;
			PersistentMissionData.GetAvailableAndProgressCounts(ref num2, ref num);
			this.m_availableMissionsTabLabel.text = StaticDB.GetString("AVAILABLE", null) + " - " + num2;
			this.m_inProgressMissionsTabLabel.text = StaticDB.GetString("IN_PROGRESS", null) + " - " + num;
			this.m_noMissionsAvailableLabel.gameObject.SetActive(num2 == 0);
			this.m_noMissionsInProgressLabel.gameObject.SetActive(num == 0);
		}

		private void ShowMissionStartedAnim()
		{
			if (!this.m_missionListOrderHallNavButton.IsSelected())
			{
				return;
			}
			this.ClearMissionStartedEffect();
			this.m_currentMissionStartedEffectObj = Object.Instantiate<GameObject>(this.m_missionStartedEffectObjPrefab);
			this.m_currentMissionStartedEffectObj.transform.SetParent(this.m_inProgressMissionsTabButton.transform, false);
			this.m_currentMissionStartedEffectObj.transform.localPosition = Vector3.zero;
		}

		private void ClearMissionStartedEffect()
		{
			if (this.m_currentMissionStartedEffectObj != null)
			{
				Object.Destroy(this.m_currentMissionStartedEffectObj);
			}
		}

		public MiniMissionListItem m_miniMissionListItemPrefab;

		public GameObject m_availableMissionListScrollView;

		public GameObject m_availableMission_listContents;

		public GameObject m_inProgressMissionListScrollView;

		public GameObject m_inProgressMission_listContents;

		public Button m_availableMissionsTabButton;

		public Text m_availableMissionsTabLabel;

		public Image m_availableMissionNotSelectedImage;

		public Button m_inProgressMissionsTabButton;

		public Text m_inProgressMissionsTabLabel;

		public Image m_inProgressMissionNotSelectedImage;

		public Text m_noMissionsAvailableLabel;

		public Text m_noMissionsInProgressLabel;

		public CombatAllyListItem m_combatAllyListItem;

		private Vector2 m_multiPanelViewSizeDelta;

		public GameObject m_missionStartedEffectObjPrefab;

		public OrderHallNavButton m_missionListOrderHallNavButton;

		private GameObject m_currentMissionStartedEffectObj;
	}
}
