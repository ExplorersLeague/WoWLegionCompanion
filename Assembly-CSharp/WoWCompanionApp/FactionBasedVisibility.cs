using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class FactionBasedVisibility : MonoBehaviour
	{
		private void Start()
		{
			this.m_hordeBG.gameObject.SetActive(GarrisonStatus.Faction() == PVP_FACTION.HORDE);
			this.m_allianceBG.gameObject.SetActive(GarrisonStatus.Faction() == PVP_FACTION.ALLIANCE);
		}

		public GameObject GetActiveObject()
		{
			return (!this.m_allianceBG.activeInHierarchy) ? this.m_hordeBG : this.m_allianceBG;
		}

		public GameObject m_allianceBG;

		public GameObject m_hordeBG;
	}
}
