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
			NotificationManager.SendCustom(new NotificationParams
			{
				Delay = TimeSpan.FromSeconds((double)secondsFromNow),
				Title = StaticDB.GetString("MISSION_COMPLETE2", null),
				Message = missionName,
				SmallIconColor = Color.black,
				SmallIcon = NotificationIcon.Mission,
				Sound = true,
				CustomSound = "ui_mission_complete_toast_n"
			});
		}

		public static void ScheduleWorkOrderReadyNotification(string workOrderName, int badgeNumber, long secondsFromNow)
		{
			if (secondsFromNow < 0L)
			{
				return;
			}
			NotificationManager.SendCustom(new NotificationParams
			{
				Delay = TimeSpan.FromSeconds((double)secondsFromNow),
				Title = StaticDB.GetString("READY_FOR_PICKUP", null),
				Message = workOrderName,
				SmallIconColor = Color.black,
				SmallIcon = NotificationIcon.WorkOrder,
				Sound = true,
				CustomSound = "ui_mission_troops_ready_toast_n"
			});
		}

		public static void ScheduleTalentResearchCompleteNotification(string talentName, int badgeNumber, long secondsFromNow)
		{
			if (secondsFromNow < 0L)
			{
				return;
			}
			NotificationManager.SendCustom(new NotificationParams
			{
				Delay = TimeSpan.FromSeconds((double)secondsFromNow),
				Title = StaticDB.GetString("RESEARCH_COMPLETE", null),
				Message = talentName,
				SmallIconColor = Color.black,
				SmallIcon = NotificationIcon.Talent,
				Sound = true,
				CustomSound = "ui_orderhall_talent_ready_toast_n"
			});
		}
	}
}
