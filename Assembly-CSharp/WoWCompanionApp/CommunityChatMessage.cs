using System;

namespace WoWCompanionApp
{
	public class CommunityChatMessage
	{
		public CommunityChatMessage(ClubMessageInfo messageInfo)
		{
			this.m_messageInfo = messageInfo;
		}

		public string Author
		{
			get
			{
				return this.m_messageInfo.author.name;
			}
		}

		public string Message
		{
			get
			{
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

		private readonly ClubMessageInfo m_messageInfo;

		private static readonly DateTime BASE_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	}
}
