using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChannelDialog : MonoBehaviour
	{
		private void Awake()
		{
			CommunityData.OnChannelRefresh += this.OnChannelRefresh;
		}

		private void OnDestroy()
		{
			CommunityData.OnChannelRefresh -= this.OnChannelRefresh;
		}

		private void OnChannelRefresh(ulong communityID)
		{
			if (this.m_community.ClubId == communityID)
			{
				this.BuildContentPane();
			}
		}

		private void BuildContentPane()
		{
			this.m_content.DetachAllChildren();
			ReadOnlyCollection<CommunityStream> allStreams = this.m_community.GetAllStreams();
			foreach (CommunityStream stream in allStreams)
			{
				GameObject gameObject = this.m_content.AddAsChildObject(this.m_channelSelectPrefab);
				CommunityChannelButton channelButton = gameObject.GetComponent<CommunityChannelButton>();
				channelButton.SetCommunityInfo(this.m_community, stream);
				channelButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
				{
					this.m_selectCallback.Invoke(channelButton);
				});
				channelButton.GetComponentInChildren<Button>().onClick.AddListener(this.m_cleanupCallback);
			}
		}

		public void InitializeContentPane(Community community, UnityAction<CommunityChannelButton> selectCallback, UnityAction cleanupCallback)
		{
			this.m_community = community;
			this.m_headerText.text = this.m_community.Name.ToUpper();
			this.m_selectCallback = selectCallback;
			this.m_cleanupCallback = cleanupCallback;
			this.BuildContentPane();
		}

		public Text m_headerText;

		public GameObject m_channelSelectPrefab;

		public GameObject m_content;

		private Community m_community;

		private UnityAction<CommunityChannelButton> m_selectCallback;

		private UnityAction m_cleanupCallback;
	}
}
