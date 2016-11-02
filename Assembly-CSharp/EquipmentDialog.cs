using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowJamMessages.MobilePlayerJSON;
using WowStatConstants;
using WowStaticData;

public class EquipmentDialog : MonoBehaviour
{
	private void Awake()
	{
		this.m_titleText.font = GeneralHelpers.LoadFancyFont();
		this.m_titleText.text = StaticDB.GetString("EQUIPMENT", null);
		this.m_noEquipmentMessage.font = GeneralHelpers.LoadStandardFont();
		this.m_noEquipmentMessage.text = StaticDB.GetString("NO_EQUIPMENT2", "You do not have any Champion Equipment to equip.");
	}

	public void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
		Main instance = Main.instance;
		instance.EquipmentInventoryChangedAction = (Action)Delegate.Combine(instance.EquipmentInventoryChangedAction, new Action(this.UpdateDisplayCB));
		MobilePlayerFollowerEquipmentRequest mobilePlayerFollowerEquipmentRequest = new MobilePlayerFollowerEquipmentRequest();
		mobilePlayerFollowerEquipmentRequest.GarrFollowerTypeID = 4;
		Login.instance.SendToMobileServer(mobilePlayerFollowerEquipmentRequest);
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
		Main instance = Main.instance;
		instance.EquipmentInventoryChangedAction = (Action)Delegate.Remove(instance.EquipmentInventoryChangedAction, new Action(this.UpdateDisplayCB));
		this.m_garrAbilityID = 0;
		this.m_followerDetailView = null;
	}

	public void SetAbility(int garrAbilityID, FollowerDetailView followerDetailView)
	{
		GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(garrAbilityID);
		if (record == null)
		{
			Debug.LogWarning("Invalid garrAbilityID " + garrAbilityID);
			return;
		}
		this.m_garrAbilityID = garrAbilityID;
		this.m_followerDetailView = followerDetailView;
		this.m_abilityNameText.text = record.Name;
		this.m_abilityDescription.text = WowTextParser.parser.Parse(record.Description, 0);
		this.m_abilityDescription.supportRichText = WowTextParser.parser.IsRichText();
		this.m_abilityDisplay.SetAbility(garrAbilityID, true, true, null);
		this.UpdateEquipmentDisplay(garrAbilityID, followerDetailView);
	}

	private void UpdateEquipmentDisplay(int garrAbilityID, FollowerDetailView followerDetailView)
	{
		FollowerInventoryListItem[] componentsInChildren = this.m_equipmentListContent.GetComponentsInChildren<FollowerInventoryListItem>(true);
		foreach (FollowerInventoryListItem followerInventoryListItem in componentsInChildren)
		{
			Object.DestroyImmediate(followerInventoryListItem.gameObject);
		}
		bool active = true;
		foreach (object obj in PersistentEquipmentData.equipmentDictionary.Values)
		{
			MobileFollowerEquipment mobileFollowerEquipment = (MobileFollowerEquipment)obj;
			GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(mobileFollowerEquipment.GarrAbilityID);
			if (record != null)
			{
				if ((record.Flags & 64u) == 0u)
				{
					FollowerInventoryListItem followerInventoryListItem2 = Object.Instantiate<FollowerInventoryListItem>(this.m_equipmentListItemPrefab);
					followerInventoryListItem2.transform.SetParent(this.m_equipmentListContent.transform, false);
					followerInventoryListItem2.SetEquipment(mobileFollowerEquipment, followerDetailView, garrAbilityID);
					active = false;
				}
			}
		}
		this.m_noEquipmentMessage.gameObject.SetActive(active);
	}

	private void UpdateDisplayCB()
	{
		if (this.m_garrAbilityID > 0 && this.m_followerDetailView != null)
		{
			this.UpdateEquipmentDisplay(this.m_garrAbilityID, this.m_followerDetailView);
		}
	}

	public AbilityDisplay m_abilityDisplay;

	public Text m_titleText;

	public Text m_abilityNameText;

	public Text m_abilityDescription;

	public Text m_noEquipmentMessage;

	public FollowerInventoryListItem m_equipmentListItemPrefab;

	public GameObject m_equipmentListContent;

	private int m_garrAbilityID;

	private FollowerDetailView m_followerDetailView;
}
