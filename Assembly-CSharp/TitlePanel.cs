using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
	public int CancelIndex { get; set; }

	private void Start()
	{
		DateTime today = DateTime.Today;
		Debug.Log(string.Concat(new object[]
		{
			"Date: ",
			today.Month,
			"/",
			today.Day,
			"/",
			today.Year
		}));
		if (today.Year > 2017 || today.Month > 6 || today.Day > 23)
		{
			this.m_showPTR = false;
		}
		if (Login.instance.IsDevRegionList())
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
				"KR"
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
		this.m_briefBuildText.text = string.Format("{0:D2}", BuildNum.CodeBuildNum);
		this.m_buildText.text = string.Empty;
		this.UpdateResumeButtonVisiblity();
		this.m_legalText.font = GeneralHelpers.LoadStandardFont();
		this.m_legalText.text = StaticDB.GetString("LEGAL_TEXT", "(c) 2016 Blizzard Entertainment, Inc. All rights reserved.");
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		if (!Login.instance.IsDevRegionList())
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
		if (Login.instance.IsDevRegionList())
		{
			for (int i = 0; i < this.m_portalDropdown.options.Count; i++)
			{
				if (this.m_portalDropdown.options.ToArray()[i].text.ToLower() == Login.instance.GetBnPortal())
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
			string bnPortal = Login.instance.GetBnPortal();
			switch (bnPortal)
			{
			case "us":
				value = 0;
				break;
			case "eu":
				value = 1;
				break;
			case "kr":
				value = 2;
				break;
			case "cn":
				value = 3;
				break;
			case "beta":
				value = 4;
				break;
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
			AllPopups.instance.ShowRegionConfirmationPopup(index);
		}
	}

	private string GetDropdownPortalText()
	{
		string text;
		if (Login.instance.IsDevRegionList())
		{
			text = this.m_portalDropdown.options.ToArray()[this.m_portalDropdown.value].text.ToLower();
			if (text.ToLower() == "ptr")
			{
				text = "beta";
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
				text = "beta";
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
		bool flag = Login.instance.GetBnPortal() != dropdownPortalText;
		Login.instance.SetPortal(dropdownPortalText);
		if (flag)
		{
			Login.instance.ClearAllCachedTokens();
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
		bool flag = Login.instance.HaveCachedWebToken();
		this.m_resumeButton.gameObject.SetActive(flag);
		if (flag)
		{
			Text componentInChildren = this.m_resumeButton.GetComponentInChildren<Text>();
			if (componentInChildren != null)
			{
				componentInChildren.font = GeneralHelpers.LoadStandardFont();
				componentInChildren.text = StaticDB.GetString("LOGIN", null);
				this.m_loginButtonText.font = GeneralHelpers.LoadStandardFont();
				this.m_loginButtonText.text = StaticDB.GetString("ACCOUNT_SELECTION", null);
			}
		}
		else
		{
			this.m_loginButtonText.font = GeneralHelpers.LoadStandardFont();
			this.m_loginButtonText.text = StaticDB.GetString("LOGIN", null);
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
