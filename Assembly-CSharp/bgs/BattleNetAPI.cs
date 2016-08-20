using System;

namespace bgs
{
	public class BattleNetAPI
	{
		protected BattleNetAPI(BattleNetCSharp battlenet, string logSourceName)
		{
			this.m_battleNet = battlenet;
			this.m_logSource = new BattleNetLogSource(logSourceName);
			this.m_logDelegates = new BattleNetAPI.LogDelegate[]
			{
				new BattleNetAPI.LogDelegate(this.m_logSource.LogDebug),
				new BattleNetAPI.LogDelegate(this.m_logSource.LogError)
			};
		}

		public virtual void Initialize()
		{
		}

		public virtual void InitRPCListeners(RPCConnection rpcConnection)
		{
			this.m_rpcConnection = rpcConnection;
		}

		public virtual void OnConnected(BattleNetErrors error)
		{
		}

		public virtual void OnDisconnected()
		{
		}

		public virtual void OnLogon()
		{
		}

		public virtual void OnGameAccountSelected()
		{
		}

		public virtual void Process()
		{
		}

		public BattleNetLogSource ApiLog
		{
			get
			{
				return this.m_logSource;
			}
		}

		public bool CheckRPCCallback(string name, RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			int num = (status != BattleNetErrors.ERROR_OK) ? 1 : 0;
			this.m_logDelegates[num]("Callback invoked, status = {0}, name = {1}, request = {2}", new object[]
			{
				status,
				(!string.IsNullOrEmpty(name)) ? name : "<null>",
				(context.Request == null) ? "<null>" : context.Request.ToString()
			});
			return status == BattleNetErrors.ERROR_OK;
		}

		private BattleNetAPI.LogDelegate[] m_logDelegates;

		protected BattleNetCSharp m_battleNet;

		protected RPCConnection m_rpcConnection;

		public BattleNetLogSource m_logSource;

		private delegate void LogDelegate(string format, params object[] args);
	}
}
