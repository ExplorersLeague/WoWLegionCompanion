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

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public Text m_titleText;

	public Text m_troopButtonText;

	public Text m_followerButtonText;

	public Text m_allyButtonText;

	public Text m_talentButtonText;

	public Text m_worldMapButtonText;

	public OrderHallNavButton m_defaultNavButton;

	public AutoCenterScrollRect m_autoCenterScrollRect;

	public float m_navButtonInitialEntranceDelay;

	public float m_navButtonEntranceDelay;
}
