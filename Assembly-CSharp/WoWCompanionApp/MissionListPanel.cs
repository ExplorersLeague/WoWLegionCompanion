using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionListPanel : MonoBehaviour
	{
		private void Start()
		{
			this.m_currentCheatMissionID = 0;
		}

		private void Update()
		{
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				bool flag = false;
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null)
				{
					if (record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
					{
						if (wrapperGarrisonMission.MissionState == 1)
						{
							TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
							if ((wrapperGarrisonMission.MissionDuration - t).TotalSeconds <= 0.0)
							{
								flag = true;
							}
						}
						if (wrapperGarrisonMission.MissionState == 2)
						{
							flag = true;
						}
						if (flag)
						{
						}
					}
				}
			}
		}

		public void ShowAvailableMissionList()
		{
			this.availableMissionListView.gameObject.SetActive(true);
			this.inProgressMissionListView.gameObject.SetActive(false);
			this.availableMissionsButtonHighlight.gameObject.SetActive(true);
			this.inProgressMissionsButtonHighlight.gameObject.SetActive(false);
		}

		public void ShowInProgressMissionList()
		{
			this.availableMissionListView.gameObject.SetActive(false);
			this.inProgressMissionListView.gameObject.SetActive(true);
			this.availableMissionsButtonHighlight.gameObject.SetActive(false);
			this.inProgressMissionsButtonHighlight.gameObject.SetActive(true);
		}

		public void OnUIRefresh()
		{
			this.availableMissionListView.OnUIRefresh();
			this.inProgressMissionListView.OnUIRefresh();
			MissionListItem[] componentsInChildren = this.availableMissionListContents.GetComponentsInChildren<MissionListItem>(true);
			MissionListItem[] componentsInChildren2 = this.inProgressMissionListContents.GetComponentsInChildren<MissionListItem>(true);
			this.availableMissionsButtonText.text = "Available - " + componentsInChildren.Length;
			this.inProgressMissionsButtonText.text = "In Progress - " + componentsInChildren2.Length;
		}

		public void SetCharacterName(string name)
		{
			this.characterNameText.text = name;
		}

		public void SetCharacterLevelAndClass(int level, int charClassID)
		{
		}

		public Text characterNameText;

		public Text characterLevelAndClassText;

		public Image availableMissionsButtonHighlight;

		public Text availableMissionsButtonText;

		public MissionListView availableMissionListView;

		public GameObject availableMissionListContents;

		public Image inProgressMissionsButtonHighlight;

		public Text inProgressMissionsButtonText;

		public MissionListView inProgressMissionListView;

		public GameObject inProgressMissionListContents;

		public GameObject CompleteMissionCheatPopup;

		private int m_currentCheatMissionID;
	}
}
