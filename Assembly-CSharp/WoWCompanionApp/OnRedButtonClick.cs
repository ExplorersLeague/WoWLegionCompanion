using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WoWCompanionApp
{
	public class OnRedButtonClick : MonoBehaviour
	{
		public void ResizeRedButton(BaseEventData e)
		{
			RectTransform rectTransform = this.redButtonGameObject.transform as RectTransform;
			this.initialScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
			rectTransform.localScale = new Vector3(rectTransform.localScale.x * 0.8f, rectTransform.localScale.y * 0.8f, 0f);
		}

		public void RevertRedButtonSize(BaseEventData e)
		{
			RectTransform rectTransform = this.redButtonGameObject.transform as RectTransform;
			rectTransform.localScale = new Vector3(this.initialScale.x, this.initialScale.y, this.initialScale.z);
		}

		public GameObject redButtonGameObject;

		private Vector3 initialScale;
	}
}
