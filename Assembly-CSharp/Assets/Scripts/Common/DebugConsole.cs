using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Common
{
	public class DebugConsole : MonoBehaviour
	{
		public void Awake()
		{
			this.Subscribe();
		}

		public void Subscribe()
		{
			if (!this._subscribed)
			{
				Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
				this._subscribed = true;
			}
		}

		public void Update()
		{
			if (Input.GetKeyDown(96))
			{
				this._expand = !this._expand;
			}
		}

		public void HandleLog(string message, string stackTrace, LogType type)
		{
			this._log.AppendLine(message);
			if (this.StackTrace)
			{
				this._log.AppendLine(stackTrace);
			}
			this._fullLog.AppendLine(message);
			this._fullLog.AppendLine(stackTrace);
		}

		public void OnGUI()
		{
			if (!this.Show)
			{
				return;
			}
			if (GUI.Button(this.ButtonRect, "Console"))
			{
				this._expand = !this._expand;
			}
			if (this._expand)
			{
				this._scrollPos = GUI.BeginScrollView(this.PosRect, this._scrollPos, this.ViewRect);
				GUI.TextArea(new Rect(0f, 0f, this.ViewRect.width - 50f, this.ViewRect.height), this._log.ToString());
				GUI.EndScrollView();
			}
		}

		public string GetLogs()
		{
			return this._fullLog.ToString();
		}

		public Rect ButtonRect = new Rect(0f, 0f, 200f, 80f);

		public Rect PosRect = new Rect(0f, 80f, 1000f, 1000f);

		public Rect ViewRect = new Rect(0f, 0f, 1000f, 1000f);

		public bool Show;

		public bool StackTrace;

		private bool _expand;

		private Vector2 _scrollPos;

		private int _count;

		private readonly StringBuilder _log = new StringBuilder();

		private readonly StringBuilder _fullLog = new StringBuilder();

		private bool _subscribed;
	}
}
