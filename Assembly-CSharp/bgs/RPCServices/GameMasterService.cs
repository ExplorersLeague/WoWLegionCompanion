using System;
using bnet.protocol;
using bnet.protocol.game_master;

namespace bgs.RPCServices
{
	public class GameMasterService : ServiceDescriptor
	{
		public GameMasterService() : base("bnet.protocol.game_master.GameMaster")
		{
			this.Methods = new MethodDescriptor[16];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.JoinGame", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinGameResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.ListFactories", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ListFactoriesResponse>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.FindGame", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FindGameResponse>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.CancelGameEntry", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.GameEnded", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.PlayerLeft", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.RegisterServer", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)8)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.UnregisterServer", 8u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)9)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.RegisterUtilities", 9u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)10)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.UnregisterUtilities", 10u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)11)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.Subscribe", 11u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>));
			this.Methods[(int)((UIntPtr)12)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.Unsubscribe", 12u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)13)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.ChangeGame", 13u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)14)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.GetFactoryInfo", 14u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetFactoryInfoResponse>));
			this.Methods[(int)((UIntPtr)15)] = new MethodDescriptor("bnet.protocol.game_master.GameMaster.GetGameStats", 15u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameStatsResponse>));
		}

		public const uint JOIN_GAME_ID = 1u;

		public const uint LIST_FACTORIES_ID = 2u;

		public const uint FIND_GAME_ID = 3u;

		public const uint CANCEL_GAME_ENTRY_ID = 4u;

		public const uint GAME_ENDED_ID = 5u;

		public const uint PLAYER_LEFT_ID = 6u;

		public const uint REGISTER_SERVER_ID = 7u;

		public const uint UNREGISTER_SERVER_ID = 8u;

		public const uint REGISTER_UTILITIES_ID = 9u;

		public const uint UNREGISTER_UTILITIES_ID = 10u;

		public const uint SUBSCRIBE_ID = 11u;

		public const uint UNSUBSCRIBE_ID = 12u;

		public const uint CHANGE_GAME_ID = 13u;

		public const uint GET_FACTORY_INFO_ID = 14u;

		public const uint GET_GAME_STATS_ID = 15u;
	}
}
