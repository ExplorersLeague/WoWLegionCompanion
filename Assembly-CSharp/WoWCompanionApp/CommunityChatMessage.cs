using System;

namespace WoWCompanionApp
{
	public class CommunityChatMessage
	{
		public CommunityChatMessage(ulong communityID, ulong streamID, ClubMessageInfo messageInfo)
		{
			this.CommunityID = communityID;
			this.StreamID = streamID;
			this.m_messageInfo = messageInfo;
		}

		public string Author
		{
			get
			{
				return this.m_messageInfo.author.name;
			}
		}

		public ulong CommunityID { get; private set; }

		public ulong StreamID { get; private set; }

		public uint MemberID
		{
			get
			{
				return this.m_messageInfo.author.memberId;
			}
		}

		public uint ClassID
		{
			get
			{
				uint? classID = this.m_messageInfo.author.classID;
				return (classID == null) ? 0u : classID.Value;
			}
		}

		public bool Destroyed
		{
			get
			{
				return this.m_messageInfo.destroyed;
			}
		}

		public string Message
		{
			get
			{
				if (this.Destroyed && this.m_messageInfo.destroyer != null)
				{
					return MobileClient.FormatString(StaticDB.GetString("MESSAGE_DELETED", "[PH] Message deleted by %s"), this.m_messageInfo.destroyer.Value.name ?? string.Empty);
				}
				return this.m_messageInfo.content;
			}
		}

		public DateTime TimeStamp
		{
			get
			{
				return CommunityChatMessage.BASE_EPOCH.AddTicks(Convert.ToInt64(this.m_messageInfo.messageId.epoch) * 10L).ToLocalTime();
			}
		}

		public ClubMessageIdentifier MessageIdentifier
		{
			get
			{
				return this.m_messageInfo.messageId;
			}
		}

		public bool PostedByModerator()
		{
			return this.m_messageInfo.author.role != null && this.m_messageInfo.author.role.Value != 4;
		}

		public void UpdateMessage(Club.ClubMessageUpdatedEvent messageEvent)
		{
			ClubMessageInfo? messageInfo = Club.GetMessageInfo(messageEvent.ClubID, messageEvent.StreamID, messageEvent.MessageID);
			if (messageInfo != null)
			{
				this.m_messageInfo = messageInfo.Value;
			}
		}

		public void DeleteMessage()
		{
			Club.DestroyMessage(this.CommunityID, this.StreamID, this.MessageIdentifier);
		}

		public bool CanBeDestroyed()
		{
			if (this.m_messageInfo.destroyed)
			{
				return false;
			}
			ClubPrivilegeInfo clubPrivileges = Club.GetClubPrivileges(this.CommunityID);
			return this.m_messageInfo.author.isSelf || clubPrivileges.canDestroyOtherMessage;
		}

		public PlayerLocation GetAsPlayerLocation()
		{
			PlayerLocation result = default(PlayerLocation);
			result.locationType = 5;
			result.clubID = new ulong?(this.CommunityID);
			result.guid = string.Empty;
			result.streamID = new ulong?(this.StreamID);
			result.position = new ulong?(this.MessageIdentifier.position);
			result.epoch = new ulong?(this.MessageIdentifier.epoch);
			return result;
		}

		public bool CreatedBySelf()
		{
			return this.m_messageInfo.author.isSelf;
		}

		public void HandleMemberUpdatedEvent(Club.ClubMemberUpdatedEvent memberUpdatedEvent)
		{
			if (memberUpdatedEvent.ClubID == this.CommunityID && memberUpdatedEvent.MemberID == this.MemberID)
			{
				ClubMessageInfo? messageInfo = Club.GetMessageInfo(this.CommunityID, this.StreamID, this.MessageIdentifier);
				this.m_messageInfo = ((messageInfo == null) ? this.m_messageInfo : messageInfo.Value);
			}
		}

		private ClubMessageInfo m_messageInfo;

		private static readonly DateTime BASE_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	}
}
