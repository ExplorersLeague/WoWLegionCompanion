using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AdvancedInputFieldPlugin;
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
			Club.OnClubRemoved += new Club.ClubRemovedHandler(this.OnClubRemoved);
			Club.OnClubStreamRemoved += new Club.ClubStreamRemovedHandler(this.OnStreamRemoved);
			Club.OnClubStreamUpdated += new Club.ClubStreamUpdatedHandler(this.OnStreamUpdated);
			Club.OnClubMemberRoleUpdated += new Club.ClubMemberRoleUpdatedHandler(this.OnRoleUpdatedEvent);
		}

		private void OnDestroy()
		{
			Club.OnClubStreamSubscribed -= new Club.ClubStreamSubscribedHandler(this.OnStreamSubscribed);
			Club.OnClubMessageHistoryReceived -= new Club.ClubMessageHistoryReceivedHandler(this.OnMessageHistoryReceived);
			Club.OnClubMessageAdded -= new Club.ClubMessageAddedHandler(this.OnMessageAdded);
			Club.OnStreamViewMarkerUpdated -= new Club.StreamViewMarkerUpdatedHandler(this.OnViewMarkerUpdated);
			Club.OnClubMessageUpdated -= new Club.ClubMessageUpdatedHandler(this.OnMessageUpdated);
			Club.OnClubRemoved -= new Club.ClubRemovedHandler(this.OnClubRemoved);
			Club.OnClubStreamRemoved -= new Club.ClubStreamRemovedHandler(this.OnStreamRemoved);
			Club.OnClubStreamUpdated -= new Club.ClubStreamUpdatedHandler(this.OnStreamUpdated);
			Club.OnClubMemberRoleUpdated -= new Club.ClubMemberRoleUpdatedHandler(this.OnRoleUpdatedEvent);
		}

		private void OnDisable()
		{
			this.ResetFocusedStream();
			if (this.m_advInputField.Selected)
			{
				this.m_advInputField.ManualDeselect(4);
			}
			this.m_advInputField.Clear();
		}

		private void OnApplicationPause(bool pause)
		{
			if (this.m_advInputField.Selected)
			{
				this.m_advInputField.ManualDeselect(4);
			}
		}

		private void OnApplicationFocus(bool focus)
		{
			if (this.m_advInputField.Selected)
			{
				this.m_advInputField.ManualDeselect(4);
			}
		}

		private void Update()
		{
			this.AdjustForKeyboard();
		}

		private void ResetChatPanel()
		{
			this.m_earliestDate = (this.m_latestDate = string.Empty);
			this.m_lastChatItem = null;
			this.m_advInputField.Clear();
			if (this.m_advInputField.Selected)
			{
				this.m_advInputField.ManualDeselect(4);
			}
			if (this.m_channelSelectDialog != null)
			{
				this.m_channelSelectDialog.SetActive(false);
			}
			if (this.m_channelSettingsDialog != null)
			{
				this.m_channelSettingsDialog.SetActive(false);
			}
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
				this.m_community = null;
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
				this.m_headerText.text = this.m_focusedStream.Name;
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
			if (this.m_focusedStream != null && this.m_community != null)
			{
				this.m_otherChannelNotification.SetActive(this.m_community.HasUnreadMessages(this.m_focusedStream));
				this.m_otherCommunityNotification.SetActive(CommunityData.Instance.HasUnreadCommunityMessages(this.m_community));
			}
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
			if (this.m_focusedStream != null)
			{
				this.m_focusedStream.HandleClubMessageHistoryEvent(historyEvent);
				this.RebuildMessageList();
			}
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
				this.m_focusedStream.ClearNotifications();
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
			CultureInfo cultureInfoLocale = MobileDeviceLocale.GetCultureInfoLocale();
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = cultureInfoLocale;
			string text = message.TimeStamp.ToString(StaticDB.GetString("FULL_DATE", "dddd, MMMM d, yyyy"));
			if (this.m_earliestDate == string.Empty)
			{
				this.m_earliestDate = text;
			}
			if (this.m_latestDate != text)
			{
				this.m_latestDate = text;
				GameObject gameObject = this.m_chatContent.AddAsChildObject(this.m_dateObjectPrefab);
				Text componentInChildren = gameObject.GetComponentInChildren<Text>();
				componentInChildren.text = text;
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
			Thread.CurrentThread.CurrentCulture = currentCulture;
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
			if (this.m_advInputField.Text != string.Empty)
			{
				this.m_focusedStream.AddMessage(this.m_advInputField.Text);
				this.m_advInputField.Text = string.Empty;
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

		public void ForceCloseChatPanel()
		{
			this.ResetChatPanel();
			this.ResetFocusedStream();
			this.CloseChatPanel();
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
			GameObject gameObject = Main.instance.AddChildToLevel2Canvas(this.m_notificationSettingsPrefab);
			gameObject.GetComponentInChildren<CommunityNotificationsDialog>().SetCommunity(this.m_community);
		}

		public void UpdatePendingMessages(Club.ClubMemberUpdatedEvent memberEvent)
		{
			if (this.m_community != null && this.m_community.ClubId == memberEvent.ClubID && this.m_pendingMemberIDs.Contains(memberEvent.MemberID))
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

		private void OnClubRemoved(Club.ClubRemovedEvent clubRemovedEvent)
		{
			if (this.m_community != null && clubRemovedEvent.ClubID == this.m_community.ClubId && base.gameObject.activeSelf)
			{
				string baseTag = (!this.m_community.IsGuild()) ? "COMMUNITY_NO_LONGER_VALID" : "GUILD_NO_LONGER_VALID";
				this.ForceCloseChatPanel();
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString(baseTag, "[PH] You've been removed from this Community/Guild."));
			}
		}

		private void OnStreamRemoved(Club.ClubStreamRemovedEvent streamRemovedEvent)
		{
			if (this.m_focusedStream != null && this.m_community != null && streamRemovedEvent.StreamID == this.m_focusedStream.StreamId && streamRemovedEvent.ClubID == this.m_community.ClubId && base.gameObject.activeSelf)
			{
				this.ForceCloseChatPanel();
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("CHANNEL_NO_LONGER_VALID", "[PH] The channel is no longer valid."));
			}
		}

		private void OnStreamUpdated(Club.ClubStreamUpdatedEvent streamUpdatedEvent)
		{
			if (this.m_focusedStream != null && this.m_community != null && streamUpdatedEvent.StreamID == this.m_focusedStream.StreamId && streamUpdatedEvent.ClubID == this.m_community.ClubId && base.gameObject.activeSelf && !this.m_community.CanAccessUpdatedChannel(streamUpdatedEvent))
			{
				this.ForceCloseChatPanel();
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("CHANNEL_NO_LONGER_VALID", "[PH] The channel is no longer valid."));
			}
		}

		private void OnRoleUpdatedEvent(Club.ClubMemberRoleUpdatedEvent roleUpdatedEvent)
		{
			if (this.m_focusedStream != null && this.m_community != null && roleUpdatedEvent.ClubID == this.m_community.ClubId && base.gameObject.activeSelf)
			{
				CommunityMember updatedMember = this.m_community.GetUpdatedMember(roleUpdatedEvent);
				if (updatedMember != null && updatedMember.IsSelf && !updatedMember.IsModerator && this.m_focusedStream.ForLeadersAndModerators)
				{
					this.ForceCloseChatPanel();
					AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("CHANNEL_NO_LONGER_VALID", "[PH] The channel is no longer valid."));
				}
			}
		}

		public void AdjustForKeyboard()
		{
			float num = 0f;
			if (this.m_advInputField.Selected)
			{
				num = this.m_parentCanvasScalar.referenceResolution.y * this.GetOnScreenKeyboardRatio();
			}
			RectTransform rectTransform = base.transform as RectTransform;
			if (num != rectTransform.offsetMin.y)
			{
				rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, num);
				this.ScrollToBottom();
			}
		}

		public float GetOnScreenKeyboardRatio()
		{
			float result;
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView", new object[0]);
				using (AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("android.graphics.Rect", new object[0]))
				{
					androidJavaObject.Call("getWindowVisibleDisplayFrame", new object[]
					{
						androidJavaObject2
					});
					result = (float)(Screen.height - androidJavaObject2.Call<int>("height", new object[0])) / (float)Screen.height;
				}
			}
			return result;
		}

		public GameObject m_chatContent;

		public GameObject m_chatObjectPrefab;

		public GameObject m_dateObjectPrefab;

		public GameObject m_notificationSettingsPrefab;

		public GameObject m_channelRosterPrefab;

		public GameObject m_channelSelectDialog;

		public GameObject m_otherCommunityNotification;

		public GameObject m_otherChannelNotification;

		public GameObject m_channelSettingsDialog;

		public AdvancedInputField m_advInputField;

		public Text m_headerText;

		public CanvasScaler m_parentCanvasScalar;

		private string m_earliestDate;

		private string m_latestDate;

		private Community m_community;

		private CommunityStream m_focusedStream;

		private CommunityChatItem m_lastChatItem;

		private int m_childCountBeforeRefresh;

		private bool m_requestPending;

		private const int MINUTES_FOR_MINIMIZE = 5;

		private const string MON_DAY_YEAR_FORMAT = "MMM dd, yyyy";

		private HashSet<uint> m_pendingMemberIDs = new HashSet<uint>();
	}
}
