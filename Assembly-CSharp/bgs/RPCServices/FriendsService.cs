using System;
using bnet.protocol;
using bnet.protocol.friends;

namespace bgs.RPCServices
{
	public class FriendsService : ServiceDescriptor
	{
		public FriendsService() : base("bnet.protocol.friends.FriendsService")
		{
			this.Methods = new MethodDescriptor[13];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.SubscribeToFriends", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeToFriendsResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.SendInvitation", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.AcceptInvitation", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.RevokeInvitation", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.DeclineInvitation", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.IgnoreInvitation", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.AssignRole", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)8)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.RemoveFriend", 8u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GenericFriendResponse>));
			this.Methods[(int)((UIntPtr)9)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.ViewFriends", 9u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ViewFriendsResponse>));
			this.Methods[(int)((UIntPtr)10)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.UpdateFriendState", 10u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)11)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.UnsubscribeToFriends", 11u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)12)] = new MethodDescriptor("bnet.protocol.friends.FriendsService.RevokeAllInvitations", 12u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		public const uint SUBSCRIBE_TO_FRIENDS = 1u;

		public const uint SEND_INVITATION = 2u;

		public const uint ACCEPT_INVITATION = 3u;

		public const uint REVOKE_INVITATION = 4u;

		public const uint DECLINE_INVITATION = 5u;

		public const uint IGNORE_INVITATION = 6u;

		public const uint ASSIGN_ROLE = 7u;

		public const uint REMOVE_FRIEND = 8u;

		public const uint VIEW_FRIENDS = 9u;

		public const uint UPDATE_FRIEND_STATE = 10u;

		public const uint UNSUBSCRIBE_TO_FRIENDS = 11u;

		public const uint REVOKE_ALL_INVITATIONS = 12u;
	}
}
