using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.attribute;
using bnet.protocol.channel;
using bnet.protocol.game_master;
using bnet.protocol.game_utilities;
using bnet.protocol.notification;

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

		public ServiceDescriptor GameMasterService
		{
			get
			{
				return this.m_gameMasterService;
			}
		}

		public ServiceDescriptor GameMasterSubscriberService
		{
			get
			{
				return this.m_gameMasterSubscriberService;
			}
		}

		public ServiceDescriptor GameFactorySubscribeService
		{
			get
			{
				return this.m_gameFactorySubscriberService;
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
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameUtilitiesService.Id, 6u, new RPCContextDelegate(this.HandleGameUtilityServerRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameFactorySubscriberService.Id, 1u, new RPCContextDelegate(this.HandleNotifyGameFoundRequest));
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.s_gameRequest = 0UL;
			this.m_queueEvents.Clear();
			this.m_utilPackets.Clear();
			this.m_getAllValuesForAttributeResults.Clear();
		}

		private void HandleGameUtilityServerRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: GameUtilityServer");
		}

		private void HandleNotifyGameFoundRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: NotifyGameFound");
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

		public ulong CurrentGameRequest
		{
			get
			{
				return this.s_gameRequest;
			}
			set
			{
				this.s_gameRequest = value;
			}
		}

		public bool IsFindGamePending { get; private set; }

		public void FindGame(byte[] requestGuid, int gameType, int scenario, long deckId, long aiDeckId, bool setScenarioIdAttr)
		{
			if (this.s_gameRequest != 0UL)
			{
				LogAdapter.Log(LogLevel.Warning, "WARNING: FindGame called with an active game");
				this.CancelFindGame(this.s_gameRequest);
				this.s_gameRequest = 0UL;
			}
			Player player = new Player();
			Identity identity = new Identity();
			identity.SetGameAccountId(this.m_battleNet.GameAccountId);
			player.SetIdentity(identity);
			player.AddAttribute(ProtocolHelper.CreateAttribute("type", (long)gameType));
			player.AddAttribute(ProtocolHelper.CreateAttribute("scenario", (long)scenario));
			player.AddAttribute(ProtocolHelper.CreateAttribute("deck", (long)((int)deckId)));
			player.AddAttribute(ProtocolHelper.CreateAttribute("aideck", (long)((int)aiDeckId)));
			player.AddAttribute(ProtocolHelper.CreateAttribute("request_guid", requestGuid));
			GameProperties gameProperties = new GameProperties();
			AttributeFilter attributeFilter = new AttributeFilter();
			attributeFilter.SetOp(AttributeFilter.Types.Operation.MATCH_ALL);
			if (!BattleNet.IsVersionInt())
			{
				attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("version", BattleNet.GetVersion()));
			}
			else
			{
				int num = 0;
				if (!int.TryParse(BattleNet.GetVersion(), out num))
				{
					LogAdapter.Log(LogLevel.Error, "Could not convert BattleNetVersion to int: " + BattleNet.GetVersion());
				}
				attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("version", (long)num));
			}
			attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("GameType", (long)gameType));
			if (setScenarioIdAttr)
			{
				attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("ScenarioId", (long)scenario));
			}
			gameProperties.SetFilter(attributeFilter);
			gameProperties.AddCreationAttributes(ProtocolHelper.CreateAttribute("type", (long)gameType));
			gameProperties.AddCreationAttributes(ProtocolHelper.CreateAttribute("scenario", (long)scenario));
			FindGameRequest findGameRequest = new FindGameRequest();
			findGameRequest.AddPlayer(player);
			findGameRequest.SetProperties(gameProperties);
			findGameRequest.SetAdvancedNotification(true);
			FindGameRequest findGameRequest2 = findGameRequest;
			this.PrintFindGameRequest(findGameRequest2);
			this.IsFindGamePending = true;
			this.m_rpcConnection.QueueRequest(this.m_gameMasterService.Id, 3u, findGameRequest2, new RPCContextDelegate(this.FindGameCallback), 0u);
		}

		public void CancelFindGame()
		{
			this.CancelFindGame(this.s_gameRequest);
			this.s_gameRequest = 0UL;
		}

		public void CreateFriendlyChallengeGame(long myDeck, long hisDeck, bnet.protocol.EntityId hisGameAccount, int scenario)
		{
			FindGameRequest findGameRequest = new FindGameRequest();
			Player player = new Player();
			Identity identity = new Identity();
			identity.SetGameAccountId(this.m_battleNet.GameAccountId);
			GameProperties gameProperties = new GameProperties();
			AttributeFilter attributeFilter = new AttributeFilter();
			attributeFilter.SetOp(AttributeFilter.Types.Operation.MATCH_ALL);
			if (!BattleNet.IsVersionInt())
			{
				attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("version", BattleNet.GetVersion()));
			}
			else
			{
				int num = 0;
				if (!int.TryParse(BattleNet.GetVersion(), out num))
				{
					LogAdapter.Log(LogLevel.Error, "Could not convert BattleNetVersion to int: " + BattleNet.GetVersion());
				}
				attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("version", (long)num));
			}
			attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("GameType", 1L));
			attributeFilter.AddAttribute(ProtocolHelper.CreateAttribute("ScenarioId", (long)scenario));
			gameProperties.SetFilter(attributeFilter);
			gameProperties.AddCreationAttributes(ProtocolHelper.CreateAttribute("type", 1L));
			gameProperties.AddCreationAttributes(ProtocolHelper.CreateAttribute("scenario", (long)scenario));
			player.SetIdentity(identity);
			player.AddAttribute(ProtocolHelper.CreateAttribute("type", 1L));
			player.AddAttribute(ProtocolHelper.CreateAttribute("scenario", (long)scenario));
			player.AddAttribute(ProtocolHelper.CreateAttribute("deck", (long)((int)myDeck)));
			findGameRequest.AddPlayer(player);
			identity = new Identity();
			player = new Player();
			identity.SetGameAccountId(hisGameAccount);
			player.SetIdentity(identity);
			player.AddAttribute(ProtocolHelper.CreateAttribute("type", 1L));
			player.AddAttribute(ProtocolHelper.CreateAttribute("scenario", (long)scenario));
			player.AddAttribute(ProtocolHelper.CreateAttribute("deck", (long)((int)hisDeck)));
			findGameRequest.AddPlayer(player);
			findGameRequest.SetProperties(gameProperties);
			findGameRequest.SetAdvancedNotification(true);
			FindGameRequest findGameRequest2 = findGameRequest;
			this.PrintFindGameRequest(findGameRequest2);
			this.IsFindGamePending = true;
			this.m_rpcConnection.QueueRequest(this.m_gameMasterService.Id, 3u, findGameRequest2, new RPCContextDelegate(this.FindGameCallback), 0u);
		}

		private void CancelFindGame(ulong gameRequestId)
		{
			CancelGameEntryRequest cancelGameEntryRequest = new CancelGameEntryRequest();
			cancelGameEntryRequest.RequestId = gameRequestId;
			Player player = new Player();
			Identity identity = new Identity();
			identity.SetGameAccountId(this.m_battleNet.GameAccountId);
			player.SetIdentity(identity);
			cancelGameEntryRequest.AddPlayer(player);
			GamesAPI.CancelGameContext @object = new GamesAPI.CancelGameContext(gameRequestId);
			this.m_rpcConnection.QueueRequest(this.m_gameMasterService.Id, 4u, cancelGameEntryRequest, new RPCContextDelegate(@object.CancelGameCallback), 0u);
		}

		public void GameLeft(ChannelAPI.ChannelReferenceObject channelRefObject, RemoveNotification notification)
		{
			base.ApiLog.LogDebug(string.Concat(new object[]
			{
				"GameLeft ChannelID: ",
				channelRefObject.m_channelData.m_channelId,
				" notification: ",
				notification
			}));
			if (this.s_gameRequest != 0UL)
			{
				this.s_gameRequest = 0UL;
			}
		}

		public QueueEvent GetQueueEvent()
		{
			QueueEvent result = null;
			object queueEvents = this.m_queueEvents;
			lock (queueEvents)
			{
				if (this.m_queueEvents.Count > 0)
				{
					result = this.m_queueEvents.Dequeue();
				}
			}
			return result;
		}

		private void AddQueueEvent(QueueEvent.Type queueType, int minSeconds = 0, int maxSeconds = 0, int bnetError = 0, GameServerInfo gsInfo = null)
		{
			QueueEvent item = new QueueEvent(queueType, minSeconds, maxSeconds, bnetError, gsInfo);
			object queueEvents = this.m_queueEvents;
			lock (queueEvents)
			{
				this.m_queueEvents.Enqueue(item);
			}
		}

		private void QueueUpdate(QueueEvent.Type queueType, Notification notification)
		{
			int minSeconds = 0;
			int maxSeconds = 0;
			foreach (bnet.protocol.attribute.Attribute attribute in notification.AttributeList)
			{
				if (attribute.Name == "min_wait" && attribute.Value.HasUintValue)
				{
					minSeconds = (int)attribute.Value.UintValue;
				}
				else if (attribute.Name == "max_wait" && attribute.Value.HasUintValue)
				{
					maxSeconds = (int)attribute.Value.UintValue;
				}
			}
			this.AddQueueEvent(queueType, minSeconds, maxSeconds, 0, null);
		}

		public void QueueEntryHandler(Notification notification)
		{
			base.ApiLog.LogDebug("QueueEntryHandler: " + notification);
			this.QueueUpdate(QueueEvent.Type.QUEUE_DELAY, notification);
		}

		public void QueueUpdateHandler(Notification notification)
		{
			base.ApiLog.LogDebug("QueueUpdateHandler: " + notification);
			this.QueueUpdate(QueueEvent.Type.QUEUE_UPDATE, notification);
		}

		public void QueueExitHandler(Notification notification)
		{
			BattleNetErrors battleNetErrors = (BattleNetErrors)ProtocolHelper.GetUintAttribute(notification.AttributeList, "error", 0UL, null);
			ulong uintAttribute = ProtocolHelper.GetUintAttribute(notification.AttributeList, "game_request_id", 0UL, null);
			base.ApiLog.LogDebug("QueueExitHandler: requestId={0} code={1}", new object[]
			{
				uintAttribute,
				(uint)battleNetErrors
			});
			if (battleNetErrors != BattleNetErrors.ERROR_OK)
			{
				QueueEvent.Type type = QueueEvent.Type.QUEUE_DELAY_ERROR;
				base.ApiLog.LogDebug("QueueExitHandler event={0} code={1}", new object[]
				{
					type,
					(uint)battleNetErrors
				});
				this.AddQueueEvent(type, 0, 0, (int)battleNetErrors, null);
			}
		}

		public void MatchMakerStartHandler(Notification notification)
		{
			base.ApiLog.LogDebug("MM_START");
			this.AddQueueEvent(QueueEvent.Type.QUEUE_ENTER, 0, 0, 0, null);
		}

		public void MatchMakerEndHandler(Notification notification)
		{
			BattleNetErrors battleNetErrors = (BattleNetErrors)ProtocolHelper.GetUintAttribute(notification.AttributeList, "error", 0UL, null);
			ulong uintAttribute = ProtocolHelper.GetUintAttribute(notification.AttributeList, "game_request_id", 0UL, null);
			base.ApiLog.LogDebug("MM_END requestId={0} code={1}", new object[]
			{
				uintAttribute,
				(uint)battleNetErrors
			});
			QueueEvent.Type type;
			if (battleNetErrors == BattleNetErrors.ERROR_OK)
			{
				type = QueueEvent.Type.QUEUE_LEAVE;
			}
			else if (battleNetErrors == BattleNetErrors.ERROR_GAME_MASTER_GAME_ENTRY_CANCELLED)
			{
				type = QueueEvent.Type.QUEUE_CANCEL;
			}
			else if (battleNetErrors == BattleNetErrors.ERROR_GAME_MASTER_GAME_ENTRY_ABORTED_CLIENT_DROPPED)
			{
				type = QueueEvent.Type.ABORT_CLIENT_DROPPED;
			}
			else
			{
				type = QueueEvent.Type.QUEUE_AMM_ERROR;
			}
			base.ApiLog.LogDebug("MatchMakerEndHandler event={0} code={1}", new object[]
			{
				type,
				(uint)battleNetErrors
			});
			this.AddQueueEvent(type, 0, 0, (int)battleNetErrors, null);
		}

		public void GameEntryHandler(Notification notification)
		{
			base.ApiLog.LogDebug("GAME_ENTRY");
			string address = null;
			int port = 0;
			string version = null;
			int gameHandle = 0;
			int num = 0;
			string auroraPassword = null;
			bool resumable = false;
			string spectatorPassword = null;
			foreach (bnet.protocol.attribute.Attribute attribute in notification.AttributeList)
			{
				if (attribute.Name.Equals("connection_info") && attribute.Value.HasMessageValue)
				{
					byte[] messageValue = attribute.Value.MessageValue;
					ConnectInfo connectInfo = ConnectInfo.ParseFrom(messageValue);
					address = connectInfo.Host;
					port = connectInfo.Port;
					if (connectInfo.HasToken)
					{
						auroraPassword = Encoding.UTF8.GetString(connectInfo.Token);
					}
					foreach (bnet.protocol.attribute.Attribute attribute2 in connectInfo.AttributeList)
					{
						if (attribute2.Name.Equals("version") && attribute2.Value.HasStringValue)
						{
							version = attribute2.Value.StringValue;
						}
						else if (attribute2.Name.Equals("game") && attribute2.Value.HasIntValue)
						{
							gameHandle = (int)attribute2.Value.IntValue;
						}
						else if (attribute2.Name.Equals("id") && attribute2.Value.HasIntValue)
						{
							num = (int)attribute2.Value.IntValue;
						}
						else if (attribute2.Name.Equals("resumable") && attribute2.Value.HasBoolValue)
						{
							resumable = attribute2.Value.BoolValue;
						}
						else if (attribute2.Name.Equals("spectator_password") && attribute2.Value.HasStringValue)
						{
							spectatorPassword = attribute2.Value.StringValue;
						}
					}
				}
				else if (attribute.Name.Equals("game_handle") && attribute.Value.HasMessageValue)
				{
					byte[] messageValue2 = attribute.Value.MessageValue;
					GameHandle gameHandle2 = GameHandle.ParseFrom(messageValue2);
					this.m_battleNet.Channel.JoinChannel(gameHandle2.GameId, ChannelAPI.ChannelType.GAME_CHANNEL);
				}
				else if (attribute.Name.Equals("sender_id") && attribute.Value.HasMessageValue)
				{
					base.ApiLog.LogDebug("sender_id");
				}
			}
			this.AddQueueEvent(QueueEvent.Type.QUEUE_GAME_STARTED, 0, 0, 0, new GameServerInfo
			{
				Address = address,
				Port = port,
				AuroraPassword = auroraPassword,
				Version = version,
				GameHandle = gameHandle,
				ClientHandle = (long)num,
				Resumable = resumable,
				SpectatorPassword = spectatorPassword
			});
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

		private void FindGameCallback(RPCContext context)
		{
			this.IsFindGamePending = false;
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			base.ApiLog.LogDebug("Find Game Callback, status=" + status);
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnFindGame, status, 0);
				return;
			}
			byte[] payload = context.Payload;
			FindGameResponse findGameResponse = FindGameResponse.ParseFrom(payload);
			if (findGameResponse.HasRequestId)
			{
				this.s_gameRequest = findGameResponse.RequestId;
			}
		}

		private void PrintFindGameRequest(FindGameRequest request)
		{
			string text = "FindGameRequest: { ";
			int playerCount = request.PlayerCount;
			for (int i = 0; i < playerCount; i++)
			{
				Player player = request.Player[i];
				text += this.PrintPlayer(player);
			}
			if (request.HasFactoryId)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Factory Id: ",
					request.FactoryId,
					" "
				});
			}
			if (request.HasProperties)
			{
				text += this.PrintGameProperties(request.Properties);
			}
			if (request.HasObjectId)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Obj Id: ",
					request.ObjectId,
					" "
				});
			}
			if (request.HasRequestId)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Request Id: ",
					request.RequestId,
					" "
				});
			}
			text += "}";
			base.ApiLog.LogDebug(text);
		}

		private string PrintPlayer(Player player)
		{
			string text = string.Empty;
			text += "Player: [";
			if (player.HasIdentity)
			{
				this.PrintGameMasterIdentity(player.Identity);
			}
			int attributeCount = player.AttributeCount;
			text += "Attributes: ";
			for (int i = 0; i < attributeCount; i++)
			{
				bnet.protocol.attribute.Attribute attribute = player.Attribute[i];
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"[Name: ",
					attribute.Name,
					" Value: ",
					attribute.Value,
					"] "
				});
			}
			return text + "] ";
		}

		private string PrintGameMasterIdentity(Identity identity)
		{
			string text = string.Empty;
			text += "Identity: [";
			if (identity.HasAccountId)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Acct Id: ",
					identity.AccountId.High,
					":",
					identity.AccountId.Low,
					" "
				});
			}
			if (identity.HasGameAccountId)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Game Acct Id: ",
					identity.GameAccountId.High,
					":",
					identity.GameAccountId.Low,
					" "
				});
			}
			return text + "] ";
		}

		private string PrintGameProperties(GameProperties properties)
		{
			string text = string.Empty;
			text = "Game Properties: [";
			int creationAttributesCount = properties.CreationAttributesCount;
			text += "Creation Attributes: ";
			for (int i = 0; i < creationAttributesCount; i++)
			{
				bnet.protocol.attribute.Attribute attribute = properties.CreationAttributes[i];
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"[Name: ",
					attribute.Name,
					" Value: ",
					attribute.Value,
					"] "
				});
			}
			if (properties.HasFilter)
			{
				this.PrintGameMasterAttributeFilter(properties.Filter);
			}
			if (properties.HasCreate)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Create New Game?: ",
					properties.Create,
					" "
				});
			}
			if (properties.HasOpen)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Game Is Open?: ",
					properties.Open,
					" "
				});
			}
			if (properties.HasProgramId)
			{
				text = text + "Program Id(4CC): " + properties.ProgramId;
			}
			return text;
		}

		private string PrintGameMasterAttributeFilter(AttributeFilter filter)
		{
			string text = "Attribute Filter: [";
			string str;
			switch (filter.Op)
			{
			case AttributeFilter.Types.Operation.MATCH_NONE:
				str = "MATCH_NONE";
				break;
			case AttributeFilter.Types.Operation.MATCH_ANY:
				str = "MATCH_ANY";
				break;
			case AttributeFilter.Types.Operation.MATCH_ALL:
				str = "MATCH_ALL";
				break;
			case AttributeFilter.Types.Operation.MATCH_ALL_MOST_SPECIFIC:
				str = "MATCH_ALL_MOST_SPECIFIC";
				break;
			default:
				str = "UNKNOWN";
				break;
			}
			text = text + "Operation: " + str + " ";
			text += "Attributes: [";
			int attributeCount = filter.AttributeCount;
			for (int i = 0; i < attributeCount; i++)
			{
				bnet.protocol.attribute.Attribute attribute = filter.Attribute[i];
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"Name: ",
					attribute.Name,
					" Value: ",
					attribute.Value,
					" "
				});
			}
			return text + "] ";
		}

		private const int NO_AI_DECK = 0;

		private const bool RANK_NA = true;

		private const bool RANKED = false;

		private const bool UNRANKED = true;

		private Queue<GamesAPI.UtilResponse> m_utilPackets = new Queue<GamesAPI.UtilResponse>();

		private Queue<GamesAPI.GetAllValuesForAttributeResult> m_getAllValuesForAttributeResults = new Queue<GamesAPI.GetAllValuesForAttributeResult>();

		private Queue<QueueEvent> m_queueEvents = new Queue<QueueEvent>();

		private ServiceDescriptor m_gameUtilitiesService = new GameUtilitiesService();

		private ServiceDescriptor m_gameMasterService = new GameMasterService();

		private ServiceDescriptor m_gameMasterSubscriberService = new GameMasterSubscriberService();

		private ServiceDescriptor m_gameFactorySubscriberService = new GameFactorySubscriberService();

		private static bool warnComplete;

		private ulong s_gameRequest;

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

		private class CancelGameContext
		{
			public CancelGameContext(ulong gameRequestId)
			{
				this.m_gameRequestId = gameRequestId;
			}

			public void CancelGameCallback(RPCContext context)
			{
				BattleNetCSharp battleNetCSharp = BattleNet.Get() as BattleNetCSharp;
				if (battleNetCSharp == null || battleNetCSharp.Games == null)
				{
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				battleNetCSharp.Games.ApiLog.LogDebug("CancelGameCallback, status=" + status);
				if (status == BattleNetErrors.ERROR_OK || status == BattleNetErrors.ERROR_GAME_MASTER_INVALID_GAME)
				{
					if (battleNetCSharp.Games.IsFindGamePending || (battleNetCSharp.Games.CurrentGameRequest != 0UL && battleNetCSharp.Games.CurrentGameRequest != this.m_gameRequestId))
					{
						battleNetCSharp.Games.ApiLog.LogDebug("CancelGameCallback received for id={0} but is not the current gameRequest={1}, ignoring it.", new object[]
						{
							this.m_gameRequestId,
							battleNetCSharp.Games.CurrentGameRequest
						});
					}
					else
					{
						battleNetCSharp.Games.CurrentGameRequest = 0UL;
						battleNetCSharp.Games.AddQueueEvent(QueueEvent.Type.QUEUE_CANCEL, 0, 0, 0, null);
					}
				}
				else
				{
					battleNetCSharp.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnCancelGame, status, 0);
				}
			}

			private ulong m_gameRequestId;
		}
	}
}
