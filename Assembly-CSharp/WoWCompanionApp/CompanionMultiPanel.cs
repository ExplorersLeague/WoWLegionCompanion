using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CompanionMultiPanel : MonoBehaviour
	{
		private void Awake()
		{
			if (this.m_initialPanel == null)
			{
				this.m_initialPanel = this.m_gameCanvasGroup;
			}
			this.m_currentPanel = this.m_initialPanel;
		}

		public void ShowPanel(CanvasGroup panel)
		{
			this.m_currentPanel.gameObject.SetActive(false);
			this.m_currentPanel = panel;
			this.m_currentPanel.gameObject.SetActive(true);
		}

		private void OnEnable()
		{
			FPSThrottler.SetForceNormalFPS(true);
			this.m_characterName.text = Singleton<CharacterData>.instance.CharacterName;
		}

		private void OnDisable()
		{
			FPSThrottler.SetForceNormalFPS(false);
		}

		public CanvasGroup m_characterCanvasGroup;

		public CanvasGroup m_eventsCanvasGroup;

		public CanvasGroup m_gameCanvasGroup;

		public CanvasGroup m_newsCanvasGroup;

		public CanvasGroup m_socialCanvasGroup;

		public Text m_characterName;

		public CanvasGroup m_initialPanel;

		private CanvasGroup m_currentPanel;
	}
}
