using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.notification;

namespace bgs.RPCServices
{
	public class NotificationService : ServiceDescriptor
	{
		public NotificationService() : base("bnet.protocol.notification.NotificationService")
		{
			this.Methods = new MethodDescriptor[5];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.notification.NotificationService.SendNotification";
			uint i = 1u;
			if (NotificationService.<>f__mg$cache0 == null)
			{
				NotificationService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods[num] = new MethodDescriptor(n, i, NotificationService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.notification.NotificationService.RegisterClient";
			uint i2 = 2u;
			if (NotificationService.<>f__mg$cache1 == null)
			{
				NotificationService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, NotificationService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.notification.NotificationService.UnregisterClient";
			uint i3 = 3u;
			if (NotificationService.<>f__mg$cache2 == null)
			{
				NotificationService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, NotificationService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.notification.NotificationService.FindClient";
			uint i4 = 4u;
			if (NotificationService.<>f__mg$cache3 == null)
			{
				NotificationService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FindClientResponse>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, NotificationService.<>f__mg$cache3);
		}

		public const uint SEND_NOTIFICATION_ID = 1u;

		public const uint REGISTER_CLIENT_ID = 2u;

		public const uint UNREGISTER_CLIENT_ID = 3u;

		public const uint FIND_CLIENT_ID = 4u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache1;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache2;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache3;
	}
}
