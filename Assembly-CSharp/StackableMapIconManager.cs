using System;
using System.Collections.Generic;
using UnityEngine;

public class StackableMapIconManager : MonoBehaviour
{
	private void Awake()
	{
		StackableMapIconManager.s_instance = this;
		this.m_containers = new List<StackableMapIconContainer>();
	}

	public static void RegisterStackableMapIcon(StackableMapIcon icon)
	{
		if (StackableMapIconManager.s_instance == null)
		{
			Debug.LogError("ERROR: RegisterStackableMapIcon with null s_instance");
			return;
		}
		if (icon == null)
		{
			Debug.LogError("ERROR: RegisterStackableMapIcon with null icon");
			return;
		}
		bool flag = false;
		foreach (StackableMapIconContainer stackableMapIconContainer in StackableMapIconManager.s_instance.m_containers)
		{
			if (!(stackableMapIconContainer == null))
			{
				if (stackableMapIconContainer.gameObject.activeSelf)
				{
					if (stackableMapIconContainer.m_startLocationMapID == icon.m_startLocationMapID)
					{
						Rect worldRect = stackableMapIconContainer.GetWorldRect();
						if (icon.GetWorldRect().Overlaps(worldRect))
						{
							stackableMapIconContainer.AddStackableMapIcon(icon);
							icon.SetContainer(stackableMapIconContainer);
							StackableMapIconManager.s_instance.m_containers.Add(stackableMapIconContainer);
							flag = true;
							break;
						}
					}
				}
			}
		}
		if (!flag)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(StackableMapIconManager.s_instance.m_stackableMapContainerPrefab);
			gameObject.transform.SetParent(icon.transform.parent, false);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			RectTransform component2 = icon.GetComponent<RectTransform>();
			component.anchorMin = component2.anchorMin;
			component.anchorMax = component2.anchorMax;
			component.sizeDelta = icon.m_iconBoundsRT.sizeDelta;
			component.anchoredPosition = Vector2.zero;
			StackableMapIconContainer component3 = gameObject.GetComponent<StackableMapIconContainer>();
			if (component3 != null)
			{
				component3.m_startLocationMapID = icon.m_startLocationMapID;
				component3.AddStackableMapIcon(icon);
				icon.SetContainer(component3);
				StackableMapIconManager.s_instance.m_containers.Add(component3);
			}
			else
			{
				Debug.LogError("ERROR: containerObj has no StackableMapIconContainer!!");
			}
		}
	}

	public static void RemoveStackableMapIconContainer(StackableMapIconContainer container)
	{
		if (StackableMapIconManager.s_instance != null && StackableMapIconManager.s_instance.m_containers.Contains(container))
		{
			StackableMapIconManager.s_instance.m_containers.Remove(container);
		}
	}

	public GameObject m_stackableMapContainerPrefab;

	private List<StackableMapIconContainer> m_containers;

	private static StackableMapIconManager s_instance;
}
