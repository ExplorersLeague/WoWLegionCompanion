using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class LogPanel : MonoBehaviour
	{
		public void ShowPanel()
		{
			base.gameObject.SetActive(true);
			this.UpdateLogDisplay();
		}

		public void ClosePanel()
		{
			base.gameObject.SetActive(false);
		}

		public void ClearLog()
		{
			this.m_logger.ClearLog();
			this.UpdateLogDisplay();
		}

		public void UpdateLogDisplay()
		{
			int num = 1;
			this.m_logText.text = string.Empty;
			foreach (string text in this.m_logger.LogLines)
			{
				if (num > 1)
				{
					Text logText = this.m_logText;
					logText.text += "\n\n";
				}
				Text logText2 = this.m_logText;
				string text2 = logText2.text;
				logText2.text = string.Concat(new object[]
				{
					text2,
					num,
					") ",
					text
				});
				num++;
			}
		}

		public Text m_logText;

		public Logger m_logger;
	}
}
