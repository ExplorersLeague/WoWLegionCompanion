﻿using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class TalentTooltip : BaseDialog
	{
		private void Start()
		{
		}

		public void SetTalent(TalentTreeItemAbilityButton abilityButton)
		{
			this.m_abilityButton = abilityButton;
			this.m_garrTalentRec = StaticDB.garrTalentDB.GetRecord(abilityButton.GetTalentID());
			this.m_talentName.text = this.m_garrTalentRec.Name;
			this.m_talentDescription.text = WowTextParser.parser.Parse(this.m_garrTalentRec.Description, 0);
			this.m_talentDescription.supportRichText = WowTextParser.parser.IsRichText();
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, this.m_garrTalentRec.IconFileDataID);
			if (sprite != null)
			{
				this.m_abilityIcon.sprite = sprite;
			}
			this.m_researchTimeAndCostSection.SetActive(true);
			int num = (!abilityButton.CanRespec()) ? this.m_garrTalentRec.ResearchCost : this.m_garrTalentRec.RespecCost;
			this.m_resourceCostText.text = ((GarrisonStatus.Resources() >= num) ? "<color=#ffffffff>" : "<color=#FF0000FF>") + ((!abilityButton.CanRespec()) ? this.m_garrTalentRec.ResearchCost : this.m_garrTalentRec.RespecCost) + "</color>";
			Sprite sprite2 = GeneralHelpers.LoadCurrencyIcon((int)this.m_garrTalentRec.ResearchCostCurrencyTypesID);
			if (sprite2 != null)
			{
				this.m_resourceIcon.sprite = sprite2;
			}
			bool flag = abilityButton.IsResearching() && abilityButton.IsRespec();
			bool flag2 = false;
			TalentTreeItemAbilityButton sameTierButton = abilityButton.GetSameTierButton();
			if (sameTierButton != null && (sameTierButton.IsOwned() || (sameTierButton.IsResearching() && sameTierButton.IsRespec())))
			{
				flag2 = true;
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)((!abilityButton.CanRespec() && !flag && !flag2) ? this.m_garrTalentRec.ResearchDurationSecs : this.m_garrTalentRec.RespecDurationSecs));
			this.m_researchDurationText.text = timeSpan.GetDurationString(false);
			this.m_yourResourcesDisplayObj.SetActive(false);
			if (abilityButton.CanResearch() || abilityButton.CanRespec())
			{
				this.m_availableForResearchSection.SetActive(true);
				this.m_unavailableForResearchSection.SetActive(false);
				this.m_researchOrRespecText.text = ((!abilityButton.CanRespec()) ? StaticDB.GetString("RESEARCH", null) : StaticDB.GetString("RESPEC", null));
				this.m_yourResourcesDisplayObj.SetActive(true);
			}
			else
			{
				this.m_availableForResearchSection.SetActive(false);
				this.m_unavailableForResearchSection.SetActive(true);
				if (this.m_abilityButton.IsOwned())
				{
					this.m_yourResourcesDisplayObj.SetActive(false);
					this.m_statusText.text = "<color=#ffffffff>" + StaticDB.GetString("TALENT_OWNED", null) + "</color>";
					this.m_researchTimeAndCostSection.SetActive(false);
				}
				else if (this.m_abilityButton.IsResearching())
				{
					TimeSpan timeSpan2 = (!abilityButton.IsRespec()) ? this.m_abilityButton.GetRemainingResearchTime() : this.m_abilityButton.GetRemainingRespecTime();
					this.m_statusText.text = string.Concat(new string[]
					{
						"<color=#FFC600FF>",
						StaticDB.GetString("TIME_LEFT", null),
						"</color> <color=#ffffffff>",
						timeSpan2.GetDurationString(false),
						"</color>"
					});
				}
				else if (GarrisonStatus.Resources() < num)
				{
					this.m_yourResourcesDisplayObj.SetActive(true);
					this.m_statusText.text = "<color=#FF0000FF>" + StaticDB.GetString("NEED_MORE_RESOURCES", null) + "</color>";
				}
				else if (this.m_talentTreePanel.AnyTalentIsResearching())
				{
					this.m_statusText.text = "<color=#FF0000FF>" + StaticDB.GetString("ALREADY_RESEARCHING", null) + "</color>";
				}
				else
				{
					string whyCantResearch = this.m_abilityButton.GetWhyCantResearch();
					if (whyCantResearch != null && whyCantResearch != string.Empty)
					{
						this.m_statusText.text = "<color=#FF0000FF>" + whyCantResearch + "</color>";
					}
					else
					{
						this.m_statusText.text = "<color=#FF0000FF>" + StaticDB.GetString("MUST_RESEARCH_PREVIOUS_TIER", null) + "</color>";
					}
				}
			}
		}

		public void StartResearch()
		{
			this.m_abilityButton.StartResearch();
			base.gameObject.SetActive(false);
		}

		[Header("Common")]
		public TalentTreePanel m_talentTreePanel;

		public Image m_abilityIcon;

		public Text m_talentName;

		public Text m_talentDescription;

		public GameObject m_yourResourcesDisplayObj;

		[Header("Research Time And Cost")]
		public GameObject m_researchTimeAndCostSection;

		public Text m_researchOrRespecText;

		public Image m_resourceIcon;

		public Text m_resourceCostText;

		public Text m_researchDurationText;

		[Header("Available For Research")]
		public GameObject m_availableForResearchSection;

		[Header("Unavailable For Research")]
		public GameObject m_unavailableForResearchSection;

		public Text m_statusText;

		private TalentTreeItemAbilityButton m_abilityButton;

		private GarrTalentRec m_garrTalentRec;
	}
}
