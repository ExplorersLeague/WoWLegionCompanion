using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class AdventureMapWorldQuest : MonoBehaviour
	{
		private void OnEnable()
		{
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.TestIconSizeChanged = (Action<float>)Delegate.Combine(instance.TestIconSizeChanged, new Action<float>(this.OnTestIconSizeChanged));
			PinchZoomContentManager pinchZoomContentManager = AdventureMapPanel.instance.m_pinchZoomContentManager;
			pinchZoomContentManager.ZoomFactorChanged = (Action<bool>)Delegate.Combine(pinchZoomContentManager.ZoomFactorChanged, new Action<bool>(this.HandleZoomChanged));
		}

		private void OnDisable()
		{
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.TestIconSizeChanged = (Action<float>)Delegate.Remove(instance.TestIconSizeChanged, new Action<float>(this.OnTestIconSizeChanged));
			PinchZoomContentManager pinchZoomContentManager = AdventureMapPanel.instance.m_pinchZoomContentManager;
			pinchZoomContentManager.ZoomFactorChanged = (Action<bool>)Delegate.Remove(pinchZoomContentManager.ZoomFactorChanged, new Action<bool>(this.HandleZoomChanged));
		}

		private void ItemStatsUpdated(int itemID, int itemContext, WrapperItemStats itemStats, WrapperItemInstance? itemInstance)
		{
			if (this.m_itemID == itemID && this.m_itemContext == itemContext)
			{
				ItemStatCache instance = ItemStatCache.instance;
				instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats, WrapperItemInstance?>)Delegate.Remove(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats, WrapperItemInstance?>(this.ItemStatsUpdated));
				this.ShowILVL();
			}
		}

		private void OnTestIconSizeChanged(float newScale)
		{
			base.transform.localScale = Vector3.one * newScale;
		}

		private void HandleZoomChanged(bool force)
		{
			if (this.m_zoomScaleRoot != null)
			{
				this.m_zoomScaleRoot.transform.localScale = Vector3.one * AdventureMapPanel.instance.m_pinchZoomContentManager.m_zoomFactor;
			}
		}

		public int QuestID
		{
			get
			{
				return this.m_questID;
			}
		}

		private void ShowILVL()
		{
			ItemRec record = StaticDB.itemDB.GetRecord(this.m_itemID);
			if (record == null)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Invalid Item ID ",
					this.m_itemID,
					" from Quest ID ",
					this.m_questID,
					". Ignoring for showing iLevel on map."
				}));
				return;
			}
			if (AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gear) && (record.ClassID == 2 || record.ClassID == 3 || record.ClassID == 4 || record.ClassID == 6))
			{
				WrapperItemStats? itemStats = ItemStatCache.instance.GetItemStats(this.m_itemID, this.m_itemContext, this.m_itemInstance);
				if (itemStats != null)
				{
					this.m_quantityArea.gameObject.SetActive(true);
					this.m_quantity.text = StaticDB.GetString("ILVL", null) + " " + itemStats.Value.ItemLevel;
				}
				else
				{
					ItemStatCache instance = ItemStatCache.instance;
					instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats, WrapperItemInstance?>)Delegate.Combine(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats, WrapperItemInstance?>(this.ItemStatsUpdated));
				}
			}
		}

		public void UpdateLootQuality(int itemID, int itemContext, WrapperItemStats stats, WrapperItemInstance? itemInstance)
		{
			if (itemID != this.m_itemID)
			{
				return;
			}
			this.UpdateLootQualityImpl(stats);
			ItemStatCache instance = ItemStatCache.instance;
			instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats, WrapperItemInstance?>)Delegate.Remove(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats, WrapperItemInstance?>(this.UpdateLootQuality));
		}

		private void UpdateLootQualityImpl(WrapperItemStats stats)
		{
			this.m_lootQuality = (ITEM_QUALITY)stats.Quality;
			if (this.m_normalGlow != null)
			{
				if (this.m_lootQuality < ITEM_QUALITY.STANDARD)
				{
					this.m_normalGlow.color = this.WORLD_QUEST_GLOW_COLOR_DEFAULT;
				}
				if (this.m_lootQuality > ITEM_QUALITY.STANDARD)
				{
					string text = "#" + GeneralHelpers.GetItemQualityColor((int)this.m_lootQuality);
					Color color;
					if (ColorUtility.TryParseHtmlString(text, ref color))
					{
						this.m_normalGlow.color = color;
					}
				}
			}
		}

		public void SetQuestID(int questID)
		{
			this.m_questID = questID;
			base.gameObject.name = "WorldQuest " + this.m_questID;
			if (!WorldQuestData.WorldQuestDictionary.ContainsKey(this.m_questID))
			{
				return;
			}
			WrapperWorldQuest wrapperWorldQuest = WorldQuestData.WorldQuestDictionary[this.m_questID];
			if (wrapperWorldQuest.Items == null)
			{
				return;
			}
			this.m_quantityArea.gameObject.SetActive(false);
			bool flag = false;
			foreach (WrapperWorldQuestReward wrapperWorldQuestReward in wrapperWorldQuest.Items)
			{
				ItemRec record = StaticDB.itemDB.GetRecord(wrapperWorldQuestReward.RecordID);
				if (record == null)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Invalid Item ID ",
						wrapperWorldQuestReward.RecordID,
						" from Quest ID ",
						this.m_questID,
						". Ignoring for loot quality check."
					}));
				}
				else
				{
					flag = true;
					if (record.OverallQualityID > (int)this.m_lootQuality)
					{
						this.m_lootQuality = (ITEM_QUALITY)record.OverallQualityID;
					}
					SpellEffectRec spellEffectRec2 = StaticDB.itemEffectDB.GetRecordsByParentID(wrapperWorldQuestReward.RecordID).SelectMany((ItemEffectRec itemEffectRec) => StaticDB.spellEffectDB.GetRecordsByParentID(itemEffectRec.SpellID)).FirstOrDefault((SpellEffectRec spellEffectRec) => spellEffectRec.Effect == 240);
					this.m_main.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, wrapperWorldQuestReward.FileDataID);
					this.m_itemID = wrapperWorldQuestReward.RecordID;
					this.m_itemContext = wrapperWorldQuestReward.ItemContext;
					this.m_itemInstance = wrapperWorldQuestReward.ItemInstance;
					if (!ItemStatCache.instance.HasItemStats(wrapperWorldQuestReward.RecordID))
					{
						ItemStatCache instance = ItemStatCache.instance;
						instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats, WrapperItemInstance?>)Delegate.Combine(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats, WrapperItemInstance?>(this.UpdateLootQuality));
					}
					WrapperItemStats? itemStats = ItemStatCache.instance.GetItemStats(wrapperWorldQuestReward.RecordID, wrapperWorldQuestReward.ItemContext, wrapperWorldQuestReward.ItemInstance);
					if (itemStats != null)
					{
						this.UpdateLootQualityImpl(itemStats.Value);
					}
					this.ShowILVL();
				}
			}
			if (!flag)
			{
				if (wrapperWorldQuest.Currencies.Count > 0)
				{
					foreach (WrapperWorldQuestReward wrapperWorldQuestReward2 in wrapperWorldQuest.Currencies)
					{
						CurrencyTypesRec record2 = StaticDB.currencyTypesDB.GetRecord(wrapperWorldQuestReward2.RecordID);
						if (record2 != null)
						{
							int num = ((record2.Flags & 8u) == 0u) ? 1 : 100;
							this.m_main.sprite = CurrencyContainerDB.LoadCurrencyContainerIcon(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity / num);
							CurrencyContainerRec currencyContainerRec = CurrencyContainerDB.CheckAndGetValidCurrencyContainer(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity / num);
							if (currencyContainerRec != null)
							{
								this.m_lootQuality = (ITEM_QUALITY)currencyContainerRec.ContainerQuality;
							}
						}
						if (AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.OrderResources))
						{
							this.m_quantityArea.gameObject.SetActive(true);
							this.m_quantity.text = wrapperWorldQuestReward2.Quantity.ToString();
						}
					}
				}
				else if (wrapperWorldQuest.Money > 0)
				{
					this.m_main.sprite = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
					if (AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gold))
					{
						this.m_quantityArea.gameObject.SetActive(true);
						this.m_quantity.text = string.Empty + wrapperWorldQuest.Money / 100 / 100;
					}
				}
				else if (wrapperWorldQuest.Experience > 0)
				{
					this.m_main.sprite = GeneralHelpers.GetLocalizedFollowerXpIcon();
				}
			}
			this.m_endTime = wrapperWorldQuest.EndTime;
			int areaID = 0;
			WorldMapAreaRec record3 = StaticDB.worldMapAreaDB.GetRecord(wrapperWorldQuest.WorldMapAreaID);
			if (record3 != null)
			{
				areaID = record3.AreaID;
			}
			this.m_areaID = areaID;
			QuestInfoRec record4 = StaticDB.questInfoDB.GetRecord(wrapperWorldQuest.QuestInfoID);
			if (record4 == null)
			{
				return;
			}
			bool active = (record4.Modifiers & 2) != 0;
			this.m_dragonFrame.gameObject.SetActive(active);
			bool active2 = record4.Type == 12;
			if (this.m_lootQuality < ITEM_QUALITY.STANDARD)
			{
				this.m_normalGlow.color = this.WORLD_QUEST_GLOW_COLOR_DEFAULT;
			}
			if (this.m_lootQuality > ITEM_QUALITY.STANDARD)
			{
				string text = "#" + GeneralHelpers.GetItemQualityColor((int)this.m_lootQuality);
				Color color;
				if (ColorUtility.TryParseHtmlString(text, ref color))
				{
					this.m_normalGlow.color = color;
				}
			}
			if (this.m_assaultEffect != null)
			{
				this.m_assaultEffect.SetActive(active2);
			}
			bool flag2 = (record4.Modifiers & 1) != 0;
			if (flag2 && record4.Type != 3)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-RareQuest");
			}
			bool flag3 = (record4.Modifiers & 4) != 0;
			if (flag3 && record4.Type != 3)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-EpicQuest");
			}
		}

		public void OnClick()
		{
			Main.instance.m_UISound.Play_SelectWorldQuest();
			UiAnimMgr.instance.PlayAnim("MinimapPulseAnim", base.transform, Vector3.zero, 2f, 0f);
			AllPopups.instance.ShowWorldQuestTooltip(this.m_questID);
			StackableMapIcon component = base.GetComponent<StackableMapIcon>();
			if (component != null)
			{
				StackableMapIconContainer container = component.GetContainer();
				AdventureMapPanel.instance.SetSelectedIconContainer(container);
			}
		}

		private void Awake()
		{
			this.m_errorImage.gameObject.SetActive(false);
			this.m_dragonFrame.gameObject.SetActive(false);
			this.m_highlight.gameObject.SetActive(false);
			this.m_expiringSoon.gameObject.SetActive(false);
		}

		private void Update()
		{
			TimeSpan timeSpan = this.m_endTime - GarrisonStatus.CurrentTime();
			bool active = timeSpan.TotalSeconds < 4500.0;
			this.m_expiringSoon.gameObject.SetActive(active);
			if (timeSpan.TotalSeconds <= 0.0)
			{
				StackableMapIcon component = base.gameObject.GetComponent<StackableMapIcon>();
				GameObject gameObject = base.gameObject;
				if (component != null)
				{
					component.RemoveFromContainer();
				}
				if (gameObject != null)
				{
					Object.Destroy(gameObject);
					return;
				}
			}
		}

		public void ForceUpdate()
		{
			this.Update();
		}

		public Image m_errorImage;

		public Image m_dragonFrame;

		public Image m_background;

		public Image m_main;

		public Image m_highlight;

		public Image m_expiringSoon;

		public int m_areaID;

		public GameObject m_zoomScaleRoot;

		public GameObject m_quantityArea;

		public Text m_quantity;

		public Image m_normalGlow;

		public GameObject m_assaultEffect;

		private int m_questID;

		private ITEM_QUALITY m_lootQuality;

		private const int WORLD_QUEST_TIME_LOW_MINUTES = 75;

		private DateTime m_endTime;

		private int m_itemID;

		private int m_itemContext;

		private WrapperItemInstance? m_itemInstance;

		private Color WORLD_QUEST_GLOW_COLOR_DEFAULT = new Color(255f, 210f, 0f);
	}
}
