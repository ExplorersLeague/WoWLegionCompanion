using System;
using UnityEngine;
using UnityEngine.UI;

public class StackableMapIconContainer : MonoBehaviour
{
	private void OnEnable()
	{
		this.ShowExplodedList(false);
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.SelectedIconContainerChanged = (Action<StackableMapIconContainer>)Delegate.Combine(instance.SelectedIconContainerChanged, new Action<StackableMapIconContainer>(this.HandleSelectedIconContainerChanged));
	}

	private void OnDisable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.SelectedIconContainerChanged = (Action<StackableMapIconContainer>)Delegate.Remove(instance.SelectedIconContainerChanged, new Action<StackableMapIconContainer>(this.HandleSelectedIconContainerChanged));
	}

	private void OnDestroy()
	{
		StackableMapIconManager.RemoveStackableMapIconContainer(this);
	}

	public void ShowExplodedList(bool show)
	{
		this.m_explodedListIsVisible = show;
		this.m_multiRootCanvasGroup.alpha = ((!show) ? 0f : 1f);
		this.m_multiRootCanvasGroup.interactable = show;
		this.m_multiRootCanvasGroup.blocksRaycasts = show;
	}

	private bool ExplodedListIsVisible()
	{
		return this.m_explodedListIsVisible;
	}

	private void HandleSelectedIconContainerChanged(StackableMapIconContainer container)
	{
		if (container == this)
		{
			if (this.GetCount() > 1)
			{
				this.ShowExplodedList(true);
			}
		}
		else
		{
			this.ShowExplodedList(false);
		}
	}

	public Rect GetWorldRect()
	{
		Vector3[] array = new Vector3[4];
		RectTransform component = base.GetComponent<RectTransform>();
		component.GetWorldCorners(array);
		float num = array[2].x - array[0].x;
		float num2 = array[2].y - array[0].y;
		float zoomFactor = AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor;
		num *= zoomFactor;
		num2 *= zoomFactor;
		Rect result;
		result..ctor(array[0].x, array[0].y, num, num2);
		return result;
	}

	public void AddStackableMapIcon(StackableMapIcon icon)
	{
		StackableMapIcon componentInChildren = this.m_singleRoot.GetComponentInChildren<StackableMapIcon>(true);
		StackableMapIcon[] componentsInChildren = this.m_multiRoot.GetComponentsInChildren<StackableMapIcon>(true);
		bool flag = true;
		if (componentInChildren == null && componentsInChildren.Length == 0)
		{
			icon.transform.SetParent(this.m_singleRoot.transform, false);
			icon.transform.transform.localPosition = Vector3.zero;
		}
		else if (componentInChildren != null && componentsInChildren.Length == 0)
		{
			componentInChildren.transform.SetParent(this.m_multiRoot.transform, false);
			icon.transform.SetParent(this.m_multiRoot.transform, false);
			flag = false;
		}
		else
		{
			icon.transform.SetParent(this.m_multiRoot.transform, false);
			flag = false;
		}
		this.ShowExplodedList(false);
		if (flag)
		{
			this.m_countainerPreviewIconsGroup.SetActive(false);
		}
		else
		{
			this.m_countainerPreviewIconsGroup.SetActive(true);
		}
		componentsInChildren = this.m_multiRoot.GetComponentsInChildren<StackableMapIcon>(true);
		this.m_iconCount.text = string.Empty + componentsInChildren.Length;
		base.gameObject.name = "IconContainer (" + ((componentsInChildren.Length <= 0) ? "Single" : (string.Empty + componentsInChildren.Length)) + ")";
		base.gameObject.SetActive(true);
	}

	public void RemoveStackableMapIcon(StackableMapIcon icon)
	{
		StackableMapIcon componentInChildren = this.m_singleRoot.GetComponentInChildren<StackableMapIcon>(true);
		StackableMapIcon[] componentsInChildren = this.m_multiRoot.GetComponentsInChildren<StackableMapIcon>(true);
		bool flag = false;
		if (componentsInChildren.Length == 2)
		{
			componentsInChildren[0].transform.SetParent(this.m_singleRoot.transform, false);
			componentsInChildren[0].transform.localPosition = Vector3.zero;
			componentsInChildren[1].transform.SetParent(this.m_singleRoot.transform, false);
			componentsInChildren[1].transform.localPosition = Vector3.zero;
			flag = true;
		}
		else if (componentInChildren != null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.ShowExplodedList(false);
		if (flag)
		{
			this.m_countainerPreviewIconsGroup.SetActive(false);
		}
		else
		{
			this.m_countainerPreviewIconsGroup.SetActive(true);
		}
		int num = componentsInChildren.Length - 1;
		this.m_iconCount.text = string.Empty + num;
		base.gameObject.name = "IconContainer (" + ((num <= 0) ? "Single" : (string.Empty + num)) + ")";
	}

	public void PlayTapSound()
	{
		Main.instance.m_UISound.Play_SelectWorldQuest();
	}

	public void ToggleIconList()
	{
		this.PlayTapSound();
		this.ShowExplodedList(!this.m_multiRoot.activeSelf);
		if (this.m_multiRoot.activeSelf)
		{
			AdventureMapPanel.instance.SetSelectedIconContainer(this);
			UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 3f, 0f);
		}
		else
		{
			AdventureMapPanel.instance.SetSelectedIconContainer(null);
		}
	}

	public int GetCount()
	{
		StackableMapIcon componentInChildren = this.m_singleRoot.GetComponentInChildren<StackableMapIcon>(true);
		StackableMapIcon[] componentsInChildren = this.m_multiRoot.GetComponentsInChildren<StackableMapIcon>(true);
		int num = 0;
		if (componentInChildren != null)
		{
			num++;
		}
		return num + componentsInChildren.Length;
	}

	public GameObject m_countainerPreviewIconsGroup;

	public Text m_iconCount;

	public GameObject m_multiRoot;

	public CanvasGroup m_multiRootCanvasGroup;

	public GameObject m_singleRoot;

	public GameObject m_deadRoot;

	private bool m_explodedListIsVisible;
}
