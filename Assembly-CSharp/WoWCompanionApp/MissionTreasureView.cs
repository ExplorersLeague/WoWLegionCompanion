using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class MissionTreasureView : MonoBehaviour
	{
		private void Start()
		{
			if (GarrisonStatus.Faction() == PVP_FACTION.HORDE)
			{
				this.m_hordeChest.SetActive(true);
				this.m_allianceChest.SetActive(false);
			}
			else if (GarrisonStatus.Faction() == PVP_FACTION.ALLIANCE)
			{
				this.m_hordeChest.SetActive(false);
				this.m_allianceChest.SetActive(true);
			}
		}

		public GameObject m_hordeChest;

		public GameObject m_allianceChest;
	}
}
