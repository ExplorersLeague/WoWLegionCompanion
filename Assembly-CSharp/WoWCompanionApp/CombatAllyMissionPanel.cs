using System;
using UnityEngine;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class CombatAllyMissionPanel : MonoBehaviour
	{
		public void Show()
		{
			int num = 0;
			CombatAllyMissionState combatAllyMissionState = CombatAllyMissionState.notAvailable;
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null)
				{
					if ((record.Flags & 16u) != 0u)
					{
						num = wrapperGarrisonMission.MissionRecID;
						if (wrapperGarrisonMission.MissionState == 1)
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
}
