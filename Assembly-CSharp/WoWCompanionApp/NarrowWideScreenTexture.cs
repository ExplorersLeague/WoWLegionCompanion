using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class NarrowWideScreenTexture : Image
	{
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_delayedSwap = (Main.instance == null);
			if (!this.m_delayedSwap && Main.instance.IsNarrowScreen())
			{
				base.sprite = this.m_narrowScreenImage;
			}
		}

		private void Update()
		{
			if (this.m_delayedSwap && Main.instance != null)
			{
				if (Main.instance.IsNarrowScreen())
				{
					base.sprite = this.m_narrowScreenImage;
				}
				this.m_delayedSwap = false;
			}
		}

		public Sprite m_narrowScreenImage;

		private bool m_delayedSwap;
	}
}
