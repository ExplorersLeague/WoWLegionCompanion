using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
	public class NotificationParams
	{
		public int Id;

		public TimeSpan Delay;

		public string Title;

		public string Message;

		public string Ticker;

		public bool Sound = true;

		public bool Vibrate = true;

		public int[] Vibration = new int[]
		{
			1000,
			1000
		};

		public bool Light = true;

		public int LightOnMs = 3000;

		public int LightOffMs = 3000;

		public Color LightColor = Color.green;

		public NotificationIcon SmallIcon;

		public Color SmallIconColor;

		public string LargeIcon;

		public NotificationExecuteMode Mode;
	}
}
