using System;
using UnityEngine;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class BountyInfoTooltip : BaseDialog
	{
		public void SetBounty(WrapperWorldQuestBounty bounty)
		{
			this.m_bounty = bounty;
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, bounty.IconFileDataID);
			if (sprite != null)
			{
				this.m_bountyIconInvalidFileDataID.gameObject.SetActive(false);
				this.m_bountyIcon.sprite = sprite;
			}
			else
			{
				this.m_bountyIconInvalidFileDataID.gameObject.SetActive(true);
				this.m_bountyIconInvalidFileDataID.text = string.Empty + bounty.IconFileDataID;
			}
			QuestV2Rec record = StaticDB.questDB.GetRecord(bounty.QuestID);
			if (record != null)
			{
				this.m_bountyName.text = record.QuestTitle;
				this.m_bountyDescription.text = string.Concat(new object[]
				{
					string.Empty,
					bounty.NumCompleted,
					"/",
					bounty.NumNeeded,
					" ",
					record.LogDescription
				});
			}
			else
			{
				this.m_bountyName.text = "Unknown Quest ID " + bounty.QuestID;
				this.m_bountyDescription.text = "Unknown Quest ID " + bounty.QuestID;
			}
			this.m_timeLeft.text = StaticDB.GetString("TIME_LEFT", "Time Left: PH");
			RectTransform[] componentsInChildren = this.m_bountyQuestIconArea.GetComponentsInChildren<RectTransform>(true);
			foreach (RectTransform rectTransform in componentsInChildren)
			{
				if (rectTransform != null && rectTransform.gameObject != this.m_bountyQuestIconArea.gameObject)
				{
					rectTransform.SetParent(null);
					Object.Destroy(rectTransform.gameObject);
				}
			}
			for (int j = 0; j < bounty.NumCompleted; j++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.m_bountyQuestCompleteIconPrefab);
				gameObject.transform.SetParent(this.m_bountyQuestIconArea.transform, false);
			}
			for (int k = 0; k < bounty.NumNeeded - bounty.NumCompleted; k++)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(this.m_bountyQuestAvailableIconPrefab);
				gameObject2.transform.SetParent(this.m_bountyQuestIconArea.transform, false);
			}
			this.UpdateTimeRemaining();
			if (bounty.Items.Count > 0 && StaticDB.itemDB.GetRecord(bounty.Items[0].RecordID) != null)
			{
				WrapperWorldQuestReward wrapperWorldQuestReward = bounty.Items[0];
				Sprite rewardSprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, wrapperWorldQuestReward.FileDataID);
				this.m_rewardInfo.SetReward(MissionRewardDisplay.RewardType.item, wrapperWorldQuestReward.RecordID, wrapperWorldQuestReward.Quantity, rewardSprite, wrapperWorldQuestReward.ItemContext, wrapperWorldQuestReward.ItemInstance);
			}
			else if (bounty.Money > 1000000)
			{
				Sprite iconSprite = Resources.Load<Sprite>("MiscIcons/INV_Misc_Coin_01");
				this.m_rewardInfo.SetGold(bounty.Money / 10000, iconSprite);
			}
			else if (bounty.Currencies.Count > 1)
			{
				int num = 0;
				foreach (WrapperWorldQuestReward wrapperWorldQuestReward2 in bounty.Currencies)
				{
					bool flag = wrapperWorldQuestReward2.RecordID == 1560;
					CurrencyTypesRec record2 = StaticDB.currencyTypesDB.GetRecord(wrapperWorldQuestReward2.RecordID);
					CurrencyContainerRec currencyContainerRec = CurrencyContainerDB.CheckAndGetValidCurrencyContainer(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity);
					if (currencyContainerRec != null)
					{
						Sprite iconSprite2 = CurrencyContainerDB.LoadCurrencyContainerIcon(wrapperWorldQuestReward2.RecordID, wrapperWorldQuestReward2.Quantity);
						int num2 = wrapperWorldQuestReward2.Quantity / (((record2.Flags & 8u) == 0u) ? 1 : 100);
						if (num2 > num)
						{
							num = num2;
							this.m_rewardInfo.SetCurrency(wrapperWorldQuestReward2.RecordID, num, iconSprite2);
						}
					}
					else
					{
						Sprite iconSprite2 = GeneralHelpers.LoadCurrencyIcon(wrapperWorldQuestReward2.RecordID);
						int num3 = wrapperWorldQuestReward2.Quantity / (((record2.Flags & 8u) == 0u) ? 1 : 100);
						if (num3 > num || flag)
						{
							num = num3;
							this.m_rewardInfo.SetCurrency(wrapperWorldQuestReward2.RecordID, num, iconSprite2);
							if (flag)
							{
								break;
							}
						}
					}
				}
			}
		}

		private bool UpdateTimeRemaining()
		{
			TimeSpan timeSpan = this.m_bounty.EndTime - GarrisonStatus.CurrentTime();
			timeSpan = ((timeSpan.TotalSeconds <= 0.0) ? TimeSpan.Zero : timeSpan);
			this.m_timeLeft.text = StaticDB.GetString("TIME_LEFT", "Time Left: PH") + " " + timeSpan.GetDurationString(timeSpan.Days == 0, TimeUnit.Minute);
			return timeSpan.TotalSeconds > 0.0;
		}

		private void Update()
		{
			if (!this.UpdateTimeRemaining())
			{
				base.gameObject.SetActive(false);
			}
		}

		public Image m_bountyIcon;

		public Text m_bountyIconInvalidFileDataID;

		public Text m_bountyName;

		public Text m_timeLeft;

		public Text m_bountyDescription;

		public GameObject m_bountyQuestCompleteIconPrefab;

		public GameObject m_bountyQuestAvailableIconPrefab;

		public Transform m_bountyQuestIconArea;

		public RewardInfoPopup m_rewardInfo;

		private WrapperWorldQuestBounty m_bounty;
	}
}
