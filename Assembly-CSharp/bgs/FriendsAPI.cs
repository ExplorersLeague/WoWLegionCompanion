using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.friends;
using bnet.protocol.invitation;

namespace bgs
{
	public class FriendsAPI : BattleNetAPI
	{
		public FriendsAPI(BattleNetCSharp battlenet) : base(battlenet, "Friends")
		{
		}

		public ServiceDescriptor FriendsService
		{
			get
			{
				return this.m_friendsService;
			}
		}

		public ServiceDescriptor FriendsNotifyService
		{
			get
			{
				return this.m_friendsNotifyService;
			}
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 1u, new RPCContextDelegate(this.NotifyFriendAddedListenerCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 2u, new RPCContextDelegate(this.NotifyFriendRemovedListenerCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 3u, new RPCContextDelegate(this.NotifyReceivedInvitationAddedCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 4u, new RPCContextDelegate(this.NotifyReceivedInvitationRemovedCallback));
		}

		public override void Initialize()
		{
			base.Initialize();
			if (this.m_state == FriendsAPI.FriendsAPIState.INITIALIZING)
			{
				return;
			}
			this.StartInitialize();
			this.Subscribe();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public bool IsInitialized
		{
			get
			{
				return this.m_state == FriendsAPI.FriendsAPIState.INITIALIZED || this.m_state == FriendsAPI.FriendsAPIState.FAILED_TO_INITIALIZE;
			}
		}

		public float InitializeTimeOut
		{
			get
			{
				return this.m_initializeTimeOut;
			}
			set
			{
				this.m_initializeTimeOut = value;
			}
		}

		public override void Process()
		{
			base.Process();
			if (this.m_state == FriendsAPI.FriendsAPIState.INITIALIZING && BattleNet.GetRealTimeSinceStartup() - this.m_subscribeStartTime >= (double)this.InitializeTimeOut)
			{
				this.m_state = FriendsAPI.FriendsAPIState.FAILED_TO_INITIALIZE;
				base.ApiLog.LogWarning("Battle.net Friends API C#: Initialize timed out.");
			}
		}

		public bool IsFriend(BnetEntityId entityId)
		{
			return this.m_friendEntityId.ContainsKey(entityId);
		}

		public bool GetFriendsActiveGameAccounts(BnetEntityId entityId, [Out] Map<ulong, bnet.protocol.EntityId> gameAccounts)
		{
			return this.m_friendEntityId.TryGetValue(entityId, out gameAccounts);
		}

		public bool AddFriendsActiveGameAccount(BnetEntityId entityId, bnet.protocol.EntityId gameAccount, ulong index)
		{
			if (this.IsFriend(entityId))
			{
				if (!this.m_friendEntityId[entityId].ContainsKey(index))
				{
					this.m_friendEntityId[entityId].Add(index, gameAccount);
					this.m_battleNet.Presence.PresenceSubscribe(gameAccount);
				}
				return true;
			}
			return false;
		}

		public void RemoveFriendsActiveGameAccount(BnetEntityId entityId, ulong index)
		{
			bnet.protocol.EntityId entityId2;
			if (this.IsFriend(entityId) && this.m_friendEntityId[entityId].TryGetValue(index, out entityId2))
			{
				this.m_battleNet.Presence.PresenceUnsubscribe(entityId2);
				this.m_friendEntityId[entityId].Remove(index);
			}
		}

		public void GetFriendsInfo(ref FriendsInfo info)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			info.maxFriends = (int)this.m_maxFriends;
			info.maxRecvInvites = (int)this.m_maxReceivedInvitations;
			info.maxSentInvites = (int)this.m_maxSentInvitations;
			info.friendsSize = (int)this.m_friendsCount;
			info.updateSize = this.m_updateList.Count;
		}

		public void ClearFriendsUpdates()
		{
			this.m_updateList.Clear();
		}

		public void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			this.m_updateList.CopyTo(updates, 0);
		}

		public void SendFriendInvite(string sender, string target, bool byEmail)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(0UL);
			entityId.SetHigh(0UL);
			sendInvitationRequest.SetTargetId(entityId);
			InvitationParams invitationParams = new InvitationParams();
			FriendInvitationParams friendInvitationParams = new FriendInvitationParams();
			if (byEmail)
			{
				friendInvitationParams.SetTargetEmail(target);
				friendInvitationParams.AddRole(2u);
			}
			else
			{
				friendInvitationParams.SetTargetBattleTag(target);
				friendInvitationParams.AddRole(1u);
			}
			invitationParams.SetFriendParams(friendInvitationParams);
			sendInvitationRequest.SetParams(invitationParams);
			SendInvitationRequest sendInvitationRequest2 = sendInvitationRequest;
			if (!sendInvitationRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to SendFriendInvite.");
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService.Id, 2u, sendInvitationRequest2, new RPCContextDelegate(this.SendInvitationCallback), 0u);
		}

		public void ManageFriendInvite(int action, ulong inviteId)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			if (action != 1)
			{
				if (action == 3)
				{
					this.DeclineInvitation(inviteId);
				}
			}
			else
			{
				this.AcceptInvitation(inviteId);
			}
		}

		public void RemoveFriend(BnetAccountId account)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(account.GetLo());
			entityId.SetHigh(account.GetHi());
			GenericFriendRequest genericFriendRequest = new GenericFriendRequest();
			genericFriendRequest.SetTargetId(entityId);
			GenericFriendRequest genericFriendRequest2 = genericFriendRequest;
			if (!genericFriendRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to RemoveFriend.");
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnRemoveFriend, BattleNetErrors.ERROR_API_NOT_READY, 0);
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService.Id, 8u, genericFriendRequest2, new RPCContextDelegate(this.RemoveFriendCallback), 0u);
		}

		private void Subscribe()
		{
			SubscribeToFriendsRequest subscribeToFriendsRequest = new SubscribeToFriendsRequest();
			subscribeToFriendsRequest.SetObjectId(0UL);
			SubscribeToFriendsRequest subscribeToFriendsRequest2 = subscribeToFriendsRequest;
			if (!subscribeToFriendsRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to Subscribe.");
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService.Id, 1u, subscribeToFriendsRequest2, new RPCContextDelegate(this.SubscribeToFriendsCallback), 0u);
		}

		private void AcceptInvitation(ulong inviteId)
		{
			GenericRequest genericRequest = new GenericRequest();
			genericRequest.SetInvitationId(inviteId);
			GenericRequest genericRequest2 = genericRequest;
			if (!genericRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to AcceptInvitation.");
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnAcceptInvitation, BattleNetErrors.ERROR_API_NOT_READY, 0);
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService.Id, 3u, genericRequest2, new RPCContextDelegate(this.AcceptInvitationCallback), 0u);
		}

		private void DeclineInvitation(ulong inviteId)
		{
			GenericRequest genericRequest = new GenericRequest();
			genericRequest.SetInvitationId(inviteId);
			GenericRequest genericRequest2 = genericRequest;
			if (!genericRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to DeclineInvitation.");
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnDeclineInvitation, BattleNetErrors.ERROR_API_NOT_READY, 0);
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService.Id, 5u, genericRequest2, new RPCContextDelegate(this.DeclineInvitationCallback), 0u);
		}

		private void SubscribeToFriendsCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZING)
			{
				return;
			}
			if (context.Header.Status == 0u)
			{
				this.m_state = FriendsAPI.FriendsAPIState.INITIALIZED;
				base.ApiLog.LogDebug("Battle.net Friends API C#: Initialized.");
				SubscribeToFriendsResponse response = SubscribeToFriendsResponse.ParseFrom(context.Payload);
				this.ProcessSubscribeToFriendsResponse(response);
			}
			else
			{
				this.m_state = FriendsAPI.FriendsAPIState.FAILED_TO_INITIALIZE;
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to initialize.");
			}
		}

		private void SendInvitationCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to SendInvitation. " + status);
			}
			this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnSendInvitation, status, 0);
		}

		private void AcceptInvitationCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to AcceptInvitation. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnAcceptInvitation, status, 0);
			}
		}

		private void DeclineInvitationCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to DeclineInvitation. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnDeclineInvitation, status, 0);
			}
		}

		private void RemoveFriendCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to RemoveFriend. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnRemoveFriend, status, 0);
			}
		}

		private void NotifyFriendAddedListenerCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BnetEntityId entityId = this.ExtractEntityIdFromFriendNotification(context.Payload);
			this.AddFriendInternal(entityId);
		}

		private void NotifyFriendRemovedListenerCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BnetEntityId entityId = this.ExtractEntityIdFromFriendNotification(context.Payload);
			this.RemoveFriendInternal(entityId);
		}

		private void NotifyReceivedInvitationAddedCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			Invitation invitation = this.ExtractInvitationFromInvitationNotification(context.Payload);
			this.AddInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE, invitation, 0);
		}

		private void NotifyReceivedInvitationRemovedCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			Invitation invitation = this.ExtractInvitationFromInvitationNotification(context.Payload);
			this.AddInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE_REMOVED, invitation, 0);
		}

		private void ProcessSubscribeToFriendsResponse(SubscribeToFriendsResponse response)
		{
			if (response.HasMaxFriends)
			{
				this.m_maxFriends = response.MaxFriends;
			}
			if (response.HasMaxReceivedInvitations)
			{
				this.m_maxReceivedInvitations = response.MaxReceivedInvitations;
			}
			if (response.HasMaxSentInvitations)
			{
				this.m_maxSentInvitations = response.MaxSentInvitations;
			}
			for (int i = 0; i < response.FriendsCount; i++)
			{
				Friend friend = response.Friends[i];
				BnetEntityId bnetEntityId = new BnetEntityId();
				bnetEntityId.SetLo(friend.Id.Low);
				bnetEntityId.SetHi(friend.Id.High);
				this.AddFriendInternal(bnetEntityId);
			}
			for (int j = 0; j < response.ReceivedInvitationsCount; j++)
			{
				Invitation invitation = response.ReceivedInvitations[j];
				this.AddInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE, invitation, 0);
			}
			for (int k = 0; k < response.SentInvitationsCount; k++)
			{
				Invitation invitation2 = response.SentInvitations[k];
				this.AddInvitationInternal(FriendsUpdate.Action.FRIEND_SENT_INVITE, invitation2, 0);
			}
		}

		private void StartInitialize()
		{
			this.m_subscribeStartTime = BattleNet.GetRealTimeSinceStartup();
			this.m_state = FriendsAPI.FriendsAPIState.INITIALIZING;
			this.m_maxFriends = 0u;
			this.m_maxReceivedInvitations = 0u;
			this.m_maxSentInvitations = 0u;
			this.m_friendsCount = 0u;
			this.m_updateList = new List<FriendsUpdate>();
			this.m_friendEntityId = new Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>>();
		}

		private void AddFriendInternal(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = 1;
			item.entity1 = entityId;
			this.m_updateList.Add(item);
			this.m_battleNet.Presence.PresenceSubscribe(BnetEntityId.CreateForProtocol(entityId));
			this.m_friendEntityId.Add(entityId, new Map<ulong, bnet.protocol.EntityId>());
			this.m_friendsCount = (uint)this.m_friendEntityId.Count;
		}

		private void RemoveFriendInternal(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = 2;
			item.entity1 = entityId;
			this.m_updateList.Add(item);
			this.m_battleNet.Presence.PresenceUnsubscribe(BnetEntityId.CreateForProtocol(entityId));
			if (this.m_friendEntityId.ContainsKey(entityId))
			{
				foreach (bnet.protocol.EntityId entityId2 in this.m_friendEntityId[entityId].Values)
				{
					this.m_battleNet.Presence.PresenceUnsubscribe(entityId2);
				}
				this.m_friendEntityId.Remove(entityId);
			}
			this.m_friendsCount = (uint)this.m_friendEntityId.Count;
		}

		private void AddInvitationInternal(FriendsUpdate.Action action, Invitation invitation, int reason)
		{
			if (invitation == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = (int)action;
			item.long1 = invitation.Id;
			item.entity1 = this.GetBnetEntityIdFromIdentity(invitation.InviterIdentity);
			if (invitation.HasInviterName)
			{
				item.string1 = invitation.InviterName;
			}
			item.entity2 = this.GetBnetEntityIdFromIdentity(invitation.InviteeIdentity);
			if (invitation.HasInviteeName)
			{
				item.string2 = invitation.InviteeName;
			}
			if (invitation.HasInvitationMessage)
			{
				item.string3 = invitation.InvitationMessage;
			}
			item.bool1 = false;
			if (invitation.HasCreationTime)
			{
				item.long2 = invitation.CreationTime;
			}
			if (invitation.HasExpirationTime)
			{
				item.long3 = invitation.ExpirationTime;
			}
			this.m_updateList.Add(item);
		}

		private BnetEntityId GetBnetEntityIdFromIdentity(Identity identity)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			if (identity.HasAccountId)
			{
				bnetEntityId.SetLo(identity.AccountId.Low);
				bnetEntityId.SetHi(identity.AccountId.High);
			}
			else if (identity.HasGameAccountId)
			{
				bnetEntityId.SetLo(identity.GameAccountId.Low);
				bnetEntityId.SetHi(identity.GameAccountId.High);
			}
			else
			{
				bnetEntityId.SetLo(0UL);
				bnetEntityId.SetHi(0UL);
			}
			return bnetEntityId;
		}

		private BnetEntityId ExtractEntityIdFromFriendNotification(byte[] payload)
		{
			FriendNotification friendNotification = FriendNotification.ParseFrom(payload);
			FriendNotification friendNotification2 = friendNotification;
			if (!friendNotification2.IsInitialized)
			{
				return null;
			}
			return BnetEntityId.CreateFromProtocol(friendNotification2.Target.Id);
		}

		private Invitation ExtractInvitationFromInvitationNotification(byte[] payload)
		{
			InvitationNotification invitationNotification = InvitationNotification.ParseFrom(payload);
			InvitationNotification invitationNotification2 = invitationNotification;
			if (!invitationNotification2.IsInitialized)
			{
				return null;
			}
			return invitationNotification2.Invitation;
		}

		private ServiceDescriptor m_friendsService = new FriendsService();

		private ServiceDescriptor m_friendsNotifyService = new FriendsNotify();

		private FriendsAPI.FriendsAPIState m_state;

		private double m_subscribeStartTime;

		private float m_initializeTimeOut = 5f;

		private uint m_maxFriends;

		private uint m_maxReceivedInvitations;

		private uint m_maxSentInvitations;

		private uint m_friendsCount;

		private List<FriendsUpdate> m_updateList = new List<FriendsUpdate>();

		private Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>> m_friendEntityId = new Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>>();

		public enum InviteAction
		{
			INVITE_ACCEPT = 1,
			INVITE_REVOKE,
			INVITE_DECLINE,
			INVITE_IGNORE
		}

		private enum FriendsAPIState
		{
			NOT_SET,
			INITIALIZING,
			INITIALIZED,
			FAILED_TO_INITIALIZE
		}
	}
}
