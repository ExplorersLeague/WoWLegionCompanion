using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WoWCompanionApp
{
	public class Community
	{
		public Community(ClubInfo clubInfo)
		{
			this.m_clubInfo = clubInfo;
		}

		public void PopulateCommunityInfo()
		{
			this.RefreshStreams();
			this.RefreshMemberList();
		}

		public ulong ClubId
		{
			get
			{
				return this.m_clubInfo.clubId;
			}
		}

		public string Name
		{
			get
			{
				return this.m_clubInfo.name;
			}
		}

		public string Description
		{
			get
			{
				return this.m_clubInfo.description;
			}
		}

		public uint AvatarId
		{
			get
			{
				return this.m_clubInfo.avatarId;
			}
		}

		public bool IsGuild()
		{
			return this.m_clubInfo.clubType == 2;
		}

		public void EditCommunity(string name, string description, uint? avatarId)
		{
			Club.EditClub(this.ClubId, name, this.m_clubInfo.shortName, description, avatarId, string.Empty);
		}

		public void CreateStream(string name, string subject, bool modsOnly)
		{
			Club.CreateStream(this.ClubId, name, subject, modsOnly);
		}

		public CommunityStream GetDefaultStream(ulong streamId)
		{
			if (this.m_streamList.ContainsKey(streamId))
			{
				return this.m_streamList[streamId];
			}
			if (this.m_streamList.Count > 0)
			{
				return this.m_streamList.First<KeyValuePair<ulong, CommunityStream>>().Value;
			}
			return null;
		}

		public void RefreshStreams()
		{
			List<ClubStreamInfo> streams = Club.GetStreams(this.ClubId);
			List<ulong> list = new List<ulong>();
			using (Dictionary<ulong, CommunityStream>.Enumerator enumerator = this.m_streamList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<ulong, CommunityStream> stream = enumerator.Current;
					if (!streams.Any((ClubStreamInfo info) => info.streamId == stream.Value.StreamId))
					{
						list.Add(stream.Key);
					}
				}
			}
			foreach (ulong key in list)
			{
				this.m_streamList.Remove(key);
			}
			foreach (ClubStreamInfo info2 in streams)
			{
				this.AddStream(info2);
			}
			List<ClubStreamNotificationSetting> clubStreamNotificationSettings = Club.GetClubStreamNotificationSettings(this.ClubId);
			foreach (ClubStreamNotificationSetting filter in clubStreamNotificationSettings)
			{
				this.m_streamList[filter.streamId].Filter = filter;
			}
		}

		private void AddStream(ClubStreamInfo info)
		{
			if (!this.m_streamList.ContainsKey(info.streamId))
			{
				CommunityStream communityStream = new CommunityStream(this.ClubId, info);
				this.m_streamList.Add(communityStream.StreamId, communityStream);
			}
		}

		public ReadOnlyCollection<CommunityStream> GetAllStreams()
		{
			List<CommunityStream> list = new List<CommunityStream>();
			foreach (CommunityStream item in this.m_streamList.Values)
			{
				list.Add(item);
			}
			return list.AsReadOnly();
		}

		public void RefreshMemberList()
		{
			this.m_memberList.Clear();
			List<uint> clubMembers = Club.GetClubMembers(this.ClubId, null);
			foreach (uint num in clubMembers)
			{
				ClubMemberInfo? memberInfo = Club.GetMemberInfo(this.ClubId, num);
				if (memberInfo != null)
				{
					this.m_memberList.Add(new CommunityMember(this.ClubId, memberInfo.Value));
				}
			}
		}

		public ReadOnlyCollection<CommunityMember> GetMemberList()
		{
			return this.m_memberList.AsReadOnly();
		}

		public void LeaveOrDestroyClub()
		{
			if (this.CanDeleteClub())
			{
				Club.DestroyClub(this.ClubId);
			}
			else
			{
				Club.LeaveClub(this.ClubId);
			}
		}

		public bool CanLeaveClub()
		{
			return Club.GetMemberInfoForSelf(this.ClubId).Value.role != 1;
		}

		public bool CanDeleteClub()
		{
			return Club.GetMemberInfoForSelf(this.ClubId).Value.role == 1 && this.m_memberList.Count < 2;
		}

		public bool HasUnreadMessages(CommunityStream ignoreStream = null)
		{
			foreach (CommunityStream communityStream in this.m_streamList.Values)
			{
				if (communityStream != ignoreStream && communityStream.HasUnreadMessages())
				{
					return true;
				}
			}
			return false;
		}

		public void MarkAllAsRead()
		{
			foreach (CommunityStream communityStream in this.m_streamList.Values)
			{
				communityStream.ClearNotifications();
			}
		}

		public void HandleClubUpdatedEvent(Club.ClubUpdatedEvent updateEvent)
		{
			ClubInfo? clubInfo = Club.GetClubInfo(updateEvent.ClubID);
			if (clubInfo != null)
			{
				this.m_clubInfo = clubInfo.Value;
			}
		}

		public void HandleStreamAddedEvent(Club.ClubStreamAddedEvent streamAddedEvent)
		{
			ClubStreamInfo? streamInfo = Club.GetStreamInfo(streamAddedEvent.ClubID, streamAddedEvent.StreamID);
			if (streamInfo != null)
			{
				this.AddStream(streamInfo.Value);
			}
		}

		public void HandleStreamRemovedEvent(Club.ClubStreamRemovedEvent streamRemovedEvent)
		{
			if (this.m_streamList.ContainsKey(streamRemovedEvent.StreamID))
			{
				this.m_streamList.Remove(streamRemovedEvent.StreamID);
			}
		}

		public void HandleStreamUpdatedEvent(Club.ClubStreamUpdatedEvent streamUpdatedEvent)
		{
			if (this.m_streamList.ContainsKey(streamUpdatedEvent.StreamID))
			{
				this.m_streamList[streamUpdatedEvent.StreamID].HandleStreamUpdatedEvent(streamUpdatedEvent);
			}
		}

		public void HandleMemberAddedEvent(Club.ClubMemberAddedEvent addedEvent)
		{
			ClubMemberInfo? memberInfo = Club.GetMemberInfo(this.ClubId, addedEvent.MemberID);
			if (memberInfo != null)
			{
				this.m_memberList.Add(new CommunityMember(this.ClubId, memberInfo.Value));
			}
		}

		public void HandleMemberRemovedEvent(Club.ClubMemberRemovedEvent removeMemberEvent)
		{
			this.m_memberList.RemoveAll((CommunityMember member) => member.MemberID == removeMemberEvent.MemberID);
		}

		public void HandleMemberUpdatedEvent(Club.ClubMemberUpdatedEvent updateMemberEvent)
		{
			CommunityMember communityMember = this.m_memberList.Find((CommunityMember member) => member.MemberID == updateMemberEvent.MemberID);
			if (communityMember != null)
			{
				communityMember.HandleMemberUpdatedEvent(updateMemberEvent);
			}
		}

		public void HandleMemberRoleUpdatedEvent(Club.ClubMemberRoleUpdatedEvent updateRoleEvent)
		{
			CommunityMember communityMember = this.m_memberList.Find((CommunityMember member) => member.MemberID == updateRoleEvent.MemberID);
			if (communityMember != null)
			{
				communityMember.HandleRoleUpdateEvent(updateRoleEvent);
				if (communityMember.IsSelf)
				{
					this.RefreshStreams();
					CommunityData.Instance.FireChannelRefreshCallback(updateRoleEvent.ClubID);
				}
			}
		}

		public void HandleMemberPresenceUpdatedEvent(Club.ClubMemberPresenceUpdatedEvent updatePresenceEvent)
		{
			CommunityMember communityMember = this.m_memberList.Find((CommunityMember member) => member.MemberID == updatePresenceEvent.MemberID);
			if (communityMember != null)
			{
				communityMember.HandlePresenceUpdateEvent(updatePresenceEvent);
			}
		}

		public void HandleMessageAddedEvent(Club.ClubMessageAddedEvent messageEvent)
		{
			if (this.m_streamList.ContainsKey(messageEvent.StreamID))
			{
				this.m_streamList[messageEvent.StreamID].HandleMessageAddedEvent(messageEvent);
			}
		}

		public void HandleMessageUpdatedEvent(Club.ClubMessageUpdatedEvent messageEvent)
		{
			if (this.m_streamList.ContainsKey(messageEvent.StreamID))
			{
				this.m_streamList[messageEvent.StreamID].HandleMessageUpdatedEvent(messageEvent);
			}
		}

		public CommunityMember GetUpdatedMember(Club.ClubMemberRoleUpdatedEvent updateRoleEvent)
		{
			this.HandleMemberRoleUpdatedEvent(updateRoleEvent);
			return this.m_memberList.Find((CommunityMember member) => member.MemberID == updateRoleEvent.MemberID);
		}

		public bool CanAccessUpdatedChannel(Club.ClubStreamUpdatedEvent streamUpdatedEvent)
		{
			ClubStreamInfo? streamInfo = Club.GetStreamInfo(streamUpdatedEvent.ClubID, streamUpdatedEvent.StreamID);
			if (streamInfo != null)
			{
				if (!streamInfo.Value.leadersAndModeratorsOnly)
				{
					return true;
				}
				CommunityMember communityMember = this.m_memberList.Find((CommunityMember member) => member.IsSelf);
				if (communityMember != null && communityMember.IsModerator)
				{
					return true;
				}
			}
			return false;
		}

		private ClubInfo m_clubInfo;

		private Dictionary<ulong, CommunityStream> m_streamList = new Dictionary<ulong, CommunityStream>();

		private List<CommunityMember> m_memberList = new List<CommunityMember>();
	}
}
