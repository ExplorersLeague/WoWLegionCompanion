using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class OKCancelDialog : MonoBehaviour
	{
		public event Action onOK;

		public event Action onCancel;

		private void Awake()
		{
			base.transform.Find("PopupView").localScale = new Vector3(0.8f, 0.8f, 1f);
		}

		private void OnEnable()
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(new BackButtonManager.BackAction(this.OnClickCancel));
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}

		public void SetText(string titleKey, string messageKey = null)
		{
			this.m_titleText.text = StaticDB.GetString(titleKey, null);
			if (messageKey != null)
			{
				this.m_messageText.text = StaticDB.GetString(messageKey, null);
			}
			else
			{
				this.m_messageText.text = string.Empty;
			}
		}

		public void OnClickOkay()
		{
			this.onOK();
			Object.Destroy(base.gameObject);
		}

		public void OnClickCancel()
		{
			this.onCancel();
			Object.Destroy(base.gameObject);
		}

		public Text m_titleText;

		public Text m_messageText;
	}
}
