using System;
using System.Text;
using GarbageFreeStringBuilder;

namespace WoWCompanionApp
{
	public static class DateTimeExtensions
	{
		private static void InitStrings()
		{
			if (DateTimeExtensions.m_dayText == null)
			{
				DateTimeExtensions.m_dayText = StaticDB.GetString("DAY_ABBREVIATION", null);
			}
			if (DateTimeExtensions.m_daysText == null)
			{
				DateTimeExtensions.m_daysText = StaticDB.GetString("DAYS_ABBREVIATION", null);
			}
			if (DateTimeExtensions.m_hourText == null)
			{
				DateTimeExtensions.m_hourText = StaticDB.GetString("HOUR_ABBREVIATION", null);
			}
			if (DateTimeExtensions.m_minuteText == null)
			{
				DateTimeExtensions.m_minuteText = StaticDB.GetString("MINUTE_ABBREVIATION", null);
			}
			if (DateTimeExtensions.m_secondText == null)
			{
				DateTimeExtensions.m_secondText = StaticDB.GetString("SECOND_ABBREVIATION", null);
			}
		}

		public static string GetDurationString(this TimeSpan timeSpan, bool displayLargestUnitOnly = false, TimeUnit smallestAllowedUnit = TimeUnit.Second)
		{
			DateTimeExtensions.InitStrings();
			StringBuilder stringBuilder = new StringBuilder();
			if (timeSpan.Days > 0 || smallestAllowedUnit == TimeUnit.Day)
			{
				stringBuilder.ConcatFormat("{0} {1}", timeSpan.Days, (timeSpan.Days != 1) ? DateTimeExtensions.m_daysText : DateTimeExtensions.m_dayText);
				if (!displayLargestUnitOnly && smallestAllowedUnit != TimeUnit.Day && timeSpan.Hours > 0)
				{
					stringBuilder.ConcatFormat(" {0} {1}", timeSpan.Hours, DateTimeExtensions.m_hourText);
				}
			}
			else if (timeSpan.Hours > 0 || smallestAllowedUnit == TimeUnit.Hour)
			{
				stringBuilder.ConcatFormat("{0} {1}", timeSpan.Hours, DateTimeExtensions.m_hourText);
				if (!displayLargestUnitOnly && smallestAllowedUnit != TimeUnit.Hour && timeSpan.Minutes > 0)
				{
					stringBuilder.ConcatFormat(" {0} {1}", timeSpan.Minutes, DateTimeExtensions.m_minuteText);
				}
			}
			else if (timeSpan.Minutes > 0 || smallestAllowedUnit == TimeUnit.Minute)
			{
				stringBuilder.ConcatFormat("{0} {1}", timeSpan.Minutes, DateTimeExtensions.m_minuteText);
				if (!displayLargestUnitOnly && smallestAllowedUnit != TimeUnit.Minute && timeSpan.Seconds > 0)
				{
					stringBuilder.ConcatFormat(" {0} {1}", timeSpan.Seconds, DateTimeExtensions.m_secondText);
				}
			}
			else if (timeSpan.Seconds > 0)
			{
				stringBuilder.ConcatFormat("{0} {1}", timeSpan.Seconds, DateTimeExtensions.m_secondText);
			}
			return stringBuilder.ToString();
		}

		private static string m_dayText;

		private static string m_daysText;

		private static string m_hourText;

		private static string m_minuteText;

		private static string m_secondText;
	}
}
