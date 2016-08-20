using System;
using UnityEngine;
using WowStatConstants;

public class MissionPanelSlider : MonoBehaviour
{
	private void OnEnable()
	{
		this.m_sliderPanel = base.GetComponent<SliderPanel>();
		this.m_sliderPanel.m_masterCanvasGroup.alpha = 0f;
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.OnZoomOutMap = (Action)Delegate.Combine(instance.OnZoomOutMap, new Action(this.OnZoomOutMap));
		if (this.m_usedForMissionList)
		{
			AdventureMapPanel instance2 = AdventureMapPanel.instance;
			instance2.MissionSelectedFromListAction = (Action<int>)Delegate.Combine(instance2.MissionSelectedFromListAction, new Action<int>(this.HandleMissionChanged));
		}
		else
		{
			AdventureMapPanel instance3 = AdventureMapPanel.instance;
			instance3.MissionMapSelectionChangedAction = (Action<int>)Delegate.Combine(instance3.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
		}
		AdventureMapPanel instance4 = AdventureMapPanel.instance;
		instance4.OnShowMissionRewardPanel = (Action<bool>)Delegate.Combine(instance4.OnShowMissionRewardPanel, new Action<bool>(this.OnShowMissionRewardPanel));
		SliderPanel sliderPanel = this.m_sliderPanel;
		sliderPanel.SliderPanelMaximizedAction = (Action)Delegate.Combine(sliderPanel.SliderPanelMaximizedAction, new Action(this.OnSliderPanelMaximized));
		SliderPanel sliderPanel2 = this.m_sliderPanel;
		sliderPanel2.SliderPanelBeginMinimizeAction = (Action)Delegate.Combine(sliderPanel2.SliderPanelBeginMinimizeAction, new Action(this.RevealMap));
		SliderPanel sliderPanel3 = this.m_sliderPanel;
		sliderPanel3.SliderPanelBeginDragAction = (Action)Delegate.Combine(sliderPanel3.SliderPanelBeginDragAction, new Action(this.RevealMap));
		SliderPanel sliderPanel4 = this.m_sliderPanel;
		sliderPanel4.SliderPanelBeginShrinkToPreviewPositionAction = (Action)Delegate.Combine(sliderPanel4.SliderPanelBeginShrinkToPreviewPositionAction, new Action(this.RevealMap));
		SliderPanel sliderPanel5 = this.m_sliderPanel;
		sliderPanel5.SliderPanelFinishMinimizeAction = (Action)Delegate.Combine(sliderPanel5.SliderPanelFinishMinimizeAction, new Action(this.HandleSliderPanelFinishMinimize));
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideSliderPanel, this.m_sliderPanel.gameObject);
	}

	private void OnDisable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.OnZoomOutMap = (Action)Delegate.Remove(instance.OnZoomOutMap, new Action(this.OnZoomOutMap));
		if (this.m_usedForMissionList)
		{
			AdventureMapPanel instance2 = AdventureMapPanel.instance;
			instance2.MissionSelectedFromListAction = (Action<int>)Delegate.Remove(instance2.MissionSelectedFromListAction, new Action<int>(this.HandleMissionChanged));
		}
		else
		{
			AdventureMapPanel instance3 = AdventureMapPanel.instance;
			instance3.MissionMapSelectionChangedAction = (Action<int>)Delegate.Remove(instance3.MissionMapSelectionChangedAction, new Action<int>(this.HandleMissionChanged));
		}
		AdventureMapPanel instance4 = AdventureMapPanel.instance;
		instance4.OnShowMissionRewardPanel = (Action<bool>)Delegate.Remove(instance4.OnShowMissionRewardPanel, new Action<bool>(this.OnShowMissionRewardPanel));
		SliderPanel sliderPanel = this.m_sliderPanel;
		sliderPanel.SliderPanelMaximizedAction = (Action)Delegate.Remove(sliderPanel.SliderPanelMaximizedAction, new Action(this.OnSliderPanelMaximized));
		SliderPanel sliderPanel2 = this.m_sliderPanel;
		sliderPanel2.SliderPanelBeginMinimizeAction = (Action)Delegate.Remove(sliderPanel2.SliderPanelBeginMinimizeAction, new Action(this.RevealMap));
		SliderPanel sliderPanel3 = this.m_sliderPanel;
		sliderPanel3.SliderPanelBeginDragAction = (Action)Delegate.Remove(sliderPanel3.SliderPanelBeginDragAction, new Action(this.RevealMap));
		SliderPanel sliderPanel4 = this.m_sliderPanel;
		sliderPanel4.SliderPanelBeginShrinkToPreviewPositionAction = (Action)Delegate.Remove(sliderPanel4.SliderPanelBeginShrinkToPreviewPositionAction, new Action(this.RevealMap));
		SliderPanel sliderPanel5 = this.m_sliderPanel;
		sliderPanel5.SliderPanelFinishMinimizeAction = (Action)Delegate.Remove(sliderPanel5.SliderPanelFinishMinimizeAction, new Action(this.HandleSliderPanelFinishMinimize));
		Main.instance.m_backButtonManager.PopBackAction();
	}

	private void OnSliderPanelMaximized()
	{
	}

	private void HandleSliderPanelFinishMinimize()
	{
		AdventureMapPanel.instance.SelectMissionFromMap(0);
	}

	private void RevealMap()
	{
	}

	private void OnFollowerDetailViewSliderPanelMaximized()
	{
		this.m_missionDetailView.m_topLevelDetailViewCanvasGroup.alpha = 0f;
	}

	private void RevealMissionDetails()
	{
		this.m_missionDetailView.m_topLevelDetailViewCanvasGroup.alpha = 1f;
	}

	public void OnZoomOutMap()
	{
		this.m_sliderPanel.HideSliderPanel();
	}

	public void HandleMissionChanged(int garrMissionID)
	{
		if (garrMissionID > 0)
		{
			if (this.m_disablePreview)
			{
				this.m_sliderPanel.MaximizeSliderPanel();
			}
			else
			{
				this.m_sliderPanel.ShowSliderPanel();
			}
		}
		else
		{
			this.m_sliderPanel.HideSliderPanel();
		}
		iTween.StopByName(base.gameObject, "bounce");
		if (!this.m_disablePreview)
		{
			iTween.PunchPosition(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"bounce",
				"y",
				16,
				"time",
				2.2,
				"delay",
				4,
				"looptype",
				"loop"
			}));
		}
	}

	private void OnShowMissionRewardPanel(bool show)
	{
		if (show)
		{
			this.m_sliderPanel.HideSliderPanel();
		}
	}

	public void StopTheBounce()
	{
		iTween.StopByName(base.gameObject, "bounce");
	}

	public void PlayMinimizeSound()
	{
		Main.instance.m_UISound.Play_DefaultNavClick();
	}

	public MissionDetailView m_missionDetailView;

	public FollowerDetailView m_followerDetailView;

	public bool m_isVertical;

	public bool m_disablePreview;

	public bool m_usedForMissionList;

	public SliderPanel m_sliderPanel;

	private int m_garrFollowerID;
}
