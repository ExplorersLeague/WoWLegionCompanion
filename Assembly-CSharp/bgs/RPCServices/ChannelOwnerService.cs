using System;
using System.Runtime.CompilerServices;
using bnet.protocol.channel;

namespace bgs.RPCServices
{
	public class ChannelOwnerService : ServiceDescriptor
	{
		public ChannelOwnerService() : base("bnet.protocol.channel.ChannelOwner")
		{
			this.Methods = new MethodDescriptor[7];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.channel.ChannelOwner.GetChannelId";
			uint i = 1u;
			if (ChannelOwnerService.<>f__mg$cache0 == null)
			{
				ChannelOwnerService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetChannelIdResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, ChannelOwnerService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.channel.ChannelOwner.CreateChannel";
			uint i2 = 2u;
			if (ChannelOwnerService.<>f__mg$cache1 == null)
			{
				ChannelOwnerService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<CreateChannelResponse>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChannelOwnerService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.channel.ChannelOwner.JoinChannel";
			uint i3 = 3u;
			if (ChannelOwnerService.<>f__mg$cache2 == null)
			{
				ChannelOwnerService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinChannelResponse>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChannelOwnerService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.channel.ChannelOwner.FindChannel";
			uint i4 = 4u;
			if (ChannelOwnerService.<>f__mg$cache3 == null)
			{
				ChannelOwnerService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FindChannelResponse>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ChannelOwnerService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.channel.ChannelOwner.GetChannelInfo";
			uint i5 = 5u;
			if (ChannelOwnerService.<>f__mg$cache4 == null)
			{
				ChannelOwnerService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetChannelInfoResponse>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, ChannelOwnerService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.channel.ChannelOwner.SubscribeChannel";
			uint i6 = 6u;
			if (ChannelOwnerService.<>f__mg$cache5 == null)
			{
				ChannelOwnerService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeChannelResponse>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, ChannelOwnerService.<>f__mg$cache5);
		}

		public const uint GET_CHANNELID_ID = 1u;

		public const uint CREATE_CHANNEL_ID = 2u;

		public const uint JOIN_CHANNEL_ID = 3u;

		public const uint FIND_CHANNEL_ID = 4u;

		public const uint GET_CHANNEL_INFO_ID = 5u;

		public const uint SUBSCRIBE_CHANNEL_ID = 6u;

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
	}
}
