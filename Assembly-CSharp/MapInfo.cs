using System;
using UnityEngine;
using UnityEngine.UI;

public class MapInfo : MonoBehaviour
{
	private void Awake()
	{
		this.Init();
	}

	public void SetMaxZoom(float val)
	{
		this.m_maxZoomFactor = val;
	}

	public Vector2 GetFillViewSize()
	{
		if (!this.m_initialized)
		{
			this.Init();
			AdventureMapPanel.instance.m_pinchZoomContentManager.SetZoom(1f, false);
		}
		return this.m_fillViewSize;
	}

	public void CalculateFillScale()
	{
		float mapW = this.m_mapW;
		float mapH = this.m_mapH;
		float num = mapW / mapH;
		float num2 = AdventureMapPanel.instance.m_mapViewRT.rect.width / AdventureMapPanel.instance.m_mapViewRT.rect.height;
		this.m_viewRelativeScale = 1f;
		if (num < num2)
		{
			this.m_viewRelativeScale = AdventureMapPanel.instance.m_mapViewRT.rect.width / mapW;
		}
		else
		{
			this.m_viewRelativeScale = AdventureMapPanel.instance.m_mapViewRT.rect.height / mapH;
		}
		this.m_fillViewSize.x = mapW * this.m_viewRelativeScale;
		this.m_fillViewSize.y = mapH * this.m_viewRelativeScale;
	}

	public float GetViewRelativeScale()
	{
		return this.m_viewRelativeScale;
	}

	private void Init()
	{
		if (!this.m_initialized)
		{
			this.CalculateFillScale();
			this.m_initialized = true;
		}
	}

	private void Update()
	{
		if (this.m_initialized)
		{
			this.CalculateFillScale();
			AdventureMapPanel.instance.m_pinchZoomContentManager.SetZoom(AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor, false);
		}
	}

	public float m_minZoomFactor;

	public float m_maxZoomFactor;

	public GameObject m_scaleRoot;

	public Image m_mapImage;

	public float m_mapW;

	public float m_mapH;

	private bool m_initialized;

	private Vector2 m_fillViewSize;

	private float m_viewRelativeScale;
}
