using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class EmissaryCollection : MonoBehaviour
	{
		public void ClearCollection()
		{
			BountySite[] componentsInChildren = this.m_collectionObject.GetComponentsInChildren<BountySite>();
			foreach (BountySite bountySite in componentsInChildren)
			{
				Object.Destroy(bountySite);
			}
			this.m_collectionObject.DetachAllChildren();
			base.gameObject.SetActive(false);
		}

		public void AddBountyObjectToCollection(GameObject obj)
		{
			BountySite[] componentsInChildren = this.m_collectionObject.GetComponentsInChildren<BountySite>();
			if (componentsInChildren.Length < 3)
			{
				obj.transform.SetParent(this.m_collectionObject.transform, false);
				base.gameObject.SetActive(true);
			}
		}

		public GameObject m_collectionObject;

		public CanvasGroup m_canvasGroup;
	}
}
