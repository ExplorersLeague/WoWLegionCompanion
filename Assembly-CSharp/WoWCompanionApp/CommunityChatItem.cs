using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChatItem : MonoBehaviour
	{
		public void SetChatInfo(CommunityChatMessage message)
		{
			this.m_characterName.text = message.Author;
			this.m_bodyText.text = message.Message;
			this.m_timeStamp = message.TimeStamp;
			this.m_postTime.text = this.m_timeStamp.ToShortTimeString();
			this.m_moderatorImage.SetActive(message.PostedByModerator());
		}

		public void MinimizeChatItem()
		{
			this.m_headerObject.SetActive(false);
			this.m_characterImage.GetComponent<LayoutElement>().minHeight = 0f;
			this.m_characterImage.GetComponent<Image>().enabled = false;
		}

		public string GetAuthor()
		{
			return this.m_characterName.text;
		}

		public DateTime TimeStamp
		{
			get
			{
				return this.m_timeStamp;
			}
		}

		public Text m_characterName;

		public Text m_postTime;

		public Text m_bodyText;

		public GameObject m_headerObject;

		public GameObject m_characterImage;

		public GameObject m_moderatorImage;

		private DateTime m_timeStamp;
	}
}
