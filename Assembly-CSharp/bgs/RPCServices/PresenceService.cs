using System;
using System.Runtime.CompilerServices;
using bnet.protocol;

namespace bgs.RPCServices
{
	public class PresenceService : ServiceDescriptor
	{
		public PresenceService() : base("bnet.protocol.presence.PresenceService")
		{
			this.Methods = new MethodDescriptor[5];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.presence.PresenceService.Subscribe";
			uint i = 1u;
			if (PresenceService.<>f__mg$cache0 == null)
			{
				PresenceService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods[num] = new MethodDescriptor(n, i, PresenceService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.presence.PresenceService.Unsubscribe";
			uint i2 = 2u;
			if (PresenceService.<>f__mg$cache1 == null)
			{
				PresenceService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, PresenceService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.presence.PresenceService.Update";
			uint i3 = 3u;
			if (PresenceService.<>f__mg$cache2 == null)
			{
				PresenceService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, PresenceService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.presence.PresenceService.Query";
			uint i4 = 4u;
			if (PresenceService.<>f__mg$cache3 == null)
			{
				PresenceService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, PresenceService.<>f__mg$cache3);
		}

		public const uint SUBSCRIBE_ID = 1u;

		public const uint UNSUBSCRIBE_ID = 2u;

		public const uint UPDATE_ID = 3u;

		public const uint QUERY_ID = 4u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache1;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache2;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache3;
	}
}
