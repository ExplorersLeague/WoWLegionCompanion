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
			this.m_showLootIconInsteadOfMain = true;
		}

		private void OnDisable()
		{
			AdventureMapPanel instance = AdventureMapPanel.instance;
			instance.TestIconSizeChanged = (Action<float>)Delegate.Remove(instance.TestIconSizeChanged, new Action<float>(this.OnTestIconSizeChanged));
			PinchZoomContentManager pinchZoomContentManager = AdventureMapPanel.instance.m_pinchZoomContentManager;
			pinchZoomContentManager.ZoomFactorChanged = (Action<bool>)Delegate.Remove(pinchZoomContentManager.ZoomFactorChanged, new Action<bool>(this.HandleZoomChanged));
		}

		private void ItemStatsUpdated(int itemID, int itemContext, WrapperItemStats itemStats)
		{
			if (this.m_itemID == itemID && this.m_itemContext == itemContext)
			{
				ItemStatCache instance = ItemStatCache.instance;
				instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats>)Delegate.Remove(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats>(this.ItemStatsUpdated));
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
				WrapperItemStats? itemStats = ItemStatCache.instance.GetItemStats(this.m_itemID, this.m_itemContext);
				if (itemStats != null)
				{
					this.m_quantityArea.gameObject.SetActive(true);
					this.m_quantity.text = StaticDB.GetString("ILVL", null) + " " + itemStats.Value.ItemLevel;
				}
				else
				{
					ItemStatCache instance = ItemStatCache.instance;
					instance.ItemStatCacheUpdateAction = (Action<int, int, WrapperItemStats>)Delegate.Combine(instance.ItemStatCacheUpdateAction, new Action<int, int, WrapperItemStats>(this.ItemStatsUpdated));
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
					if (this.m_showLootIconInsteadOfMain)
					{
						SpellEffectRec spellEffectRec2 = StaticDB.itemEffectDB.GetRecordsByParentID(wrapperWorldQuestReward.RecordID).SelectMany((ItemEffectRec itemEffectRec) => StaticDB.spellEffectDB.GetRecordsByParentID(itemEffectRec.SpellID)).FirstOrDefault((SpellEffectRec spellEffectRec) => spellEffectRec.Effect == 240);
						this.m_main.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, wrapperWorldQuestReward.FileDataID);
						this.m_itemID = wrapperWorldQuestReward.RecordID;
						this.m_itemContext = wrapperWorldQuestReward.ItemContext;
						this.ShowILVL();
					}
				}
			}
			if (!flag && this.m_showLootIconInsteadOfMain)
			{
				if (wrapperWorldQuest.Currencies.Count > 0)
				{
					foreach (WrapperWorldQuestReward wrapperWorldQuestReward2 in wrapperWorldQuest.Currencies)
					{
						CurrencyTypesRec record2 = StaticDB.currencyTypesDB.GetRecord(wrapperWorldQuestReward2.RecordID);
						if (record2 != null)
						{
							this.m_main.sprite = CurrencyContainerDB.LoadCurrencyContainerIcon(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity);
							CurrencyContainerRec currencyContainerRec = CurrencyContainerDB.CheckAndGetValidCurrencyContainer(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity);
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
			bool flag2 = record4.Type == 7;
			this.m_normalGlow.gameObject.SetActive(!flag2);
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
			this.m_legionAssaultGlow.gameObject.SetActive(flag2);
			bool flag3 = (record4.Modifiers & 1) != 0;
			if (flag3 && record4.Type != 3)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-RareQuest");
			}
			bool flag4 = (record4.Modifiers & 4) != 0;
			if (flag4 && record4.Type != 3)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-EpicQuest");
			}
			int uitextureAtlasMemberID;
			string text2;
			switch (record4.Type)
			{
			case 1:
			{
				int profession = record4.Profession;
				switch (profession)
				{
				case 182:
					uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-herbalism");
					text2 = "Mobile-Herbalism";
					break;
				default:
					if (profession != 164)
					{
						if (profession != 165)
						{
							if (profession != 129)
							{
								if (profession != 171)
								{
									if (profession != 197)
									{
										if (profession != 202)
										{
											if (profession != 333)
											{
												if (profession != 356)
												{
													if (profession != 393)
													{
														if (profession != 755)
														{
															if (profession != 773)
															{
																if (profession != 794)
																{
																	uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-questmarker-questbang");
																	text2 = "Mobile-QuestExclamationIcon";
																}
																else
																{
																	uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-archaeology");
																	text2 = "Mobile-Archaeology";
																}
															}
															else
															{
																uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-inscription");
																text2 = "Mobile-Inscription";
															}
														}
														else
														{
															uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-jewelcrafting");
															text2 = "Mobile-Jewelcrafting";
														}
													}
													else
													{
														uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-skinning");
														text2 = "Mobile-Skinning";
													}
												}
												else
												{
													uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-fishing");
													text2 = "Mobile-Fishing";
												}
											}
											else
											{
												uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-enchanting");
												text2 = "Mobile-Enchanting";
											}
										}
										else
										{
											uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-engineering");
											text2 = "Mobile-Engineering";
										}
									}
									else
									{
										uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-tailoring");
										text2 = "Mobile-Tailoring";
									}
								}
								else
								{
									uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-alchemy");
									text2 = "Mobile-Alchemy";
								}
							}
							else
							{
								uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-firstaid");
								text2 = "Mobile-FirstAid";
							}
						}
						else
						{
							uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-leatherworking");
							text2 = "Mobile-Leatherworking";
						}
					}
					else
					{
						uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-blacksmithing");
						text2 = "Mobile-Blacksmithing";
					}
					break;
				case 185:
					uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-cooking");
					text2 = "Mobile-Cooking";
					break;
				case 186:
					uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-mining");
					text2 = "Mobile-Mining";
					break;
				}
				goto IL_788;
			}
			case 3:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-pvp-ffa");
				text2 = "Mobile-PVP";
				goto IL_788;
			case 4:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-petbattle");
				text2 = "Mobile-Pets";
				goto IL_788;
			}
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-questmarker-questbang");
			text2 = "Mobile-QuestExclamationIcon";
			IL_788:
			if (!this.m_showLootIconInsteadOfMain)
			{
				if (text2 != null)
				{
					this.m_main.sprite = Resources.Load<Sprite>("NewWorldQuest/" + text2);
				}
				else if (uitextureAtlasMemberID > 0)
				{
					this.m_main.sprite = TextureAtlas.instance.GetAtlasSprite(uitextureAtlasMemberID);
					this.m_main.SetNativeSize();
				}
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

		public Image m_legionAssaultGlow;

		private int m_questID;

		private ITEM_QUALITY m_lootQuality;

		private const int WORLD_QUEST_TIME_LOW_MINUTES = 75;

		private DateTime m_endTime;

		private int m_itemID;

		private int m_itemContext;

		private Color WORLD_QUEST_GLOW_COLOR_DEFAULT = new Color(255f, 210f, 0f);

		public bool m_showLootIconInsteadOfMain;
	}
}
