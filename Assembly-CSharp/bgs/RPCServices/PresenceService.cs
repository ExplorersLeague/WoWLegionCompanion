using System;
using bnet.protocol;

namespace bgs.RPCServices
{
	public class PresenceService : ServiceDescriptor
	{
		public PresenceService() : base("bnet.protocol.presence.PresenceService")
		{
			this.Methods = new MethodDescriptor[5];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Subscribe", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Unsubscribe", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Update", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.presence.PresenceService.Query", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		public const uint SUBSCRIBE_ID = 1u;

		public const uint UNSUBSCRIBE_ID = 2u;

		public const uint UPDATE_ID = 3u;

		public const uint QUERY_ID = 4u;
	}
}
