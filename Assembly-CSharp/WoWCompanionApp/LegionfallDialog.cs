using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class LegionfallDialog : MonoBehaviour
	{
		private void Start()
		{
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			LayoutElement component2 = this.m_legionfallBuildingPanelPrefab.GetComponent<LayoutElement>();
			HorizontalLayoutGroup component3 = this.m_content.GetComponent<HorizontalLayoutGroup>();
			component3.padding.left = (int)((component.rect.width - component2.minWidth) / 2f);
			component3.padding.right = component3.padding.left;
			component3.spacing = (float)(component3.padding.left - 39);
		}

		private void OnEnable()
		{
			LegionfallBuildingPanel[] componentsInChildren = this.m_content.gameObject.GetComponentsInChildren<LegionfallBuildingPanel>(true);
			foreach (LegionfallBuildingPanel legionfallBuildingPanel in componentsInChildren)
			{
				Object.Destroy(legionfallBuildingPanel.gameObject);
			}
			LegionfallBuildingPanel legionfallBuildingPanel2 = Object.Instantiate<LegionfallBuildingPanel>(this.m_legionfallBuildingPanelPrefab);
			legionfallBuildingPanel2.InitPanel(1, 46277);
			legionfallBuildingPanel2.transform.SetParent(this.m_content, false);
			LegionfallBuildingPanel legionfallBuildingPanel3 = Object.Instantiate<LegionfallBuildingPanel>(this.m_legionfallBuildingPanelPrefab);
			legionfallBuildingPanel3.InitPanel(3, 46735);
			legionfallBuildingPanel3.transform.SetParent(this.m_content, false);
			LegionfallBuildingPanel legionfallBuildingPanel4 = Object.Instantiate<LegionfallBuildingPanel>(this.m_legionfallBuildingPanelPrefab);
			legionfallBuildingPanel4.InitPanel(4, 46736);
			legionfallBuildingPanel4.transform.SetParent(this.m_content, false);
		}

		public LegionfallBuildingPanel m_legionfallBuildingPanelPrefab;

		public Transform m_content;
	}
}
