using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WoWCompanionApp
{
	public class DialogFactory : Singleton<DialogFactory>
	{
		private Canvas MainCanvas
		{
			get
			{
				if (this._mainCanvas == null)
				{
					GameObject gameObject = GameObject.FindGameObjectWithTag(DialogFactory.MainCanvasName);
					this._mainCanvas = gameObject.GetComponent<Canvas>();
				}
				return this._mainCanvas;
			}
		}

		private Canvas GameCanvas
		{
			get
			{
				if (this._gameCanvas == null)
				{
					GameObject gameObject = GameObject.FindGameObjectWithTag(DialogFactory.GameCanvasName);
					if (gameObject != null)
					{
						this._gameCanvas = gameObject.GetComponent<Canvas>();
					}
				}
				return this._gameCanvas;
			}
		}

		private Canvas Level2Canvas
		{
			get
			{
				if (this._level2Canvas == null)
				{
					GameObject gameObject = GameObject.FindGameObjectWithTag(DialogFactory.Level2CanvasName);
					if (gameObject != null)
					{
						this._level2Canvas = gameObject.GetComponent<Canvas>();
					}
				}
				return this._level2Canvas;
			}
		}

		private Canvas Level3Canvas
		{
			get
			{
				if (this._level3Canvas == null)
				{
					GameObject gameObject = GameObject.FindGameObjectWithTag(DialogFactory.Level3CanvasName);
					if (gameObject != null)
					{
						this._level3Canvas = gameObject.GetComponent<Canvas>();
					}
				}
				return this._level3Canvas;
			}
		}

		protected new void Awake()
		{
			base.Awake();
			SceneManager.activeSceneChanged += delegate(Scene oldScene, Scene newScene)
			{
				this._mainCanvas = null;
				this._level2Canvas = null;
				this._level3Canvas = null;
			};
		}

		private DialogType InstantiateDialog<DialogType>(DialogType prefab, Canvas parentCanvas) where DialogType : Component
		{
			if (parentCanvas == this.MainCanvas)
			{
				string text = DialogFactory.MainCanvasName;
			}
			else if (parentCanvas == this.GameCanvas)
			{
				string text = DialogFactory.GameCanvasName;
			}
			else if (parentCanvas == this.Level2Canvas)
			{
				string text = DialogFactory.Level2CanvasName;
			}
			else if (parentCanvas == this.Level3Canvas)
			{
				string text = DialogFactory.Level3CanvasName;
			}
			DialogType result = Object.Instantiate<DialogType>(prefab, parentCanvas.transform, false);
			result.gameObject.name = typeof(DialogType).Name;
			return result;
		}

		public MissionDialog CreateMissionDialog(int missionID)
		{
			if (this.m_missionDialog == null)
			{
				this.m_missionDialog = Object.Instantiate<MissionDialog>(this.m_missionDialogPrefab, this.GameCanvas.GetComponentInChildren<GamePanel>().m_missionListPanel.gameObject.transform, false);
				this.m_missionDialog.gameObject.name = "MissionDialog";
			}
			this.m_missionDialog.gameObject.SetActive(true);
			this.m_missionDialog.SetMission(missionID);
			return this.m_missionDialog;
		}

		public void CloseMissionDialog()
		{
			if (this.m_missionDialog == null)
			{
				return;
			}
			this.m_missionDialog.gameObject.SetActive(false);
		}

		public ActivationConfirmationDialog CreateChampionActivationConfirmationDialog(FollowerDetailView followerDetailView)
		{
			ActivationConfirmationDialog activationConfirmationDialog = Object.Instantiate<ActivationConfirmationDialog>(this.m_activationConfirmationDialogPrefab, this.GameCanvas.transform, false);
			activationConfirmationDialog.gameObject.name = "ActivationConfirmationDialog";
			activationConfirmationDialog.Show(followerDetailView);
			return activationConfirmationDialog;
		}

		public DeactivationConfirmationDialog CreateChampionDeactivationConfirmationDialog(FollowerDetailView followerDetailView)
		{
			DeactivationConfirmationDialog deactivationConfirmationDialog = Object.Instantiate<DeactivationConfirmationDialog>(this.m_deactivationConfirmationDialogPrefab, this.GameCanvas.transform, false);
			deactivationConfirmationDialog.gameObject.name = "DeactivationConfirmationDialog";
			deactivationConfirmationDialog.Show(followerDetailView);
			return deactivationConfirmationDialog;
		}

		public AppSettingsDialog CreateAppSettingsDialog()
		{
			Canvas canvas = this.Level3Canvas ?? this.MainCanvas;
			AppSettingsDialog appSettingsDialog = Object.Instantiate<AppSettingsDialog>(this.m_appSettingsDialogPrefab, canvas.transform, false);
			appSettingsDialog.gameObject.name = "AppSettingsDialog";
			appSettingsDialog.gameObject.SetActive(true);
			TiledRandomTexture[] componentsInChildren = this.m_appSettingsDialogPrefab.GetComponentsInChildren<TiledRandomTexture>();
			TiledRandomTexture[] componentsInChildren2 = appSettingsDialog.GetComponentsInChildren<TiledRandomTexture>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				TiledRandomTexture divider = componentsInChildren2[i];
				RectTransform rectTransform = divider.transform as RectTransform;
				Rect rect = rectTransform.rect;
				TiledRandomTexture tiledRandomTexture = componentsInChildren.FirstOrDefault((TiledRandomTexture tex) => tex.gameObject.name == divider.gameObject.name);
				if (tiledRandomTexture != null)
				{
					RectTransform rectTransform2 = tiledRandomTexture.transform as RectTransform;
					rectTransform.localScale = rectTransform2.localScale;
					rectTransform.anchoredPosition = rectTransform2.anchoredPosition;
				}
			}
			return appSettingsDialog;
		}

		public CharacterViewPanel CreateCharacterViewPanel()
		{
			return Object.Instantiate<CharacterViewPanel>(this.m_characterViewPanelPrefab, this.Level3Canvas.transform, false);
		}

		public MissionTypeDialog CreateMissionTypeDialog()
		{
			MissionTypeDialog missionTypeDialog = Object.Instantiate<MissionTypeDialog>(this.m_missionTypeDialogPrefab, this.Level3Canvas.transform, false);
			missionTypeDialog.gameObject.name = "MissionTypeDialog";
			return missionTypeDialog;
		}

		public MissionInfoDialog CreateMissionInfoDialog()
		{
			MissionInfoDialog missionInfoDialog = Object.Instantiate<MissionInfoDialog>(this.m_missionInfoDialogPrefab, this.Level3Canvas.transform, false);
			missionInfoDialog.gameObject.name = "MissionInfoDialog";
			return missionInfoDialog;
		}

		public CommunitiesChatContextMenu CreateChatContextMenu(CommunityChatMessage message)
		{
			if (this.m_communitesChatContextMenu == null)
			{
				return null;
			}
			CommunitiesChatContextMenu communitiesChatContextMenu = Object.Instantiate<CommunitiesChatContextMenu>(this.m_communitesChatContextMenu, this.Level3Canvas.transform, false);
			communitiesChatContextMenu.SetContextByMessage(message);
			communitiesChatContextMenu.gameObject.name = "CommunitiesChatContextMenu";
			return communitiesChatContextMenu;
		}

		public ReportPlayerDialog CreateReportPlayerDialog()
		{
			if (this.m_reportPlayerDialogPrefab == null)
			{
				return null;
			}
			ReportPlayerDialog reportPlayerDialog = Object.Instantiate<ReportPlayerDialog>(this.m_reportPlayerDialogPrefab, this.Level3Canvas.transform, false);
			reportPlayerDialog.gameObject.name = "ReportPlayerDialog";
			return reportPlayerDialog;
		}

		public OKCancelDialog CreateOKCancelDialog(string titleKey, string messageKey, Action onOK, Action onCancel = null)
		{
			Canvas canvas = this.Level3Canvas ?? this.MainCanvas;
			OKCancelDialog okcancelDialog = Object.Instantiate<OKCancelDialog>(this.m_okCancelDialogPrefab, canvas.transform, false);
			okcancelDialog.gameObject.name = "OKCancelDialog";
			okcancelDialog.SetText(titleKey, messageKey);
			okcancelDialog.onOK += onOK;
			if (onCancel != null)
			{
				okcancelDialog.onCancel += onCancel;
			}
			return okcancelDialog;
		}

		public EventInviteResponseDialog CreateEventInviteResponseDialog(CalendarEventItem eventItem)
		{
			EventInviteResponseDialog eventInviteResponseDialog = this.InstantiateDialog<EventInviteResponseDialog>(this.m_eventInviteResponseDialogPrefab, this.Level3Canvas);
			eventInviteResponseDialog.SetCalendarEvent(eventItem);
			return eventInviteResponseDialog;
		}

		public ArticleView CreateArticleViewPanel(NewsArticle article)
		{
			ArticleView articleView = this.InstantiateDialog<ArticleView>(this.m_ArticleViewPanelPrefab, this.Level3Canvas);
			articleView.SetArticle(article);
			return articleView;
		}

		public AddInviteDialog CreateAddInviteDialog(EventInviteResponseDialog eventDialog)
		{
			AddInviteDialog addInviteDialog = this.InstantiateDialog<AddInviteDialog>(this.m_addInviteDialogPrefab, this.Level3Canvas);
			addInviteDialog.EventDialog = eventDialog;
			return addInviteDialog;
		}

		public CalendarFiltersDialog CreateCalendarFiltersDialog(EventsListPanel eventsListPanel)
		{
			CalendarFiltersDialog calendarFiltersDialog = this.InstantiateDialog<CalendarFiltersDialog>(this.m_calendarFiltersDialogPrefab, this.Level3Canvas);
			calendarFiltersDialog.EventsListPanel = eventsListPanel;
			return calendarFiltersDialog;
		}

		public CommunityDescriptionDialog CreateCommunityDescriptionDialog(CommunityPendingInvite pendingInvite)
		{
			CommunityDescriptionDialog communityDescriptionDialog = this.InstantiateDialog<CommunityDescriptionDialog>(this.m_communityDescriptionDialogPrefab, this.Level3Canvas);
			communityDescriptionDialog.PopulateFromInvite(pendingInvite);
			return communityDescriptionDialog;
		}

		public HolidayDetailsPanel CreateHolidayDetailsPanel(CalendarEventData eventData)
		{
			HolidayDetailsPanel holidayDetailsPanel = this.InstantiateDialog<HolidayDetailsPanel>(this.m_holidayDetailsPanel, this.Level3Canvas);
			holidayDetailsPanel.SetCalendarEventData(eventData);
			return holidayDetailsPanel;
		}

		private static readonly string MainCanvasName = "MainCanvas";

		private static readonly string GameCanvasName = "GameCanvas";

		private static readonly string Level2CanvasName = "Level2Canvas";

		private static readonly string Level3CanvasName = "Level3Canvas";

		private Canvas _mainCanvas;

		private Canvas _gameCanvas;

		private Canvas _level2Canvas;

		private Canvas _level3Canvas;

		public BaseDialog m_baseDialogPrefab;

		public OKCancelDialog m_okCancelDialogPrefab;

		public MissionDialog m_missionDialogPrefab;

		private MissionDialog m_missionDialog;

		public ActivationConfirmationDialog m_activationConfirmationDialogPrefab;

		public DeactivationConfirmationDialog m_deactivationConfirmationDialogPrefab;

		public MissionTypeDialog m_missionTypeDialogPrefab;

		public MissionInfoDialog m_missionInfoDialogPrefab;

		public CommunitiesChatContextMenu m_communitesChatContextMenu;

		public AppSettingsDialog m_appSettingsDialogPrefab;

		public ReportPlayerDialog m_reportPlayerDialogPrefab;

		public CharacterViewPanel m_characterViewPanelPrefab;

		public EventInviteResponseDialog m_eventInviteResponseDialogPrefab;

		public ArticleView m_ArticleViewPanelPrefab;

		public AddInviteDialog m_addInviteDialogPrefab;

		public CalendarFiltersDialog m_calendarFiltersDialogPrefab;

		public CommunityDescriptionDialog m_communityDescriptionDialogPrefab;

		public HolidayDetailsPanel m_holidayDetailsPanel;
	}
}
