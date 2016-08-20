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

		public static void FindGame(int gameType, int missionId, long deckId, long aiDeckId, bool setScenarioIdAttr)
		{
			Guid guid = Guid.NewGuid();
			BattleNet.s_impl.FindGame(guid.ToByteArray(), gameType, missionId, deckId, aiDeckId, setScenarioIdAttr);
		}

		public static void CancelFindGame()
		{
			BattleNet.s_impl.CancelFindGame();
		}

		public static QueueEvent GetQueueEvent()
		{
			return BattleNet.s_impl.GetQueueEvent();
		}

		public static int PresenceSize()
		{
			return BattleNet.s_impl.PresenceSize();
		}

		public static void ClearPresence()
		{
			BattleNet.s_impl.ClearPresence();
		}

		public static void GetPresence([Out] PresenceUpdate[] updates)
		{
			BattleNet.s_impl.GetPresence(updates);
		}

		public static void SetPresenceBool(uint field, bool val)
		{
			BattleNet.s_impl.SetPresenceBool(field, val);
		}

		public static void SetPresenceInt(uint field, long val)
		{
			BattleNet.s_impl.SetPresenceInt(field, val);
		}

		public static void SetPresenceString(uint field, string val)
		{
			BattleNet.s_impl.SetPresenceString(field, val);
		}

		public static void SetPresenceBlob(uint field, byte[] val)
		{
			BattleNet.s_impl.SetPresenceBlob(field, val);
		}

		public static void SetRichPresence([In] RichPresenceUpdate[] updates)
		{
			BattleNet.s_impl.SetRichPresence(updates);
		}

		public static void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			BattleNet.s_impl.RequestPresenceFields(isGameAccountEntityId, entityId, fieldList);
		}

		public static void SendFriendlyChallengeInvite(ref bgs.types.EntityId gameAccount, int scenarioId)
		{
			BattleNet.s_impl.SendFriendlyChallengeInvite(ref gameAccount, scenarioId);
		}

		public static void SetPartyDeck(ref bgs.types.EntityId partyId, long deckID)
		{
			BattleNet.s_impl.SetMyFriendlyChallengeDeck(ref partyId, deckID);
		}

		public static void GetPartyUpdatesInfo(ref PartyInfo info)
		{
			BattleNet.s_impl.GetPartyUpdatesInfo(ref info);
		}

		public static void GetPartyUpdates([Out] PartyEvent[] updates)
		{
			BattleNet.s_impl.GetPartyUpdates(updates);
		}

		public static void ClearPartyUpdates()
		{
			BattleNet.s_impl.ClearPartyUpdates();
		}

		public static void DeclineFriendlyChallenge(ref bgs.types.EntityId partyId)
		{
			BattleNet.s_impl.DeclineFriendlyChallenge(ref partyId);
		}

		public static void AcceptFriendlyChallenge(ref bgs.types.EntityId partyId)
		{
			BattleNet.s_impl.AcceptFriendlyChallenge(ref partyId);
		}

		public static void RescindFriendlyChallenge(ref bgs.types.EntityId partyId)
		{
			BattleNet.s_impl.RescindFriendlyChallenge(ref partyId);
		}

		public static void CreateParty(string partyType, int privacyLevel, byte[] creatorBlob)
		{
			BattleNet.s_impl.CreateParty(partyType, privacyLevel, creatorBlob);
		}

		public static void JoinParty(bgs.types.EntityId partyId, string partyType)
		{
			BattleNet.s_impl.JoinParty(partyId, partyType);
		}

		public static void LeaveParty(bgs.types.EntityId partyId)
		{
			BattleNet.s_impl.LeaveParty(partyId);
		}

		public static void DissolveParty(bgs.types.EntityId partyId)
		{
			BattleNet.s_impl.DissolveParty(partyId);
		}

		public static void SetPartyPrivacy(bgs.types.EntityId partyId, int privacyLevel)
		{
			BattleNet.s_impl.SetPartyPrivacy(partyId, privacyLevel);
		}

		public static void AssignPartyRole(bgs.types.EntityId partyId, bgs.types.EntityId memberId, uint roleId)
		{
			BattleNet.s_impl.AssignPartyRole(partyId, memberId, roleId);
		}

		public static void SendPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId inviteeId, bool isReservation)
		{
			BattleNet.s_impl.SendPartyInvite(partyId, inviteeId, isReservation);
		}

		public static void AcceptPartyInvite(ulong invitationId)
		{
			BattleNet.s_impl.AcceptPartyInvite(invitationId);
		}

		public static void DeclinePartyInvite(ulong invitationId)
		{
			BattleNet.s_impl.DeclinePartyInvite(invitationId);
		}

		public static void RevokePartyInvite(bgs.types.EntityId partyId, ulong invitationId)
		{
			BattleNet.s_impl.RevokePartyInvite(partyId, invitationId);
		}

		public static void RequestPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId whomToAskForApproval, bgs.types.EntityId whomToInvite, string szPartyType)
		{
			BattleNet.s_impl.RequestPartyInvite(partyId, whomToAskForApproval, whomToInvite, szPartyType);
		}

		public static void IgnoreInviteRequest(bgs.types.EntityId partyId, bgs.types.EntityId requestedTargetId)
		{
			BattleNet.s_impl.IgnoreInviteRequest(partyId, requestedTargetId);
		}

		public static void KickPartyMember(bgs.types.EntityId partyId, bgs.types.EntityId memberId)
		{
			BattleNet.s_impl.KickPartyMember(partyId, memberId);
		}

		public static void SendPartyChatMessage(bgs.types.EntityId partyId, string message)
		{
			BattleNet.s_impl.SendPartyChatMessage(partyId, message);
		}

		public static void ClearPartyAttribute(bgs.types.EntityId partyId, string attributeKey)
		{
			BattleNet.s_impl.ClearPartyAttribute(partyId, attributeKey);
		}

		public static void SetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, [In] long value)
		{
			BattleNet.s_impl.SetPartyAttributeLong(partyId, attributeKey, value);
		}

		public static void SetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, [In] string value)
		{
			BattleNet.s_impl.SetPartyAttributeString(partyId, attributeKey, value);
		}

		public static void SetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, [In] byte[] value)
		{
			BattleNet.s_impl.SetPartyAttributeBlob(partyId, attributeKey, value);
		}

		public static int GetPartyPrivacy(bgs.types.EntityId partyId)
		{
			return BattleNet.s_impl.GetPartyPrivacy(partyId);
		}

		public static int GetCountPartyMembers(bgs.types.EntityId partyId)
		{
			return BattleNet.s_impl.GetCountPartyMembers(partyId);
		}

		public static int GetMaxPartyMembers(bgs.types.EntityId partyId)
		{
			return BattleNet.s_impl.GetMaxPartyMembers(partyId);
		}

		public static void GetPartyMembers(bgs.types.EntityId partyId, out PartyMember[] members)
		{
			BattleNet.s_impl.GetPartyMembers(partyId, out members);
		}

		public static void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			BattleNet.s_impl.GetReceivedPartyInvites(out invites);
		}

		public static void GetPartySentInvites(bgs.types.EntityId partyId, out PartyInvite[] invites)
		{
			BattleNet.s_impl.GetPartySentInvites(partyId, out invites);
		}

		public static void GetPartyInviteRequests(bgs.types.EntityId partyId, out InviteRequest[] requests)
		{
			BattleNet.s_impl.GetPartyInviteRequests(partyId, out requests);
		}

		public static void GetAllPartyAttributes(bgs.types.EntityId partyId, out string[] allKeys)
		{
			BattleNet.s_impl.GetAllPartyAttributes(partyId, out allKeys);
		}

		public static bool GetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, out long value)
		{
			return BattleNet.s_impl.GetPartyAttributeLong(partyId, attributeKey, out value);
		}

		public static void GetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, out string value)
		{
			BattleNet.s_impl.GetPartyAttributeString(partyId, attributeKey, out value);
		}

		public static void GetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, out byte[] value)
		{
			BattleNet.s_impl.GetPartyAttributeBlob(partyId, attributeKey, out value);
		}

		public static void GetPartyListenerEvents(out PartyListenerEvent[] events)
		{
			BattleNet.s_impl.GetPartyListenerEvents(out events);
		}

		public static void ClearPartyListenerEvents()
		{
			BattleNet.s_impl.ClearPartyListenerEvents();
		}

		public static void GetFriendsInfo(ref FriendsInfo info)
		{
			BattleNet.s_impl.GetFriendsInfo(ref info);
		}

		public static void ClearFriendsUpdates()
		{
			BattleNet.s_impl.ClearFriendsUpdates();
		}

		public static void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			BattleNet.s_impl.GetFriendsUpdates(updates);
		}

		public static void SendFriendInvite(string inviter, string invitee, bool byEmail)
		{
			BattleNet.s_impl.SendFriendInvite(inviter, invitee, byEmail);
		}

		public static void ManageFriendInvite(int action, ulong inviteId)
		{
			BattleNet.s_impl.ManageFriendInvite(action, inviteId);
		}

		public static void RemoveFriend(BnetAccountId account)
		{
			BattleNet.s_impl.RemoveFriend(account);
		}

		public static void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			BattleNet.s_impl.SendWhisper(gameAccount, message);
		}

		public static void GetWhisperInfo(ref WhisperInfo info)
		{
			BattleNet.s_impl.GetWhisperInfo(ref info);
		}

		public static void GetWhispers([Out] BnetWhisper[] whispers)
		{
			BattleNet.s_impl.GetWhispers(whispers);
		}

		public static void ClearWhispers()
		{
			BattleNet.s_impl.ClearWhispers();
		}

		public static int GetNotificationCount()
		{
			return BattleNet.s_impl.GetNotificationCount();
		}

		public static void GetNotifications([Out] BnetNotification[] notifications)
		{
			BattleNet.s_impl.GetNotifications(notifications);
		}

		public static void ClearNotifications()
		{
			BattleNet.s_impl.ClearNotifications();
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

		public static string FilterProfanity(string unfiltered)
		{
			return BattleNet.s_impl.FilterProfanity(unfiltered);
		}

		public static void ApplicationWasPaused()
		{
			BattleNet.s_impl.ApplicationWasPaused();
		}

		public static void ApplicationWasUnpaused()
		{
			BattleNet.s_impl.ApplicationWasUnpaused();
		}

		public const string COUNTRY_KOREA = "KOR";

		public const string COUNTRY_CHINA = "CHN";

		private static IBattleNet s_impl = new BattleNetCSharp();

		public enum BnetEvent
		{
			Disconnected
		}

		public class GameServerInfo
		{
			public string Address { get; set; }

			public int Port { get; set; }

			public int GameHandle { get; set; }

			public long ClientHandle { get; set; }

			public string AuroraPassword { get; set; }

			public string Version { get; set; }

			public int Mission { get; set; }

			public bool Resumable { get; set; }

			public string SpectatorPassword { get; set; }

			public bool SpectatorMode { get; set; }
		}
	}
}
