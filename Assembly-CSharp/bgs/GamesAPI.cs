using System;
using System.Collections.Generic;
using bgs.RPCServices;
using bnet.protocol.attribute;
using bnet.protocol.game_utilities;

namespace bgs
{
	public class GamesAPI : BattleNetAPI
	{
		public GamesAPI(BattleNetCSharp battlenet) : base(battlenet, "Games")
		{
		}

		public ServiceDescriptor GameUtilityService
		{
			get
			{
				return this.m_gameUtilitiesService;
			}
		}

		public GamesAPI.UtilResponse NextUtilPacket()
		{
			if (this.m_utilPackets.Count > 0)
			{
				return this.m_utilPackets.Dequeue();
			}
			return null;
		}

		public GamesAPI.GetAllValuesForAttributeResult NextGetAllValuesForAttributeResult()
		{
			if (this.m_getAllValuesForAttributeResults.Count > 0)
			{
				return this.m_getAllValuesForAttributeResults.Dequeue();
			}
			return null;
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
			this.m_utilPackets.Clear();
			this.m_getAllValuesForAttributeResults.Clear();
		}

		public void GetAllValuesForAttribute(string attributeKey, int context)
		{
			GetAllValuesForAttributeRequest getAllValuesForAttributeRequest = new GetAllValuesForAttributeRequest();
			getAllValuesForAttributeRequest.AttributeKey = attributeKey;
			if (this.m_rpcConnection == null)
			{
				base.ApiLog.LogError("GetAllValuesForAttribute could not send, connection not valid : " + getAllValuesForAttributeRequest.ToString());
				return;
			}
			RPCContext rpccontext = this.m_rpcConnection.QueueRequest(this.m_gameUtilitiesService.Id, 10u, getAllValuesForAttributeRequest, new RPCContextDelegate(this.GetAllValuesForAttributeCallback), 0u);
			rpccontext.Context = context;
		}

		public void SendUtilPacket(int packetId, int systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			ClientRequest clientRequest = this.CreateClientRequest(packetId, systemId, bytes, route);
			if (this.m_rpcConnection == null)
			{
				base.ApiLog.LogError("SendUtilPacket could not send, connection not valid : " + clientRequest.ToString());
				return;
			}
			if (!GamesAPI.warnComplete)
			{
				base.ApiLog.LogWarning("SendUtilPacket: need to map context to RPCContext");
				GamesAPI.warnComplete = true;
			}
			RPCContext rpccontext = this.m_rpcConnection.QueueRequest(this.m_gameUtilitiesService.Id, 1u, clientRequest, new RPCContextDelegate(this.UtilClientRequestCallback), 0u);
			rpccontext.SystemId = systemId;
			rpccontext.Context = context;
		}

		public void SendClientRequest(ClientRequest request, RPCContextDelegate callback = null)
		{
			this.m_rpcConnection.QueueRequest(this.m_gameUtilitiesService.Id, 1u, request, (callback == null) ? new RPCContextDelegate(this.ClientRequestCallback) : callback, 0u);
		}

		private ClientRequest CreateClientRequest(int type, int sys, byte[] bs, ulong route)
		{
			ClientRequest clientRequest = new ClientRequest();
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("p", bs));
			if (!BattleNet.IsVersionInt())
			{
				clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("v", BattleNet.GetVersion() + ((sys != 0) ? "b" : "c")));
			}
			else
			{
				int num = 0;
				if (!int.TryParse(BattleNet.GetVersion(), out num))
				{
					LogAdapter.Log(LogLevel.Error, "Could not convert BattleNetVersion to int: " + BattleNet.GetVersion());
				}
				clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("v", (long)(10 * num + sys)));
			}
			if (route != 0UL)
			{
				clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("r", route));
			}
			return clientRequest;
		}

		private void GetAllValuesForAttributeCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnGetAllValuesForAttribute, status, 0);
				return;
			}
			GetAllValuesForAttributeResponse response = ProtobufUtil.ParseFrom<GetAllValuesForAttributeResponse>(context.Payload, 0, -1);
			this.m_getAllValuesForAttributeResults.Enqueue(new GamesAPI.GetAllValuesForAttributeResult(response, context.Context));
		}

		private void UtilClientRequestCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnClientRequest, status, 0);
				return;
			}
			ClientResponse clientResponse = ClientResponse.ParseFrom(context.Payload);
			if (clientResponse.AttributeCount >= 2)
			{
				bnet.protocol.attribute.Attribute attribute = clientResponse.AttributeList[0];
				bnet.protocol.attribute.Attribute attribute2 = clientResponse.AttributeList[1];
				if (!attribute.Value.HasIntValue || !attribute2.Value.HasBlobValue)
				{
					base.ApiLog.LogError("Malformed Attribute in Util Packet: incorrect values");
				}
				this.m_utilPackets.Enqueue(new GamesAPI.UtilResponse(clientResponse, context.Context));
			}
			else
			{
				base.ApiLog.LogError("Malformed Attribute in Util Packet: missing values");
			}
		}

		private void ClientRequestCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				if (context.SystemId != 1)
				{
					this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnClientRequest, status, 0);
				}
				return;
			}
			ClientResponse response = ClientResponse.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("Enqueuing response");
			this.m_utilPackets.Enqueue(new GamesAPI.UtilResponse(response, context.Context));
		}

		public static ClientResponse GetClientResponseFromContext(RPCContext context)
		{
			return ClientResponse.ParseFrom(context.Payload);
		}

		private Queue<GamesAPI.UtilResponse> m_utilPackets = new Queue<GamesAPI.UtilResponse>();

		private Queue<GamesAPI.GetAllValuesForAttributeResult> m_getAllValuesForAttributeResults = new Queue<GamesAPI.GetAllValuesForAttributeResult>();

		private ServiceDescriptor m_gameUtilitiesService = new GameUtilitiesService();

		private static bool warnComplete;

		public class UtilResponse
		{
			public UtilResponse(ClientResponse response, int context)
			{
				this.m_response = response;
				this.m_context = context;
			}

			public ClientResponse m_response;

			public int m_context;
		}

		public class GetAllValuesForAttributeResult
		{
			public GetAllValuesForAttributeResult(GetAllValuesForAttributeResponse response, int context)
			{
				this.m_response = response;
				this.m_context = context;
			}

			public GetAllValuesForAttributeResponse m_response;

			public int m_context;
		}
	}
}
