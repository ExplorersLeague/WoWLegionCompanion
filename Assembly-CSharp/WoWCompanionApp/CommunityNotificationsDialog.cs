using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityNotificationsDialog : MonoBehaviour
	{
		public void SetCommunity(Community community)
		{
			this.m_community = community;
			this.m_headerCommunityText.text = community.Name.ToUpper();
			this.BuildContentPanel();
		}

		private void BuildContentPanel()
		{
			this.m_contentPane.DetachAllChildren();
			this.AddChannelSettingsToContent();
		}

		private void AddPushSettingToContent()
		{
			GameObject gameObject = this.m_contentPane.AddAsChildObject(this.m_pushSettingPrefab);
		}

		private void AddChannelSettingsToContent()
		{
			this.m_contentPane.AddAsChildObject(this.m_sectionDividerPrefab);
			ReadOnlyCollection<CommunityStream> allStreams = this.m_community.GetAllStreams();
			foreach (CommunityStream channel in allStreams)
			{
				GameObject gameObject = this.m_contentPane.AddAsChildObject(this.m_channelNotificationSettingPrefab);
				gameObject.GetComponentInChildren<CommunityChannelNotificationButton>().SetChannel(channel);
			}
		}

		public Text m_headerCommunityText;

		public GameObject m_contentPane;

		public GameObject m_pushSettingPrefab;

		public GameObject m_sectionDividerPrefab;

		public GameObject m_channelNotificationSettingPrefab;

		private Community m_community;
	}
}
