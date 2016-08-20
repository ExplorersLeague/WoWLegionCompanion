using System;
using System.Collections.Generic;
using System.Linq;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.attribute;
using bnet.protocol.channel;
using bnet.protocol.channel_invitation;
using bnet.protocol.invitation;
using bnet.protocol.presence;

namespace bgs
{
	public class ChannelAPI : BattleNetAPI
	{
		public ChannelAPI(BattleNetCSharp battlenet) : base(battlenet, "Channel")
		{
		}

		public static ulong GetNextObjectId()
		{
			return ChannelAPI.s_nextObjectId += 1UL;
		}

		public void AddActiveChannel(ulong objectId, ChannelAPI.ChannelReferenceObject channelRefObject)
		{
			this.m_activeChannels.Add(objectId, channelRefObject);
			this.m_channelEntityObjectMap[channelRefObject.m_channelData.m_channelId] = objectId;
		}

		public void RemoveActiveChannel(ulong objectId)
		{
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(objectId);
			if (channelReferenceObject != null)
			{
				this.m_channelEntityObjectMap.Remove(channelReferenceObject.m_channelData.m_channelId);
				this.m_activeChannels.Remove(objectId);
			}
		}

		public ChannelAPI.ChannelReferenceObject GetChannelReferenceObject(EntityId entityId)
		{
			ulong key;
			ChannelAPI.ChannelReferenceObject result;
			if (this.m_channelEntityObjectMap.TryGetValue(entityId, out key) && this.m_activeChannels.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		public ChannelAPI.ChannelReferenceObject GetChannelReferenceObject(ulong objectId)
		{
			ChannelAPI.ChannelReferenceObject result;
			if (this.m_activeChannels.TryGetValue(objectId, out result))
			{
				return result;
			}
			return null;
		}

		public ChannelAPI.ReceivedInvite[] GetAllReceivedInvites()
		{
			return this.m_receivedInvitations.Values.SelectMany((List<ChannelAPI.ReceivedInvite> l) => l).ToArray<ChannelAPI.ReceivedInvite>();
		}

		public ChannelAPI.ReceivedInvite[] GetReceivedInvites(ChannelAPI.InvitationServiceType serviceType)
		{
			List<ChannelAPI.ReceivedInvite> list;
			this.m_receivedInvitations.TryGetValue(serviceType, out list);
			return (list != null) ? list.ToArray() : new ChannelAPI.ReceivedInvite[0];
		}

		public Suggestion[] GetInviteRequests(EntityId channelId)
		{
			Suggestion[] array = null;
			List<Suggestion> list;
			if (this.m_receivedInviteRequests != null && this.m_receivedInviteRequests.TryGetValue(channelId, out list))
			{
				array = list.ToArray();
			}
			if (array == null)
			{
				array = new Suggestion[0];
			}
			return array;
		}

		public void RemoveInviteRequestsFor(EntityId channelId, EntityId suggesteeId, uint removeReason)
		{
			if (this.m_receivedInviteRequests == null || suggesteeId == null)
			{
				return;
			}
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(channelId);
			bool flag = channelReferenceObject != null && channelReferenceObject.m_channelData.m_channelType == ChannelAPI.ChannelType.PARTY_CHANNEL;
			List<Suggestion> list;
			if (this.m_receivedInviteRequests.TryGetValue(channelId, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					Suggestion suggestion = list[i];
					if (suggesteeId.Equals(suggestion.SuggesterId))
					{
						list.RemoveAt(i);
						i--;
						if (flag)
						{
							this.m_battleNet.Party.ReceivedInviteRequestDelta(channelId, suggestion, new uint?(removeReason));
						}
					}
				}
				if (list.Count == 0)
				{
					this.m_receivedInviteRequests.Remove(channelId);
					if (this.m_receivedInviteRequests.Count == 0)
					{
						this.m_receivedInviteRequests = null;
					}
				}
			}
		}

		public ChannelAPI.ReceivedInvite GetReceivedInvite(ChannelAPI.InvitationServiceType serviceType, ulong invitationId)
		{
			foreach (ChannelAPI.ReceivedInvite receivedInvite in this.GetReceivedInvites(serviceType))
			{
				if (receivedInvite.Invitation.Id == invitationId)
				{
					return receivedInvite;
				}
			}
			return null;
		}

		public ServiceDescriptor ChannelService
		{
			get
			{
				return this.m_channelService;
			}
		}

		public ServiceDescriptor ChannelSubscriberService
		{
			get
			{
				return this.m_channelSubscriberService;
			}
		}

		public ServiceDescriptor ChannelOwnerService
		{
			get
			{
				return this.m_channelOwnerService;
			}
		}

		public ServiceDescriptor ChannelInvitationService
		{
			get
			{
				return ChannelAPI.m_channelInvitationService;
			}
		}

		public ServiceDescriptor ChannelInvitationNotifyService
		{
			get
			{
				return ChannelAPI.m_channelInvitationNotifyService;
			}
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 1u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyAdd));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 2u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyJoin));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 3u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyRemove));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 4u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyLeave));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 5u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifySendMessage));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 6u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyUpdateChannelState));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelSubscriberService.Id, 7u, new RPCContextDelegate(this.HandleChannelSubscriber_NotifyUpdateMemberState));
			this.m_rpcConnection.RegisterServiceMethodListener(ChannelAPI.m_channelInvitationNotifyService.Id, 1u, new RPCContextDelegate(this.HandleChannelInvitation_NotifyReceivedInvitationAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(ChannelAPI.m_channelInvitationNotifyService.Id, 2u, new RPCContextDelegate(this.HandleChannelInvitation_NotifyReceivedInvitationRemoved));
			this.m_rpcConnection.RegisterServiceMethodListener(ChannelAPI.m_channelInvitationNotifyService.Id, 3u, new RPCContextDelegate(this.HandleChannelInvitation_NotifyReceivedSuggestionAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(ChannelAPI.m_channelInvitationNotifyService.Id, 4u, new RPCContextDelegate(this.HandleChannelInvitation_NotifyHasRoomForInvitation));
		}

		public override void Initialize()
		{
			base.Initialize();
			this.SubscribeToInvitationService();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.m_activeChannels.Clear();
			this.m_channelEntityObjectMap.Clear();
		}

		private void SubscribeToInvitationService()
		{
			bnet.protocol.channel_invitation.SubscribeRequest subscribeRequest = new bnet.protocol.channel_invitation.SubscribeRequest();
			subscribeRequest.SetObjectId(0UL);
			this.m_rpcConnection.QueueRequest(ChannelAPI.m_channelInvitationService.Id, 1u, subscribeRequest, new RPCContextDelegate(this.SubscribeToInvitationServiceCallback), 0u);
		}

		private void SubscribeToInvitationServiceCallback(RPCContext context)
		{
			base.CheckRPCCallback("SubscribeToInvitationServiceCallback", context);
		}

		public void SendInvitation(EntityId channelId, EntityId entityId, ChannelAPI.InvitationServiceType serviceType, RPCContextDelegate callback)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			sendInvitationRequest.SetTargetId(entityId);
			InvitationParams invitationParams = new InvitationParams();
			ChannelInvitationParams channelInvitationParams = new ChannelInvitationParams();
			channelInvitationParams.SetChannelId(channelId);
			channelInvitationParams.SetServiceType((uint)serviceType);
			invitationParams.SetChannelParams(channelInvitationParams);
			sendInvitationRequest.SetParams(invitationParams);
			this.m_rpcConnection.QueueRequest(ChannelAPI.m_channelInvitationService.Id, 3u, sendInvitationRequest, callback, 0u);
		}

		public void AcceptInvitation(ulong invitationId, EntityId channelId, ChannelAPI.ChannelType channelType, RPCContextDelegate callback = null)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			acceptInvitationRequest.SetInvitationId(invitationId);
			acceptInvitationRequest.SetObjectId(ChannelAPI.GetNextObjectId());
			ChannelAPI.ChannelData channelData = new ChannelAPI.ChannelData(this, channelId, 0UL, channelType);
			channelData.SetSubscriberObjectId(acceptInvitationRequest.ObjectId);
			this.m_rpcConnection.QueueRequest(ChannelAPI.m_channelInvitationService.Id, 4u, acceptInvitationRequest, delegate(RPCContext ctx)
			{
				channelData.AcceptInvitationCallback(ctx, callback);
			}, 0u);
		}

		public void DeclineInvitation(ulong invitationId, EntityId channelId, RPCContextDelegate callback)
		{
			GenericRequest genericRequest = new GenericRequest();
			genericRequest.SetInvitationId(invitationId);
			this.m_rpcConnection.QueueRequest(ChannelAPI.m_channelInvitationService.Id, 5u, genericRequest, callback, 0u);
		}

		public void RevokeInvitation(ulong invitationId, EntityId channelId, RPCContextDelegate callback)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			revokeInvitationRequest.SetInvitationId(invitationId);
			revokeInvitationRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(ChannelAPI.m_channelInvitationService.Id, 6u, revokeInvitationRequest, callback, 0u);
		}

		public void SuggestInvitation(EntityId partyId, EntityId whomToAskForApproval, EntityId whomToInvite, RPCContextDelegate callback)
		{
			SuggestInvitationRequest suggestInvitationRequest = new SuggestInvitationRequest();
			suggestInvitationRequest.SetChannelId(partyId);
			suggestInvitationRequest.SetApprovalId(whomToAskForApproval);
			suggestInvitationRequest.SetTargetId(whomToInvite);
			this.m_rpcConnection.QueueRequest(ChannelAPI.m_channelInvitationService.Id, 7u, suggestInvitationRequest, callback, 0u);
		}

		private void HandleChannelInvitation_NotifyReceivedInvitationAdded(RPCContext context)
		{
			base.ApiLog.LogDebug("HandleChannelInvitation_NotifyReceivedInvitationAdded");
			InvitationAddedNotification invitationAddedNotification = InvitationAddedNotification.ParseFrom(context.Payload);
			if (invitationAddedNotification.Invitation.HasChannelInvitation)
			{
				ChannelInvitation channelInvitation = invitationAddedNotification.Invitation.ChannelInvitation;
				ChannelAPI.InvitationServiceType serviceType = (ChannelAPI.InvitationServiceType)channelInvitation.ServiceType;
				List<ChannelAPI.ReceivedInvite> list;
				if (!this.m_receivedInvitations.TryGetValue(serviceType, out list))
				{
					list = new List<ChannelAPI.ReceivedInvite>();
					this.m_receivedInvitations[serviceType] = list;
				}
				list.Add(new ChannelAPI.ReceivedInvite(channelInvitation, invitationAddedNotification.Invitation));
				ChannelAPI.InvitationServiceType invitationServiceType = serviceType;
				if (invitationServiceType == ChannelAPI.InvitationServiceType.INVITATION_SERVICE_TYPE_PARTY)
				{
					this.m_battleNet.Party.ReceivedInvitationAdded(invitationAddedNotification, channelInvitation);
				}
			}
		}

		private void HandleChannelInvitation_NotifyReceivedInvitationRemoved(RPCContext context)
		{
			base.ApiLog.LogDebug("HandleChannelInvitation_NotifyReceivedInvitationRemoved");
			InvitationRemovedNotification invitationRemovedNotification = InvitationRemovedNotification.ParseFrom(context.Payload);
			if (invitationRemovedNotification.Invitation.HasChannelInvitation)
			{
				ChannelInvitation channelInvitation = invitationRemovedNotification.Invitation.ChannelInvitation;
				ChannelAPI.InvitationServiceType serviceType = (ChannelAPI.InvitationServiceType)channelInvitation.ServiceType;
				ulong id = invitationRemovedNotification.Invitation.Id;
				string szPartyType = string.Empty;
				if (serviceType == ChannelAPI.InvitationServiceType.INVITATION_SERVICE_TYPE_PARTY)
				{
					szPartyType = this.m_battleNet.Party.GetReceivedInvitationPartyType(id);
				}
				List<ChannelAPI.ReceivedInvite> list;
				if (this.m_receivedInvitations.TryGetValue(serviceType, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].Invitation.Id == id)
						{
							list.RemoveAt(i);
							break;
						}
					}
					if (list.Count == 0)
					{
						this.m_receivedInvitations.Remove(serviceType);
					}
				}
				ChannelAPI.InvitationServiceType invitationServiceType = serviceType;
				if (invitationServiceType == ChannelAPI.InvitationServiceType.INVITATION_SERVICE_TYPE_PARTY)
				{
					this.m_battleNet.Party.ReceivedInvitationRemoved(szPartyType, invitationRemovedNotification, channelInvitation);
				}
			}
		}

		private void HandleChannelInvitation_NotifyReceivedSuggestionAdded(RPCContext context)
		{
			SuggestionAddedNotification suggestionAddedNotification = SuggestionAddedNotification.ParseFrom(context.Payload);
			EntityId entityId = (!suggestionAddedNotification.Suggestion.HasChannelId) ? null : suggestionAddedNotification.Suggestion.ChannelId;
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(entityId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelInvitation_NotifyReceivedSuggestionAdded had unexpected traffic for channelId: " + entityId);
				return;
			}
			base.ApiLog.LogDebug("HandleChannelInvitation_NotifyReceivedSuggestionAdded: " + suggestionAddedNotification);
			if (this.m_receivedInviteRequests == null)
			{
				this.m_receivedInviteRequests = new Map<EntityId, List<Suggestion>>();
			}
			List<Suggestion> list;
			if (!this.m_receivedInviteRequests.TryGetValue(entityId, out list))
			{
				list = new List<Suggestion>();
				this.m_receivedInviteRequests[entityId] = list;
			}
			if (list.IndexOf(suggestionAddedNotification.Suggestion) < 0)
			{
				list.Add(suggestionAddedNotification.Suggestion);
			}
			ChannelAPI.ChannelType channelType = channelReferenceObject.m_channelData.m_channelType;
			if (channelType == ChannelAPI.ChannelType.PARTY_CHANNEL)
			{
				this.m_battleNet.Party.ReceivedInviteRequestDelta(entityId, suggestionAddedNotification.Suggestion, null);
			}
		}

		private void HandleChannelInvitation_NotifyHasRoomForInvitation(RPCContext context)
		{
			base.ApiLog.LogDebug("HandleChannelInvitation_NotifyHasRoomForInvitation");
		}

		public void JoinChannel(EntityId channelId, ChannelAPI.ChannelType channelType)
		{
			JoinChannelRequest joinChannelRequest = new JoinChannelRequest();
			joinChannelRequest.SetChannelId(channelId);
			joinChannelRequest.SetObjectId(ChannelAPI.GetNextObjectId());
			ChannelAPI.ChannelData channelData = new ChannelAPI.ChannelData(this, channelId, 0UL, channelType);
			channelData.SetSubscriberObjectId(joinChannelRequest.ObjectId);
			this.m_rpcConnection.QueueRequest(this.m_channelOwnerService.Id, 3u, joinChannelRequest, new RPCContextDelegate(channelData.JoinChannelCallback), (uint)channelType);
		}

		public void UpdateChannelAttributes(ChannelAPI.ChannelData channelData, List<bnet.protocol.attribute.Attribute> attributeList, RPCContextDelegate callback)
		{
			UpdateChannelStateRequest updateChannelStateRequest = new UpdateChannelStateRequest();
			bnet.protocol.channel.ChannelState channelState = new bnet.protocol.channel.ChannelState();
			foreach (bnet.protocol.attribute.Attribute val in attributeList)
			{
				channelState.AddAttribute(val);
			}
			updateChannelStateRequest.SetStateChange(channelState);
			this.m_rpcConnection.QueueRequest(this.m_channelService.Id, 4u, updateChannelStateRequest, callback, (uint)channelData.m_objectId);
		}

		private void HandleChannelSubscriber_NotifyAdd(RPCContext context)
		{
			AddNotification addNotification = AddNotification.ParseFrom(context.Payload);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifyAdd had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyAdd: " + addNotification);
			ChannelAPI.ChannelType channelType = channelReferenceObject.m_channelData.m_channelType;
			switch (channelType)
			{
			case ChannelAPI.ChannelType.PRESENCE_CHANNEL:
				if (addNotification.ChannelState.HasPresence)
				{
					bnet.protocol.presence.ChannelState presence = addNotification.ChannelState.Presence;
					this.m_battleNet.Presence.HandlePresenceUpdates(presence, channelReferenceObject);
				}
				goto IL_16E;
			}
			ChannelAPI.ChannelData channelData = (ChannelAPI.ChannelData)channelReferenceObject.m_channelData;
			if (channelData != null)
			{
				channelData.m_channelState = addNotification.ChannelState;
				foreach (Member member in addNotification.MemberList)
				{
					EntityId gameAccountId = member.Identity.GameAccountId;
					channelData.m_members.Add(gameAccountId, member);
					if (!this.m_battleNet.GameAccountId.Equals(gameAccountId))
					{
						this.m_battleNet.Presence.PresenceSubscribe(member.Identity.GameAccountId);
					}
				}
			}
			IL_16E:
			if (channelType == ChannelAPI.ChannelType.PARTY_CHANNEL)
			{
				this.m_battleNet.Party.PartyJoined(channelReferenceObject, addNotification);
			}
		}

		private void HandleChannelSubscriber_NotifyJoin(RPCContext context)
		{
			JoinNotification joinNotification = JoinNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyJoin: " + joinNotification);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifyJoin had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			ChannelAPI.ChannelType channelType = channelReferenceObject.m_channelData.m_channelType;
			switch (channelType)
			{
			case ChannelAPI.ChannelType.PRESENCE_CHANNEL:
				goto IL_103;
			}
			ChannelAPI.ChannelData channelData = (ChannelAPI.ChannelData)channelReferenceObject.m_channelData;
			if (channelData != null)
			{
				EntityId gameAccountId = joinNotification.Member.Identity.GameAccountId;
				channelData.m_members.Add(gameAccountId, joinNotification.Member);
				if (!this.m_battleNet.GameAccountId.Equals(gameAccountId))
				{
					this.m_battleNet.Presence.PresenceSubscribe(joinNotification.Member.Identity.GameAccountId);
				}
			}
			IL_103:
			if (channelType == ChannelAPI.ChannelType.PARTY_CHANNEL)
			{
				this.m_battleNet.Party.PartyMemberJoined(channelReferenceObject, joinNotification);
			}
		}

		private void HandleChannelSubscriber_NotifyRemove(RPCContext context)
		{
			RemoveNotification removeNotification = RemoveNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyRemove: " + removeNotification);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifyRemove had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			switch (channelReferenceObject.m_channelData.m_channelType)
			{
			case ChannelAPI.ChannelType.CHAT_CHANNEL:
				break;
			case ChannelAPI.ChannelType.PARTY_CHANNEL:
				this.m_battleNet.Party.PartyLeft(channelReferenceObject, removeNotification);
				break;
			case ChannelAPI.ChannelType.GAME_CHANNEL:
				this.m_battleNet.Games.GameLeft(channelReferenceObject, removeNotification);
				break;
			default:
				goto IL_144;
			}
			ChannelAPI.ChannelData channelData = (ChannelAPI.ChannelData)channelReferenceObject.m_channelData;
			if (channelData != null)
			{
				foreach (Member member in channelData.m_members.Values)
				{
					if (!this.m_battleNet.GameAccountId.Equals(member.Identity.GameAccountId))
					{
						this.m_battleNet.Presence.PresenceUnsubscribe(member.Identity.GameAccountId);
					}
				}
			}
			IL_144:
			this.RemoveActiveChannel(context.Header.ObjectId);
		}

		private void HandleChannelSubscriber_NotifyLeave(RPCContext context)
		{
			LeaveNotification leaveNotification = LeaveNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyLeave: " + leaveNotification);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifyLeave had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			switch (channelReferenceObject.m_channelData.m_channelType)
			{
			case ChannelAPI.ChannelType.CHAT_CHANNEL:
			case ChannelAPI.ChannelType.GAME_CHANNEL:
				break;
			case ChannelAPI.ChannelType.PARTY_CHANNEL:
				this.m_battleNet.Party.PartyMemberLeft(channelReferenceObject, leaveNotification);
				break;
			default:
				return;
			}
			ChannelAPI.ChannelData channelData = (ChannelAPI.ChannelData)channelReferenceObject.m_channelData;
			if (channelData != null)
			{
				channelData.m_members.Remove(leaveNotification.MemberId);
				if (!this.m_battleNet.GameAccountId.Equals(leaveNotification.MemberId))
				{
					this.m_battleNet.Presence.PresenceUnsubscribe(leaveNotification.MemberId);
				}
			}
		}

		private void HandleChannelSubscriber_NotifySendMessage(RPCContext context)
		{
			SendMessageNotification sendMessageNotification = SendMessageNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifySendMessage: " + sendMessageNotification);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifySendMessage had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			ChannelAPI.ChannelType channelType = channelReferenceObject.m_channelData.m_channelType;
			if (channelType == ChannelAPI.ChannelType.PARTY_CHANNEL)
			{
				this.m_battleNet.Party.PartyMessageReceived(channelReferenceObject, sendMessageNotification);
			}
		}

		private void HandleChannelSubscriber_NotifyUpdateChannelState(RPCContext context)
		{
			UpdateChannelStateNotification updateChannelStateNotification = UpdateChannelStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyUpdateChannelState: " + updateChannelStateNotification);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifyUpdateChannelState had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			ChannelAPI.ChannelType channelType = channelReferenceObject.m_channelData.m_channelType;
			switch (channelType)
			{
			case ChannelAPI.ChannelType.PRESENCE_CHANNEL:
				if (updateChannelStateNotification.StateChange.HasPresence)
				{
					bnet.protocol.presence.ChannelState presence = updateChannelStateNotification.StateChange.Presence;
					this.m_battleNet.Presence.HandlePresenceUpdates(presence, channelReferenceObject);
				}
				return;
			case ChannelAPI.ChannelType.CHAT_CHANNEL:
			case ChannelAPI.ChannelType.GAME_CHANNEL:
				break;
			case ChannelAPI.ChannelType.PARTY_CHANNEL:
				this.m_battleNet.Party.PreprocessPartyChannelUpdated(channelReferenceObject, updateChannelStateNotification);
				break;
			default:
				return;
			}
			ChannelAPI.ChannelData channelData = (ChannelAPI.ChannelData)channelReferenceObject.m_channelData;
			if (channelData != null)
			{
				bool flag = channelType == ChannelAPI.ChannelType.PARTY_CHANNEL;
				bool flag2 = false;
				Map<string, Variant> map = null;
				bnet.protocol.channel.ChannelState channelState = channelData.m_channelState;
				bnet.protocol.channel.ChannelState stateChange = updateChannelStateNotification.StateChange;
				if (stateChange.HasMaxMembers)
				{
					channelState.MaxMembers = stateChange.MaxMembers;
				}
				if (stateChange.HasMinMembers)
				{
					channelState.MinMembers = stateChange.MinMembers;
				}
				if (stateChange.HasMaxInvitations)
				{
					channelState.MaxInvitations = stateChange.MaxInvitations;
				}
				if (stateChange.HasPrivacyLevel && channelState.PrivacyLevel != stateChange.PrivacyLevel)
				{
					channelState.PrivacyLevel = stateChange.PrivacyLevel;
					flag2 = true;
				}
				if (stateChange.HasName)
				{
					channelState.Name = stateChange.Name;
				}
				if (stateChange.HasDelegateName)
				{
					channelState.DelegateName = stateChange.DelegateName;
				}
				if (stateChange.HasChannelType)
				{
					if (!flag)
					{
						channelState.ChannelType = stateChange.ChannelType;
					}
					if (flag && stateChange.ChannelType != PartyAPI.PARTY_TYPE_DEFAULT)
					{
						channelState.ChannelType = stateChange.ChannelType;
						int num = -1;
						for (int i = 0; i < channelState.AttributeList.Count; i++)
						{
							if (channelState.AttributeList[i].Name == "WTCG.Party.Type")
							{
								num = i;
								break;
							}
						}
						bnet.protocol.attribute.Attribute attribute = ProtocolHelper.CreateAttribute("WTCG.Party.Type", channelState.ChannelType);
						if (num >= 0)
						{
							channelState.AttributeList[num] = attribute;
						}
						else
						{
							channelState.AttributeList.Add(attribute);
						}
					}
				}
				if (stateChange.HasProgram)
				{
					channelState.Program = stateChange.Program;
				}
				if (stateChange.HasAllowOfflineMembers)
				{
					channelState.AllowOfflineMembers = stateChange.AllowOfflineMembers;
				}
				if (stateChange.HasSubscribeToPresence)
				{
					channelState.SubscribeToPresence = stateChange.SubscribeToPresence;
				}
				if (stateChange.AttributeCount > 0 && map == null)
				{
					map = new Map<string, Variant>();
				}
				for (int j = 0; j < stateChange.AttributeCount; j++)
				{
					bnet.protocol.attribute.Attribute attribute2 = stateChange.AttributeList[j];
					int num2 = -1;
					for (int k = 0; k < channelState.AttributeList.Count; k++)
					{
						bnet.protocol.attribute.Attribute attribute3 = channelState.AttributeList[k];
						if (attribute3.Name == attribute2.Name)
						{
							num2 = k;
							break;
						}
					}
					if (attribute2.Value.IsNone())
					{
						if (num2 >= 0)
						{
							channelState.AttributeList.RemoveAt(num2);
						}
					}
					else if (num2 >= 0)
					{
						channelState.Attribute[num2] = attribute2;
					}
					else
					{
						channelState.AddAttribute(attribute2);
					}
					map.Add(attribute2.Name, attribute2.Value);
				}
				if (stateChange.HasReason)
				{
					IList<Invitation> invitationList = stateChange.InvitationList;
					IList<Invitation> invitationList2 = channelState.InvitationList;
					for (int l = 0; l < invitationList.Count; l++)
					{
						Invitation invitation = invitationList[l];
						for (int m = 0; m < invitationList2.Count; m++)
						{
							Invitation invitation2 = invitationList2[m];
							if (invitation2.Id == invitation.Id)
							{
								channelState.InvitationList.RemoveAt(m);
								break;
							}
						}
					}
				}
				else
				{
					channelState.Invitation.AddRange(stateChange.InvitationList);
				}
				channelData.m_channelState = channelState;
				if (flag)
				{
					if (flag2)
					{
						this.m_battleNet.Party.PartyPrivacyChanged(channelData.m_channelId, channelState.PrivacyLevel);
					}
					if (stateChange.InvitationList.Count > 0)
					{
						uint? removeReason = null;
						if (stateChange.HasReason)
						{
							removeReason = new uint?(stateChange.Reason);
						}
						foreach (Invitation invite in stateChange.InvitationList)
						{
							this.m_battleNet.Party.PartyInvitationDelta(channelData.m_channelId, invite, removeReason);
						}
					}
					if (map != null)
					{
						foreach (KeyValuePair<string, Variant> keyValuePair in map)
						{
							this.m_battleNet.Party.PartyAttributeChanged(channelData.m_channelId, keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			}
		}

		private void HandleChannelSubscriber_NotifyUpdateMemberState(RPCContext context)
		{
			UpdateMemberStateNotification updateMemberStateNotification = UpdateMemberStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleChannelSubscriber_NotifyUpdateMemberState: " + updateMemberStateNotification);
			ChannelAPI.ChannelReferenceObject channelReferenceObject = this.GetChannelReferenceObject(context.Header.ObjectId);
			if (channelReferenceObject == null)
			{
				base.ApiLog.LogError("HandleChannelSubscriber_NotifyUpdateMemberState had unexpected traffic for objectId : " + context.Header.ObjectId);
				return;
			}
			ChannelAPI.ChannelType channelType = channelReferenceObject.m_channelData.m_channelType;
			ChannelAPI.ChannelData channelData = (ChannelAPI.ChannelData)channelReferenceObject.m_channelData;
			EntityId channelId = channelData.m_channelId;
			List<EntityId> list = null;
			for (int i = 0; i < updateMemberStateNotification.StateChangeList.Count; i++)
			{
				Member deltaMember = updateMemberStateNotification.StateChangeList[i];
				if (!deltaMember.Identity.HasGameAccountId)
				{
					base.ApiLog.LogError("HandleChannelSubscriber_NotifyUpdateMemberState no identity/gameAccount in Member list at index={0} channelId={1}-{2}", new object[]
					{
						i,
						channelId.High,
						channelId.Low
					});
				}
				else
				{
					EntityId gameAccountId = deltaMember.Identity.GameAccountId;
					Map<string, Variant> map = null;
					Member member;
					Member cachedMember;
					if (!channelData.m_members.TryGetValue(gameAccountId, out cachedMember))
					{
						member = deltaMember;
					}
					else
					{
						Member cachedMember2 = cachedMember;
						MemberState state = cachedMember.State;
						if (deltaMember.State.AttributeCount > 0)
						{
							if (map == null)
							{
								map = new Map<string, Variant>();
							}
							for (int j = 0; j < deltaMember.State.AttributeCount; j++)
							{
								bnet.protocol.attribute.Attribute attribute = deltaMember.State.AttributeList[j];
								int num = -1;
								for (int k = 0; k < state.AttributeList.Count; k++)
								{
									bnet.protocol.attribute.Attribute attribute2 = state.AttributeList[k];
									if (attribute2.Name == attribute.Name)
									{
										num = k;
										break;
									}
								}
								if (attribute.Value.IsNone())
								{
									if (num >= 0)
									{
										state.AttributeList.RemoveAt(num);
									}
								}
								else if (num >= 0)
								{
									state.Attribute[num] = attribute;
								}
								else
								{
									state.AddAttribute(attribute);
								}
								map.Add(attribute.Name, attribute.Value);
							}
						}
						else
						{
							if (deltaMember.State.HasPrivileges)
							{
								state.Privileges = deltaMember.State.Privileges;
							}
							if (cachedMember.State.RoleCount != deltaMember.State.RoleCount || !cachedMember.State.RoleList.All((uint roleId) => deltaMember.State.RoleList.Contains(roleId)) || !deltaMember.State.RoleList.All((uint roleId) => cachedMember.State.RoleList.Contains(roleId)))
							{
								if (list == null)
								{
									list = new List<EntityId>();
								}
								list.Add(gameAccountId);
								state.ClearRole();
								state.Role.AddRange(deltaMember.State.RoleList);
							}
							if (deltaMember.State.HasInfo)
							{
								if (state.HasInfo)
								{
									if (deltaMember.State.Info.HasBattleTag)
									{
										state.Info.SetBattleTag(deltaMember.State.Info.BattleTag);
									}
								}
								else
								{
									state.SetInfo(deltaMember.State.Info);
								}
							}
						}
						cachedMember2.SetState(state);
						member = cachedMember2;
					}
					if (member != null)
					{
						channelData.m_members[gameAccountId] = member;
					}
					if (map != null)
					{
					}
				}
			}
			if (list != null)
			{
				bool flag = channelType == ChannelAPI.ChannelType.PARTY_CHANNEL;
				if (flag)
				{
					this.m_battleNet.Party.MemberRolesChanged(channelReferenceObject, list);
				}
			}
		}

		private static ulong s_nextObjectId = 0UL;

		private Map<ulong, ChannelAPI.ChannelReferenceObject> m_activeChannels = new Map<ulong, ChannelAPI.ChannelReferenceObject>();

		private Map<EntityId, ulong> m_channelEntityObjectMap = new Map<EntityId, ulong>();

		private Map<ChannelAPI.InvitationServiceType, List<ChannelAPI.ReceivedInvite>> m_receivedInvitations = new Map<ChannelAPI.InvitationServiceType, List<ChannelAPI.ReceivedInvite>>();

		private Map<EntityId, List<Suggestion>> m_receivedInviteRequests;

		private ServiceDescriptor m_channelService = new ChannelService();

		private ServiceDescriptor m_channelSubscriberService = new ChannelSubscriberService();

		private ServiceDescriptor m_channelOwnerService = new ChannelOwnerService();

		private static ServiceDescriptor m_channelInvitationService = new ChannelInvitationService();

		private static ServiceDescriptor m_channelInvitationNotifyService = new ChannelInvitationNotifyService();

		public enum ChannelType
		{
			PRESENCE_CHANNEL,
			CHAT_CHANNEL,
			PARTY_CHANNEL,
			GAME_CHANNEL
		}

		public enum InvitationServiceType
		{
			INVITATION_SERVICE_TYPE_NONE,
			INVITATION_SERVICE_TYPE_PARTY,
			INVITATION_SERVICE_TYPE_CHAT,
			INVITATION_SERVICE_TYPE_GAMES
		}

		public class BaseChannelData
		{
			public BaseChannelData(EntityId entityId, ulong objectId, ChannelAPI.ChannelType channelType)
			{
				this.m_channelId = entityId;
				this.m_channelType = channelType;
				this.m_objectId = objectId;
			}

			public void SetChannelId(EntityId channelId)
			{
				this.m_channelId = channelId;
			}

			public void SetObjectId(ulong objectId)
			{
				this.m_objectId = objectId;
			}

			public void SetSubscriberObjectId(ulong objectId)
			{
				this.m_subscriberObjectId = objectId;
			}

			public EntityId m_channelId;

			public ChannelAPI.ChannelType m_channelType;

			public ulong m_objectId;

			public ulong m_subscriberObjectId;
		}

		public class ChannelData : ChannelAPI.BaseChannelData
		{
			public ChannelData(ChannelAPI channelAPI, EntityId entityId, ulong objectId, ChannelAPI.ChannelType channelType) : base(entityId, objectId, channelType)
			{
				this.m_channelState = new bnet.protocol.channel.ChannelState();
				this.m_members = new Map<EntityId, Member>();
				this.m_channelAPI = channelAPI;
			}

			public void JoinChannelCallback(RPCContext context)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != BattleNetErrors.ERROR_OK)
				{
					this.m_channelAPI.ApiLog.LogError("JoinChannelCallback: " + status.ToString());
					return;
				}
				JoinChannelResponse joinChannelResponse = JoinChannelResponse.ParseFrom(context.Payload);
				base.SetObjectId(joinChannelResponse.ObjectId);
				this.m_channelAPI.AddActiveChannel(this.m_subscriberObjectId, new ChannelAPI.ChannelReferenceObject(this));
				this.m_channelAPI.ApiLog.LogDebug("JoinChannelCallback, status=" + status.ToString());
			}

			public void AcceptInvitationCallback(RPCContext context, RPCContextDelegate callback)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != BattleNetErrors.ERROR_OK)
				{
					this.m_channelAPI.ApiLog.LogError("AcceptInvitationCallback: " + status.ToString());
					return;
				}
				AcceptInvitationResponse acceptInvitationResponse = AcceptInvitationResponse.ParseFrom(context.Payload);
				base.SetObjectId(acceptInvitationResponse.ObjectId);
				this.m_channelAPI.AddActiveChannel(this.m_subscriberObjectId, new ChannelAPI.ChannelReferenceObject(this));
				this.m_channelAPI.ApiLog.LogDebug("AcceptInvitationCallback, status=" + status.ToString());
				if (callback != null)
				{
					callback(context);
				}
			}

			public bnet.protocol.channel.ChannelState m_channelState;

			public Map<EntityId, Member> m_members;

			private ChannelAPI m_channelAPI;
		}

		public class ChannelReferenceObject
		{
			public ChannelReferenceObject(EntityId entityId, ChannelAPI.ChannelType channelType)
			{
				this.m_channelData = new ChannelAPI.BaseChannelData(entityId, 0UL, channelType);
			}

			public ChannelReferenceObject(ChannelAPI.BaseChannelData channelData)
			{
				this.m_channelData = channelData;
			}

			public ChannelAPI.BaseChannelData m_channelData;
		}

		public class ReceivedInvite
		{
			public ReceivedInvite(ChannelInvitation c, Invitation i)
			{
				this.ChannelInvitation = c;
				this.Invitation = i;
			}

			public EntityId ChannelId
			{
				get
				{
					return this.ChannelInvitation.ChannelDescription.ChannelId;
				}
			}

			public bnet.protocol.channel.ChannelState State
			{
				get
				{
					return this.ChannelInvitation.ChannelDescription.State;
				}
			}

			public string ChannelType
			{
				get
				{
					return this.State.ChannelType;
				}
			}

			public IList<bnet.protocol.attribute.Attribute> Attributes
			{
				get
				{
					return this.State.AttributeList;
				}
			}

			public ChannelInvitation ChannelInvitation;

			public Invitation Invitation;
		}
	}
}
