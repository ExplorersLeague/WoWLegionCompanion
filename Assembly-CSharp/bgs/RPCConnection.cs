using System;
using System.Collections.Generic;
using System.Diagnostics;
using bgs.RPCServices;
using bnet.protocol;

namespace bgs
{
	public class RPCConnection : IClientConnectionListener<BattleNetPacket>
	{
		public long MillisecondsSinceLastPacketSent
		{
			get
			{
				return this.m_stopWatch.ElapsedMilliseconds;
			}
		}

		public void SetOnConnectHandler(RPCConnection.OnConnectHandler handler)
		{
			this.m_onConnectHandler = handler;
		}

		public void SetOnDisconnectHandler(RPCConnection.OnDisconectHandler handler)
		{
			this.m_onDisconnectHandler = handler;
		}

		public void Connect(string host, int port, SslParameters sslParams)
		{
			this.m_stopWatch = new Stopwatch();
			if (sslParams.useSsl)
			{
				this.Connection = new SslClientConnection(sslParams.bundleSettings)
				{
					OnlyOneSend = true
				};
			}
			else
			{
				this.Connection = new ClientConnection<BattleNetPacket>();
			}
			this.Connection.AddListener(this, null);
			this.Connection.AddConnectHandler(new ConnectHandler(this.OnConnectCallback));
			this.Connection.AddDisconnectHandler(new DisconnectHandler(this.OnDisconnectCallback));
			this.Connection.Connect(host, port);
		}

		public void Disconnect()
		{
			if (this.Connection is SslClientConnection)
			{
				((SslClientConnection)this.Connection).BlockOnSend = true;
			}
			this.Update();
			this.Connection.Disconnect();
		}

		public uint GetImportedServiceNameHash(uint serviceId)
		{
			ServiceDescriptor importedServiceDescriptor = this.GetImportedServiceDescriptor(serviceId);
			if (importedServiceDescriptor != null)
			{
				return importedServiceDescriptor.Hash;
			}
			return uint.MaxValue;
		}

		public uint GetExportedServiceNameHash(uint serviceId)
		{
			ServiceDescriptor exportedServiceDescriptor = this.GetExportedServiceDescriptor(serviceId);
			if (exportedServiceDescriptor != null)
			{
				return exportedServiceDescriptor.Hash;
			}
			return uint.MaxValue;
		}

		public void BeginAuth()
		{
			this.m_connMetering.ResetStartupPeriod();
		}

		public RPCContext QueueRequest(uint serviceId, uint methodId, IProtoBuf message, RPCContextDelegate callback = null, uint objectId = 0u)
		{
			if (message == null)
			{
				return null;
			}
			object obj = this.tokenLock;
			uint num;
			lock (obj)
			{
				num = RPCConnection.nextToken;
				RPCConnection.nextToken += 1u;
			}
			RPCContext rpccontext = new RPCContext();
			if (callback != null)
			{
				rpccontext.Callback = callback;
				this.waitingForResponse.Add(num, rpccontext);
			}
			Header header = this.CreateHeader(serviceId, methodId, objectId, num, message.GetSerializedSize());
			BattleNetPacket battleNetPacket = new BattleNetPacket(header, message);
			rpccontext.Header = header;
			rpccontext.Request = message;
			if (!this.m_connMetering.AllowRPCCall(serviceId, methodId))
			{
				this.m_pendingOutboundPackets.Add(battleNetPacket);
				this.LogOutgoingPacket(battleNetPacket, true);
			}
			else
			{
				this.QueuePacket(battleNetPacket);
			}
			return rpccontext;
		}

		public void QueueResponse(RPCContext context, IProtoBuf message)
		{
			if (message == null || context.Header == null)
			{
				this.m_logSource.LogError("QueueResponse: invalid response");
				return;
			}
			if (this.serviceHelper.GetImportedServiceById(context.Header.ServiceId) == null)
			{
				this.m_logSource.LogError("QueueResponse: error, unrecognized service id: " + context.Header.ServiceId);
				return;
			}
			this.m_logSource.LogDebug(string.Concat(new object[]
			{
				"QueueResponse: type=",
				this.serviceHelper.GetImportedServiceById(context.Header.ServiceId).GetMethodName(context.Header.MethodId),
				" data=",
				message
			}));
			Header header = context.Header;
			header.SetServiceId(254u);
			header.SetMethodId(0u);
			header.SetSize(message.GetSerializedSize());
			context.Header = header;
			BattleNetPacket packet = new BattleNetPacket(context.Header, message);
			this.QueuePacket(packet);
		}

		public void RegisterServiceMethodListener(uint serviceId, uint methodId, RPCContextDelegate callback)
		{
			ServiceDescriptor exportedServiceDescriptor = this.GetExportedServiceDescriptor(serviceId);
			if (exportedServiceDescriptor != null)
			{
				exportedServiceDescriptor.RegisterMethodListener(methodId, callback);
			}
		}

		public void Update()
		{
			this.ProcessPendingOutboundPackets();
			if (this.outBoundPackets.Count > 0)
			{
				object obj = this.outBoundPackets;
				Queue<BattleNetPacket> queue;
				lock (obj)
				{
					queue = new Queue<BattleNetPacket>(this.outBoundPackets.ToArray());
					this.outBoundPackets.Clear();
				}
				while (queue.Count > 0)
				{
					BattleNetPacket packet = queue.Dequeue();
					if (this.Connection != null)
					{
						this.Connection.QueuePacket(packet);
					}
					else
					{
						this.m_logSource.LogError("##Client Connection object does not exists!##");
					}
				}
			}
			if (this.Connection != null)
			{
				this.Connection.Update();
			}
			if (this.incomingPackets.Count > 0)
			{
				object obj2 = this.incomingPackets;
				Queue<BattleNetPacket> queue2;
				lock (obj2)
				{
					queue2 = new Queue<BattleNetPacket>(this.incomingPackets.ToArray());
					this.incomingPackets.Clear();
				}
				while (queue2.Count > 0)
				{
					BattleNetPacket battleNetPacket = queue2.Dequeue();
					Header header = battleNetPacket.GetHeader();
					this.PrintHeader(header);
					byte[] payload = (byte[])battleNetPacket.GetBody();
					if (header.ServiceId == 254u)
					{
						RPCContext rpccontext;
						if (this.waitingForResponse.TryGetValue(header.Token, out rpccontext))
						{
							ServiceDescriptor importedServiceById = this.serviceHelper.GetImportedServiceById(rpccontext.Header.ServiceId);
							MethodDescriptor.ParseMethod parseMethod = null;
							if (importedServiceById != null)
							{
								parseMethod = importedServiceById.GetParser(rpccontext.Header.MethodId);
							}
							if (parseMethod == null)
							{
								if (importedServiceById != null)
								{
									this.m_logSource.LogWarning("Incoming Response: Unable to find method for serviceName={0} method id={1}", new object[]
									{
										importedServiceById.Name,
										rpccontext.Header.MethodId
									});
									int methodCount = importedServiceById.GetMethodCount();
									this.m_logSource.LogDebug("  Found {0} methods", new object[]
									{
										methodCount
									});
									for (int i = 0; i < methodCount; i++)
									{
										MethodDescriptor methodDescriptor = importedServiceById.GetMethodDescriptor((uint)i);
										if (methodDescriptor == null && i != 0)
										{
											this.m_logSource.LogDebug("  Found method id={0} name={1}", new object[]
											{
												i,
												"<null>"
											});
										}
										else
										{
											this.m_logSource.LogDebug("  Found method id={0} name={1}", new object[]
											{
												i,
												methodDescriptor.Name
											});
										}
									}
								}
								else
								{
									this.m_logSource.LogWarning("Incoming Response: Unable to identify service id={0}", new object[]
									{
										rpccontext.Header.ServiceId
									});
								}
							}
							rpccontext.Header = header;
							rpccontext.Payload = payload;
							rpccontext.ResponseReceived = true;
							if (rpccontext.Callback != null)
							{
								rpccontext.Callback(rpccontext);
							}
							this.waitingForResponse.Remove(header.Token);
						}
					}
					else
					{
						ServiceDescriptor exportedServiceDescriptor = this.GetExportedServiceDescriptor(header.ServiceId);
						if (exportedServiceDescriptor != null)
						{
							MethodDescriptor.ParseMethod parser = this.serviceHelper.GetExportedServiceById(header.ServiceId).GetParser(header.MethodId);
							if (parser == null)
							{
								this.m_logSource.LogDebug("Incoming Packet: NULL TYPE service=" + this.serviceHelper.GetExportedServiceById(header.ServiceId).Name + ", method=" + this.serviceHelper.GetExportedServiceById(header.ServiceId).GetMethodName(header.MethodId));
							}
							if (exportedServiceDescriptor.HasMethodListener(header.MethodId))
							{
								exportedServiceDescriptor.NotifyMethodListener(new RPCContext
								{
									Header = header,
									Payload = payload,
									ResponseReceived = true
								});
							}
							else
							{
								string text = (exportedServiceDescriptor == null || string.IsNullOrEmpty(exportedServiceDescriptor.Name)) ? "<null>" : exportedServiceDescriptor.Name;
								this.m_logSource.LogError(string.Concat(new object[]
								{
									"[!]Unhandled Server Request Received (Service Name: ",
									text,
									" Service id:",
									header.ServiceId,
									" Method id:",
									header.MethodId,
									")"
								}));
							}
						}
						else
						{
							this.m_logSource.LogError(string.Concat(new object[]
							{
								"[!]Server Requested an Unsupported (Service id:",
								header.ServiceId,
								" Method id:",
								header.MethodId,
								")"
							}));
						}
					}
				}
			}
		}

		public void PacketReceived(BattleNetPacket p, object state)
		{
			object obj = this.incomingPackets;
			lock (obj)
			{
				this.incomingPackets.Enqueue(p);
			}
		}

		protected Header CreateHeader(uint serviceId, uint methodId, uint objectId, uint token, uint size)
		{
			Header header = new Header();
			header.SetServiceId(serviceId);
			header.SetMethodId(methodId);
			if (objectId != 0u)
			{
				header.SetObjectId((ulong)objectId);
			}
			header.SetToken(token);
			header.SetSize(size);
			return header;
		}

		protected void QueuePacket(BattleNetPacket packet)
		{
			this.LogOutgoingPacket(packet, false);
			object obj = this.outBoundPackets;
			lock (obj)
			{
				this.outBoundPackets.Enqueue(packet);
				this.m_stopWatch.Reset();
				this.m_stopWatch.Start();
			}
		}

		private void LogOutgoingPacket(BattleNetPacket packet, bool wasMetered)
		{
			if (this.m_logSource == null)
			{
				LogAdapter.Log(LogLevel.Warning, "tried to log with null log source, skipping");
				return;
			}
			bool flag = false;
			IProtoBuf protoBuf = (IProtoBuf)packet.GetBody();
			Header header = packet.GetHeader();
			uint serviceId = header.ServiceId;
			uint methodId = header.MethodId;
			string text = (!wasMetered) ? "QueueRequest" : "QueueRequest (METERED)";
			if (!string.IsNullOrEmpty(protoBuf.ToString()))
			{
				ServiceDescriptor importedServiceById = this.serviceHelper.GetImportedServiceById(serviceId);
				string text2 = (importedServiceById != null) ? importedServiceById.GetMethodName(methodId) : "null";
				if (!text2.Contains("KeepAlive"))
				{
					this.m_logSource.LogDebug("{0}: type = {1}, header = {2}, request = {3}", new object[]
					{
						text,
						text2,
						header.ToString(),
						protoBuf.ToString()
					});
				}
			}
			else
			{
				ServiceDescriptor importedServiceById2 = this.serviceHelper.GetImportedServiceById(serviceId);
				string text3 = (importedServiceById2 != null) ? importedServiceById2.GetMethodName(methodId) : null;
				if (text3 != "bnet.protocol.connection.ConnectionService.KeepAlive" && text3 != null)
				{
					this.m_logSource.LogDebug("{0}: type = {1}, header = {2}", new object[]
					{
						text,
						text3,
						header.ToString()
					});
				}
				else
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.m_logSource.LogDebugStackTrace("LogOutgoingPacket: ", 32, 1);
			}
		}

		private void ProcessPendingOutboundPackets()
		{
			if (this.m_pendingOutboundPackets.Count > 0)
			{
				List<BattleNetPacket> list = new List<BattleNetPacket>();
				foreach (BattleNetPacket battleNetPacket in this.m_pendingOutboundPackets)
				{
					Header header = battleNetPacket.GetHeader();
					uint serviceId = header.ServiceId;
					uint methodId = header.MethodId;
					if (this.m_connMetering.AllowRPCCall(serviceId, methodId))
					{
						this.QueuePacket(battleNetPacket);
					}
					else
					{
						list.Add(battleNetPacket);
					}
				}
				this.m_pendingOutboundPackets = list;
			}
		}

		private void PrintHeader(Header h)
		{
			string text = string.Format("Packet received: Header = [ ServiceId: {0}, MethodId: {1} Token: {2} Size: {3} Status: {4}", new object[]
			{
				h.ServiceId,
				h.MethodId,
				h.Token,
				h.Size,
				(BattleNetErrors)h.Status
			});
			if (h.ErrorCount > 0)
			{
				text += " Error:[";
				foreach (ErrorInfo errorInfo in h.ErrorList)
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						" ErrorInfo{ ",
						errorInfo.ObjectAddress.Host.Label,
						"/",
						errorInfo.ObjectAddress.Host.Epoch,
						"}"
					});
				}
				text += "]";
			}
			text += "]";
			this.m_logSource.LogDebug(text);
		}

		private void OnConnectCallback(BattleNetErrors error)
		{
			if (this.m_onConnectHandler != null)
			{
				this.m_onConnectHandler(error);
			}
		}

		private void OnDisconnectCallback(BattleNetErrors error)
		{
			if (this.m_onDisconnectHandler != null)
			{
				this.m_onDisconnectHandler(error);
			}
		}

		private ServiceDescriptor GetImportedServiceDescriptor(uint serviceId)
		{
			return this.serviceHelper.GetImportedServiceById(serviceId);
		}

		private ServiceDescriptor GetExportedServiceDescriptor(uint serviceId)
		{
			return this.serviceHelper.GetExportedServiceById(serviceId);
		}

		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				this.m_cmLogSource.LogWarning("Downloading of the connection metering data failed!");
				return;
			}
			this.m_cmLogSource.LogDebug("Connection metering file downloaded. Length={0}", new object[]
			{
				data.Length
			});
			this.m_connMetering.SetConnectionMeteringData(data, this.serviceHelper);
		}

		private string GetMethodName(Header header)
		{
			return this.GetMethodName(header, true);
		}

		private string GetMethodName(Header header, bool outgoing)
		{
			if (header.ServiceId == 254u)
			{
				return "Response";
			}
			ServiceDescriptor serviceDescriptor;
			if (outgoing)
			{
				serviceDescriptor = this.serviceHelper.GetImportedServiceById(header.ServiceId);
			}
			else
			{
				serviceDescriptor = this.serviceHelper.GetExportedServiceById(header.ServiceId);
			}
			return (serviceDescriptor != null) ? serviceDescriptor.GetMethodName(header.MethodId) : "No Descriptor";
		}

		private string GetServiceName(Header header, bool outgoing)
		{
			if (header.ServiceId == 254u)
			{
				return "Response";
			}
			ServiceDescriptor serviceDescriptor;
			if (outgoing)
			{
				serviceDescriptor = this.serviceHelper.GetImportedServiceById(header.ServiceId);
			}
			else
			{
				serviceDescriptor = this.serviceHelper.GetExportedServiceById(header.ServiceId);
			}
			return (serviceDescriptor != null) ? serviceDescriptor.Name : "No Descriptor";
		}

		public string PacketToString(BattleNetPacket packet, bool outgoing)
		{
			return this.PacketHeaderToString(packet.GetHeader(), outgoing);
		}

		public string PacketHeaderToString(Header header, bool outgoing)
		{
			string text = string.Empty;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Service:(",
				header.ServiceId,
				")",
				this.GetServiceName(header, outgoing)
			});
			text += " ";
			text = text + "Method:(" + ((!header.HasMethodId) ? "?)" : (header.MethodId + ")" + this.GetMethodName(header, outgoing)));
			text += " ";
			text = text + "Token:" + header.Token;
			text += " ";
			text = text + "Status:" + (BattleNetErrors)header.Status;
			if (header.ErrorCount > 0)
			{
				text += " Error:[";
				foreach (ErrorInfo errorInfo in header.ErrorList)
				{
					text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						" ErrorInfo{ ",
						errorInfo.ObjectAddress.Host.Label,
						"/",
						errorInfo.ObjectAddress.Host.Epoch,
						"}"
					});
				}
				text += "]";
			}
			return text;
		}

		private const int RESPONSE_SERVICE_ID = 254;

		private BattleNetLogSource m_logSource = new BattleNetLogSource("Network");

		private BattleNetLogSource m_cmLogSource = new BattleNetLogSource("ConnectionMetering");

		private IClientConnection<BattleNetPacket> Connection;

		public ServiceCollectionHelper serviceHelper = new ServiceCollectionHelper();

		private Queue<BattleNetPacket> outBoundPackets = new Queue<BattleNetPacket>();

		private Queue<BattleNetPacket> incomingPackets = new Queue<BattleNetPacket>();

		private List<BattleNetPacket> m_pendingOutboundPackets = new List<BattleNetPacket>();

		private object tokenLock = new object();

		private static uint nextToken;

		private Dictionary<uint, RPCContext> waitingForResponse = new Dictionary<uint, RPCContext>();

		private RPCConnection.OnConnectHandler m_onConnectHandler;

		private RPCConnection.OnDisconectHandler m_onDisconnectHandler;

		private Stopwatch m_stopWatch;

		private RPCConnectionMetering m_connMetering = new RPCConnectionMetering();

		public delegate void OnConnectHandler(BattleNetErrors error);

		public delegate void OnDisconectHandler(BattleNetErrors error);
	}
}
