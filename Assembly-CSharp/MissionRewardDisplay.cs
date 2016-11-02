using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class MissionRewardDisplay : MonoBehaviour
{
	private void Awake()
	{
		this.ClearResults();
	}

	public void ClearResults()
	{
		this.m_enableLootEffect_success = false;
		this.m_enableLootEffect_fail = false;
		if (this.m_greenCheck != null)
		{
			this.m_greenCheck.gameObject.SetActive(false);
		}
		if (this.m_redX != null)
		{
			this.m_redX.gameObject.SetActive(false);
		}
		if (this.m_effectHandle != null)
		{
			UiAnimation anim = this.m_effectHandle.GetAnim();
			if (anim != null)
			{
				anim.Stop(0f);
			}
		}
	}

	public void ShowResultSuccess(float delay)
	{
		this.m_enableLootEffect_success = true;
		this.timeRemainingUntilLootEffect = delay;
	}

	public void ShowResultFail(float delay)
	{
		this.m_enableLootEffect_fail = true;
		this.timeRemainingUntilLootEffect = delay;
	}

	public void ShowRewardTooltip()
	{
		Main.instance.allPopups.ShowRewardTooltip(this.m_rewardType, this.m_rewardID, this.m_rewardQuantity, this.m_rewardIcon, this.m_itemContext);
	}

	private void Update()
	{
		if (this.timeRemainingUntilLootEffect <= 0f)
		{
			return;
		}
		this.timeRemainingUntilLootEffect -= Time.deltaTime;
		if (this.timeRemainingUntilLootEffect <= 0f)
		{
			if (this.m_enableLootEffect_success)
			{
				this.m_greenCheck.gameObject.SetActive(true);
				this.m_effectHandle = UiAnimMgr.instance.PlayAnim(this.m_uiAnimation_success, this.m_greenCheckEffectRootTransform, Vector3.zero, this.m_animScale_success, 0f);
				Main.instance.m_UISound.Play_GreenCheck();
			}
			if (this.m_enableLootEffect_fail)
			{
				this.m_redX.gameObject.SetActive(true);
				this.m_effectHandle = UiAnimMgr.instance.PlayAnim(this.m_uiAnimation_fail, this.m_redFailXEffectRootTransform, Vector3.zero, this.m_animScale_fail, 0f);
				Main.instance.m_UISound.Play_RedFailX();
			}
		}
	}

	public static void InitMissionRewards(GameObject prefab, Transform parent, JamGarrisonMissionReward[] rewards)
	{
		foreach (JamGarrisonMissionReward jamGarrisonMissionReward in rewards)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(prefab);
			gameObject.SetActive(true);
			gameObject.transform.SetParent(parent, false);
			MissionRewardDisplay component = gameObject.GetComponent<MissionRewardDisplay>();
			if (jamGarrisonMissionReward.ItemID > 0)
			{
				component.InitReward(MissionRewardDisplay.RewardType.item, jamGarrisonMissionReward.ItemID, (int)jamGarrisonMissionReward.ItemQuantity, 0, jamGarrisonMissionReward.ItemFileDataID);
			}
			else if (jamGarrisonMissionReward.FollowerXP > 0u)
			{
				component.InitReward(MissionRewardDisplay.RewardType.followerXP, 0, (int)jamGarrisonMissionReward.FollowerXP, 0, 0);
			}
			else if (jamGarrisonMissionReward.CurrencyQuantity > 0u)
			{
				if (jamGarrisonMissionReward.CurrencyType == 0)
				{
					component.InitReward(MissionRewardDisplay.RewardType.gold, 0, (int)(jamGarrisonMissionReward.CurrencyQuantity / 10000u), 0, 0);
				}
				else
				{
					CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(jamGarrisonMissionReward.CurrencyType);
					int rewardQuantity = (int)((ulong)jamGarrisonMissionReward.CurrencyQuantity / (ulong)(((record.Flags & 8u) == 0u) ? 1L : 100L));
					component.InitReward(MissionRewardDisplay.RewardType.currency, jamGarrisonMissionReward.CurrencyType, rewardQuantity, 0, 0);
				}
			}
		}
	}

	public static void InitWorldQuestRewards(MobileWorldQuest worldQuest, GameObject prefab, Transform parent)
	{
		if (worldQuest.Item != null)
		{
			foreach (MobileWorldQuestReward mobileWorldQuestReward in worldQuest.Item)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(prefab);
				gameObject.transform.SetParent(parent, false);
				MissionRewardDisplay component = gameObject.GetComponent<MissionRewardDisplay>();
				component.InitReward(MissionRewardDisplay.RewardType.item, mobileWorldQuestReward.RecordID, mobileWorldQuestReward.Quantity, mobileWorldQuestReward.ItemContext, mobileWorldQuestReward.FileDataID);
			}
		}
		if (worldQuest.Money > 0)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(prefab);
			gameObject2.transform.SetParent(parent, false);
			MissionRewardDisplay component2 = gameObject2.GetComponent<MissionRewardDisplay>();
			component2.InitReward(MissionRewardDisplay.RewardType.gold, 0, worldQuest.Money / 10000, 0, 0);
		}
		if (worldQuest.Experience > 0)
		{
			GameObject gameObject3 = Object.Instantiate<GameObject>(prefab);
			gameObject3.transform.SetParent(parent, false);
			MissionRewardDisplay component3 = gameObject3.GetComponent<MissionRewardDisplay>();
			component3.InitReward(MissionRewardDisplay.RewardType.followerXP, 0, worldQuest.Experience, 0, 0);
		}
		foreach (MobileWorldQuestReward mobileWorldQuestReward2 in worldQuest.Currency)
		{
			GameObject gameObject4 = Object.Instantiate<GameObject>(prefab);
			gameObject4.transform.SetParent(parent, false);
			MissionRewardDisplay component4 = gameObject4.GetComponent<MissionRewardDisplay>();
			CurrencyTypesRec record = StaticDB.currencyTypesDB.GetRecord(mobileWorldQuestReward2.RecordID);
			if (record != null)
			{
				int rewardQuantity = mobileWorldQuestReward2.Quantity / (((record.Flags & 8u) == 0u) ? 1 : 100);
				component4.InitReward(MissionRewardDisplay.RewardType.currency, mobileWorldQuestReward2.RecordID, rewardQuantity, 0, 0);
			}
			else
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"WORLD QUEST ",
					worldQuest.QuestID,
					" has bogus currency reward (id ",
					mobileWorldQuestReward2.RecordID,
					")"
				}));
			}
		}
	}

	public void InitReward(MissionRewardDisplay.RewardType rewardType, int rewardID, int rewardQuantity, int itemContext, int iconFileDataID = 0)
	{
		if (rewardType == MissionRewardDisplay.RewardType.faction)
		{
			return;
		}
		this.ClearResults();
		this.m_rewardType = rewardType;
		this.m_rewardID = rewardID;
		this.m_rewardQuantity = rewardQuantity;
		this.m_itemContext = itemContext;
		if (this.m_iconErrorText != null)
		{
			this.m_iconErrorText.gameObject.SetActive(false);
		}
		switch (this.m_rewardType)
		{
		case MissionRewardDisplay.RewardType.item:
		{
			Sprite sprite = null;
			ItemRec record = StaticDB.itemDB.GetRecord(this.m_rewardID);
			if (iconFileDataID > 0)
			{
				sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, iconFileDataID);
			}
			else if (record != null)
			{
				sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
			}
			if (sprite != null)
			{
				this.m_rewardIcon.sprite = sprite;
			}
			else if (this.m_iconErrorText != null)
			{
				this.m_iconErrorText.gameObject.SetActive(true);
				this.m_iconErrorText.text = string.Empty + iconFileDataID;
			}
			if (this.m_rewardName != null)
			{
				if (record != null)
				{
					this.m_rewardName.text = record.Display;
					this.m_rewardName.color = GeneralHelpers.GetQualityColor(record.OverallQualityID);
				}
				else
				{
					this.m_rewardName.text = "Unknown Item " + this.m_rewardID;
				}
			}
			if (this.m_isExpandedDisplay)
			{
				this.m_rewardQuantityText.text = ((this.m_rewardQuantity <= 1) ? string.Empty : this.m_rewardQuantity.ToString("N0"));
			}
			break;
		}
		case MissionRewardDisplay.RewardType.gold:
			this.m_rewardIcon.sprite = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
			if (this.m_isExpandedDisplay)
			{
				this.m_rewardQuantityText.text = string.Empty;
				this.m_rewardName.text = ((this.m_rewardQuantity <= 1) ? string.Empty : this.m_rewardQuantity.ToString("N0"));
			}
			break;
		case MissionRewardDisplay.RewardType.followerXP:
			this.m_rewardIcon.sprite = GeneralHelpers.GetLocalizedFollowerXpIcon();
			this.m_rewardQuantityText.text = string.Empty;
			if (this.m_rewardName != null && this.m_isExpandedDisplay)
			{
				this.m_rewardName.text = ((this.m_rewardQuantity <= 1) ? string.Empty : (this.m_rewardQuantity.ToString("N0") + " " + StaticDB.GetString("XP2", "XP")));
			}
			break;
		case MissionRewardDisplay.RewardType.currency:
		{
			Sprite sprite2 = GeneralHelpers.LoadCurrencyIcon(this.m_rewardID);
			if (sprite2 != null)
			{
				this.m_rewardIcon.sprite = sprite2;
			}
			else
			{
				this.m_iconErrorText.gameObject.SetActive(true);
				this.m_iconErrorText.text = "c " + this.m_rewardID;
			}
			if (this.m_isExpandedDisplay)
			{
				CurrencyTypesRec record2 = StaticDB.currencyTypesDB.GetRecord(rewardID);
				if (record2 != null)
				{
					this.m_rewardName.text = record2.Name;
				}
				else
				{
					this.m_rewardName.text = string.Empty;
				}
				this.m_rewardQuantityText.text = ((this.m_rewardQuantity <= 1) ? string.Empty : this.m_rewardQuantity.ToString("N0"));
			}
			break;
		}
		}
		if (!this.m_isExpandedDisplay)
		{
			this.m_rewardQuantityText.text = ((this.m_rewardQuantity <= 1) ? string.Empty : this.m_rewardQuantity.ToString("N0"));
		}
	}

	private int GetFactionRewardQuantity(int index)
	{
		switch (index)
		{
		default:
			return 0;
		case 1:
			return 10;
		case 2:
			return 25;
		case 3:
			return 75;
		case 4:
			return 150;
		case 5:
			return 250;
		case 6:
			return 350;
		case 7:
			return 500;
		case 8:
			return 1000;
		}
	}

	public bool m_isExpandedDisplay;

	public Image m_rewardIcon;

	public Text m_rewardQuantityText;

	public Button m_mainButton;

	public Text m_rewardName;

	[Header("Loot result effects")]
	private bool m_enableLootEffect_success;

	private bool m_enableLootEffect_fail;

	public string m_uiAnimation_success;

	public string m_uiAnimation_fail;

	public float m_animScale_success;

	public float m_animScale_fail;

	public Image m_greenCheck;

	public Image m_redX;

	public Transform m_greenCheckEffectRootTransform;

	public Transform m_redFailXEffectRootTransform;

	[Header("Error reporting")]
	public Text m_iconErrorText;

	private float timeRemainingUntilLootEffect;

	private int m_rewardQuantity;

	private MissionRewardDisplay.RewardType m_rewardType;

	private int m_itemContext;

	private int m_rewardID;

	private UiAnimMgr.UiAnimHandle m_effectHandle;

	public enum RewardType
	{
		item,
		gold,
		followerXP,
		currency,
		faction
	}
}
