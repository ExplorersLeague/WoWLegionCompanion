using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.authentication;

namespace bgs
{
	public class AuthenticationAPI : BattleNetAPI
	{
		public AuthenticationAPI(BattleNetCSharp battlenet) : base(battlenet, "Authentication")
		{
		}

		public ServiceDescriptor AuthServerService
		{
			get
			{
				return this.m_authServerService;
			}
		}

		public ServiceDescriptor AuthClientService
		{
			get
			{
				return this.m_authClientService;
			}
		}

		public bnet.protocol.EntityId GameAccountId
		{
			get
			{
				return this.m_gameAccount;
			}
		}

		public byte[] SessionKey
		{
			get
			{
				return this.m_sessionKey;
			}
		}

		public bnet.protocol.EntityId AccountId
		{
			get
			{
				return this.m_accountEntity;
			}
		}

		public void GetQueueInfo(ref QueueInfo queueInfo)
		{
			queueInfo.position = this.m_queueInfo.position;
			queueInfo.end = this.m_queueInfo.end;
			queueInfo.stdev = this.m_queueInfo.stdev;
			queueInfo.changed = this.m_queueInfo.changed;
			this.m_queueInfo.changed = false;
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 5u, new RPCContextDelegate(this.HandleLogonCompleteRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 10u, new RPCContextDelegate(this.HandleLogonUpdateRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 1u, new RPCContextDelegate(this.HandleLoadModuleRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 12u, new RPCContextDelegate(this.HandleLogonQueueUpdate));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 13u, new RPCContextDelegate(this.HandleLogonQueueEnd));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_authClientService.Id, 14u, new RPCContextDelegate(this.HandleGameAccountSelected));
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public bool AuthenticationFailure()
		{
			return this.m_authenticationFailure;
		}

		public void VerifyWebCredentials(string token)
		{
			if (this.m_rpcConnection == null)
			{
				return;
			}
			VerifyWebCredentialsRequest verifyWebCredentialsRequest = new VerifyWebCredentialsRequest();
			byte[] bytes = Encoding.UTF8.GetBytes(token);
			verifyWebCredentialsRequest.SetWebCredentials(bytes);
			this.m_rpcConnection.BeginAuth();
			this.m_rpcConnection.QueueRequest(this.AuthClientService.Id, 7u, verifyWebCredentialsRequest, null, 0u);
		}

		public List<bnet.protocol.EntityId> GetGameAccountList()
		{
			return this.m_gameAccounts;
		}

		private void HandleLogonCompleteRequest(RPCContext context)
		{
			byte[] payload = context.Payload;
			LogonResult logonResult = LogonResult.ParseFrom(payload);
			BattleNetErrors errorCode = (BattleNetErrors)logonResult.ErrorCode;
			if (errorCode != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_OnFinish, errorCode, 0);
				return;
			}
			this.m_accountEntity = logonResult.Account;
			this.m_battleNet.Presence.PresenceSubscribe(this.m_accountEntity);
			this.m_gameAccounts = new List<bnet.protocol.EntityId>();
			foreach (bnet.protocol.EntityId entityId in logonResult.GameAccountList)
			{
				this.m_gameAccounts.Add(entityId);
				this.m_battleNet.Presence.PresenceSubscribe(entityId);
			}
			if (this.m_gameAccounts.Count > 0)
			{
				this.m_gameAccount = logonResult.GameAccountList[0];
			}
			this.m_sessionKey = logonResult.SessionKey;
			this.m_battleNet.IssueSelectGameAccountRequest();
			this.m_battleNet.SetConnectedRegion(logonResult.ConnectedRegion);
			base.ApiLog.LogDebug("LogonComplete {0}", new object[]
			{
				logonResult
			});
			base.ApiLog.LogDebug("Region (connected): {0}", new object[]
			{
				logonResult.ConnectedRegion
			});
		}

		private void HandleLogonUpdateRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: LogonUpdate");
		}

		private void HandleLoadModuleRequest(RPCContext context)
		{
			base.ApiLog.LogWarning("RPC Called: LoadModule");
			this.m_authenticationFailure = true;
		}

		private void HandleLogonQueueUpdate(RPCContext context)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = LogonQueueUpdateRequest.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleLogonQueueUpdate : " + logonQueueUpdateRequest.ToString());
			long end = (long)((logonQueueUpdateRequest.EstimatedTime - (ulong)this.m_battleNet.ServerTimeUTCAtConnectMicroseconds) / 1000000UL);
			this.SaveQueuePosition((int)logonQueueUpdateRequest.Position, end, (long)logonQueueUpdateRequest.EtaDeviationInSec, false);
		}

		private void HandleLogonQueueEnd(RPCContext context)
		{
			base.ApiLog.LogDebug("HandleLogonQueueEnd : ");
			this.SaveQueuePosition(0, 0L, 0L, true);
		}

		private void HandleGameAccountSelected(RPCContext context)
		{
			GameAccountSelectedRequest gameAccountSelectedRequest = GameAccountSelectedRequest.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleGameAccountSelected : " + gameAccountSelectedRequest.ToString());
		}

		public void SaveQueuePosition(int position, long end, long stdev, bool ended)
		{
			this.m_queueInfo.changed = (ended || position != this.m_queueInfo.position || end != this.m_queueInfo.end || stdev != this.m_queueInfo.stdev);
			this.m_queueInfo.position = position;
			this.m_queueInfo.end = end;
			this.m_queueInfo.stdev = stdev;
		}

		private ServiceDescriptor m_authServerService = new AuthServerService();

		private ServiceDescriptor m_authClientService = new AuthClientService();

		private QueueInfo m_queueInfo;

		private List<bnet.protocol.EntityId> m_gameAccounts;

		private bnet.protocol.EntityId m_accountEntity;

		private bnet.protocol.EntityId m_gameAccount;

		private byte[] m_sessionKey;

		private bool m_authenticationFailure;
	}
}
