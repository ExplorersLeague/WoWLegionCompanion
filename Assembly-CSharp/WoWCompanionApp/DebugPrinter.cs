using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace WoWCompanionApp
{
	public class DebugPrinter : Singleton<DebugPrinter>
	{
		public static void Log(string message, Object context = null, DebugPrinter.LogLevel level = DebugPrinter.LogLevel.Info)
		{
			Singleton<DebugPrinter>.Instance.debugInfo.Enqueue(new DebugPrinter.DebugPrintInfo
			{
				Message = message + "\n" + new StackTrace(1, true).ToString(),
				Level = level,
				Context = context
			});
		}

		private void Update()
		{
			foreach (DebugPrinter.DebugPrintInfo debugPrintInfo in this.debugInfo)
			{
				DebugPrinter.LogLevel level = debugPrintInfo.Level;
				if (level != DebugPrinter.LogLevel.Info)
				{
					if (level != DebugPrinter.LogLevel.Warning)
					{
						if (level == DebugPrinter.LogLevel.Error)
						{
							Debug.LogError(debugPrintInfo.Message, debugPrintInfo.Context);
						}
					}
					else
					{
						Debug.LogWarning(debugPrintInfo.Message, debugPrintInfo.Context);
					}
				}
				else
				{
					Debug.Log(debugPrintInfo.Message, debugPrintInfo.Context);
				}
			}
			this.debugInfo.Clear();
		}

		private Queue<DebugPrinter.DebugPrintInfo> debugInfo = new Queue<DebugPrinter.DebugPrintInfo>();

		public enum LogLevel
		{
			Info,
			Warning,
			Error
		}

		private struct DebugPrintInfo
		{
			public string Message;

			public DebugPrinter.LogLevel Level;

			public Object Context;
		}
	}
}
