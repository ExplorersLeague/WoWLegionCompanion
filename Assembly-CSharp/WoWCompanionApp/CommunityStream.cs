using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WoWCompanionApp
{
	public class CommunityStream
	{
		public CommunityStream(ulong clubId, ClubStreamInfo streamInfo)
		{
			this.m_clubId = clubId;
			this.m_streamInfo = streamInfo;
		}

		public ulong StreamId
		{
			get
			{
				return this.m_streamInfo.streamId;
			}
		}

		public string Name
		{
			get
			{
				return this.m_streamInfo.name;
			}
		}

		public string Subject
		{
			get
			{
				return this.m_streamInfo.subject;
			}
		}

		public bool ForLeadersAndModerators
		{
			get
			{
				return this.m_streamInfo.leadersAndModeratorsOnly;
			}
		}

		public ClubStreamNotificationSetting Filter
		{
			set
			{
				this.m_notificationSetting = value;
			}
		}

		public void EditStream(string name, string subject, bool? modsOnly)
		{
			Club.EditStream(this.m_clubId, this.StreamId, name, subject, modsOnly);
		}

		public ReadOnlyCollection<CommunityChatMessage> GetMessages()
		{
			return this.m_messages.AsReadOnly();
		}

		public void SetNotificationFilter(ClubStreamNotificationFilter filter)
		{
			this.m_notificationSetting.filter = filter;
			List<ClubStreamNotificationSetting> list = new List<ClubStreamNotificationSetting>();
			list.Add(this.m_notificationSetting);
			Club.SetClubStreamNotificationSettings(this.m_clubId, list);
		}

		public bool ShouldReceiveNotifications()
		{
			return this.m_notificationSetting.filter == 2;
		}

		public void AddMessage(string message)
		{
			Club.SendMessage(this.m_clubId, this.m_streamInfo.streamId, message);
		}

		public void EditMessage(CommunityChatMessage message, string newMessage)
		{
			Club.EditMessage(this.m_clubId, this.StreamId, message.MessageIdentifier, newMessage);
		}

		public void DeleteMessage(CommunityChatMessage message)
		{
			Club.DestroyMessage(this.m_clubId, this.StreamId, message.MessageIdentifier);
		}

		public bool RequestMoreMessages()
		{
			return Club.RequestMoreMessagesBefore(this.m_clubId, this.StreamId, null, new uint?(50u));
		}

		public bool FocusStream()
		{
			return Club.FocusStream(this.m_clubId, this.StreamId);
		}

		public void UnfocusStream()
		{
			Club.UnfocusStream(this.m_clubId, this.StreamId);
		}

		public bool IsSubscribed()
		{
			return Club.IsSubscribedToStream(this.m_clubId, this.StreamId);
		}

		public void ClearNotifications()
		{
			Club.AdvanceStreamViewMarker(this.m_clubId, this.StreamId);
		}

		public void HandleClubMessageHistoryEvent(Club.ClubMessageHistoryReceivedEvent historyEvent)
		{
			if (historyEvent.ClubID != this.m_clubId || historyEvent.StreamID != this.StreamId)
			{
				return;
			}
			List<ClubMessageInfo> messagesInRange = Club.GetMessagesInRange(this.m_clubId, this.StreamId, historyEvent.ContiguousRange.oldestMessageId, historyEvent.ContiguousRange.newestMessageId);
			this.m_messages.Clear();
			foreach (ClubMessageInfo messageInfo in messagesInRange)
			{
				this.m_messages.Add(new CommunityChatMessage(this.m_clubId, this.StreamId, messageInfo));
			}
		}

		public CommunityChatMessage HandleMessageAddedEvent(Club.ClubMessageAddedEvent messageEvent)
		{
			ClubMessageInfo? messageInfo = Club.GetMessageInfo(this.m_clubId, this.StreamId, messageEvent.MessageID);
			if (messageInfo != null)
			{
				CommunityChatMessage communityChatMessage = new CommunityChatMessage(this.m_clubId, this.StreamId, messageInfo.Value);
				this.m_messages.Add(communityChatMessage);
				return communityChatMessage;
			}
			return null;
		}

		public CommunityChatMessage HandleMessageUpdatedEvent(Club.ClubMessageUpdatedEvent messageEvent)
		{
			if (messageEvent.ClubID == this.m_clubId && messageEvent.StreamID == this.StreamId)
			{
				CommunityChatMessage communityChatMessage = this.m_messages.Find((CommunityChatMessage message) => message.MessageIdentifier.epoch == messageEvent.MessageID.epoch && message.MessageIdentifier.position == messageEvent.MessageID.position);
				communityChatMessage.UpdateMessage(messageEvent);
				return communityChatMessage;
			}
			return null;
		}

		public bool HasUnreadMessages()
		{
			return Club.GetStreamViewMarker(this.m_clubId, this.StreamId) != null;
		}

		private ulong m_clubId;

		private readonly ClubStreamInfo m_streamInfo;

		private List<CommunityChatMessage> m_messages = new List<CommunityChatMessage>();

		private ClubStreamNotificationSetting m_notificationSetting;
	}
}
