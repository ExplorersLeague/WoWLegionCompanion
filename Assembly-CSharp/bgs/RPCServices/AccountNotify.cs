using System;
using System.Runtime.CompilerServices;
using bnet.protocol.account;

namespace bgs.RPCServices
{
	public class AccountNotify : ServiceDescriptor
	{
		public AccountNotify() : base("bnet.protocol.account.AccountNotify")
		{
			this.Methods = new MethodDescriptor[5];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.account.AccountNotify.NotifyAccountStateUpdated";
			uint i = 1u;
			if (AccountNotify.<>f__mg$cache0 == null)
			{
				AccountNotify.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<AccountStateNotification>);
			}
			methods[num] = new MethodDescriptor(n, i, AccountNotify.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.account.AccountNotify.NotifyGameAccountStateUpdated";
			uint i2 = 2u;
			if (AccountNotify.<>f__mg$cache1 == null)
			{
				AccountNotify.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountStateNotification>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, AccountNotify.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.account.AccountNotify.NotifyGameAccountsUpdated";
			uint i3 = 3u;
			if (AccountNotify.<>f__mg$cache2 == null)
			{
				AccountNotify.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountNotification>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, AccountNotify.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.account.AccountNotify.NotifyGameSessionUpdated";
			uint i4 = 4u;
			if (AccountNotify.<>f__mg$cache3 == null)
			{
				AccountNotify.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountSessionNotification>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, AccountNotify.<>f__mg$cache3);
		}

		public const uint NOTIFY_ACCOUNT_STATE_UPDATED_ID = 1u;

		public const uint NOTIFY_GAME_ACCOUNT_STATE_UPDATED_ID = 2u;

		public const uint NOTIFY_GAME_ACCOUNTS_UPDATED_ID = 3u;

		public const uint NOTIFY_GAME_SESSION_UPDATED_ID = 4u;

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
