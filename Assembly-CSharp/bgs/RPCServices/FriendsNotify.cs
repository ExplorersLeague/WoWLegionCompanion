using System;
using System.Runtime.CompilerServices;
using bnet.protocol.friends;

namespace bgs.RPCServices
{
	public class FriendsNotify : ServiceDescriptor
	{
		public FriendsNotify() : base("bnet.protocol.friends.FriendsNotify")
		{
			this.Methods = new MethodDescriptor[8];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.friends.FriendsNotify.NotifyFriendAdded";
			uint i = 1u;
			if (FriendsNotify.<>f__mg$cache0 == null)
			{
				FriendsNotify.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FriendNotification>);
			}
			methods[num] = new MethodDescriptor(n, i, FriendsNotify.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.friends.FriendsNotify.NotifyFriendRemoved";
			uint i2 = 2u;
			if (FriendsNotify.<>f__mg$cache1 == null)
			{
				FriendsNotify.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FriendNotification>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, FriendsNotify.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.friends.FriendsNotify.NotifyReceivedInvitationAdded";
			uint i3 = 3u;
			if (FriendsNotify.<>f__mg$cache2 == null)
			{
				FriendsNotify.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, FriendsNotify.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.friends.FriendsNotify.NotifyReceivedInvitationRemoved";
			uint i4 = 4u;
			if (FriendsNotify.<>f__mg$cache3 == null)
			{
				FriendsNotify.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, FriendsNotify.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.friends.FriendsNotify.NotifySentInvitationAdded";
			uint i5 = 5u;
			if (FriendsNotify.<>f__mg$cache4 == null)
			{
				FriendsNotify.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, FriendsNotify.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.friends.FriendsNotify.NotifySentInvitationRemoved";
			uint i6 = 6u;
			if (FriendsNotify.<>f__mg$cache5 == null)
			{
				FriendsNotify.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<InvitationNotification>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, FriendsNotify.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.friends.FriendsNotify.NotifyUpdateFriendState";
			uint i7 = 7u;
			if (FriendsNotify.<>f__mg$cache6 == null)
			{
				FriendsNotify.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<UpdateFriendStateNotification>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, FriendsNotify.<>f__mg$cache6);
		}

		public const uint NOTIFY_FRIEND_ADDED = 1u;

		public const uint NOTIFY_FRIEND_REMOVED = 2u;

		public const uint NOTIFY_RECEIVED_INVITATION_ADDED = 3u;

		public const uint NOTIFY_RECEIVED_INVITATION_REMOVED = 4u;

		public const uint NOTIFY_SENT_INVITATION_ADDED = 5u;

		public const uint NOTIFY_SENT_INVITATION_REMOVED = 6u;

		public const uint NOTIFY_UPDATE_FRIEND_STATE = 7u;

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
	}
}
