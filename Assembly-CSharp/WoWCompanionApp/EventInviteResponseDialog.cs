using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class EventInviteResponseDialog : BaseDialog
	{
		public static ulong? ForceClosedEventID { get; private set; }

		public bool IsForceClosed { get; set; }

		public CalendarEventItem CalendarEventItem { get; private set; }

		private IEnumerator Start()
		{
			base.GetComponent<CanvasGroup>().alpha = 0f;
			foreach (Selectable selectable in base.GetComponentsInChildren<Selectable>())
			{
				selectable.interactable = false;
			}
			this.m_addInviteButton.gameObject.SetActive(false);
			while (Calendar.IsActionPending())
			{
				yield return null;
			}
			while (this.CalendarEventItem == null)
			{
				yield return null;
			}
			if (!this.CalendarEventItem.OpenEvent(new Calendar.CalendarOpenEventHandler(this.OnOpenEvent)))
			{
				EventsListPanel componentInParent = this.CalendarEventItem.gameObject.GetComponentInParent<EventsListPanel>();
				if (componentInParent != null)
				{
					componentInParent.OnEventOpenFailed();
				}
				this.CloseDialog();
			}
			this.IsForceClosed = true;
			SceneManager.activeSceneChanged += delegate(Scene scene, Scene sceneLoadMode)
			{
				EventInviteResponseDialog.ForceClosedEventID = null;
			};
			yield break;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Calendar.OnCalendarUpdateInviteList += new Calendar.CalendarUpdateInviteListHandler(this.OnUpdateInviteList);
			Calendar.OnCalendarCloseEvent += new Calendar.CalendarCloseEventHandler(this.OnCloseEvent);
			EventInviteResponseDialog.ForceClosedEventID = null;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Calendar.OnCalendarUpdateInviteList -= new Calendar.CalendarUpdateInviteListHandler(this.OnUpdateInviteList);
			Calendar.OnCalendarCloseEvent -= new Calendar.CalendarCloseEventHandler(this.OnCloseEvent);
			base.StopAllCoroutines();
		}

		private void Update()
		{
		}

		public void SetCalendarEvent(CalendarEventItem eventItem)
		{
			this.CalendarEventItem = eventItem;
		}

		public void OnOpenEvent(Calendar.CalendarOpenEventEvent eventArgs)
		{
			Calendar.OnCalendarOpenEvent -= new Calendar.CalendarOpenEventHandler(this.OnOpenEvent);
			base.GetComponent<CanvasGroup>().alpha = 1f;
			foreach (Selectable selectable in base.GetComponentsInChildren<Selectable>())
			{
				selectable.interactable = true;
			}
			CalendarEventInfo calendarEventInfo;
			if (!Calendar.GetEventInfo(ref calendarEventInfo))
			{
				this.CloseDialog();
				return;
			}
			this.m_eventTitle.text = calendarEventInfo.title;
			this.m_eventDescription.text = calendarEventInfo.description;
			this.m_eventDateTime.text = calendarEventInfo.time.ToDateTime().ToString(StaticDB.GetString("EVENT_DATE_AND_TIME", "M/d/yy h:mm tt"));
			if (this.CalendarEventItem.EventData.IsCommunityEvent)
			{
				Community community = CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID);
				this.m_communityName.text = ((community == null) ? string.Empty : community.Name);
			}
			else
			{
				this.m_communityName.text = string.Empty;
			}
			foreach (Button button in base.GetComponentsInChildren<Button>())
			{
				button.interactable = true;
			}
			bool flag = calendarEventInfo.time.ToDateTime() >= DateAndTime.GetServerTimeLocal();
			this.m_addInviteButton.interactable = flag;
			this.m_addInviteButton.gameObject.SetActive(this.CalendarEventItem.EventData.ModStatus == "CREATOR" || this.CalendarEventItem.EventData.ModStatus == "MODERATOR");
			if (!flag || calendarEventInfo.isLocked)
			{
				this.m_acceptButton.interactable = false;
				this.m_tentativeButton.interactable = false;
				this.m_declineButton.interactable = false;
			}
			else if (this.CalendarEventItem.EventData.IsCommunityEvent)
			{
				if (this.CalendarEventItem.EventData.ModStatus == "CREATOR")
				{
					this.m_acceptButton.interactable = false;
					this.m_tentativeButton.interactable = false;
					this.m_declineButton.interactable = false;
				}
				else
				{
					bool flag2 = (CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID) == null) ? (calendarEventInfo.inviteStatus == 1u) : (calendarEventInfo.inviteStatus == 6u || calendarEventInfo.inviteStatus == 8u);
					bool flag3 = (CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID) == null) ? (calendarEventInfo.inviteStatus == 8u) : (calendarEventInfo.inviteStatus == 6u || calendarEventInfo.inviteStatus == 8u);
					bool flag4 = (CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID) == null) ? (calendarEventInfo.inviteStatus == 2u) : (calendarEventInfo.inviteStatus == 7u);
					this.m_acceptButton.interactable = !flag2;
					this.m_tentativeButton.interactable = !flag3;
					this.m_declineButton.interactable = !flag4;
				}
			}
			else if (!CalendarStatusExtensions.CanRSVP(calendarEventInfo.inviteStatus))
			{
				this.m_acceptButton.interactable = false;
				this.m_tentativeButton.interactable = false;
				this.m_declineButton.interactable = false;
			}
			else
			{
				this.m_acceptButton.interactable = (calendarEventInfo.inviteStatus != 1u);
				this.m_tentativeButton.interactable = (calendarEventInfo.inviteStatus != 8u);
				this.m_declineButton.interactable = (calendarEventInfo.inviteStatus != 2u);
			}
			if (this.CalendarEventItem.EventData.IsCommunityEvent && CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID) != null)
			{
				this.m_acceptButton.GetComponentInChildren<Text>().text = StaticDB.GetString("EVENT_SIGN_UP", "SIGN UP [PH]");
				this.m_declineButton.GetComponentInChildren<Text>().text = StaticDB.GetString("EVENT_REMOVE_SIGN_UP", "REMOVE SIGN UP [PH]");
			}
			this.UpdateInvitesList();
		}

		private IEnumerator GetInvitesCoroutine()
		{
			this.m_addInviteButton.GetComponent<Button>().interactable = false;
			bool acceptInteractable = this.m_acceptButton.interactable;
			bool tentativeInteractable = this.m_tentativeButton.interactable;
			bool declineInteractable = this.m_declineButton.interactable;
			this.m_acceptButton.interactable = false;
			this.m_tentativeButton.interactable = false;
			this.m_declineButton.interactable = false;
			while (!Calendar.AreNamesReady() || Calendar.IsActionPending())
			{
				yield return null;
			}
			this.m_invites.Clear();
			for (uint num = 0u; num < Calendar.GetNumInvites(); num += 1u)
			{
				CalendarEventInviteInfo key2;
				if (Calendar.EventGetInvite(num, ref key2))
				{
					this.m_invites.Add(key2, num);
				}
			}
			IOrderedEnumerable<CalendarEventInviteInfo> invites = this.m_invites.Keys.OrderBy((CalendarEventInviteInfo key) => key, EventInviteResponseDialog.Sorter);
			foreach (EventRosterPage eventRosterPage in this.m_inviteListScrollArea.GetComponentsInChildren<EventRosterPage>())
			{
				Object.Destroy(eventRosterPage.gameObject);
			}
			while (base.GetComponentsInChildren<EventRosterPage>().Length > 0)
			{
				yield return null;
			}
			EventRosterPage currentMemberPage = this.CreateRosterPage();
			foreach (CalendarEventInviteInfo invite in invites)
			{
				if (currentMemberPage.AtCapacity())
				{
					currentMemberPage = this.CreateRosterPage();
				}
				currentMemberPage.AddMemberToRoster(invite);
				yield return null;
			}
			this.m_inviteListScrollArea.GetComponentInParent<AutoCenterScrollRect>().CenterOnItem(0);
			bool isTodayOrLater = this.CalendarEventItem.EventData.EventTime >= DateAndTime.GetServerTimeLocal();
			this.m_addInviteButton.GetComponent<Button>().interactable = isTodayOrLater;
			this.m_acceptButton.interactable = acceptInteractable;
			this.m_tentativeButton.interactable = tentativeInteractable;
			this.m_declineButton.interactable = declineInteractable;
			yield break;
		}

		private EventRosterPage CreateRosterPage()
		{
			EventRosterPage eventRosterPage = Object.Instantiate<EventRosterPage>(this.m_rosterPagePrefab, this.m_inviteListScrollArea.transform, false);
			eventRosterPage.transform.SetAsLastSibling();
			eventRosterPage.transform.localScale = Vector3.one;
			return eventRosterPage;
		}

		public override void CloseDialog()
		{
			Calendar.CloseEvent();
			base.CloseDialog();
			EventInviteResponseDialog.ForceClosedEventID = null;
		}

		public void OnAccept()
		{
			if (this.CalendarEventItem.EventData.IsCommunityEvent && CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID) != null)
			{
				Calendar.EventSignUp();
			}
			else
			{
				Calendar.EventAvailable();
			}
			this.IsForceClosed = false;
			base.StartCoroutine(this.UpdateEventAndCloseDialogCoroutine());
		}

		public void OnTentative()
		{
			Calendar.EventTentative();
			this.IsForceClosed = false;
			base.StartCoroutine(this.UpdateEventAndCloseDialogCoroutine());
		}

		public void OnDecline()
		{
			if (this.CalendarEventItem.EventData.IsCommunityEvent && CommunityData.Instance.GetCommunity(this.CalendarEventItem.EventData.ClubID) != null)
			{
				Calendar.RemoveEvent();
			}
			else
			{
				Calendar.EventDecline();
			}
			this.IsForceClosed = false;
			base.StartCoroutine(this.UpdateEventAndCloseDialogCoroutine());
		}

		private IEnumerator UpdateEventAndCloseDialogCoroutine()
		{
			while (Calendar.IsActionPending())
			{
				yield return null;
			}
			this.CloseDialog();
			yield break;
		}

		private void OnUpdateInviteList(Calendar.CalendarUpdateInviteListEvent eventAgs)
		{
			this.UpdateInvitesList();
		}

		public void UpdateInvitesList()
		{
			base.StartCoroutine(this.GetInvitesCoroutine());
		}

		public void OpenAddInviteDialog()
		{
			AddInviteDialog addInviteDialog = Singleton<DialogFactory>.Instance.CreateAddInviteDialog(this);
		}

		private void OnCloseEvent(Calendar.CalendarCloseEventEvent eventArgs)
		{
			this.CloseDialog();
			if (this.IsForceClosed)
			{
				EventInviteResponseDialog.ForceClosedEventID = new ulong?(this.CalendarEventItem.EventData.EventID);
			}
		}

		public Text m_eventTitle;

		public Text m_eventDescription;

		public Text m_eventDateTime;

		public Text m_communityName;

		public GameObject m_inviteListScrollArea;

		public Button m_addInviteButton;

		public GameObject m_rsvpButtonArea;

		public Button m_acceptButton;

		public Button m_tentativeButton;

		public Button m_declineButton;

		public EventRosterPage m_rosterPagePrefab;

		private static List<uint?> InviteStatusPriorities = new List<uint?>
		{
			new uint?(3u),
			new uint?(1u),
			new uint?(8u),
			new uint?(0u),
			new uint?(2u),
			null
		};

		private static PriorityComparer<CalendarEventInviteInfo> Sorter = new PriorityComparer<CalendarEventInviteInfo>(new PriorityComparer<CalendarEventInviteInfo>.SortFunction[]
		{
			(CalendarEventInviteInfo invite1, CalendarEventInviteInfo invite2) => -1 * invite1.inviteIsMine.CompareTo(invite2.inviteIsMine),
			(CalendarEventInviteInfo invite1, CalendarEventInviteInfo invite2) => (!(invite1.modStatus == invite2.modStatus)) ? ((!(invite1.modStatus == "CREATOR")) ? ((!(invite2.modStatus == "CREATOR")) ? 0 : 1) : -1) : 0,
			(CalendarEventInviteInfo invite1, CalendarEventInviteInfo invite2) => (!(invite1.modStatus == invite2.modStatus)) ? ((!(invite1.modStatus == "MODERATOR")) ? ((!(invite2.modStatus == "MODERATOR")) ? 0 : 1) : -1) : 0,
			(CalendarEventInviteInfo invite1, CalendarEventInviteInfo invite2) => EventInviteResponseDialog.InviteStatusPriorities.IndexOf(invite1.inviteStatus).CompareTo(EventInviteResponseDialog.InviteStatusPriorities.IndexOf(invite2.inviteStatus)),
			(CalendarEventInviteInfo invite1, CalendarEventInviteInfo invite2) => string.Compare(invite1.name, invite2.name)
		});

		private Dictionary<CalendarEventInviteInfo, uint> m_invites = new Dictionary<CalendarEventInviteInfo, uint>();
	}
}
