using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class InviteLinkDialog : MonoBehaviour
	{
		private void Awake()
		{
			Club.OnClubAdded += new Club.ClubAddedHandler(this.OnClubAdded);
			Club.OnClubTicketReceived += new Club.ClubTicketReceivedHandler(this.OnTicketReceived);
			this.m_inputField.onEndEdit.AddListener(delegate
			{
				this.ValidateLink();
			});
			this.ResetObjectVisibility();
		}

		private void OnDestroy()
		{
			Club.OnClubAdded -= new Club.ClubAddedHandler(this.OnClubAdded);
			Club.OnClubTicketReceived -= new Club.ClubTicketReceivedHandler(this.OnTicketReceived);
		}

		public void PasteCode()
		{
		}

		public void ConfirmLink()
		{
			Club.RedeemTicket(this.m_validTicketKey);
		}

		public void OnNextClick()
		{
			this.ShowLinkConfirmPanel();
		}

		public void OnBackClick()
		{
			this.HideLinkConfirmPanel();
		}

		private void ValidateLink()
		{
			if (this.m_validTicketKey != this.m_inputField.text)
			{
				if (!string.IsNullOrEmpty(this.m_inputField.text))
				{
					this.m_errorText.gameObject.SetActive(false);
					this.m_nextButton.interactable = false;
					this.m_validTicketKey = null;
					Club.RequestTicket(this.m_inputField.text);
					return;
				}
				this.ResetObjectVisibility();
			}
		}

		private void OnClubAdded(Club.ClubAddedEvent clubAddedEvent)
		{
			base.GetComponent<BaseDialog>().CloseDialog();
		}

		private void OnTicketReceived(Club.ClubTicketReceivedEvent ticketEvent)
		{
			ClubErrorType clubErrorType;
			ClubInfo? clubInfo;
			bool flag;
			Club.GetLastTicketResponse(ticketEvent.Ticket, ref clubErrorType, ref clubInfo, ref flag);
			if (clubErrorType == null)
			{
				if (clubInfo != null)
				{
					ClubInfo value = clubInfo.Value;
					if (value.clubType != 1)
					{
						this.ShowErrorText("COMMUNITIES_WRONG_COMMUNITY");
						return;
					}
					this.m_nextButton.interactable = true;
					this.m_communityText.text = value.name;
					this.m_communityIcon.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, (int)((value.avatarId != 0u) ? value.avatarId : ((uint)StaticDB.communityIconDB.GetRecord(1).IconFileID)));
					this.m_validTicketKey = ticketEvent.Ticket;
					this.m_validClubInfo = value;
				}
			}
			else
			{
				this.ShowErrorText("COMMUNITIES_INVALID_LINK");
			}
		}

		private void ResetObjectVisibility()
		{
			this.m_errorText.gameObject.SetActive(false);
			this.m_nextButton.interactable = false;
		}

		private void ShowErrorText(string key)
		{
			this.m_errorText.gameObject.SetActive(true);
			this.m_errorText.SetNewStringKey(key);
		}

		private void ShowLinkConfirmPanel()
		{
			this.m_inputParentObj.SetActive(false);
			this.m_confirmationParentObj.SetActive(true);
			this.m_windowFrame.sizeDelta = new Vector2(this.m_windowFrame.sizeDelta.x, this.m_confirmationDialogHeight);
		}

		private void HideLinkConfirmPanel()
		{
			this.m_inputParentObj.SetActive(true);
			this.m_confirmationParentObj.SetActive(false);
			this.m_windowFrame.sizeDelta = new Vector2(this.m_windowFrame.sizeDelta.x, this.m_linkInputDialogHeight);
		}

		public InputField m_inputField;

		public LocalizedText m_errorText;

		public Text m_communityText;

		public Button m_nextButton;

		public GameObject m_inputParentObj;

		public GameObject m_confirmationParentObj;

		public RectTransform m_windowFrame;

		public Image m_communityIcon;

		public float m_linkInputDialogHeight;

		public float m_confirmationDialogHeight;

		private string m_validTicketKey;

		private ClubInfo m_validClubInfo;

		private const string COMMUNITIES_INVALID_LINK = "COMMUNITIES_INVALID_LINK";

		private const string COMMUNITIES_WRONG_COMMUNITY = "COMMUNITIES_WRONG_COMMUNITY";
	}
}
