using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CalendarEventItem : MonoBehaviour
	{
		public void SetEventInfo(string date, string time, string eventName, string subtext)
		{
			this.m_date.text = date;
			this.m_time.text = time;
			this.m_eventName.text = eventName;
			this.m_subtext.text = subtext;
		}

		public void OpenEventItem()
		{
			AllPopups.instance.OpenCalendarDialog(this);
		}

		public Text m_date;

		public Text m_time;

		public Text m_eventName;

		public Text m_subtext;
	}
}
