using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChatItem : MonoBehaviour
	{
		public void Start()
		{
			HoldPressTrigger component = base.GetComponent<HoldPressTrigger>();
			if (component != null)
			{
				component.SetCallback(new Action(this.HoldPressEvent));
			}
		}

		public void HoldPressEvent()
		{
			if (this.CanOpenContextMenu())
			{
				Singleton<DialogFactory>.Instance.CreateChatContextMenu(this.m_message);
			}
		}

		public void SetChatInfo(CommunityChatMessage message)
		{
			this.m_message = message;
			this.m_characterName.text = message.Author;
			this.m_bodyText.text = message.Message;
			this.m_bodyText.fontStyle = ((!message.Destroyed) ? 0 : 2);
			this.m_postTime.text = message.TimeStamp.ToShortTimeString();
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

		public bool IsSameMessage(CommunityChatMessage compareMessage)
		{
			return compareMessage.MessageIdentifier.epoch == this.m_message.MessageIdentifier.epoch && compareMessage.MessageIdentifier.position == this.m_message.MessageIdentifier.position;
		}

		public DateTime TimeStamp
		{
			get
			{
				return this.m_message.TimeStamp;
			}
		}

		private bool CanOpenContextMenu()
		{
			return !this.m_message.Destroyed && (this.m_message.CanBeDestroyed() || ReportSystem.CanReportPlayer(this.m_message.GetAsPlayerLocation()));
		}

		public bool PostMadeByMemberID(uint memberID)
		{
			return this.m_message.MemberID == memberID;
		}

		public void HandleMemberUpdatedEvent(Club.ClubMemberUpdatedEvent memberEvent)
		{
			this.m_message.HandleMemberUpdatedEvent(memberEvent);
			this.SetChatInfo(this.m_message);
		}

		public Text m_characterName;

		public Text m_postTime;

		public Text m_bodyText;

		public GameObject m_headerObject;

		public GameObject m_characterImage;

		public GameObject m_moderatorImage;

		public Image m_classIcon;

		private CommunityChatMessage m_message;
	}
}
