using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.connection;

namespace bgs.RPCServices
{
	public class ConnectionService : ServiceDescriptor
	{
		public ConnectionService() : base("bnet.protocol.connection.ConnectionService")
		{
			this.Methods = new MethodDescriptor[8];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.connection.ConnectionService.Connect";
			uint i = 1u;
			if (ConnectionService.<>f__mg$cache0 == null)
			{
				ConnectionService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ConnectResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, ConnectionService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.connection.ConnectionService.Bind";
			uint i2 = 2u;
			if (ConnectionService.<>f__mg$cache1 == null)
			{
				ConnectionService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<BindResponse>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ConnectionService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.connection.ConnectionService.Echo";
			uint i3 = 3u;
			if (ConnectionService.<>f__mg$cache2 == null)
			{
				ConnectionService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<EchoResponse>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ConnectionService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.connection.ConnectionService.ForceDisconnect";
			uint i4 = 4u;
			if (ConnectionService.<>f__mg$cache3 == null)
			{
				ConnectionService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ConnectionService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.connection.ConnectionService.KeepAlive";
			uint i5 = 5u;
			if (ConnectionService.<>f__mg$cache4 == null)
			{
				ConnectionService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, ConnectionService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.connection.ConnectionService.Encrypt";
			uint i6 = 6u;
			if (ConnectionService.<>f__mg$cache5 == null)
			{
				ConnectionService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, ConnectionService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.connection.ConnectionService.RequestDisconnect";
			uint i7 = 7u;
			if (ConnectionService.<>f__mg$cache6 == null)
			{
				ConnectionService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, ConnectionService.<>f__mg$cache6);
		}

		public const uint CONNECT_METHOD_ID = 1u;

		public const uint BIND_METHOD_ID = 2u;

		public const uint ECHO_METHOD_ID = 3u;

		public const uint FORCE_DISCONNECT_METHOD_ID = 4u;

		public const uint KEEP_ALIVE_METHOD_ID = 5u;

		public const uint ENCRYPT_METHOD_ID = 6u;

		public const uint REQUEST_DISCONNECT_METHOD_ID = 7u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache1;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache2;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache3;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache4;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache5;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache6;
	}
}
