using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class MissionTypeDialog : MonoBehaviour
	{
		public void InitializeMissionDialog(int missionId, Sprite missionTypeSprite)
		{
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(missionId);
			if (record != null)
			{
				GarrMechanicRec record2 = StaticDB.garrMechanicDB.GetRecord(record.EnvGarrMechanicID);
				if (record2 != null)
				{
					GarrMechanicTypeRec record3 = StaticDB.garrMechanicTypeDB.GetRecord((int)record2.GarrMechanicTypeID);
					if (record3 != null)
					{
						this.m_icon.sprite = missionTypeSprite;
						this.m_missionTypeName.text = record3.Name;
						this.m_missionTypeDescription.text = WowTextParser.parser.Parse(record3.Description, 0).Replace("FFFFD200", "FFD200FF");
					}
				}
			}
		}

		public Image m_icon;

		public Text m_missionTypeName;

		public Text m_missionTypeDescription;

		private const string BROKEN_COLOR_STRING = "FFFFD200";

		private const string FIXED_COLOR_STRING = "FFD200FF";
	}
}
