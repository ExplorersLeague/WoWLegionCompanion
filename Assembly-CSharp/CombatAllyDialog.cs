using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class CombatAllyDialog : MonoBehaviour
{
	public void Start()
	{
		this.m_combatAllyCost.font = GeneralHelpers.LoadStandardFont();
		this.m_titleText.text = StaticDB.GetString("COMBAT_ALLY", null);
	}

	public void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
		if (this.m_missionPanelSlider.m_sliderPanel.IsShowing() || this.m_missionPanelSlider.m_sliderPanel.IsBusyMoving())
		{
			base.gameObject.SetActive(false);
			return;
		}
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	private void CreateCombatAllyItems(int combatAllyMissionID, int combatAllyMissionCost)
	{
		foreach (JamGarrisonFollower jamGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
		{
			FollowerStatus followerStatus = GeneralHelpers.GetFollowerStatus(jamGarrisonFollower);
			if (jamGarrisonFollower.ZoneSupportSpellID > 0 && (followerStatus == FollowerStatus.available || followerStatus == FollowerStatus.onMission))
			{
				FollowerInventoryListItem followerInventoryListItem = Object.Instantiate<FollowerInventoryListItem>(this.m_combatAllyChampionListItemPrefab);
				followerInventoryListItem.transform.SetParent(this.m_combatAllyListContent.transform, false);
				followerInventoryListItem.SetCombatAllyChampion(jamGarrisonFollower, combatAllyMissionID, combatAllyMissionCost);
			}
		}
	}

	public void Init()
	{
		FollowerInventoryListItem[] componentsInChildren = this.m_combatAllyListContent.GetComponentsInChildren<FollowerInventoryListItem>(true);
		foreach (FollowerInventoryListItem followerInventoryListItem in componentsInChildren)
		{
			Object.DestroyImmediate(followerInventoryListItem.gameObject);
		}
		int num = 0;
		foreach (object obj in PersistentMissionData.missionDictionary.Values)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)obj;
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(jamGarrisonMobileMission.MissionRecID);
			if (record != null)
			{
				if ((record.Flags & 16u) != 0u)
				{
					this.CreateCombatAllyItems(jamGarrisonMobileMission.MissionRecID, (int)record.MissionCost);
					num = (int)record.MissionCost;
					break;
				}
			}
		}
		if (num <= GarrisonStatus.Resources())
		{
			this.m_combatAllyCost.text = string.Concat(new object[]
			{
				StaticDB.GetString("COST2", "Cost:"),
				" <color=#ffffffff>",
				num,
				"</color>"
			});
		}
		else
		{
			this.m_combatAllyCost.text = string.Concat(new object[]
			{
				StaticDB.GetString("COST2", "Cost:"),
				" <color=#ff0000ff>",
				num,
				"</color>"
			});
		}
		Sprite sprite = GeneralHelpers.LoadCurrencyIcon(1220);
		if (sprite != null)
		{
			this.m_combatAllyCostResourceIcon.sprite = sprite;
		}
	}

	public FollowerInventoryListItem m_combatAllyChampionListItemPrefab;

	public GameObject m_combatAllyListContent;

	public Text m_combatAllyCost;

	public Image m_combatAllyCostResourceIcon;

	public Text m_titleText;

	public MissionPanelSlider m_missionPanelSlider;
}
