using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class HolidayDetailsPanel : BaseDialog
	{
		public CalendarEventData EventData { get; private set; }

		public void SetCalendarEventData(CalendarEventData eventData)
		{
			this.EventData = eventData;
		}

		private IEnumerator Start()
		{
			base.GetComponent<CanvasGroup>().alpha = 0f;
			while (Calendar.IsActionPending())
			{
				yield return null;
			}
			while (this.EventData == null)
			{
				yield return null;
			}
			base.GetComponent<CanvasGroup>().alpha = 1f;
			int offsetMonth = this.EventData.EventTime.Month - DateAndTime.GetServerTimeLocal().Month;
			CalendarHolidayInfo eventInfo;
			if (!Calendar.GetHolidayInfo(offsetMonth, (uint)(this.EventData.EventTime.Day - 1), this.EventData.EventIndex, ref eventInfo))
			{
				this.CloseDialog();
				yield break;
			}
			this.m_title.text = eventInfo.name;
			this.m_description.text = eventInfo.description;
			this.m_date.text = this.EventData.StartTime.ToString(StaticDB.GetString("EVENT_DATE_AND_TIME", "M/d/yy h:mm tt")) + " - " + this.EventData.EndTime.ToString(StaticDB.GetString("EVENT_DATE_AND_TIME", "M/d/yy h:mm tt"));
			yield break;
		}

		public Text m_title;

		public Text m_description;

		public Text m_date;
	}
}
