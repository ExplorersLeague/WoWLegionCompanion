using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class PartyBuffDisplay : MonoBehaviour
	{
		public void SetAbility(int garrAbilityID)
		{
			GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(garrAbilityID);
			if (record == null)
			{
				Debug.LogWarning("Invalid garrAbilityID " + garrAbilityID);
				return;
			}
			this.m_abilityName.text = record.Name;
			this.m_abilityDescription.text = WowTextParser.parser.Parse(record.Description, 0);
			this.m_abilityDescription.supportRichText = WowTextParser.parser.IsRichText();
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
			if (sprite != null)
			{
				this.m_abilityIcon.sprite = sprite;
			}
		}

		public void UseReducedHeight()
		{
			this.m_abilityName.fontSize = 16;
			this.m_abilityDescription.fontSize = 15;
		}

		public Image m_abilityIcon;

		public Text m_abilityName;

		public Text m_abilityDescription;
	}
}
