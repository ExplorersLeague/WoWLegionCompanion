using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class ZoneButton : MonoBehaviour
	{
		private void Start()
		{
			this.m_zoneName = StaticDB.GetString(this.m_zoneNameTag, null);
		}

		private void Update()
		{
		}

		public string GetZoneName()
		{
			return this.m_zoneName;
		}

		public void OnTap()
		{
			AdventureMapPanel.instance.SetSelectedIconContainer(null);
			if (AdventureMapPanel.instance.m_testEnableTapToZoomOut && Mathf.Approximately(AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor, AdventureMapPanel.instance.m_activeMapInfo.m_maxZoomFactor))
			{
				if (AdventureMapPanel.instance.GetCurrentMapMission() > 0 || AdventureMapPanel.instance.GetCurrentWorldQuest() > 0)
				{
					AdventureMapPanel.instance.SelectMissionFromMap(0);
					AdventureMapPanel.instance.SelectWorldQuest(0);
				}
			}
			else
			{
				AdventureMapPanel.instance.CenterAndZoom(base.transform.position, this, true);
			}
		}

		public string m_zoneNameTag;

		public AdventureMapPanel.eZone m_zoneID;

		private string m_zoneName;
	}
}
