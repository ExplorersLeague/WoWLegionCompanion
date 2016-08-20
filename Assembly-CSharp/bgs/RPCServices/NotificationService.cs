using System;
using bnet.protocol;
using bnet.protocol.notification;

namespace bgs.RPCServices
{
	public class NotificationService : ServiceDescriptor
	{
		public NotificationService() : base("bnet.protocol.notification.NotificationService")
		{
			this.Methods = new MethodDescriptor[5];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.notification.NotificationService.SendNotification", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.notification.NotificationService.RegisterClient", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.notification.NotificationService.UnregisterClient", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.notification.NotificationService.FindClient", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FindClientResponse>));
		}

		public const uint SEND_NOTIFICATION_ID = 1u;

		public const uint REGISTER_CLIENT_ID = 2u;

		public const uint UNREGISTER_CLIENT_ID = 3u;

		public const uint FIND_CLIENT_ID = 4u;
	}
}
