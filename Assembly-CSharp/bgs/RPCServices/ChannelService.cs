using System;
using System.Runtime.CompilerServices;
using bnet.protocol;

namespace bgs.RPCServices
{
	public class ChannelService : ServiceDescriptor
	{
		public ChannelService() : base("bnet.protocol.channel.Channel")
		{
			this.Methods = new MethodDescriptor[9];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.channel.Channel.AddMember";
			uint i = 1u;
			if (ChannelService.<>f__mg$cache0 == null)
			{
				ChannelService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods[num] = new MethodDescriptor(n, i, ChannelService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.channel.Channel.RemoveMember";
			uint i2 = 2u;
			if (ChannelService.<>f__mg$cache1 == null)
			{
				ChannelService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChannelService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.channel.Channel.SendMessage";
			uint i3 = 3u;
			if (ChannelService.<>f__mg$cache2 == null)
			{
				ChannelService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChannelService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.channel.Channel.UpdateChannelState";
			uint i4 = 4u;
			if (ChannelService.<>f__mg$cache3 == null)
			{
				ChannelService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ChannelService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.channel.Channel.UpdateMemberState";
			uint i5 = 5u;
			if (ChannelService.<>f__mg$cache4 == null)
			{
				ChannelService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, ChannelService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.channel.Channel.Dissolve";
			uint i6 = 6u;
			if (ChannelService.<>f__mg$cache5 == null)
			{
				ChannelService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, ChannelService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.channel.Channel.AddMember";
			uint i7 = 7u;
			if (ChannelService.<>f__mg$cache6 == null)
			{
				ChannelService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, ChannelService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)8);
			string n8 = "bnet.protocol.channel.Channel.UnsubscribeMember";
			uint i8 = 8u;
			if (ChannelService.<>f__mg$cache7 == null)
			{
				ChannelService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, ChannelService.<>f__mg$cache7);
		}

		public const uint ADD_MEMBER_ID = 1u;

		public const uint REMOVE_MEMBER_ID = 2u;

		public const uint SEND_MESSAGE_ID = 3u;

		public const uint UPDATE_CHANNEL_STATE_ID = 4u;

		public const uint UPDATE_MEMBER_STATE_ID = 5u;

		public const uint DISSOLVE_ID = 6u;

		public const uint SETROLES_ID = 7u;

		public const uint UNSUBSCRIBE_MEMBER_ID = 8u;

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

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache7;
	}
}
