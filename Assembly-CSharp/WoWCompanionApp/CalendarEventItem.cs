using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CalendarEventItem : MonoBehaviour
	{
		public uint EventIndex
		{
			get
			{
				return this.EventData.EventIndex;
			}
		}

		public CalendarEventData EventData { get; private set; }

		public void SetDateTime(DateTime dateTime)
		{
		}

		public void SetEventInfo(CalendarEventData eventData)
		{
			this.EventData = eventData;
			this.m_date.text = this.GetDateString(eventData.EventTime);
			if (this.m_time != null)
			{
				this.m_time.text = eventData.EventTime.ToString(StaticDB.GetString("SHORT_TIME", "h:mm tt"));
			}
			this.m_eventName.text = eventData.Title;
			if (this.m_subtext != null)
			{
				TextCycler component = this.m_subtext.GetComponent<TextCycler>();
				Community community = CommunityData.Instance.GetCommunity(this.EventData.ClubID);
				if (this.EventData.CalendarType == CalendarType.CommunityAnnouncement)
				{
					if (community != null)
					{
						component.AddString(community.Name);
					}
				}
				else if (CalendarStatusExtensions.IsAttending(new uint?(this.EventData.InviteStatus)))
				{
					component.AddString(StaticDB.GetString("YOU_ARE_GOING", "You are going [PH]"));
					if (community != null)
					{
						component.AddString(community.Name);
					}
					this.UpdateNumAttendees();
				}
				else if (this.EventData.InviteStatus == 8u)
				{
					component.AddString(StaticDB.GetString("TENTATIVE", "Tentative [PH]"));
					if (community != null)
					{
						component.AddString(community.Name);
					}
				}
				else if (CalendarStatusExtensions.IsInvited(new uint?(this.EventData.InviteStatus)))
				{
					if (!string.IsNullOrEmpty(this.EventData.InvitedBy))
					{
						component.AddString(string.Format(StaticDB.GetString("INVITED_BY", "Invited by {0} [PH]"), this.EventData.InvitedBy));
					}
					else
					{
						component.AddString(StaticDB.GetString("REPLY_TO_EVENT", "Reply to this event [PH]"));
					}
					if (community != null)
					{
						component.AddString(community.Name);
					}
				}
				else if (this.EventData.InviteStatus == 2u)
				{
					component.AddString(StaticDB.GetString("DECLINED", "You are not going [PH]"));
				}
				else
				{
					this.m_subtext.gameObject.SetActive(false);
				}
			}
			if (this.m_inviteStatus != null)
			{
				if (CalendarStatusExtensions.IsAttending(new uint?(this.EventData.InviteStatus)))
				{
					this.m_inviteStatus.sprite = this.m_checkMark;
				}
				else if (this.EventData.InviteStatus == 8u)
				{
					this.m_inviteStatus.sprite = this.m_questionMark;
				}
				else if (CalendarStatusExtensions.IsInvited(new uint?(this.EventData.InviteStatus)))
				{
					this.m_inviteStatus.sprite = this.m_exclamationMark;
				}
				else if (this.EventData.InviteStatus == 2u)
				{
					this.m_inviteStatus.sprite = this.m_xMark;
				}
				else
				{
					this.m_inviteStatus.gameObject.SetActive(false);
				}
			}
		}

		public void OpenEventItem()
		{
			if (this.EventData.CalendarType == CalendarType.Holiday)
			{
				Singleton<DialogFactory>.Instance.CreateHolidayDetailsPanel(this.EventData);
			}
			else
			{
				EventInviteResponseDialog dialog = Singleton<DialogFactory>.Instance.CreateEventInviteResponseDialog(this);
				base.GetComponentInParent<EventsListPanel>().OnEventSelected(dialog);
			}
		}

		private string GetDateString(DateTime dateTime)
		{
			if (dateTime.DayOfYear == DateTime.Now.DayOfYear)
			{
				return StaticDB.GetString("TODAY_CAPS", "Today [PH]");
			}
			if (dateTime.DayOfYear == DateTime.Now.DayOfYear + 1 || dateTime.DayOfYear == 1)
			{
				return StaticDB.GetString("TOMORROW_CAPS", "Tomorrow [PH]");
			}
			return dateTime.ToString(StaticDB.GetString("SHORT_MONTH_AND_DAY", "MMM d"));
		}

		private void SetNumAttendees(uint numAttendees)
		{
			this.m_numAttendees = new uint?(numAttendees);
			TextCycler textCycler = (!(this.m_subtext != null)) ? null : this.m_subtext.GetComponent<TextCycler>();
			if (textCycler != null && CalendarStatusExtensions.IsAttending(new uint?(this.EventData.InviteStatus)) && this.m_numAttendees.Value > 1u)
			{
				textCycler.ClearStrings();
				TextCycler textCycler2 = textCycler;
				string @string = StaticDB.GetString("YOU_PLUS_PEOPLE_ARE_GOING", "You +{0} are going. [PH]");
				uint? numAttendees2 = this.m_numAttendees;
				textCycler2.AddString(string.Format(@string, (numAttendees2 == null) ? null : new uint?(numAttendees2.GetValueOrDefault() - 1u)));
				Community community = CommunityData.Instance.GetCommunity(this.EventData.ClubID);
				if (community != null)
				{
					textCycler.AddString(community.Name);
				}
			}
		}

		private void OnOpenEvent(Calendar.CalendarOpenEventEvent eventArgs)
		{
			Calendar.OnCalendarOpenEvent -= new Calendar.CalendarOpenEventHandler(this.OnOpenEvent);
			List<CalendarEventInviteInfo> list = new List<CalendarEventInviteInfo>();
			for (uint num = 0u; num < Calendar.GetNumInvites(); num += 1u)
			{
				CalendarEventInviteInfo item;
				if (Calendar.EventGetInvite(num, ref item))
				{
					list.Add(item);
				}
			}
			this.SetNumAttendees((uint)list.Count((CalendarEventInviteInfo invite) => CalendarStatusExtensions.IsAttending(invite.inviteStatus)));
			Calendar.CloseEvent();
		}

		private void Update()
		{
			RectTransform rectTransform = base.GetComponentInChildren<TiledRandomTexture>().transform as RectTransform;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
		}

		private void UpdateNumAttendees()
		{
			if (!CalendarStatusExtensions.IsAttending(new uint?(this.EventData.InviteStatus)))
			{
				return;
			}
			base.StartCoroutine(this.UpdateNumAttendeesCoroutine());
		}

		private IEnumerator UpdateNumAttendeesCoroutine()
		{
			while (Calendar.IsActionPending())
			{
				yield return null;
			}
			this.OpenEvent(new Calendar.CalendarOpenEventHandler(this.OnOpenEvent));
			yield break;
		}

		public bool OpenEvent(Calendar.CalendarOpenEventHandler callback)
		{
			Calendar.OnCalendarOpenEvent += callback;
			if (!Calendar.OpenEvent(this.EventData.StartTime.Month - DateAndTime.GetServerTimeLocal().Month, (uint)(this.EventData.StartTime.Day - 1), this.EventIndex))
			{
				Calendar.OnCalendarOpenEvent -= callback;
				return false;
			}
			return true;
		}

		public Text m_date;

		public Text m_time;

		public Text m_eventName;

		public Text m_subtext;

		public Image m_inviteStatus;

		public Sprite m_checkMark;

		public Sprite m_questionMark;

		public Sprite m_exclamationMark;

		public Sprite m_xMark;

		private uint? m_numAttendees;
	}
}
