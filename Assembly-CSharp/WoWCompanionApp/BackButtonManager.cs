using System;
using System.Collections.Generic;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class BackButtonManager : MonoBehaviour
	{
		private void Awake()
		{
			this.m_backActionStack = new Stack<BackButtonManager.BackAction>(10);
		}

		public void PushBackAction(BackActionType backActionType, GameObject backActionTarget = null)
		{
			BackButtonManager.BackAction action = delegate
			{
			};
			switch (backActionType)
			{
			case BackActionType.hideAllPopups:
				action = delegate
				{
					if (AllPopups.instance != null)
					{
						AllPopups.instance.HideAllPopups();
					}
				};
				break;
			case BackActionType.hideSliderPanel:
				action = delegate
				{
					if (backActionTarget != null)
					{
						SliderPanel component = backActionTarget.GetComponent<SliderPanel>();
						if (component != null)
						{
							component.HideSliderPanel();
						}
					}
				};
				break;
			case BackActionType.hideMissionDialog:
				action = delegate
				{
					AllPopups.instance.m_missionDialog.m_missionDetailView.HideMissionDetailView();
				};
				break;
			case BackActionType.hideHamburgerMenu:
				action = delegate
				{
					AllPopups.instance.HideHamburgerMenu();
				};
				break;
			}
			this.PushBackAction(action);
		}

		public void PushBackAction(BackButtonManager.BackAction action)
		{
			this.m_backActionStack.Push(action);
		}

		public BackButtonManager.BackAction PopBackAction()
		{
			return this.m_backActionStack.Pop();
		}

		private void Update()
		{
			if (Input.GetKeyDown(27))
			{
				if (this.m_backActionStack.Count == 0)
				{
					return;
				}
				BackButtonManager.BackAction backAction = this.m_backActionStack.Peek();
				backAction();
			}
		}

		private Stack<BackButtonManager.BackAction> m_backActionStack;

		public delegate void BackAction();
	}
}
