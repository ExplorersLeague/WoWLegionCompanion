using System;
using System.Collections.Generic;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account;

namespace bgs
{
	public class AccountAPI : BattleNetAPI
	{
		public AccountAPI(BattleNetCSharp battlenet) : base(battlenet, "Account")
		{
		}

		public void GetGameAccountState(GetGameAccountStateRequest request, RPCContextDelegate callback)
		{
			this.m_rpcConnection.QueueRequest(this.m_accountService.Id, 31u, request, callback, 0u);
		}

		public AccountAPI.GameSessionInfo LastGameSessionInfo { get; set; }

		public int GameSessionRunningCount { get; set; }

		public ServiceDescriptor AccountService
		{
			get
			{
				return this.m_accountService;
			}
		}

		public ServiceDescriptor AccountNotifyService
		{
			get
			{
				return this.m_accountNotify;
			}
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 1u, new RPCContextDelegate(this.HandleAccountNotify_AccountStateUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 2u, new RPCContextDelegate(this.HandleAccountNotify_GameAccountStateUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 3u, new RPCContextDelegate(this.HandleAccountNotify_GameAccountsUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 4u, new RPCContextDelegate(this.HandleAccountNotify_GameSessionUpdated));
		}

		public override void Initialize()
		{
			base.ApiLog.LogDebug("Account API initializing");
			base.Initialize();
			this.GetAccountLevelInfo(this.m_battleNet.AccountId);
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public uint GetPreferredRegion()
		{
			return this.m_preferredRegion;
		}

		public string GetAccountCountry()
		{
			return this.m_accountCountry;
		}

		public bool CheckLicense(uint licenseId)
		{
			if (this.m_licenses == null || this.m_licenses.Count == 0)
			{
				return false;
			}
			foreach (AccountLicense accountLicense in this.m_licenses)
			{
				if (accountLicense.Id == licenseId)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasLicenses
		{
			get
			{
				return this.m_licenses != null && this.m_licenses.Count > 0;
			}
		}

		public void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			if (!reload)
			{
				if (this.LastGameSessionInfo != null)
				{
					restrictions.loaded = true;
					restrictions.sessionStartTime = this.LastGameSessionInfo.SessionStartTime;
					return;
				}
				if (this.GameSessionRunningCount > 0)
				{
					return;
				}
			}
			this.LastGameSessionInfo = null;
			this.GameSessionRunningCount++;
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			getGameSessionInfoRequest.SetEntityId(this.m_battleNet.GameAccountId);
			AccountAPI.GetGameSessionInfoRequestContext @object = new AccountAPI.GetGameSessionInfoRequestContext(this);
			this.m_rpcConnection.QueueRequest(this.m_accountService.Id, 34u, getGameSessionInfoRequest, new RPCContextDelegate(@object.GetGameSessionInfoRequestContextCallback), 0u);
		}

		private void GetAccountLevelInfo(bnet.protocol.EntityId accountId)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			getAccountStateRequest.SetEntityId(accountId);
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetFieldAccountLevelInfo(true);
			getAccountStateRequest.SetOptions(accountFieldOptions);
			this.m_rpcConnection.QueueRequest(this.m_accountService.Id, 30u, getAccountStateRequest, new RPCContextDelegate(this.GetAccountStateCallback), 0u);
		}

		private void GetAccountStateCallback(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("GetAccountLevelInfo invalid context!");
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogError("GetAccountLevelInfo failed with error={0}", new object[]
				{
					status.ToString()
				});
				return;
			}
			GetAccountStateResponse getAccountStateResponse = GetAccountStateResponse.ParseFrom(context.Payload);
			if (getAccountStateResponse == null || !getAccountStateResponse.IsInitialized)
			{
				base.ApiLog.LogWarning("GetAccountStateCallback unable to parse response!");
				return;
			}
			if (!getAccountStateResponse.HasState || !getAccountStateResponse.State.HasAccountLevelInfo)
			{
				base.ApiLog.LogWarning("GetAccountStateCallback response has no data!");
				return;
			}
			GetAccountStateRequest getAccountStateRequest = (GetAccountStateRequest)context.Request;
			if (getAccountStateRequest != null && getAccountStateRequest.EntityId == this.m_battleNet.AccountId)
			{
				AccountLevelInfo accountLevelInfo = getAccountStateResponse.State.AccountLevelInfo;
				this.m_preferredRegion = accountLevelInfo.PreferredRegion;
				this.m_accountCountry = accountLevelInfo.Country;
				base.ApiLog.LogDebug("Region (preferred): {0}", new object[]
				{
					this.m_preferredRegion
				});
				base.ApiLog.LogDebug("Country (account): {0}", new object[]
				{
					this.m_accountCountry
				});
				if (accountLevelInfo.LicensesList.Count > 0)
				{
					this.m_licenses.Clear();
					base.ApiLog.LogDebug("Found {0} licenses.", new object[]
					{
						accountLevelInfo.LicensesList.Count
					});
					for (int i = 0; i < accountLevelInfo.LicensesList.Count; i++)
					{
						AccountLicense accountLicense = accountLevelInfo.LicensesList[i];
						this.m_licenses.Add(accountLicense);
						base.ApiLog.LogDebug("Adding license id={0}", new object[]
						{
							accountLicense.Id
						});
					}
				}
				else
				{
					base.ApiLog.LogWarning("No licenses found!");
				}
			}
			base.ApiLog.LogDebug("GetAccountLevelInfo, status=" + status.ToString());
		}

		private void SubscribeToAccountService()
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = new SubscriptionUpdateRequest();
			SubscriberReference subscriberReference = new SubscriberReference();
			subscriberReference.SetEntityId(this.m_battleNet.AccountId);
			subscriberReference.SetObjectId(0UL);
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetAllFields(true);
			subscriberReference.SetAccountOptions(accountFieldOptions);
			subscriptionUpdateRequest.AddRef(subscriberReference);
			subscriberReference = new SubscriberReference();
			subscriberReference.SetEntityId(this.m_battleNet.GameAccountId);
			subscriberReference.SetObjectId(0UL);
			GameAccountFieldOptions gameAccountFieldOptions = new GameAccountFieldOptions();
			gameAccountFieldOptions.SetAllFields(true);
			subscriptionUpdateRequest.AddRef(subscriberReference);
			this.m_rpcConnection.QueueRequest(this.m_accountService.Id, 25u, subscriptionUpdateRequest, new RPCContextDelegate(this.SubscribeToAccountServiceCallback), 0u);
		}

		private void SubscribeToAccountServiceCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogError("SubscribeToAccountServiceCallback: " + status.ToString());
				return;
			}
			base.ApiLog.LogDebug("SubscribeToAccountServiceCallback, status=" + status.ToString());
		}

		private void HandleAccountNotify_AccountStateUpdated(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleAccountNotify_AccountStateUpdated invalid context!");
				return;
			}
			AccountStateNotification accountStateNotification = AccountStateNotification.ParseFrom(context.Payload);
			if (accountStateNotification == null || !accountStateNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleAccountNotify_AccountStateUpdated unable to parse response!");
				return;
			}
			if (!accountStateNotification.HasState)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasState=false, data={0}", new object[]
				{
					accountStateNotification
				});
				return;
			}
			AccountState state = accountStateNotification.State;
			if (!state.HasAccountLevelInfo)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasAccountLevelInfo=false, data={0}", new object[]
				{
					accountStateNotification
				});
				return;
			}
			AccountLevelInfo accountLevelInfo = state.AccountLevelInfo;
			if (!accountLevelInfo.HasPreferredRegion)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasPreferredRegion=false, data={0}", new object[]
				{
					accountStateNotification
				});
				return;
			}
			base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated, data={0}", new object[]
			{
				accountStateNotification
			});
		}

		private void HandleAccountNotify_GameAccountStateUpdated(RPCContext context)
		{
			GameAccountStateNotification arg = GameAccountStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameAccountStateUpdated, data=" + arg);
		}

		private void HandleAccountNotify_GameAccountsUpdated(RPCContext context)
		{
			GameAccountNotification arg = GameAccountNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameAccountsUpdated, data=" + arg);
		}

		private void HandleAccountNotify_GameSessionUpdated(RPCContext context)
		{
			GameAccountSessionNotification arg = GameAccountSessionNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameSessionUpdated, data=" + arg);
		}

		private ServiceDescriptor m_accountService = new AccountService();

		private ServiceDescriptor m_accountNotify = new AccountNotify();

		private uint m_preferredRegion = uint.MaxValue;

		private string m_accountCountry;

		private List<AccountLicense> m_licenses = new List<AccountLicense>();

		public class GameSessionInfo
		{
			public ulong SessionStartTime;
		}

		private class GetGameSessionInfoRequestContext
		{
			public GetGameSessionInfoRequestContext(AccountAPI parent)
			{
				this.m_parent = parent;
			}

			public void GetGameSessionInfoRequestContextCallback(RPCContext context)
			{
				this.m_parent.GameSessionRunningCount--;
				if (context == null || context.Payload == null)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions invalid context!");
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != BattleNetErrors.ERROR_OK)
				{
					this.m_parent.ApiLog.LogError("GetPlayRestrictions failed with error={0}", new object[]
					{
						status.ToString()
					});
					return;
				}
				GetGameSessionInfoResponse getGameSessionInfoResponse = GetGameSessionInfoResponse.ParseFrom(context.Payload);
				if (getGameSessionInfoResponse == null || !getGameSessionInfoResponse.IsInitialized)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions unable to parse response!");
					return;
				}
				if (!getGameSessionInfoResponse.HasSessionInfo)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions response has no data!");
					return;
				}
				this.m_parent.LastGameSessionInfo = new AccountAPI.GameSessionInfo();
				if (getGameSessionInfoResponse.SessionInfo.HasStartTime)
				{
					this.m_parent.LastGameSessionInfo.SessionStartTime = (ulong)getGameSessionInfoResponse.SessionInfo.StartTime;
				}
				else
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions response has no HasStartTime!");
				}
			}

			private AccountAPI m_parent;
		}
	}
}
