using System;
using System.Runtime.CompilerServices;
using bnet.protocol.notification;

namespace bgs.RPCServices
{
	public class NotificationListenerService : ServiceDescriptor
	{
		public NotificationListenerService() : base("bnet.protocol.notification.NotificationListener")
		{
			this.Methods = new MethodDescriptor[2];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.notification.NotificationListener.OnNotificationReceived";
			uint i = 1u;
			if (NotificationListenerService.<>f__mg$cache0 == null)
			{
				NotificationListenerService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<Notification>);
			}
			methods[num] = new MethodDescriptor(n, i, NotificationListenerService.<>f__mg$cache0);
		}

		public const uint ON_NOTIFICATION_REC_ID = 1u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;
	}
}
