using System;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
	public List<string> LogLines
	{
		get
		{
			return this.m_logLines;
		}
		set
		{
		}
	}

	private void Start()
	{
		this.m_panelLogHandler = new Logger.PanelLogHandler(this, this.m_panelScript);
		if (this.m_panelLogHandler != null)
		{
		}
		this.m_logLines = new List<string>();
	}

	public void AddLogLine(string newLine)
	{
		this.m_logLines.Add(newLine);
		if (this.m_logLines.Count > this.m_maxLogLines)
		{
			this.m_logLines.RemoveAt(0);
		}
		string text = string.Empty;
		int num = this.m_logLines.Count - 1;
		if (num >= 0)
		{
			int num2 = num - 19;
			if (num2 < 0)
			{
				num2 = 0;
			}
			for (int i = num; i >= num2; i--)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					1 + i - num2,
					": ",
					this.m_logLines[i],
					"\n"
				});
			}
			Main.instance.SetDebugText(text);
		}
	}

	public void ClearLog()
	{
		this.m_logLines.Clear();
	}

	public LogPanel m_panelScript;

	public int m_maxLogLines;

	private Logger.PanelLogHandler m_panelLogHandler;

	private List<string> m_logLines;

	public class PanelLogHandler : ILogHandler
	{
		public PanelLogHandler(Logger loggerScript, LogPanel panelScript)
		{
			this.m_loggerScript = loggerScript;
			this.m_panelScript = panelScript;
		}

		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			this.m_loggerScript.AddLogLine(string.Format(format, args));
			this.m_DefaultLogHandler.LogFormat(logType, context, format, args);
			if (this.m_panelScript != null && this.m_panelScript.gameObject.activeSelf)
			{
				this.m_panelScript.UpdateLogDisplay();
			}
		}

		public void LogException(Exception exception, Object context)
		{
			this.m_DefaultLogHandler.LogException(exception, context);
		}

		private ILogHandler m_DefaultLogHandler = Debug.logger.logHandler;

		private Logger m_loggerScript;

		private LogPanel m_panelScript;
	}
}
