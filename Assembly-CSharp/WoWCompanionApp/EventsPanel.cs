using System;
using UnityEngine;

namespace WoWCompanionApp
{
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

		public EventsListPanel m_eventsListPanel;
	}
}
