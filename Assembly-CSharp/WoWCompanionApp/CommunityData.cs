using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WoWCompanionApp
{
	public class CommunityData
	{
		private CommunityData()
		{
			Club.OnClubAdded += new Club.ClubAddedHandler(this.OnClubAdded);
			Club.OnClubRemoved += new Club.ClubRemovedHandler(this.OnClubRemoved);
			Club.OnClubUpdated += new Club.ClubUpdatedHandler(this.OnClubUpdated);
			Club.OnClubInvitationAddedForSelf += new Club.ClubInvitationAddedForSelfHandler(this.OnInviteAdded);
			Club.OnClubInvitationRemovedForSelf += new Club.ClubInvitationRemovedForSelfHandler(this.OnInviteRemoved);
			Club.OnClubMemberAdded += new Club.ClubMemberAddedHandler(this.OnMemberAdded);
			Club.OnClubMemberRemoved += new Club.ClubMemberRemovedHandler(this.OnMemberRemoved);
			Club.OnClubMemberUpdated += new Club.ClubMemberUpdatedHandler(this.OnMemberUpdated);
			Club.OnClubMemberRoleUpdated += new Club.ClubMemberRoleUpdatedHandler(this.OnMemberRoleUpdated);
			Club.OnClubMemberPresenceUpdated += new Club.ClubMemberPresenceUpdatedHandler(this.OnMemberPresenceUpdated);
			Club.OnClubStreamAdded += new Club.ClubStreamAddedHandler(this.OnStreamAdded);
			Club.OnClubStreamRemoved += new Club.ClubStreamRemovedHandler(this.OnStreamRemoved);
		}

		public static event CommunityData.RefreshHandler OnCommunityRefresh;

		public static event CommunityData.RefreshHandler OnInviteRefresh;

		public static event CommunityData.CommunityRefreshHandler OnChannelRefresh;

		public static event CommunityData.CommunityRefreshHandler OnRosterRefresh;

		public static CommunityData Instance
		{
			get
			{
				if (CommunityData.m_instance == null)
				{
					CommunityData.m_instance = new CommunityData();
				}
				return CommunityData.m_instance;
			}
		}

		public void Shutdown()
		{
			if (CommunityData.m_instance != null)
			{
				Club.OnClubAdded -= new Club.ClubAddedHandler(this.OnClubAdded);
				Club.OnClubRemoved -= new Club.ClubRemovedHandler(this.OnClubRemoved);
				Club.OnClubUpdated -= new Club.ClubUpdatedHandler(this.OnClubUpdated);
				Club.OnClubInvitationAddedForSelf -= new Club.ClubInvitationAddedForSelfHandler(this.OnInviteAdded);
				Club.OnClubInvitationRemovedForSelf -= new Club.ClubInvitationRemovedForSelfHandler(this.OnInviteRemoved);
				Club.OnClubMemberAdded -= new Club.ClubMemberAddedHandler(this.OnMemberAdded);
				Club.OnClubMemberRemoved -= new Club.ClubMemberRemovedHandler(this.OnMemberRemoved);
				Club.OnClubMemberUpdated -= new Club.ClubMemberUpdatedHandler(this.OnMemberUpdated);
				Club.OnClubMemberRoleUpdated -= new Club.ClubMemberRoleUpdatedHandler(this.OnMemberRoleUpdated);
				Club.OnClubMemberPresenceUpdated -= new Club.ClubMemberPresenceUpdatedHandler(this.OnMemberPresenceUpdated);
				Club.OnClubStreamAdded -= new Club.ClubStreamAddedHandler(this.OnStreamAdded);
				Club.OnClubStreamRemoved -= new Club.ClubStreamRemovedHandler(this.OnStreamRemoved);
				CommunityData.m_communityDictionary.Clear();
				CommunityData.m_pendingInvites.Clear();
				CommunityData.m_instance = null;
			}
		}

		private void OnClubAdded(Club.ClubAddedEvent newClubEvent)
		{
			ClubInfo? clubInfo = Club.GetClubInfo(newClubEvent.ClubID);
			if (clubInfo != null)
			{
				this.AddCommunity(clubInfo.Value);
				this.FireCommunityRefreshCallback();
			}
		}

		private void OnClubUpdated(Club.ClubUpdatedEvent updateClubEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(updateClubEvent.ClubID))
			{
				CommunityData.m_communityDictionary[updateClubEvent.ClubID].HandleClubUpdatedEvent(updateClubEvent);
			}
		}

		private void OnClubRemoved(Club.ClubRemovedEvent removeClubEvent)
		{
			CommunityData.m_communityDictionary.Remove(removeClubEvent.ClubID);
			this.FireCommunityRefreshCallback();
		}

		private void OnStreamAdded(Club.ClubStreamAddedEvent newStreamEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(newStreamEvent.ClubID))
			{
				CommunityData.m_communityDictionary[newStreamEvent.ClubID].HandleStreamAddedEvent(newStreamEvent);
				this.FireChannelRefreshCallback(newStreamEvent.ClubID);
			}
		}

		private void OnStreamRemoved(Club.ClubStreamRemovedEvent removeStreamEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(removeStreamEvent.ClubID))
			{
				CommunityData.m_communityDictionary[removeStreamEvent.ClubID].HandleStreamRemovedEvent(removeStreamEvent);
				this.FireChannelRefreshCallback(removeStreamEvent.ClubID);
			}
		}

		private void OnInviteAdded(Club.ClubInvitationAddedForSelfEvent newInviteEvent)
		{
			CommunityData.m_pendingInvites.Add(new CommunityPendingInvite(newInviteEvent.Invitation));
			this.FireInviteRefreshCallback();
		}

		private void OnInviteRemoved(Club.ClubInvitationRemovedForSelfEvent inviteRemovedEvent)
		{
			this.RefreshInvitations();
			this.FireInviteRefreshCallback();
		}

		private void OnMemberAdded(Club.ClubMemberAddedEvent newMemberEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(newMemberEvent.ClubID))
			{
				CommunityData.m_communityDictionary[newMemberEvent.ClubID].HandleMemberAddedEvent(newMemberEvent);
				this.FireRosterRefreshCallback(newMemberEvent.ClubID);
			}
		}

		private void OnMemberRemoved(Club.ClubMemberRemovedEvent removeMemberEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(removeMemberEvent.ClubID))
			{
				CommunityData.m_communityDictionary[removeMemberEvent.ClubID].HandleMemberRemovedEvent(removeMemberEvent);
				this.FireRosterRefreshCallback(removeMemberEvent.ClubID);
			}
		}

		private void OnMemberUpdated(Club.ClubMemberUpdatedEvent updateMemberEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(updateMemberEvent.ClubID))
			{
				CommunityData.m_communityDictionary[updateMemberEvent.ClubID].HandleMemberUpdatedEvent(updateMemberEvent);
				this.FireRosterRefreshCallback(updateMemberEvent.ClubID);
			}
		}

		private void OnMemberRoleUpdated(Club.ClubMemberRoleUpdatedEvent updateRoleEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(updateRoleEvent.ClubID))
			{
				CommunityData.m_communityDictionary[updateRoleEvent.ClubID].HandleMemberRoleUpdatedEvent(updateRoleEvent);
				this.FireRosterRefreshCallback(updateRoleEvent.ClubID);
			}
		}

		private void OnMemberPresenceUpdated(Club.ClubMemberPresenceUpdatedEvent updatePresenceEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(updatePresenceEvent.ClubID))
			{
				CommunityData.m_communityDictionary[updatePresenceEvent.ClubID].HandleMemberPresenceUpdatedEvent(updatePresenceEvent);
				this.FireRosterRefreshCallback(updatePresenceEvent.ClubID);
			}
		}

		private void AddCommunity(ClubInfo community)
		{
			if (!CommunityData.m_communityDictionary.ContainsKey(community.clubId))
			{
				Community community2 = new Community(community);
				CommunityData.m_communityDictionary.Add(community2.ClubId, community2);
				community2.RefreshStreams();
			}
		}

		public void ClearData()
		{
			CommunityData.m_communityDictionary.Clear();
			CommunityData.m_pendingInvites.Clear();
		}

		public void ForEachCommunity(Action<Community> action)
		{
			foreach (Community community in CommunityData.m_communityDictionary.Values)
			{
				if (!community.IsGuild())
				{
					action(community);
				}
			}
		}

		public void ForGuild(Action<Community> action)
		{
			if (CommunityData.m_guildCommunity != null)
			{
				action(CommunityData.m_guildCommunity);
			}
		}

		public void RefreshCommunities()
		{
			List<ClubInfo> subscribedClubs = Club.GetSubscribedClubs();
			foreach (ClubInfo community in subscribedClubs)
			{
				CommunityData.Instance.AddCommunity(community);
				if (community.clubType == 2)
				{
					CommunityData.m_guildCommunity = CommunityData.m_communityDictionary[community.clubId];
				}
			}
		}

		public bool HasUnreadCommunityMessages(Community ignoreCommunity = null)
		{
			foreach (Community community in CommunityData.m_communityDictionary.Values)
			{
				if (community != ignoreCommunity && community.HasUnreadMessages(null))
				{
					return true;
				}
			}
			return false;
		}

		public void RefreshInvitations()
		{
			CommunityData.m_pendingInvites.Clear();
			List<ClubSelfInvitationInfo> invitationsForSelf = Club.GetInvitationsForSelf();
			foreach (ClubSelfInvitationInfo inviteInfo in invitationsForSelf)
			{
				CommunityData.m_pendingInvites.Add(new CommunityPendingInvite(inviteInfo));
			}
		}

		public ReadOnlyCollection<CommunityPendingInvite> GetPendingInvites()
		{
			return CommunityData.m_pendingInvites.AsReadOnly();
		}

		public void HandleMessageAddedEvent(Club.ClubMessageAddedEvent messageEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(messageEvent.ClubID))
			{
				CommunityData.m_communityDictionary[messageEvent.ClubID].HandleMessageAddedEvent(messageEvent);
			}
		}

		public void HandleMessageUpdatedEvent(Club.ClubMessageUpdatedEvent messageEvent)
		{
			if (CommunityData.m_communityDictionary.ContainsKey(messageEvent.ClubID))
			{
				CommunityData.m_communityDictionary[messageEvent.ClubID].HandleMessageUpdatedEvent(messageEvent);
			}
		}

		private void FireCommunityRefreshCallback()
		{
			if (CommunityData.OnCommunityRefresh != null)
			{
				CommunityData.OnCommunityRefresh();
			}
		}

		private void FireRosterRefreshCallback(ulong clubID)
		{
			if (CommunityData.OnRosterRefresh != null)
			{
				CommunityData.OnRosterRefresh(clubID);
			}
		}

		private void FireChannelRefreshCallback(ulong clubID)
		{
			if (CommunityData.OnChannelRefresh != null)
			{
				CommunityData.OnChannelRefresh(clubID);
			}
		}

		private void FireInviteRefreshCallback()
		{
			if (CommunityData.OnInviteRefresh != null)
			{
				CommunityData.OnInviteRefresh();
			}
		}

		public bool HasCommunities()
		{
			int num = (CommunityData.m_guildCommunity != null) ? 1 : 0;
			return CommunityData.m_communityDictionary.Count > num;
		}

		public bool HasGuild()
		{
			return CommunityData.m_guildCommunity != null;
		}

		private static CommunityData m_instance = null;

		private static Dictionary<ulong, Community> m_communityDictionary = new Dictionary<ulong, Community>();

		private static List<CommunityPendingInvite> m_pendingInvites = new List<CommunityPendingInvite>();

		private static Community m_guildCommunity = null;

		public delegate void RefreshHandler();

		public delegate void CommunityRefreshHandler(ulong communityId);
	}
}
