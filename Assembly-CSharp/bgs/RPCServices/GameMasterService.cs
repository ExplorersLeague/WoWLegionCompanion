using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.game_master;

namespace bgs.RPCServices
{
	public class GameMasterService : ServiceDescriptor
	{
		public GameMasterService() : base("bnet.protocol.game_master.GameMaster")
		{
			this.Methods = new MethodDescriptor[16];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.game_master.GameMaster.JoinGame";
			uint i = 1u;
			if (GameMasterService.<>f__mg$cache0 == null)
			{
				GameMasterService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<JoinGameResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, GameMasterService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.game_master.GameMaster.ListFactories";
			uint i2 = 2u;
			if (GameMasterService.<>f__mg$cache1 == null)
			{
				GameMasterService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ListFactoriesResponse>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, GameMasterService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.game_master.GameMaster.FindGame";
			uint i3 = 3u;
			if (GameMasterService.<>f__mg$cache2 == null)
			{
				GameMasterService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<FindGameResponse>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, GameMasterService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.game_master.GameMaster.CancelGameEntry";
			uint i4 = 4u;
			if (GameMasterService.<>f__mg$cache3 == null)
			{
				GameMasterService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, GameMasterService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)5);
			string n5 = "bnet.protocol.game_master.GameMaster.GameEnded";
			uint i5 = 5u;
			if (GameMasterService.<>f__mg$cache4 == null)
			{
				GameMasterService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, GameMasterService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)6);
			string n6 = "bnet.protocol.game_master.GameMaster.PlayerLeft";
			uint i6 = 6u;
			if (GameMasterService.<>f__mg$cache5 == null)
			{
				GameMasterService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, GameMasterService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)7);
			string n7 = "bnet.protocol.game_master.GameMaster.RegisterServer";
			uint i7 = 7u;
			if (GameMasterService.<>f__mg$cache6 == null)
			{
				GameMasterService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, GameMasterService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)8);
			string n8 = "bnet.protocol.game_master.GameMaster.UnregisterServer";
			uint i8 = 8u;
			if (GameMasterService.<>f__mg$cache7 == null)
			{
				GameMasterService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, GameMasterService.<>f__mg$cache7);
			MethodDescriptor[] methods9 = this.Methods;
			int num9 = (int)((UIntPtr)9);
			string n9 = "bnet.protocol.game_master.GameMaster.RegisterUtilities";
			uint i9 = 9u;
			if (GameMasterService.<>f__mg$cache8 == null)
			{
				GameMasterService.<>f__mg$cache8 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods9[num9] = new MethodDescriptor(n9, i9, GameMasterService.<>f__mg$cache8);
			MethodDescriptor[] methods10 = this.Methods;
			int num10 = (int)((UIntPtr)10);
			string n10 = "bnet.protocol.game_master.GameMaster.UnregisterUtilities";
			uint i10 = 10u;
			if (GameMasterService.<>f__mg$cache9 == null)
			{
				GameMasterService.<>f__mg$cache9 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods10[num10] = new MethodDescriptor(n10, i10, GameMasterService.<>f__mg$cache9);
			MethodDescriptor[] methods11 = this.Methods;
			int num11 = (int)((UIntPtr)11);
			string n11 = "bnet.protocol.game_master.GameMaster.Subscribe";
			uint i11 = 11u;
			if (GameMasterService.<>f__mg$cacheA == null)
			{
				GameMasterService.<>f__mg$cacheA = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SubscribeResponse>);
			}
			methods11[num11] = new MethodDescriptor(n11, i11, GameMasterService.<>f__mg$cacheA);
			MethodDescriptor[] methods12 = this.Methods;
			int num12 = (int)((UIntPtr)12);
			string n12 = "bnet.protocol.game_master.GameMaster.Unsubscribe";
			uint i12 = 12u;
			if (GameMasterService.<>f__mg$cacheB == null)
			{
				GameMasterService.<>f__mg$cacheB = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods12[num12] = new MethodDescriptor(n12, i12, GameMasterService.<>f__mg$cacheB);
			MethodDescriptor[] methods13 = this.Methods;
			int num13 = (int)((UIntPtr)13);
			string n13 = "bnet.protocol.game_master.GameMaster.ChangeGame";
			uint i13 = 13u;
			if (GameMasterService.<>f__mg$cacheC == null)
			{
				GameMasterService.<>f__mg$cacheC = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods13[num13] = new MethodDescriptor(n13, i13, GameMasterService.<>f__mg$cacheC);
			MethodDescriptor[] methods14 = this.Methods;
			int num14 = (int)((UIntPtr)14);
			string n14 = "bnet.protocol.game_master.GameMaster.GetFactoryInfo";
			uint i14 = 14u;
			if (GameMasterService.<>f__mg$cacheD == null)
			{
				GameMasterService.<>f__mg$cacheD = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetFactoryInfoResponse>);
			}
			methods14[num14] = new MethodDescriptor(n14, i14, GameMasterService.<>f__mg$cacheD);
			MethodDescriptor[] methods15 = this.Methods;
			int num15 = (int)((UIntPtr)15);
			string n15 = "bnet.protocol.game_master.GameMaster.GetGameStats";
			uint i15 = 15u;
			if (GameMasterService.<>f__mg$cacheE == null)
			{
				GameMasterService.<>f__mg$cacheE = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetGameStatsResponse>);
			}
			methods15[num15] = new MethodDescriptor(n15, i15, GameMasterService.<>f__mg$cacheE);
		}

		public const uint JOIN_GAME_ID = 1u;

		public const uint LIST_FACTORIES_ID = 2u;

		public const uint FIND_GAME_ID = 3u;

		public const uint CANCEL_GAME_ENTRY_ID = 4u;

		public const uint GAME_ENDED_ID = 5u;

		public const uint PLAYER_LEFT_ID = 6u;

		public const uint REGISTER_SERVER_ID = 7u;

		public const uint UNREGISTER_SERVER_ID = 8u;

		public const uint REGISTER_UTILITIES_ID = 9u;

		public const uint UNREGISTER_UTILITIES_ID = 10u;

		public const uint SUBSCRIBE_ID = 11u;

		public const uint UNSUBSCRIBE_ID = 12u;

		public const uint CHANGE_GAME_ID = 13u;

		public const uint GET_FACTORY_INFO_ID = 14u;

		public const uint GET_GAME_STATS_ID = 15u;

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
	}
}
