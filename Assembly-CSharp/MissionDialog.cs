using System;
using UnityEngine;

public class MissionDialog : MonoBehaviour
{
	private void OnEnable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.MissionSelectedFromListAction = (Action<int>)Delegate.Combine(instance.MissionSelectedFromListAction, new Action<int>(this.HandleMissionSelected));
	}

	private void OnDisable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.MissionSelectedFromListAction = (Action<int>)Delegate.Remove(instance.MissionSelectedFromListAction, new Action<int>(this.HandleMissionSelected));
	}

	private void HandleMissionSelected(int garrMissionID)
	{
		if (garrMissionID == 0)
		{
			this.m_missionDetailView.gameObject.SetActive(false);
			return;
		}
		this.m_missionDetailView.gameObject.SetActive(true);
		this.m_missionDetailView.HandleMissionSelected(garrMissionID);
		this.m_followerListView.m_missionDetailView = this.m_missionDetailView;
		this.m_followerListView.InitFollowerList();
		this.m_followerListView.HandleMissionChanged(garrMissionID);
	}

	public MissionDetailView m_missionDetailView;

	public FollowerListView m_followerListView;
}
