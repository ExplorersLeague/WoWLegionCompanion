using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account;
using bnet.protocol.game_utilities;

namespace bgs
{
	public class BattleNet
	{
		public static IBattleNet Get()
		{
			return BattleNet.s_impl;
		}

		public static ClientInterface Client()
		{
			return BattleNet.s_impl.Client();
		}

		public static bool IsInitialized()
		{
			return BattleNet.s_impl.IsInitialized();
		}

		public static bool Init(bool internalMode, string userEmailAddress, string targetServer, int port, SslParameters sslParams, ClientInterface ci)
		{
			return BattleNet.s_impl.Init(internalMode, userEmailAddress, targetServer, port, sslParams, ci);
		}

		public static bool Reset(bool internalMode, string userEmailAddress, string targetServer, int port, SslParameters sslParams, ClientInterface ci)
		{
			BattleNet.RequestCloseAurora();
			BattleNet.s_impl = new BattleNetCSharp();
			return BattleNet.Init(internalMode, userEmailAddress, targetServer, port, sslParams, ci);
		}

		public static void AppQuit()
		{
			BattleNet.s_impl.AppQuit();
		}

		public static void ProcessAurora()
		{
			BattleNet.s_impl.ProcessAurora();
		}

		public static void QueryAurora()
		{
			BattleNet.s_impl.QueryAurora();
		}

		public static void CloseAurora()
		{
			BattleNet.s_impl.CloseAurora();
		}

		public static void RequestCloseAurora()
		{
			BattleNet.s_impl.RequestCloseAurora();
		}

		public static int BattleNetStatus()
		{
			return BattleNet.s_impl.BattleNetStatus();
		}

		public static void SendUtilPacket(int packetId, int systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			BattleNet.s_impl.SendUtilPacket(packetId, systemId, bytes, size, subID, context, route);
		}

		public static void SendClientRequest(ClientRequest request, RPCContextDelegate callback = null)
		{
			BattleNet.s_impl.SendClientRequest(request, callback);
		}

		public static void GetAllValuesForAttribute(string attributeKey, int context)
		{
			BattleNet.s_impl.GetAllValuesForAttribute(attributeKey, context);
		}

		public static void GetGameAccountState(GetGameAccountStateRequest request, RPCContextDelegate callback)
		{
			BattleNet.s_impl.GetGameAccountState(request, callback);
		}

		public static GamesAPI.UtilResponse NextUtilPacket()
		{
			return BattleNet.s_impl.NextUtilPacket();
		}

		public static GamesAPI.GetAllValuesForAttributeResult NextGetAllValuesForAttributeResult()
		{
			return BattleNet.s_impl.NextGetAllValuesForAttributeResult();
		}

		public static int GetErrorsCount()
		{
			return BattleNet.s_impl.GetErrorsCount();
		}

		public static void GetErrors([Out] BnetErrorInfo[] errors)
		{
			BattleNet.s_impl.GetErrors(errors);
		}

		public static void ClearErrors()
		{
			BattleNet.s_impl.ClearErrors();
		}

		public static List<bnet.protocol.EntityId> GetGameAccountList()
		{
			return BattleNet.s_impl.GetGameAccountList();
		}

		public static bgs.types.EntityId GetMyGameAccountId()
		{
			return BattleNet.s_impl.GetMyGameAccountId();
		}

		public static string GetMyBattleTag()
		{
			return BattleNet.s_impl.GetMyBattleTag();
		}

		public static bgs.types.EntityId GetMyAccountId()
		{
			return BattleNet.s_impl.GetMyAccountId();
		}

		public static byte[] GetSessionKey()
		{
			return BattleNet.s_impl.GetSessionKey();
		}

		public static void GetQueueInfo(ref QueueInfo queueInfo)
		{
			BattleNet.s_impl.GetQueueInfo(ref queueInfo);
		}

		public static BattleNetLogSource Log
		{
			get
			{
				return BattleNet.s_impl.GetLogSource();
			}
		}

		public static string GetVersion()
		{
			return BattleNet.s_impl.GetVersion();
		}

		public static bool IsVersionInt()
		{
			return BattleNet.s_impl.IsVersionInt();
		}

		public static string GetEnvironment()
		{
			return BattleNet.s_impl.GetEnvironment();
		}

		public static int GetPort()
		{
			return BattleNet.s_impl.GetPort();
		}

		public static string GetUserEmailAddress()
		{
			return BattleNet.s_impl.GetUserEmailAddress();
		}

		public static long CurrentUTCTime()
		{
			return BattleNet.s_impl.CurrentUTCTime();
		}

		public static double GetRealTimeSinceStartup()
		{
			return BattleNet.s_impl.GetRealTimeSinceStartup();
		}

		public static string GetLaunchOption(string key, bool encrypted)
		{
			return BattleNet.s_impl.GetLaunchOption(key, encrypted);
		}

		public static constants.BnetRegion GetAccountRegion()
		{
			return BattleNet.s_impl.GetAccountRegion();
		}

		public static string GetAccountCountry()
		{
			return BattleNet.s_impl.GetAccountCountry();
		}

		public static constants.BnetRegion GetCurrentRegion()
		{
			return BattleNet.s_impl.GetCurrentRegion();
		}

		public static void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			BattleNet.s_impl.GetPlayRestrictions(ref restrictions, reload);
		}

		public static int GetShutdownMinutes()
		{
			return BattleNet.s_impl.GetShutdownMinutes();
		}

		public static int GetBnetEventsSize()
		{
			return BattleNet.s_impl.GetBnetEventsSize();
		}

		public static void ClearBnetEvents()
		{
			BattleNet.s_impl.ClearBnetEvents();
		}

		public static void GetBnetEvents([Out] BattleNet.BnetEvent[] events)
		{
			BattleNet.s_impl.GetBnetEvents(events);
		}

		public static bool CheckWebAuth(out string url)
		{
			return BattleNet.s_impl.CheckWebAuth(out url);
		}

		public static void ProvideWebAuthToken(string token)
		{
			BattleNet.s_impl.ProvideWebAuthToken(token);
		}

		public static int NumChallenges()
		{
			return BattleNet.s_impl.NumChallenges();
		}

		public static void ClearChallenges()
		{
			BattleNet.s_impl.ClearChallenges();
		}

		public static void GetChallenges([Out] ChallengeInfo[] challenges)
		{
			BattleNet.s_impl.GetChallenges(challenges);
		}

		public static void AnswerChallenge(ulong challengeID, string answer)
		{
			BattleNet.s_impl.AnswerChallenge(challengeID, answer);
		}

		public static void CancelChallenge(ulong challengeID)
		{
			BattleNet.s_impl.CancelChallenge(challengeID);
		}

		public static void ApplicationWasPaused()
		{
			BattleNet.s_impl.ApplicationWasPaused();
		}

		public static void ApplicationWasUnpaused()
		{
			BattleNet.s_impl.ApplicationWasUnpaused();
		}

		private static IBattleNet s_impl = new BattleNetCSharp();

		public const string COUNTRY_KOREA = "KOR";

		public const string COUNTRY_CHINA = "CHN";

		public enum BnetEvent
		{
			Disconnected
		}
	}
}
