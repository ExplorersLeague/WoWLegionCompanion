using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	[RequireComponent(typeof(Dropdown))]
	public class LanguageDropdown : MonoBehaviour
	{
		private void Start()
		{
			if (MobileDeviceLocale.IsChineseDevice())
			{
				Object.Destroy(base.transform.parent.gameObject);
				return;
			}
			this.m_dropdown = base.GetComponent<Dropdown>();
			this.m_dropdown.options = (from entry in this.m_languageEntries
			select new Dropdown.OptionData(entry.displayName)).ToList<Dropdown.OptionData>();
			int num = this.m_languageEntries.FindIndex((LanguageDropdown.LanguageEntry entry) => entry.localeCode == MobileDeviceLocale.GetBestGuessForLocale());
			this.m_dropdown.value = ((num == -1) ? 0 : num);
			this.m_dropdown.RefreshShownValue();
			this.m_dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChanged));
			this.m_value = this.m_dropdown.value;
		}

		public void OnValueChanged(int value)
		{
			if (this.m_value == value)
			{
				return;
			}
			Singleton<DialogFactory>.Instance.CreateOKCancelDialog("RESTART_REQUIRED", "ARE_YOU_SURE", delegate
			{
				SecurePlayerPrefs.SetString(MobileDeviceLocale.LocaleKey, this.m_languageEntries[value].localeCode, Main.uniqueIdentifier);
				Application.Quit();
			}, delegate
			{
				this.m_dropdown.value = this.m_value;
			});
		}

		private Dropdown m_dropdown;

		private List<LanguageDropdown.LanguageEntry> m_languageEntries = new List<LanguageDropdown.LanguageEntry>(new LanguageDropdown.LanguageEntry[]
		{
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "enUS",
				displayName = "English"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "deDE",
				displayName = "Deutsch"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "esES",
				displayName = "Español (EU)"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "esMX",
				displayName = "Español (AL)"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "frFR",
				displayName = "Français"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "itIT",
				displayName = "Italiano"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "koKR",
				displayName = "한국어"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "ptBR",
				displayName = "Português"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "ruRU",
				displayName = "Русский"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "zhCN",
				displayName = "简体中文"
			},
			new LanguageDropdown.LanguageEntry
			{
				localeCode = "zhTW",
				displayName = "繁體中文"
			}
		});

		private int m_value;

		private struct LanguageEntry
		{
			public string localeCode;

			public string displayName;
		}
	}
}
