using System;
using System.Runtime.CompilerServices;
using bnet.protocol.game_master;

namespace bgs.RPCServices
{
	public class GameFactorySubscriberService : ServiceDescriptor
	{
		public GameFactorySubscriberService() : base("bnet.protocol.game_master.GameFactorySubscriber")
		{
			this.Methods = new MethodDescriptor[2];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.game_master.GameFactorySubscriber.NotifyGameFound";
			uint i = 1u;
			if (GameFactorySubscriberService.<>f__mg$cache0 == null)
			{
				GameFactorySubscriberService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameFoundNotification>);
			}
			methods[num] = new MethodDescriptor(n, i, GameFactorySubscriberService.<>f__mg$cache0);
		}

		public const uint NOTIFY_GAME_FOUND_ID = 1u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;
	}
}
