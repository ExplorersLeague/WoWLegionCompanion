using System;
using bnet.protocol.channel;

namespace bgs.RPCServices
{
	public class ChannelSubscriberService : ServiceDescriptor
	{
		public ChannelSubscriberService() : base("bnet.protocol.channel.ChannelSubscriber")
		{
			this.Methods = new MethodDescriptor[8];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyAdd", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AddNotification>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyJoin", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinNotification>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyRemove", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<RemoveNotification>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyLeave", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LeaveNotification>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifySendMessage", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SendMessageNotification>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyUpdateChannelState", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateChannelStateNotification>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.channel.ChannelSubscriber.NotifyUpdateMemberState", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateMemberStateNotification>));
		}

		public const uint NOTIFY_ADD_ID = 1u;

		public const uint NOTIFY_JOIN_ID = 2u;

		public const uint NOTIFY_REMOVE_ID = 3u;

		public const uint NOTIFY_LEAVE_ID = 4u;

		public const uint NOTIFY_SEND_MESSAGE_ID = 5u;

		public const uint NOTIFY_UPDATE_CHANNEL_STATE_ID = 6u;

		public const uint NOTIFY_UPDATE_MEMBER_STATE_ID = 7u;
	}
}
