using System;

namespace bgs
{
	public struct BnetNotification
	{
		public BnetNotification(string notificationType)
		{
			this.NotificationType = notificationType;
			this.BlobMessage = new byte[0];
			this.MessageType = 0;
			this.MessageSize = 0;
		}

		public const string NotificationType_UtilNotificationMessage = "WTCG.UtilNotificationMessage";

		public const string NotificationAttribute_MessageType = "message_type";

		public const string NotificationAttribute_MessageSize = "message_size";

		public const string NotificationAttribute_MessageFragmentPrefix = "fragment_";

		public string NotificationType;

		public byte[] BlobMessage;

		public int MessageType;

		public int MessageSize;
	}
}
