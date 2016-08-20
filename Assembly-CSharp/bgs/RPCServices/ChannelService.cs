using System;
using bnet.protocol;

namespace bgs.RPCServices
{
	public class ChannelService : ServiceDescriptor
	{
		public ChannelService() : base("bnet.protocol.channel.Channel")
		{
			this.Methods = new MethodDescriptor[9];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.channel.Channel.AddMember", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.channel.Channel.RemoveMember", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.channel.Channel.SendMessage", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.channel.Channel.UpdateChannelState", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.channel.Channel.UpdateMemberState", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.channel.Channel.Dissolve", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.channel.Channel.AddMember", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)8)] = new MethodDescriptor("bnet.protocol.channel.Channel.UnsubscribeMember", 8u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		public const uint ADD_MEMBER_ID = 1u;

		public const uint REMOVE_MEMBER_ID = 2u;

		public const uint SEND_MESSAGE_ID = 3u;

		public const uint UPDATE_CHANNEL_STATE_ID = 4u;

		public const uint UPDATE_MEMBER_STATE_ID = 5u;

		public const uint DISSOLVE_ID = 6u;

		public const uint SETROLES_ID = 7u;

		public const uint UNSUBSCRIBE_MEMBER_ID = 8u;
	}
}
