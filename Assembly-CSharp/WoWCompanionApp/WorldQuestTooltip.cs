using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class WorldQuestTooltip : MonoBehaviour
	{
		private void Start()
		{
			this.m_rewardsLabel.font = FontLoader.LoadStandardFont();
			this.m_rewardsLabel.text = StaticDB.GetString("REWARDS", "Rewards");
			this.m_timeLeftString = StaticDB.GetString("TIME_LEFT", "Time Left: PH");
		}

		public void OnEnable()
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void EnableAdditionalRewardDisplays(int highestActiveIndex)
		{
			this.m_rewardInfo[1].gameObject.SetActive(highestActiveIndex >= 1);
			this.m_rewardInfo[2].gameObject.SetActive(highestActiveIndex >= 2);
		}

		private void InitRewardInfoDisplay(WrapperWorldQuest worldQuest)
		{
			int num = 0;
			this.m_rewardInfo[0].gameObject.SetActive(true);
			this.m_rewardInfo[1].gameObject.SetActive(false);
			this.m_rewardInfo[2].gameObject.SetActive(false);
			if (worldQuest.Items != null && worldQuest.Items.Count<WrapperWorldQuestReward>() > 0)
			{
				foreach (WrapperWorldQuestReward wrapperWorldQuestReward in worldQuest.Items)
				{
					Sprite rewardSprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, wrapperWorldQuestReward.FileDataID);
					this.m_rewardInfo[num].SetReward(MissionRewardDisplay.RewardType.item, wrapperWorldQuestReward.RecordID, wrapperWorldQuestReward.Quantity, rewardSprite, wrapperWorldQuestReward.ItemContext);
					this.EnableAdditionalRewardDisplays(num++);
					if (num >= 3)
					{
						break;
					}
				}
			}
			else if (worldQuest.Currencies.Count<WrapperWorldQuestReward>() > 0)
			{
				foreach (WrapperWorldQuestReward wrapperWorldQuestReward2 in worldQuest.Currencies)
				{
					CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(wrapperWorldQuestReward2.RecordID);
					CurrencyContainerRec currencyContainerRec = CurrencyContainerDB.CheckAndGetValidCurrencyContainer(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity);
					if (currencyContainerRec != null)
					{
						Sprite iconSprite = CurrencyContainerDB.LoadCurrencyContainerIcon(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity);
						int quantity = wrapperWorldQuestReward2.Quantity / (((record.Flags & 8u) == 0u) ? 1 : 100);
						this.m_rewardInfo[num].SetCurrency(wrapperWorldQuestReward2.RecordID, quantity, iconSprite);
						this.EnableAdditionalRewardDisplays(num++);
						if (num >= 3)
						{
							break;
						}
					}
					else
					{
						Sprite iconSprite = GeneralHelpers.LoadCurrencyIcon(wrapperWorldQuestReward2.RecordID);
						int quantity = wrapperWorldQuestReward2.Quantity / (((record.Flags & 8u) == 0u) ? 1 : 100);
						this.m_rewardInfo[num].SetCurrency(wrapperWorldQuestReward2.RecordID, quantity, iconSprite);
						this.EnableAdditionalRewardDisplays(num++);
						if (num >= 3)
						{
							break;
						}
					}
				}
			}
			else if (worldQuest.Money > 0)
			{
				Sprite iconSprite2 = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
				this.m_rewardInfo[num].SetGold(worldQuest.Money / 10000, iconSprite2);
				this.EnableAdditionalRewardDisplays(num++);
				if (num >= 3)
				{
					return;
				}
			}
			else if (worldQuest.Experience > 0)
			{
				Sprite localizedFollowerXpIcon = GeneralHelpers.GetLocalizedFollowerXpIcon();
				this.m_rewardInfo[num].SetFollowerXP(worldQuest.Experience, localizedFollowerXpIcon);
				this.EnableAdditionalRewardDisplays(num++);
				if (num >= 3)
				{
					return;
				}
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
					transform.SetParent(null);
					Object.Destroy(transform.gameObject);
				}
			}
			WrapperWorldQuest worldQuest = WorldQuestData.WorldQuestDictionary[this.m_questID];
			this.m_worldQuestNameText.text = worldQuest.QuestTitle;
			BountySite[] componentsInChildren2 = this.m_bountyLogoRoot.transform.GetComponentsInChildren<BountySite>(true);
			foreach (BountySite bountySite in componentsInChildren2)
			{
				bountySite.transform.SetParent(null);
				Object.Destroy(bountySite.gameObject);
			}
			int num = 0;
			if (PersistentBountyData.bountiesByWorldQuestDictionary.ContainsKey(worldQuest.QuestID))
			{
				WrapperBountiesByWorldQuest wrapperBountiesByWorldQuest = PersistentBountyData.bountiesByWorldQuestDictionary[worldQuest.QuestID];
				for (int k = 0; k < wrapperBountiesByWorldQuest.BountyQuestIDs.Count; k++)
				{
					foreach (WrapperWorldQuestBounty bounty in PersistentBountyData.bountyDictionary.Values)
					{
						if (wrapperBountiesByWorldQuest.BountyQuestIDs[k] == bounty.QuestID)
						{
							QuestV2Rec record = StaticDB.questDB.GetRecord(bounty.QuestID);
							if (record != null)
							{
								GameObject gameObject = Object.Instantiate<GameObject>(this.m_worldQuestObjectiveDisplayPrefab);
								gameObject.transform.SetParent(this.m_worldQuestObjectiveRoot.transform, false);
								Text component = gameObject.GetComponent<Text>();
								component.text = record.QuestTitle;
								component.color = new Color(1f, 0.773f, 0f, 1f);
								BountySite bountySite2 = Object.Instantiate<BountySite>(this.m_bountyLogoPrefab);
								bountySite2.SetBounty(bounty);
								bountySite2.transform.SetParent(this.m_bountyLogoRoot.transform, false);
								num++;
							}
						}
					}
				}
			}
			this.EnableBountyFiligree(num);
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_worldQuestObjectiveDisplayPrefab);
			gameObject2.transform.SetParent(this.m_worldQuestObjectiveRoot.transform, false);
			this.m_worldQuestTimeText = gameObject2.GetComponent<Text>();
			this.m_worldQuestTimeText.text = worldQuest.QuestTitle;
			this.m_worldQuestTimeText.color = new Color(1f, 0.773f, 0f, 1f);
			foreach (WrapperWorldQuestObjective wrapperWorldQuestObjective in worldQuest.Objectives)
			{
				GameObject gameObject3 = Object.Instantiate<GameObject>(this.m_worldQuestObjectiveDisplayPrefab);
				gameObject3.transform.SetParent(this.m_worldQuestObjectiveRoot.transform, false);
				Text component2 = gameObject3.GetComponent<Text>();
				component2.text = "- " + wrapperWorldQuestObjective.Text;
			}
			this.InitRewardInfoDisplay(worldQuest);
			this.m_endTime = worldQuest.EndTime;
			QuestInfoRec record2 = StaticDB.questInfoDB.GetRecord(worldQuest.QuestInfoID);
			if (record2 == null)
			{
				return;
			}
			bool active = (record2.Modifiers & 2) != 0;
			this.m_dragonFrame.gameObject.SetActive(active);
			if (record2.Type == 7)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-NormalQuest");
				this.m_main.sprite = Resources.Load<Sprite>("NewWorldQuest/Map-LegionInvasion-SargerasCrest");
				return;
			}
			this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-NormalQuest");
			bool flag = (record2.Modifiers & 1) != 0;
			if (flag && record2.Type != 3)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-RareQuest");
			}
			bool flag2 = (record2.Modifiers & 4) != 0;
			if (flag2 && record2.Type != 3)
			{
				this.m_background.sprite = Resources.Load<Sprite>("NewWorldQuest/Mobile-EpicQuest");
			}
			int uitextureAtlasMemberID;
			string text;
			switch (record2.Type)
			{
			case 1:
			{
				int profession = record2.Profession;
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
				goto IL_6F5;
			}
			case 3:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-pvp-ffa");
				text = "Mobile-PVP";
				goto IL_6F5;
			case 4:
				uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-icon-petbattle");
				text = "Mobile-Pets";
				goto IL_6F5;
			}
			uitextureAtlasMemberID = TextureAtlas.GetUITextureAtlasMemberID("worldquest-questmarker-questbang");
			text = "Mobile-QuestExclamationIcon";
			IL_6F5:
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

		private void EnableBountyFiligree(int activeBounties)
		{
			this.m_singleEmissaryFiligree.SetActive(activeBounties == 1);
			this.m_doubleEmissaryFiligree.SetActive(activeBounties == 2);
		}

		private void UpdateTimeRemaining()
		{
			TimeSpan timeSpan = this.m_endTime - GarrisonStatus.CurrentTime();
			if (timeSpan.TotalSeconds < 0.0)
			{
				timeSpan = TimeSpan.Zero;
			}
			this.m_worldQuestTimeText.text = this.m_timeLeftString + " " + timeSpan.GetDurationString(false);
			bool active = timeSpan.TotalSeconds < 4500.0;
			this.m_expiringSoon.gameObject.SetActive(active);
		}

		private void Update()
		{
			this.UpdateTimeRemaining();
		}

		[Header("World Quest Icon Layers")]
		public Image m_dragonFrame;

		public Image m_background;

		public Image m_main;

		public Image m_expiringSoon;

		[Header("World Quest Info")]
		public Text m_worldQuestNameText;

		private Text m_worldQuestTimeText;

		public MissionRewardDisplay m_missionRewardDisplayPrefab;

		public GameObject m_worldQuestObjectiveRoot;

		public GameObject m_worldQuestObjectiveDisplayPrefab;

		public GameObject m_bountyLogoRoot;

		public BountySite m_bountyLogoPrefab;

		public GameObject m_singleEmissaryFiligree;

		public GameObject m_doubleEmissaryFiligree;

		[Header("Misc")]
		public RewardInfoPopup[] m_rewardInfo;

		public Text m_rewardsLabel;

		private int m_questID;

		private const int WORLD_QUEST_TIME_LOW_MINUTES = 75;

		private DateTime m_endTime;

		private string m_timeLeftString;
	}
}
