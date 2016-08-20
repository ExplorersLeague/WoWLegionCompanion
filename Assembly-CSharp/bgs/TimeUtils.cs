using System;
using System.Text;
using System.Text.RegularExpressions;

namespace bgs
{
	public class TimeUtils
	{
		public static long BinaryStamp()
		{
			return DateTime.UtcNow.ToBinary();
		}

		public static DateTime ConvertEpochMicrosecToDateTime(ulong microsec)
		{
			return TimeUtils.EPOCH_TIME.AddMilliseconds(microsec / 1000.0);
		}

		public static TimeSpan GetElapsedTimeSinceEpoch(DateTime? endDateTime = null)
		{
			DateTime d = (endDateTime == null) ? DateTime.UtcNow : endDateTime.Value;
			return d - TimeUtils.EPOCH_TIME;
		}

		public static void GetElapsedTime(int seconds, out TimeUtils.ElapsedTimeType timeType, out int time)
		{
			time = 0;
			if (seconds < 60)
			{
				timeType = TimeUtils.ElapsedTimeType.SECONDS;
				time = seconds;
				return;
			}
			if (seconds < 3600)
			{
				timeType = TimeUtils.ElapsedTimeType.MINUTES;
				time = seconds / 60;
				return;
			}
			int num = seconds / 86400;
			if (num == 0)
			{
				timeType = TimeUtils.ElapsedTimeType.HOURS;
				time = seconds / 3600;
				return;
			}
			if (num == 1)
			{
				timeType = TimeUtils.ElapsedTimeType.YESTERDAY;
				return;
			}
			int num2 = seconds / 604800;
			if (num2 == 0)
			{
				timeType = TimeUtils.ElapsedTimeType.DAYS;
				time = num;
				return;
			}
			if (num2 < 4)
			{
				timeType = TimeUtils.ElapsedTimeType.WEEKS;
				time = num2;
				return;
			}
			timeType = TimeUtils.ElapsedTimeType.MONTH_AGO;
		}

		public static string GetDevElapsedTimeString(TimeSpan span)
		{
			return TimeUtils.GetDevElapsedTimeString((long)span.TotalMilliseconds);
		}

		public static string GetDevElapsedTimeString(long ms)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			if (ms >= 3600000L)
			{
				TimeUtils.AppendDevTimeUnitsString("{0}h", 3600000, stringBuilder, ref ms, ref num);
			}
			if (ms >= 60000L)
			{
				TimeUtils.AppendDevTimeUnitsString("{0}m", 60000, stringBuilder, ref ms, ref num);
			}
			if (ms >= 1000L)
			{
				TimeUtils.AppendDevTimeUnitsString("{0}s", 1000, stringBuilder, ref ms, ref num);
			}
			if (num <= 1)
			{
				TimeUtils.AppendDevTimeUnitsString("{0}ms", 1, stringBuilder, ref ms, ref num);
			}
			return stringBuilder.ToString();
		}

		public static bool TryParseDevSecFromElapsedTimeString(string timeStr, out float sec)
		{
			sec = 0f;
			MatchCollection matchCollection = Regex.Matches(timeStr, "(?<number>(?:[0-9]+,)*[0-9]+)\\s*(?<units>[a-zA-Z]+)");
			if (matchCollection.Count == 0)
			{
				return false;
			}
			Match match = matchCollection[0];
			if (!match.Groups[0].Success)
			{
				return false;
			}
			Group group = match.Groups["number"];
			Group group2 = match.Groups["units"];
			if (!group.Success || !group2.Success)
			{
				return false;
			}
			string value = group.Value;
			string text = group2.Value;
			if (!float.TryParse(value, out sec))
			{
				return false;
			}
			text = TimeUtils.ParseTimeUnitsStr(text);
			if (text == "min")
			{
				sec *= 60f;
			}
			else if (text == "hour")
			{
				sec *= 3600f;
			}
			return true;
		}

		public static float ForceDevSecFromElapsedTimeString(string timeStr)
		{
			float result;
			TimeUtils.TryParseDevSecFromElapsedTimeString(timeStr, out result);
			return result;
		}

		private static void AppendDevTimeUnitsString(string formatString, int msPerUnit, StringBuilder builder, ref long ms, ref int unitCount)
		{
			long num = ms / (long)msPerUnit;
			if (num > 0L)
			{
				if (unitCount > 0)
				{
					builder.Append(' ');
				}
				builder.AppendFormat(formatString, num);
				unitCount++;
			}
			ms -= num * (long)msPerUnit;
		}

		private static string ParseTimeUnitsStr(string unitsStr)
		{
			if (unitsStr == null)
			{
				return "sec";
			}
			unitsStr = unitsStr.ToLowerInvariant();
			string text = unitsStr;
			switch (text)
			{
			case "s":
			case "sec":
			case "secs":
			case "second":
			case "seconds":
				return "sec";
			case "m":
			case "min":
			case "mins":
			case "minute":
			case "minutes":
				return "min";
			case "h":
			case "hour":
			case "hours":
				return "hour";
			}
			return "sec";
		}

		public const int SEC_PER_MINUTE = 60;

		public const int SEC_PER_HOUR = 3600;

		public const int SEC_PER_DAY = 86400;

		public const int SEC_PER_WEEK = 604800;

		public const int MS_PER_SEC = 1000;

		public const int MS_PER_MINUTE = 60000;

		public const int MS_PER_HOUR = 3600000;

		public const string DEFAULT_TIME_UNITS_STR = "sec";

		public static readonly DateTime EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static readonly TimeUtils.ElapsedStringSet SPLASHSCREEN_DATETIME_STRINGSET = new TimeUtils.ElapsedStringSet
		{
			m_seconds = "GLOBAL_DATETIME_SPLASHSCREEN_SECONDS",
			m_minutes = "GLOBAL_DATETIME_SPLASHSCREEN_MINUTES",
			m_hours = "GLOBAL_DATETIME_SPLASHSCREEN_HOURS",
			m_yesterday = "GLOBAL_DATETIME_SPLASHSCREEN_DAY",
			m_days = "GLOBAL_DATETIME_SPLASHSCREEN_DAYS",
			m_weeks = "GLOBAL_DATETIME_SPLASHSCREEN_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_SPLASHSCREEN_MONTH"
		};

		public enum ElapsedTimeType
		{
			SECONDS,
			MINUTES,
			HOURS,
			YESTERDAY,
			DAYS,
			WEEKS,
			MONTH_AGO
		}

		public class ElapsedStringSet
		{
			public string m_seconds;

			public string m_minutes;

			public string m_hours;

			public string m_yesterday;

			public string m_days;

			public string m_weeks;

			public string m_monthAgo;
		}
	}
}
