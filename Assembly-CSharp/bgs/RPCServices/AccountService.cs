using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.account;

namespace bgs.RPCServices
{
	public class AccountService : ServiceDescriptor
	{
		public AccountService() : base("bnet.protocol.account.AccountService")
		{
			this.Methods = new MethodDescriptor[37];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)12);
			string n = "bnet.protocol.account.AccountService.GetGameAccount";
			uint i = 12u;
			if (AccountService.<>f__mg$cache0 == null)
			{
				AccountService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountBlob>);
			}
			methods[num] = new MethodDescriptor(n, i, AccountService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)13);
			string n2 = "bnet.protocol.account.AccountService.GetAccount";
			uint i2 = 13u;
			if (AccountService.<>f__mg$cache1 == null)
			{
				AccountService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAccountResponse>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, AccountService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)14);
			string n3 = "bnet.protocol.account.AccountService.CreateGameAccount";
			uint i3 = 14u;
			if (AccountService.<>f__mg$cache2 == null)
			{
				AccountService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountHandle>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, AccountService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)15);
			string n4 = "bnet.protocol.account.AccountService.IsIgrAddress";
			uint i4 = 15u;
			if (AccountService.<>f__mg$cache3 == null)
			{
				AccountService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, AccountService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)20);
			string n5 = "bnet.protocol.account.AccountService.CacheExpire";
			uint i5 = 20u;
			if (AccountService.<>f__mg$cache4 == null)
			{
				AccountService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, AccountService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)21);
			string n6 = "bnet.protocol.account.AccountService.CredentialUpdate";
			uint i6 = 21u;
			if (AccountService.<>f__mg$cache5 == null)
			{
				AccountService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, AccountService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)22);
			string n7 = "bnet.protocol.account.AccountService.FlagUpdate";
			uint i7 = 22u;
			if (AccountService.<>f__mg$cache6 == null)
			{
				AccountService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<CredentialUpdateResponse>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, AccountService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)23);
			string n8 = "bnet.protocol.account.AccountService.GetWalletList";
			uint i8 = 23u;
			if (AccountService.<>f__mg$cache7 == null)
			{
				AccountService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetWalletListResponse>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, AccountService.<>f__mg$cache7);
			MethodDescriptor[] methods9 = this.Methods;
			int num9 = (int)((UIntPtr)24);
			string n9 = "bnet.protocol.account.AccountService.GetEBalance";
			uint i9 = 24u;
			if (AccountService.<>f__mg$cache8 == null)
			{
				AccountService.<>f__mg$cache8 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetEBalanceResponse>);
			}
			methods9[num9] = new MethodDescriptor(n9, i9, AccountService.<>f__mg$cache8);
			MethodDescriptor[] methods10 = this.Methods;
			int num10 = (int)((UIntPtr)25);
			string n10 = "bnet.protocol.account.AccountService.Subscribe";
			uint i10 = 25u;
			if (AccountService.<>f__mg$cache9 == null)
			{
				AccountService.<>f__mg$cache9 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscriptionUpdateResponse>);
			}
			methods10[num10] = new MethodDescriptor(n10, i10, AccountService.<>f__mg$cache9);
			MethodDescriptor[] methods11 = this.Methods;
			int num11 = (int)((UIntPtr)26);
			string n11 = "bnet.protocol.account.AccountService.Unsubscribe";
			uint i11 = 26u;
			if (AccountService.<>f__mg$cacheA == null)
			{
				AccountService.<>f__mg$cacheA = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods11[num11] = new MethodDescriptor(n11, i11, AccountService.<>f__mg$cacheA);
			MethodDescriptor[] methods12 = this.Methods;
			int num12 = (int)((UIntPtr)27);
			string n12 = "bnet.protocol.account.AccountService.GetEBalanceRestrictions";
			uint i12 = 27u;
			if (AccountService.<>f__mg$cacheB == null)
			{
				AccountService.<>f__mg$cacheB = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetEBalanceRestrictionsResponse>);
			}
			methods12[num12] = new MethodDescriptor(n12, i12, AccountService.<>f__mg$cacheB);
			MethodDescriptor[] methods13 = this.Methods;
			int num13 = (int)((UIntPtr)30);
			string n13 = "bnet.protocol.account.AccountService.GetAccountState";
			uint i13 = 30u;
			if (AccountService.<>f__mg$cacheC == null)
			{
				AccountService.<>f__mg$cacheC = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAccountStateResponse>);
			}
			methods13[num13] = new MethodDescriptor(n13, i13, AccountService.<>f__mg$cacheC);
			MethodDescriptor[] methods14 = this.Methods;
			int num14 = (int)((UIntPtr)31);
			string n14 = "bnet.protocol.account.AccountService.GetGameAccountState";
			uint i14 = 31u;
			if (AccountService.<>f__mg$cacheD == null)
			{
				AccountService.<>f__mg$cacheD = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameAccountStateResponse>);
			}
			methods14[num14] = new MethodDescriptor(n14, i14, AccountService.<>f__mg$cacheD);
			MethodDescriptor[] methods15 = this.Methods;
			int num15 = (int)((UIntPtr)32);
			string n15 = "bnet.protocol.account.AccountService.GetLicenses";
			uint i15 = 32u;
			if (AccountService.<>f__mg$cacheE == null)
			{
				AccountService.<>f__mg$cacheE = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetLicensesResponse>);
			}
			methods15[num15] = new MethodDescriptor(n15, i15, AccountService.<>f__mg$cacheE);
			MethodDescriptor[] methods16 = this.Methods;
			int num16 = (int)((UIntPtr)33);
			string n16 = "bnet.protocol.account.AccountService.GetGameTimeRemainingInfo";
			uint i16 = 33u;
			if (AccountService.<>f__mg$cacheF == null)
			{
				AccountService.<>f__mg$cacheF = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameTimeRemainingInfoResponse>);
			}
			methods16[num16] = new MethodDescriptor(n16, i16, AccountService.<>f__mg$cacheF);
			MethodDescriptor[] methods17 = this.Methods;
			int num17 = (int)((UIntPtr)34);
			string n17 = "bnet.protocol.account.AccountService.GetGameSessionInfo";
			uint i17 = 34u;
			if (AccountService.<>f__mg$cache10 == null)
			{
				AccountService.<>f__mg$cache10 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameSessionInfoResponse>);
			}
			methods17[num17] = new MethodDescriptor(n17, i17, AccountService.<>f__mg$cache10);
			MethodDescriptor[] methods18 = this.Methods;
			int num18 = (int)((UIntPtr)35);
			string n18 = "bnet.protocol.account.AccountService.GetCAISInfo";
			uint i18 = 35u;
			if (AccountService.<>f__mg$cache11 == null)
			{
				AccountService.<>f__mg$cache11 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetCAISInfoResponse>);
			}
			methods18[num18] = new MethodDescriptor(n18, i18, AccountService.<>f__mg$cache11);
			MethodDescriptor[] methods19 = this.Methods;
			int num19 = (int)((UIntPtr)36);
			string n19 = "bnet.protocol.account.AccountService.ForwardCacheExpire";
			uint i19 = 36u;
			if (AccountService.<>f__mg$cache12 == null)
			{
				AccountService.<>f__mg$cache12 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods19[num19] = new MethodDescriptor(n19, i19, AccountService.<>f__mg$cache12);
		}

		public const uint GET_GAME_ACCOUNT_ID = 12u;

		public const uint GET_ACCOUNT_ID = 13u;

		public const uint CREATE_GAME_ACCOUNT_ID = 14u;

		public const uint IS_IGR_ADDRESS_ID = 15u;

		public const uint CACHE_EXPIRE_ID = 20u;

		public const uint CREDENTIAL_UPDATE_ID = 21u;

		public const uint FLAG_UPDATE_ID = 22u;

		public const uint GET_WALLET_LIST_ID = 23u;

		public const uint GET_EBALANCE_ID = 24u;

		public const uint SUBSCRIBE_ID = 25u;

		public const uint UNSUBSCRIBE_ID = 26u;

		public const uint GET_EBALANCE_RESTRICTIONS_ID = 27u;

		public const uint GET_ACCOUNT_STATE_ID = 30u;

		public const uint GET_GAME_ACCOUNT_STATE_ID = 31u;

		public const uint GET_LICENSES_ID = 32u;

		public const uint GET_GAME_TIME_REMAINING_INFO_ID = 33u;

		public const uint GET_GAME_SESSION_INFO_ID = 34u;

		public const uint GET_CAIS_INFO_ID = 35u;

		public const uint FORWARD_CACHE_EXPIRE_ID = 36u;

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

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheB;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheC;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheD;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheE;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cacheF;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache10;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache11;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache12;
	}
}
