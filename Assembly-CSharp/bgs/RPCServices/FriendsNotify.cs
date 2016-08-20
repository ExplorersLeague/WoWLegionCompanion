using System;
using bnet.protocol.friends;

namespace bgs.RPCServices
{
	public class FriendsNotify : ServiceDescriptor
	{
		public FriendsNotify() : base("bnet.protocol.friends.FriendsNotify")
		{
			this.Methods = new MethodDescriptor[8];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifyFriendAdded", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FriendNotification>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifyFriendRemoved", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FriendNotification>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifyReceivedInvitationAdded", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifyReceivedInvitationRemoved", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifySentInvitationAdded", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifySentInvitationRemoved", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.friends.FriendsNotify.NotifyUpdateFriendState", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateFriendStateNotification>));
		}

		public const uint NOTIFY_FRIEND_ADDED = 1u;

		public const uint NOTIFY_FRIEND_REMOVED = 2u;

		public const uint NOTIFY_RECEIVED_INVITATION_ADDED = 3u;

		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED = 4u;

		public const uint NOTIFY_SENT_INVITATION_ADDED = 5u;

		public const uint NOTIFY_SENT_INVITATION_REMOVED = 6u;

		public const uint NOTIFY_UPDATE_FRIEND_STATE = 7u;
	}
}
