using System;
using UnityEngine;
using WowJamMessages;
using WowStatConstants;
using WowStaticData;

public class CombatAllyMissionPanel : MonoBehaviour
{
	public void Show()
	{
		int num = 0;
		CombatAllyMissionState combatAllyMissionState = CombatAllyMissionState.notAvailable;
		foreach (object obj in PersistentMissionData.missionDictionary.Values)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)obj;
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(jamGarrisonMobileMission.MissionRecID);
			if (record != null)
			{
				if ((record.Flags & 16u) != 0u)
				{
					num = jamGarrisonMobileMission.MissionRecID;
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
		if (num > 0)
		{
			this.m_missionDetailView.HandleMissionSelected(num);
		}
		this.m_missionDetailView.SetCombatAllyMissionState(combatAllyMissionState);
		this.m_sliderPanel.MaximizeSliderPanel();
	}

	public void Hide()
	{
		this.m_sliderPanel.HideSliderPanel();
	}

	private void Update()
	{
	}

	public MissionDetailView m_missionDetailView;

	public SliderPanel m_sliderPanel;
}
