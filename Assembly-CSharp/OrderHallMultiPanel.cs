using System;
using UnityEngine;
using UnityEngine.UI;

public class OrderHallMultiPanel : MonoBehaviour
{
	private void Start()
	{
		if (this.m_titleText != null)
		{
			this.m_titleText.font = GeneralHelpers.LoadStandardFont();
			this.m_titleText.text = StaticDB.GetString("CLASS_ORDER_HALL", null);
		}
		this.m_troopButtonText.font = GeneralHelpers.LoadStandardFont();
		this.m_troopButtonText.text = StaticDB.GetString("RECRUIT", null);
		this.m_followerButtonText.font = GeneralHelpers.LoadStandardFont();
		this.m_followerButtonText.text = StaticDB.GetString("FOLLOWERS", null);
		this.m_allyButtonText.font = GeneralHelpers.LoadStandardFont();
		this.m_allyButtonText.text = StaticDB.GetString("MISSIONS", null);
		this.m_talentButtonText.font = GeneralHelpers.LoadStandardFont();
		this.m_talentButtonText.text = StaticDB.GetString("RESEARCH", null);
		this.m_worldMapButtonText.font = GeneralHelpers.LoadStandardFont();
		this.m_worldMapButtonText.text = StaticDB.GetString("WORLD_MAP", null);
		Text[] componentsInChildren = base.GetComponentsInChildren<Text>(true);
		foreach (Text text in componentsInChildren)
		{
			if (text.text == "Abilities")
			{
				text.text = StaticDB.GetString("ABILITIES", null);
			}
			else if (text.text == "Counters:")
			{
				text.text = StaticDB.GetString("COUNTERS", null) + ":";
			}
		}
		this.m_defaultNavButton.SelectMe();
	}

	private void HideMainPanels()
	{
		if (this.m_actuallyDisablePanels)
		{
			this.m_missionListPanelCanvasGroup.alpha = 1f;
			this.m_missionListPanelCanvasGroup.interactable = true;
			this.m_missionListPanelCanvasGroup.blocksRaycasts = true;
			this.m_troopsPanelCanvasGroup.alpha = 1f;
			this.m_troopsPanelCanvasGroup.interactable = true;
			this.m_troopsPanelCanvasGroup.blocksRaycasts = true;
			this.m_adventureMapPanelCanvasGroup.alpha = 1f;
			this.m_adventureMapPanelCanvasGroup.interactable = true;
			this.m_adventureMapPanelCanvasGroup.blocksRaycasts = true;
			this.m_playerInfoDisplayCanvasGroup.alpha = 1f;
			this.m_playerInfoDisplayCanvasGroup.interactable = true;
			this.m_playerInfoDisplayCanvasGroup.blocksRaycasts = true;
			this.m_followersPanelCanvasGroup.alpha = 1f;
			this.m_followersPanelCanvasGroup.interactable = true;
			this.m_followersPanelCanvasGroup.blocksRaycasts = true;
			this.m_talentTreePanelCanvasGroup.alpha = 1f;
			this.m_talentTreePanelCanvasGroup.interactable = true;
			this.m_talentTreePanelCanvasGroup.blocksRaycasts = true;
			this.m_missionListPanelCanvasGroup.gameObject.SetActive(false);
			this.m_troopsPanelCanvasGroup.gameObject.SetActive(false);
			this.m_adventureMapPanelCanvasGroup.gameObject.SetActive(false);
			this.m_followersPanelCanvasGroup.gameObject.SetActive(false);
			this.m_talentTreePanelCanvasGroup.gameObject.SetActive(false);
		}
		else
		{
			this.m_missionListPanelRTH.DisableTargetList();
			this.m_troopsPanelRTH.DisableTargetList();
			this.m_adventureMapPanelRTH.DisableTargetList();
			this.m_followersPanelRTH.DisableTargetList();
			this.m_talentTreePanelRTH.DisableTargetList();
			this.m_missionListPanelCanvasGroup.alpha = 0f;
			this.m_missionListPanelCanvasGroup.interactable = false;
			this.m_missionListPanelCanvasGroup.blocksRaycasts = false;
			this.m_troopsPanelCanvasGroup.alpha = 0f;
			this.m_troopsPanelCanvasGroup.interactable = false;
			this.m_troopsPanelCanvasGroup.blocksRaycasts = false;
			this.m_adventureMapPanelCanvasGroup.alpha = 0f;
			this.m_adventureMapPanelCanvasGroup.interactable = false;
			this.m_adventureMapPanelCanvasGroup.blocksRaycasts = false;
			this.m_playerInfoDisplayCanvasGroup.alpha = 0f;
			this.m_playerInfoDisplayCanvasGroup.interactable = false;
			this.m_playerInfoDisplayCanvasGroup.blocksRaycasts = false;
			this.m_followersPanelCanvasGroup.alpha = 0f;
			this.m_followersPanelCanvasGroup.interactable = false;
			this.m_followersPanelCanvasGroup.blocksRaycasts = false;
			this.m_talentTreePanelCanvasGroup.alpha = 0f;
			this.m_talentTreePanelCanvasGroup.interactable = false;
			this.m_talentTreePanelCanvasGroup.blocksRaycasts = false;
		}
	}

	public void ShowMissionListPanel()
	{
		this.HideMainPanels();
		if (this.m_actuallyDisablePanels)
		{
			this.m_missionListPanelCanvasGroup.gameObject.SetActive(true);
		}
		else
		{
			this.m_missionListPanelRTH.EnableTargetList();
			this.m_missionListPanelCanvasGroup.alpha = 1f;
			this.m_missionListPanelCanvasGroup.interactable = true;
			this.m_missionListPanelCanvasGroup.blocksRaycasts = true;
		}
	}

	public void ShowTroopsPanel()
	{
		this.HideMainPanels();
		if (this.m_actuallyDisablePanels)
		{
			this.m_troopsPanelCanvasGroup.gameObject.SetActive(true);
		}
		else
		{
			this.m_troopsPanelRTH.EnableTargetList();
			this.m_troopsPanelCanvasGroup.alpha = 1f;
			this.m_troopsPanelCanvasGroup.interactable = true;
			this.m_troopsPanelCanvasGroup.blocksRaycasts = true;
		}
	}

	public void ShowAdventureMapPanel()
	{
		this.HideMainPanels();
		if (this.m_actuallyDisablePanels)
		{
			this.m_adventureMapPanelCanvasGroup.gameObject.SetActive(true);
		}
		else
		{
			this.m_adventureMapPanelRTH.EnableTargetList();
			this.m_adventureMapPanelCanvasGroup.alpha = 1f;
			this.m_adventureMapPanelCanvasGroup.interactable = true;
			this.m_adventureMapPanelCanvasGroup.blocksRaycasts = true;
			this.m_playerInfoDisplayCanvasGroup.alpha = 1f;
			this.m_playerInfoDisplayCanvasGroup.interactable = true;
			this.m_playerInfoDisplayCanvasGroup.blocksRaycasts = true;
		}
	}

	public void ShowFollowersPanel()
	{
		this.HideMainPanels();
		if (this.m_actuallyDisablePanels)
		{
			this.m_followersPanelCanvasGroup.gameObject.SetActive(true);
		}
		else
		{
			this.m_followersPanelRTH.EnableTargetList();
			this.m_followersPanelCanvasGroup.alpha = 1f;
			this.m_followersPanelCanvasGroup.interactable = true;
			this.m_followersPanelCanvasGroup.blocksRaycasts = true;
		}
	}

	public void ShowTalentTreePanel()
	{
		this.HideMainPanels();
		if (this.m_actuallyDisablePanels)
		{
			this.m_talentTreePanelCanvasGroup.gameObject.SetActive(true);
		}
		else
		{
			this.m_talentTreePanelRTH.EnableTargetList();
			this.m_talentTreePanelCanvasGroup.alpha = 1f;
			this.m_talentTreePanelCanvasGroup.interactable = true;
			this.m_talentTreePanelCanvasGroup.blocksRaycasts = true;
		}
	}

	private void OnEnable()
	{
		this.ShowAdventureMapPanel();
		this.m_navBarArea.SetActive(true);
	}

	public void SelectDefaultNavButton()
	{
		this.m_defaultNavButton.SelectMe();
	}

	public Text m_titleText;

	public Text m_troopButtonText;

	public Text m_followerButtonText;

	public Text m_allyButtonText;

	public Text m_talentButtonText;

	public Text m_worldMapButtonText;

	public OrderHallNavButton m_defaultNavButton;

	public OrderHallNavButton m_talentNavButton;

	public AutoCenterScrollRect m_autoCenterScrollRect;

	public float m_navButtonInitialEntranceDelay;

	public float m_navButtonEntranceDelay;

	public Canvas m_missionListPanelCanvas;

	public CanvasGroup m_missionListPanelCanvasGroup;

	public MiniMissionListPanel m_miniMissionListPanel;

	public RaycastTargetHack m_missionListPanelRTH;

	public Canvas m_troopsPanelCanvas;

	public CanvasGroup m_troopsPanelCanvasGroup;

	public TroopsPanel m_troopsPanel;

	public RaycastTargetHack m_troopsPanelRTH;

	public Canvas m_adventureMapPanelCanvas;

	public CanvasGroup m_adventureMapPanelCanvasGroup;

	public CanvasGroup m_playerInfoDisplayCanvasGroup;

	public AdventureMapPanel m_adventureMapPanel;

	public RaycastTargetHack m_adventureMapPanelRTH;

	public Canvas m_followersPanelCanvas;

	public CanvasGroup m_followersPanelCanvasGroup;

	public OrderHallFollowersPanel m_followersPanel;

	public RaycastTargetHack m_followersPanelRTH;

	public Canvas m_talentTreePanelCanvas;

	public CanvasGroup m_talentTreePanelCanvasGroup;

	public TalentTreePanel m_talentTreePanel;

	public RaycastTargetHack m_talentTreePanelRTH;

	public GameObject m_navBarArea;

	private bool m_actuallyDisablePanels;
}
