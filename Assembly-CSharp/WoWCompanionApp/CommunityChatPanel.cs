using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CommunityChatPanel : MonoBehaviour
	{
		private void Awake()
		{
			base.gameObject.transform.localScale = Vector3.one;
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			Vector2 zero = Vector2.zero;
			base.gameObject.GetComponent<RectTransform>().offsetMax = zero;
			component.offsetMin = zero;
			ScrollRect componentInChildren = base.GetComponentInChildren<ScrollRect>();
			componentInChildren.onValueChanged.AddListener(delegate
			{
				this.OnContentScroll();
			});
			Club.OnClubStreamSubscribed += new Club.ClubStreamSubscribedHandler(this.OnStreamSubscribed);
			Club.OnClubMessageHistoryReceived += new Club.ClubMessageHistoryReceivedHandler(this.OnMessageHistoryReceived);
			Club.OnClubMessageAdded += new Club.ClubMessageAddedHandler(this.OnMessageAdded);
			Club.OnStreamViewMarkerUpdated += new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
			Club.OnClubMessageUpdated += new Club.ClubMessageUpdatedHandler(this.OnMessageUpdated);
		}

		private void OnDestroy()
		{
			Club.OnClubStreamSubscribed -= new Club.ClubStreamSubscribedHandler(this.OnStreamSubscribed);
			Club.OnClubMessageHistoryReceived -= new Club.ClubMessageHistoryReceivedHandler(this.OnMessageHistoryReceived);
			Club.OnClubMessageAdded -= new Club.ClubMessageAddedHandler(this.OnMessageAdded);
			Club.OnStreamViewMarkerUpdated -= new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
			Club.OnClubMessageUpdated -= new Club.ClubMessageUpdatedHandler(this.OnMessageUpdated);
		}

		private void OnDisable()
		{
			this.ResetFocusedStream();
		}

		private void Update()
		{
		}

		private void ResetChatPanel()
		{
			this.m_earliestDate = (this.m_latestDate = string.Empty);
			this.m_lastChatItem = null;
			this.m_chatContent.DetachAllChildren();
		}

		private void ResetFocusedStream()
		{
			if (this.m_focusedStream != null)
			{
				this.m_childCountBeforeRefresh = 0;
				this.m_requestPending = false;
				this.m_focusedStream.UnfocusStream();
				this.m_focusedStream = null;
				this.m_pendingMemberIDs.Clear();
			}
		}

		public void InitializeChatContent(Community community, CommunityStream stream)
		{
			if (this.m_focusedStream == null || this.m_focusedStream.StreamId != stream.StreamId)
			{
				this.ResetFocusedStream();
				this.ResetChatPanel();
				this.m_community = community;
				this.m_focusedStream = stream;
				this.m_headerText.text = this.m_focusedStream.Name.ToUpper();
				this.UpdateNotificationMarkers();
				this.m_focusedStream.FocusStream();
				if (this.m_focusedStream.IsSubscribed())
				{
					this.BuildMessageList();
				}
				this.m_channelSelectDialog.GetComponent<CommunityChannelDialog>().InitializeContentPane(this.m_community, new UnityAction<CommunityChannelButton>(base.GetComponentInParent<SocialPanel>().SelectChannelButton), delegate
				{
					this.m_channelSelectDialog.SetActive(false);
				});
			}
			this.m_focusedStream.ClearNotifications();
		}

		private void UpdateNotificationMarkers()
		{
			this.m_otherChannelNotification.SetActive(this.m_community.HasUnreadMessages(this.m_focusedStream));
			this.m_otherCommunityNotification.SetActive(CommunityData.Instance.HasUnreadCommunityMessages(this.m_community));
		}

		private void OnStreamSubscribed(Club.ClubStreamSubscribedEvent subscribeEvent)
		{
			if (this.m_focusedStream.RequestMoreMessages())
			{
				this.GetPreloadedMessages();
			}
			else
			{
				this.m_requestPending = true;
			}
		}

		private void GetPreloadedMessages()
		{
		}

		private void OnMessageHistoryReceived(Club.ClubMessageHistoryReceivedEvent historyEvent)
		{
			this.m_childCountBeforeRefresh = this.m_chatContent.transform.childCount;
			this.m_focusedStream.HandleClubMessageHistoryEvent(historyEvent);
			this.RebuildMessageList();
			this.m_requestPending = false;
		}

		private void OnMessageAdded(Club.ClubMessageAddedEvent messageEvent)
		{
			CommunityChatMessage communityChatMessage = null;
			if (this.m_focusedStream != null)
			{
				communityChatMessage = this.m_focusedStream.HandleMessageAddedEvent(messageEvent);
			}
			if (communityChatMessage != null)
			{
				this.AddChatMessage(communityChatMessage);
				this.ScrollToBottom();
			}
			else
			{
				CommunityData.Instance.HandleMessageAddedEvent(messageEvent);
			}
		}

		private void OnMessageUpdated(Club.ClubMessageUpdatedEvent messageEvent)
		{
			CommunityChatMessage newMessage = null;
			if (this.m_focusedStream != null)
			{
				newMessage = this.m_focusedStream.HandleMessageUpdatedEvent(messageEvent);
			}
			if (newMessage != null)
			{
				CommunityChatItem communityChatItem = this.m_chatContent.GetComponentsInChildren<CommunityChatItem>().FirstOrDefault((CommunityChatItem item) => item.IsSameMessage(newMessage));
				if (communityChatItem != null)
				{
					communityChatItem.SetChatInfo(newMessage);
				}
			}
			else
			{
				CommunityData.Instance.HandleMessageUpdatedEvent(messageEvent);
			}
		}

		private void OnViewMarkerUpdated(Club.StreamViewMarkerUpdatedEvent markerEvent)
		{
			this.UpdateNotificationMarkers();
		}

		private void RebuildMessageList()
		{
			this.ResetChatPanel();
			this.BuildMessageList();
		}

		private void BuildMessageList()
		{
			foreach (CommunityChatMessage message in this.m_focusedStream.GetMessages())
			{
				this.AddChatMessage(message);
			}
			this.ScrollToBottom();
			if (this.m_childCountBeforeRefresh != 0)
			{
				this.SnapTo(this.m_chatContent.transform.GetChild(this.m_chatContent.transform.childCount - this.m_childCountBeforeRefresh).GetComponent<RectTransform>());
			}
		}

		private void ScrollToBottom()
		{
			Canvas.ForceUpdateCanvases();
			ScrollRect componentInChildren = base.GetComponentInChildren<ScrollRect>();
			componentInChildren.verticalNormalizedPosition = 0f;
		}

		private void AddChatMessage(CommunityChatMessage message)
		{
			string text = message.TimeStamp.ToString("MMM dd, yyyy");
			if (this.m_earliestDate == string.Empty)
			{
				this.m_earliestDate = text;
			}
			if (this.m_latestDate != text)
			{
				this.m_latestDate = text;
				GameObject gameObject = this.m_chatContent.AddAsChildObject(this.m_dateObjectPrefab);
				Text componentInChildren = gameObject.GetComponentInChildren<Text>();
				componentInChildren.text = text.ToUpper();
			}
			GameObject gameObject2 = this.m_chatContent.AddAsChildObject(this.m_chatObjectPrefab);
			CommunityChatItem component = gameObject2.GetComponent<CommunityChatItem>();
			component.SetChatInfo(message);
			if (string.IsNullOrEmpty(message.Author))
			{
				if (this.m_pendingMemberIDs.Count == 0)
				{
					Club.OnClubMemberUpdated += new Club.ClubMemberUpdatedHandler(this.UpdatePendingMessages);
				}
				this.m_pendingMemberIDs.Add(message.MemberID);
			}
			if (this.ShouldMinimizeChatItem(message))
			{
				gameObject2.GetComponent<CommunityChatItem>().MinimizeChatItem();
			}
			else
			{
				this.m_lastChatItem = component;
			}
		}

		private bool ShouldMinimizeChatItem(CommunityChatMessage message)
		{
			if (this.m_lastChatItem == null)
			{
				return false;
			}
			TimeSpan timeSpan = message.TimeStamp - this.m_lastChatItem.TimeStamp;
			return this.m_lastChatItem.GetAuthor() == message.Author && timeSpan.Minutes < 5 && timeSpan.Days == 0 && timeSpan.Hours == 0;
		}

		public void SendChatMessage()
		{
			if (this.m_chatInputText.text != string.Empty)
			{
				this.m_focusedStream.AddMessage(this.m_chatInputText.text);
			}
		}

		private void OnContentScroll()
		{
			ScrollRect componentInChildren = base.GetComponentInChildren<ScrollRect>();
			if (!this.m_requestPending && componentInChildren.verticalNormalizedPosition == 1f && this.m_focusedStream != null)
			{
				if (this.m_focusedStream.RequestMoreMessages())
				{
					this.GetPreloadedMessages();
				}
				else
				{
					this.m_requestPending = true;
				}
			}
		}

		public void SnapTo(RectTransform target)
		{
			RectTransform component = this.m_chatContent.GetComponent<RectTransform>();
			float height = base.GetComponentInChildren<RectTransform>().rect.height;
			float height2 = component.rect.height;
			if (height2 < height)
			{
				return;
			}
			Canvas.ForceUpdateCanvases();
			ScrollRect componentInChildren = base.GetComponentInChildren<ScrollRect>();
			Vector2 anchoredPosition = componentInChildren.transform.InverseTransformPoint(component.position) - componentInChildren.transform.InverseTransformPoint(target.position);
			if (anchoredPosition.y + height > height2)
			{
				anchoredPosition.y = height2 - height + 60f;
			}
			component.anchoredPosition = anchoredPosition;
			component.offsetMin = new Vector2(40f, component.offsetMin.y);
			component.offsetMax = new Vector2(-40f, component.offsetMax.y);
		}

		public void CloseChatPanel()
		{
			base.gameObject.GetComponentInParent<SocialPanel>().CloseChatPanel();
		}

		public void ShowRoster()
		{
			GameObject gameObject = Main.instance.AddChildToLevel2Canvas(this.m_channelRosterPrefab);
			CommunityRosterDialog component = gameObject.GetComponent<CommunityRosterDialog>();
			component.SetRosterData(this.m_community);
		}

		public void OpenNotificationSettings()
		{
			Main.instance.AddChildToLevel2Canvas(this.m_notificationSettingsPrefab);
		}

		public void UpdatePendingMessages(Club.ClubMemberUpdatedEvent memberEvent)
		{
			if (this.m_community.ClubId == memberEvent.ClubID && this.m_pendingMemberIDs.Contains(memberEvent.MemberID))
			{
				IEnumerable<CommunityChatItem> enumerable = from item in this.m_chatContent.GetComponentsInChildren<CommunityChatItem>()
				where item.PostMadeByMemberID(memberEvent.MemberID)
				select item;
				foreach (CommunityChatItem communityChatItem in enumerable)
				{
					communityChatItem.HandleMemberUpdatedEvent(memberEvent);
				}
				this.m_pendingMemberIDs.Remove(memberEvent.MemberID);
				if (this.m_pendingMemberIDs.Count == 0)
				{
					Club.OnClubMemberUpdated -= new Club.ClubMemberUpdatedHandler(this.UpdatePendingMessages);
				}
			}
		}

		public GameObject m_chatContent;

		public GameObject m_chatObjectPrefab;

		public GameObject m_dateObjectPrefab;

		public GameObject m_notificationSettingsPrefab;

		public GameObject m_channelRosterPrefab;

		public GameObject m_channelSelectDialog;

		public GameObject m_otherCommunityNotification;

		public GameObject m_otherChannelNotification;

		public InputField m_chatInputText;

		public Text m_headerText;

		private string m_earliestDate;

		private string m_latestDate;

		private Community m_community;

		private CommunityStream m_focusedStream;

		private CommunityChatItem m_lastChatItem;

		private int m_childCountBeforeRefresh;

		private bool m_requestPending;

		private bool m_showingChannelSelect;

		private const int MINUTES_FOR_MINIMIZE = 5;

		private const string MON_DAY_YEAR_FORMAT = "MMM dd, yyyy";

		private HashSet<uint> m_pendingMemberIDs = new HashSet<uint>();
	}
}
