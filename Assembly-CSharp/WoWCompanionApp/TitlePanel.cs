using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoWCompanionApp
{
	public class TitlePanel : MonoBehaviour
	{
		public int CancelIndex { get; set; }

		private void Start()
		{
			DateTime utcNow = DateTime.UtcNow;
			Debug.Log(string.Concat(new object[]
			{
				"Date: ",
				utcNow.Month,
				"/",
				utcNow.Day,
				"/",
				utcNow.Year
			}));
			DateTime t = new DateTime(2019, 3, 12, 7, 0, 0, DateTimeKind.Utc);
			if (utcNow > t)
			{
				this.m_showPTR = false;
			}
			if (Singleton<Login>.instance.IsDevRegionList())
			{
				this.m_regionOptions = new string[]
				{
					"WoW-Dev",
					"PTR",
					"ST-US",
					"ST-EU",
					"ST-KR",
					"ST-CN",
					"US",
					"EU",
					"CN",
					"KR",
					"ST21"
				};
			}
			else if (this.m_showPTR)
			{
				this.m_regionOptions = new string[5];
			}
			else
			{
				this.m_regionOptions = new string[4];
			}
			this.m_briefBuildText.text = Application.version;
			this.m_buildText.text = string.Empty;
			this.UpdateResumeButtonVisiblity();
			List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
			if (!Singleton<Login>.instance.IsDevRegionList())
			{
				this.m_regionOptions[0] = StaticDB.GetString("AMERICAS_AND_OCEANIC", "Americas and Oceanic");
				this.m_regionOptions[1] = StaticDB.GetString("EUROPE", "Europe");
				this.m_regionOptions[2] = StaticDB.GetString("KOREA_AND_TAIWAN", "Korea and Taiwan");
				this.m_regionOptions[3] = StaticDB.GetString("CHINA", "China");
				if (this.m_showPTR)
				{
					this.m_regionOptions[4] = "PTR";
				}
			}
			for (int i = 0; i < this.m_regionOptions.Length; i++)
			{
				list.Add(new Dropdown.OptionData
				{
					text = this.m_regionOptions[i]
				});
			}
			this.m_portalDropdown.ClearOptions();
			this.m_portalDropdown.AddOptions(list);
		}

		private void Update()
		{
		}

		private void OnEnable()
		{
			this.m_legionLogo.SetActive(false);
			this.m_legionLogo_CN.SetActive(false);
			this.m_legionLogo_TW.SetActive(false);
			string locale = Main.instance.GetLocale();
			if (locale == "zhCN")
			{
				this.m_legionLogo_CN.SetActive(true);
			}
			else if (locale == "zhTW")
			{
				this.m_legionLogo_TW.SetActive(true);
			}
			else
			{
				this.m_legionLogo.SetActive(true);
			}
			if (Singleton<Login>.instance.IsDevRegionList())
			{
				for (int i = 0; i < this.m_portalDropdown.options.Count; i++)
				{
					if (this.m_portalDropdown.options.ToArray()[i].text.ToLower() == Singleton<Login>.instance.GetBnPortal())
					{
						this.m_showDialog = false;
						this.m_portalDropdown.value = i;
						this.m_showDialog = true;
						break;
					}
				}
			}
			else
			{
				int value = 0;
				string bnPortal = Singleton<Login>.instance.GetBnPortal();
				if (bnPortal != null)
				{
					if (!(bnPortal == "us"))
					{
						if (!(bnPortal == "eu"))
						{
							if (!(bnPortal == "kr"))
							{
								if (!(bnPortal == "cn"))
								{
									if (bnPortal == "beta" || bnPortal == "test")
									{
										value = 4;
									}
								}
								else
								{
									value = 3;
								}
							}
							else
							{
								value = 2;
							}
						}
						else
						{
							value = 1;
						}
					}
					else
					{
						value = 0;
					}
				}
				this.m_showDialog = false;
				this.m_portalDropdown.value = value;
				this.m_showDialog = true;
			}
			this.CancelIndex = this.m_portalDropdown.value;
		}

		public void PortalDropdownChanged(int index)
		{
			Debug.Log(string.Concat(new object[]
			{
				"New index ",
				index,
				", cancelIndex ",
				this.CancelIndex
			}));
			if (this.m_showDialog)
			{
				Singleton<Login>.Instance.LoginUI.ShowRegionConfirmationPopup(index);
			}
		}

		private string GetDropdownPortalText()
		{
			string text;
			if (Singleton<Login>.instance.IsDevRegionList())
			{
				text = this.m_portalDropdown.options.ToArray()[this.m_portalDropdown.value].text.ToLower();
				if (text.ToLower() == "ptr")
				{
					text = "test";
				}
			}
			else
			{
				switch (this.m_portalDropdown.value)
				{
				default:
					text = "us";
					break;
				case 1:
					text = "eu";
					break;
				case 2:
					text = "kr";
					break;
				case 3:
					text = "cn";
					break;
				case 4:
					text = "test";
					break;
				}
			}
			return text;
		}

		public void SetRegionIndex()
		{
			Debug.Log("Set index " + this.m_portalDropdown.value);
			this.CancelIndex = this.m_portalDropdown.value;
			string dropdownPortalText = this.GetDropdownPortalText();
			bool flag = Singleton<Login>.instance.GetBnPortal() != dropdownPortalText;
			Singleton<Login>.instance.SetPortal(dropdownPortalText);
			if (flag)
			{
				Singleton<Login>.instance.ClearAllCachedTokens();
				Debug.Log("Quitting");
				Application.Quit();
			}
		}

		public void CancelRegionIndex()
		{
			Debug.Log("Canceled index " + this.CancelIndex);
			this.m_showDialog = false;
			this.m_portalDropdown.value = this.CancelIndex;
			this.m_showDialog = true;
		}

		public void UpdateResumeButtonVisiblity()
		{
			bool flag = Singleton<Login>.instance.HaveCachedWebToken();
			this.m_resumeButton.gameObject.SetActive(flag);
			if (flag)
			{
				Text componentInChildren = this.m_resumeButton.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					componentInChildren.font = GeneralHelpers.LoadStandardFont();
					componentInChildren.text = StaticDB.GetString("LOGIN_CAPS", null);
					this.m_loginButtonText.font = GeneralHelpers.LoadStandardFont();
					this.m_loginButtonText.text = StaticDB.GetString("ACCOUNT_SELECTION_CAPS", null);
				}
			}
			else
			{
				this.m_loginButtonText.font = GeneralHelpers.LoadStandardFont();
				this.m_loginButtonText.text = StaticDB.GetString("LOGIN_CAPS", null);
			}
		}

		public GameObject m_legionLogo;

		public GameObject m_legionLogo_CN;

		public GameObject m_legionLogo_TW;

		public GameObject m_legionLogoGlowRoot;

		public Text m_buildText;

		public Text m_briefBuildText;

		public Text m_loginButtonText;

		public Text m_legalText;

		public Dropdown m_portalDropdown;

		public Button m_resumeButton;

		private bool m_showDialog = true;

		private bool m_showPTR = true;

		private string[] m_regionOptions;
	}
}
