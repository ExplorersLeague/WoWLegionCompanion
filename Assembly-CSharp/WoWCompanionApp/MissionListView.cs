using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionListView : MonoBehaviour
	{
		private void Start()
		{
		}

		private void Update()
		{
		}

		private void InitMissionList()
		{
			RectTransform[] componentsInChildren = this.missionListViewContents.GetComponentsInChildren<RectTransform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] != null && componentsInChildren[i] != this.missionListViewContents.transform)
				{
					componentsInChildren[i].gameObject.transform.SetParent(null);
					Object.Destroy(componentsInChildren[i].gameObject);
				}
			}
			List<WrapperGarrisonMission> list = PersistentMissionData.missionDictionary.Values.ToList<WrapperGarrisonMission>();
			if (this.isInProgressMissionList)
			{
				list = (from mission in list
				orderby mission.StartTime + mission.MissionDuration
				select mission).ToList<WrapperGarrisonMission>();
			}
			else
			{
				list = (from mission in list
				orderby StaticDB.garrMissionDB.GetRecord(mission.MissionRecID).TargetLevel
				select mission).ToList<WrapperGarrisonMission>();
			}
			foreach (WrapperGarrisonMission wrapperGarrisonMission in list)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null)
				{
					if (record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
					{
						if (this.isInProgressMissionList)
						{
							if (wrapperGarrisonMission.MissionState == 0)
							{
								continue;
							}
							if (wrapperGarrisonMission.MissionState == 1)
							{
								TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
								if ((wrapperGarrisonMission.MissionDuration - t).TotalSeconds <= 0.0)
								{
									continue;
								}
							}
						}
						if (this.isInProgressMissionList || wrapperGarrisonMission.MissionState == 0)
						{
							GameObject gameObject = Object.Instantiate<GameObject>(this.missionListItemPrefab);
							gameObject.transform.SetParent(this.missionListViewContents.transform, false);
							MissionListItem component = gameObject.GetComponent<MissionListItem>();
							component.Init(record.ID);
						}
					}
				}
			}
		}

		public void OnUIRefresh()
		{
			this.InitMissionList();
		}

		private PersistentMissionData missionData;

		public GameObject missionListViewContents;

		public GameObject missionListItemPrefab;

		public GameObject collectLootListItemPrefab;

		public bool isInProgressMissionList;

		public GameObject missionRewardDisplayPrefab;
	}
}
