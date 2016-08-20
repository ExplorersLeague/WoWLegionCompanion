using System;
using System.Collections.Generic;
using bgs.Wrapper.Impl;

namespace bgs
{
	public class LogAdapter
	{
		public static void SetLogger<T>(T outputter) where T : LoggerInterface, new()
		{
			LogAdapter.s_impl = outputter;
		}

		public static void Log(LogLevel logLevel, string str)
		{
			LogAdapter.s_impl.Log(logLevel, str);
		}

		public static List<string> GetLogEvents()
		{
			return LogAdapter.s_impl.GetLogEvents();
		}

		public static void ClearLogEvents()
		{
			LogAdapter.s_impl.ClearLogEvents();
		}

		private static LoggerInterface s_impl = new DefaultLogger();
	}
}
