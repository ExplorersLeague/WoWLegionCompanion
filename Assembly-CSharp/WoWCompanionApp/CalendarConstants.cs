using System;

namespace WoWCompanionApp
{
	public static class CalendarConstants
	{
		public const int MAX_EVENT_HISTORY_DAYS = 14;

		public const int NUM_DAYS_VIEWABLE_COMMUNITY_EVENTS = 28;

		public const int MAX_TOTAL_EVENTS = 170;

		public const int MAX_PLAYER_EVENTS_PER_PERIOD = 30;

		public const int MAX_COMMUNITY_EVENTS_PER_PERIOD = 100;

		public const int MAX_TOTAL_INVITES = 70;

		public const int MAX_EVENT_INVITES = 100;

		public const int MAX_EVENT_CREATE_MONTH_OFFSET = 12;

		public const int CALENDAR_HOLIDAY_TICK = 3600000;

		public const int NUM_CALENDAR_MONTHS = 12;

		public const int NUM_CALENDAR_WEEKDAYS = 7;

		public const int MAX_CALENDAR_DAYS_IN_MONTH = 31;

		public const int MIN_CALENDAR_YEAR = 4;

		public const int MIN_CALENDAR_MONTH = 10;

		public const int MIN_CALENDAR_DAY = 23;

		private const int WOW_TIME_YEAR_BITS = 5;

		private const int WOW_TIME_YEAR_MASK = 31;

		public const int MAX_CALENDAR_YEAR = 31;

		public const uint MAX_CALENDAR_RESTRICTED_LEVEL = 19u;

		public const int CALENDAR_EVENT_ALERT_MINUTES = 15;

		public const int CALENDAR_THROTTLE_INVITE_MS = 2000;

		public const int CALENDAR_THROTTLE_EVENT_MS = 5000;
	}
}
