using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class StackableMapIconContainer : MonoBehaviour
	{
		private void Awake()
		{
			this.m_icons = new List<StackableMapIcon>();
			this.m_iconCount.font = FontLoader.LoadStandardFont();
		}

		private void OnEnable()
		{
			this.ShowIconArea(false);
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.SelectedIconContainerChanged = (Action<StackableMapIconContainer>)Delegate.Combine(instance.SelectedIconContainerChanged, new Action<StackableMapIconContainer>(this.HandleSelectedIconContainerChanged));
		}

		private void OnDisable()
		{
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.SelectedIconContainerChanged = (Action<StackableMapIconContainer>)Delegate.Remove(instance.SelectedIconContainerChanged, new Action<StackableMapIconContainer>(this.HandleSelectedIconContainerChanged));
		}

		public void ShowIconAreaBG(bool show)
		{
			this.m_iconAreaBG.gameObject.SetActive(show);
		}

		private void Update()
		{
			if (this.GetIconCount() > 1)
			{
				AdventureMapWorldQuest[] componentsInChildren = base.GetComponentsInChildren<AdventureMapWorldQuest>();
				foreach (AdventureMapWorldQuest adventureMapWorldQuest in componentsInChildren)
				{
					adventureMapWorldQuest.ForceUpdate();
				}
			}
		}

		public void ShowIconArea(bool show)
		{
			if (!show && this.GetIconCount() <= 1)
			{
				return;
			}
			this.m_iconAreaCanvasGroup.gameObject.SetActive(show);
			if (show)
			{
				RectTransform component = this.m_iconAreaCanvas.GetComponent<RectTransform>();
				component.rect.Set(0f, 0f, 120f, 192f);
				if (this.m_closeButton != null)
				{
					RectTransform rectTransform = this.m_closeButton.transform as RectTransform;
					AdventureMapPanel componentInParent = base.gameObject.GetComponentInParent<AdventureMapPanel>();
					if (componentInParent != null)
					{
						RectTransform rectTransform2 = base.gameObject.GetComponentInParent<AdventureMapPanel>().transform as RectTransform;
						rectTransform.position = rectTransform2.position;
						rectTransform.SetSizeWithCurrentAnchors(0, rectTransform2.rect.width);
						rectTransform.SetSizeWithCurrentAnchors(1, rectTransform2.rect.height);
					}
				}
			}
			if (show && this.GetIconCount() > 1)
			{
				this.m_iconAreaCanvas.sortingOrder = 2;
			}
			else
			{
				this.m_iconAreaCanvas.sortingOrder = 1;
			}
			if (this.m_closeButton != null)
			{
				this.m_closeButton.gameObject.SetActive(show && this.GetIconCount() > 1);
			}
		}

		private void HandleSelectedIconContainerChanged(StackableMapIconContainer container)
		{
			if (container == this)
			{
				if (this.GetIconCount() > 1)
				{
					this.ShowIconArea(true);
				}
			}
			else
			{
				this.ShowIconArea(false);
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
			if (this.m_icons.Contains(icon))
			{
				return;
			}
			this.m_icons.Add(icon);
			icon.transform.SetParent(this.m_iconArea.transform, false);
			icon.transform.transform.localPosition = Vector3.zero;
			this.UpdateAppearance();
		}

		public void RemoveStackableMapIcon(StackableMapIcon icon)
		{
			if (this.m_icons.Contains(icon))
			{
				if (this.GetIconCount() == 1)
				{
					StackableMapIconManager.RemoveStackableMapIconContainer(this);
					Object.Destroy(base.gameObject);
					return;
				}
				this.m_icons.Remove(icon);
			}
			this.UpdateAppearance();
		}

		private void UpdateAppearance()
		{
			if (this.m_icons == null || this.GetIconCount() == 0)
			{
				StackableMapIconManager.RemoveStackableMapIconContainer(this);
				Object.Destroy(base.gameObject);
				return;
			}
			this.ShowIconArea(false);
			if (this.GetIconCount() == 1)
			{
				this.m_countainerPreviewIconsGroup.SetActive(false);
				this.ShowIconArea(true);
				this.ShowIconAreaBG(false);
			}
			else
			{
				this.m_countainerPreviewIconsGroup.SetActive(true);
				this.ShowIconAreaBG(true);
			}
			this.m_iconCount.text = string.Empty + this.GetIconCount();
			base.gameObject.name = "IconContainer (" + ((this.GetIconCount() <= 0) ? "Single" : (string.Empty + this.GetIconCount())) + ")";
			base.gameObject.SetActive(true);
		}

		public void PlayTapSound()
		{
			Main.instance.m_UISound.Play_SelectWorldQuest();
		}

		public void ToggleIconList()
		{
			this.PlayTapSound();
			this.ShowIconArea(!this.m_iconArea.activeSelf);
			if (this.m_iconArea.activeSelf)
			{
				AdventureMapPanel.instance.SetSelectedIconContainer(this);
				UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 3f, 0f);
			}
			else
			{
				AdventureMapPanel.instance.SetSelectedIconContainer(null);
			}
		}

		public int GetIconCount()
		{
			return this.m_icons.Count;
		}

		public GameObject m_countainerPreviewIconsGroup;

		public Text m_iconCount;

		public GameObject m_iconArea;

		public Canvas m_iconAreaCanvas;

		public CanvasGroup m_iconAreaCanvasGroup;

		public Image m_iconAreaBG;

		public int m_startLocationMapID;

		public GameObject m_closeButton;

		public List<StackableMapIcon> m_icons;
	}
}
