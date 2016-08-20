using System;

public class Duration
{
	public Duration(int duration)
	{
		this.InitStrings();
		this.m_duration = duration;
		this.m_minutes = this.m_duration / 60;
		this.m_hours = this.m_minutes / 60;
		this.m_days = this.m_hours / 24;
		this.m_remainderMinutes = ((this.m_hours <= 0) ? this.m_minutes : (this.m_minutes - 60 * this.m_hours));
		if (this.m_days > 0)
		{
			this.m_durationString = string.Concat(new object[]
			{
				string.Empty,
				this.m_days,
				" ",
				(this.m_days != 1) ? Duration.m_daysText : Duration.m_dayText
			});
		}
		else if (this.m_hours > 0)
		{
			this.m_durationString = string.Concat(new object[]
			{
				string.Empty,
				this.m_hours,
				" ",
				Duration.m_hourText
			});
		}
		else if (this.m_minutes > 0)
		{
			this.m_durationString = string.Concat(new object[]
			{
				string.Empty,
				this.m_minutes,
				" ",
				Duration.m_minuteText
			});
		}
		else if (this.m_duration > 0)
		{
			this.m_durationString = string.Concat(new object[]
			{
				string.Empty,
				this.m_duration,
				" ",
				Duration.m_secondText
			});
		}
		else
		{
			this.m_durationString = string.Empty;
		}
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
			return this.m_duration;
		}
	}

	public string DurationString
	{
		get
		{
			return this.m_durationString;
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

	private int m_days;

	private int m_hours;

	private int m_minutes;

	private int m_remainderMinutes;

	private int m_duration;

	private string m_durationString;

	private static string m_dayText;

	private static string m_daysText;

	private static string m_hourText;

	private static string m_minuteText;

	private static string m_secondText;
}
