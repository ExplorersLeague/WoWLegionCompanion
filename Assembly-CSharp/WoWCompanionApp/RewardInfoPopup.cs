using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
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
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
			if (this.m_azeriteFrame != null)
			{
				this.m_azeriteFrame.SetActive(false);
			}
			if (this.m_rewardIconBorder != null)
			{
				this.m_rewardIconBorder.gameObject.SetActive(false);
			}
		}

		private void OnDisable()
		{
			if (!this.m_disableScreenBlurEffect)
			{
				Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
				Main.instance.m_canvasBlurManager.RemoveBlurRef_Level2Canvas();
			}
			Main.instance.m_backButtonManager.PopBackAction();
		}

		public void SetReward(MissionRewardDisplay.RewardType rewardType, int rewardID, int rewardQuantity, Sprite rewardSprite, int itemContext, WrapperItemInstance? itemInstance)
		{
			this.m_rewardType = rewardType;
			this.m_rewardID = rewardID;
			switch (rewardType)
			{
			case MissionRewardDisplay.RewardType.item:
			{
				ItemStatCache instance = ItemStatCache.instance;
				instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats, WrapperItemInstance?>)Delegate.Combine(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats, WrapperItemInstance?>(this.ItemStatsUpdated));
				this.SetItem(rewardID, itemContext, rewardSprite, itemInstance);
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

		private void ItemStatsUpdated(int itemID, int itemContext, WrapperItemStats itemStats, WrapperItemInstance? itemInstance)
		{
			if (this.m_rewardType == MissionRewardDisplay.RewardType.item)
			{
				this.SetItem(this.m_rewardID, itemContext, this.m_rewardIcon.sprite, itemInstance);
				ItemStatCache instance = ItemStatCache.instance;
				instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats, WrapperItemInstance?>)Delegate.Remove(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats, WrapperItemInstance?>(this.ItemStatsUpdated));
			}
		}

		public void SetItem(int itemID, int itemContext, Sprite iconSprite, WrapperItemInstance? itemInstance)
		{
			this.m_rewardQuantity.text = string.Empty;
			this.m_rewardName.text = string.Empty;
			this.m_rewardDescription.text = string.Empty;
			this.m_rewardIcon.sprite = iconSprite;
			ItemRec record3 = StaticDB.itemDB.GetRecord(itemID);
			if (record3 != null)
			{
				WrapperItemStats? itemStats = ItemStatCache.instance.GetItemStats(itemID, itemContext, itemInstance);
				Color color;
				ColorUtility.TryParseHtmlString("#" + GeneralHelpers.GetItemQualityColor(1), ref color);
				if (itemStats != null)
				{
					this.m_rewardName.text = GeneralHelpers.GetItemQualityColorTag(itemStats.Value.Quality) + record3.Display + "</color>";
					ColorUtility.TryParseHtmlString("#" + GeneralHelpers.GetItemQualityColor(itemStats.Value.Quality), ref color);
				}
				else
				{
					this.m_rewardName.text = GeneralHelpers.GetItemQualityColorTag(record3.OverallQualityID) + record3.Display + "</color>";
					ColorUtility.TryParseHtmlString("#" + GeneralHelpers.GetItemQualityColor(record3.OverallQualityID), ref color);
				}
				this.m_rewardName.supportRichText = true;
				if (record3.ItemNameDescriptionID > 0)
				{
					ItemNameDescriptionRec record2 = StaticDB.itemNameDescriptionDB.GetRecord(record3.ItemNameDescriptionID);
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
				if (this.m_azeriteFrame != null)
				{
					this.m_azeriteFrame.SetActive(StaticDB.azeriteEmpoweredItemDB.GetRecordFirstOrDefault((AzeriteEmpoweredItemRec record) => record.ItemID == itemID) != null);
				}
				if (record3.ClassID == 2 || record3.ClassID == 3 || record3.ClassID == 4 || record3.ClassID == 5 || record3.ClassID == 6)
				{
					int itemLevel = record3.ItemLevel;
					if (itemStats != null)
					{
						itemLevel = itemStats.Value.ItemLevel;
					}
					bool flag = itemStats != null && (itemStats.Value.BonusFlags & 16u) != 0u;
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
						(!flag) ? string.Empty : StaticDB.GetString("PLUS", "+ [PH]"),
						"</color>"
					});
				}
				if (record3.Bonding > 0)
				{
					string text2 = string.Empty;
					if ((record3.Flags[0] & 134217728) != 0)
					{
						if ((record3.Flags[1] & 131072) != 0)
						{
							text2 = StaticDB.GetString("ITEM_BIND_TO_BNETACCOUNT", null);
						}
						else
						{
							text2 = StaticDB.GetString("ITEM_BIND_TO_ACCOUNT", null);
						}
					}
					else if (record3.Bonding == 1)
					{
						text2 = StaticDB.GetString("ITEM_BIND_ON_PICKUP", null);
					}
					else if (record3.Bonding == 4)
					{
						text2 = StaticDB.GetString("ITEM_BIND_QUEST", null);
					}
					else if (record3.Bonding == 2)
					{
						text2 = StaticDB.GetString("ITEM_BIND_ON_EQUIP", null);
					}
					else if (record3.Bonding == 3)
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
				ItemSubClassRec itemSubclass = StaticDB.GetItemSubclass(record3.ClassID, record3.SubclassID);
				if (itemSubclass != null && itemSubclass.DisplayName != null && itemSubclass.DisplayName != string.Empty && ((int)itemSubclass.DisplayFlags & 1) == 0 && record3.InventoryType != 16)
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
				string inventoryTypeString = GeneralHelpers.GetInventoryTypeString((INVENTORY_TYPE)record3.InventoryType);
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
					if (itemStats.Value.MinDamage != 0 || itemStats.Value.MaxDamage != 0)
					{
						if (this.m_rewardDescription.text != string.Empty)
						{
							Text rewardDescription5 = this.m_rewardDescription;
							rewardDescription5.text += "\n";
						}
						if (itemStats.Value.MinDamage == itemStats.Value.MaxDamage)
						{
							Text rewardDescription6 = this.m_rewardDescription;
							rewardDescription6.text += GeneralHelpers.TextOrderString(itemStats.Value.MinDamage.ToString(), StaticDB.GetString("DAMAGE", null));
						}
						else
						{
							Text rewardDescription7 = this.m_rewardDescription;
							rewardDescription7.text += GeneralHelpers.TextOrderString(itemStats.Value.MinDamage.ToString() + " - " + itemStats.Value.MaxDamage.ToString(), StaticDB.GetString("DAMAGE", null));
						}
					}
					if (itemStats.Value.EffectiveArmor > 0)
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
							GeneralHelpers.TextOrderString(itemStats.Value.EffectiveArmor.ToString(), StaticDB.GetString("ARMOR", null)),
							"</color>"
						});
					}
					foreach (WrapperItemBonusStat wrapperItemBonusStat in itemStats.Value.BonusStats)
					{
						if (wrapperItemBonusStat.BonusAmount != 0)
						{
							string bonusStatString = GeneralHelpers.GetBonusStatString((BonusStatIndex)wrapperItemBonusStat.StatID);
							if (!string.IsNullOrEmpty(bonusStatString))
							{
								if (this.m_rewardDescription.text != string.Empty)
								{
									Text rewardDescription10 = this.m_rewardDescription;
									rewardDescription10.text += "\n";
								}
								Text rewardDescription11 = this.m_rewardDescription;
								rewardDescription11.text = rewardDescription11.text + "<color=#" + GeneralHelpers.GetMobileStatColorString(wrapperItemBonusStat.Color) + ">";
								string str;
								if (wrapperItemBonusStat.BonusAmount > 0)
								{
									str = "+";
								}
								else
								{
									str = "-";
								}
								Text rewardDescription12 = this.m_rewardDescription;
								rewardDescription12.text = rewardDescription12.text + GeneralHelpers.TextOrderString(str + wrapperItemBonusStat.BonusAmount.ToString(), bonusStatString) + "</color>";
							}
						}
					}
				}
				int requiredLevel = record3.RequiredLevel;
				if (itemStats != null)
				{
					requiredLevel = itemStats.Value.RequiredLevel;
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
						text3 = GeneralHelpers.GetMobileStatColorString(5);
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
				string itemDescription = GeneralHelpers.GetItemDescription(record3);
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
				if (this.m_rewardIconBorder != null)
				{
					this.m_rewardIconBorder.gameObject.SetActive(true);
					this.m_rewardIconBorder.color = color;
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
			CurrencyContainerRec currencyContainerRec = CurrencyContainerDB.CheckAndGetValidCurrencyContainer(currencyID, quantity);
			if (currencyContainerRec != null)
			{
				this.m_rewardName.text = GeneralHelpers.GetItemQualityColorTag((int)currencyContainerRec.ContainerQuality) + currencyContainerRec.ContainerName + "</color>";
				this.m_rewardDescription.text = GeneralHelpers.QuantityRule(currencyContainerRec.ContainerDescription, quantity);
				this.m_rewardQuantity.text = string.Empty;
				this.m_rewardIcon.sprite = iconSprite;
				if (this.m_rewardIconBorder != null)
				{
					Color color;
					ColorUtility.TryParseHtmlString("#" + GeneralHelpers.GetItemQualityColor((int)currencyContainerRec.ContainerQuality), ref color);
					this.m_rewardIconBorder.gameObject.SetActive(true);
					this.m_rewardIconBorder.color = color;
				}
			}
			else
			{
				if (record != null)
				{
					this.m_rewardName.text = record.Name;
					this.m_rewardDescription.text = record.Description;
				}
				this.m_rewardQuantity.text = ((quantity <= 1) ? string.Empty : (string.Empty + quantity));
				this.m_rewardIcon.sprite = iconSprite;
			}
		}

		public void SetGold(int quantity, Sprite iconSprite)
		{
			this.m_rewardName.text = StaticDB.GetString("GOLD", null);
			this.m_rewardDescription.text = StaticDB.GetString("GOLD_DESCRIPTION", null);
			this.m_rewardQuantity.text = ((quantity <= 1) ? string.Empty : (string.Empty + quantity.ToString(MobileDeviceLocale.GetCultureInfoLocale())));
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

		public Image m_rewardIconBorder;

		public GameObject m_azeriteFrame;

		private int m_rewardID;

		private MissionRewardDisplay.RewardType m_rewardType;

		public bool m_muteEnableSFX;

		public bool m_disableScreenBlurEffect;
	}
}
