using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public sealed class CalendarType : IComparable
	{
		private CalendarType(string value)
		{
			this.value = value;
			CalendarType.typeStringDict.Add(value, this);
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			CalendarType calendarType = obj as CalendarType;
			if (calendarType != null)
			{
				return this.value.CompareTo(calendarType.value);
			}
			throw new ArgumentException("CompareTo target is not CalendarTypeString");
		}

		public static implicit operator string(CalendarType calendarTypeString)
		{
			return calendarTypeString.value;
		}

		public static implicit operator CalendarType(string typeString)
		{
			if (CalendarType.typeStringDict.ContainsKey(typeString))
			{
				return CalendarType.typeStringDict[typeString];
			}
			throw new ArgumentException("Invalid calendar type string");
		}

		private string value;

		private static Dictionary<string, CalendarType> typeStringDict = new Dictionary<string, CalendarType>();

		public static CalendarType Player = new CalendarType("PLAYER");

		public static CalendarType CommunityAnnouncement = new CalendarType("GUILD_ANNOUNCEMENT");

		public static CalendarType GuildSignup = new CalendarType("GUILD_EVENT");

		public static CalendarType CommunitySignup = new CalendarType("COMMUNITY_EVENT");

		public static CalendarType System = new CalendarType("SYSTEM");

		public static CalendarType Holiday = new CalendarType("HOLIDAY");

		public static CalendarType RaidLockout = new CalendarType("RAID_LOCKOUT");

		public static CalendarType RaidResetDeprecated = new CalendarType("RAID_RESET");
	}
}
