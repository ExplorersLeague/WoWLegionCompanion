using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class MissionReportAlertPanel : MonoBehaviour
	{
		private Dictionary<int, bool> GetRequestedMissionCollectionDictionary()
		{
			if (this._requestedMissionCollection == null)
			{
				this._requestedMissionCollection = new Dictionary<int, bool>();
			}
			return this._requestedMissionCollection;
		}

		private void Awake()
		{
		}

		private void Update()
		{
			this.completedMissionsText.text = string.Empty + PersistentMissionData.GetNumCompletedMissions(false) + " Completed Missions";
		}

		private void OnEnable()
		{
			this.GetRequestedMissionCollectionDictionary().Clear();
			this.okButton.SetActive(false);
			this.mainCanvas.renderMode = 1;
			if (GarrisonStatus.Faction() == PVP_FACTION.HORDE)
			{
				this.hordeCommander.SetActive(true);
				this.allianceCommander.SetActive(false);
			}
			else
			{
				this.hordeCommander.SetActive(false);
				this.allianceCommander.SetActive(true);
			}
			this.completedMissionsText.text = string.Empty + PersistentMissionData.GetNumCompletedMissions(false) + " Completed Missions";
			MissionListItem[] componentsInChildren = this.completedMissionListContents.GetComponentsInChildren<MissionListItem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Object.Destroy(componentsInChildren[i].gameObject);
			}
			MissionRewardDisplay[] componentsInChildren2 = this.missionRewardsIconArea.GetComponentsInChildren<MissionRewardDisplay>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				Object.Destroy(componentsInChildren2[j].gameObject);
			}
			this.missionReportView.SetActive(true);
			this.missionResultsView.SetActive(false);
		}

		private void OnDisable()
		{
			this.mainCanvas.renderMode = 0;
		}

		private bool MissionIsOnCompletedMissionList(int garrMissionID)
		{
			MissionListItem[] componentsInChildren = this.completedMissionListContents.GetComponentsInChildren<MissionListItem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].garrMissionID == garrMissionID)
				{
					return true;
				}
			}
			return false;
		}

		private void PopulateCompletedMissionList()
		{
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				if ((wrapperGarrisonMission.MissionState == 2 || wrapperGarrisonMission.MissionState == 6) && !this.MissionIsOnCompletedMissionList(wrapperGarrisonMission.MissionRecID))
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.missionListItemPrefab);
					gameObject.transform.SetParent(this.completedMissionListContents.transform, false);
					MissionListItem component = gameObject.GetComponent<MissionListItem>();
					component.Init(wrapperGarrisonMission.MissionRecID);
					component.isResultsItem = true;
				}
			}
		}

		private void CollectFirstCompletedMission()
		{
			MissionListItem[] componentsInChildren = this.completedMissionListContents.GetComponentsInChildren<MissionListItem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[componentsInChildren[i].garrMissionID];
				if (!this.GetRequestedMissionCollectionDictionary().ContainsKey(componentsInChildren[i].garrMissionID) && PersistentMissionData.missionDictionary.ContainsKey(componentsInChildren[i].garrMissionID) && wrapperGarrisonMission.MissionState == 2)
				{
					this.GetRequestedMissionCollectionDictionary().Add(componentsInChildren[i].garrMissionID, true);
					Main.instance.ClaimMissionBonus(componentsInChildren[i].garrMissionID);
					break;
				}
			}
		}

		public void CompleteAllMissions()
		{
			Main.instance.CompleteAllMissions();
			this.missionReportView.SetActive(false);
			this.missionResultsView.SetActive(true);
			this.PopulateCompletedMissionList();
			this.CollectFirstCompletedMission();
		}

		public void OnMissionStatusChanged()
		{
			this.PopulateCompletedMissionList();
			this.CollectFirstCompletedMission();
			int num = 0;
			MissionListItem[] componentsInChildren = this.completedMissionListContents.GetComponentsInChildren<MissionListItem>(true);
			MissionRewardDisplay[] componentsInChildren2 = this.missionRewardsIconArea.transform.GetComponentsInChildren<MissionRewardDisplay>(true);
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				Object.Destroy(componentsInChildren2[i].gameObject);
			}
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				WrapperGarrisonMission wrapperGarrisonMission = PersistentMissionData.missionDictionary[componentsInChildren[j].garrMissionID];
				if (PersistentMissionData.missionDictionary.ContainsKey(componentsInChildren[j].garrMissionID) && wrapperGarrisonMission.MissionState == 6)
				{
					componentsInChildren[j].inProgressDarkener.SetActive(true);
					componentsInChildren[j].missionResultsText.gameObject.SetActive(true);
					if (this.GetRequestedMissionCollectionDictionary().ContainsKey(componentsInChildren[j].garrMissionID))
					{
						componentsInChildren[j].missionResultsText.text = "<color=#00ff00ff>SUCCEEDED!</color>";
						MissionRewardDisplay[] componentsInChildren3 = componentsInChildren[j].missionRewardGroup.GetComponentsInChildren<MissionRewardDisplay>(true);
						for (int k = 0; k < componentsInChildren3.Length; k++)
						{
							GameObject gameObject = Object.Instantiate<GameObject>(this.missionRewardResultsDisplayPrefab);
							gameObject.transform.SetParent(this.missionRewardsIconArea.transform, false);
						}
					}
					else
					{
						componentsInChildren[j].missionResultsText.text = "<color=#ff0000ff>FAILED</color>";
					}
				}
				else
				{
					num++;
				}
				if (num == 0)
				{
					this.okButton.SetActive(true);
				}
			}
		}

		public void ShowMissionListAndRefreshData()
		{
			Debug.Log("Request Data Refresh");
			Main.instance.MobileRequestData();
			Main.instance.allPanels.ShowAdventureMap();
		}

		public GameObject missionReportView;

		public GameObject allianceCommander;

		public GameObject hordeCommander;

		public Text completedMissionsText;

		public GameObject missionResultsView;

		public GameObject missionRewardsIconArea;

		public GameObject missionListItemPrefab;

		public GameObject missionRewardResultsDisplayPrefab;

		public GameObject completedMissionListContents;

		public GameObject okButton;

		public Canvas mainCanvas;

		private Dictionary<int, bool> _requestedMissionCollection;
	}
}
