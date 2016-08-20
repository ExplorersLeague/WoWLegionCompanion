using System;
using bnet.protocol;
using bnet.protocol.game_utilities;
using bnet.protocol.server_pool;

namespace bgs.RPCServices
{
	public class GameUtilitiesService : ServiceDescriptor
	{
		public GameUtilitiesService() : base("bnet.protocol.game_utilities.GameUtilities")
		{
			this.Methods = new MethodDescriptor[11];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.ProcessClientRequest", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ClientResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.PresenceChannelCreated", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.GetPlayerVariables", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetPlayerVariablesResponse>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.GetLoad", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerState>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.ProcessServerRequest", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerResponse>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOnline", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)8)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOffline", 8u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)10)] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.GetAllValuesForAttribute", 10u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAllValuesForAttributeResponse>));
		}

		public const uint PROCESS_CLIENT_REQUEST_ID = 1u;

		public const uint PRESENCE_CHANNEL_CREATED_ID = 2u;

		public const uint GET_PLAYER_VARIABLES_ID = 3u;

		public const uint GET_LOAD_ID = 5u;

		public const uint PROCESS_SERVER_REQUEST_ID = 6u;

		public const uint NOTIFY_GAME_ACCT_ONLINE_ID = 7u;

		public const uint NOTIFY_GAME_ACCT_OFFLINE_ID = 8u;

		public const uint GET_ALL_VALUES_FOR_ATTRIBUTE_ID = 10u;
	}
}
