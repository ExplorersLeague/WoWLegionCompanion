using System;
using System.Linq;
using UnityEngine;
using WowStaticData;

namespace WoWCompanionApp
{
	public class StaticDB : Singleton<StaticDB>
	{
		public void InitDBs(AssetBundle nonLocalizedBundle, AssetBundle localizedBundle)
		{
			if (StaticDB.s_initialized)
			{
				Debug.Log("WARNING! ATTEMPTED TO INIT STATIC DBS THAT WERE ALREADY INITIALIZED!! IGNORING");
				return;
			}
			string path = "Assets/BundleAssets/StaticDB/";
			string locale = Main.instance.GetLocale();
			this.m_azeriteEmpoweredItemDB = new AzeriteEmpoweredItemDB();
			if (!this.m_azeriteEmpoweredItemDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load AzeriteEmpoweredItem static DB");
			}
			this.m_charShipmentDB = new CharShipmentDB();
			if (!this.m_charShipmentDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load charShipmentDB static DB");
			}
			this.m_charShipmentContainerDB = new CharShipmentContainerDB();
			if (!this.m_charShipmentContainerDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load charShipmentContainerDB static DB");
			}
			this.m_chrClassesDB = new ChrClassesDB();
			if (!this.m_chrClassesDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load chrClasses static DB");
			}
			this.m_communityIconDB = new CommunityIconDB();
			if (!this.m_communityIconDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load CommunityIcon static DB");
			}
			this.m_creatureDB = new CreatureDB();
			if (!this.m_creatureDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load creature static DB");
			}
			this.m_currencyTypesDB = new CurrencyTypesDB();
			if (!this.m_currencyTypesDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load currencyTypes static DB");
			}
			this.m_currencyContainerDB = new CurrencyContainerDB();
			if (!this.m_currencyContainerDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load currencyContainer static DB");
			}
			this.m_factionDB = new FactionDB();
			if (!this.m_factionDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load faction static DB");
			}
			this.m_garrAbilityDB = new GarrAbilityDB();
			if (!this.m_garrAbilityDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrAbility static DB");
			}
			this.m_garrAbilityCategoryDB = new GarrAbilityCategoryDB();
			if (!this.m_garrAbilityCategoryDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrAbilityCategory static DB");
			}
			this.m_garrAbilityEffectDB = new GarrAbilityEffectDB();
			if (!this.m_garrAbilityEffectDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrAbilityEffect static DB");
			}
			this.m_garrClassSpecDB = new GarrClassSpecDB();
			if (!this.m_garrClassSpecDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrClassSpec static DB");
			}
			this.m_garrEncounterDB = new GarrEncounterDB();
			if (!this.m_garrEncounterDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrEncounter static DB");
			}
			this.m_garrEncounterSetXEncounterDB = new GarrEncounterSetXEncounterDB();
			if (!this.m_garrEncounterSetXEncounterDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrEncounterSetXEncounterDB static DB");
			}
			this.m_garrFollItemSetMemberDB = new GarrFollItemSetMemberDB();
			if (!this.m_garrFollItemSetMemberDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrFollItemSetMember static DB");
			}
			this.m_garrFollowerDB = new GarrFollowerDB();
			if (!this.m_garrFollowerDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrFollower static DB");
			}
			this.m_garrFollowerLevelXPDB = new GarrFollowerLevelXPDB();
			if (!this.m_garrFollowerLevelXPDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrFollowerLevelXP static DB");
			}
			this.m_garrFollowerTypeDB = new GarrFollowerTypeDB();
			if (!this.m_garrFollowerTypeDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load m_garrFollowerType static DB");
			}
			this.m_garrFollowerQualityDB = new GarrFollowerQualityDB();
			if (!this.m_garrFollowerQualityDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load m_garrFollowerQuality static DB");
			}
			this.m_garrFollowerXAbilityDB = new GarrFollowerXAbilityDB();
			if (!this.m_garrFollowerXAbilityDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load m_garrFollowerXAbilty static DB");
			}
			this.m_garrMechanicDB = new GarrMechanicDB();
			if (!this.m_garrMechanicDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrMechanic static DB");
			}
			this.m_garrMechanicTypeDB = new GarrMechanicTypeDB();
			if (!this.m_garrMechanicTypeDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrMechanicType static DB");
			}
			this.m_garrMissionDB = new GarrMissionDB();
			if (!this.m_garrMissionDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrMission static DB");
			}
			this.m_garrMissionRewardDB = new GarrMissionRewardDB();
			if (!this.m_garrMissionRewardDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrMissionReward static DB");
			}
			this.m_garrMissionTypeDB = new GarrMissionTypeDB();
			if (!this.m_garrMissionTypeDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrMissionType static DB");
			}
			this.m_garrMissionXEncounterDB = new GarrMissionXEncounterDB();
			if (!this.m_garrMissionXEncounterDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrMissionXEncounter static DB");
			}
			this.m_garrStringDB = new GarrStringDB();
			if (!this.m_garrStringDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrStringDB static DB");
			}
			this.m_garrTalentDB = new GarrTalentDB();
			if (!this.m_garrTalentDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrTalent static DB");
			}
			this.m_garrTalentTreeDB = new GarrTalentTreeDB();
			if (!this.m_garrTalentTreeDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load garrTalentTree static DB");
			}
			this.m_itemDB = new ItemDB();
			if (!this.m_itemDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load item static DB");
			}
			this.m_itemSubClassDB = new ItemSubClassDB();
			if (!this.m_itemSubClassDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load item sub class static DB");
			}
			this.m_itemEffectDB = new ItemEffectDB();
			if (!this.m_itemEffectDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load item effect static DB");
			}
			this.m_itemNameDescriptionDB = new ItemNameDescriptionDB();
			if (!this.m_itemNameDescriptionDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load item name description DB");
			}
			this.m_mobileStringsDB = new MobileStringsDB();
			if (!this.m_mobileStringsDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load MobileStrings static DB");
			}
			this.m_questDB = new QuestV2DB();
			if (!this.m_questDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load Quest static DB");
			}
			this.m_questInfoDB = new QuestInfoDB();
			if (!this.m_questInfoDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load QuestInfo static DB");
			}
			this.m_spellEffectDB = new SpellEffectDB();
			if (!this.m_spellEffectDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load SpellEffect static DB");
			}
			this.m_spellTooltipDB = new SpellTooltipDB();
			if (!this.m_spellTooltipDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load SpellTooltip static DB");
			}
			this.m_vw_mobileSpellDB = new VW_MobileSpellDB();
			if (!this.m_vw_mobileSpellDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load VW_MobileSpellDB static DB");
			}
			this.m_uiTextureAtlasMemberDB = new UiTextureAtlasMemberDB();
			if (!this.m_uiTextureAtlasMemberDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load uiTextureAtlasMember static DB");
			}
			this.m_uiTextureAtlasDB = new UiTextureAtlasDB();
			if (!this.m_uiTextureAtlasDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load uiTextureAtlas static DB");
			}
			this.m_uiTextureKitDB = new UiTextureKitDB();
			if (!this.m_uiTextureKitDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load uiTextureKit static DB");
			}
			this.m_worldMapAreaDB = new WorldMapAreaDB();
			if (!this.m_worldMapAreaDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load worldMapArea static DB");
			}
			this.m_rewardPackDB = new RewardPackDB();
			if (!this.m_rewardPackDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load rewardPack static DB");
			}
			this.m_rewardPackXItemDB = new RewardPackXItemDB();
			if (!this.m_rewardPackXItemDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load rewardPackXItem static DB");
			}
			this.m_rewardPackXCurrencyTypeDB = new RewardPackXCurrencyTypeDB();
			if (!this.m_rewardPackXCurrencyTypeDB.Load(path, nonLocalizedBundle, localizedBundle, locale))
			{
				Debug.Log("Failed to load rewardPackXCurrencyType static DB");
			}
			if (localizedBundle != null)
			{
				localizedBundle.Unload(true);
			}
			if (nonLocalizedBundle != null)
			{
				nonLocalizedBundle.Unload(true);
			}
			StaticDB.s_initialized = true;
		}

		public static AzeriteEmpoweredItemDB azeriteEmpoweredItemDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_azeriteEmpoweredItemDB;
			}
		}

		public static CharShipmentDB charShipmentDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_charShipmentDB;
			}
		}

		public static CharShipmentContainerDB charShipmentContainerDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_charShipmentContainerDB;
			}
		}

		public static ChrClassesDB chrClassesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_chrClassesDB;
			}
		}

		public static CommunityIconDB communityIconDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_communityIconDB;
			}
		}

		public static CreatureDB creatureDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_creatureDB;
			}
		}

		public static CurrencyTypesDB currencyTypesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_currencyTypesDB;
			}
		}

		public static CurrencyContainerDB currencyContainerDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_currencyContainerDB;
			}
		}

		public static FactionDB factionDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_factionDB;
			}
		}

		public static GarrAbilityDB garrAbilityDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrAbilityDB;
			}
		}

		public static GarrAbilityCategoryDB garrAbilityCategoryDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrAbilityCategoryDB;
			}
		}

		public static GarrAbilityEffectDB garrAbilityEffectDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrAbilityEffectDB;
			}
		}

		public static GarrClassSpecDB garrClassSpecDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrClassSpecDB;
			}
		}

		public static GarrEncounterSetXEncounterDB garrEncounterSetXEncounterDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrEncounterSetXEncounterDB;
			}
		}

		public static GarrEncounterDB garrEncounterDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrEncounterDB;
			}
		}

		public static GarrFollItemSetMemberDB garrFollItemSetMemberDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrFollItemSetMemberDB;
			}
		}

		public static GarrFollowerDB garrFollowerDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrFollowerDB;
			}
		}

		public static GarrFollowerLevelXPDB garrFollowerLevelXPDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrFollowerLevelXPDB;
			}
		}

		public static GarrFollowerTypeDB garrFollowerTypeDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrFollowerTypeDB;
			}
		}

		public static GarrFollowerQualityDB garrFollowerQualityDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrFollowerQualityDB;
			}
		}

		public static GarrFollowerXAbilityDB garrFollowerXAbilityDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrFollowerXAbilityDB;
			}
		}

		public static GarrMechanicDB garrMechanicDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMechanicDB;
			}
		}

		public static GarrMechanicTypeDB garrMechanicTypeDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMechanicTypeDB;
			}
		}

		public static GarrMissionDB garrMissionDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMissionDB;
			}
		}

		public static GarrMissionRewardDB garrMissionRewardDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMissionRewardDB;
			}
		}

		public static GarrMissionTypeDB garrMissionTypeDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMissionTypeDB;
			}
		}

		public static GarrMissionXEncounterDB garrMissionXEncounterDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMissionXEncounterDB;
			}
		}

		public static GarrStringDB garrStringDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrStringDB;
			}
		}

		public static GarrTalentDB garrTalentDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrTalentDB;
			}
		}

		public static GarrTalentTreeDB garrTalentTreeDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrTalentTreeDB;
			}
		}

		public static ItemDB itemDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_itemDB;
			}
		}

		public static ItemSubClassDB itemSubClassDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_itemSubClassDB;
			}
		}

		public static ItemSubClassRec GetItemSubclass(int classID, int subClassID)
		{
			return StaticDB.itemSubClassDB.GetRecordsByParentID(classID).FirstOrDefault((ItemSubClassRec subClassRec) => subClassRec.SubClassID == subClassID);
		}

		public static ItemEffectDB itemEffectDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_itemEffectDB;
			}
		}

		public static ItemNameDescriptionDB itemNameDescriptionDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_itemNameDescriptionDB;
			}
		}

		public static QuestV2DB questDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_questDB;
			}
		}

		public static QuestInfoDB questInfoDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_questInfoDB;
			}
		}

		public static SpellEffectDB spellEffectDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_spellEffectDB;
			}
		}

		public static SpellTooltipDB spellTooltipDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_spellTooltipDB;
			}
		}

		public static VW_MobileSpellDB vw_mobileSpellDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_vw_mobileSpellDB;
			}
		}

		public static bool StringsAvailable()
		{
			return Singleton<StaticDB>.instance != null && Singleton<StaticDB>.instance.m_mobileStringsDB != null;
		}

		public static string GetString(string baseTag, string fallbackString = null)
		{
			if (Singleton<StaticDB>.instance == null || Singleton<StaticDB>.instance.m_mobileStringsDB == null)
			{
				return fallbackString ?? "<NoStringsLoaded>";
			}
			MobileStringsRec record = Singleton<StaticDB>.instance.m_mobileStringsDB.GetRecord(baseTag);
			if (record == null)
			{
				Debug.Log("No rec for tag " + baseTag);
				return fallbackString ?? "<NoRec>";
			}
			if (record.TagText == string.Empty)
			{
				return fallbackString ?? "<NoTxt>";
			}
			return record.TagText;
		}

		public static UiTextureAtlasDB uiTextureAtlasDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_uiTextureAtlasDB;
			}
		}

		public static UiTextureAtlasMemberDB uiTextureAtlasMemberDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_uiTextureAtlasMemberDB;
			}
		}

		public static UiTextureKitDB uiTextureKitDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_uiTextureKitDB;
			}
		}

		public static WorldMapAreaDB worldMapAreaDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_worldMapAreaDB;
			}
		}

		public static RewardPackDB rewardPackDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_rewardPackDB;
			}
		}

		public static RewardPackXItemDB rewardPackXItemDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_rewardPackXItemDB;
			}
		}

		public static RewardPackXCurrencyTypeDB rewardPackXCurrencyTypeDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_rewardPackXCurrencyTypeDB;
			}
		}

		private static bool s_initialized;

		private AzeriteEmpoweredItemDB m_azeriteEmpoweredItemDB;

		private CharShipmentDB m_charShipmentDB;

		private CharShipmentContainerDB m_charShipmentContainerDB;

		private ChrClassesDB m_chrClassesDB;

		private CommunityIconDB m_communityIconDB;

		private CreatureDB m_creatureDB;

		private CurrencyTypesDB m_currencyTypesDB;

		private CurrencyContainerDB m_currencyContainerDB;

		private FactionDB m_factionDB;

		private GarrAbilityDB m_garrAbilityDB;

		private GarrAbilityCategoryDB m_garrAbilityCategoryDB;

		private GarrAbilityEffectDB m_garrAbilityEffectDB;

		private GarrClassSpecDB m_garrClassSpecDB;

		private GarrEncounterDB m_garrEncounterDB;

		private GarrEncounterSetXEncounterDB m_garrEncounterSetXEncounterDB;

		private GarrFollItemSetMemberDB m_garrFollItemSetMemberDB;

		private GarrFollowerDB m_garrFollowerDB;

		private GarrFollowerLevelXPDB m_garrFollowerLevelXPDB;

		private GarrFollowerTypeDB m_garrFollowerTypeDB;

		private GarrFollowerQualityDB m_garrFollowerQualityDB;

		private GarrFollowerXAbilityDB m_garrFollowerXAbilityDB;

		private GarrMechanicDB m_garrMechanicDB;

		private GarrMechanicTypeDB m_garrMechanicTypeDB;

		private GarrMissionDB m_garrMissionDB;

		private GarrMissionRewardDB m_garrMissionRewardDB;

		private GarrMissionTypeDB m_garrMissionTypeDB;

		private GarrMissionXEncounterDB m_garrMissionXEncounterDB;

		private GarrStringDB m_garrStringDB;

		private GarrTalentDB m_garrTalentDB;

		private GarrTalentTreeDB m_garrTalentTreeDB;

		private ItemDB m_itemDB;

		private ItemSubClassDB m_itemSubClassDB;

		private ItemEffectDB m_itemEffectDB;

		private ItemNameDescriptionDB m_itemNameDescriptionDB;

		private MobileStringsDB m_mobileStringsDB;

		private QuestV2DB m_questDB;

		private QuestInfoDB m_questInfoDB;

		private SpellEffectDB m_spellEffectDB;

		private SpellTooltipDB m_spellTooltipDB;

		private VW_MobileSpellDB m_vw_mobileSpellDB;

		private UiTextureAtlasDB m_uiTextureAtlasDB;

		private UiTextureAtlasMemberDB m_uiTextureAtlasMemberDB;

		private UiTextureKitDB m_uiTextureKitDB;

		private WorldMapAreaDB m_worldMapAreaDB;

		private RewardPackDB m_rewardPackDB;

		private RewardPackXItemDB m_rewardPackXItemDB;

		private RewardPackXCurrencyTypeDB m_rewardPackXCurrencyTypeDB;
	}
}
