using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.authentication;

namespace bgs.RPCServices
{
	public class AuthServerService : ServiceDescriptor
	{
		public AuthServerService() : base("bnet.protocol.authentication.AuthenticationServer")
		{
			this.Methods = new MethodDescriptor[8];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.authentication.AuthenticationServer.Logon";
			uint i = 1u;
			if (AuthServerService.<>f__mg$cache0 == null)
			{
				AuthServerService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods[num] = new MethodDescriptor(n, i, AuthServerService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.authentication.AuthenticationServer.ModuleNotify";
			uint i2 = 2u;
			if (AuthServerService.<>f__mg$cache1 == null)
			{
				AuthServerService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, AuthServerService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.authentication.AuthenticationServer.ModuleMessage";
			uint i3 = 3u;
			if (AuthServerService.<>f__mg$cache2 == null)
			{
				AuthServerService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, AuthServerService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.authentication.AuthenticationServer.SelectGameAccount_DEPRECATED";
			uint i4 = 4u;
			if (AuthServerService.<>f__mg$cache3 == null)
			{
				AuthServerService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, AuthServerService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.authentication.AuthenticationServer.GenerateTempCookie";
			uint i5 = 5u;
			if (AuthServerService.<>f__mg$cache4 == null)
			{
				AuthServerService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GenerateSSOTokenResponse>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, AuthServerService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.authentication.AuthenticationServer.SelectGameAccount";
			uint i6 = 6u;
			if (AuthServerService.<>f__mg$cache5 == null)
			{
				AuthServerService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, AuthServerService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.authentication.AuthenticationServer.VerifyWebCredentials";
			uint i7 = 7u;
			if (AuthServerService.<>f__mg$cache6 == null)
			{
				AuthServerService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, AuthServerService.<>f__mg$cache6);
		}

		public const uint LOGON_METHOD_ID = 1u;

		public const uint MODULE_NOTIFY_METHOD_ID = 2u;

		public const uint MODULE_MESSAGE_METHOD_ID = 3u;

		public const uint SELECT_GAME_ACCT_DEPRECATED_METHOD_ID = 4u;

		public const uint GEN_TEMP_COOKIE_METHOD_ID = 5u;

		public const uint SELECT_GAME_ACCT_METHOD_ID = 6u;

		public const uint VERIFY_WEB_CREDENTIALS_METHOD_ID = 7u;

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
