using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class CalendarCVar
	{
		private CalendarCVar(string name, CalendarCVar.CVarType type, bool appOnly = false)
		{
			this.Name = name;
			this.AppOnly = appOnly;
			CalendarCVar.cvarDictionary.Add(type, this);
		}

		public string Name { get; private set; }

		public bool AppOnly { get; private set; }

		public bool GetValue()
		{
			if (!this.AppOnly)
			{
				return CVarScripts.GetCVar(this.Name) == "1";
			}
			bool result = true;
			string @string = SecurePlayerPrefs.GetString(this.Name, Main.uniqueIdentifier);
			if (string.IsNullOrEmpty(@string))
			{
				return true;
			}
			bool.TryParse(@string, out result);
			return result;
		}

		public static bool GetCVarValue(CalendarCVar.CVarType cvar)
		{
			return CalendarCVar.cvarDictionary.ContainsKey(cvar) && CalendarCVar.cvarDictionary[cvar].GetValue();
		}

		public void SetValue(bool value)
		{
			if (this.AppOnly)
			{
				SecurePlayerPrefs.SetString(this.Name, value.ToString(), Main.uniqueIdentifier);
			}
			else
			{
				CVarScripts.SetCVar(this.Name, (!value) ? "0" : "1", null);
			}
		}

		public static void SetCVarValue(CalendarCVar.CVarType cvar, bool value)
		{
			if (CalendarCVar.cvarDictionary.ContainsKey(cvar))
			{
				CalendarCVar.cvarDictionary[cvar].SetValue(value);
			}
		}

		private static Dictionary<CalendarCVar.CVarType, CalendarCVar> cvarDictionary = new Dictionary<CalendarCVar.CVarType, CalendarCVar>();

		public static CalendarCVar ShowWeeklyHolidays = new CalendarCVar("calendarShowWeeklyHolidays", CalendarCVar.CVarType.WeeklyHolidays, false);

		public static CalendarCVar ShowDarkmoonFaire = new CalendarCVar("calendarShowDarkmoon", CalendarCVar.CVarType.DarkmoonFaire, false);

		public static CalendarCVar ShowBattlegrounds = new CalendarCVar("calendarShowBattlegrounds", CalendarCVar.CVarType.Battlegrounds, false);

		public static CalendarCVar ShowRaidLockouts = new CalendarCVar("calendarShowLockouts", CalendarCVar.CVarType.RaidLockouts, false);

		public static CalendarCVar ShowGuildEvents = new CalendarCVar("calendarShowGuildEvents", CalendarCVar.CVarType.GuildEvents, true);

		public static CalendarCVar ShowCommunityEvents = new CalendarCVar("calendarShowCommunityEvents", CalendarCVar.CVarType.CommunityEvents, true);

		public enum CVarType
		{
			WeeklyHolidays,
			DarkmoonFaire,
			Battlegrounds,
			RaidLockouts,
			GuildEvents,
			CommunityEvents
		}
	}
}
