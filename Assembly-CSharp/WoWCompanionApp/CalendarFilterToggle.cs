using System;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[RequireComponent(typeof(Toggle))]
	public class CalendarFilterToggle : MonoBehaviour
	{
		private void Awake()
		{
			this.m_toggle = base.GetComponent<Toggle>();
			this.m_toggle.isOn = CalendarCVar.GetCVarValue(this.m_cvar);
		}

		public void OnValueChanged(bool value)
		{
			CalendarCVar.SetCVarValue(this.m_cvar, this.m_toggle.isOn);
		}

		public CalendarCVar.CVarType m_cvar;

		private Toggle m_toggle;
	}
}
