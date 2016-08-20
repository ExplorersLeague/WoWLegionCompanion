using System;
using System.Collections.Generic;

namespace bgs
{
	public class Logger
	{
		public LogLevel GetDefaultLevel()
		{
			return LogLevel.Debug;
		}

		public void Print(string format, params object[] args)
		{
			LogLevel defaultLevel = this.GetDefaultLevel();
			this.Print(defaultLevel, format, args);
		}

		public void Print(LogLevel level, string format, params object[] args)
		{
			string message;
			if (args.Length == 0)
			{
				message = format;
			}
			else
			{
				message = string.Format(format, args);
			}
			this.Print(level, message);
		}

		public void Print(LogLevel level, string message)
		{
			LogAdapter.Log(level, message);
		}

		public List<string> GetLogEvents()
		{
			return LogAdapter.GetLogEvents();
		}

		public void ClearLogEvents()
		{
			LogAdapter.ClearLogEvents();
		}
	}
}
