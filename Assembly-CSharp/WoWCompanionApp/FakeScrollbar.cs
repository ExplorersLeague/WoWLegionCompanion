using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class FakeScrollbar : MonoBehaviour
	{
		private void Update()
		{
			base.GetComponent<Slider>().value = this.m_scrollbar.GetComponent<Scrollbar>().value;
			if (this.m_scrollbar.activeSelf)
			{
				if (this.m_sliderCanvas)
				{
					this.m_sliderCanvas.enabled = true;
				}
			}
			else if (this.m_sliderCanvas)
			{
				this.m_sliderCanvas.enabled = false;
			}
		}

		public GameObject m_scrollbar;

		public Canvas m_sliderCanvas;
	}
}
