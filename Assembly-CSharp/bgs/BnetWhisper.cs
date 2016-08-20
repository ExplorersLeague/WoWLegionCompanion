using System;

namespace bgs
{
	public class BnetWhisper
	{
		public BnetGameAccountId GetSpeakerId()
		{
			return this.m_speakerId;
		}

		public void SetSpeakerId(BnetGameAccountId id)
		{
			this.m_speakerId = id;
		}

		public BnetGameAccountId GetReceiverId()
		{
			return this.m_receiverId;
		}

		public void SetReceiverId(BnetGameAccountId id)
		{
			this.m_receiverId = id;
		}

		public string GetMessage()
		{
			return this.m_message;
		}

		public void SetMessage(string message)
		{
			this.m_message = message;
		}

		public ulong GetTimestampMicrosec()
		{
			return this.m_timestampMicrosec;
		}

		public void SetTimestampMicrosec(ulong microsec)
		{
			this.m_timestampMicrosec = microsec;
		}

		public void SetTimestampMilliseconds(double milliseconds)
		{
			this.m_timestampMicrosec = (ulong)(milliseconds * 1000.0);
		}

		public BnetErrorInfo GetErrorInfo()
		{
			return this.m_errorInfo;
		}

		public void SetErrorInfo(BnetErrorInfo errorInfo)
		{
			this.m_errorInfo = errorInfo;
		}

		private BnetGameAccountId m_speakerId;

		private BnetGameAccountId m_receiverId;

		private string m_message;

		private ulong m_timestampMicrosec;

		private BnetErrorInfo m_errorInfo;
	}
}
