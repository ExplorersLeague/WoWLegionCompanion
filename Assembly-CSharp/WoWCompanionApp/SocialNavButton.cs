using System;

namespace WoWCompanionApp
{
	public class SocialNavButton : CompanionNavButton
	{
		private void Start()
		{
			base.InitializeButtonState(true);
		}

		private void Awake()
		{
			Club.OnStreamViewMarkerUpdated += new Club.StreamViewMarkerUpdatedHandler(this.OnStreamViewMarkerUpdate);
		}

		private void OnDestroy()
		{
			Club.OnStreamViewMarkerUpdated -= new Club.StreamViewMarkerUpdatedHandler(this.OnStreamViewMarkerUpdate);
		}

		protected override void UpdateNotificationState()
		{
			this.m_notificationImage.SetActive(CommunityData.Instance.HasUnreadCommunityMessages(null));
		}

		private void OnStreamViewMarkerUpdate(Club.StreamViewMarkerUpdatedEvent markerEvent)
		{
			this.UpdateNotificationState();
		}
	}
}
