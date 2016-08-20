using System;
using UnityEngine;

public class ZoneButtonMissionArea : MonoBehaviour
{
	private void OnEnable()
	{
		if (ZoneButtonMissionArea.m_pinchZoomManager == null)
		{
			ZoneButtonMissionArea.m_pinchZoomManager = base.gameObject.GetComponentInParent<PinchZoomContentManager>();
		}
		PinchZoomContentManager pinchZoomManager = ZoneButtonMissionArea.m_pinchZoomManager;
		pinchZoomManager.ZoomFactorChanged = (Action)Delegate.Combine(pinchZoomManager.ZoomFactorChanged, new Action(this.OnZoomChanged));
	}

	private void OnDisable()
	{
		PinchZoomContentManager pinchZoomManager = ZoneButtonMissionArea.m_pinchZoomManager;
		pinchZoomManager.ZoomFactorChanged = (Action)Delegate.Remove(pinchZoomManager.ZoomFactorChanged, new Action(this.OnZoomChanged));
	}

	private void OnZoomChanged()
	{
		MapInfo componentInParent = base.gameObject.GetComponentInParent<MapInfo>();
		CanvasGroup component = base.gameObject.GetComponent<CanvasGroup>();
		component.alpha = (ZoneButtonMissionArea.m_pinchZoomManager.m_zoomFactor - 1f) / (componentInParent.m_maxZoomFactor - 1f);
		component.blocksRaycasts = (component.alpha > 0.99f);
	}

	private static PinchZoomContentManager m_pinchZoomManager;
}
