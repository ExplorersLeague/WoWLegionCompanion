using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class OptionsDialog : MonoBehaviour
{
	public void SyncWithOptions()
	{
		this.m_mapFilters[0].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableAll));
		this.m_mapFilters[1].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableArtifactPower));
		this.m_mapFilters[3].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableGear));
		this.m_mapFilters[4].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableGold));
		this.m_mapFilters[2].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableOrderResources));
		this.m_mapFilters[5].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableProfessionMats));
		this.m_mapFilters[6].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnablePetCharms));
		this.m_mapFilters[7].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyKirinTor));
		this.m_mapFilters[8].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyValarjar));
		this.m_mapFilters[9].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyNightfallen));
		this.m_mapFilters[10].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyWardens));
		this.m_mapFilters[11].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyDreamweavers));
		this.m_mapFilters[12].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyCourtOfFarondis));
		this.m_mapFilters[13].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyHighmountainTribes));
		bool flag = Main.instance.m_UISound.IsSFXEnabled();
		string @string = SecurePlayerPrefs.GetString("EnableSFX", Main.uniqueIdentifier);
		if (@string != null)
		{
			if (@string == "true")
			{
				flag = true;
			}
			else if (@string == "false")
			{
				flag = false;
			}
		}
		if (Main.instance.m_UISound.IsSFXEnabled() != flag)
		{
			Main.instance.m_UISound.EnableSFX(flag);
		}
		this.m_enableSFX.isOn = Main.instance.m_UISound.IsSFXEnabled();
		this.m_mapFilters[0].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.All);
		this.m_mapFilters[1].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.ArtifactPower);
		this.m_mapFilters[3].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gear);
		this.m_mapFilters[4].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gold);
		this.m_mapFilters[2].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.OrderResources);
		this.m_mapFilters[5].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.ProfessionMats);
		this.m_mapFilters[6].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.PetCharms);
		this.m_mapFilters[7].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_KirinTor);
		this.m_mapFilters[8].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_Valarjar);
		this.m_mapFilters[9].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_Nightfallen);
		this.m_mapFilters[10].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_Wardens);
		this.m_mapFilters[11].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_Dreamweavers);
		this.m_mapFilters[12].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_CourtOfFarondis);
		this.m_mapFilters[13].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_HighmountainTribes);
		this.m_mapFilters[0].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableAll));
		this.m_mapFilters[1].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableArtifactPower));
		this.m_mapFilters[3].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableGear));
		this.m_mapFilters[4].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableGold));
		this.m_mapFilters[2].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableOrderResources));
		this.m_mapFilters[5].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableProfessionMats));
		this.m_mapFilters[6].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnablePetCharms));
		this.m_mapFilters[7].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyKirinTor));
		this.m_mapFilters[8].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyValarjar));
		this.m_mapFilters[9].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyNightfallen));
		this.m_mapFilters[10].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyWardens));
		this.m_mapFilters[11].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyDreamweavers));
		this.m_mapFilters[12].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyCourtOfFarondis));
		this.m_mapFilters[13].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyHighmountainTribes));
	}

	private string GetQuestTitle(int questID)
	{
		QuestV2Rec record = StaticDB.questDB.GetRecord(questID);
		if (record == null)
		{
			Debug.LogError("Invalid Quest ID " + questID);
			return string.Empty;
		}
		return record.QuestTitle;
	}

	private bool BountyIsActive(int bountyQuestID)
	{
		foreach (object obj in PersistentBountyData.bountyDictionary.Values)
		{
			MobileWorldQuestBounty mobileWorldQuestBounty = (MobileWorldQuestBounty)obj;
			if (mobileWorldQuestBounty.QuestID == bountyQuestID)
			{
				return true;
			}
		}
		return false;
	}

	private void Start()
	{
		this.m_enableSFX.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableSFX));
		this.m_titleText.font = GeneralHelpers.LoadStandardFont();
		this.m_okText.font = GeneralHelpers.LoadStandardFont();
		this.m_filterTitleText.font = GeneralHelpers.LoadStandardFont();
		this.m_sfxText.font = GeneralHelpers.LoadStandardFont();
		this.m_titleText.text = StaticDB.GetString("OPTIONS", "Options");
		this.m_okText.text = StaticDB.GetString("OK", null);
		this.m_filterTitleText.text = StaticDB.GetString("WORLD_QUEST_FILTERS", null);
		this.m_sfxText.text = StaticDB.GetString("ENABLE_SFX", null);
		this.m_mapFilters[0].GetComponentInChildren<Text>().text = StaticDB.GetString("SHOW_ALL", "Show All");
		this.m_mapFilters[1].GetComponentInChildren<Text>().text = StaticDB.GetString("ARTIFACT_POWER", "Artifact Power");
		this.m_mapFilters[3].GetComponentInChildren<Text>().text = StaticDB.GetString("EQUIPMENT", null);
		this.m_mapFilters[4].GetComponentInChildren<Text>().text = StaticDB.GetString("GOLD", "Gold");
		this.m_mapFilters[2].GetComponentInChildren<Text>().text = StaticDB.GetString("ORDER_RESOURCES", "Order Resources");
		this.m_mapFilters[5].GetComponentInChildren<Text>().text = StaticDB.GetString("PROFESSION_MATERIALS", "Profession Materials");
		this.m_mapFilters[6].GetComponentInChildren<Text>().text = StaticDB.GetString("PET_CHARMS", "Pet Charms");
		this.m_mapFilters[7].GetComponentInChildren<Text>().text = this.GetQuestTitle(43179);
		this.m_mapFilters[8].GetComponentInChildren<Text>().text = this.GetQuestTitle(42234);
		this.m_mapFilters[9].GetComponentInChildren<Text>().text = this.GetQuestTitle(42421);
		this.m_mapFilters[10].GetComponentInChildren<Text>().text = this.GetQuestTitle(42422);
		this.m_mapFilters[11].GetComponentInChildren<Text>().text = this.GetQuestTitle(42170);
		this.m_mapFilters[12].GetComponentInChildren<Text>().text = this.GetQuestTitle(42420);
		this.m_mapFilters[13].GetComponentInChildren<Text>().text = this.GetQuestTitle(42233);
		this.SyncWithOptions();
	}

	private void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
		this.m_mapFilters[7].transform.parent.gameObject.SetActive(this.BountyIsActive(43179));
		this.m_mapFilters[8].transform.parent.gameObject.SetActive(this.BountyIsActive(42234));
		this.m_mapFilters[9].transform.parent.gameObject.SetActive(this.BountyIsActive(42421));
		this.m_mapFilters[10].transform.parent.gameObject.SetActive(this.BountyIsActive(42422));
		this.m_mapFilters[11].transform.parent.gameObject.SetActive(this.BountyIsActive(42170));
		this.m_mapFilters[12].transform.parent.gameObject.SetActive(this.BountyIsActive(42420));
		this.m_mapFilters[13].transform.parent.gameObject.SetActive(this.BountyIsActive(42233));
	}

	private void OnDisable()
	{
		Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PopBackAction();
	}

	private void OnValueChanged_EnableSFX(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		Main.instance.m_UISound.EnableSFX(isOn);
		SecurePlayerPrefs.SetString("EnableSFX", isOn.ToString().ToLower(), Main.uniqueIdentifier);
	}

	private void OnValueChanged_EnableAll(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.All, isOn);
	}

	private void OnValueChanged_EnableArtifactPower(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.ArtifactPower, isOn);
	}

	private void OnValueChanged_EnableGear(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Gear, isOn);
	}

	private void OnValueChanged_EnableGold(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Gold, isOn);
	}

	private void OnValueChanged_EnableOrderResources(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.OrderResources, isOn);
	}

	private void OnValueChanged_EnableProfessionMats(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.ProfessionMats, isOn);
	}

	private void OnValueChanged_EnablePetCharms(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.PetCharms, isOn);
	}

	private void OnValueChanged_EnableBountyKirinTor(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_KirinTor, isOn);
	}

	private void OnValueChanged_EnableBountyValarjar(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_Valarjar, isOn);
	}

	private void OnValueChanged_EnableBountyNightfallen(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_Nightfallen, isOn);
	}

	private void OnValueChanged_EnableBountyWardens(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_Wardens, isOn);
	}

	private void OnValueChanged_EnableBountyDreamweavers(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_Dreamweavers, isOn);
	}

	private void OnValueChanged_EnableBountyCourtOfFarondis(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_CourtOfFarondis, isOn);
	}

	private void OnValueChanged_EnableBountyHighmountainTribes(bool isOn)
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_HighmountainTribes, isOn);
	}

	public Toggle m_enableSFX;

	public Toggle[] m_mapFilters;

	public Text m_titleText;

	public Text m_okText;

	public Text m_filterTitleText;

	public Text m_sfxText;
}
