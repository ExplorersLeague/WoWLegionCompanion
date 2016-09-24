using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class WorldQuestTooltip : MonoBehaviour
{
	private void Start()
	{
		this.m_rewardsLabel.font = GeneralHelpers.LoadStandardFont();
		this.m_rewardsLabel.text = StaticDB.GetString("REWARDS", "Rewards");
		this.m_timeLeftString = StaticDB.GetString("TIME_REMAINING", null);
	}

	public void OnEnable()
	{
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	private void InitRewardInfoDisplay(MobileWorldQuest worldQuest)
	{
		if (worldQuest.Item != null && worldQuest.Item.Count<MobileWorldQuestReward>() > 0)
		{
			MobileWorldQuestReward[] item = worldQuest.Item;
			int num = 0;
			if (num < item.Length)
			{
				MobileWorldQuestReward mobileWorldQuestReward = item[num];
				Sprite rewardSprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, mobileWorldQuestReward.FileDataID);
				this.m_rewardInfo.SetReward(MissionRewardDisplay.RewardType.item, mobileWorldQuestReward.RecordID, mobileWorldQuestReward.Quantity, rewardSprite, mobileWorldQuestReward.ItemContext);
			}
		}
		else if (worldQuest.Currency.Count<MobileWorldQuestReward>() > 0)
		{
			MobileWorldQuestReward[] currency = worldQuest.Currency;
			int num2 = 0;
			if (num2 < currency.Length)
			{
				MobileWorldQuestReward mobileWorldQuestReward2 = currency[num2];
				Sprite iconSprite = GeneralHelpers.LoadCurrencyIcon(mobileWorldQuestReward2.RecordID);
				CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(mobileWorldQuestReward2.RecordID);
				int quantity = mobileWorldQuestReward2.Quantity / (((record.Flags & 8u) == 0u) ? 1 : 100);
				this.m_rewardInfo.SetCurrency(mobileWorldQuestReward2.RecordID, quantity, iconSprite);
			}
		}
		else if (worldQuest.Money > 0)
		{
			Sprite iconSprite2 = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
			this.m_rewardInfo.SetGold(worldQuest.Money / 10000, iconSprite2);
		}
		else if (worldQuest.Experience > 0)
		{
			Sprite localizedFollowerXpIcon = GeneralHelpers.GetLocalizedFollowerXpIcon();
			this.m_rewardInfo.SetFollowerXP(worldQuest.Experience, localizedFollowerXpIcon);
		}
	}

	public void SetQuest(int questID)
	{
		this.m_expiringSoon.gameObject.SetActive(false);
		this.m_questID = questID;
		Transform[] componentsInChildren = this.m_worldQuestObjectiveRoot.GetComponentsInChildren<Transform>(true);
		foreach (Transform transform in componentsInChildren)
		{
			if (transform != null && transform != this.m_worldQuestObjectiveRoot.transform)
			{
				Object.DestroyImmediate(transform.gameObject);
			}
		}
		MobileWorldQuest mobileWorldQuest = (MobileWorldQuest)WorldQuestData.worldQuestDictionary[this.m_questID];
		this.m_worldQuestNameText.text = mobileWorldQuest.QuestTitle;
		foreach (MobileWorldQuestObjective mobileWorldQuestObjective in mobileWorldQuest.Objective.AsEnumerable<MobileWorldQuestObjective>())
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_worldQuestObjectiveDisplayPrefab);
			gameObject.transform.SetParent(this.m_worldQuestObjectiveRoot.transform, false);
			Text component = gameObject.GetComponent<Text>();
			component.text = "-" + mobileWorldQuestObjective.Text;
		}
		this.InitRewardInfoDisplay(mobileWorldQuest);
		this.m_endTime = (long)(mobileWorldQuest.EndTime - 900);
		QuestInfoRec record = StaticDB.questInfoDB.GetRecord(mobileWorldQuest.QuestInfoID);
		if (record == null)
		{
			return;
		}
		bool active = (record.Modifiers & 2) != 0;
		this.m_dragonFrame.gameObject.SetActive(active);
		this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-NormalQuest");
		bool flag = (record.Modifiers & 1) != 0;
		if (flag && record.Type != 3)
		{
			this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-RareQuest");
		}
		bool flag2 = (record.Modifiers & 4) != 0;
		if (flag2 && record.Type != 3)
		{
			this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-EpicQuest");
		}
		int uitextureAtlasMemberID;
		string text;
		switch (record.Type)
		{
		case 1:
		{
			int profession = record.Profession;
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
			goto IL_4BB;
		}
		case 3:
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-pvp-ffa");
			text = "Mobile-PVP";
			goto IL_4BB;
		case 4:
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-petbattle");
			text = "Mobile-Pets";
			goto IL_4BB;
		}
		uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-questmarker-questbang");
		text = "Mobile-QuestExclamationIcon";
		IL_4BB:
		if (text != null)
		{
			this.m_main.sprite = Resources.Load<Sprite>("NewWorldQuest/" + text);
		}
		else if (uitextureAtlasMemberID > 0)
		{
			this.m_main.sprite = TextureAtlas.instance.GetAtlasSprite(uitextureAtlasMemberID);
			this.m_main.SetNativeSize();
		}
		this.UpdateTimeRemaining();
	}

	private void UpdateTimeRemaining()
	{
		int num = (int)(this.m_endTime - GarrisonStatus.CurrentTime());
		if (num < 0)
		{
			num = 0;
		}
		Duration duration = new Duration(num, false);
		this.m_worldQuestTimeText.text = this.m_timeLeftString + " " + duration.DurationString;
		bool active = num < 4500;
		this.m_expiringSoon.gameObject.SetActive(active);
	}

	private void Update()
	{
		this.UpdateTimeRemaining();
	}

	private const int WORLD_QUEST_TIME_LOW_MINUTES = 75;

	[Header("World Quest Icon Layers")]
	public Image m_dragonFrame;

	public Image m_background;

	public Image m_main;

	public Image m_expiringSoon;

	[Header("World Quest Info")]
	public Text m_worldQuestNameText;

	public Text m_worldQuestTimeText;

	public MissionRewardDisplay m_missionRewardDisplayPrefab;

	public GameObject m_worldQuestObjectiveRoot;

	public GameObject m_worldQuestObjectiveDisplayPrefab;

	[Header("Misc")]
	public RewardInfoPopup m_rewardInfo;

	public Text m_rewardsLabel;

	private int m_questID;

	private long m_endTime;

	private string m_timeLeftString;
}
