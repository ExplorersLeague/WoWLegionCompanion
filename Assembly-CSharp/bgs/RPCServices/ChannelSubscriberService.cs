using System;
using System.Runtime.CompilerServices;
using bnet.protocol.channel;

namespace bgs.RPCServices
{
	public class ChannelSubscriberService : ServiceDescriptor
	{
		public ChannelSubscriberService() : base("bnet.protocol.channel.ChannelSubscriber")
		{
			this.Methods = new MethodDescriptor[8];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.channel.ChannelSubscriber.NotifyAdd";
			uint i = 1u;
			if (ChannelSubscriberService.<>f__mg$cache0 == null)
			{
				ChannelSubscriberService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AddNotification>);
			}
			methods[num] = new MethodDescriptor(n, i, ChannelSubscriberService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.channel.ChannelSubscriber.NotifyJoin";
			uint i2 = 2u;
			if (ChannelSubscriberService.<>f__mg$cache1 == null)
			{
				ChannelSubscriberService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinNotification>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChannelSubscriberService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.channel.ChannelSubscriber.NotifyRemove";
			uint i3 = 3u;
			if (ChannelSubscriberService.<>f__mg$cache2 == null)
			{
				ChannelSubscriberService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<RemoveNotification>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChannelSubscriberService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.channel.ChannelSubscriber.NotifyLeave";
			uint i4 = 4u;
			if (ChannelSubscriberService.<>f__mg$cache3 == null)
			{
				ChannelSubscriberService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LeaveNotification>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ChannelSubscriberService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.channel.ChannelSubscriber.NotifySendMessage";
			uint i5 = 5u;
			if (ChannelSubscriberService.<>f__mg$cache4 == null)
			{
				ChannelSubscriberService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SendMessageNotification>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, ChannelSubscriberService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.channel.ChannelSubscriber.NotifyUpdateChannelState";
			uint i6 = 6u;
			if (ChannelSubscriberService.<>f__mg$cache5 == null)
			{
				ChannelSubscriberService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateChannelStateNotification>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, ChannelSubscriberService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.channel.ChannelSubscriber.NotifyUpdateMemberState";
			uint i7 = 7u;
			if (ChannelSubscriberService.<>f__mg$cache6 == null)
			{
				ChannelSubscriberService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateMemberStateNotification>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, ChannelSubscriberService.<>f__mg$cache6);
		}

		public const uint NOTIFY_ADD_ID = 1u;

		public const uint NOTIFY_JOIN_ID = 2u;

		public const uint NOTIFY_REMOVE_ID = 3u;

		public const uint NOTIFY_LEAVE_ID = 4u;

		public const uint NOTIFY_SEND_MESSAGE_ID = 5u;

		public const uint NOTIFY_UPDATE_CHANNEL_STATE_ID = 6u;

		public const uint NOTIFY_UPDATE_MEMBER_STATE_ID = 7u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache1;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache2;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache3;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache4;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache5;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache6;
	}
}
