using System;
using UnityEngine;

namespace WoWCompanionApp
{
	public class AutoCenterItem : MonoBehaviour
	{
		public void SetCentered(bool isCentered)
		{
			this.m_panelIsCentered = isCentered;
		}

		public bool IsCentered()
		{
			return this.m_panelIsCentered;
		}

		private bool m_panelIsCentered;
	}
}
