using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class SpellDisplay : MonoBehaviour
	{
		public void SetSpell(int spellID)
		{
			this.m_spellID = spellID;
			VW_MobileSpellRec record = StaticDB.vW_MobileSpellDB.GetRecord(this.m_spellID);
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
				if (this.m_grayscaleShader != null)
				{
					Material material = new Material(this.m_grayscaleShader);
					this.m_spellIcon.material = material;
				}
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

		public void SetLocked(bool locked)
		{
			if (this.m_padlockIcon != null)
			{
				this.m_padlockIcon.gameObject.SetActive(locked);
				this.m_spellIcon.material.SetFloat("_GrayscaleAmount", (!locked) ? 0f : 1f);
			}
		}

		public Image m_spellIcon;

		public Text m_iconError;

		public Text m_spellName;

		public Image m_padlockIcon;

		public Shader m_grayscaleShader;

		private int m_spellID;
	}
}
