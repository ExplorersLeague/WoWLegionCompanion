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
			this.m_enableDetailedZoneMaps.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableDetailedZoneMaps));
			this.m_enableAutoZoomInOut.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableAutoZoomInOut));
			this.m_enableTapToZoomOut.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableTapToZoomOut));
			if (this.m_enableCheatCompleteMissionButton != null && this.m_cheatCompleteButton != null)
			{
				this.m_enableCheatCompleteMissionButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableCheatCompleteButton));
			}
		}

		private void OnEnable()
		{
			if (this.m_adventureMapPanel.gameObject.activeSelf)
			{
				this.m_enableDetailedZoneMaps.isOn = this.m_adventureMapPanel.m_testEnableDetailedZoneMaps;
				this.m_enableAutoZoomInOut.isOn = this.m_adventureMapPanel.m_testEnableAutoZoomInOut;
				this.m_enableTapToZoomOut.isOn = this.m_adventureMapPanel.m_testEnableTapToZoomOut;
			}
			if (this.m_enableCheatCompleteMissionButton != null && this.m_cheatCompleteButton != null)
			{
				this.m_enableCheatCompleteMissionButton.isOn = this.m_cheatCompleteButton.activeSelf;
			}
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void Update()
		{
			this.m_enableDetailedZoneMaps.gameObject.SetActive(this.m_adventureMapPanel.gameObject.activeSelf);
			this.m_enableAutoZoomInOut.gameObject.SetActive(this.m_adventureMapPanel.gameObject.activeSelf);
			this.m_enableTapToZoomOut.gameObject.SetActive(this.m_adventureMapPanel.gameObject.activeSelf);
		}

		private void OnDisable()
		{
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void OnValueChanged_EnableDetailedZoneMaps(bool isOn)
		{
			this.m_adventureMapPanel.m_testEnableDetailedZoneMaps = isOn;
		}

		private void OnValueChanged_EnableAutoZoomInOut(bool isOn)
		{
			this.m_adventureMapPanel.m_testEnableAutoZoomInOut = isOn;
		}

		private void OnValueChanged_EnableTapToZoomOut(bool isOn)
		{
			this.m_adventureMapPanel.m_testEnableTapToZoomOut = isOn;
		}

		private void OnValueChanged_EnableCheatCompleteButton(bool isOn)
		{
			this.m_cheatCompleteButton.SetActive(isOn);
		}

		public void TestUIEffect()
		{
			UiAnimMgr.instance.PlayAnim("ItemReadyToUseGlowLoop", this.m_testEffectArea.transform, Vector3.zero, 2f, 0f);
		}

		public Toggle m_enableDetailedZoneMaps;

		public Toggle m_enableAutoZoomInOut;

		public Toggle m_enableTapToZoomOut;

		public Toggle m_enableCheatCompleteMissionButton;

		public GameObject m_cheatCompleteButton;

		public GameObject m_testEffectArea;

		public AdventureMapPanel m_adventureMapPanel;
	}
}
