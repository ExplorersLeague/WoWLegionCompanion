using System;
using UnityEngine;

public class ZoneMapOverlay : MonoBehaviour
{
	private void Awake()
	{
		this.m_mainMapInfo = base.GetComponentInParent<MapInfo>();
		this.m_canvasGroup = base.GetComponent<CanvasGroup>();
	}

	private void Update()
	{
		this.SetTargetAlphaForZoomFactor(AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor);
		this.UpdateFade();
	}

	private void SetTargetAlphaForZoomFactor(float zoomFactor)
	{
		if (!AdventureMapPanel.instance.m_testEnableDetailedZoneMaps)
		{
			this.m_targetAlpha = 0f;
			return;
		}
		float num = (zoomFactor - this.m_mainMapInfo.m_minZoomFactor) / (this.m_mainMapInfo.m_maxZoomFactor - this.m_mainMapInfo.m_minZoomFactor);
		if (num < this.m_minZoomFade)
		{
			this.m_targetAlpha = 0f;
		}
		else if (num > this.m_maxZoomFade)
		{
			this.m_targetAlpha = 1f;
		}
		else
		{
			this.m_targetAlpha = (num - this.m_minZoomFade) / (this.m_maxZoomFade - this.m_minZoomFade);
		}
	}

	private void UpdateFade()
	{
		if (this.m_canvasGroup.alpha == this.m_targetAlpha)
		{
			return;
		}
		float num = this.m_canvasGroup.alpha;
		if (num < this.m_targetAlpha)
		{
			num += this.m_fadeInSpeed * Time.deltaTime;
		}
		else
		{
			num -= this.m_fadeOutSpeed * Time.deltaTime;
		}
		this.m_canvasGroup.alpha = Mathf.Clamp(num, 0f, 1f);
	}

	public float m_minZoomFade;

	public float m_maxZoomFade;

	private MapInfo m_mainMapInfo;

	private CanvasGroup m_canvasGroup;

	private float m_targetAlpha;

	public float m_fadeInSpeed;

	public float m_fadeOutSpeed;
}
