using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class LegionfallButton : MonoBehaviour
	{
		private void OnEnable()
		{
			this.SetVisibility(LegionfallData.HasAccess());
			PinchZoomContentManager pinchZoomManager = this.m_pinchZoomManager;
			pinchZoomManager.ZoomFactorChanged = (Action<bool>)Delegate.Combine(pinchZoomManager.ZoomFactorChanged, new Action<bool>(this.OnZoomChanged));
			Singleton<GarrisonWrapper>.Instance.ContributionInfoChangedAction += this.HandleContributionInfoChanged;
		}

		private void OnDisable()
		{
			PinchZoomContentManager pinchZoomManager = this.m_pinchZoomManager;
			pinchZoomManager.ZoomFactorChanged = (Action<bool>)Delegate.Remove(pinchZoomManager.ZoomFactorChanged, new Action<bool>(this.OnZoomChanged));
			Singleton<GarrisonWrapper>.Instance.ContributionInfoChangedAction -= this.HandleContributionInfoChanged;
		}

		private void OnZoomChanged(bool force)
		{
			if (!this.m_isVisible)
			{
				return;
			}
			CanvasGroup component = base.gameObject.GetComponent<CanvasGroup>();
			MapInfo componentInParent = base.gameObject.GetComponentInParent<MapInfo>();
			component.alpha = (componentInParent.m_maxZoomFactor - this.m_pinchZoomManager.m_zoomFactor) / (componentInParent.m_maxZoomFactor - 1f);
			if (component.alpha < 0.99f)
			{
				component.interactable = false;
				component.blocksRaycasts = false;
			}
			else
			{
				component.interactable = true;
				component.blocksRaycasts = true;
			}
		}

		private void HandleContributionInfoChanged()
		{
			this.SetVisibility(LegionfallData.HasAccess());
		}

		private void SetVisibility(bool isVisible)
		{
			this.m_isVisible = isVisible;
			CanvasGroup component = base.gameObject.GetComponent<CanvasGroup>();
			component.alpha = ((!this.m_isVisible) ? 0f : 1f);
			component.interactable = this.m_isVisible;
			component.blocksRaycasts = this.m_isVisible;
		}

		public PinchZoomContentManager m_pinchZoomManager;

		private bool m_isVisible;
	}
}
