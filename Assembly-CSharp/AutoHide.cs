using System;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	[Header("If not specefied, will use parent rect")]
	public RectTransform m_clipRT;

	private RectTransform m_myRT;

	private CanvasGroup m_myCanvasGroup;

	private Rect m_clipRect;

	private Vector3[] m_clipRTWorldCorners;

	private Vector3[] m_myRTCorners;

	private Rect m_myRect;
}
