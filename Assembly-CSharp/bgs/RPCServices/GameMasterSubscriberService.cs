using System;
using bnet.protocol.game_master;

namespace bgs.RPCServices
{
	public class GameMasterSubscriberService : ServiceDescriptor
	{
		public GameMasterSubscriberService() : base("bnet.protocol.game_master.GameMasterSubscriber")
		{
			this.Methods = new MethodDescriptor[2];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.game_master.GameMasterSubscriber.NotifyFactoryUpdate", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FactoryUpdateNotification>));
		}

		public const uint NOTIFY_FACTORY_UPDATE_ID = 1u;
	}
}
