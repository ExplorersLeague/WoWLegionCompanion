using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class AbilityInfoPopup : BaseDialog
	{
		public void SetAbility(int garrAbilityID)
		{
			GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(garrAbilityID);
			if (record == null)
			{
				Debug.LogWarning("Invalid garrAbilityID " + garrAbilityID);
				return;
			}
			this.m_abilityNameText.text = record.Name;
			this.m_abilityDescription.text = WowTextParser.parser.Parse(record.Description, 0);
			this.m_abilityDescription.supportRichText = WowTextParser.parser.IsRichText();
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
			if (sprite != null)
			{
				this.m_abilityIcon.sprite = sprite;
			}
		}

		public void SetSpell(int spellID)
		{
			VW_MobileSpellRec record = StaticDB.vw_mobileSpellDB.GetRecord(spellID);
			if (record == null)
			{
				this.m_abilityNameText.text = "Err Spell ID " + spellID;
				Debug.LogWarning("Invalid spellID " + spellID);
				return;
			}
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.SpellIconFileDataID);
			if (sprite != null)
			{
				this.m_abilityIcon.sprite = sprite;
			}
			else
			{
				Debug.LogWarning("Invalid or missing icon: " + record.SpellIconFileDataID);
			}
			this.m_abilityNameText.text = record.Name;
			SpellTooltipRec record2 = StaticDB.spellTooltipDB.GetRecord(spellID);
			if (record2 == null)
			{
				this.m_abilityNameText.text = "Err Tooltip ID " + spellID;
				Debug.LogWarning("Invalid Tooltip " + spellID);
				return;
			}
			this.m_abilityDescription.text = WowTextParser.parser.Parse(record2.Description, 0);
		}

		public Image m_abilityIcon;

		public Text m_abilityNameText;

		public Text m_abilityDescription;
	}
}
