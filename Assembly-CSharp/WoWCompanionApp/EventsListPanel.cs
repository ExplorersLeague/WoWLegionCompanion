using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class EventsListPanel : MonoBehaviour
	{
		private void Awake()
		{
			CommunityData.Instance.RefreshCommunities();
			Calendar.OpenCalendar();
		}

		private void OnEnable()
		{
			Calendar.OnCalendarUpdateGuildEvents += new Calendar.CalendarUpdateGuildEventsHandler(this.UpdateHandler<Calendar.CalendarUpdateGuildEventsEvent>);
			Calendar.OnCalendarUpdateEventList += new Calendar.CalendarUpdateEventListHandler(this.UpdateHandler<Calendar.CalendarUpdateEventListEvent>);
			Calendar.OnCalendarUpdateInviteList += new Calendar.CalendarUpdateInviteListHandler(this.UpdateHandler<Calendar.CalendarUpdateInviteListEvent>);
		}

		private void OnDisable()
		{
			Calendar.OnCalendarUpdateGuildEvents -= new Calendar.CalendarUpdateGuildEventsHandler(this.UpdateHandler<Calendar.CalendarUpdateGuildEventsEvent>);
			Calendar.OnCalendarUpdateEventList -= new Calendar.CalendarUpdateEventListHandler(this.UpdateHandler<Calendar.CalendarUpdateEventListEvent>);
			Calendar.OnCalendarUpdateInviteList -= new Calendar.CalendarUpdateInviteListHandler(this.UpdateHandler<Calendar.CalendarUpdateInviteListEvent>);
		}

		private void UpdateHandler<T>(T eventArgs)
		{
			this.m_refreshQueued = true;
		}

		private void Update()
		{
			if (this.m_refreshQueued)
			{
				this.RefreshPanelContent();
				this.m_refreshQueued = false;
			}
		}

		public void RefreshPanelContent()
		{
			this.m_scrollContent.DetachAllChildren();
			base.StopAllCoroutines();
			base.StartCoroutine(this.AddEventsToContent());
		}

		private IEnumerator AddEventsToContent()
		{
			CultureInfo cultureInfo = MobileDeviceLocale.GetCultureInfoLocale();
			CultureInfo currentCultureInfo = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			int numPendingInvites = 0;
			this.m_dateHeaders.Clear();
			uint numGuildEvents = Calendar.GetNumGuildEvents();
			List<CalendarGuildEventInfo> guildEvents = new List<CalendarGuildEventInfo>();
			for (uint num = 0u; num < numGuildEvents; num += 1u)
			{
				CalendarGuildEventInfo item;
				if (Calendar.GetGuildEventInfo(num, ref item))
				{
					guildEvents.Add(item);
				}
			}
			CalendarEventItem forceClosedEventItem = null;
			DateTime eventDate = DateTime.Now;
			int eventCount = 0;
			for (int offsetMonth = 0; offsetMonth <= 1; offsetMonth++)
			{
				int year = DateTime.Now.Year + ((DateTime.Now.Month != 12 || offsetMonth != 1) ? 0 : 1);
				int month = DateTime.Now.Month + offsetMonth;
				if (month > 12)
				{
					month -= 12;
				}
				int daysInMonth = DateTime.DaysInMonth(year, month);
				uint day = (uint)((offsetMonth != 0) ? 0 : (DateTime.Now.Day - 1));
				while ((ulong)day < (ulong)((long)daysInMonth) && (eventDate - DateTime.Now).TotalDays < 15.0)
				{
					int currentEventCount = eventCount;
					uint numDayEvents = Calendar.GetNumDayEvents(offsetMonth, day);
					if (numDayEvents > 0u || guildEvents.Any((CalendarGuildEventInfo guildEvent) => this.$this.IsEventOnDay(guildEvent, eventDate)))
					{
						GameObject gameObject = this.m_scrollContent.AddAsChildObject(this.m_scrollSectionHeaderPrefab);
						gameObject.GetComponentInChildren<Text>().text = eventDate.ToString(StaticDB.GetString("FULL_DATE", "dddd, MMMM d, yyyy"));
						this.m_dateHeaders.Add(eventDate, gameObject);
						List<CalendarEventData> list = new List<CalendarEventData>();
						for (uint num2 = 0u; num2 < numDayEvents; num2 += 1u)
						{
							CalendarDayEvent dayEvent;
							if (Calendar.GetDayEvent(offsetMonth, day, num2, ref dayEvent))
							{
								CalendarEventData calendarEventData = new CalendarEventData(dayEvent, num2);
								list.Add(calendarEventData);
								if (calendarEventData.IsPendingInvite())
								{
									numPendingInvites++;
								}
							}
						}
						uint num3 = 0u;
						while ((ulong)num3 < (ulong)((long)guildEvents.Count))
						{
							CalendarGuildEventInfo guildEvent = guildEvents[(int)num3];
							if (this.IsEventOnDay(guildEvent, eventDate))
							{
								CalendarEventData calendarEventData2 = list.FirstOrDefault((CalendarEventData eventData) => eventData.EventID == guildEvent.eventID);
								if (calendarEventData2 == null)
								{
									list.Add(new CalendarEventData(guildEvent, num3));
								}
								else
								{
									calendarEventData2.AddGuildInfo(guildEvent);
								}
							}
							num3 += 1u;
						}
						list.Sort(EventsListPanel.Sorter);
						bool flag = false;
						foreach (CalendarEventData calendarEventData3 in list)
						{
							GameObject gameObject2;
							if (calendarEventData3.CalendarType == CalendarType.Holiday)
							{
								gameObject2 = this.m_scrollContent.AddAsChildObject(this.m_holidayListItemPrefab);
							}
							else
							{
								gameObject2 = this.m_scrollContent.AddAsChildObject(this.m_eventListItemPrefab);
							}
							if (gameObject2 != null)
							{
								gameObject2.GetComponentInChildren<CalendarEventItem>().SetEventInfo(calendarEventData3);
								bool flag2 = !this.m_isOnlyShowingPendingInvites || calendarEventData3.IsPendingInvite();
								if (calendarEventData3.IsCommunityEvent && calendarEventData3.ClubID != null)
								{
									ulong? guildID = CommunityData.Instance.GetGuildID();
									if (guildID != null && guildID.Value == calendarEventData3.ClubID.Value)
									{
										flag2 &= CalendarCVar.ShowGuildEvents.GetValue();
									}
									else
									{
										flag2 &= CalendarCVar.ShowCommunityEvents.GetValue();
									}
								}
								gameObject2.gameObject.SetActive(flag2);
								flag = (flag || flag2);
								if (calendarEventData3.EventID == EventInviteResponseDialog.ForceClosedEventID)
								{
									forceClosedEventItem = gameObject2.GetComponentInChildren<CalendarEventItem>();
								}
								if (flag)
								{
									eventCount++;
								}
							}
						}
						gameObject.gameObject.SetActive(flag);
					}
					eventDate = eventDate.AddDays(1.0);
					if (eventCount > currentEventCount)
					{
						yield return null;
					}
					day += 1u;
				}
			}
			if (numPendingInvites > 0)
			{
				GameObject gameObject3 = this.m_scrollContent.AddAsChildObject(this.m_scrollSectionHeaderPrefab);
				gameObject3.GetComponentInChildren<Text>().text = StaticDB.GetString("CALENDAR_PENDING_INVITATIONS_HEADER", "Pending Invitations [PH]");
				gameObject3.transform.SetSiblingIndex(0);
				this.m_pendingInvitesButton = this.m_scrollContent.AddAsChildObject(this.m_pendingInviteButtonPrefab);
				this.m_pendingInvitesButton.GetComponentInChildren<Button>().onClick.AddListener(new UnityAction(this.TogglePendingInvitesFilter));
				this.m_pendingInvitesButton.transform.SetSiblingIndex(1);
				Text componentInChildren = this.m_pendingInvitesButton.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					componentInChildren.text = StaticDB.GetString((!this.m_isOnlyShowingPendingInvites) ? "CALENDAR_PENDING_INVITATIONS" : "BACK", (!this.m_isOnlyShowingPendingInvites) ? "Invitations [PH]" : "Back [PH]");
				}
			}
			else if (this.m_isOnlyShowingPendingInvites)
			{
				this.TogglePendingInvitesFilter();
			}
			Thread.CurrentThread.CurrentCulture = currentCultureInfo;
			if (forceClosedEventItem != null)
			{
				forceClosedEventItem.OpenEventItem();
			}
			else if (EventInviteResponseDialog.ForceClosedEventID != null)
			{
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("EVENT_NOT_AVAILABLE", "Event is no longer available [PH]"));
			}
			yield break;
		}

		public void OnEventSelected(BaseDialog dialog)
		{
			this.m_gears.gameObject.SetActive(true);
			this.m_gears.transform.SetParent(dialog.transform.parent, false);
			this.m_gears.transform.SetAsLastSibling();
			Calendar.OnCalendarOpenEvent += new Calendar.CalendarOpenEventHandler(this.OnEventDetailsOpened);
		}

		public void OnEventDetailsOpened(Calendar.CalendarOpenEventEvent eventArgs)
		{
			this.m_gears.gameObject.SetActive(false);
			this.m_gears.transform.SetParent(base.gameObject.transform.parent, false);
			Calendar.OnCalendarOpenEvent -= new Calendar.CalendarOpenEventHandler(this.OnEventDetailsOpened);
		}

		public void OnEventOpenFailed()
		{
			this.m_gears.gameObject.SetActive(false);
			this.m_gears.transform.SetParent(base.gameObject.transform.parent, false);
			Calendar.OnCalendarOpenEvent -= new Calendar.CalendarOpenEventHandler(this.OnEventDetailsOpened);
			AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("EVENT_NOT_AVAILABLE", "Event is no longer available [PH]"));
		}

		public void ShowCalendarFiltersDialog()
		{
			Singleton<DialogFactory>.Instance.CreateCalendarFiltersDialog(this);
		}

		private void TogglePendingInvitesFilter()
		{
			this.m_isOnlyShowingPendingInvites = !this.m_isOnlyShowingPendingInvites;
			CalendarEventItem[] componentsInChildren = this.m_scrollContent.GetComponentsInChildren<CalendarEventItem>(true);
			foreach (CalendarEventItem calendarEventItem in componentsInChildren)
			{
				calendarEventItem.gameObject.SetActive(!this.m_isOnlyShowingPendingInvites || calendarEventItem.EventData.IsPendingInvite());
			}
			using (Dictionary<DateTime, GameObject>.KeyCollection.Enumerator enumerator = this.m_dateHeaders.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DateTime date = enumerator.Current;
					EventsListPanel $this = this;
					this.m_dateHeaders[date].gameObject.SetActive(componentsInChildren.Any((CalendarEventItem eventItem) => eventItem.gameObject.activeInHierarchy && $this.IsEventOnDay(eventItem.EventData, date)));
				}
			}
			if (this.m_pendingInvitesButton != null)
			{
				Text componentInChildren = this.m_pendingInvitesButton.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					componentInChildren.text = StaticDB.GetString((!this.m_isOnlyShowingPendingInvites) ? "CALENDAR_PENDING_INVITATIONS" : "BACK", (!this.m_isOnlyShowingPendingInvites) ? "Invitations [PH]" : "Back [PH]");
				}
			}
		}

		private bool IsEventOnDay(CalendarDayEvent dayEvent, DateTime dateTime)
		{
			DateTime dateTime2 = dayEvent.startTime.ToDateTime();
			return dateTime2.Year == dateTime.Year && (long)dateTime2.DayOfYear + (long)((ulong)dayEvent.sequenceIndex) == (long)dateTime.DayOfYear;
		}

		private bool IsEventOnDay(CalendarGuildEventInfo guildEvent, DateTime dateTime)
		{
			return guildEvent.year == dateTime.Year && (ulong)guildEvent.month == (ulong)((long)(dateTime.Month - 1)) && (ulong)guildEvent.monthDay == (ulong)((long)(dateTime.Day - 1));
		}

		private bool IsEventOnDay(CalendarEventData eventData, DateTime dateTime)
		{
			return eventData.EventTime.Year == dateTime.Year && eventData.EventTime.DayOfYear == dateTime.DayOfYear;
		}

		public GameObject m_scrollContent;

		public GameObject m_eventListItemPrefab;

		public GameObject m_holidayListItemPrefab;

		public GameObject m_scrollSectionHeaderPrefab;

		public GameObject m_pendingInviteButtonPrefab;

		public GameObject m_gears;

		public GameObject m_pendingInvitesButton;

		private bool m_refreshQueued;

		private bool m_isOnlyShowingPendingInvites;

		private Dictionary<DateTime, GameObject> m_dateHeaders = new Dictionary<DateTime, GameObject>();

		private const int MaxNumDaysToShow = 15;

		private static List<CalendarType> CalendarTypePriorities = new List<CalendarType>
		{
			CalendarType.System,
			CalendarType.Holiday,
			CalendarType.Player,
			CalendarType.GuildSignup,
			CalendarType.CommunityAnnouncement,
			CalendarType.CommunitySignup,
			CalendarType.RaidLockout,
			CalendarType.RaidResetDeprecated
		};

		private static PriorityComparer<CalendarEventData> Sorter = new PriorityComparer<CalendarEventData>(new PriorityComparer<CalendarEventData>.SortFunction[]
		{
			(CalendarEventData event1, CalendarEventData event2) => -1 * (event1.CalendarType == CalendarType.Holiday).CompareTo(event2.CalendarType == CalendarType.Holiday),
			(CalendarEventData event1, CalendarEventData event2) => event1.EventTime.CompareTo(event2.EventTime),
			(CalendarEventData event1, CalendarEventData event2) => EventsListPanel.CalendarTypePriorities.IndexOf(event1.CalendarType).CompareTo(EventsListPanel.CalendarTypePriorities.IndexOf(event2.CalendarType)),
			(CalendarEventData event1, CalendarEventData event2) => event1.Title.CompareTo(event2.Title)
		});
	}
}
