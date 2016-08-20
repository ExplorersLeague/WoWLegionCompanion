using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace bgs
{
	public class TcpConnection
	{
		public string Host { get; private set; }

		public int Port { get; private set; }

		public IPAddress ResolvedAddress
		{
			get
			{
				return this.m_resolvedIPAddress;
			}
		}

		public Socket Socket
		{
			get
			{
				return this.m_socket;
			}
		}

		public void Connect(string host, int port)
		{
			this.LogWarning(string.Format("TcpConnection - Connecting to host: {0}, port: {1}", host, port));
			this.Host = host;
			this.Port = port;
			this.m_candidateIPAddresses = new Queue<IPAddress>();
			IPAddress item;
			if (IPAddress.TryParse(this.Host, out item))
			{
				this.m_candidateIPAddresses.Enqueue(item);
			}
			try
			{
				Dns.BeginGetHostByName(this.Host, new AsyncCallback(this.GetHostEntryCallback), null);
			}
			catch (Exception ex)
			{
				this.LogWarning(string.Format("TcpConnection - Connect() failed, could not get host entry. ip: {0}, port: {1}, exception: {2}", this.Host, this.Port, ex.Message));
				this.OnFailure();
			}
		}

		private void GetHostEntryCallback(IAsyncResult ar)
		{
			IPHostEntry iphostEntry = Dns.EndGetHostByName(ar);
			Array.Sort<IPAddress>(iphostEntry.AddressList, delegate(IPAddress x, IPAddress y)
			{
				if (x.AddressFamily < y.AddressFamily)
				{
					return -1;
				}
				if (x.AddressFamily > y.AddressFamily)
				{
					return 1;
				}
				return 0;
			});
			foreach (IPAddress item in iphostEntry.AddressList)
			{
				this.m_candidateIPAddresses.Enqueue(item);
			}
			this.ConnectInternal();
		}

		private void ConnectInternal()
		{
			this.LogDebug(string.Format("TcpConnection - ConnectInternal. address-count: {0}", this.m_candidateIPAddresses.Count));
			this.Disconnect();
			if (this.m_candidateIPAddresses.Count == 0)
			{
				this.LogWarning(string.Format("TcpConnection - Could not connect to ip: {0}, port: {1}", this.Host, this.Port));
				this.OnFailure();
				return;
			}
			this.m_resolvedIPAddress = this.m_candidateIPAddresses.Dequeue();
			IPEndPoint end_point = new IPEndPoint(this.m_resolvedIPAddress, this.Port);
			this.LogDebug(string.Format("TcpConnection - Create Socket with ip: {0}, port: {1}, af: {2}", this.m_resolvedIPAddress, this.Port, this.m_resolvedIPAddress.AddressFamily));
			this.m_socket = new Socket(this.m_resolvedIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				this.m_socket.BeginConnect(end_point, new AsyncCallback(this.ConnectCallback), null);
			}
			catch (Exception ex)
			{
				this.LogDebug(string.Format("TcpConnection - BeginConnect() failed. ip: {0}, port: {1}, af: {2}, exception: {3}", new object[]
				{
					this.m_resolvedIPAddress,
					this.Port,
					this.m_resolvedIPAddress.AddressFamily,
					ex.Message
				}));
				this.ConnectInternal();
			}
		}

		private void ConnectCallback(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				this.m_socket.EndConnect(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null || !this.m_socket.Connected)
			{
				this.LogDebug(string.Format("TcpConnection - EndConnect() failed. ip: {0}, port: {1}, af: {2}, exception: {3}", new object[]
				{
					this.m_resolvedIPAddress,
					this.Port,
					this.m_resolvedIPAddress.AddressFamily,
					ex.Message
				}));
				this.ConnectInternal();
			}
			else
			{
				this.LogDebug(string.Format("TcpConnection - Connected to ip: {0}, port: {1}, af: {2}", this.m_resolvedIPAddress, this.Port, this.m_resolvedIPAddress.AddressFamily));
				this.OnSuccess();
			}
		}

		public bool MatchSslCertName(IEnumerable<string> certNames)
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(this.Host);
			foreach (string text in certNames)
			{
				if (text.StartsWith("::ffff:"))
				{
					string hostNameOrAddress = text.Substring("::ffff:".Length);
					IPHostEntry hostEntry2 = Dns.GetHostEntry(hostNameOrAddress);
					foreach (IPAddress comparand in hostEntry2.AddressList)
					{
						foreach (IPAddress ipaddress in hostEntry.AddressList)
						{
							if (ipaddress.Equals(comparand))
							{
								return true;
							}
						}
					}
				}
			}
			string text2 = string.Format("TcpConnection - MatchSslCertName failed.", new object[0]);
			foreach (string arg in certNames)
			{
				text2 += string.Format("\n\t certName: {0}", arg);
			}
			foreach (IPAddress arg2 in hostEntry.AddressList)
			{
				text2 += string.Format("\n\t hostAddress: {0}", arg2);
			}
			this.LogWarning(text2);
			return false;
		}

		public void Disconnect()
		{
			if (this.m_socket == null)
			{
				return;
			}
			if (this.m_socket.Connected)
			{
				try
				{
					this.m_socket.Shutdown(SocketShutdown.Both);
					this.m_socket.Close();
				}
				catch (SocketException ex)
				{
					this.LogWarning(string.Format("TcpConnection.Disconnect() - SocketException: {0}", ex.Message));
				}
			}
			this.m_socket = null;
		}

		private Socket m_socket;

		private Queue<IPAddress> m_candidateIPAddresses;

		private IPAddress m_resolvedIPAddress;

		public Action<string> LogDebug = delegate
		{
		};

		public Action<string> LogWarning = delegate
		{
		};

		public Action OnFailure = delegate
		{
		};

		public Action OnSuccess = delegate
		{
		};
	}
}
