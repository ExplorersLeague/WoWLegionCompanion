using System;
using System.Collections;
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
		IEnumerator enumerator = PersistentMissionData.missionDictionary.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
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
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
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
