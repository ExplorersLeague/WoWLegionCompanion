using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChatItem : MonoBehaviour
	{
		public void SetChatInfo(CommunityChatMessage message)
		{
			this.m_message = message;
			this.m_characterName.text = message.Author;
			this.m_bodyText.text = message.Message;
			this.m_timeStamp = message.TimeStamp;
			this.m_postTime.text = this.m_timeStamp.ToShortTimeString();
			this.m_classIcon.sprite = GeneralHelpers.LoadClassIcon((int)this.m_message.ClassID);
			this.m_moderatorImage.SetActive(message.PostedByModerator());
		}

		public void MinimizeChatItem()
		{
			this.m_headerObject.SetActive(false);
			this.m_characterImage.GetComponent<LayoutElement>().minHeight = 0f;
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

		public Image m_classIcon;

		private CommunityChatMessage m_message;

		private DateTime m_timeStamp;
	}
}
