using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionInfoDialog : MonoBehaviour
	{
		public void InitializeDialog(int missionId)
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(missionId);
			if (record != null)
			{
				this.m_missionName.text = record.Name;
				this.m_missionDescription.text = record.Description;
			}
		}

		public Text m_missionName;

		public Text m_missionDescription;
	}
}
