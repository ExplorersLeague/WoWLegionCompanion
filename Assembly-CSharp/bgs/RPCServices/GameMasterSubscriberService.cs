using System;
using System.Runtime.CompilerServices;
using bnet.protocol.game_master;

namespace bgs.RPCServices
{
	public class GameMasterSubscriberService : ServiceDescriptor
	{
		public GameMasterSubscriberService() : base("bnet.protocol.game_master.GameMasterSubscriber")
		{
			this.Methods = new MethodDescriptor[2];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.game_master.GameMasterSubscriber.NotifyFactoryUpdate";
			uint i = 1u;
			if (GameMasterSubscriberService.<>f__mg$cache0 == null)
			{
				GameMasterSubscriberService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FactoryUpdateNotification>);
			}
			methods[num] = new MethodDescriptor(n, i, GameMasterSubscriberService.<>f__mg$cache0);
		}

		public const uint NOTIFY_FACTORY_UPDATE_ID = 1u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;
	}
}
