using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bnet.protocol.attribute;
using bnet.protocol.notification;

namespace bgs
{
	public class NotificationAPI : BattleNetAPI
	{
		public NotificationAPI(BattleNetCSharp battlenet) : base(battlenet, "Notification")
		{
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public void OnNotification(string notificationType, Notification notification)
		{
			if (notification.AttributeCount <= 0)
			{
				return;
			}
			BnetNotification item = new BnetNotification(notificationType);
			SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();
			int num = 0;
			item.MessageType = 0;
			item.MessageSize = 0;
			for (int i = 0; i < notification.AttributeCount; i++)
			{
				bnet.protocol.attribute.Attribute attribute = notification.Attribute[i];
				if (attribute.Name == "message_type")
				{
					item.MessageType = (int)attribute.Value.IntValue;
				}
				else if (attribute.Name == "message_size")
				{
					item.MessageSize = (int)attribute.Value.IntValue;
				}
				else if (attribute.Name.StartsWith("fragment_"))
				{
					num += attribute.Value.BlobValue.Length;
					sortedDictionary.Add(attribute.Name, i);
				}
			}
			if (item.MessageType == 0)
			{
				BattleNet.Log.LogError(string.Format("Missing notification type {0} of size {1}", item.MessageType, item.MessageSize));
				return;
			}
			if (0 < num)
			{
				item.BlobMessage = new byte[num];
				SortedDictionary<string, int>.Enumerator enumerator = sortedDictionary.GetEnumerator();
				int num2 = 0;
				while (enumerator.MoveNext())
				{
					List<bnet.protocol.attribute.Attribute> attribute2 = notification.Attribute;
					KeyValuePair<string, int> keyValuePair = enumerator.Current;
					byte[] blobValue = attribute2[keyValuePair.Value].Value.BlobValue;
					Array.Copy(blobValue, 0, item.BlobMessage, num2, blobValue.Length);
					num2 += blobValue.Length;
				}
			}
			if (item.MessageSize != num)
			{
				BattleNet.Log.LogError(string.Format("Message size mismatch for notification type {0} - {1} != {2}", item.MessageType, item.MessageSize, num));
				return;
			}
			this.m_notifications.Add(item);
		}

		public int GetNotificationCount()
		{
			return this.m_notifications.Count;
		}

		public void GetNotifications([Out] BnetNotification[] Notifications)
		{
			this.m_notifications.CopyTo(Notifications, 0);
		}

		public void ClearNotifications()
		{
			this.m_notifications.Clear();
		}

		private List<BnetNotification> m_notifications = new List<BnetNotification>();
	}
}
