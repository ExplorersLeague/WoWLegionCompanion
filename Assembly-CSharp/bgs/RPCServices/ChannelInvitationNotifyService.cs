using System;
using System.Runtime.CompilerServices;
using bnet.protocol.channel_invitation;

namespace bgs.RPCServices
{
	public class ChannelInvitationNotifyService : ServiceDescriptor
	{
		public ChannelInvitationNotifyService() : base("bnet.protocol.channel_invitation.ChannelInvitationNotify")
		{
			this.Methods = new MethodDescriptor[5];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.channel_invitation.ChannelInvitationNotify.NotifyReceivedInvitationAdded";
			uint i = 1u;
			if (ChannelInvitationNotifyService.<>f__mg$cache0 == null)
			{
				ChannelInvitationNotifyService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationAddedNotification>);
			}
			methods[num] = new MethodDescriptor(n, i, ChannelInvitationNotifyService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.channel_invitation.ChannelInvitationNotify.NotifyReceivedInvitationRemoved";
			uint i2 = 2u;
			if (ChannelInvitationNotifyService.<>f__mg$cache1 == null)
			{
				ChannelInvitationNotifyService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationRemovedNotification>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChannelInvitationNotifyService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.channel_invitation.ChannelInvitationNotify.NotifyReceivedSuggestionAdded";
			uint i3 = 3u;
			if (ChannelInvitationNotifyService.<>f__mg$cache2 == null)
			{
				ChannelInvitationNotifyService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SuggestionAddedNotification>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChannelInvitationNotifyService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.channel_invitation.ChannelInvitationNotify.HasRoomForInvitation";
			uint i4 = 4u;
			if (ChannelInvitationNotifyService.<>f__mg$cache3 == null)
			{
				ChannelInvitationNotifyService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<HasRoomForInvitationRequest>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ChannelInvitationNotifyService.<>f__mg$cache3);
		}

		public const uint NOTIFY_RECEIVED_INVITATION_ADDED_ID = 1u;

		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED_ID = 2u;

		public const uint NOTIFY_RECEIVED_SUGGESTION_ADDED_ID = 3u;

		public const uint HAS_ROOM_FOR_INVITATION_ID = 4u;

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
