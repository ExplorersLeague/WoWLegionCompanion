using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class DebugOptionsPopup : MonoBehaviour
	{
		private void Start()
		{
			if (this.m_enableCheatCompleteMissionButton != null && this.m_cheatCompleteButton != null)
			{
				this.m_enableCheatCompleteMissionButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableCheatCompleteButton));
			}
			if (this.m_showTouchKeyboardStateButton != null && this.m_touchKeyboardDebugObject != null)
			{
				this.m_showTouchKeyboardStateButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableTouchScreenDebugObject));
			}
		}

		private void OnEnable()
		{
			if (this.m_enableCheatCompleteMissionButton != null && this.m_cheatCompleteButton != null)
			{
				this.m_enableCheatCompleteMissionButton.isOn = this.m_cheatCompleteButton.activeSelf;
			}
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void Update()
		{
		}

		private void OnDisable()
		{
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void OnValueChanged_EnableCheatCompleteButton(bool isOn)
		{
			this.m_cheatCompleteButton.SetActive(isOn);
		}

		private void OnValueChanged_EnableTouchScreenDebugObject(bool isOn)
		{
			this.m_touchKeyboardDebugObject.SetActive(isOn);
		}

		public Toggle m_enableCheatCompleteMissionButton;

		public Toggle m_showTouchKeyboardStateButton;

		public GameObject m_cheatCompleteButton;

		public GameObject m_touchKeyboardDebugObject;
	}
}
