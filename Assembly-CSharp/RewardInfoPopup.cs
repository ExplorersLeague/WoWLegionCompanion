using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class RewardInfoPopup : MonoBehaviour
{
	private void Start()
	{
		this.m_rewardName.font = GeneralHelpers.LoadStandardFont();
		this.m_rewardDescription.font = GeneralHelpers.LoadStandardFont();
		this.m_rewardQuantity.font = GeneralHelpers.LoadStandardFont();
	}

	public void OnEnable()
	{
		if (!this.m_muteEnableSFX)
		{
			Main.instance.m_UISound.Play_ShowGenericTooltip();
		}
		if (!this.m_disableScreenBlurEffect)
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_canvasBlurManager.AddBlurRef_Level2Canvas();
		}
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		if (!this.m_disableScreenBlurEffect)
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_canvasBlurManager.RemoveBlurRef_Level2Canvas();
		}
		if (this.m_rewardType == MissionRewardDisplay.RewardType.item)
		{
			ItemStatCache instance = ItemStatCache.instance;
			instance.ItemStatCacheUpdateAction = (Action<int, int, MobileItemStats>)Delegate.Remove(instance.ItemStatCacheUpdateAction, new Action<int, int, MobileItemStats>(this.ItemStatsUpdated));
		}
		Main.instance.m_backButtonManager.PopBackAction();
	}

	public void SetReward(MissionRewardDisplay.RewardType rewardType, int rewardID, int rewardQuantity, Sprite rewardSprite, int itemContext)
	{
		this.m_rewardType = rewardType;
		this.m_rewardID = rewardID;
		switch (rewardType)
		{
		case MissionRewardDisplay.RewardType.item:
		{
			ItemStatCache instance = ItemStatCache.instance;
			instance.ItemStatCacheUpdateAction = (Action<int, int, MobileItemStats>)Delegate.Combine(instance.ItemStatCacheUpdateAction, new Action<int, int, MobileItemStats>(this.ItemStatsUpdated));
			this.SetItem(rewardID, itemContext, rewardSprite);
			break;
		}
		case MissionRewardDisplay.RewardType.gold:
			this.SetGold(rewardQuantity, rewardSprite);
			break;
		case MissionRewardDisplay.RewardType.followerXP:
			this.SetFollowerXP(rewardQuantity, rewardSprite);
			break;
		case MissionRewardDisplay.RewardType.currency:
			this.SetCurrency(rewardID, rewardQuantity, rewardSprite);
			break;
		case MissionRewardDisplay.RewardType.faction:
			this.SetFaction(rewardID, rewardQuantity, rewardSprite);
			break;
		}
	}

	private void ItemStatsUpdated(int itemID, int itemContext, MobileItemStats itemStats)
	{
		if (this.m_rewardType == MissionRewardDisplay.RewardType.item)
		{
			this.SetItem(this.m_rewardID, itemContext, this.m_rewardIcon.sprite);
		}
	}

	public void SetItem(int itemID, int itemContext, Sprite iconSprite)
	{
		this.m_rewardQuantity.text = string.Empty;
		this.m_rewardName.text = string.Empty;
		this.m_rewardDescription.text = string.Empty;
		this.m_rewardIcon.sprite = iconSprite;
		ItemRec record = StaticDB.itemDB.GetRecord(itemID);
		if (record != null)
		{
			MobileItemStats itemStats = ItemStatCache.instance.GetItemStats(itemID, itemContext);
			if (itemStats != null)
			{
				this.m_rewardName.text = GeneralHelpers.GetItemQualityColorTag(itemStats.Quality) + record.Display + "</color>";
			}
			else
			{
				this.m_rewardName.text = GeneralHelpers.GetItemQualityColorTag(record.OverallQualityID) + record.Display + "</color>";
			}
			this.m_rewardName.supportRichText = true;
			if (record.ItemNameDescriptionID > 0)
			{
				ItemNameDescriptionRec record2 = StaticDB.itemNameDescriptionDB.GetRecord(record.ItemNameDescriptionID);
				if (record2 != null)
				{
					Text rewardName = this.m_rewardName;
					string text = rewardName.text;
					rewardName.text = string.Concat(new string[]
					{
						text,
						"\n<color=#",
						GeneralHelpers.GetColorFromInt(record2.Color),
						"ff>",
						record2.Description,
						"</color>"
					});
				}
			}
			if (record.ClassID == 2 || record.ClassID == 3 || record.ClassID == 4 || record.ClassID == 5 || record.ClassID == 6)
			{
				int itemLevel = record.ItemLevel;
				if (itemStats != null)
				{
					itemLevel = itemStats.ItemLevel;
				}
				Text rewardName2 = this.m_rewardName;
				string text = rewardName2.text;
				rewardName2.text = string.Concat(new string[]
				{
					text,
					"\n<color=#",
					GeneralHelpers.s_defaultColor,
					">",
					StaticDB.GetString("ITEM_LEVEL", null),
					" ",
					itemLevel.ToString(),
					"</color>"
				});
			}
			if (record.Bonding > 0)
			{
				string text2 = string.Empty;
				if ((record.Flags & 134217728) != 0)
				{
					if ((record.Flags1 & 131072) != 0)
					{
						text2 = StaticDB.GetString("ITEM_BIND_TO_BNETACCOUNT", null);
					}
					else
					{
						text2 = StaticDB.GetString("ITEM_BIND_TO_ACCOUNT", null);
					}
				}
				else if (record.Bonding == 1)
				{
					text2 = StaticDB.GetString("ITEM_BIND_ON_PICKUP", null);
				}
				else if (record.Bonding == 4)
				{
					text2 = StaticDB.GetString("ITEM_BIND_QUEST", null);
				}
				else if (record.Bonding == 2)
				{
					text2 = StaticDB.GetString("ITEM_BIND_ON_EQUIP", null);
				}
				else if (record.Bonding == 3)
				{
					text2 = StaticDB.GetString("ITEM_BIND_ON_USE", null);
				}
				if (text2 != string.Empty)
				{
					Text rewardName3 = this.m_rewardName;
					string text = rewardName3.text;
					rewardName3.text = string.Concat(new string[]
					{
						text,
						"\n<color=#",
						GeneralHelpers.s_normalColor,
						">",
						text2,
						"</color>"
					});
				}
			}
			ItemSubClassRec itemSubclass = StaticDB.GetItemSubclass(record.ClassID, record.SubclassID);
			if (itemSubclass != null && itemSubclass.DisplayName != null && itemSubclass.DisplayName != string.Empty && (itemSubclass.DisplayFlags & 1) == 0 && record.InventoryType != 16)
			{
				if (this.m_rewardDescription.text != string.Empty)
				{
					Text rewardDescription = this.m_rewardDescription;
					rewardDescription.text += "\n";
				}
				Text rewardDescription2 = this.m_rewardDescription;
				string text = rewardDescription2.text;
				rewardDescription2.text = string.Concat(new string[]
				{
					text,
					"<color=#",
					GeneralHelpers.s_normalColor,
					">",
					itemSubclass.DisplayName,
					"</color>"
				});
			}
			string inventoryTypeString = GeneralHelpers.GetInventoryTypeString((INVENTORY_TYPE)record.InventoryType);
			if (inventoryTypeString != null && inventoryTypeString != string.Empty)
			{
				if (this.m_rewardDescription.text != string.Empty)
				{
					Text rewardDescription3 = this.m_rewardDescription;
					rewardDescription3.text += "\n";
				}
				Text rewardDescription4 = this.m_rewardDescription;
				string text = rewardDescription4.text;
				rewardDescription4.text = string.Concat(new string[]
				{
					text,
					"<color=#",
					GeneralHelpers.s_normalColor,
					">",
					inventoryTypeString,
					"</color>"
				});
			}
			if (itemStats != null)
			{
				if (itemStats.MinDamage != 0 || itemStats.MaxDamage != 0)
				{
					if (this.m_rewardDescription.text != string.Empty)
					{
						Text rewardDescription5 = this.m_rewardDescription;
						rewardDescription5.text += "\n";
					}
					if (itemStats.MinDamage == itemStats.MaxDamage)
					{
						Text rewardDescription6 = this.m_rewardDescription;
						rewardDescription6.text += GeneralHelpers.TextOrderString(itemStats.MinDamage.ToString(), StaticDB.GetString("DAMAGE", null));
					}
					else
					{
						Text rewardDescription7 = this.m_rewardDescription;
						rewardDescription7.text += GeneralHelpers.TextOrderString(itemStats.MinDamage.ToString() + " - " + itemStats.MaxDamage.ToString(), StaticDB.GetString("DAMAGE", null));
					}
				}
				if (itemStats.EffectiveArmor > 0)
				{
					if (this.m_rewardDescription.text != string.Empty)
					{
						Text rewardDescription8 = this.m_rewardDescription;
						rewardDescription8.text += "\n";
					}
					Text rewardDescription9 = this.m_rewardDescription;
					string text = rewardDescription9.text;
					rewardDescription9.text = string.Concat(new string[]
					{
						text,
						"<color=#",
						GeneralHelpers.s_normalColor,
						">",
						GeneralHelpers.TextOrderString(itemStats.EffectiveArmor.ToString(), StaticDB.GetString("ARMOR", null)),
						"</color>"
					});
				}
				foreach (MobileItemBonusStat mobileItemBonusStat in itemStats.BonusStat)
				{
					if (mobileItemBonusStat.BonusAmount != 0)
					{
						if (this.m_rewardDescription.text != string.Empty)
						{
							Text rewardDescription10 = this.m_rewardDescription;
							rewardDescription10.text += "\n";
						}
						Text rewardDescription11 = this.m_rewardDescription;
						rewardDescription11.text = rewardDescription11.text + "<color=#" + GeneralHelpers.GetMobileStatColorString(mobileItemBonusStat.Color) + ">";
						string str;
						if (mobileItemBonusStat.BonusAmount > 0)
						{
							str = "+";
						}
						else
						{
							str = "-";
						}
						Text rewardDescription12 = this.m_rewardDescription;
						rewardDescription12.text = rewardDescription12.text + GeneralHelpers.TextOrderString(str + mobileItemBonusStat.BonusAmount.ToString(), GeneralHelpers.GetBonusStatString((BonusStatIndex)mobileItemBonusStat.StatID)) + "</color>";
					}
				}
			}
			int requiredLevel = record.RequiredLevel;
			if (itemStats != null)
			{
				requiredLevel = itemStats.RequiredLevel;
			}
			if (requiredLevel > 1)
			{
				if (this.m_rewardDescription.text != string.Empty)
				{
					Text rewardDescription13 = this.m_rewardDescription;
					rewardDescription13.text += "\n";
				}
				string text3 = GeneralHelpers.s_normalColor;
				if (GarrisonStatus.CharacterLevel() < requiredLevel)
				{
					text3 = GeneralHelpers.GetMobileStatColorString(MobileStatColor.MOBILE_STAT_COLOR_ERROR);
				}
				Text rewardDescription14 = this.m_rewardDescription;
				string text = rewardDescription14.text;
				rewardDescription14.text = string.Concat(new object[]
				{
					text,
					"<color=#",
					text3,
					">",
					StaticDB.GetString("ITEM_MIN_LEVEL", null),
					" ",
					requiredLevel,
					"</color>"
				});
			}
			string itemDescription = GeneralHelpers.GetItemDescription(record);
			if (itemDescription != null && itemDescription != string.Empty)
			{
				if (this.m_rewardDescription.text != string.Empty)
				{
					Text rewardDescription15 = this.m_rewardDescription;
					rewardDescription15.text += "\n";
				}
				Text rewardDescription16 = this.m_rewardDescription;
				rewardDescription16.text += itemDescription;
			}
			else if (itemStats == null)
			{
				if (this.m_rewardDescription.text != string.Empty)
				{
					Text rewardDescription17 = this.m_rewardDescription;
					rewardDescription17.text += "\n";
				}
				Text rewardDescription18 = this.m_rewardDescription;
				rewardDescription18.text += "...";
			}
		}
		else
		{
			this.m_rewardName.text = "Unknown Item" + itemID;
			this.m_rewardDescription.text = string.Empty;
		}
	}

	public void SetFollowerXP(int quantity, Sprite iconSprite)
	{
		this.m_rewardName.text = StaticDB.GetString("EXPERIENCE", null);
		this.m_rewardDescription.text = StaticDB.GetString("EXPERIENCE_DESCRIPTION", null);
		this.m_rewardQuantity.text = ((quantity <= 1) ? string.Empty : (string.Empty + quantity));
		this.m_rewardIcon.sprite = iconSprite;
	}

	public void SetCurrency(int currencyID, int quantity, Sprite iconSprite)
	{
		CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(currencyID);
		if (record != null)
		{
			this.m_rewardName.text = record.Name;
			this.m_rewardDescription.text = record.Description;
		}
		this.m_rewardQuantity.text = ((quantity <= 1) ? string.Empty : (string.Empty + quantity));
		this.m_rewardIcon.sprite = iconSprite;
	}

	public void SetGold(int quantity, Sprite iconSprite)
	{
		this.m_rewardName.text = StaticDB.GetString("GOLD", null);
		this.m_rewardDescription.text = StaticDB.GetString("GOLD_DESCRIPTION", null);
		this.m_rewardQuantity.text = ((quantity <= 1) ? string.Empty : (string.Empty + quantity));
		this.m_rewardIcon.sprite = iconSprite;
	}

	public void SetFaction(int factionID, int quantity, Sprite iconSprite)
	{
		FactionRec record = StaticDB.factionDB.GetRecord(factionID);
		if (record != null)
		{
			this.m_rewardName.text = string.Concat(new object[]
			{
				StaticDB.GetString("REPUTATION_AWARD", null),
				"\n<color=#",
				GeneralHelpers.s_defaultColor,
				">",
				record.Name,
				" +",
				quantity,
				"</color>"
			});
			this.m_rewardName.supportRichText = true;
			this.m_rewardDescription.text = string.Empty;
		}
		this.m_rewardQuantity.text = ((quantity <= 1) ? string.Empty : (string.Empty + quantity));
		this.m_rewardIcon.sprite = iconSprite;
	}

	public Text m_rewardName;

	public Text m_rewardDescription;

	public Text m_rewardQuantity;

	public Image m_rewardIcon;

	private int m_rewardID;

	private MissionRewardDisplay.RewardType m_rewardType;

	public bool m_muteEnableSFX;

	public bool m_disableScreenBlurEffect;
}
