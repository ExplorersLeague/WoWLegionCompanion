using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class CalendarFiltersDialog : BaseDialog
	{
		public EventsListPanel EventsListPanel { get; set; }

		private void Start()
		{
			this.m_originalToggleValues = base.GetComponentsInChildren<CalendarFilterToggle>().ToDictionary((CalendarFilterToggle filterToggle) => filterToggle, (CalendarFilterToggle filterToggle) => filterToggle.GetComponent<Toggle>().isOn);
		}

		public override void CloseDialog()
		{
			base.CloseDialog();
			if (this.m_originalToggleValues.Any((KeyValuePair<CalendarFilterToggle, bool> pair) => pair.Key.GetComponent<Toggle>().isOn != pair.Value))
			{
				MobileAccountData.SendAccountSettingsToServer();
				if (this.EventsListPanel != null)
				{
					this.EventsListPanel.RefreshPanelContent();
				}
			}
		}

		private Dictionary<CalendarFilterToggle, bool> m_originalToggleValues;
	}
}
