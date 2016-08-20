using System;
using bnet.protocol.account;

namespace bgs.RPCServices
{
	public class AccountNotify : ServiceDescriptor
	{
		public AccountNotify() : base("bnet.protocol.account.AccountNotify")
		{
			this.Methods = new MethodDescriptor[5];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyAccountStateUpdated", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AccountStateNotification>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameAccountStateUpdated", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountStateNotification>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameAccountsUpdated", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountNotification>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.account.AccountNotify.NotifyGameSessionUpdated", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountSessionNotification>));
		}

		public const uint NOTIFY_ACCOUNT_STATE_UPDATED_ID = 1u;

		public const uint NOTIFY_GAME_ACCOUNT_STATE_UPDATED_ID = 2u;

		public const uint NOTIFY_GAME_ACCOUNTS_UPDATED_ID = 3u;

		public const uint NOTIFY_GAME_SESSION_UPDATED_ID = 4u;
	}
}
