using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.authentication;

namespace bgs.RPCServices
{
	public class AuthClientService : ServiceDescriptor
	{
		public AuthClientService() : base("bnet.protocol.authentication.AuthenticationClient")
		{
			this.Methods = new MethodDescriptor[15];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.authentication.AuthenticationClient.ModuleLoad";
			uint i = 1u;
			if (AuthClientService.<>f__mg$cache0 == null)
			{
				AuthClientService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ModuleLoadRequest>);
			}
			methods[num] = new MethodDescriptor(n, i, AuthClientService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.authentication.AuthenticationClient.ModuleMessage";
			uint i2 = 2u;
			if (AuthClientService.<>f__mg$cache1 == null)
			{
				AuthClientService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ModuleMessageRequest>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, AuthClientService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.authentication.AuthenticationClient.AccountSettings";
			uint i3 = 3u;
			if (AuthClientService.<>f__mg$cache2 == null)
			{
				AuthClientService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AccountSettingsNotification>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, AuthClientService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.authentication.AuthenticationClient.ServerStateChange";
			uint i4 = 4u;
			if (AuthClientService.<>f__mg$cache3 == null)
			{
				AuthClientService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerStateChangeRequest>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, AuthClientService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.authentication.AuthenticationClient.LogonComplete";
			uint i5 = 5u;
			if (AuthClientService.<>f__mg$cache4 == null)
			{
				AuthClientService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonResult>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, AuthClientService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.authentication.AuthenticationClient.MemModuleLoad";
			uint i6 = 6u;
			if (AuthClientService.<>f__mg$cache5 == null)
			{
				AuthClientService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemModuleLoadRequest>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, AuthClientService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)10);
			string n7 = "bnet.protocol.authentication.AuthenticationClient.LogonUpdate";
			uint i7 = 10u;
			if (AuthClientService.<>f__mg$cache6 == null)
			{
				AuthClientService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonUpdateRequest>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, AuthClientService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)11);
			string n8 = "bnet.protocol.authentication.AuthenticationClient.VersionInfoUpdated";
			uint i8 = 11u;
			if (AuthClientService.<>f__mg$cache7 == null)
			{
				AuthClientService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<VersionInfoNotification>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, AuthClientService.<>f__mg$cache7);
			MethodDescriptor[] methods9 = this.Methods;
			int num9 = (int)((UIntPtr)12);
			string n9 = "bnet.protocol.authentication.AuthenticationClient.LogonQueueUpdate";
			uint i9 = 12u;
			if (AuthClientService.<>f__mg$cache8 == null)
			{
				AuthClientService.<>f__mg$cache8 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonQueueUpdateRequest>);
			}
			methods9[num9] = new MethodDescriptor(n9, i9, AuthClientService.<>f__mg$cache8);
			MethodDescriptor[] methods10 = this.Methods;
			int num10 = (int)((UIntPtr)13);
			string n10 = "bnet.protocol.authentication.AuthenticationClient.LogonQueueEnd";
			uint i10 = 13u;
			if (AuthClientService.<>f__mg$cache9 == null)
			{
				AuthClientService.<>f__mg$cache9 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods10[num10] = new MethodDescriptor(n10, i10, AuthClientService.<>f__mg$cache9);
			MethodDescriptor[] methods11 = this.Methods;
			int num11 = (int)((UIntPtr)14);
			string n11 = "bnet.protocol.authentication.AuthenticationClient.GameAccountSelected";
			uint i11 = 14u;
			if (AuthClientService.<>f__mg$cacheA == null)
			{
				AuthClientService.<>f__mg$cacheA = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountSelectedRequest>);
			}
			methods11[num11] = new MethodDescriptor(n11, i11, AuthClientService.<>f__mg$cacheA);
		}

		public const uint MODULE_LOAD_METHOD_ID = 1u;

		public const uint MODULE_MESSAGE_METHOD_ID = 2u;

		public const uint ACCOUNT_SETTINGS_METHOD_ID = 3u;

		public const uint SERVER_STATE_CHANGE_METHOD_ID = 4u;

		public const uint LOGON_COMPLETE_METHOD_ID = 5u;

		public const uint MEM_MODULE_LOAD_METHOD_ID = 6u;

		public const uint LOGON_UPDATE_METHOD_ID = 10u;

		public const uint VERSION_INFO_UPDATED_ID = 11u;

		public const uint LOGON_QUEUE_UPDATE_ID = 12u;

		public const uint LOGON_QUEUE_END_ID = 13u;

		public const uint GAME_ACCOUNT_SELECTED = 14u;

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

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache7;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache8;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache9;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheA;
	}
}
