using System;
using UnityEngine;

public class StackableMapIcon : MonoBehaviour
{
	public Rect GetWorldRect()
	{
		Vector3[] array = new Vector3[4];
		this.m_iconBoundsRT.GetWorldCorners(array);
		float num = array[2].x - array[0].x;
		float num2 = array[2].y - array[0].y;
		float zoomFactor = AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor;
		num *= zoomFactor;
		num2 *= zoomFactor;
		Rect result;
		result..ctor(array[0].x, array[0].y, num, num2);
		return result;
	}

	public void SetContainer(StackableMapIconContainer iconContainer)
	{
		this.m_iconContainer = iconContainer;
	}

	public StackableMapIconContainer GetContainer()
	{
		return this.m_iconContainer;
	}

	public void RegisterWithManager()
	{
		StackableMapIconManager.RegisterStackableMapIcon(this);
	}

	private void OnDestroy()
	{
		if (this.m_iconContainer != null)
		{
			this.m_iconContainer.RemoveStackableMapIcon(this);
		}
	}

	public RectTransform m_iconBoundsRT;

	private StackableMapIconContainer m_iconContainer;
}
