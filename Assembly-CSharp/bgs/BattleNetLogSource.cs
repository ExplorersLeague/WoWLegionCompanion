using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace bgs
{
	public class BattleNetLogSource
	{
		public BattleNetLogSource(string sourceName)
		{
			this.m_sourceName = sourceName;
		}

		private string FormatStacktrace(StackFrame sf, bool fullPath = false)
		{
			if (sf != null)
			{
				string arg = (!fullPath) ? Path.GetFileName(sf.GetFileName()) : sf.GetFileName();
				return string.Format(" ({2} at {0}:{1})", arg, sf.GetFileLineNumber(), sf.GetMethod());
			}
			return string.Empty;
		}

		private void LogMessage(LogLevel logLevel, string message, bool includeFilename = true)
		{
			StackTrace stackTrace = new StackTrace(new StackFrame(2, true));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(this.m_sourceName);
			stringBuilder.Append("] ");
			stringBuilder.Append(message);
			if (stackTrace != null && includeFilename)
			{
				StackFrame frame = stackTrace.GetFrame(0);
				stringBuilder.Append(this.FormatStacktrace(frame, false));
			}
			Log.BattleNet.Print(logLevel, stringBuilder.ToString());
		}

		public void LogDebugStackTrace(string message, int maxFrames, int skipFrames = 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(message + "\n");
			for (int i = 1 + skipFrames; i < maxFrames; i++)
			{
				StackTrace stackTrace = new StackTrace(new StackFrame(i, true));
				if (stackTrace == null)
				{
					break;
				}
				StackFrame frame = stackTrace.GetFrame(0);
				if (frame == null)
				{
					break;
				}
				if (frame.GetMethod() == null || frame.GetMethod().ToString().StartsWith("<"))
				{
					break;
				}
				stringBuilder.Append(string.Format("File \"{0}\", line {1} -- {2}\n", Path.GetFileName(frame.GetFileName()), frame.GetFileLineNumber(), frame.GetMethod()));
			}
			this.LogMessage(LogLevel.Debug, stringBuilder.ToString().TrimEnd(new char[0]), false);
		}

		public void LogDebug(string message)
		{
			this.LogMessage(LogLevel.Debug, message, true);
		}

		public void LogDebug(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(LogLevel.Debug, message, true);
		}

		public void LogInfo(string message)
		{
			this.LogMessage(LogLevel.Info, message, true);
		}

		public void LogInfo(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(LogLevel.Info, message, true);
		}

		public void LogWarning(string message)
		{
			this.LogMessage(LogLevel.Warning, message, true);
		}

		public void LogWarning(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(LogLevel.Warning, message, true);
		}

		public void LogError(string message)
		{
			this.LogMessage(LogLevel.Error, message, true);
		}

		public void LogError(string format, params object[] args)
		{
			string message = string.Format(format, args);
			this.LogMessage(LogLevel.Error, message, true);
		}

		private string m_sourceName;
	}
}
