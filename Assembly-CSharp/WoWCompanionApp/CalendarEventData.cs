using System;

namespace WoWCompanionApp
{
	public class CalendarEventData
	{
		public CalendarEventData(CalendarDayEvent dayEvent, uint eventIndex)
		{
			this.EventID = dayEvent.eventID;
			this.CalendarType = dayEvent.calendarType;
			this.StartTime = dayEvent.startTime.ToDateTime();
			this.EndTime = dayEvent.endTime.ToDateTime();
			this.Title = dayEvent.title;
			this.EventIndex = eventIndex;
			this.InviteStatus = dayEvent.inviteStatus;
			this.InvitedBy = dayEvent.invitedBy;
			this.ModStatus = dayEvent.modStatus;
			this.NumSequenceDays = dayEvent.numSequenceDays;
			this.SequenceIndex = dayEvent.sequenceIndex;
			this.SequenceType = dayEvent.sequenceType;
			this.EventTime = this.StartTime.AddDays(this.SequenceIndex);
			uint? num = (dayEvent.clubID == 0UL) ? null : new uint?((uint)dayEvent.clubID);
			this.ClubID = ((num == null) ? null : new ulong?((ulong)num.Value));
			this.IsCommunityEvent = (this.ClubID != null);
		}

		public CalendarEventData(CalendarGuildEventInfo guildEvent, uint eventIndex)
		{
			this.EventID = guildEvent.eventID;
			this.CalendarType = guildEvent.calendarType;
			this.StartTime = new DateTime(guildEvent.year, (int)(guildEvent.month + 1u), (int)(guildEvent.monthDay + 1u), guildEvent.hour, guildEvent.minute, 0);
			this.Title = guildEvent.title;
			this.EventIndex = eventIndex;
			this.InviteStatus = guildEvent.inviteStatus;
			this.IsCommunityEvent = true;
			this.ClubID = new ulong?(guildEvent.clubID);
			this.NumSequenceDays = 1u;
			this.SequenceIndex = 0u;
			this.EventTime = this.StartTime;
		}

		public ulong EventID { get; private set; }

		public CalendarType CalendarType { get; private set; }

		public DateTime StartTime { get; private set; }

		public DateTime EndTime { get; private set; }

		public DateTime EventTime { get; private set; }

		public string Title { get; private set; }

		public string Description { get; private set; }

		public uint EventIndex { get; private set; }

		public uint InviteStatus { get; private set; }

		public string InvitedBy { get; private set; }

		public string ModStatus { get; private set; }

		public bool IsCommunityEvent { get; private set; }

		public ulong? ClubID { get; private set; }

		public uint NumSequenceDays { get; private set; }

		public uint SequenceIndex { get; private set; }

		public string SequenceType { get; private set; }

		public bool IsPendingInvite()
		{
			return CalendarStatusExtensions.IsInvited(new uint?(this.InviteStatus)) && this.StartTime >= DateAndTime.GetServerTimeLocal() && (this.CalendarType == CalendarType.Player || this.CalendarType == CalendarType.GuildSignup || this.CalendarType == CalendarType.CommunitySignup);
		}

		public void AddGuildInfo(CalendarGuildEventInfo guildEvent)
		{
			this.IsCommunityEvent = true;
			this.ClubID = new ulong?(guildEvent.clubID);
		}
	}
}
