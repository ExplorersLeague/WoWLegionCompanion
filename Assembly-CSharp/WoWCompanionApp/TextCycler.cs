using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[RequireComponent(typeof(Text))]
	[RequireComponent(typeof(CanvasGroup))]
	public class TextCycler : MonoBehaviour
	{
		private void Awake()
		{
			this.m_textStringList.AddRange(this.m_textStrings);
			this.m_canvasGroup = base.GetComponent<CanvasGroup>();
			this.m_text = base.GetComponent<Text>();
			this.m_currentSeconds = this.m_fadeSeconds;
		}

		private void Update()
		{
			if (this.m_textStringList.Count == 0)
			{
				return;
			}
			if (this.m_textStringList.Count == 1 || string.IsNullOrEmpty(this.m_text.text))
			{
				this.m_text.text = this.m_textStringList[0];
				this.m_currentIndex = 0;
				this.m_currentSeconds = this.m_fadeSeconds;
				this.m_canvasGroup.alpha = 1f;
				return;
			}
			this.m_currentSeconds += Time.deltaTime;
			float num = 2f * this.m_fadeSeconds + this.m_waitSeconds;
			bool flag = false;
			if (this.m_currentSeconds > num)
			{
				this.m_currentSeconds -= num;
				flag = true;
			}
			if (this.m_currentSeconds > this.m_fadeSeconds + this.m_waitSeconds)
			{
				this.m_canvasGroup.alpha = Mathf.Lerp(1f, 0f, (this.m_currentSeconds - (this.m_fadeSeconds + this.m_waitSeconds)) / this.m_fadeSeconds);
			}
			else if (this.m_currentSeconds > this.m_fadeSeconds)
			{
				this.m_canvasGroup.alpha = 1f;
			}
			else
			{
				this.m_canvasGroup.alpha = Mathf.Lerp(0f, 1f, this.m_currentSeconds / this.m_fadeSeconds);
				if (flag)
				{
					this.m_currentIndex++;
					if (this.m_currentIndex >= this.m_textStringList.Count)
					{
						this.m_currentIndex -= this.m_textStringList.Count;
					}
					this.m_text.text = this.m_textStringList[this.m_currentIndex];
				}
			}
		}

		public void AddString(string s)
		{
			this.m_textStringList.Add(s);
			this.m_currentSeconds = this.m_fadeSeconds;
		}

		public void ClearStrings()
		{
			this.m_textStringList.Clear();
			this.m_currentIndex = 0;
			this.m_currentSeconds = 0f;
			this.m_text.text = string.Empty;
		}

		public float m_fadeSeconds = 1f;

		public float m_waitSeconds = 2f;

		public string[] m_textStrings;

		private List<string> m_textStringList = new List<string>();

		private CanvasGroup m_canvasGroup;

		private Text m_text;

		private float m_currentSeconds;

		private int m_currentIndex;
	}
}
