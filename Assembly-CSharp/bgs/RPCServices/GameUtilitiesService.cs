using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.game_utilities;
using bnet.protocol.server_pool;

namespace bgs.RPCServices
{
	public class GameUtilitiesService : ServiceDescriptor
	{
		public GameUtilitiesService() : base("bnet.protocol.game_utilities.GameUtilities")
		{
			this.Methods = new MethodDescriptor[11];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.game_utilities.GameUtilities.ProcessClientRequest";
			uint i = 1u;
			if (GameUtilitiesService.<>f__mg$cache0 == null)
			{
				GameUtilitiesService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ClientResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, GameUtilitiesService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.game_utilities.GameUtilities.PresenceChannelCreated";
			uint i2 = 2u;
			if (GameUtilitiesService.<>f__mg$cache1 == null)
			{
				GameUtilitiesService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, GameUtilitiesService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.game_utilities.GameUtilities.GetPlayerVariables";
			uint i3 = 3u;
			if (GameUtilitiesService.<>f__mg$cache2 == null)
			{
				GameUtilitiesService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetPlayerVariablesResponse>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, GameUtilitiesService.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)5);
			string n4 = "bnet.protocol.game_utilities.GameUtilities.GetLoad";
			uint i4 = 5u;
			if (GameUtilitiesService.<>f__mg$cache3 == null)
			{
				GameUtilitiesService.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerState>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, GameUtilitiesService.<>f__mg$cache3);
			MethodDescriptor[] methods5 = this.Methods;
			int num5 = (int)((UIntPtr)6);
			string n5 = "bnet.protocol.game_utilities.GameUtilities.ProcessServerRequest";
			uint i5 = 6u;
			if (GameUtilitiesService.<>f__mg$cache4 == null)
			{
				GameUtilitiesService.<>f__mg$cache4 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerResponse>);
			}
			methods5[num5] = new MethodDescriptor(n5, i5, GameUtilitiesService.<>f__mg$cache4);
			MethodDescriptor[] methods6 = this.Methods;
			int num6 = (int)((UIntPtr)7);
			string n6 = "bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOnline";
			uint i6 = 7u;
			if (GameUtilitiesService.<>f__mg$cache5 == null)
			{
				GameUtilitiesService.<>f__mg$cache5 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods6[num6] = new MethodDescriptor(n6, i6, GameUtilitiesService.<>f__mg$cache5);
			MethodDescriptor[] methods7 = this.Methods;
			int num7 = (int)((UIntPtr)8);
			string n7 = "bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOffline";
			uint i7 = 8u;
			if (GameUtilitiesService.<>f__mg$cache6 == null)
			{
				GameUtilitiesService.<>f__mg$cache6 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			}
			methods7[num7] = new MethodDescriptor(n7, i7, GameUtilitiesService.<>f__mg$cache6);
			MethodDescriptor[] methods8 = this.Methods;
			int num8 = (int)((UIntPtr)10);
			string n8 = "bnet.protocol.game_utilities.GameUtilities.GetAllValuesForAttribute";
			uint i8 = 10u;
			if (GameUtilitiesService.<>f__mg$cache7 == null)
			{
				GameUtilitiesService.<>f__mg$cache7 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetAllValuesForAttributeResponse>);
			}
			methods8[num8] = new MethodDescriptor(n8, i8, GameUtilitiesService.<>f__mg$cache7);
		}

		public const uint PROCESS_CLIENT_REQUEST_ID = 1u;

		public const uint PRESENCE_CHANNEL_CREATED_ID = 2u;

		public const uint GET_PLAYER_VARIABLES_ID = 3u;

		public const uint GET_LOAD_ID = 5u;

		public const uint PROCESS_SERVER_REQUEST_ID = 6u;

		public const uint NOTIFY_GAME_ACCT_ONLINE_ID = 7u;

		public const uint NOTIFY_GAME_ACCT_OFFLINE_ID = 8u;

		public const uint GET_ALL_VALUES_FOR_ATTRIBUTE_ID = 10u;

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
	}
}
