using System;
using System.Runtime.CompilerServices;
using bnet.protocol;

namespace bgs.RPCServices
{
	public class ResourcesService : ServiceDescriptor
	{
		public ResourcesService() : base("bnet.protocol.resources.Resources")
		{
			this.Methods = new MethodDescriptor[2];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.resources.Resources.GetContentHandle";
			uint i = 1u;
			if (ResourcesService.<>f__mg$cache0 == null)
			{
				ResourcesService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ContentHandle>);
			}
			methods[num] = new MethodDescriptor(n, i, ResourcesService.<>f__mg$cache0);
		}

		public const uint GET_CONTENT_HANDLE = 1u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;
	}
}
