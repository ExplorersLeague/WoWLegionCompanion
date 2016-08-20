using System;
using UnityEngine;
using WowJamMessages;
using WowStaticData;

public class RewardPanelSlider : MonoBehaviour
{
	private void Awake()
	{
		this.ClearRewardIcons();
		this.m_sliderPanel = base.GetComponent<SliderPanel>();
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.OnZoomOutMap = (Action)Delegate.Combine(instance.OnZoomOutMap, new Action(this.OnZoomOutMap));
		AdventureMapPanel instance2 = AdventureMapPanel.instance;
		instance2.MissionMapSelectionChangedAction = (Action<int>)Delegate.Combine(instance2.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
		AdventureMapPanel instance3 = AdventureMapPanel.instance;
		instance3.OnAddMissionLootToRewardPanel = (Action<int>)Delegate.Combine(instance3.OnAddMissionLootToRewardPanel, new Action<int>(this.OnAddMissionLootToRewardPanel));
		AdventureMapPanel instance4 = AdventureMapPanel.instance;
		instance4.OnShowMissionRewardPanel = (Action<bool>)Delegate.Combine(instance4.OnShowMissionRewardPanel, new Action<bool>(this.OnShowMissionRewardPanel));
	}

	private void OnZoomOutMap()
	{
		this.m_sliderPanel.HideSliderPanel();
	}

	public void ClearRewardIcons()
	{
		MissionRewardDisplay[] componentsInChildren = this.m_rewardIconArea.GetComponentsInChildren<MissionRewardDisplay>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.DestroyImmediate(componentsInChildren[i].gameObject);
		}
	}

	public void OnShowMissionRewardPanel(bool show)
	{
		if (show)
		{
			this.m_sliderPanel.ShowSliderPanel();
		}
		else
		{
			this.m_sliderPanel.HideSliderPanel();
		}
	}

	public void OnAddMissionLootToRewardPanel(int garrMissionID)
	{
		JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)PersistentMissionData.missionDictionary[garrMissionID];
		MissionRewardDisplay.InitMissionRewards(AdventureMapPanel.instance.m_missionRewardResultsDisplayPrefab, this.m_rewardIconArea.transform, jamGarrisonMobileMission.Reward);
		if (jamGarrisonMobileMission.MissionState != 3)
		{
			return;
		}
		GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
		if (record == null)
		{
			return;
		}
		if (StaticDB.rewardPackDB.GetRecord(record.OvermaxRewardPackID) == null)
		{
			return;
		}
		if (jamGarrisonMobileMission.OvermaxReward.Length > 0)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(AdventureMapPanel.instance.m_missionRewardResultsDisplayPrefab);
			gameObject.transform.SetParent(this.m_rewardIconArea.transform, false);
			MissionRewardDisplay component = gameObject.GetComponent<MissionRewardDisplay>();
			component.InitReward(MissionRewardDisplay.RewardType.item, jamGarrisonMobileMission.OvermaxReward[0].ItemID, (int)jamGarrisonMobileMission.OvermaxReward[0].ItemQuantity, 0, jamGarrisonMobileMission.OvermaxReward[0].ItemFileDataID);
		}
	}

	public void OnShowMissionRewardSlider(bool show)
	{
		if (show)
		{
			this.m_sliderPanel.ShowSliderPanel();
		}
		else
		{
			this.m_sliderPanel.HideSliderPanel();
		}
	}

	private void HandleMissionChanged(int garrMissionID)
	{
		this.m_sliderPanel.HideSliderPanel();
	}

	public SliderPanel m_sliderPanel;

	public GameObject m_rewardIconArea;

	public bool m_isVertical;
}
