using System;
using Assets.SimpleAndroidNotifications;
using UnityEngine;

namespace WoWCompanionApp
{
	public class LocalNotifications
	{
		public static void RegisterForNotifications()
		{
		}

		public static void ClearPending()
		{
			NotificationManager.CancelAll();
		}

		public static void ScheduleMissionCompleteNotification(string missionName, int badgeNumber, long secondsFromNow)
		{
			if (secondsFromNow < 0L)
			{
				return;
			}
			NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds((double)secondsFromNow), StaticDB.GetString("MISSION_COMPLETE2", null), missionName, Color.black, NotificationIcon.Mission, false);
		}

		public static void ScheduleWorkOrderReadyNotification(string workOrderName, int badgeNumber, long secondsFromNow)
		{
			if (secondsFromNow < 0L)
			{
				return;
			}
			NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds((double)secondsFromNow), StaticDB.GetString("READY_FOR_PICKUP", null), workOrderName, Color.black, NotificationIcon.WorkOrder, false);
		}

		public static void ScheduleTalentResearchCompleteNotification(string talentName, int badgeNumber, long secondsFromNow)
		{
			if (secondsFromNow < 0L)
			{
				return;
			}
			NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds((double)secondsFromNow), StaticDB.GetString("RESEARCH_COMPLETE", null), talentName, Color.black, NotificationIcon.Talent, false);
		}
	}
}
