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
		pinchZoomManager.ZoomFactorChanged = (Action<bool>)Delegate.Combine(pinchZoomManager.ZoomFactorChanged, new Action<bool>(this.OnZoomChanged));
	}

	private void OnDisable()
	{
		PinchZoomContentManager pinchZoomManager = ZoneButtonMissionArea.m_pinchZoomManager;
		pinchZoomManager.ZoomFactorChanged = (Action<bool>)Delegate.Remove(pinchZoomManager.ZoomFactorChanged, new Action<bool>(this.OnZoomChanged));
	}

	private void OnZoomChanged(bool force)
	{
		MapInfo componentInParent = base.gameObject.GetComponentInParent<MapInfo>();
		CanvasGroup component = base.gameObject.GetComponent<CanvasGroup>();
		component.alpha = (ZoneButtonMissionArea.m_pinchZoomManager.m_zoomFactor - 1f) / (componentInParent.m_maxZoomFactor - 1f);
		bool flag = component.alpha > 0.99f;
		if (flag != component.blocksRaycasts || force)
		{
			CanvasGroup[] componentsInChildren = base.gameObject.GetComponentsInChildren<CanvasGroup>(true);
			foreach (CanvasGroup canvasGroup in componentsInChildren)
			{
				canvasGroup.blocksRaycasts = flag;
			}
		}
	}

	private static PinchZoomContentManager m_pinchZoomManager;
}
