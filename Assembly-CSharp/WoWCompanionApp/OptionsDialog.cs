using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WowStatConstants;
using WowStaticData;

namespace WoWCompanionApp
{
	public class OptionsDialog : MonoBehaviour
	{
		public void SyncWithOptions()
		{
			this.m_mapFilters[0].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableAll));
			this.m_mapFilters[1].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableAzerite));
			this.m_mapFilters[3].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableGear));
			this.m_mapFilters[4].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableGold));
			this.m_mapFilters[2].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableOrderResources));
			this.m_mapFilters[5].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableProfessionMats));
			this.m_mapFilters[6].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnablePetBattles));
			this.m_mapFilters[7].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableReputations));
			this.m_mapFilters[8].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyChampionsOfAzeroth));
			this.m_mapFilters[9].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyZandalariEmpire));
			this.m_mapFilters[10].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyProudmooreAdmiralty));
			this.m_mapFilters[11].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyOrderOfEmbers));
			this.m_mapFilters[12].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyStormsWake));
			this.m_mapFilters[13].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyTalanjisExpedition));
			this.m_mapFilters[14].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyVoldunai));
			this.m_mapFilters[15].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyTortollanSeekers));
			this.m_mapFilters[16].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyAllianceWarEffort));
			this.m_mapFilters[17].onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyHordeWarEffort));
			this.m_mapFilters[0].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.All);
			this.m_mapFilters[1].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Azerite);
			this.m_mapFilters[3].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gear);
			this.m_mapFilters[4].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Gold);
			this.m_mapFilters[2].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.OrderResources);
			this.m_mapFilters[5].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.ProfessionMats);
			this.m_mapFilters[6].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.PetBattles);
			this.m_mapFilters[7].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Reputation);
			this.m_mapFilters[8].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_ChampionsOfAzeroth);
			this.m_mapFilters[9].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_ZandalariEmpire);
			this.m_mapFilters[10].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_ProudmooreAdmiralty);
			this.m_mapFilters[11].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_OrderOfEmbers);
			this.m_mapFilters[12].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_StormsWake);
			this.m_mapFilters[13].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_TalanjisExpedition);
			this.m_mapFilters[14].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_Voldunai);
			this.m_mapFilters[15].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_TortollanSeekers);
			this.m_mapFilters[16].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_AllianceWarEffort);
			this.m_mapFilters[17].isOn = AdventureMapPanel.instance.IsFilterEnabled(MapFilterType.Bounty_HordeWarEffort);
			this.m_mapFilters[0].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableAll));
			this.m_mapFilters[1].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableAzerite));
			this.m_mapFilters[3].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableGear));
			this.m_mapFilters[4].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableGold));
			this.m_mapFilters[2].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableOrderResources));
			this.m_mapFilters[5].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableProfessionMats));
			this.m_mapFilters[6].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnablePetBattles));
			this.m_mapFilters[7].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableReputations));
			this.m_mapFilters[8].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyChampionsOfAzeroth));
			this.m_mapFilters[9].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyZandalariEmpire));
			this.m_mapFilters[10].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyProudmooreAdmiralty));
			this.m_mapFilters[11].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyOrderOfEmbers));
			this.m_mapFilters[12].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyStormsWake));
			this.m_mapFilters[13].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyTalanjisExpedition));
			this.m_mapFilters[14].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyVoldunai));
			this.m_mapFilters[15].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyTortollanSeekers));
			this.m_mapFilters[16].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyAllianceWarEffort));
			this.m_mapFilters[17].onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged_EnableBountyHordeWarEffort));
		}

		private string GetQuestTitle(int questID)
		{
			QuestV2Rec record = StaticDB.questV2DB.GetRecord(questID);
			if (record == null)
			{
				Debug.LogError("Invalid Quest ID " + questID);
				return string.Empty;
			}
			return record.QuestTitle;
		}

		private bool BountyIsActive(int bountyQuestID)
		{
			foreach (WrapperWorldQuestBounty wrapperWorldQuestBounty in PersistentBountyData.bountyDictionary.Values)
			{
				if (wrapperWorldQuestBounty.QuestID == bountyQuestID)
				{
					return true;
				}
			}
			return false;
		}

		private void Start()
		{
			this.m_mapFilters[0].GetComponentInChildren<Text>().text = StaticDB.GetString("SHOW_ALL", "Show All");
			this.m_mapFilters[1].GetComponentInChildren<Text>().text = StaticDB.GetString("AZERITE", "[PH]Azerite");
			this.m_mapFilters[3].GetComponentInChildren<Text>().text = StaticDB.GetString("EQUIPMENT", null);
			this.m_mapFilters[4].GetComponentInChildren<Text>().text = StaticDB.GetString("GOLD", "Gold");
			this.m_mapFilters[2].GetComponentInChildren<Text>().text = StaticDB.GetString("WAR_RESOURCES", "[PH]War Resources");
			this.m_mapFilters[5].GetComponentInChildren<Text>().text = StaticDB.GetString("PROFESSION_MATERIALS", "Profession Materials");
			this.m_mapFilters[6].GetComponentInChildren<Text>().text = StaticDB.GetString("PET_BATTLES", "[PH]Pet Battles");
			this.m_mapFilters[7].GetComponentInChildren<Text>().text = StaticDB.GetString("REPUTATION", "[PH]Reputation");
			this.m_mapFilters[8].GetComponentInChildren<Text>().text = this.GetQuestTitle(50562);
			this.m_mapFilters[9].GetComponentInChildren<Text>().text = this.GetQuestTitle(50598);
			this.m_mapFilters[10].GetComponentInChildren<Text>().text = this.GetQuestTitle(50599);
			this.m_mapFilters[11].GetComponentInChildren<Text>().text = this.GetQuestTitle(50600);
			this.m_mapFilters[12].GetComponentInChildren<Text>().text = this.GetQuestTitle(50601);
			this.m_mapFilters[13].GetComponentInChildren<Text>().text = this.GetQuestTitle(50602);
			this.m_mapFilters[14].GetComponentInChildren<Text>().text = this.GetQuestTitle(50603);
			this.m_mapFilters[15].GetComponentInChildren<Text>().text = this.GetQuestTitle(50604);
			this.m_mapFilters[16].GetComponentInChildren<Text>().text = this.GetQuestTitle(50605);
			this.m_mapFilters[17].GetComponentInChildren<Text>().text = this.GetQuestTitle(50606);
			this.SyncWithOptions();
		}

		private void OnEnable()
		{
			Main.instance.m_canvasBlurManager.AddBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PushBackAction(BackActionType.hideAllPopups, null);
			this.m_mapFilters[8].transform.parent.gameObject.SetActive(this.BountyIsActive(50562));
			this.m_mapFilters[9].transform.parent.gameObject.SetActive(this.BountyIsActive(50598));
			this.m_mapFilters[10].transform.parent.gameObject.SetActive(this.BountyIsActive(50599));
			this.m_mapFilters[11].transform.parent.gameObject.SetActive(this.BountyIsActive(50600));
			this.m_mapFilters[12].transform.parent.gameObject.SetActive(this.BountyIsActive(50601));
			this.m_mapFilters[13].transform.parent.gameObject.SetActive(this.BountyIsActive(50602));
			this.m_mapFilters[14].transform.parent.gameObject.SetActive(this.BountyIsActive(50603));
			this.m_mapFilters[15].transform.parent.gameObject.SetActive(this.BountyIsActive(50604));
			this.m_mapFilters[16].transform.parent.gameObject.SetActive(this.BountyIsActive(50605));
			this.m_mapFilters[17].transform.parent.gameObject.SetActive(this.BountyIsActive(50606));
		}

		private void OnDisable()
		{
			Main.instance.m_canvasBlurManager.RemoveBlurRef_MainCanvas();
			Main.instance.m_backButtonManager.PopBackAction();
		}

		private void OnValueChanged_EnableAll(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.All, isOn);
		}

		private void OnValueChanged_EnableAzerite(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Azerite, isOn);
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

		private void OnValueChanged_EnablePetBattles(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.PetBattles, isOn);
		}

		private void OnValueChanged_EnableReputations(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Reputation, isOn);
		}

		private void OnValueChanged_EnableBountyChampionsOfAzeroth(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_ChampionsOfAzeroth, isOn);
		}

		private void OnValueChanged_EnableBountyZandalariEmpire(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_ZandalariEmpire, isOn);
		}

		private void OnValueChanged_EnableBountyProudmooreAdmiralty(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_ProudmooreAdmiralty, isOn);
		}

		private void OnValueChanged_EnableBountyOrderOfEmbers(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_OrderOfEmbers, isOn);
		}

		private void OnValueChanged_EnableBountyStormsWake(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_StormsWake, isOn);
		}

		private void OnValueChanged_EnableBountyTalanjisExpedition(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_TalanjisExpedition, isOn);
		}

		private void OnValueChanged_EnableBountyVoldunai(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_Voldunai, isOn);
		}

		private void OnValueChanged_EnableBountyTortollanSeekers(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_TortollanSeekers, isOn);
		}

		private void OnValueChanged_EnableBountyAllianceWarEffort(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_AllianceWarEffort, isOn);
		}

		private void OnValueChanged_EnableBountyHordeWarEffort(bool isOn)
		{
			Main.instance.m_UISound.Play_ButtonBlackClick();
			AdventureMapPanel.instance.EnableMapFilter(MapFilterType.Bounty_HordeWarEffort, isOn);
		}

		public Toggle[] m_mapFilters;

		public GameObject m_FilterOptionsArea;
	}
}
