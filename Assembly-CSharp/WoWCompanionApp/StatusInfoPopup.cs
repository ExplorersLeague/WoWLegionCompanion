using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class StatusInfoPopup : MonoBehaviour
	{
		private void Awake()
		{
			this.m_popupView.SetActive(false);
		}

		public void SetStatusText(string statusText)
		{
			this.m_statusText.text = statusText;
		}

		public void Show()
		{
			Main.instance.m_UISound.Play_ShowGenericTooltip();
			this.m_popupView.SetActive(true);
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		}

		public void Hide()
		{
			this.m_popupView.SetActive(false);
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		}

		public GameObject m_popupView;

		public Text m_statusText;
	}
}
