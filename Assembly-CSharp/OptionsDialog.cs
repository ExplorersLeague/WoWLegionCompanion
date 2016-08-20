using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WowStatConstants;

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
		this.m_enableSFX.isOn = Main.instance.m_UISound.IsSFXEnabled();
		this.m_mapFilters[0].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.All);
		this.m_mapFilters[1].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.ArtifactPower);
		this.m_mapFilters[3].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gear);
		this.m_mapFilters[4].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gold);
		this.m_mapFilters[2].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.OrderResources);
		this.m_mapFilters[5].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.ProfessionMats);
		this.m_mapFilters[6].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.PetCharms);
		this.m_mapFilters[0].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableAll));
		this.m_mapFilters[1].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableArtifactPower));
		this.m_mapFilters[3].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableGear));
		this.m_mapFilters[4].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableGold));
		this.m_mapFilters[2].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableOrderResources));
		this.m_mapFilters[5].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableProfessionMats));
		this.m_mapFilters[6].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnablePetCharms));
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
		this.m_mapFilters[3].GetComponentInChildren<Text>().text = StaticDB.GetString("GEAR", "Gear");
		this.m_mapFilters[4].GetComponentInChildren<Text>().text = StaticDB.GetString("GOLD", "Gold");
		this.m_mapFilters[2].GetComponentInChildren<Text>().text = StaticDB.GetString("ORDER_RESOURCES", "Order Resources");
		this.m_mapFilters[5].GetComponentInChildren<Text>().text = StaticDB.GetString("PROFESSION_MATERIALS", "Profession Materials");
		this.m_mapFilters[6].GetComponentInChildren<Text>().text = StaticDB.GetString("PET_CHARMS", "Pet Charms");
		this.SyncWithOptions();
	}

	private void OnEnable()
	{
		Main.instance.m_UISound.Play_ShowGenericTooltip();
		Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
		Main.instance.m_backButtonManager.PushBackAction(BackAction.hideAllPopups, null);
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

	public Toggle m_enableSFX;

	public Toggle[] m_mapFilters;

	public Text m_titleText;

	public Text m_okText;

	public Text m_filterTitleText;

	public Text m_sfxText;
}
