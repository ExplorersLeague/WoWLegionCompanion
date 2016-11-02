using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

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

	private void ItemStatsUpdated(int itemID, int itemContext, MobileItemStats itemStats)
	{
		if (this.m_itemID == itemID && this.m_itemContext == itemContext)
		{
			ItemStatCache instance = ItemStatCache.instance;
			instance.ItemStatCacheUpdateAction = (Action<int, int, MobileItemStats>)Delegate.Remove(instance.ItemStatCacheUpdateAction, new Action<int, int, MobileItemStats>(this.ItemStatsUpdated));
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
			MobileItemStats itemStats = ItemStatCache.instance.GetItemStats(this.m_itemID, this.m_itemContext);
			if (itemStats != null)
			{
				this.m_quantityArea.gameObject.SetActive(true);
				this.m_quantity.text = StaticDB.GetString("ILVL", null) + " " + itemStats.ItemLevel;
			}
			else
			{
				ItemStatCache instance = ItemStatCache.instance;
				instance.ItemStatCacheUpdateAction = (Action<int, int, MobileItemStats>)Delegate.Combine(instance.ItemStatCacheUpdateAction, new Action<int, int, MobileItemStats>(this.ItemStatsUpdated));
			}
		}
	}

	public void SetQuestID(int questID)
	{
		this.m_questID = questID;
		base.gameObject.name = "WorldQuest " + this.m_questID;
		MobileWorldQuest mobileWorldQuest = (MobileWorldQuest)WorldQuestData.worldQuestDictionary[this.m_questID];
		if (mobileWorldQuest == null || mobileWorldQuest.Item == null)
		{
			return;
		}
		this.m_quantityArea.gameObject.SetActive(false);
		bool flag = false;
		foreach (MobileWorldQuestReward mobileWorldQuestReward in mobileWorldQuest.Item)
		{
			ItemRec record = StaticDB.itemDB.GetRecord(mobileWorldQuestReward.RecordID);
			if (record == null)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Invalid Item ID ",
					mobileWorldQuestReward.RecordID,
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
					bool isArtifactXP = false;
					int quantity = 0;
					StaticDB.itemEffectDB.EnumRecordsByParentID(mobileWorldQuestReward.RecordID, delegate(ItemEffectRec itemEffectRec)
					{
						StaticDB.spellEffectDB.EnumRecordsByParentID(itemEffectRec.SpellID, delegate(SpellEffectRec spellEffectRec)
						{
							if (spellEffectRec.Effect == 240)
							{
								isArtifactXP = true;
								quantity = GeneralHelpers.ApplyArtifactXPMultiplier(spellEffectRec.EffectBasePoints, GarrisonStatus.ArtifactXpMultiplier);
								return false;
							}
							return true;
						});
						return !isArtifactXP;
					});
					if (isArtifactXP)
					{
						this.m_main.sprite = Resources.Load<Sprite>("WorldMap/INV_Artifact_XP02");
						if (AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.ArtifactPower))
						{
							this.m_quantityArea.gameObject.SetActive(true);
							this.m_quantity.text = quantity.ToString();
						}
					}
					else
					{
						this.m_main.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, mobileWorldQuestReward.FileDataID);
						this.m_itemID = mobileWorldQuestReward.RecordID;
						this.m_itemContext = mobileWorldQuestReward.ItemContext;
						this.ShowILVL();
					}
				}
			}
		}
		if (!flag && this.m_showLootIconInsteadOfMain)
		{
			if (mobileWorldQuest.Currency.GetLength(0) > 0)
			{
				foreach (MobileWorldQuestReward mobileWorldQuestReward2 in mobileWorldQuest.Currency)
				{
					CurrencyTypesRec record2 = StaticDB.currencyTypesDB.GetRecord(mobileWorldQuestReward2.RecordID);
					if (record2 != null)
					{
						this.m_main.sprite = GeneralHelpers.LoadCurrencyIcon(mobileWorldQuestReward2.RecordID);
					}
					if (AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.OrderResources))
					{
						this.m_quantityArea.gameObject.SetActive(true);
						this.m_quantity.text = mobileWorldQuestReward2.Quantity.ToString();
					}
				}
			}
			else if (mobileWorldQuest.Money > 0)
			{
				this.m_main.sprite = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
				if (AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gold))
				{
					this.m_quantityArea.gameObject.SetActive(true);
					this.m_quantity.text = string.Empty + mobileWorldQuest.Money / 100 / 100;
				}
			}
			else if (mobileWorldQuest.Experience > 0)
			{
				this.m_main.sprite = GeneralHelpers.GetLocalizedFollowerXpIcon();
			}
		}
		this.m_endTime = (long)(mobileWorldQuest.EndTime - 900);
		int areaID = 0;
		WorldMapAreaRec record3 = StaticDB.worldMapAreaDB.GetRecord(mobileWorldQuest.WorldMapAreaID);
		if (record3 != null)
		{
			areaID = record3.AreaID;
		}
		this.m_areaID = areaID;
		QuestInfoRec record4 = StaticDB.questInfoDB.GetRecord(mobileWorldQuest.QuestInfoID);
		if (record4 == null)
		{
			return;
		}
		bool active = (record4.Modifiers & 2) != 0;
		this.m_dragonFrame.gameObject.SetActive(active);
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
		int uitextureAtlasMemberID;
		string text;
		switch (record4.Type)
		{
		case 1:
		{
			int profession = record4.Profession;
			switch (profession)
			{
			case 182:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-herbalism");
				text = "Mobile-Herbalism";
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
																text = "Mobile-QuestExclamationIcon";
															}
															else
															{
																uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-archaeology");
																text = "Mobile-Archaeology";
															}
														}
														else
														{
															uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-inscription");
															text = "Mobile-Inscription";
														}
													}
													else
													{
														uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-jewelcrafting");
														text = "Mobile-Jewelcrafting";
													}
												}
												else
												{
													uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-skinning");
													text = "Mobile-Skinning";
												}
											}
											else
											{
												uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-fishing");
												text = "Mobile-Fishing";
											}
										}
										else
										{
											uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-enchanting");
											text = "Mobile-Enchanting";
										}
									}
									else
									{
										uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-engineering");
										text = "Mobile-Engineering";
									}
								}
								else
								{
									uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-tailoring");
									text = "Mobile-Tailoring";
								}
							}
							else
							{
								uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-alchemy");
								text = "Mobile-Alchemy";
							}
						}
						else
						{
							uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-firstaid");
							text = "Mobile-FirstAid";
						}
					}
					else
					{
						uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-leatherworking");
						text = "Mobile-Leatherworking";
					}
				}
				else
				{
					uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-blacksmithing");
					text = "Mobile-Blacksmithing";
				}
				break;
			case 185:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-cooking");
				text = "Mobile-Cooking";
				break;
			case 186:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-mining");
				text = "Mobile-Mining";
				break;
			}
			goto IL_6D0;
		}
		case 3:
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-pvp-ffa");
			text = "Mobile-PVP";
			goto IL_6D0;
		case 4:
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-petbattle");
			text = "Mobile-Pets";
			goto IL_6D0;
		}
		uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-questmarker-questbang");
		text = "Mobile-QuestExclamationIcon";
		IL_6D0:
		if (!this.m_showLootIconInsteadOfMain)
		{
			if (text != null)
			{
				this.m_main.sprite = Resources.Load<Sprite>("NewWorldQuest/" + text);
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
		long num = this.m_endTime - GarrisonStatus.CurrentTime();
		bool active = num < 4500L;
		this.m_expiringSoon.gameObject.SetActive(active);
		if (num <= 0L)
		{
			StackableMapIcon component = base.gameObject.GetComponent<StackableMapIcon>();
			GameObject gameObject = base.gameObject;
			if (component != null)
			{
				component.RemoveFromContainer();
			}
			if (gameObject != null)
			{
				Object.DestroyImmediate(gameObject);
				return;
			}
		}
	}

	private const int WORLD_QUEST_TIME_LOW_MINUTES = 75;

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

	private int m_questID;

	private ITEM_QUALITY m_lootQuality;

	private long m_endTime;

	private int m_itemID;

	private int m_itemContext;

	public bool m_showLootIconInsteadOfMain;
}
