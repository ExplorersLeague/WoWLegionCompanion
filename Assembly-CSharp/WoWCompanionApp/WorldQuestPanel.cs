using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
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
						Object.Destroy(componentsInChildren[i].gameObject);
					}
				}
				if (this.m_questID > 0 && WorldQuestData.WorldQuestDictionary.ContainsKey(this.m_questID))
				{
					WrapperWorldQuest worldQuest = WorldQuestData.WorldQuestDictionary[this.m_questID];
					this.m_worldQuestNameText.text = worldQuest.QuestTitle;
					this.m_worldQuestDescriptionText.text = worldQuest.QuestTitle;
					TimeSpan timeSpan = worldQuest.EndTime - GarrisonStatus.CurrentTime();
					if (timeSpan.TotalSeconds < 0.0)
					{
						timeSpan = TimeSpan.Zero;
					}
					this.m_worldQuestTimeText.text = timeSpan.GetDurationString(false, TimeUnit.Second);
					MissionRewardDisplay.InitWorldQuestRewards(worldQuest, this.m_missionRewardDisplayPrefab.gameObject, this.m_lootGroupObj.transform);
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
}
