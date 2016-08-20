using System;
using UnityEngine;

public class MissionLocationScroller : MonoBehaviour
{
	private void Awake()
	{
		this.m_myRT = base.GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector2 anchoredPosition = this.m_myRT.anchoredPosition;
		anchoredPosition.x += this.scrollSpeed * Time.deltaTime;
		this.m_myRT.anchoredPosition = anchoredPosition;
		if (this.m_myRT.anchoredPosition.x <= -this.imageWidth * 0.5f * this.m_myRT.localScale.x)
		{
			anchoredPosition = this.m_myRT.anchoredPosition;
			anchoredPosition.x = 0f;
			this.m_myRT.anchoredPosition = anchoredPosition;
		}
	}

	public float scrollSpeed;

	public float imageWidth;

	private RectTransform m_myRT;
}
