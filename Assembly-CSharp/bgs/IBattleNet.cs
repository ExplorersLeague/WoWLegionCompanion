using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account;
using bnet.protocol.game_utilities;

namespace bgs
{
	public interface IBattleNet
	{
		bool IsInitialized();

		bool Init(bool fromEditor, string username, string targetServer, int port, SslParameters sslParams, ClientInterface ci);

		ClientInterface Client();

		void AppQuit();

		void ProcessAurora();

		void QueryAurora();

		void CloseAurora();

		void RequestCloseAurora();

		int BattleNetStatus();

		void SendUtilPacket(int packetId, int systemId, byte[] bytes, int size, int subID, int context, ulong route);

		void SendClientRequest(ClientRequest request, RPCContextDelegate callback = null);

		void GetAllValuesForAttribute(string attributeKey, int context);

		void GetGameAccountState(GetGameAccountStateRequest request, RPCContextDelegate callback);

		GamesAPI.UtilResponse NextUtilPacket();

		GamesAPI.GetAllValuesForAttributeResult NextGetAllValuesForAttributeResult();

		int GetErrorsCount();

		void GetErrors([Out] BnetErrorInfo[] errors);

		void ClearErrors();

		List<bnet.protocol.EntityId> GetGameAccountList();

		bgs.types.EntityId GetMyGameAccountId();

		bgs.types.EntityId GetMyAccountId();

		string GetMyBattleTag();

		byte[] GetSessionKey();

		void GetQueueInfo(ref QueueInfo queueInfo);

		string GetVersion();

		bool IsVersionInt();

		string GetEnvironment();

		int GetPort();

		string GetUserEmailAddress();

		BattleNetLogSource GetLogSource();

		double GetRealTimeSinceStartup();

		long CurrentUTCTime();

		string GetLaunchOption(string key, bool encrypted);

		constants.BnetRegion GetAccountRegion();

		string GetAccountCountry();

		constants.BnetRegion GetCurrentRegion();

		void GetPlayRestrictions(ref Lockouts restrictions, bool reload);

		int GetShutdownMinutes();

		int GetBnetEventsSize();

		void ClearBnetEvents();

		void GetBnetEvents([Out] BattleNet.BnetEvent[] events);

		int NumChallenges();

		void ClearChallenges();

		void GetChallenges([Out] ChallengeInfo[] challenges);

		void AnswerChallenge(ulong challengeID, string answer);

		void CancelChallenge(ulong challengeID);

		void ApplicationWasPaused();

		void ApplicationWasUnpaused();

		bool CheckWebAuth(out string url);

		void ProvideWebAuthToken(string token);
	}
}
