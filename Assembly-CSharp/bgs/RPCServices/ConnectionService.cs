using System;
using bnet.protocol;
using bnet.protocol.connection;

namespace bgs.RPCServices
{
	public class ConnectionService : ServiceDescriptor
	{
		public ConnectionService() : base("bnet.protocol.connection.ConnectionService")
		{
			this.Methods = new MethodDescriptor[8];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Connect", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ConnectResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Bind", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<BindResponse>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Echo", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<EchoResponse>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.ForceDisconnect", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.KeepAlive", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.Encrypt", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.connection.ConnectionService.RequestDisconnect", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
		}

		public const uint CONNECT_METHOD_ID = 1u;

		public const uint BIND_METHOD_ID = 2u;

		public const uint ECHO_METHOD_ID = 3u;

		public const uint FORCE_DISCONNECT_METHOD_ID = 4u;

		public const uint KEEP_ALIVE_METHOD_ID = 5u;

		public const uint ENCRYPT_METHOD_ID = 6u;

		public const uint REQUEST_DISCONNECT_METHOD_ID = 7u;
	}
}
