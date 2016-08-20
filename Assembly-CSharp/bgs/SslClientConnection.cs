using System;
using System.Collections.Generic;

namespace bgs
{
	public class SslClientConnection : IClientConnection<BattleNetPacket>
	{
		public SslClientConnection(SslCertBundleSettings bundleSettings)
		{
			this.m_connectionState = SslClientConnection.ConnectionState.Disconnected;
			this.m_receiveBuffer = new byte[SslClientConnection.RECEIVE_BUFFER_SIZE];
			this.m_backingBuffer = new byte[SslClientConnection.BACKING_BUFFER_SIZE];
			this.m_bundleSettings = bundleSettings;
		}

		public void Connect(string host, int port)
		{
			this.m_hostAddress = host;
			this.m_hostPort = port;
			this.Disconnect();
			this.m_sslSocket = new SslSocket();
			this.m_connectionState = SslClientConnection.ConnectionState.Connecting;
			try
			{
				this.m_sslSocket.BeginConnect(host, port, this.m_bundleSettings, new SslSocket.BeginConnectDelegate(this.ConnectCallback));
			}
			catch (Exception ex)
			{
				string str = this.m_hostAddress + ":" + this.m_hostPort;
				LogAdapter.Log(LogLevel.Warning, "Could not connect to " + str + " -- " + ex.Message);
				this.m_connectionState = SslClientConnection.ConnectionState.ConnectionFailed;
				this.TriggerOnConnectHandler(BattleNetErrors.ERROR_RPC_PEER_UNKNOWN);
			}
			this.m_bundleSettings = null;
		}

		public void Disconnect()
		{
			if (this.m_sslSocket != null)
			{
				this.m_sslSocket.Close();
				this.m_sslSocket = null;
			}
			this.m_connectionState = SslClientConnection.ConnectionState.Disconnected;
		}

		public bool AddConnectHandler(ConnectHandler handler)
		{
			if (this.m_connectHandlers.Contains(handler))
			{
				return false;
			}
			this.m_connectHandlers.Add(handler);
			return true;
		}

		public bool RemoveConnectHandler(ConnectHandler handler)
		{
			return this.m_connectHandlers.Remove(handler);
		}

		public bool AddDisconnectHandler(DisconnectHandler handler)
		{
			if (this.m_disconnectHandlers.Contains(handler))
			{
				return false;
			}
			this.m_disconnectHandlers.Add(handler);
			return true;
		}

		public bool RemoveDisconnectHandler(DisconnectHandler handler)
		{
			return this.m_disconnectHandlers.Remove(handler);
		}

		private void SendBytes(byte[] bytes)
		{
			if (bytes.Length > 0)
			{
				bool block = this.BlockOnSend;
				object blockLock = new object();
				this.m_sslSocket.BeginSend(bytes, delegate(bool wasSent)
				{
					if (!wasSent)
					{
						this.TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT);
					}
					object blockLock2 = blockLock;
					lock (blockLock2)
					{
						block = false;
					}
				});
				if (this.BlockOnSend)
				{
					float num = (float)BattleNet.GetRealTimeSinceStartup();
					while ((float)BattleNet.GetRealTimeSinceStartup() - num < 1f)
					{
						object blockLock3 = blockLock;
						lock (blockLock3)
						{
							if (!block)
							{
								break;
							}
						}
					}
				}
			}
		}

		public void SendPacket(BattleNetPacket packet)
		{
			byte[] bytes = packet.Encode();
			this.SendBytes(bytes);
		}

		public void QueuePacket(BattleNetPacket packet)
		{
			this.m_outQueue.Enqueue(packet);
		}

		public void AddListener(IClientConnectionListener<BattleNetPacket> listener, object state)
		{
			this.m_listeners.Add(listener);
			this.m_listenerStates.Add(state);
		}

		public void RemoveListener(IClientConnectionListener<BattleNetPacket> listener)
		{
			while (this.m_listeners.Remove(listener))
			{
			}
		}

		public void Update()
		{
			SslSocket.Process();
			List<SslClientConnection.ConnectionEvent> connectionEvents = this.m_connectionEvents;
			lock (connectionEvents)
			{
				foreach (SslClientConnection.ConnectionEvent connectionEvent in this.m_connectionEvents)
				{
					switch (connectionEvent.Type)
					{
					case SslClientConnection.ConnectionEventTypes.OnConnected:
						if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
						{
							this.Disconnect();
							this.m_connectionState = SslClientConnection.ConnectionState.ConnectionFailed;
						}
						else
						{
							this.m_connectionState = SslClientConnection.ConnectionState.Connected;
						}
						foreach (ConnectHandler connectHandler in this.m_connectHandlers.ToArray())
						{
							connectHandler(connectionEvent.Error);
						}
						break;
					case SslClientConnection.ConnectionEventTypes.OnDisconnected:
						if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
						{
							this.Disconnect();
						}
						foreach (DisconnectHandler disconnectHandler in this.m_disconnectHandlers.ToArray())
						{
							disconnectHandler(connectionEvent.Error);
						}
						break;
					case SslClientConnection.ConnectionEventTypes.OnPacketCompleted:
						for (int k = 0; k < this.m_listeners.Count; k++)
						{
							IClientConnectionListener<BattleNetPacket> clientConnectionListener = this.m_listeners[k];
							object state = this.m_listenerStates[k];
							clientConnectionListener.PacketReceived(connectionEvent.Packet, state);
						}
						break;
					}
				}
				this.m_connectionEvents.Clear();
			}
			if (this.m_sslSocket == null || this.m_connectionState != SslClientConnection.ConnectionState.Connected)
			{
				return;
			}
			while (this.m_outQueue.Count > 0)
			{
				if (this.OnlyOneSend && !this.m_sslSocket.m_canSend)
				{
					return;
				}
				BattleNetPacket packet = this.m_outQueue.Dequeue();
				this.SendPacket(packet);
			}
		}

		public bool Active
		{
			get
			{
				return this.m_sslSocket.Connected;
			}
		}

		public bool BlockOnSend { get; set; }

		public bool OnlyOneSend { get; set; }

		~SslClientConnection()
		{
			if (this.m_sslSocket != null)
			{
				this.Disconnect();
			}
		}

		private void TriggerOnConnectHandler(BattleNetErrors error)
		{
			SslClientConnection.ConnectionEvent connectionEvent = new SslClientConnection.ConnectionEvent();
			connectionEvent.Type = SslClientConnection.ConnectionEventTypes.OnConnected;
			connectionEvent.Error = error;
			List<SslClientConnection.ConnectionEvent> connectionEvents = this.m_connectionEvents;
			lock (connectionEvents)
			{
				this.m_connectionEvents.Add(connectionEvent);
			}
		}

		private void TriggerOnDisconnectHandler(BattleNetErrors error)
		{
			SslClientConnection.ConnectionEvent connectionEvent = new SslClientConnection.ConnectionEvent();
			connectionEvent.Type = SslClientConnection.ConnectionEventTypes.OnDisconnected;
			connectionEvent.Error = error;
			List<SslClientConnection.ConnectionEvent> connectionEvents = this.m_connectionEvents;
			lock (connectionEvents)
			{
				this.m_connectionEvents.Add(connectionEvent);
			}
		}

		private void ConnectCallback(bool connectFailed, bool isEncrypted, bool isSigned)
		{
			if (!connectFailed)
			{
				try
				{
					this.m_sslSocket.BeginReceive(this.m_receiveBuffer, SslClientConnection.RECEIVE_BUFFER_SIZE, new SslSocket.BeginReceiveDelegate(this.ReceiveCallback));
				}
				catch (Exception)
				{
					connectFailed = true;
				}
			}
			if (connectFailed || !this.m_sslSocket.Connected)
			{
				this.TriggerOnConnectHandler(BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE);
			}
			else
			{
				this.TriggerOnConnectHandler(BattleNetErrors.ERROR_OK);
			}
		}

		private void BytesReceived(byte[] bytes, int nBytes, int offset)
		{
			while (nBytes > 0)
			{
				if (this.m_currentPacket == null)
				{
					this.m_currentPacket = new BattleNetPacket();
				}
				int num = this.m_currentPacket.Decode(bytes, offset, nBytes);
				nBytes -= num;
				offset += num;
				if (!this.m_currentPacket.IsLoaded())
				{
					Array.Copy(bytes, offset, this.m_backingBuffer, 0, nBytes);
					this.m_backingBufferBytes = nBytes;
					return;
				}
				SslClientConnection.ConnectionEvent connectionEvent = new SslClientConnection.ConnectionEvent();
				connectionEvent.Type = SslClientConnection.ConnectionEventTypes.OnPacketCompleted;
				connectionEvent.Packet = this.m_currentPacket;
				List<SslClientConnection.ConnectionEvent> connectionEvents = this.m_connectionEvents;
				lock (connectionEvents)
				{
					this.m_connectionEvents.Add(connectionEvent);
				}
				this.m_currentPacket = null;
			}
			this.m_backingBufferBytes = 0;
		}

		private void BytesReceived(int nBytes)
		{
			if (this.m_backingBufferBytes > 0)
			{
				int num = this.m_backingBufferBytes + nBytes;
				if (num > this.m_backingBuffer.Length)
				{
					int num2 = (num + SslClientConnection.BACKING_BUFFER_SIZE - 1) / SslClientConnection.BACKING_BUFFER_SIZE;
					byte[] array = new byte[num2 * SslClientConnection.BACKING_BUFFER_SIZE];
					Array.Copy(this.m_backingBuffer, 0, array, 0, this.m_backingBuffer.Length);
					this.m_backingBuffer = array;
				}
				Array.Copy(this.m_receiveBuffer, 0, this.m_backingBuffer, this.m_backingBufferBytes, nBytes);
				this.m_backingBufferBytes = 0;
				this.BytesReceived(this.m_backingBuffer, num, 0);
			}
			else
			{
				this.BytesReceived(this.m_receiveBuffer, nBytes, 0);
			}
		}

		private void ReceiveCallback(int bytesReceived)
		{
			if (bytesReceived == 0 || !this.m_sslSocket.Connected)
			{
				this.TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
				return;
			}
			if (this.m_sslSocket != null && this.m_sslSocket.Connected)
			{
				try
				{
					if (bytesReceived > 0)
					{
						this.BytesReceived(bytesReceived);
						this.m_sslSocket.BeginReceive(this.m_receiveBuffer, SslClientConnection.RECEIVE_BUFFER_SIZE, new SslSocket.BeginReceiveDelegate(this.ReceiveCallback));
					}
				}
				catch (Exception)
				{
					this.TriggerOnDisconnectHandler(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
				}
			}
		}

		private const float BLOCKING_SEND_TIME_OUT = 1f;

		private static int RECEIVE_BUFFER_SIZE = 262144;

		private static int BACKING_BUFFER_SIZE = 262144;

		private SslClientConnection.ConnectionState m_connectionState;

		private SslSocket m_sslSocket;

		private byte[] m_receiveBuffer;

		private byte[] m_backingBuffer;

		private int m_backingBufferBytes;

		private Queue<BattleNetPacket> m_outQueue = new Queue<BattleNetPacket>();

		private string m_hostAddress;

		private int m_hostPort;

		private BattleNetPacket m_currentPacket;

		private SslCertBundleSettings m_bundleSettings;

		private List<IClientConnectionListener<BattleNetPacket>> m_listeners = new List<IClientConnectionListener<BattleNetPacket>>();

		private List<object> m_listenerStates = new List<object>();

		private List<SslClientConnection.ConnectionEvent> m_connectionEvents = new List<SslClientConnection.ConnectionEvent>();

		private List<ConnectHandler> m_connectHandlers = new List<ConnectHandler>();

		private List<DisconnectHandler> m_disconnectHandlers = new List<DisconnectHandler>();

		private enum ConnectionState
		{
			Disconnected,
			Connecting,
			ConnectionFailed,
			Connected
		}

		private enum ConnectionEventTypes
		{
			OnConnected,
			OnDisconnected,
			OnPacketCompleted
		}

		private class ConnectionEvent
		{
			public SslClientConnection.ConnectionEventTypes Type { get; set; }

			public BattleNetErrors Error { get; set; }

			public BattleNetPacket Packet { get; set; }
		}
	}
}
