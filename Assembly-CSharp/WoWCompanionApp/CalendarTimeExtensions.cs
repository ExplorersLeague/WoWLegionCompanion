using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public static class CalendarTimeExtensions
	{
		public static DateTime ToDateTime(this CalendarTime calendarTime)
		{
			DateTime result;
			try
			{
				result = new DateTime(calendarTime.year, (int)(calendarTime.month + 1u), (int)(calendarTime.monthDay + 1u), calendarTime.hour, calendarTime.minute, 0);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				Debug.LogException(ex);
				result = DateTime.MaxValue;
			}
			return result;
		}
	}
}
