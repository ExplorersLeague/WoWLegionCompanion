using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;

public class WorldQuestPanel : MonoBehaviour
{
	public int QuestID
	{
		get
		{
			return this.m_questID;
		}
		set
		{
			this.m_questID = value;
			MissionRewardDisplay[] componentsInChildren = this.m_lootGroupObj.GetComponentsInChildren<MissionRewardDisplay>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] != null)
				{
					Object.DestroyImmediate(componentsInChildren[i].gameObject);
				}
			}
			if (this.m_questID > 0)
			{
				MobileWorldQuest mobileWorldQuest = (MobileWorldQuest)WorldQuestData.worldQuestDictionary[this.m_questID];
				if (mobileWorldQuest != null)
				{
					this.m_worldQuestNameText.text = mobileWorldQuest.QuestTitle;
					this.m_worldQuestDescriptionText.text = mobileWorldQuest.QuestTitle;
					int num = (int)((long)mobileWorldQuest.EndTime - GarrisonStatus.CurrentTime());
					if (num < 0)
					{
						num = 0;
					}
					Duration duration = new Duration(num, false);
					this.m_worldQuestTimeText.text = duration.DurationString;
					MissionRewardDisplay.InitWorldQuestRewards(mobileWorldQuest, this.m_missionRewardDisplayPrefab.gameObject, this.m_lootGroupObj.transform);
				}
			}
		}
	}

	private void Awake()
	{
		this.m_sliderPanel = base.GetComponent<SliderPanel>();
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.OnZoomOutMap = (Action)Delegate.Combine(instance.OnZoomOutMap, new Action(this.OnZoomOutMap));
		AdventureMapPanel instance2 = AdventureMapPanel.instance;
		instance2.MissionMapSelectionChangedAction = (Action<int>)Delegate.Combine(instance2.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
		AdventureMapPanel instance3 = AdventureMapPanel.instance;
		instance3.OnShowMissionRewardPanel = (Action<bool>)Delegate.Combine(instance3.OnShowMissionRewardPanel, new Action<bool>(this.OnShowMissionRewardPanel));
		AdventureMapPanel instance4 = AdventureMapPanel.instance;
		instance4.WorldQuestChangedAction = (Action<int>)Delegate.Combine(instance4.WorldQuestChangedAction, new Action<int>(this.HandleWorldQuestChanged));
	}

	public void OnZoomOutMap()
	{
		this.m_sliderPanel.HideSliderPanel();
	}

	public void HandleMissionChanged(int garrMissionID)
	{
		if (garrMissionID != 0)
		{
			this.m_sliderPanel.HideSliderPanel();
		}
	}

	private void OnShowMissionRewardPanel(bool show)
	{
		this.m_sliderPanel.HideSliderPanel();
	}

	private void HandleWorldQuestChanged(int worldQuestID)
	{
		this.QuestID = worldQuestID;
		if (this.QuestID != 0)
		{
			this.m_sliderPanel.ShowSliderPanel();
		}
		else
		{
			this.m_sliderPanel.HideSliderPanel();
		}
	}

	public SliderPanel m_sliderPanel;

	public Text m_worldQuestNameText;

	public Text m_worldQuestDescriptionText;

	public Text m_worldQuestTimeText;

	public MissionRewardDisplay m_missionRewardDisplayPrefab;

	public GameObject m_lootGroupObj;

	private int m_questID;
}
