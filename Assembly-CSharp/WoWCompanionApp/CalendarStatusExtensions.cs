using System;

namespace WoWCompanionApp
{
	public static class CalendarStatusExtensions
	{
		public static bool IsAttending(uint? status)
		{
			return status != null && ((CALENDAR_STATUS)status.Value).IsAttending();
		}

		public static bool IsAttending(this CALENDAR_STATUS status)
		{
			return status == CALENDAR_STATUS.CALENDAR_STATUS_CONFIRMED || status == CALENDAR_STATUS.CALENDAR_STATUS_AVAILABLE || status == CALENDAR_STATUS.CALENDAR_STATUS_SIGNEDUP;
		}

		public static bool IsInvited(uint? status)
		{
			return status != null && ((CALENDAR_STATUS)status.Value).IsInvited();
		}

		public static bool IsInvited(this CALENDAR_STATUS status)
		{
			return status == CALENDAR_STATUS.CALENDAR_STATUS_INVITED || status == CALENDAR_STATUS.CALENDAR_STATUS_NOT_SIGNEDUP;
		}

		public static bool IsDeclined(uint? status)
		{
			return status != null && ((CALENDAR_STATUS)status.Value).IsDeclined();
		}

		public static bool IsDeclined(this CALENDAR_STATUS status)
		{
			return status == CALENDAR_STATUS.CALENDAR_STATUS_DECLINED || status == CALENDAR_STATUS.CALENDAR_STATUS_NOT_SIGNEDUP;
		}

		public static bool CanRSVP(uint? status)
		{
			return status != null && ((CALENDAR_STATUS)status.Value).CanRSVP();
		}

		public static bool CanRSVP(this CALENDAR_STATUS status)
		{
			switch (status)
			{
			case CALENDAR_STATUS.CALENDAR_STATUS_INVITED:
			case CALENDAR_STATUS.CALENDAR_STATUS_AVAILABLE:
			case CALENDAR_STATUS.CALENDAR_STATUS_DECLINED:
			case CALENDAR_STATUS.CALENDAR_STATUS_SIGNEDUP:
			case CALENDAR_STATUS.CALENDAR_STATUS_NOT_SIGNEDUP:
			case CALENDAR_STATUS.CALENDAR_STATUS_TENTATIVE:
				return true;
			}
			return false;
		}
	}
}
