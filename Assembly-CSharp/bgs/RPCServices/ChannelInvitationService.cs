using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.channel_invitation;
using bnet.protocol.invitation;

namespace bgs.RPCServices
{
	public class ChannelInvitationService : ServiceDescriptor
	{
		public ChannelInvitationService() : base("bnet.protocol.channel_invitation.ChannelInvitationService")
		{
			this.Methods = new MethodDescriptor[12];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.channel_invitation.ChannelInvitationService.Subscribe";
			uint i = 1u;
			if (ChannelInvitationService.<>f__mg$cache0 == null)
			{
				ChannelInvitationService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, ChannelInvitationService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.channel_invitation.ChannelInvitationService.Unsubscribe";
			uint i2 = 2u;
			if (ChannelInvitationService.<>f__mg$cache1 == null)
			{
				ChannelInvitationService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChannelInvitationService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.channel_invitation.ChannelInvitationService.SendInvitation";
			uint i3 = 3u;
			if (ChannelInvitationService.<>f__mg$cache2 == null)
			{
				ChannelInvitationService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SendInvitationResponse>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChannelInvitationService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.channel_invitation.ChannelInvitationService.AcceptInvitation";
			uint i4 = 4u;
			if (ChannelInvitationService.<>f__mg$cache3 == null)
			{
				ChannelInvitationService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AcceptInvitationResponse>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ChannelInvitationService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.channel_invitation.ChannelInvitationService.DeclineInvitation";
			uint i5 = 5u;
			if (ChannelInvitationService.<>f__mg$cache4 == null)
			{
				ChannelInvitationService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, ChannelInvitationService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.channel_invitation.ChannelInvitationService.RevokeInvitation";
			uint i6 = 6u;
			if (ChannelInvitationService.<>f__mg$cache5 == null)
			{
				ChannelInvitationService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, ChannelInvitationService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.channel_invitation.ChannelInvitationService.SuggestInvitation";
			uint i7 = 7u;
			if (ChannelInvitationService.<>f__mg$cache6 == null)
			{
				ChannelInvitationService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, ChannelInvitationService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)8);
			string n8 = "bnet.protocol.channel_invitation.ChannelInvitationService.IncrementChannelCount";
			uint i8 = 8u;
			if (ChannelInvitationService.<>f__mg$cache7 == null)
			{
				ChannelInvitationService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<IncrementChannelCountResponse>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, ChannelInvitationService.<>f__mg$cache7);
			MethodDescriptor[] methods9 = this.Methods;
			int num9 = (int)((UIntPtr)9);
			string n9 = "bnet.protocol.channel_invitation.ChannelInvitationService.DecrementChannelCount";
			uint i9 = 9u;
			if (ChannelInvitationService.<>f__mg$cache8 == null)
			{
				ChannelInvitationService.<>f__mg$cache8 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods9[num9] = new MethodDescriptor(n9, i9, ChannelInvitationService.<>f__mg$cache8);
			MethodDescriptor[] methods10 = this.Methods;
			int num10 = (int)((UIntPtr)10);
			string n10 = "bnet.protocol.channel_invitation.ChannelInvitationService.UpdateChannelCount";
			uint i10 = 10u;
			if (ChannelInvitationService.<>f__mg$cache9 == null)
			{
				ChannelInvitationService.<>f__mg$cache9 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods10[num10] = new MethodDescriptor(n10, i10, ChannelInvitationService.<>f__mg$cache9);
			MethodDescriptor[] methods11 = this.Methods;
			int num11 = (int)((UIntPtr)11);
			string n11 = "bnet.protocol.channel_invitation.ChannelInvitationService.ListChannelCount";
			uint i11 = 11u;
			if (ChannelInvitationService.<>f__mg$cacheA == null)
			{
				ChannelInvitationService.<>f__mg$cacheA = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ListChannelCountResponse>);
			}
			methods11[num11] = new MethodDescriptor(n11, i11, ChannelInvitationService.<>f__mg$cacheA);
		}

		public const uint SUBSCRIBE_ID = 1u;

		public const uint UNSUBSCRIBE_ID = 2u;

		public const uint SEND_INVITATION_ID = 3u;

		public const uint ACCEPT_INVITATION_ID = 4u;

		public const uint DECLINE_INVITATION_ID = 5u;

		public const uint REVOKE_INVITATION_ID = 6u;

		public const uint SUGGEST_INVITATION_ID = 7u;

		public const uint INCREMENT_CHANNEL_COUNT_ID = 8u;

		public const uint DECREMENT_CHANNEL_COUNT_ID = 9u;

		public const uint UPDATE_CHANNEL_COUNT_ID = 10u;

		public const uint LIST_CHANNEL_COUNT_ID = 11u;

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

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache8;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache9;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheA;
	}
}
