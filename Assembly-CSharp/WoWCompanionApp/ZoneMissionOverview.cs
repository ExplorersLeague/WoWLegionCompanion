using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class ZoneMissionOverview : MonoBehaviour
	{
		private void OnEnable()
		{
			if (ZoneMissionOverview.m_pinchZoomManager == null)
			{
				ZoneMissionOverview.m_pinchZoomManager = base.gameObject.GetComponentInParent<PinchZoomContentManager>();
			}
			PinchZoomContentManager pinchZoomManager = ZoneMissionOverview.m_pinchZoomManager;
			pinchZoomManager.ZoomFactorChanged = (Action<bool>)Delegate.Combine(pinchZoomManager.ZoomFactorChanged, new Action<bool>(this.OnZoomChanged));
			Singleton<GarrisonWrapper>.Instance.InvasionPOIChangedAction += this.HandleInvasionPOIChanged;
		}

		private void OnDisable()
		{
			PinchZoomContentManager pinchZoomManager = ZoneMissionOverview.m_pinchZoomManager;
			pinchZoomManager.ZoomFactorChanged = (Action<bool>)Delegate.Remove(pinchZoomManager.ZoomFactorChanged, new Action<bool>(this.OnZoomChanged));
			Singleton<GarrisonWrapper>.Instance.InvasionPOIChangedAction -= this.HandleInvasionPOIChanged;
		}

		private void Start()
		{
			if (this.zoneNameTag.Length > 0)
			{
				this.m_zoneNameArea.SetActive(this.zoneNameTag.Length > 0);
				this.zoneNameText.text = StaticDB.GetString(this.zoneNameTag, null);
				this.HandleInvasionPOIChanged();
			}
			else
			{
				this.m_invasionZoneNameArea.SetActive(false);
				this.m_zoneNameArea.SetActive(false);
				this.m_statsArea.SetActive(false);
			}
		}

		private void Update()
		{
			if (this.m_invasionZoneNameArea.activeSelf)
			{
				TimeSpan timeSpan = LegionfallData.GetCurrentInvasionExpirationTime() - GarrisonStatus.CurrentTime();
				if (((timeSpan.TotalSeconds <= 0.0) ? TimeSpan.Zero : timeSpan).TotalSeconds <= 0.0)
				{
					this.m_invasionZoneNameArea.SetActive(false);
				}
			}
		}

		private void OnZoomChanged(bool force)
		{
			CanvasGroup component = base.gameObject.GetComponent<CanvasGroup>();
			MapInfo componentInParent = base.gameObject.GetComponentInParent<MapInfo>();
			component.alpha = (componentInParent.m_maxZoomFactor - ZoneMissionOverview.m_pinchZoomManager.m_zoomFactor) / (componentInParent.m_maxZoomFactor - 1f);
			if (component.alpha < 0.99f)
			{
				component.interactable = false;
			}
			else
			{
				component.interactable = true;
			}
		}

		private void HandleInvasionPOIChanged()
		{
			if (LegionfallData.HasCurrentInvasionPOI() && LegionfallData.GetCurrentInvasionPOI().AreaPoiID == this.m_invasionPOIID)
			{
				this.m_invasionZoneNameArea.SetActive(true);
			}
			else
			{
				this.m_invasionZoneNameArea.SetActive(false);
			}
		}

		public ZoneMissionStat statDisplay_AvailableMissions;

		public ZoneMissionStat statDisplay_InProgressMissions;

		public ZoneMissionStat statDisplay_CompleteMissions;

		public ZoneMissionStat statDisplay_WorldQuests;

		public GameObject m_zoneNameArea;

		public GameObject m_statsArea;

		public GameObject m_bountyButtonRoot;

		public GameObject m_anonymousBountyButtonRoot;

		public int[] m_areaID;

		public string zoneNameTag;

		public Text zoneNameText;

		public int m_invasionPOIID;

		public GameObject m_invasionZoneNameArea;

		private static PinchZoomContentManager m_pinchZoomManager;
	}
}
