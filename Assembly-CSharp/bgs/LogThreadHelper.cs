using System;
using System.Collections.Generic;

namespace bgs
{
	public class LogThreadHelper
	{
		public LogThreadHelper(string name)
		{
			this.m_logSource = new BattleNetLogSource(name);
		}

		public void Process()
		{
			List<LogThreadHelper.LogEntry> queuedLogs = this.m_queuedLogs;
			lock (queuedLogs)
			{
				foreach (LogThreadHelper.LogEntry logEntry in this.m_queuedLogs)
				{
					switch (logEntry.Level)
					{
					case LogLevel.Info:
						this.m_logSource.LogInfo(logEntry.Message);
						continue;
					case LogLevel.Warning:
						this.m_logSource.LogWarning(logEntry.Message);
						continue;
					case LogLevel.Error:
						this.m_logSource.LogError(logEntry.Message);
						continue;
					}
					this.m_logSource.LogDebug(logEntry.Message);
				}
				this.m_queuedLogs.Clear();
			}
		}

		public void LogDebug(string message)
		{
			this.LogMessage(message, LogLevel.Debug);
		}

		public void LogDebug(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Debug);
		}

		public void LogInfo(string message)
		{
			this.LogMessage(message, LogLevel.Info);
		}

		public void LogInfo(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Info);
		}

		public void LogWarning(string message)
		{
			this.LogMessage(message, LogLevel.Warning);
		}

		public void LogWarning(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Warning);
		}

		public void LogError(string message)
		{
			this.LogMessage(message, LogLevel.Error);
		}

		public void LogError(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(message, LogLevel.Error);
		}

		private void LogMessage(string message, LogLevel level)
		{
			List<LogThreadHelper.LogEntry> queuedLogs = this.m_queuedLogs;
			lock (queuedLogs)
			{
				this.m_queuedLogs.Add(new LogThreadHelper.LogEntry
				{
					Message = message,
					Level = level
				});
			}
		}

		private BattleNetLogSource m_logSource;

		private List<LogThreadHelper.LogEntry> m_queuedLogs = new List<LogThreadHelper.LogEntry>();

		private class LogEntry
		{
			public string Message;

			public LogLevel Level;
		}
	}
}
