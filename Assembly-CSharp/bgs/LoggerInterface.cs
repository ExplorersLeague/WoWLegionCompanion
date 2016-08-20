using System;
using System.Collections.Generic;

namespace bgs
{
	public interface LoggerInterface
	{
		void Log(LogLevel logLevel, string str);

		List<string> GetLogEvents();

		void ClearLogEvents();
	}
}
