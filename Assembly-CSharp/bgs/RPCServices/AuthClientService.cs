using System;
using bnet.protocol;
using bnet.protocol.authentication;

namespace bgs.RPCServices
{
	public class AuthClientService : ServiceDescriptor
	{
		public AuthClientService() : base("bnet.protocol.authentication.AuthenticationClient")
		{
			this.Methods = new MethodDescriptor[15];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ModuleLoad", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ModuleLoadRequest>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ModuleMessage", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ModuleMessageRequest>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.AccountSettings", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AccountSettingsNotification>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.ServerStateChange", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerStateChangeRequest>));
			this.Methods[(int)((UIntPtr)5)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonComplete", 5u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonResult>));
			this.Methods[(int)((UIntPtr)6)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.MemModuleLoad", 6u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<MemModuleLoadRequest>));
			this.Methods[(int)((UIntPtr)10)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonUpdate", 10u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonUpdateRequest>));
			this.Methods[(int)((UIntPtr)11)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.VersionInfoUpdated", 11u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<VersionInfoNotification>));
			this.Methods[(int)((UIntPtr)12)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonQueueUpdate", 12u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<LogonQueueUpdateRequest>));
			this.Methods[(int)((UIntPtr)13)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.LogonQueueEnd", 13u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)14)] = new MethodDescriptor("bnet.protocol.authentication.AuthenticationClient.GameAccountSelected", 14u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountSelectedRequest>));
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
	}
}
