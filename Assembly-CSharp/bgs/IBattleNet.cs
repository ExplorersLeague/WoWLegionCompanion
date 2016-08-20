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

		void FindGame(byte[] requestGuid, int gameType, int missionId, long deckId, long aiDeckId, bool setScenarioIdAttr);

		void CancelFindGame();

		QueueEvent GetQueueEvent();

		int PresenceSize();

		void ClearPresence();

		void GetPresence([Out] PresenceUpdate[] updates);

		void SetPresenceBool(uint field, bool val);

		void SetPresenceInt(uint field, long val);

		void SetPresenceString(uint field, string val);

		void SetPresenceBlob(uint field, byte[] val);

		void SetRichPresence([In] RichPresenceUpdate[] updates);

		void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList);

		void SendFriendlyChallengeInvite(ref bgs.types.EntityId gameAccount, int scenarioId);

		void SetMyFriendlyChallengeDeck(ref bgs.types.EntityId partyId, long deckID);

		void GetPartyUpdatesInfo(ref PartyInfo info);

		void GetPartyUpdates([Out] PartyEvent[] updates);

		void ClearPartyUpdates();

		void DeclineFriendlyChallenge(ref bgs.types.EntityId partyId);

		void AcceptFriendlyChallenge(ref bgs.types.EntityId partyId);

		void RescindFriendlyChallenge(ref bgs.types.EntityId partyId);

		void CreateParty(string partyType, int privacyLevel, byte[] creatorBlob);

		void JoinParty(bgs.types.EntityId partyId, string partyType);

		void LeaveParty(bgs.types.EntityId partyId);

		void DissolveParty(bgs.types.EntityId partyId);

		void SetPartyPrivacy(bgs.types.EntityId partyId, int privacyLevel);

		void AssignPartyRole(bgs.types.EntityId partyId, bgs.types.EntityId memberId, uint roleId);

		void SendPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId inviteeId, bool isReservation);

		void AcceptPartyInvite(ulong invitationId);

		void DeclinePartyInvite(ulong invitationId);

		void RevokePartyInvite(bgs.types.EntityId partyId, ulong invitationId);

		void RequestPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId whomToAskForApproval, bgs.types.EntityId whomToInvite, string szPartyType);

		void IgnoreInviteRequest(bgs.types.EntityId partyId, bgs.types.EntityId requestedTargetId);

		void KickPartyMember(bgs.types.EntityId partyId, bgs.types.EntityId memberId);

		void SendPartyChatMessage(bgs.types.EntityId partyId, string message);

		void ClearPartyAttribute(bgs.types.EntityId partyId, string attributeKey);

		void SetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, [In] long value);

		void SetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, [In] string value);

		void SetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, [In] byte[] value);

		int GetPartyPrivacy(bgs.types.EntityId partyId);

		int GetCountPartyMembers(bgs.types.EntityId partyId);

		int GetMaxPartyMembers(bgs.types.EntityId partyId);

		void GetPartyMembers(bgs.types.EntityId partyId, out PartyMember[] members);

		void GetReceivedPartyInvites(out PartyInvite[] invites);

		void GetPartySentInvites(bgs.types.EntityId partyId, out PartyInvite[] invites);

		void GetPartyInviteRequests(bgs.types.EntityId partyId, out InviteRequest[] requests);

		void GetAllPartyAttributes(bgs.types.EntityId partyId, out string[] allKeys);

		bool GetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, out long value);

		void GetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, out string value);

		void GetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, out byte[] value);

		void GetPartyListenerEvents(out PartyListenerEvent[] events);

		void ClearPartyListenerEvents();

		void GetFriendsInfo(ref FriendsInfo info);

		void ClearFriendsUpdates();

		void GetFriendsUpdates([Out] FriendsUpdate[] updates);

		void SendFriendInvite(string inviter, string invitee, bool byEmail);

		void ManageFriendInvite(int action, ulong inviteId);

		void RemoveFriend(BnetAccountId account);

		void SendWhisper(BnetGameAccountId gameAccount, string message);

		void GetWhisperInfo(ref WhisperInfo info);

		void GetWhispers([Out] BnetWhisper[] whispers);

		void ClearWhispers();

		int GetNotificationCount();

		void GetNotifications([Out] BnetNotification[] notifications);

		void ClearNotifications();

		int NumChallenges();

		void ClearChallenges();

		void GetChallenges([Out] ChallengeInfo[] challenges);

		void AnswerChallenge(ulong challengeID, string answer);

		void CancelChallenge(ulong challengeID);

		void ApplicationWasPaused();

		void ApplicationWasUnpaused();

		bool CheckWebAuth(out string url);

		void ProvideWebAuthToken(string token);

		string FilterProfanity(string unfiltered);
	}
}
