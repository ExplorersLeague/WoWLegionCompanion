using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.friends;

namespace bgs.RPCServices
{
	public class FriendsService : ServiceDescriptor
	{
		public FriendsService() : base("bnet.protocol.friends.FriendsService")
		{
			this.Methods = new MethodDescriptor[13];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.friends.FriendsService.SubscribeToFriends";
			uint i = 1u;
			if (FriendsService.<>f__mg$cache0 == null)
			{
				FriendsService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeToFriendsResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, FriendsService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.friends.FriendsService.SendInvitation";
			uint i2 = 2u;
			if (FriendsService.<>f__mg$cache1 == null)
			{
				FriendsService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, FriendsService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.friends.FriendsService.AcceptInvitation";
			uint i3 = 3u;
			if (FriendsService.<>f__mg$cache2 == null)
			{
				FriendsService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, FriendsService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.friends.FriendsService.RevokeInvitation";
			uint i4 = 4u;
			if (FriendsService.<>f__mg$cache3 == null)
			{
				FriendsService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, FriendsService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.friends.FriendsService.DeclineInvitation";
			uint i5 = 5u;
			if (FriendsService.<>f__mg$cache4 == null)
			{
				FriendsService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, FriendsService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.friends.FriendsService.IgnoreInvitation";
			uint i6 = 6u;
			if (FriendsService.<>f__mg$cache5 == null)
			{
				FriendsService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, FriendsService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.friends.FriendsService.AssignRole";
			uint i7 = 7u;
			if (FriendsService.<>f__mg$cache6 == null)
			{
				FriendsService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, FriendsService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)8);
			string n8 = "bnet.protocol.friends.FriendsService.RemoveFriend";
			uint i8 = 8u;
			if (FriendsService.<>f__mg$cache7 == null)
			{
				FriendsService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GenericFriendResponse>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, FriendsService.<>f__mg$cache7);
			MethodDescriptor[] methods9 = this.Methods;
			int num9 = (int)((UIntPtr)9);
			string n9 = "bnet.protocol.friends.FriendsService.ViewFriends";
			uint i9 = 9u;
			if (FriendsService.<>f__mg$cache8 == null)
			{
				FriendsService.<>f__mg$cache8 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ViewFriendsResponse>);
			}
			methods9[num9] = new MethodDescriptor(n9, i9, FriendsService.<>f__mg$cache8);
			MethodDescriptor[] methods10 = this.Methods;
			int num10 = (int)((UIntPtr)10);
			string n10 = "bnet.protocol.friends.FriendsService.UpdateFriendState";
			uint i10 = 10u;
			if (FriendsService.<>f__mg$cache9 == null)
			{
				FriendsService.<>f__mg$cache9 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods10[num10] = new MethodDescriptor(n10, i10, FriendsService.<>f__mg$cache9);
			MethodDescriptor[] methods11 = this.Methods;
			int num11 = (int)((UIntPtr)11);
			string n11 = "bnet.protocol.friends.FriendsService.UnsubscribeToFriends";
			uint i11 = 11u;
			if (FriendsService.<>f__mg$cacheA == null)
			{
				FriendsService.<>f__mg$cacheA = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods11[num11] = new MethodDescriptor(n11, i11, FriendsService.<>f__mg$cacheA);
			MethodDescriptor[] methods12 = this.Methods;
			int num12 = (int)((UIntPtr)12);
			string n12 = "bnet.protocol.friends.FriendsService.RevokeAllInvitations";
			uint i12 = 12u;
			if (FriendsService.<>f__mg$cacheB == null)
			{
				FriendsService.<>f__mg$cacheB = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods12[num12] = new MethodDescriptor(n12, i12, FriendsService.<>f__mg$cacheB);
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

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheB;
	}
}
