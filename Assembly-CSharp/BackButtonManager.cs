using System;
using System.Collections.Generic;
using UnityEngine;
using WowStatConstants;

public class BackButtonManager : MonoBehaviour
{
	private void Awake()
	{
		this.m_backActionStack = new Stack<BackActionData>(10);
	}

	public void PushBackAction(BackAction backAction, GameObject backActionTarget = null)
	{
		BackActionData item = default(BackActionData);
		item.m_backAction = backAction;
		item.m_backActionTarget = backActionTarget;
		this.m_backActionStack.Push(item);
	}

	public BackActionData PopBackAction()
	{
		return this.m_backActionStack.Pop();
	}

	private void Update()
	{
		if (Input.GetKeyDown("escape"))
		{
			if (this.m_backActionStack.Count == 0)
			{
				return;
			}
			BackActionData backActionData = this.m_backActionStack.Peek();
			switch (backActionData.m_backAction)
			{
			case BackAction.hideAllPopups:
				AllPopups.instance.HideAllPopups();
				break;
			case BackAction.hideSliderPanel:
				if (backActionData.m_backActionTarget != null)
				{
					SliderPanel component = backActionData.m_backActionTarget.GetComponent<SliderPanel>();
					if (component != null)
					{
						component.HideSliderPanel();
					}
				}
				break;
			case BackAction.hideMissionResults:
				AllPanels.instance.m_missionResultsPanel.HideMissionResults();
				break;
			case BackAction.hideMissionDialog:
				AllPopups.instance.m_missionDialog.m_missionDetailView.HideMissionDetailView();
				break;
			}
		}
	}

	private Stack<BackActionData> m_backActionStack;
}
