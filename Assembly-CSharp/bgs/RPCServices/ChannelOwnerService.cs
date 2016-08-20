using System;
using bnet.protocol.channel;

namespace bgs.RPCServices
{
	public class ChannelOwnerService : ServiceDescriptor
	{
		public ChannelOwnerService() : base("bnet.protocol.channel.ChannelOwner")
		{
			this.Methods = new MethodDescriptor[7];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.GetChannelId", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetChannelIdResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.CreateChannel", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<CreateChannelResponse>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.JoinChannel", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinChannelResponse>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.FindChannel", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FindChannelResponse>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.GetChannelInfo", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetChannelInfoResponse>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.channel.ChannelOwner.SubscribeChannel", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeChannelResponse>));
		}

		public const uint GET_CHANNELID_ID = 1u;

		public const uint CREATE_CHANNEL_ID = 2u;

		public const uint JOIN_CHANNEL_ID = 3u;

		public const uint FIND_CHANNEL_ID = 4u;

		public const uint GET_CHANNEL_INFO_ID = 5u;

		public const uint SUBSCRIBE_CHANNEL_ID = 6u;
	}
}
