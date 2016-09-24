using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class BountyInfoTooltip : MonoBehaviour
{
	public void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		this.m_rewardsLabel.text = StaticDB.GetString("REWARDS", null);
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	public void SetBounty(MobileWorldQuestBounty bounty)
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
		this.m_timeLeft.text = StaticDB.GetString("TIME_REMAINING", null);
		RectTransform[] componentsInChildren = this.m_bountyQuestIconArea.GetComponentsInChildren<RectTransform>(true);
		foreach (RectTransform rectTransform in componentsInChildren)
		{
			if (rectTransform != null && rectTransform.gameObject != this.m_bountyQuestIconArea.gameObject)
			{
				Object.DestroyImmediate(rectTransform.gameObject);
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
		if (bounty.Item.Length > 0)
		{
			ItemRec record2 = StaticDB.itemDB.GetRecord(bounty.Item[0].RecordID);
			if (record2 != null)
			{
				this.m_lootName.text = record2.Display;
				this.m_lootDescription.text = GeneralHelpers.GetItemDescription(record2);
				Sprite sprite2 = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record2.IconFileDataID);
				if (sprite2 != null)
				{
					this.m_lootIcon.sprite = sprite2;
				}
				else if (this.m_lootIconInvalidFileDataID != null)
				{
					this.m_lootIconInvalidFileDataID.gameObject.SetActive(true);
					this.m_lootIconInvalidFileDataID.text = string.Empty + record2.IconFileDataID;
				}
			}
			else
			{
				this.m_lootName.text = "Unknown item " + bounty.Item[0].RecordID;
				this.m_lootDescription.text = "Unknown item " + bounty.Item[0].RecordID;
			}
		}
		else
		{
			this.m_lootName.text = "ERROR: Loot Not Specified";
			this.m_lootDescription.text = "ERROR: Loot Not Specified";
		}
	}

	private void UpdateTimeRemaining()
	{
		long num = (long)this.m_bounty.EndTime - GarrisonStatus.CurrentTime();
		num = ((num <= 0L) ? 0L : num);
		Duration duration = new Duration((int)num, false);
		this.m_timeLeft.text = StaticDB.GetString("TIME_REMAINING", null) + " " + duration.DurationString;
	}

	private void Update()
	{
		this.UpdateTimeRemaining();
	}

	public Image m_bountyIcon;

	public Text m_bountyIconInvalidFileDataID;

	public Text m_bountyName;

	public Text m_timeLeft;

	public Text m_bountyDescription;

	public GameObject m_bountyQuestCompleteIconPrefab;

	public GameObject m_bountyQuestAvailableIconPrefab;

	public Transform m_bountyQuestIconArea;

	public Image m_lootIcon;

	public Text m_lootIconInvalidFileDataID;

	public Text m_lootName;

	public Text m_lootDescription;

	public Text m_rewardsLabel;

	private MobileWorldQuestBounty m_bounty;
}
