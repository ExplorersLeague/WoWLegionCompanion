using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

public class MissionDescriptionTooltip : MonoBehaviour
{
	public void OnEnable()
	{
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
	}

	public void SetMission(int garrMissionID)
	{
		GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
		if (record == null)
		{
			this.m_missionName.text = string.Empty;
			this.m_missionDescription.text = string.Empty;
			return;
		}
		this.m_missionName.text = record.Name;
		this.m_missionDescription.text = record.Description;
	}

	public Image m_missionIcon;

	public Text m_missionName;

	public Text m_missionDescription;
}
