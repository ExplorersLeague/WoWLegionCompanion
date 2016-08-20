using System;
using UnityEngine;

public class ZoneMap : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		AdventureMapPanel.instance.m_pinchZoomContentManager.SetZoom(1f, base.transform.position, false);
		AdventureMapPanel.instance.m_mapViewContentsRT.localPosition = Vector3.zero;
	}

	public void SetAdventureMapZoom(float zoomFactor)
	{
		this.m_missionIconArea.transform.localScale = new Vector3(zoomFactor, zoomFactor, zoomFactor);
	}

	private void OverZoomOut()
	{
		AdventureMapPanel.instance.ShowWorldMap(true);
		base.gameObject.SetActive(false);
	}

	public string m_zoneName;

	public GameObject m_missionIconArea;
}
