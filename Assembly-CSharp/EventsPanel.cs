using System;
using UnityEngine;

public class EventsPanel : MonoBehaviour
{
	private void Start()
	{
		base.gameObject.transform.localScale = Vector3.one;
		RectTransform component = base.gameObject.GetComponent<RectTransform>();
		Vector2 zero = Vector2.zero;
		base.gameObject.GetComponent<RectTransform>().offsetMax = zero;
		component.offsetMin = zero;
	}

	private GameObject AddObjectToContent(GameObject prefab)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(prefab);
		gameObject.transform.SetParent(this.m_scrollContent.transform);
		gameObject.transform.SetAsLastSibling();
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		return gameObject;
	}

	private void ClearContent()
	{
		for (int i = this.m_scrollContent.transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(this.m_scrollContent.transform.GetChild(i).gameObject);
		}
		this.m_scrollContent.transform.DetachChildren();
	}

	private void RefreshEvents()
	{
		this.ClearContent();
	}

	private void OnEnable()
	{
	}

	public GameObject m_scrollContent;

	public GameObject m_eventObjectPrefab;

	public GameObject m_dateObjectPrefab;
}
