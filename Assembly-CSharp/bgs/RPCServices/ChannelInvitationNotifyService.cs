using System;
using bnet.protocol.channel_invitation;

namespace bgs.RPCServices
{
	public class ChannelInvitationNotifyService : ServiceDescriptor
	{
		public ChannelInvitationNotifyService() : base("bnet.protocol.channel_invitation.ChannelInvitationNotify")
		{
			this.Methods = new MethodDescriptor[5];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationNotify.NotifyReceivedInvitationAdded", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationAddedNotification>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationNotify.NotifyReceivedInvitationRemoved", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationRemovedNotification>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationNotify.NotifyReceivedSuggestionAdded", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SuggestionAddedNotification>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationNotify.HasRoomForInvitation", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<HasRoomForInvitationRequest>));
		}

		public const uint NOTIFY_RECEIVED_INVITATION_ADDED_ID = 1u;

		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED_ID = 2u;

		public const uint NOTIFY_RECEIVED_SUGGESTION_ADDED_ID = 3u;

		public const uint HAS_ROOM_FOR_INVITATION_ID = 4u;
	}
}
