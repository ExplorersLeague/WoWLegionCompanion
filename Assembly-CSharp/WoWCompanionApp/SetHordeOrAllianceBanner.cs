using System;
using UnityEngine;
using WowStatConstants;

namespace WoWCompanionApp
{
	public class SetHordeOrAllianceBanner : MonoBehaviour
	{
		private void Start()
		{
			bool flag = GarrisonStatus.Faction() == PVP_FACTION.HORDE;
			this.m_hordeBG.gameObject.SetActive(flag);
			this.m_allianceBG.gameObject.SetActive(!flag);
		}

		public GameObject m_allianceBG;

		public GameObject m_hordeBG;
	}
}
