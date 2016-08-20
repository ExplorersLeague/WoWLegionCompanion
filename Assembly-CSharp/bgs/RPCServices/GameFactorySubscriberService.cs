using System;
using bnet.protocol.game_master;

namespace bgs.RPCServices
{
	public class GameFactorySubscriberService : ServiceDescriptor
	{
		public GameFactorySubscriberService() : base("bnet.protocol.game_master.GameFactorySubscriber")
		{
			this.Methods = new MethodDescriptor[2];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.game_master.GameFactorySubscriber.NotifyGameFound", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameFoundNotification>));
		}

		public const uint NOTIFY_GAME_FOUND_ID = 1u;
	}
}
