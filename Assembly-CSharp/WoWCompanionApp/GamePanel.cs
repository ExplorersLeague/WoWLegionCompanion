using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class GamePanel : MonoBehaviour
	{
		private void Start()
		{
			if (this.m_missionListPanel != null && this.m_missionListPanel.gameObject.activeSelf)
			{
				this.m_currentPanel = this.m_missionListPanel.gameObject;
			}
			else if (this.m_mapPanel != null && this.m_mapPanel.gameObject.activeSelf)
			{
				this.m_currentPanel = this.m_mapPanel.gameObject;
			}
			else if (this.m_followersPanel != null && this.m_followersPanel.gameObject.activeSelf)
			{
				this.m_currentPanel = this.m_followersPanel.gameObject;
			}
			else if (this.m_troopsPanel != null && this.m_troopsPanel.gameObject.activeSelf)
			{
				this.m_currentPanel = this.m_troopsPanel.gameObject;
			}
			else if (this.m_talentTreePanel != null && this.m_talentTreePanel.gameObject.activeSelf)
			{
				this.m_currentPanel = this.m_troopsPanel.gameObject;
			}
			if (Main.instance.IsIphoneX() && this.m_navBarLayout != null)
			{
				HorizontalLayoutGroup component = this.m_navBarLayout.GetComponent<HorizontalLayoutGroup>();
				if (component != null)
				{
					component.padding.bottom += 15;
				}
			}
			if (this.m_birdEagleThings != null)
			{
				this.m_birdEagleThings.SetActive(!Main.instance.IsNarrowScreen());
			}
			if (this.m_mapPanel != null)
			{
				AdventureMapPanel component2 = this.m_mapPanel.GetComponent<AdventureMapPanel>();
				if (component2 != null)
				{
					component2.SetStartingMapByFaction();
				}
			}
		}

		private void OnEnable()
		{
			this.m_mapPanel.CenterAndZoomOut();
		}

		public void ShowPanel(GameObject panel)
		{
			if (this.m_currentPanel == panel)
			{
				return;
			}
			if (this.m_currentPanel != null)
			{
				this.m_currentPanel.SetActive(false);
			}
			this.m_currentPanel = panel;
			this.m_currentPanel.SetActive(true);
		}

		public void SelectOrderHallNavButton(OrderHallNavButton navButton)
		{
			if (this.OrderHallNavButtonSelectedAction != null)
			{
				this.OrderHallNavButtonSelectedAction(navButton);
			}
		}

		public MiniMissionListPanel m_missionListPanel;

		public AdventureMapPanel m_mapPanel;

		public OrderHallFollowersPanel m_followersPanel;

		public TroopsPanel m_troopsPanel;

		public TalentTreePanel m_talentTreePanel;

		private GameObject m_currentPanel;

		public Action<OrderHallNavButton> OrderHallNavButtonSelectedAction;

		public GameObject m_navBarLayout;

		public GameObject m_birdEagleThings;
	}
}
