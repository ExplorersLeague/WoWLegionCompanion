using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

public class SpellDisplay : MonoBehaviour
{
	public void SetSpell(int spellID)
	{
		this.m_spellID = spellID;
		VW_MobileSpellRec record = StaticDB.vw_mobileSpellDB.GetRecord(this.m_spellID);
		if (record == null)
		{
			this.m_spellName.text = "Err Spell ID " + this.m_spellID;
			Debug.LogWarning("Invalid spellID " + this.m_spellID);
			return;
		}
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.SpellIconFileDataID);
		if (sprite != null)
		{
			this.m_spellIcon.sprite = sprite;
			this.m_iconError.gameObject.SetActive(false);
		}
		else
		{
			Debug.LogWarning("Invalid or missing icon: " + record.SpellIconFileDataID);
			this.m_iconError.gameObject.SetActive(true);
			this.m_iconError.text = "Missing Icon " + record.SpellIconFileDataID;
		}
		this.m_spellName.text = record.Name;
	}

	public void ShowTooltip()
	{
		Main.instance.allPopups.ShowSpellInfoPopup(this.m_spellID);
	}

	public Image m_spellIcon;

	public Text m_iconError;

	public Text m_spellName;

	private int m_spellID;
}
