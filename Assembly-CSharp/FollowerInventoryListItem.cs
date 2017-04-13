using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class FollowerInventoryListItem : MonoBehaviour
{
	private void Awake()
	{
		if (this.m_combatAllyUnavailableDarkener != null)
		{
			this.m_combatAllyUnavailableDarkener.gameObject.SetActive(false);
		}
		this.m_expandableAreaCanvasGroup.interactable = false;
		if (this.m_equipmentName != null)
		{
			this.m_equipmentName.font = GeneralHelpers.LoadStandardFont();
		}
		if (this.m_equipmentQuantity != null)
		{
			this.m_equipmentQuantity.font = GeneralHelpers.LoadStandardFont();
		}
		if (this.m_equipmentDescription != null)
		{
			this.m_equipmentDescription.font = GeneralHelpers.LoadStandardFont();
		}
		if (this.m_combatAllyName != null)
		{
			this.m_combatAllyName.font = GeneralHelpers.LoadStandardFont();
		}
		if (this.m_combatAllyStatus != null)
		{
			this.m_combatAllyStatus.font = GeneralHelpers.LoadStandardFont();
		}
		if (this.m_useItemButtonLabel != null)
		{
			this.m_useItemButtonLabel.font = GeneralHelpers.LoadStandardFont();
		}
		if (FollowerInventoryListItem.m_inactiveString == null)
		{
			FollowerInventoryListItem.m_inactiveString = StaticDB.GetString("INACTIVE", "Inactive PH");
		}
		if (FollowerInventoryListItem.m_onMissionString == null)
		{
			FollowerInventoryListItem.m_onMissionString = StaticDB.GetString("ON_MISSION", "On Mission PH");
		}
		if (FollowerInventoryListItem.m_fatiguedString == null)
		{
			FollowerInventoryListItem.m_fatiguedString = StaticDB.GetString("FATIGUED", "Fatigued PH");
		}
		if (FollowerInventoryListItem.m_inBuildingString == null)
		{
			FollowerInventoryListItem.m_inBuildingString = StaticDB.GetString("IN_BUILDING", "In Building PH");
		}
		if (FollowerInventoryListItem.m_maxiLevelString == null)
		{
			FollowerInventoryListItem.m_maxiLevelString = StaticDB.GetString("MAX_ILVL", "Max iLvl PH");
		}
	}

	public bool IsEquipment()
	{
		return this.m_equipmentItem != null;
	}

	public bool IsArmament()
	{
		return this.m_armamentItem != null;
	}

	public void SetEquipment(MobileFollowerEquipment item, FollowerDetailView followerDetailView, int abilityToReplace)
	{
		this.m_abilityToReplace = abilityToReplace;
		this.m_equipmentItem = item;
		this.m_followerDetailView = followerDetailView;
		ItemRec record = StaticDB.itemDB.GetRecord(item.ItemID);
		this.m_equipmentName.text = GeneralHelpers.GetItemQualityColorTag(record.OverallQualityID) + record.Display + "</color>";
		GarrAbilityRec record2 = StaticDB.garrAbilityDB.GetRecord(item.GarrAbilityID);
		if (record2 != null)
		{
			this.m_equipmentDescription.text = record2.Description;
		}
		else
		{
			SpellTooltipRec record3 = StaticDB.spellTooltipDB.GetRecord(item.SpellID);
			if (record3 != null)
			{
				this.m_equipmentDescription.text = record3.Description;
			}
			else
			{
				this.m_equipmentDescription.text = string.Concat(new object[]
				{
					"ERROR. Ability ID:",
					item.GarrAbilityID,
					" Spell ID: ",
					item.SpellID,
					" Item ID:",
					item.ItemID
				});
			}
		}
		this.m_equipmentDescription.text = GeneralHelpers.LimitZhLineLength(WowTextParser.parser.Parse(this.m_equipmentDescription.text, 0), 18);
		this.m_equipmentDescription.supportRichText = WowTextParser.parser.IsRichText();
		if (this.m_iconErrorText != null)
		{
			this.m_iconErrorText.gameObject.SetActive(false);
		}
		Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
		if (sprite != null)
		{
			this.m_equipmentIcon.sprite = sprite;
		}
		else if (this.m_iconErrorText != null)
		{
			this.m_iconErrorText.gameObject.SetActive(true);
			this.m_iconErrorText.text = string.Empty + record.IconFileDataID;
		}
		this.m_equipmentQuantity.text = ((item.Quantity <= 1) ? string.Empty : (string.Empty + item.Quantity));
		JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[this.m_followerDetailView.GetCurrentFollower()];
		if (jamGarrisonFollower != null && jamGarrisonFollower.CurrentMissionID != 0)
		{
			this.m_useItemButtonLabel.text = StaticDB.GetString("ON_MISSION", null);
			this.m_useItemButtonLabel.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.m_useItemButton.interactable = false;
		}
		else
		{
			this.m_useItemButtonLabel.text = StaticDB.GetString("USE_ITEM", null);
		}
	}

	public void SetArmament(MobileFollowerArmamentExt item, FollowerDetailView followerDetailView)
	{
		this.m_armamentItem = item;
		this.m_followerDetailView = followerDetailView;
		ItemRec record = StaticDB.itemDB.GetRecord(item.ItemID);
		if (record != null)
		{
			this.m_equipmentName.text = GeneralHelpers.GetItemQualityColorTag(record.OverallQualityID) + record.Display + "</color>";
		}
		else
		{
			this.m_equipmentName.text = "Unknown Item " + item.ItemID;
		}
		SpellTooltipRec record2 = StaticDB.spellTooltipDB.GetRecord(item.SpellID);
		if (record2 != null)
		{
			this.m_equipmentDescription.text = record2.Description;
		}
		else
		{
			this.m_equipmentDescription.text = string.Concat(new object[]
			{
				"ERROR. Unknown Spell ID: ",
				item.SpellID,
				" Item ID:",
				item.ItemID
			});
		}
		this.m_equipmentDescription.text = WowTextParser.parser.Parse(this.m_equipmentDescription.text, item.SpellID);
		this.m_equipmentDescription.supportRichText = WowTextParser.parser.IsRichText();
		if (this.m_iconErrorText != null)
		{
			this.m_iconErrorText.gameObject.SetActive(false);
		}
		if (record != null)
		{
			Sprite sprite = GeneralHelpers.LoadIconAsset(AssetBundleType.Icons, record.IconFileDataID);
			if (sprite != null)
			{
				this.m_equipmentIcon.sprite = sprite;
			}
			else if (this.m_iconErrorText != null)
			{
				this.m_iconErrorText.gameObject.SetActive(true);
				this.m_iconErrorText.text = string.Empty + record.IconFileDataID;
			}
		}
		this.m_equipmentQuantity.text = ((item.Quantity <= 1) ? string.Empty : (string.Empty + item.Quantity));
		JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[this.m_followerDetailView.GetCurrentFollower()];
		bool flag = false;
		if (jamGarrisonFollower != null && jamGarrisonFollower.CurrentMissionID != 0)
		{
			GarrMissionRec record3 = StaticDB.garrMissionDB.GetRecord(jamGarrisonFollower.CurrentMissionID);
			if (record3 != null && (record3.Flags & 16u) != 0u)
			{
				flag = true;
			}
		}
		int num = (jamGarrisonFollower.ItemLevelArmor + jamGarrisonFollower.ItemLevelWeapon) / 2;
		bool flag2 = num >= item.MinItemLevel && num < item.MaxItemLevel;
		if (jamGarrisonFollower != null && jamGarrisonFollower.CurrentMissionID != 0 && !flag)
		{
			this.m_useItemButtonLabel.text = StaticDB.GetString("ON_MISSION", null);
			this.m_useItemButtonLabel.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.m_useItemButton.interactable = false;
		}
		else if (!flag2 || (long)num >= (long)((ulong)GeneralHelpers.GetMaxFollowerItemLevel()))
		{
			this.m_useItemButtonLabel.text = FollowerInventoryListItem.m_maxiLevelString;
			this.m_useItemButtonLabel.color = new Color(0.5f, 0.5f, 0.5f, 1f);
			this.m_useItemButton.interactable = false;
		}
		else
		{
			this.m_useItemButtonLabel.text = StaticDB.GetString("USE_ITEM", null);
		}
	}

	private void SetCombatAllyAvailabilityStatus()
	{
		bool flag = (this.m_combatAllyChampion.Flags & 4) != 0;
		bool flag2 = (this.m_combatAllyChampion.Flags & 2) != 0;
		bool flag3 = this.m_combatAllyChampion.CurrentMissionID != 0;
		bool flag4 = this.m_combatAllyChampion.CurrentBuildingID != 0;
		bool flag5 = !flag && !flag2 && !flag3 && !flag4;
		if (flag5)
		{
			this.m_combatAllyStatus.gameObject.SetActive(false);
			this.m_combatAllyUnavailableDarkener.gameObject.SetActive(false);
			this.m_mainButton.enabled = true;
			this.m_expandArrow.SetActive(true);
			Debug.Log("Available");
			return;
		}
		this.m_combatAllyStatus.gameObject.SetActive(true);
		this.m_combatAllyUnavailableDarkener.gameObject.SetActive(true);
		this.m_mainButton.enabled = false;
		this.m_expandArrow.SetActive(false);
		if (flag)
		{
			this.m_combatAllyStatus.color = Color.red;
			this.m_combatAllyStatus.text = FollowerInventoryListItem.m_inactiveString;
			this.m_combatAllyUnavailableDarkener.color = new Color(1f, 0.4f, 0.4f, 1f);
		}
		else if (flag2)
		{
			this.m_combatAllyStatus.text = FollowerInventoryListItem.m_fatiguedString;
			this.m_combatAllyUnavailableDarkener.color = new Color(0.4f, 0.4f, 1f, 1f);
		}
		else if (flag3)
		{
			this.m_combatAllyStatus.text = FollowerInventoryListItem.m_onMissionString;
			this.m_combatAllyUnavailableDarkener.color = new Color(0.4f, 0.4f, 1f, 1f);
		}
		else if (flag4)
		{
			this.m_combatAllyStatus.text = FollowerInventoryListItem.m_inBuildingString;
			this.m_combatAllyUnavailableDarkener.color = new Color(0.4f, 0.4f, 1f, 1f);
		}
	}

	public void SetCombatAllyChampion(JamGarrisonFollower follower, int garrMissionID, int missionCost)
	{
		this.m_combatAllyMissionID = garrMissionID;
		this.m_combatAllyChampion = follower;
		this.m_combatAllyAbilityDisplay.SetSpell(this.m_combatAllyChampion.ZoneSupportSpellID);
		this.m_combatAllySlot.SetFollower(follower.GarrFollowerID);
		GarrFollowerRec record = StaticDB.garrFollowerDB.GetRecord(follower.GarrFollowerID);
		CreatureRec record2 = StaticDB.creatureDB.GetRecord((GarrisonStatus.Faction() != PVP_FACTION.ALLIANCE) ? record.HordeCreatureID : record.AllianceCreatureID);
		if (follower.Quality == 6 && record.TitleName != null && record.TitleName.Length > 0)
		{
			this.m_combatAllyName.text = record.TitleName;
		}
		else if (record != null)
		{
			this.m_combatAllyName.text = record2.Name;
		}
		this.m_combatAllyName.color = GeneralHelpers.GetQualityColor(this.m_combatAllyChampion.Quality);
		if (missionCost <= GarrisonStatus.Resources())
		{
			this.m_useItemButtonLabel.text = StaticDB.GetString("ASSIGN_CHAMPION", null);
		}
		else
		{
			this.m_useItemButtonLabel.text = StaticDB.GetString("CANT_AFFORD", "Can't Afford");
			this.m_useItemButtonLabel.color = Color.red;
			this.m_useItemButton.interactable = false;
		}
		int numActiveChampions = GeneralHelpers.GetNumActiveChampions();
		int maxActiveFollowers = GarrisonStatus.GetMaxActiveFollowers();
		this.m_isOverMaxChampionSoftCap = false;
		this.m_needMoreResources = false;
		if (numActiveChampions > maxActiveFollowers)
		{
			this.m_isOverMaxChampionSoftCap = true;
		}
		if (GarrisonStatus.Resources() < missionCost)
		{
			this.m_needMoreResources = true;
		}
		this.SetCombatAllyAvailabilityStatus();
	}

	private void UpdateExpandableHeightCallback(float val)
	{
		this.m_expandableAreaLayoutElement.minHeight = val;
	}

	private void FinishExpandingCallback()
	{
		this.m_expandableAreaLayoutElement.minHeight = (float)this.m_expandableAreaMaxHeight;
		this.m_expandableAreaCanvasGroup.interactable = true;
		this.m_isExpanded = true;
	}

	private void FinishContractingCallback()
	{
		this.m_expandableAreaLayoutElement.minHeight = 0f;
		this.m_isExpanded = false;
	}

	private void UpdateExpandableAreaAlpha(float val)
	{
		this.m_expandableAreaCanvasGroup.alpha = val;
	}

	private void FinishFadingExpandableAreaOut()
	{
		this.m_expandableAreaCanvasGroup.alpha = 0f;
	}

	private void FinishFadingExpandableAreaIn()
	{
		this.m_expandableAreaCanvasGroup.alpha = 1f;
	}

	private void AssignCombatAlly()
	{
		if (this.m_isOverMaxChampionSoftCap || this.m_needMoreResources)
		{
			if (this.m_isOverMaxChampionSoftCap)
			{
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("TOO_MANY_CHAMPIONS", null));
			}
			else if (this.m_needMoreResources)
			{
				AllPopups.instance.ShowGenericPopupFull(StaticDB.GetString("NEED_MORE_RESOURCES", null));
			}
			return;
		}
		Main.instance.m_UISound.Play_StartMission();
		List<ulong> list = new List<ulong>();
		list.Add(this.m_combatAllyChampion.DbID);
		Main.instance.StartMission(this.m_combatAllyMissionID, list.ToArray());
	}

	public void HandleTapUseItemButton()
	{
		if (this.m_armamentItem != null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Attempting to use armament item ",
				this.m_armamentItem.ItemID,
				" for follower ",
				this.m_followerDetailView.GetCurrentFollower()
			}));
			Main.instance.UseArmament(this.m_followerDetailView.GetCurrentFollower(), this.m_armamentItem.ItemID);
			AllPopups.instance.ShowArmamentDialog(null, false);
		}
		else if (this.m_equipmentItem != null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Attempting to use equipment item ",
				this.m_equipmentItem.ItemID,
				" for follower ",
				this.m_followerDetailView.GetCurrentFollower()
			}));
			Main.instance.UseEquipment(this.m_followerDetailView.GetCurrentFollower(), this.m_equipmentItem.ItemID, this.m_abilityToReplace);
			AllPopups.instance.ShowEquipmentDialog(0, null, false);
		}
		else if (this.m_combatAllyChampion != null)
		{
			this.AssignCombatAlly();
			AllPopups.instance.HideCombatAllyDialog();
		}
		Main.instance.m_UISound.Play_ButtonRedClick();
	}

	public void HandleTapMainListItem()
	{
		Main.instance.m_UISound.Play_ButtonBlackClick();
		if (!this.m_isExpanded)
		{
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Expand",
				"from",
				this.m_expandableAreaLayoutElement.minHeight,
				"to",
				this.m_expandableAreaMaxHeight,
				"easeType",
				"easeOutCubic",
				"time",
				this.m_expandDuration,
				"onupdate",
				"UpdateExpandableHeightCallback",
				"oncomplete",
				"FinishExpandingCallback"
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FadeIn",
				"from",
				this.m_expandableAreaCanvasGroup.alpha,
				"to",
				1f,
				"easeType",
				"easeOutCubic",
				"time",
				this.m_expandDuration,
				"onupdate",
				"UpdateExpandableAreaAlpha",
				"oncomplete",
				"FinishFadingExpandableAreaIn"
			}));
			this.m_expandableAreaFancyEntrance.Reset();
		}
		else
		{
			this.m_expandableAreaCanvasGroup.interactable = false;
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"Contract",
				"from",
				this.m_expandableAreaLayoutElement.minHeight,
				"to",
				0,
				"easeType",
				"easeOutCubic",
				"time",
				this.m_contractDuration,
				"onupdate",
				"UpdateExpandableHeightCallback",
				"oncomplete",
				"FinishContractingCallback"
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FadeOut",
				"from",
				this.m_expandableAreaCanvasGroup.alpha,
				"to",
				0f,
				"easeType",
				"easeOutCubic",
				"time",
				this.m_contractDuration * 0.5f,
				"onupdate",
				"UpdateExpandableAreaAlpha",
				"oncomplete",
				"FinishFadingExpandableAreaOut"
			}));
		}
	}

	public void SetHeaderText(string text)
	{
		this.m_equipmentName.text = text;
	}

	[Header("Equipment Item")]
	public Image m_equipmentIcon;

	public Text m_equipmentName;

	public Text m_equipmentQuantity;

	public Text m_equipmentDescription;

	[Header("Combat Ally")]
	public SpellDisplay m_combatAllyAbilityDisplay;

	public MissionFollowerSlot m_combatAllySlot;

	public Text m_combatAllyName;

	public Text m_combatAllyStatus;

	public Image m_combatAllyUnavailableDarkener;

	public Button m_mainButton;

	[Header("Expandable Use Item Area")]
	public LayoutElement m_expandableAreaLayoutElement;

	public CanvasGroup m_expandableAreaCanvasGroup;

	public Button m_useItemButton;

	public Text m_useItemButtonLabel;

	public FancyEntrance m_expandableAreaFancyEntrance;

	public int m_expandableAreaMaxHeight;

	public float m_expandDuration;

	public float m_contractDuration;

	public GameObject m_expandArrow;

	[Header("Error reporting")]
	public Text m_iconErrorText;

	private bool m_isExpanded;

	private FollowerDetailView m_followerDetailView;

	private MobileFollowerArmamentExt m_armamentItem;

	private MobileFollowerEquipment m_equipmentItem;

	private int m_abilityToReplace;

	private JamGarrisonFollower m_combatAllyChampion;

	private int m_combatAllyMissionID;

	private bool m_isOverMaxChampionSoftCap;

	private bool m_needMoreResources;

	private static string m_inactiveString;

	private static string m_onMissionString;

	private static string m_fatiguedString;

	private static string m_inBuildingString;

	private static string m_maxiLevelString;
}
