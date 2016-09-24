using System;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages.MobileClientJSON;

public class AllPopups : MonoBehaviour
{
	private void Awake()
	{
		AllPopups.instance = this;
	}

	private void Start()
	{
		this.HideAllPopups();
	}

	private void Update()
	{
	}

	public void HideAllPopups()
	{
		this.m_abilityInfoPopup.gameObject.SetActive(false);
		this.m_rewardInfoPopup.gameObject.SetActive(false);
		this.m_cheatCompleteMissionPopup.gameObject.SetActive(false);
		this.m_debugOptionsPopup.gameObject.SetActive(false);
		this.m_chatPopup.gameObject.SetActive(false);
		this.m_emissaryPopup.gameObject.SetActive(false);
		this.m_mechanicInfoPopup.gameObject.SetActive(false);
		this.m_optionsDialog.gameObject.SetActive(false);
		this.m_connectionPopup.gameObject.SetActive(false);
		this.m_bountyInfoTooltip.gameObject.SetActive(false);
		this.m_worldQuestTooltip.gameObject.SetActive(false);
		this.m_genericPopup.gameObject.SetActive(false);
		this.m_missionDescriptionTooltip.gameObject.SetActive(false);
		this.m_armamentDialog.gameObject.SetActive(false);
		this.m_equipmentDialog.gameObject.SetActive(false);
		this.m_championActivationConfirmationDialog.gameObject.SetActive(false);
		this.m_championDeactivationConfirmationDialog.gameObject.SetActive(false);
		this.m_partyBuffsPopup.gameObject.SetActive(false);
		this.m_unassignCombatAllyConfirmationDialog.gameObject.SetActive(false);
		this.m_logoutConfirmation.gameObject.SetActive(false);
		this.m_regionConfirmation.gameObject.SetActive(false);
		this.m_talentTooltip.gameObject.SetActive(false);
		this.HideCombatAllyDialog();
		this.m_encounterPopup.gameObject.SetActive(false);
	}

	public void ShowUnassignCombatAllyConfirmationDialog()
	{
		this.m_unassignCombatAllyConfirmationDialog.Show();
	}

	public void ShowPartyBuffsPopup(int[] buffIDs)
	{
		this.m_partyBuffsPopup.gameObject.SetActive(true);
		this.m_partyBuffsPopup.Init(buffIDs);
	}

	public void ShowEncounterPopup(int garrEncounterID, int garrMechanicID)
	{
		this.m_encounterPopup.SetEncounter(garrEncounterID, garrMechanicID);
		this.m_encounterPopup.gameObject.SetActive(true);
	}

	public void ShowCombatAllyDialog()
	{
		this.m_combatAllyDialog.gameObject.SetActive(true);
		this.m_combatAllyDialog.Init();
	}

	public void HideCombatAllyDialog()
	{
		this.m_combatAllyDialog.gameObject.SetActive(false);
	}

	public void ShowMissionDescriptionTooltip(int garrMissionID)
	{
		this.HideAllPopups();
		this.m_missionDescriptionTooltip.gameObject.SetActive(true);
		this.m_missionDescriptionTooltip.SetMission(garrMissionID);
	}

	public void ShowWorldQuestTooltip(int questID)
	{
		this.HideAllPopups();
		this.m_worldQuestTooltip.gameObject.SetActive(true);
		this.m_worldQuestTooltip.SetQuest(questID);
	}

	public void ShowBountyInfoTooltip(MobileWorldQuestBounty bounty)
	{
		this.HideAllPopups();
		this.m_bountyInfoTooltip.gameObject.SetActive(true);
		this.m_bountyInfoTooltip.SetBounty(bounty);
	}

	public void ShowRewardTooltip(MissionRewardDisplay.RewardType rewardType, int rewardID, int rewardQuantity, Image rewardImage, int itemContext)
	{
		this.m_rewardInfoPopup.gameObject.SetActive(true);
		this.m_rewardInfoPopup.SetReward(rewardType, rewardID, rewardQuantity, rewardImage.sprite, itemContext);
	}

	public void ShowAbilityInfoPopup(int garrAbilityID)
	{
		this.HideAllPopups();
		this.m_abilityInfoPopup.gameObject.SetActive(true);
		this.m_abilityInfoPopup.SetAbility(garrAbilityID);
	}

	public void ShowSpellInfoPopup(int spellID)
	{
		this.HideAllPopups();
		this.m_abilityInfoPopup.gameObject.SetActive(true);
		this.m_abilityInfoPopup.SetSpell(spellID);
	}

	public void ShowMechanicInfoPopup(Image mechanicImage, string mechanicName, string mechanicDescription)
	{
		this.HideAllPopups();
		this.m_mechanicInfoPopup.gameObject.SetActive(true);
		this.m_mechanicInfoPopup.m_mechanicIcon.sprite = mechanicImage.sprite;
		this.m_mechanicInfoPopup.m_mechanicIcon.overrideSprite = mechanicImage.overrideSprite;
		this.m_mechanicInfoPopup.m_mechanicName.text = mechanicName;
		this.m_mechanicInfoPopup.m_mechanicDescription.text = mechanicDescription;
	}

	public void ShowOptionsMenuPopup()
	{
		this.HideAllPopups();
		this.m_debugOptionsPopup.gameObject.SetActive(true);
		Main.instance.m_UISound.Play_ButtonBlackClick();
	}

	public void ShowOptionsDialog()
	{
		this.HideAllPopups();
		this.m_optionsDialog.gameObject.SetActive(true);
	}

	public void ShowChatPopup()
	{
		this.HideAllPopups();
		this.m_chatPopup.gameObject.SetActive(true);
	}

	public void ShowEmissaryPopup()
	{
		this.HideAllPopups();
		this.m_emissaryPopup.gameObject.SetActive(true);
		Main.instance.RequestEmissaryFactions();
	}

	public void EmissaryFactionUpdate(MobileClientEmissaryFactionUpdate msg)
	{
		this.m_emissaryPopup.FactionUpdate(msg);
	}

	public void OnClickConnectionPopupClose()
	{
		this.m_connectionPopup.gameObject.SetActive(false);
	}

	public void ShowTalentTooltip(TalentTreeItemAbilityButton abilityButton)
	{
		this.m_talentTooltip.gameObject.SetActive(true);
		this.m_talentTooltip.SetTalent(abilityButton);
	}

	public void ShowGenericPopup(string headerText, string descriptionText)
	{
		this.HideAllPopups();
		this.m_genericPopup.SetText(headerText, descriptionText);
		this.m_genericPopup.gameObject.SetActive(true);
	}

	public void ShowGenericPopupFull(string fullText)
	{
		this.HideAllPopups();
		this.m_genericPopup.SetFullText(fullText);
		this.m_genericPopup.gameObject.SetActive(true);
	}

	public void SetCurrentFollowerDetailView(FollowerDetailView followerDetailView)
	{
		this.m_currentFollowerDetailView = followerDetailView;
	}

	public FollowerDetailView GetCurrentFollowerDetailView()
	{
		return this.m_currentFollowerDetailView;
	}

	public void HideChampionUpgradeDialogs()
	{
		this.m_armamentDialog.gameObject.SetActive(false);
		this.m_equipmentDialog.gameObject.SetActive(false);
	}

	public void ShowArmamentDialog(FollowerDetailView followerDetailView, bool show)
	{
		if (show)
		{
			this.m_armamentDialog.Init(followerDetailView);
		}
		this.m_armamentDialog.gameObject.SetActive(show);
	}

	public void ShowEquipmentDialog(int garrAbilityID, FollowerDetailView followerDetailView, bool show)
	{
		if (show)
		{
			this.m_equipmentDialog.SetAbility(garrAbilityID, followerDetailView);
		}
		this.m_equipmentDialog.gameObject.SetActive(show);
	}

	public void ShowChampionActivationConfirmationDialog(FollowerDetailView followerDetailView)
	{
		this.m_championActivationConfirmationDialog.Show(followerDetailView);
	}

	public void ShowChampionDeactivationConfirmationDialog(FollowerDetailView followerDetailView)
	{
		this.m_championDeactivationConfirmationDialog.Show(followerDetailView);
	}

	public void ShowLogoutConfirmationPopup(bool goToWebAuth)
	{
		this.m_logoutConfirmation.GoToWebAuth = goToWebAuth;
		this.m_logoutConfirmation.gameObject.SetActive(true);
	}

	public void ShowRegionConfirmationPopup(int index)
	{
		this.m_regionConfirmation.gameObject.SetActive(true);
	}

	public static AllPopups instance;

	public AbilityInfoPopup m_abilityInfoPopup;

	public RewardInfoPopup m_rewardInfoPopup;

	public GameObject m_cheatCompleteMissionPopup;

	public DebugOptionsPopup m_debugOptionsPopup;

	public ChatPopup m_chatPopup;

	public EmissaryPopup m_emissaryPopup;

	public MechanicInfoPopup m_mechanicInfoPopup;

	public OptionsDialog m_optionsDialog;

	public ConnectionPopup m_connectionPopup;

	public TalentTooltip m_talentTooltip;

	public BountyInfoTooltip m_bountyInfoTooltip;

	public WorldQuestTooltip m_worldQuestTooltip;

	public GenericPopup m_genericPopup;

	public MissionDescriptionTooltip m_missionDescriptionTooltip;

	public ArmamentDialog m_armamentDialog;

	public EquipmentDialog m_equipmentDialog;

	public ActivationConfirmationDialog m_championActivationConfirmationDialog;

	public DeactivationConfirmationDialog m_championDeactivationConfirmationDialog;

	public CombatAllyDialog m_combatAllyDialog;

	public PartyBuffsPopup m_partyBuffsPopup;

	public UnassignCombatAllyConfirmationDialog m_unassignCombatAllyConfirmationDialog;

	public LogoutConfirmation m_logoutConfirmation;

	public RegionConfirmation m_regionConfirmation;

	public EncounterPopup m_encounterPopup;

	private FollowerDetailView m_currentFollowerDetailView;
}
