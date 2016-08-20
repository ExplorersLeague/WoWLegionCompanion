using System;
using System.Collections.Generic;
using bnet.protocol.attribute;
using bnet.protocol.notification;

namespace bgs
{
	public class BroadcastAPI : BattleNetAPI
	{
		public BroadcastAPI(BattleNetCSharp battlenet) : base(battlenet, "Broadcast")
		{
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public void RegisterListener(BroadcastAPI.BroadcastCallback cb)
		{
			if (this.m_listenerList.Contains(cb))
			{
				return;
			}
			this.m_listenerList.Add(cb);
		}

		public void OnBroadcast(Notification notification)
		{
			foreach (BroadcastAPI.BroadcastCallback broadcastCallback in this.m_listenerList)
			{
				broadcastCallback(notification.AttributeList);
			}
		}

		private List<BroadcastAPI.BroadcastCallback> m_listenerList = new List<BroadcastAPI.BroadcastCallback>();

		public delegate void BroadcastCallback(IList<bnet.protocol.attribute.Attribute> AttributeList);
	}
}
