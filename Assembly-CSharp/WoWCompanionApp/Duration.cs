using System;
using System.Text;
using GarbageFreeStringBuilder;

namespace WoWCompanionApp
{
	public class Duration
	{
		public Duration(int duration, bool displayLargestUnitOnly = false)
		{
			this.FormatDurationString(duration, displayLargestUnitOnly);
		}

		public int Days
		{
			get
			{
				return this.m_days;
			}
		}

		public int Hours
		{
			get
			{
				return this.m_hours;
			}
		}

		public int Minutes
		{
			get
			{
				return this.m_minutes;
			}
		}

		public int RemainderMinutes
		{
			get
			{
				return this.m_remainderMinutes;
			}
		}

		public int DurationValue
		{
			get
			{
				return this.m_seconds;
			}
		}

		public string DurationString
		{
			get
			{
				return (this.m_sb != null) ? this.m_sb.ToString() : string.Empty;
			}
		}

		private void InitStrings()
		{
			if (Duration.m_dayText == null)
			{
				Duration.m_dayText = StaticDB.GetString("DAY_ABBREVIATION", null);
			}
			if (Duration.m_daysText == null)
			{
				Duration.m_daysText = StaticDB.GetString("DAYS_ABBREVIATION", null);
			}
			if (Duration.m_hourText == null)
			{
				Duration.m_hourText = StaticDB.GetString("HOUR_ABBREVIATION", null);
			}
			if (Duration.m_minuteText == null)
			{
				Duration.m_minuteText = StaticDB.GetString("MINUTE_ABBREVIATION", null);
			}
			if (Duration.m_secondText == null)
			{
				Duration.m_secondText = StaticDB.GetString("SECOND_ABBREVIATION", null);
			}
		}

		public void FormatDurationString(int duration, bool displayLargestUnitOnly = false)
		{
			this.InitStrings();
			if (this.m_sb == null)
			{
				this.m_sb = new StringBuilder(16);
			}
			this.m_sb.Length = 0;
			this.m_seconds = duration;
			this.m_minutes = this.m_seconds / 60;
			this.m_hours = this.m_minutes / 60;
			this.m_days = this.m_hours / 24;
			this.m_remainderHours = this.m_hours - 24 * this.m_days;
			this.m_remainderMinutes = this.m_minutes - 60 * this.m_hours;
			this.m_remainderSeconds = this.m_seconds - 60 * this.m_minutes;
			if (this.m_days > 0)
			{
				this.m_sb.ConcatFormat("{0} {1}", this.m_days, (this.m_days != 1) ? Duration.m_daysText : Duration.m_dayText);
				if (!displayLargestUnitOnly && this.m_remainderHours > 0)
				{
					this.m_sb.ConcatFormat(" {0} {1}", this.m_remainderHours, Duration.m_hourText);
				}
			}
			else if (this.m_hours > 0)
			{
				this.m_sb.ConcatFormat("{0} {1}", this.m_hours, Duration.m_hourText);
				if (!displayLargestUnitOnly && this.m_remainderMinutes > 0)
				{
					this.m_sb.ConcatFormat(" {0} {1}", this.m_remainderMinutes, Duration.m_minuteText);
				}
			}
			else if (this.m_minutes > 0)
			{
				this.m_sb.ConcatFormat("{0} {1}", this.m_minutes, Duration.m_minuteText);
				if (!displayLargestUnitOnly && this.m_remainderSeconds > 0)
				{
					this.m_sb.ConcatFormat(" {0} {1}", this.m_remainderSeconds, Duration.m_secondText);
				}
			}
			else if (this.m_seconds > 0)
			{
				this.m_sb.ConcatFormat("{0} {1}", this.m_seconds, Duration.m_secondText);
			}
		}

		private int m_days;

		private int m_hours;

		private int m_minutes;

		private int m_remainderHours;

		private int m_remainderMinutes;

		private int m_remainderSeconds;

		private int m_seconds;

		private StringBuilder m_sb;

		private static string m_dayText;

		private static string m_daysText;

		private static string m_hourText;

		private static string m_minuteText;

		private static string m_secondText;
	}
}
