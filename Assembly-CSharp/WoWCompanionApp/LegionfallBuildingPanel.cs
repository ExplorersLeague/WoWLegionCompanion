using System;
using UnityEngine;
using UnityEngine.UI;
using WowStaticData;

namespace WoWCompanionApp
{
	public class LegionfallBuildingPanel : MonoBehaviour
	{
		private void OnEnable()
		{
			Main instance = Main.instance;
			instance.MakeContributionRequestInitiatedAction = (Action)Delegate.Combine(instance.MakeContributionRequestInitiatedAction, new Action(this.HandleMakeContributionRequestInitiated));
			Main instance2 = Main.instance;
			instance2.ContributionInfoChangedAction = (Action)Delegate.Combine(instance2.ContributionInfoChangedAction, new Action(this.HandleContributionInfoChanged));
			Main instance3 = Main.instance;
			instance3.GotItemFromQuestCompletionAction = (Action<int, int, int>)Delegate.Combine(instance3.GotItemFromQuestCompletionAction, new Action<int, int, int>(this.HandleGotItemFromQuestCompletion));
		}

		private void OnDisable()
		{
			Main instance = Main.instance;
			instance.MakeContributionRequestInitiatedAction = (Action)Delegate.Remove(instance.MakeContributionRequestInitiatedAction, new Action(this.HandleMakeContributionRequestInitiated));
			Main instance2 = Main.instance;
			instance2.ContributionInfoChangedAction = (Action)Delegate.Remove(instance2.ContributionInfoChangedAction, new Action(this.HandleContributionInfoChanged));
			Main instance3 = Main.instance;
			instance3.GotItemFromQuestCompletionAction = (Action<int, int, int>)Delegate.Remove(instance3.GotItemFromQuestCompletionAction, new Action<int, int, int>(this.HandleGotItemFromQuestCompletion));
		}

		public void InitPanel(int contributionID, int questID)
		{
			if (this.m_grayscaleShader != null)
			{
				Material material = new Material(this.m_grayscaleShader);
				this.m_buildingTitleBanner.material = material;
			}
			this.m_questID = questID;
			this.m_contributionID = contributionID;
			this.m_buildingName.font = GeneralHelpers.LoadFancyFont();
			this.m_buildingState.font = GeneralHelpers.LoadStandardFont();
			this.m_buildingDescription.font = GeneralHelpers.LoadStandardFont();
			this.m_costLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_cost.font = GeneralHelpers.LoadStandardFont();
			this.m_contributeButtonLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_cantContributeText.font = GeneralHelpers.LoadStandardFont();
			this.m_cantContributeText.text = StaticDB.GetString("CANT_CONTRIBUTE", "You can only contribute when the building is under construction.");
			this.m_healthText.font = GeneralHelpers.LoadStandardFont();
			this.m_gotLootLabel.font = GeneralHelpers.LoadStandardFont();
			this.m_gotLootLabel.text = StaticDB.GetString("YOU_RECEIVED_ITEM", "You received item: (PH)");
			this.m_gotLootItemName.font = GeneralHelpers.LoadStandardFont();
			this.m_gotLootArea.SetActive(false);
			if (!LegionfallData.legionfallDictionary.ContainsKey(contributionID))
			{
				return;
			}
			WrapperContribution contribution = LegionfallData.legionfallDictionary[contributionID].contribution;
			this.m_buildingStateImageFrame_building.gameObject.SetActive(false);
			this.m_buildingStateImageFrame_active.gameObject.SetActive(false);
			this.m_buildingStateImageFrame_underAttack.gameObject.SetActive(false);
			this.m_buildingStateImageFrame_destroyed.gameObject.SetActive(false);
			this.m_contributeArea.SetActive(false);
			this.m_cantContributeArea.SetActive(true);
			this.m_healthText.gameObject.SetActive(true);
			this.m_buildingTitleBanner.material.SetFloat("_GrayscaleAmount", (contribution.State != 4) ? 0f : 1f);
			this.m_buildingTitleBanner.color = ((contribution.State != 4) ? Color.white : Color.gray);
			this.m_progressFillGlow.gameObject.SetActive(false);
			int num;
			switch (contribution.State)
			{
			case 1:
				this.m_progressFillGlow.gameObject.SetActive(true);
				num = contribution.UitextureAtlasMemberIDUnderConstruction;
				this.m_buildingState.text = StaticDB.GetString("UNDER_CONSTRUCTION", "Under Construction (PH)");
				this.m_buildingState.color = new Color(1f, 0.8235f, 0f, 1f);
				this.m_buildingStateImageFrame_building.gameObject.SetActive(true);
				this.m_contributeArea.SetActive(true);
				this.m_cantContributeArea.SetActive(false);
				this.m_progressFillBar.sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_BarFilling_UnderConstruction");
				break;
			case 2:
				num = contribution.UitextureAtlasMemberIDActive;
				this.m_buildingState.text = StaticDB.GetString("BUILDING_ACTIVE", "Building Active (PH)");
				this.m_buildingState.color = new Color(0f, 1f, 0f, 1f);
				this.m_buildingStateImageFrame_active.gameObject.SetActive(true);
				this.m_progressFillBar.sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_BarFilling_Active");
				break;
			case 3:
				num = contribution.UitextureAtlasMemberIDUnderAttack;
				this.m_buildingState.text = StaticDB.GetString("UNDER_ATTACK", "Under Attack (PH)");
				this.m_buildingState.color = new Color(1f, 0f, 0f, 1f);
				this.m_buildingStateImageFrame_underAttack.gameObject.SetActive(true);
				this.m_progressFillBar.sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_BarFilling_UnderAttack");
				break;
			case 4:
				num = contribution.UitextureAtlasMemberIDDestroyed;
				this.m_buildingState.text = StaticDB.GetString("DESTROYED", "Destroyed (PH)");
				this.m_buildingState.color = new Color(0.6f, 0.6f, 0.6f, 1f);
				this.m_buildingStateImageFrame_destroyed.gameObject.SetActive(true);
				this.m_progressFillBar.sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_BarFilling_UnderConstruction");
				break;
			default:
				return;
			}
			Sprite sprite = null;
			switch (num)
			{
			case 6318:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_CommandCenter_Active-v2");
				break;
			case 6319:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_CommandCenter_Destroyed-v2");
				break;
			case 6320:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_CommandCenter_UnderAttack-v2");
				break;
			case 6321:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_CommandCenter_UnderConstruction-v2");
				break;
			case 6322:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_MageTower_Active-v2");
				break;
			case 6323:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_MageTower_Destroyed-v2");
				break;
			case 6324:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_MageTower_UnderAttack-v2");
				break;
			case 6325:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_MageTower_UnderConstruction-v2");
				break;
			case 6326:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_NetherDisruptor_Active-v2");
				break;
			case 6327:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_NetherDisruptor_Destroyed-v2");
				break;
			case 6328:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_NetherDisruptor_UnderAttack-v2");
				break;
			case 6329:
				sprite = AssetBundleManager.LoadAsset<Sprite>(LegionfallBuildingPanel.LegionfallBundleName, "LegionfallCompanion_NetherDisruptor_UnderConstruction-v2");
				break;
			}
			if (sprite != null)
			{
				this.m_buildingStateImage.sprite = sprite;
			}
			this.m_buildingName.text = contribution.Name;
			this.m_buildingDescription.text = GeneralHelpers.LimitZhLineLength(contribution.Description, 16);
			this.m_progressFillBar.fillAmount = contribution.UnitCompletion;
			this.m_progressFillGlow.anchorMin = new Vector2(this.m_progressFillBar.fillAmount, 0.5f);
			this.m_progressFillGlow.anchorMax = new Vector2(this.m_progressFillBar.fillAmount, 0.5f);
			this.m_progressFillGlow.anchoredPosition = Vector2.zero;
			if (contribution.State == 3)
			{
				this.m_healthText.text = StaticDB.GetString("TIME_LEFT", "Time Left (PH):") + " 123 Hours";
			}
			else
			{
				this.m_healthText.text = ((int)(this.m_progressFillBar.fillAmount * 100f)).ToString() + "%";
			}
			this.m_costLabel.text = StaticDB.GetString("COST", null);
			if (LegionfallData.WarResources() >= contribution.ContributionCurrencyCost)
			{
				this.m_cost.text = LegionfallData.WarResources().ToString("N0") + "/" + contribution.ContributionCurrencyCost;
				this.m_contributeButton.interactable = true;
				this.m_contributeButtonLabel.color = new Color(1f, 0.859f, 0f, 1f);
			}
			else
			{
				this.m_cost.text = string.Concat(new object[]
				{
					"<color=#ff0000ff>",
					LegionfallData.WarResources().ToString("N0"),
					"</color>/",
					contribution.ContributionCurrencyCost
				});
				this.m_contributeButton.interactable = false;
				this.m_contributeButtonLabel.color = new Color(0.6f, 0.6f, 0.6f, 1f);
			}
			this.m_currencyIcon.sprite = GeneralHelpers.LoadCurrencyIcon(contribution.ContributionCurrencyType);
			this.m_contributeButtonLabel.text = StaticDB.GetString("CONTRIBUTE", "Contribute (PH)");
			SpellDisplay[] componentsInChildren = this.m_buffArea.GetComponentsInChildren<SpellDisplay>();
			foreach (SpellDisplay spellDisplay in componentsInChildren)
			{
				Object.Destroy(spellDisplay.gameObject);
			}
			foreach (int spell in contribution.Spells)
			{
				SpellDisplay spellDisplay2 = Object.Instantiate<SpellDisplay>(this.m_legionfallBuffSpellDisplayPrefab);
				spellDisplay2.transform.SetParent(this.m_buffArea, false);
				spellDisplay2.SetSpell(spell);
				spellDisplay2.SetLocked(contribution.State != 2 && contribution.State != 3);
			}
			iTween.Stop(base.gameObject);
			this.m_progressBarGlow.color = new Color(1f, 1f, 1f, 0f);
			this.m_progressFillGlowGlow.color = new Color(1f, 1f, 1f, 0f);
		}

		private void HandleMakeContributionRequestInitiated()
		{
			this.m_contributeButton.interactable = false;
			this.m_contributeButtonLabel.color = new Color(0.6f, 0.6f, 0.6f, 1f);
		}

		private void HandleContributionInfoChanged()
		{
			this.InitPanel(this.m_contributionID, this.m_questID);
			if (LegionfallBuildingPanel.s_lastContributionID == this.m_contributionID)
			{
				this.DoProgressBarGlow();
			}
		}

		public void MakeContribution()
		{
			if (!LegionfallData.legionfallDictionary.ContainsKey(this.m_contributionID))
			{
				return;
			}
			WrapperContribution contribution = LegionfallData.legionfallDictionary[this.m_contributionID].contribution;
			if (contribution.State == 1)
			{
				LegionfallBuildingPanel.s_lastContributionID = this.m_contributionID;
				Debug.Log("Starting to contribute to ID " + contribution.ContributionID);
				if (Main.instance.MakeContributionRequestInitiatedAction != null)
				{
					Main.instance.MakeContributionRequestInitiatedAction();
				}
				LegionCompanionWrapper.MakeContribution(contribution.ContributionID);
				this.m_lootDisplayPending = true;
				this.m_delayBeforeShowingLoot = 2f;
				this.m_lootDisplayTimeRemaining = 3f;
				Main.instance.m_UISound.Play_ButtonRedClick();
			}
		}

		public void DoProgressBarGlow()
		{
			Main.instance.m_UISound.Play_ContributeSuccess();
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"GlowIn",
				"delay",
				0f,
				"from",
				0f,
				"to",
				1f,
				"easeType",
				iTween.EaseType.easeInOutQuad,
				"time",
				this.m_glowDuration / 2f,
				"onupdate",
				"ProgressBarGlowUpdate",
				"oncomplete",
				"ProgressBarGlowInComplete"
			}));
		}

		private void ProgressBarGlowUpdate(float val)
		{
			this.m_progressBarGlow.color = new Color(1f, 1f, 1f, val);
			this.m_progressFillGlowGlow.color = new Color(1f, 1f, 1f, val);
		}

		private void ProgressBarGlowInComplete()
		{
			this.m_progressBarGlow.color = new Color(1f, 1f, 1f, 1f);
			this.m_progressFillGlowGlow.color = new Color(1f, 1f, 1f, 1f);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"GlowOut",
				"delay",
				0f,
				"from",
				1f,
				"to",
				0f,
				"easeType",
				iTween.EaseType.easeInOutQuad,
				"time",
				this.m_glowDuration / 2f,
				"onupdate",
				"ProgressBarGlowUpdate",
				"oncomplete",
				"ProgressBarGlowComplete"
			}));
		}

		private void ProgressBarGlowOutComplete()
		{
			this.m_progressBarGlow.color = new Color(1f, 1f, 1f, 0f);
			this.m_progressFillGlowGlow.color = new Color(1f, 1f, 1f, 0f);
		}

		private void HandleGotItemFromQuestCompletion(int itemID, int itemQuantity, int questID)
		{
			if (this.m_questID != questID)
			{
				return;
			}
			this.m_lootItemID = itemID;
			this.m_lootItemQuantity += itemQuantity;
		}

		private void FadeLootInCallback(float val)
		{
			this.m_lootAreaCanvasGroup.alpha = val;
			this.m_contributeAreaCanvasGroup.alpha = 1f - val;
		}

		private void FadeLootInCompleteCallback()
		{
			this.m_lootAreaCanvasGroup.alpha = 1f;
			this.m_contributeAreaCanvasGroup.alpha = 0f;
		}

		private void FadeLootOutCallback(float val)
		{
			this.m_lootAreaCanvasGroup.alpha = 1f - val;
			this.m_contributeAreaCanvasGroup.alpha = val;
		}

		private void FadeLootOutCompleteCallback()
		{
			this.m_lootAreaCanvasGroup.alpha = 0f;
			this.m_contributeAreaCanvasGroup.alpha = 1f;
			this.m_gotLootArea.SetActive(false);
			this.m_lootItemQuantity = 0;
		}

		private void Update()
		{
			if (this.m_lootDisplayPending && this.m_contributeButton.interactable)
			{
				this.m_delayBeforeShowingLoot -= Time.deltaTime;
				if (this.m_delayBeforeShowingLoot > 0f)
				{
					return;
				}
			}
			if (this.m_delayBeforeShowingLoot <= 0f && this.m_lootDisplayTimeRemaining > 0f && this.m_lootItemQuantity > 0)
			{
				if (!this.m_gotLootArea.activeSelf)
				{
					Main.instance.m_UISound.Play_GetItem();
					this.m_gotLootArea.SetActive(true);
					this.m_lootAreaCanvasGroup.alpha = 0f;
					iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
					{
						"name",
						"Fade Loot In",
						"from",
						0f,
						"to",
						1f,
						"easeType",
						"easeOutCubic",
						"time",
						0.5f,
						"onupdate",
						"FadeLootInCallback",
						"oncomplete",
						"FadeLootInCompleteCallback"
					}));
				}
				ItemRec record = StaticDB.itemDB.GetRecord(this.m_lootItemID);
				if (record != null)
				{
					this.m_gotLootItemName.text = record.Display + ((this.m_lootItemQuantity != 1) ? (" (" + this.m_lootItemQuantity + "x)") : string.Empty);
				}
				else
				{
					this.m_gotLootItemName.text = "???" + ((this.m_lootItemQuantity != 1) ? (" (" + this.m_lootItemQuantity + "x)") : string.Empty);
				}
				this.m_rewardDisplay.InitReward(MissionRewardDisplay.RewardType.item, this.m_lootItemID, 1, 0, 0);
				this.m_lootDisplayTimeRemaining -= Time.deltaTime;
			}
			else if (this.m_lootDisplayTimeRemaining <= 0f && this.m_lootDisplayPending)
			{
				if (this.m_gotLootArea.activeSelf)
				{
					iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
					{
						"name",
						"Fade Loot Out",
						"from",
						0f,
						"to",
						1f,
						"easeType",
						"easeOutCubic",
						"time",
						0.5f,
						"onupdate",
						"FadeLootOutCallback",
						"oncomplete",
						"FadeLootOutCompleteCallback"
					}));
				}
				this.m_lootDisplayPending = false;
			}
			if (LegionfallData.legionfallDictionary.ContainsKey(this.m_contributionID))
			{
				LegionfallData.ContributionData contributionData = LegionfallData.legionfallDictionary[this.m_contributionID];
				WrapperContribution contribution = contributionData.contribution;
				if (contribution.State == 3)
				{
					if (contributionData.underAttackExpireTime <= DateTime.UtcNow)
					{
						contributionData.underAttackExpireTime = GarrisonStatus.CurrentTime().AddSeconds((double)contributionData.contribution.CurrentValue);
					}
					TimeSpan timeSpan = contributionData.underAttackExpireTime - GarrisonStatus.CurrentTime();
					timeSpan = ((timeSpan.TotalSeconds <= 0.0) ? TimeSpan.Zero : timeSpan);
					this.m_healthText.text = StaticDB.GetString("TIME_LEFT", null) + " " + timeSpan.GetDurationString(false);
					this.m_progressFillBar.fillAmount = contribution.CurrentValue / contribution.UpperValue;
				}
			}
		}

		public static readonly string LegionfallBundleName = "legionfall";

		public SpellDisplay m_legionfallBuffSpellDisplayPrefab;

		public Text m_buildingName;

		public Image m_buildingStateImage;

		public Image m_buildingStateImageFrame_building;

		public Image m_buildingStateImageFrame_active;

		public Image m_buildingStateImageFrame_underAttack;

		public Image m_buildingStateImageFrame_destroyed;

		public Text m_buildingState;

		public Text m_buildingDescription;

		public Transform m_buffArea;

		public Image m_progressFillBar;

		public RectTransform m_progressFillGlow;

		public Image m_progressFillGlowGlow;

		public Text m_costLabel;

		public Text m_cost;

		public Image m_currencyIcon;

		public Text m_contributeButtonLabel;

		public Button m_contributeButton;

		public Image m_progressBarGlow;

		public float m_glowDuration;

		public GameObject m_contributeArea;

		public GameObject m_cantContributeArea;

		public Text m_cantContributeText;

		public Text m_healthText;

		public Image m_buildingTitleBanner;

		public Shader m_grayscaleShader;

		public GameObject m_gotLootArea;

		public Text m_gotLootLabel;

		public Text m_gotLootItemName;

		public MissionRewardDisplay m_rewardDisplay;

		public CanvasGroup m_contributeAreaCanvasGroup;

		public CanvasGroup m_lootAreaCanvasGroup;

		private int m_lootItemID;

		private int m_lootItemQuantity;

		private float m_lootDisplayTimeRemaining;

		private bool m_lootDisplayPending;

		private float m_delayBeforeShowingLoot;

		private int m_contributionID;

		private int m_questID;

		private static int s_lastContributionID;

		private static class ContributionState
		{
			public const int UnderConstruction = 1;

			public const int Active = 2;

			public const int UnderAttack = 3;

			public const int Destroyed = 4;
		}
	}
}
