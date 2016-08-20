using System;
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
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.Subscribe", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.Unsubscribe", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.SendInvitation", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SendInvitationResponse>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.AcceptInvitation", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AcceptInvitationResponse>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.DeclineInvitation", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.RevokeInvitation", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.SuggestInvitation", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)8)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.IncrementChannelCount", 8u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<IncrementChannelCountResponse>));
			this.Methods[(int)((UIntPtr)9)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.DecrementChannelCount", 9u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)10)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.UpdateChannelCount", 10u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)11)] = new MethodDescriptor("bnet.protocol.channel_invitation.ChannelInvitationService.ListChannelCount", 11u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ListChannelCountResponse>));
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
	}
}
