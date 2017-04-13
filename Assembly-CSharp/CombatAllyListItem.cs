using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class CombatAllyListItem : MonoBehaviour
{
	private void Awake()
	{
		this.m_combatAllyLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_assignChampionText.font = GeneralHelpers.LoadStandardFont();
		this.m_championName.font = GeneralHelpers.LoadStandardFont();
		this.m_combatAllyAbilityText.font = GeneralHelpers.LoadStandardFont();
		this.m_combatAbilityName.font = GeneralHelpers.LoadStandardFont();
		this.m_unassignCombatAllyButtonLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_combatAllyLabel.text = StaticDB.GetString("COMBAT_ALLY", null);
		this.m_assignChampionText.text = StaticDB.GetString("ORDER_HALL_ZONE_SUPPORT_DESCRIPTION_2", null);
		this.m_combatAllyAbilityText.text = StaticDB.GetString("COMBAT_ALLY_ABILITY", null);
		this.m_unassignCombatAllyButtonLabel.text = StaticDB.GetString("UNASSIGN", null);
		this.UpdateVisuals();
	}

	private void OnEnable()
	{
		this.ClearCombatAllyDisplay();
		this.UpdateVisuals();
		Main instance = Main.instance;
		instance.GarrisonDataResetFinishedAction = (Action)Delegate.Combine(instance.GarrisonDataResetFinishedAction, new Action(this.HandleDataResetFinished));
	}

	private void OnDisable()
	{
		Main instance = Main.instance;
		instance.GarrisonDataResetFinishedAction = (Action)Delegate.Remove(instance.GarrisonDataResetFinishedAction, new Action(this.HandleDataResetFinished));
	}

	public void HandleDataResetFinished()
	{
		this.UpdateVisuals();
	}

	public void HandleCompleteMissionResult(int garrMissionID, int result, int missionSuccessChance)
	{
		this.UpdateVisuals();
	}

	private void ClearCombatAllyDisplay()
	{
		this.m_combatAllySlot.SetFollower(0);
		this.m_combatAllyLabel.gameObject.SetActive(true);
		this.m_assignChampionText.gameObject.SetActive(true);
		this.m_championName.gameObject.SetActive(false);
		this.m_combatAllyAbilityText.gameObject.SetActive(false);
		this.m_combatAbilityName.gameObject.SetActive(false);
		this.m_combatAllySupportSpellDisplay.gameObject.SetActive(false);
		this.m_unassignCombatAllyButton.SetActive(false);
	}

	public void UpdateVisuals()
	{
		CombatAllyMissionState combatAllyMissionState = CombatAllyMissionState.notAvailable;
		foreach (object obj in PersistentMissionData.missionDictionary.Values)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)obj;
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(jamGarrisonMobileMission.MissionRecID);
			if (record != null)
			{
				if ((record.Flags & 16u) != 0u)
				{
					this.m_combatAllyMissionID = jamGarrisonMobileMission.MissionRecID;
					if (jamGarrisonMobileMission.MissionState == 1)
					{
						combatAllyMissionState = CombatAllyMissionState.inProgress;
					}
					else
					{
						combatAllyMissionState = CombatAllyMissionState.available;
					}
					break;
				}
			}
		}
		if (combatAllyMissionState == CombatAllyMissionState.inProgress)
		{
			foreach (JamGarrisonFollower jamGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
			{
				if (jamGarrisonFollower.CurrentMissionID == this.m_combatAllyMissionID)
				{
					this.m_combatAllySlot.SetFollower(jamGarrisonFollower.GarrFollowerID);
					this.m_combatAllyLabel.gameObject.SetActive(false);
					this.m_assignChampionText.gameObject.SetActive(false);
					this.m_championName.gameObject.SetActive(true);
					GarrFollowerRec record2 = StaticDB.garrFollowerDB.GetRecord(jamGarrisonFollower.GarrFollowerID);
					CreatureRec record3 = StaticDB.creatureDB.GetRecord((GarrisonStatus.Faction() != PVP_FACTION.ALLIANCE) ? record2.HordeCreatureID : record2.AllianceCreatureID);
					if (jamGarrisonFollower.Quality == 6 && record2.TitleName != null && record2.TitleName.Length > 0)
					{
						this.m_championName.text = record2.TitleName;
					}
					else if (record2 != null)
					{
						this.m_championName.text = record3.Name;
					}
					this.m_championName.color = GeneralHelpers.GetQualityColor(jamGarrisonFollower.Quality);
					this.m_combatAllySupportSpellDisplay.gameObject.SetActive(true);
					this.m_combatAllySupportSpellDisplay.SetSpell(jamGarrisonFollower.ZoneSupportSpellID);
					this.m_unassignCombatAllyButton.SetActive(true);
					break;
				}
			}
		}
		else
		{
			this.ClearCombatAllyDisplay();
		}
	}

	public void UnassignCombatAlly()
	{
		Main.instance.CompleteMission(this.m_combatAllyMissionID);
	}

	public MissionFollowerSlot m_combatAllySlot;

	public Text m_combatAllyLabel;

	public Text m_assignChampionText;

	public Text m_championName;

	public Text m_combatAllyAbilityText;

	public Text m_combatAbilityName;

	public SpellDisplay m_combatAllySupportSpellDisplay;

	public GameObject m_unassignCombatAllyButton;

	public Text m_unassignCombatAllyButtonLabel;

	private int m_combatAllyMissionID;
}
