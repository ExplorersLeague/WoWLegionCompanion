using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class AutoCenterScrollPips : MonoBehaviour
	{
		private void Start()
		{
			this.m_scrollRect = base.GetComponentInParent<AutoCenterScrollRect>();
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.OnCenterComplete += this.UpdateSelectedPip;
			}
			this.UpdateSelectedPip(0);
		}

		private void Update()
		{
			this.UpdatePipCount();
		}

		private void UpdatePipCount()
		{
			AutoCenterItem[] componentsInChildren = this.m_scrollRect.GetComponentsInChildren<AutoCenterItem>();
			Image[] componentsInChildren2 = base.GetComponentsInChildren<Image>();
			Image[] componentsInChildren3 = this.m_megaPipsParent.GetComponentsInChildren<Image>();
			int num = componentsInChildren.Length;
			int selectedIndex = this.m_selectedIndex % 10;
			int num2 = this.m_selectedIndex / 10;
			int num3 = (num <= 10) ? 0 : (num / 10 + 1);
			int numPips = (num3 != 0 && num2 != num3 - 1) ? 10 : (num % 10);
			this.ResizePipList(componentsInChildren2, numPips, this.m_pipSize, selectedIndex, "Scroll pip", base.transform);
			this.ResizePipList(componentsInChildren3, num3, this.m_megaPipSize, num2, "Scroll mega pip", this.m_megaPipsParent.transform);
		}

		private void ResizePipList(Image[] pipList, int numPips, float pipSize, int selectedIndex, string name, Transform parent)
		{
			if (pipList.Length > numPips)
			{
				for (int i = pipList.Length - 1; i >= numPips; i--)
				{
					Object.Destroy(pipList[i].gameObject);
				}
			}
			else if (pipList.Length < numPips)
			{
				for (int j = pipList.Length; j < numPips; j++)
				{
					GameObject gameObject = new GameObject(name + " " + j);
					gameObject.transform.SetParent(parent, false);
					Image image = gameObject.AddComponent<Image>();
					image.sprite = ((j != selectedIndex) ? this.m_unselectedPipSprite : this.m_selectedPipSprite);
					image.preserveAspect = true;
					RectTransform rectTransform = gameObject.transform as RectTransform;
					rectTransform.offsetMin = Vector2.zero;
					rectTransform.offsetMax = new Vector2(pipSize, pipSize);
				}
			}
		}

		public void UpdateSelectedPip(int index)
		{
			if (this.m_scrollRect == null)
			{
				return;
			}
			this.m_selectedIndex = index;
			int num = this.m_selectedIndex % 10;
			int num2 = this.m_selectedIndex / 10;
			Image[] componentsInChildren = base.GetComponentsInChildren<Image>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sprite = ((i != num) ? this.m_unselectedPipSprite : this.m_selectedPipSprite);
			}
			Image[] componentsInChildren2 = this.m_megaPipsParent.GetComponentsInChildren<Image>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].sprite = ((j != num2) ? this.m_unselectedPipSprite : this.m_selectedPipSprite);
			}
		}

		public Sprite m_selectedPipSprite;

		public Sprite m_unselectedPipSprite;

		public float m_pipSize = 8f;

		public float m_megaPipSize = 16f;

		private AutoCenterScrollRect m_scrollRect;

		private int m_selectedIndex;

		public GameObject m_megaPipsParent;

		private const int MaxPips = 10;
	}
}
