using System;
using bnet.protocol;
using bnet.protocol.account;

namespace bgs.RPCServices
{
	public class AccountService : ServiceDescriptor
	{
		public AccountService() : base("bnet.protocol.account.AccountService")
		{
			this.Methods = new MethodDescriptor[37];
			this.Methods[(int)((UIntPtr)12)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameAccount", 12u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountBlob>));
			this.Methods[(int)((UIntPtr)13)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetAccount", 13u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAccountResponse>));
			this.Methods[(int)((UIntPtr)14)] = new MethodDescriptor("bnet.protocol.account.AccountService.CreateGameAccount", 14u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GameAccountHandle>));
			this.Methods[(int)((UIntPtr)15)] = new MethodDescriptor("bnet.protocol.account.AccountService.IsIgrAddress", 15u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)20)] = new MethodDescriptor("bnet.protocol.account.AccountService.CacheExpire", 20u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)21)] = new MethodDescriptor("bnet.protocol.account.AccountService.CredentialUpdate", 21u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[(int)((UIntPtr)22)] = new MethodDescriptor("bnet.protocol.account.AccountService.FlagUpdate", 22u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<CredentialUpdateResponse>));
			this.Methods[(int)((UIntPtr)23)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetWalletList", 23u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetWalletListResponse>));
			this.Methods[(int)((UIntPtr)24)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetEBalance", 24u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetEBalanceResponse>));
			this.Methods[(int)((UIntPtr)25)] = new MethodDescriptor("bnet.protocol.account.AccountService.Subscribe", 25u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscriptionUpdateResponse>));
			this.Methods[(int)((UIntPtr)26)] = new MethodDescriptor("bnet.protocol.account.AccountService.Unsubscribe", 26u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[(int)((UIntPtr)27)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetEBalanceRestrictions", 27u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetEBalanceRestrictionsResponse>));
			this.Methods[(int)((UIntPtr)30)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetAccountState", 30u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAccountStateResponse>));
			this.Methods[(int)((UIntPtr)31)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameAccountState", 31u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameAccountStateResponse>));
			this.Methods[(int)((UIntPtr)32)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetLicenses", 32u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetLicensesResponse>));
			this.Methods[(int)((UIntPtr)33)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameTimeRemainingInfo", 33u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameTimeRemainingInfoResponse>));
			this.Methods[(int)((UIntPtr)34)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetGameSessionInfo", 34u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameSessionInfoResponse>));
			this.Methods[(int)((UIntPtr)35)] = new MethodDescriptor("bnet.protocol.account.AccountService.GetCAISInfo", 35u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetCAISInfoResponse>));
			this.Methods[(int)((UIntPtr)36)] = new MethodDescriptor("bnet.protocol.account.AccountService.ForwardCacheExpire", 36u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
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
	}
}
