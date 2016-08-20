using System;
using bnet.protocol;
using bnet.protocol.authentication;

namespace bgs.RPCServices
{
	public class AuthServerService : ServiceDescriptor
	{
		public AuthServerService() : base("bnet.protocol.authentication.AuthenticationServer")
		{
			this.Methods = new MethodDescriptor[8];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.Logon", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.ModuleNotify", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.ModuleMessage", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.SelectGameAccount_DEPRECATED", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.GenerateTempCookie", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GenerateSSOTokenResponse>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.SelectGameAccount", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)7)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationServer.VerifyWebCredentials", 7u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		public const uint LOGON_METHOD_ID = 1u;

		public const uint MODULE_NOTIFY_METHOD_ID = 2u;

		public const uint MODULE_MESSAGE_METHOD_ID = 3u;

		public const uint SELECT_GAME_ACCT_DEPRECATED_METHOD_ID = 4u;

		public const uint GEN_TEMP_COOKIE_METHOD_ID = 5u;

		public const uint SELECT_GAME_ACCT_METHOD_ID = 6u;

		public const uint VERIFY_WEB_CREDENTIALS_METHOD_ID = 7u;
	}
}
