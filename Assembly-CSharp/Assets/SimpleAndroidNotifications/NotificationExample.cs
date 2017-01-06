using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
	public class NotificationExample : MonoBehaviour
	{
		public void OnGUI()
		{
			if (GUILayout.Button("Simple 5 sec", new GUILayoutOption[]
			{
				GUILayout.Height((float)Screen.height * 0.2f),
				GUILayout.Width((float)Screen.width)
			}))
			{
				NotificationManager.Send(TimeSpan.FromSeconds(5.0), "Simple notification", "Customize icon and color", new Color(1f, 0.3f, 0.15f), NotificationIcon.Bell);
			}
			if (GUILayout.Button("Normal 5 sec", new GUILayoutOption[]
			{
				GUILayout.Height((float)Screen.height * 0.2f),
				GUILayout.Width((float)Screen.width)
			}))
			{
				NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(5.0), "Notification", "Notification with app icon", new Color(0f, 0.6f, 1f), NotificationIcon.Message);
			}
			if (GUILayout.Button("Custom 5 sec", new GUILayoutOption[]
			{
				GUILayout.Height((float)Screen.height * 0.2f),
				GUILayout.Width((float)Screen.width)
			}))
			{
				NotificationParams notificationParams = new NotificationParams
				{
					Id = Random.Range(0, int.MaxValue),
					Delay = TimeSpan.FromSeconds(5.0),
					Title = "Custom notification",
					Message = "Message",
					Ticker = "Ticker",
					Sound = true,
					Vibrate = true,
					Light = true,
					SmallIcon = NotificationIcon.Heart,
					SmallIconColor = new Color(0f, 0.5f, 0f),
					LargeIcon = "app_icon"
				};
				NotificationManager.SendCustom(notificationParams);
			}
			if (GUILayout.Button("Cancel all", new GUILayoutOption[]
			{
				GUILayout.Height((float)Screen.height * 0.2f),
				GUILayout.Width((float)Screen.width)
			}))
			{
				NotificationManager.CancelAll();
			}
		}
	}
}
