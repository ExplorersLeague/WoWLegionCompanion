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
		pinchZoomContentManager.ZoomFactorChanged = (Action)Delegate.Combine(pinchZoomContentManager.ZoomFactorChanged, new Action(this.HandleZoomChanged));
		this.m_showLootIconInsteadOfMain = true;
	}

	private void OnDisable()
	{
		AdventureMapPanel instance = AdventureMapPanel.instance;
		instance.TestIconSizeChanged = (Action<float>)Delegate.Remove(instance.TestIconSizeChanged, new Action<float>(this.OnTestIconSizeChanged));
		PinchZoomContentManager pinchZoomContentManager = AdventureMapPanel.instance.m_pinchZoomContentManager;
		pinchZoomContentManager.ZoomFactorChanged = (Action)Delegate.Remove(pinchZoomContentManager.ZoomFactorChanged, new Action(this.HandleZoomChanged));
	}

	private void OnTestIconSizeChanged(float newScale)
	{
		base.transform.localScale = Vector3.one * newScale;
	}

	private void HandleZoomChanged()
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

	public void SetQuestID(int questID)
	{
		this.m_questID = questID;
		base.gameObject.name = "WorldQuest " + this.m_questID;
		MobileWorldQuest mobileWorldQuest = (MobileWorldQuest)WorldQuestData.worldQuestDictionary[this.m_questID];
		if (mobileWorldQuest == null || mobileWorldQuest.Item == null)
		{
			return;
		}
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
				if (record.OverallQualityID > (int)this.m_lootQuality)
				{
					this.m_lootQuality = (ITEM_QUALITY)record.OverallQualityID;
				}
				if (this.m_showLootIconInsteadOfMain)
				{
					this.m_main.sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, mobileWorldQuestReward.FileDataID);
				}
			}
		}
		if (this.m_showLootIconInsteadOfMain)
		{
			if (mobileWorldQuest.Money > 0)
			{
				this.m_main.sprite = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
			}
			if (mobileWorldQuest.Experience > 0)
			{
				this.m_main.sprite = GeneralHelpers.GetLocalizedFollowerXpIcon();
			}
			foreach (MobileWorldQuestReward mobileWorldQuestReward2 in mobileWorldQuest.Currency)
			{
				CurrencyTypesRec record2 = StaticDB.currencyTypesDB.GetRecord(mobileWorldQuestReward2.RecordID);
				if (record2 != null)
				{
					this.m_main.sprite = GeneralHelpers.LoadCurrencyIcon(mobileWorldQuestReward2.RecordID);
				}
			}
		}
		this.m_endTime = (long)mobileWorldQuest.EndTime;
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
		bool flag = (record4.Modifiers & 1) != 0;
		if (flag && record4.Type != 3)
		{
			this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-RareQuest");
		}
		bool flag2 = (record4.Modifiers & 4) != 0;
		if (flag2 && record4.Type != 3)
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
			goto IL_55B;
		}
		case 3:
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-pvp-ffa");
			text = "Mobile-PVP";
			goto IL_55B;
		case 4:
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-petbattle");
			text = "Mobile-Pets";
			goto IL_55B;
		}
		uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-questmarker-questbang");
		text = "Mobile-QuestExclamationIcon";
		IL_55B:
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
			Object.DestroyImmediate(base.gameObject);
			return;
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

	private int m_questID;

	private ITEM_QUALITY m_lootQuality;

	private long m_endTime;

	public bool m_showLootIconInsteadOfMain;
}
