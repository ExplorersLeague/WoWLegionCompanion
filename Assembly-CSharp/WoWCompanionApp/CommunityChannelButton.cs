using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChannelButton : MonoBehaviour
	{
		private void Awake()
		{
			Club.OnStreamViewMarkerUpdated += new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
		}

		private void OnDestroy()
		{
			Club.OnStreamViewMarkerUpdated -= new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
		}

		public void SetCommunityInfo(Community community, CommunityStream stream)
		{
			this.m_channelName.text = stream.Name.ToUpper();
			this.m_communityStream = stream;
			this.m_community = community;
			this.UpdateNotificationVisibility();
		}

		public void SetSelectAction(Action<CommunityChannelButton> action)
		{
			this.m_selectAction = action;
		}

		public ulong StreamId
		{
			get
			{
				return this.m_communityStream.StreamId;
			}
		}

		public CommunityStream Stream
		{
			get
			{
				return this.m_communityStream;
			}
		}

		public Community Community
		{
			get
			{
				return this.m_community;
			}
		}

		public void SelectChannel()
		{
			if (this.m_selectAction != null)
			{
				this.m_selectAction(this);
			}
		}

		private void UpdateNotificationVisibility()
		{
			if (this.m_notification != null)
			{
				bool flag = this.m_communityStream.HasUnreadMessages();
				this.m_notification.SetActive(flag);
				this.m_textGradient.Gradient = ((!flag) ? this.m_gradientPlain : this.m_gradientNotification);
			}
		}

		private void OnViewMarkerUpdated(Club.StreamViewMarkerUpdatedEvent markerEvent)
		{
			if (markerEvent.ClubID == this.m_community.ClubId && markerEvent.StreamID == this.m_communityStream.StreamId)
			{
				this.UpdateNotificationVisibility();
			}
		}

		public Text m_channelName;

		public MeshGradient m_textGradient;

		public GameObject m_notification;

		public Gradient m_gradientNotification;

		public Gradient m_gradientPlain;

		private CommunityStream m_communityStream;

		private Community m_community;

		private Action<CommunityChannelButton> m_selectAction;
	}
}
