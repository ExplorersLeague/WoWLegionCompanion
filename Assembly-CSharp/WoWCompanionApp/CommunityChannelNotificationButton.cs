using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChannelNotificationButton : MonoBehaviour
	{
		public void SetChannel(CommunityStream stream)
		{
			this.m_stream = stream;
			this.m_channelName.text = this.m_stream.Name;
			this.m_leaderModeratorImage.SetActive(this.m_stream.ForLeadersAndModerators);
			this.UpdateToggleState();
			this.m_allButton.onValueChanged.AddListener(delegate
			{
				this.TurnOnNotifications();
			});
			this.m_nothingButton.onValueChanged.AddListener(delegate
			{
				this.TurnOffNotifications();
			});
		}

		private void UpdateToggleState()
		{
			if (this.m_stream.ShouldReceiveNotifications())
			{
				this.m_allButton.isOn = true;
			}
			else
			{
				this.m_nothingButton.isOn = true;
			}
		}

		private void TurnOnNotifications()
		{
			if (this.m_allButton.isOn)
			{
				this.m_stream.SetNotificationFilter(2);
			}
		}

		private void TurnOffNotifications()
		{
			if (this.m_nothingButton.isOn)
			{
				this.m_stream.SetNotificationFilter(0);
			}
		}

		public Text m_channelName;

		public GameObject m_leaderModeratorImage;

		public Toggle m_allButton;

		public Toggle m_nothingButton;

		private CommunityStream m_stream;
	}
}
