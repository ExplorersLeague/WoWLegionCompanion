using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace bgs
{
	public class ClientConnection<PacketType> : IClientConnection<PacketType> where PacketType : PacketFormat, new()
	{
		public ClientConnection()
		{
			this.m_connectionState = ClientConnection<PacketType>.ConnectionState.Disconnected;
			this.m_receiveBuffer = new byte[ClientConnection<PacketType>.RECEIVE_BUFFER_SIZE];
			this.m_backingBuffer = new byte[ClientConnection<PacketType>.BACKING_BUFFER_SIZE];
			this.m_stolenSocket = false;
		}

		public ClientConnection(Socket socket)
		{
			this.m_socket = socket;
			this.m_connectionState = ClientConnection<PacketType>.ConnectionState.Connected;
			this.m_receiveBuffer = new byte[ClientConnection<PacketType>.RECEIVE_BUFFER_SIZE];
			this.m_stolenSocket = true;
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

		public bool HasEvents()
		{
			return this.m_connectionEvents.Count > 0;
		}

		private void AddConnectEvent(BattleNetErrors error, Exception exception = null)
		{
			ClientConnection<PacketType>.ConnectionEvent connectionEvent = new ClientConnection<PacketType>.ConnectionEvent();
			connectionEvent.Type = ClientConnection<PacketType>.ConnectionEventTypes.OnConnected;
			connectionEvent.Error = error;
			connectionEvent.Exception = exception;
			object mutex = this.m_mutex;
			lock (mutex)
			{
				this.m_connectionEvents.Add(connectionEvent);
			}
		}

		private void AddDisconnectEvent(BattleNetErrors error)
		{
			ClientConnection<PacketType>.ConnectionEvent connectionEvent = new ClientConnection<PacketType>.ConnectionEvent();
			connectionEvent.Type = ClientConnection<PacketType>.ConnectionEventTypes.OnDisconnected;
			connectionEvent.Error = error;
			object mutex = this.m_mutex;
			lock (mutex)
			{
				this.m_connectionEvents.Add(connectionEvent);
			}
		}

		public bool Active
		{
			get
			{
				return this.m_connectionState == ClientConnection<PacketType>.ConnectionState.Connecting || this.m_connectionState == ClientConnection<PacketType>.ConnectionState.Connected;
			}
		}

		public ClientConnection<PacketType>.ConnectionState State
		{
			get
			{
				return this.m_connectionState;
			}
		}

		~ClientConnection()
		{
			this.DisconnectSocket();
		}

		public void Connect(string host, int port)
		{
			this.m_connection.LogDebug = delegate(string log)
			{
				LogAdapter.Log(LogLevel.Debug, log);
			};
			this.m_connection.LogWarning = delegate(string log)
			{
				LogAdapter.Log(LogLevel.Warning, log);
			};
			this.m_connection.OnFailure = delegate
			{
				this.m_connectionState = ClientConnection<PacketType>.ConnectionState.ConnectionFailed;
				this.AddConnectEvent(BattleNetErrors.ERROR_RPC_PEER_UNKNOWN, null);
			};
			this.m_connection.OnSuccess = new Action(this.ConnectCallback);
			this.m_connectionState = ClientConnection<PacketType>.ConnectionState.Connecting;
			this.m_connection.Connect(host, port);
		}

		private void ConnectCallback()
		{
			Exception ex = null;
			this.m_socket = this.m_connection.Socket;
			try
			{
				this.m_socket.BeginReceive(this.m_receiveBuffer, 0, ClientConnection<PacketType>.RECEIVE_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null || !this.m_socket.Connected)
			{
				LogAdapter.Log(LogLevel.Warning, string.Format("ClientConnection - BeginReceive() failed. ip:{0}, port:{1}, exception:{3}", this.m_connection.Host, this.m_connection.Port, ex.Message));
				this.DisconnectSocket();
				this.m_connectionState = ClientConnection<PacketType>.ConnectionState.ConnectionFailed;
				this.AddConnectEvent(BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE, ex);
			}
			else
			{
				this.AddConnectEvent(BattleNetErrors.ERROR_OK, null);
			}
		}

		public void Disconnect()
		{
			this.DisconnectSocket();
			this.m_connectionState = ClientConnection<PacketType>.ConnectionState.Disconnected;
		}

		private void DisconnectSocket()
		{
			if (this.m_socket == null)
			{
				return;
			}
			try
			{
				if (this.m_socket.Connected)
				{
					this.m_socket.Shutdown(SocketShutdown.Both);
					this.m_socket.Close();
				}
			}
			catch (Exception ex)
			{
				LogAdapter.Log(LogLevel.Warning, string.Format("DisconnectSocket() failed. error: {0},", ex.Message));
				if (ex is SocketException)
				{
					SocketException ex2 = (SocketException)ex;
					LogAdapter.Log(LogLevel.Warning, string.Format("\t Socket Error Code: {0},", ex2.ErrorCode));
				}
			}
			this.m_socket = null;
		}

		public void StartReceiving()
		{
			if (!this.m_stolenSocket)
			{
				LogAdapter.Log(LogLevel.Error, "StartReceiving should only be called on sockets created with ClientConnection(Socket)");
				return;
			}
			try
			{
				this.m_socket.BeginReceive(this.m_receiveBuffer, 0, ClientConnection<PacketType>.RECEIVE_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
			}
			catch (Exception ex)
			{
				LogAdapter.Log(LogLevel.Error, "error receiving from local connection: " + ex.Message);
			}
		}

		private void BytesReceived(byte[] bytes, int nBytes, int offset)
		{
			while (nBytes > 0)
			{
				if (this.m_currentPacket == null)
				{
					this.m_currentPacket = Activator.CreateInstance<PacketType>();
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
				ClientConnection<PacketType>.ConnectionEvent connectionEvent = new ClientConnection<PacketType>.ConnectionEvent();
				connectionEvent.Type = ClientConnection<PacketType>.ConnectionEventTypes.OnPacketCompleted;
				connectionEvent.Packet = this.m_currentPacket;
				object mutex = this.m_mutex;
				lock (mutex)
				{
					this.m_connectionEvents.Add(connectionEvent);
				}
				this.m_currentPacket = (PacketType)((object)null);
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
					int num2 = (num + ClientConnection<PacketType>.BACKING_BUFFER_SIZE - 1) / ClientConnection<PacketType>.BACKING_BUFFER_SIZE;
					byte[] array = new byte[num2 * ClientConnection<PacketType>.BACKING_BUFFER_SIZE];
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

		private void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				if (this.m_socket != null && this.m_socket.Connected)
				{
					int num = this.m_socket.EndReceive(ar);
					if (num > 0)
					{
						this.BytesReceived(num);
						this.m_socket.BeginReceive(this.m_receiveBuffer, 0, ClientConnection<PacketType>.RECEIVE_BUFFER_SIZE, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
						return;
					}
				}
				this.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
			}
			catch (Exception ex)
			{
				LogAdapter.Log(LogLevel.Debug, ex.ToString());
				this.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED);
			}
		}

		public bool SendBytes(byte[] bytes, AsyncCallback callback, object userData)
		{
			if (bytes.Length == 0)
			{
				return false;
			}
			if (this.m_socket == null || !this.m_socket.Connected)
			{
				return false;
			}
			bool result = false;
			try
			{
				this.m_socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, callback, userData);
				result = true;
			}
			catch (Exception)
			{
			}
			return result;
		}

		private void OnSendBytes(IAsyncResult ar)
		{
			try
			{
				this.m_socket.EndSend(ar);
			}
			catch (Exception)
			{
				this.AddDisconnectEvent(BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT);
			}
		}

		public bool SendString(string str)
		{
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			byte[] bytes = asciiencoding.GetBytes(str);
			return this.SendBytes(bytes, new AsyncCallback(this.OnSendBytes), null);
		}

		public bool SendPacket(PacketType packet)
		{
			byte[] array = packet.Encode();
			if (array.Length == 0)
			{
				return false;
			}
			if (this.m_socket == null || !this.m_socket.Connected)
			{
				return false;
			}
			object mutex = this.m_mutex;
			lock (mutex)
			{
				this.m_outPacketsInFlight++;
			}
			bool result = false;
			try
			{
				this.m_socket.BeginSend(array, 0, array.Length, SocketFlags.None, new AsyncCallback(this.OnSendPacket), null);
				result = true;
			}
			catch (Exception)
			{
				object mutex2 = this.m_mutex;
				lock (mutex2)
				{
					this.m_outPacketsInFlight--;
				}
			}
			return result;
		}

		private void OnSendPacket(IAsyncResult ar)
		{
			this.OnSendBytes(ar);
			object mutex = this.m_mutex;
			lock (mutex)
			{
				this.m_outPacketsInFlight--;
			}
		}

		public void QueuePacket(PacketType packet)
		{
			this.m_outQueue.Enqueue(packet);
		}

		public bool HasQueuedPackets()
		{
			return this.m_outQueue.Count > 0;
		}

		public void SendQueuedPackets()
		{
			while (this.m_outQueue.Count > 0)
			{
				PacketType packet = this.m_outQueue.Dequeue();
				this.SendPacket(packet);
			}
		}

		public bool HasOutPacketsInFlight()
		{
			return this.m_outPacketsInFlight > 0;
		}

		public void AddListener(IClientConnectionListener<PacketType> listener, object state)
		{
			this.m_listeners.Add(listener);
			this.m_listenerStates.Add(state);
		}

		public void RemoveListener(IClientConnectionListener<PacketType> listener)
		{
			while (this.m_listeners.Remove(listener))
			{
			}
		}

		public void Update()
		{
			object mutex = this.m_mutex;
			lock (mutex)
			{
				foreach (ClientConnection<PacketType>.ConnectionEvent connectionEvent in this.m_connectionEvents)
				{
					this.PrintConnectionException(connectionEvent);
					ClientConnection<PacketType>.ConnectionEventTypes type = connectionEvent.Type;
					if (type != ClientConnection<PacketType>.ConnectionEventTypes.OnConnected)
					{
						if (type != ClientConnection<PacketType>.ConnectionEventTypes.OnDisconnected)
						{
							if (type == ClientConnection<PacketType>.ConnectionEventTypes.OnPacketCompleted)
							{
								for (int i = 0; i < this.m_listeners.Count; i++)
								{
									IClientConnectionListener<PacketType> clientConnectionListener = this.m_listeners[i];
									object state = this.m_listenerStates[i];
									clientConnectionListener.PacketReceived(connectionEvent.Packet, state);
								}
							}
						}
						else
						{
							if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
							{
								this.Disconnect();
							}
							foreach (DisconnectHandler disconnectHandler in this.m_disconnectHandlers.ToArray())
							{
								disconnectHandler(connectionEvent.Error);
							}
						}
					}
					else
					{
						if (connectionEvent.Error != BattleNetErrors.ERROR_OK)
						{
							this.DisconnectSocket();
							this.m_connectionState = ClientConnection<PacketType>.ConnectionState.ConnectionFailed;
						}
						else
						{
							this.m_connectionState = ClientConnection<PacketType>.ConnectionState.Connected;
						}
						foreach (ConnectHandler connectHandler in this.m_connectHandlers.ToArray())
						{
							connectHandler(connectionEvent.Error);
						}
					}
				}
				this.m_connectionEvents.Clear();
			}
			if (this.m_socket == null || this.m_connectionState != ClientConnection<PacketType>.ConnectionState.Connected)
			{
				return;
			}
			this.SendQueuedPackets();
		}

		private void PrintConnectionException(ClientConnection<PacketType>.ConnectionEvent connectionEvent)
		{
			Exception exception = connectionEvent.Exception;
			if (exception == null)
			{
				return;
			}
			LogAdapter.Log(LogLevel.Error, string.Format("ClientConnection Exception - {0} - {1}:{2}\n{3}", new object[]
			{
				exception.Message,
				this.m_connection.Host,
				this.m_connection.Port,
				exception.StackTrace
			}));
		}

		private List<ConnectHandler> m_connectHandlers = new List<ConnectHandler>();

		private List<DisconnectHandler> m_disconnectHandlers = new List<DisconnectHandler>();

		private static int RECEIVE_BUFFER_SIZE = 65536;

		private static int BACKING_BUFFER_SIZE = 262144;

		private bool m_stolenSocket;

		private ClientConnection<PacketType>.ConnectionState m_connectionState;

		private Socket m_socket;

		private byte[] m_receiveBuffer;

		private byte[] m_backingBuffer;

		private int m_backingBufferBytes;

		private Queue<PacketType> m_outQueue = new Queue<PacketType>();

		private int m_outPacketsInFlight;

		private TcpConnection m_connection = new TcpConnection();

		private PacketType m_currentPacket;

		private List<IClientConnectionListener<PacketType>> m_listeners = new List<IClientConnectionListener<PacketType>>();

		private List<object> m_listenerStates = new List<object>();

		private List<ClientConnection<PacketType>.ConnectionEvent> m_connectionEvents = new List<ClientConnection<PacketType>.ConnectionEvent>();

		private object m_mutex = new object();

		public enum ConnectionState
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
			public ClientConnection<PacketType>.ConnectionEventTypes Type { get; set; }

			public BattleNetErrors Error { get; set; }

			public PacketType Packet { get; set; }

			public Exception Exception { get; set; }
		}
	}
}
