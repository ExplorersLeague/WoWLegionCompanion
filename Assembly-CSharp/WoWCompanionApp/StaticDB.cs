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
			this.m_azeriteEmpoweredItemDB = StaticDB.InitDB<AzeriteEmpoweredItemDB, int, AzeriteEmpoweredItemRec>(nonLocalizedBundle, localizedBundle);
			this.m_cfg_LanguagesDB = StaticDB.InitDB<Cfg_LanguagesDB, int, Cfg_LanguagesRec>(nonLocalizedBundle, localizedBundle);
			this.m_cfg_RealmsDB = StaticDB.InitDB<Cfg_RealmsDB, int, Cfg_RealmsRec>(nonLocalizedBundle, localizedBundle);
			this.m_cfg_RegionsDB = StaticDB.InitDB<Cfg_RegionsDB, int, Cfg_RegionsRec>(nonLocalizedBundle, localizedBundle);
			this.m_charShipmentDB = StaticDB.InitDB<CharShipmentDB, int, CharShipmentRec>(nonLocalizedBundle, localizedBundle);
			this.m_charShipmentContainerDB = StaticDB.InitDB<CharShipmentContainerDB, int, CharShipmentContainerRec>(nonLocalizedBundle, localizedBundle);
			this.m_chatProfanityDB = StaticDB.InitDB<ChatProfanityDB, int, ChatProfanityRec>(nonLocalizedBundle, localizedBundle);
			this.m_chrClassesDB = StaticDB.InitDB<ChrClassesDB, int, ChrClassesRec>(nonLocalizedBundle, localizedBundle);
			this.m_chrRacesDB = StaticDB.InitDB<ChrRacesDB, int, ChrRacesRec>(nonLocalizedBundle, localizedBundle);
			this.m_communityIconDB = StaticDB.InitDB<CommunityIconDB, int, CommunityIconRec>(nonLocalizedBundle, localizedBundle);
			this.m_creatureDB = StaticDB.InitDB<CreatureDB, int, CreatureRec>(nonLocalizedBundle, localizedBundle);
			this.m_currencyTypesDB = StaticDB.InitDB<CurrencyTypesDB, int, CurrencyTypesRec>(nonLocalizedBundle, localizedBundle);
			this.m_currencyContainerDB = StaticDB.InitDB<CurrencyContainerDB, int, CurrencyContainerRec>(nonLocalizedBundle, localizedBundle);
			this.m_difficultyDB = StaticDB.InitDB<DifficultyDB, int, DifficultyRec>(nonLocalizedBundle, localizedBundle);
			this.m_factionDB = StaticDB.InitDB<FactionDB, int, FactionRec>(nonLocalizedBundle, localizedBundle);
			this.m_factionTemplateDB = StaticDB.InitDB<FactionTemplateDB, int, FactionTemplateRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrAbilityDB = StaticDB.InitDB<GarrAbilityDB, int, GarrAbilityRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrAbilityCategoryDB = StaticDB.InitDB<GarrAbilityCategoryDB, int, GarrAbilityCategoryRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrAbilityEffectDB = StaticDB.InitDB<GarrAbilityEffectDB, int, GarrAbilityEffectRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrClassSpecDB = StaticDB.InitDB<GarrClassSpecDB, int, GarrClassSpecRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrEncounterDB = StaticDB.InitDB<GarrEncounterDB, int, GarrEncounterRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrEncounterSetXEncounterDB = StaticDB.InitDB<GarrEncounterSetXEncounterDB, int, GarrEncounterSetXEncounterRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrFollItemSetMemberDB = StaticDB.InitDB<GarrFollItemSetMemberDB, int, GarrFollItemSetMemberRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrFollowerDB = StaticDB.InitDB<GarrFollowerDB, int, GarrFollowerRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrFollowerLevelXPDB = StaticDB.InitDB<GarrFollowerLevelXPDB, int, GarrFollowerLevelXPRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrFollowerTypeDB = StaticDB.InitDB<GarrFollowerTypeDB, int, GarrFollowerTypeRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrFollowerQualityDB = StaticDB.InitDB<GarrFollowerQualityDB, int, GarrFollowerQualityRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrFollowerXAbilityDB = StaticDB.InitDB<GarrFollowerXAbilityDB, int, GarrFollowerXAbilityRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrMechanicDB = StaticDB.InitDB<GarrMechanicDB, int, GarrMechanicRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrMechanicTypeDB = StaticDB.InitDB<GarrMechanicTypeDB, int, GarrMechanicTypeRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrMissionDB = StaticDB.InitDB<GarrMissionDB, int, GarrMissionRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrMissionRewardDB = StaticDB.InitDB<GarrMissionRewardDB, int, GarrMissionRewardRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrMissionXEncounterDB = StaticDB.InitDB<GarrMissionXEncounterDB, int, GarrMissionXEncounterRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrMissionTypeDB = StaticDB.InitDB<GarrMissionTypeDB, int, GarrMissionTypeRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrStringDB = StaticDB.InitDB<GarrStringDB, int, GarrStringRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrTalentDB = StaticDB.InitDB<GarrTalentDB, int, GarrTalentRec>(nonLocalizedBundle, localizedBundle);
			this.m_garrTalentTreeDB = StaticDB.InitDB<GarrTalentTreeDB, int, GarrTalentTreeRec>(nonLocalizedBundle, localizedBundle);
			this.m_holidayDescriptionsDB = StaticDB.InitDB<HolidayDescriptionsDB, int, HolidayDescriptionsRec>(nonLocalizedBundle, localizedBundle);
			this.m_holidayNamesDB = StaticDB.InitDB<HolidayNamesDB, int, HolidayNamesRec>(nonLocalizedBundle, localizedBundle);
			this.m_holidaysDB = StaticDB.InitDB<HolidaysDB, int, HolidaysRec>(nonLocalizedBundle, localizedBundle);
			this.m_itemDB = StaticDB.InitDB<ItemDB, int, ItemRec>(nonLocalizedBundle, localizedBundle);
			this.m_itemEffectDB = StaticDB.InitDB<ItemEffectDB, int, ItemEffectRec>(nonLocalizedBundle, localizedBundle);
			this.m_itemNameDescriptionDB = StaticDB.InitDB<ItemNameDescriptionDB, int, ItemNameDescriptionRec>(nonLocalizedBundle, localizedBundle);
			this.m_itemSubClassDB = StaticDB.InitDB<ItemSubClassDB, int, ItemSubClassRec>(nonLocalizedBundle, localizedBundle);
			this.m_lFGDungeonsDB = StaticDB.InitDB<LFGDungeonsDB, int, LFGDungeonsRec>(nonLocalizedBundle, localizedBundle);
			this.m_mapDB = StaticDB.InitDB<MapDB, int, MapRec>(nonLocalizedBundle, localizedBundle);
			this.m_mobileStringsDB = StaticDB.InitDB<MobileStringsDB, string, MobileStringsRec>(nonLocalizedBundle, localizedBundle);
			this.m_questInfoDB = StaticDB.InitDB<QuestInfoDB, int, QuestInfoRec>(nonLocalizedBundle, localizedBundle);
			this.m_questV2DB = StaticDB.InitDB<QuestV2DB, int, QuestV2Rec>(nonLocalizedBundle, localizedBundle);
			this.m_rewardPackDB = StaticDB.InitDB<RewardPackDB, int, RewardPackRec>(nonLocalizedBundle, localizedBundle);
			this.m_rewardPackXItemDB = StaticDB.InitDB<RewardPackXItemDB, int, RewardPackXItemRec>(nonLocalizedBundle, localizedBundle);
			this.m_rewardPackXCurrencyTypeDB = StaticDB.InitDB<RewardPackXCurrencyTypeDB, int, RewardPackXCurrencyTypeRec>(nonLocalizedBundle, localizedBundle);
			this.m_spamMessagesDB = StaticDB.InitDB<SpamMessagesDB, int, SpamMessagesRec>(nonLocalizedBundle, localizedBundle);
			this.m_spellEffectDB = StaticDB.InitDB<SpellEffectDB, int, SpellEffectRec>(nonLocalizedBundle, localizedBundle);
			this.m_spellDurationDB = StaticDB.InitDB<SpellDurationDB, int, SpellDurationRec>(nonLocalizedBundle, localizedBundle);
			this.m_treasureDB = StaticDB.InitDB<TreasureDB, int, TreasureRec>(nonLocalizedBundle, localizedBundle);
			this.m_uiTextureAtlasDB = StaticDB.InitDB<UiTextureAtlasDB, int, UiTextureAtlasRec>(nonLocalizedBundle, localizedBundle);
			this.m_uiTextureAtlasMemberDB = StaticDB.InitDB<UiTextureAtlasMemberDB, int, UiTextureAtlasMemberRec>(nonLocalizedBundle, localizedBundle);
			this.m_uiTextureKitDB = StaticDB.InitDB<UiTextureKitDB, int, UiTextureKitRec>(nonLocalizedBundle, localizedBundle);
			this.m_vW_MobileSpellDB = StaticDB.InitDB<VW_MobileSpellDB, int, VW_MobileSpellRec>(nonLocalizedBundle, localizedBundle);
			this.m_worldMapAreaDB = StaticDB.InitDB<WorldMapAreaDB, int, WorldMapAreaRec>(nonLocalizedBundle, localizedBundle);
			this.m_SpellTooltipDB = StaticDB.InitDB<SpellTooltipDB, int, SpellTooltipRec>(nonLocalizedBundle, localizedBundle);
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

		private static MODBType InitDB<MODBType, KeyType, RecType>(AssetBundle nonLocalizedBundle, AssetBundle localizedBundle) where MODBType : MODB<KeyType, RecType>, new() where RecType : MODBRec, new()
		{
			MODBType result = Activator.CreateInstance<MODBType>();
			if (!result.Load(StaticDB.bundlePath, nonLocalizedBundle, localizedBundle, Main.instance.GetLocale()))
			{
				Debug.LogError("Failed to load " + typeof(MODBType).Name);
			}
			return result;
		}

		public static AzeriteEmpoweredItemDB azeriteEmpoweredItemDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_azeriteEmpoweredItemDB;
			}
		}

		public static Cfg_LanguagesDB cfg_LanguagesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_cfg_LanguagesDB;
			}
		}

		public static Cfg_RealmsDB cfg_RealmsDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_cfg_RealmsDB;
			}
		}

		public static Cfg_RegionsDB cfg_RegionsDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_cfg_RegionsDB;
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

		public static ChatProfanityDB chatProfanityDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_chatProfanityDB;
			}
		}

		public static ChrClassesDB chrClassesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_chrClassesDB;
			}
		}

		public static ChrRacesDB chrRacesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_chrRacesDB;
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

		public static DifficultyDB difficultyDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_difficultyDB;
			}
		}

		public static FactionDB factionDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_factionDB;
			}
		}

		public static FactionTemplateDB factionTemplateDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_factionTemplateDB;
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

		public static GarrEncounterDB garrEncounterDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrEncounterDB;
			}
		}

		public static GarrEncounterSetXEncounterDB garrEncounterSetXEncounterDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrEncounterSetXEncounterDB;
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

		public static GarrMissionXEncounterDB garrMissionXEncounterDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMissionXEncounterDB;
			}
		}

		public static GarrMissionTypeDB garrMissionTypeDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_garrMissionTypeDB;
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

		public static HolidayDescriptionsDB holidayDescriptionsDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_holidayDescriptionsDB;
			}
		}

		public static HolidayNamesDB holidayNamesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_holidayNamesDB;
			}
		}

		public static HolidaysDB holidaysDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_holidaysDB;
			}
		}

		public static ItemDB itemDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_itemDB;
			}
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

		public static ItemSubClassDB itemSubClassDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_itemSubClassDB;
			}
		}

		public static LFGDungeonsDB lFGDungeonsDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_lFGDungeonsDB;
			}
		}

		public static MapDB mapDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_mapDB;
			}
		}

		public static MobileStringsDB mobileStringsDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_mobileStringsDB;
			}
		}

		public static QuestInfoDB questInfoDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_questInfoDB;
			}
		}

		public static QuestV2DB questV2DB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_questV2DB;
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

		public static SpamMessagesDB spamMessagesDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_spamMessagesDB;
			}
		}

		public static SpellEffectDB spellEffectDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_spellEffectDB;
			}
		}

		public static SpellDurationDB spellDurationDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_spellDurationDB;
			}
		}

		public static TreasureDB treasureDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_treasureDB;
			}
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

		public static VW_MobileSpellDB vW_MobileSpellDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_vW_MobileSpellDB;
			}
		}

		public static WorldMapAreaDB worldMapAreaDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_worldMapAreaDB;
			}
		}

		public static SpellTooltipDB spellTooltipDB
		{
			get
			{
				return Singleton<StaticDB>.instance.m_SpellTooltipDB;
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

		public static ItemSubClassRec GetItemSubclass(int classID, int subClassID)
		{
			return StaticDB.itemSubClassDB.GetRecordsByParentID(classID).FirstOrDefault((ItemSubClassRec subClassRec) => (int)subClassRec.SubClassID == subClassID);
		}

		private static bool s_initialized;

		private AzeriteEmpoweredItemDB m_azeriteEmpoweredItemDB;

		private Cfg_LanguagesDB m_cfg_LanguagesDB;

		private Cfg_RealmsDB m_cfg_RealmsDB;

		private Cfg_RegionsDB m_cfg_RegionsDB;

		private CharShipmentDB m_charShipmentDB;

		private CharShipmentContainerDB m_charShipmentContainerDB;

		private ChatProfanityDB m_chatProfanityDB;

		private ChrClassesDB m_chrClassesDB;

		private ChrRacesDB m_chrRacesDB;

		private CommunityIconDB m_communityIconDB;

		private CreatureDB m_creatureDB;

		private CurrencyTypesDB m_currencyTypesDB;

		private CurrencyContainerDB m_currencyContainerDB;

		private DifficultyDB m_difficultyDB;

		private FactionDB m_factionDB;

		private FactionTemplateDB m_factionTemplateDB;

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

		private GarrMissionXEncounterDB m_garrMissionXEncounterDB;

		private GarrMissionTypeDB m_garrMissionTypeDB;

		private GarrStringDB m_garrStringDB;

		private GarrTalentDB m_garrTalentDB;

		private GarrTalentTreeDB m_garrTalentTreeDB;

		private HolidayDescriptionsDB m_holidayDescriptionsDB;

		private HolidayNamesDB m_holidayNamesDB;

		private HolidaysDB m_holidaysDB;

		private ItemDB m_itemDB;

		private ItemEffectDB m_itemEffectDB;

		private ItemNameDescriptionDB m_itemNameDescriptionDB;

		private ItemSubClassDB m_itemSubClassDB;

		private LFGDungeonsDB m_lFGDungeonsDB;

		private MapDB m_mapDB;

		private MobileStringsDB m_mobileStringsDB;

		private QuestInfoDB m_questInfoDB;

		private QuestV2DB m_questV2DB;

		private RewardPackDB m_rewardPackDB;

		private RewardPackXItemDB m_rewardPackXItemDB;

		private RewardPackXCurrencyTypeDB m_rewardPackXCurrencyTypeDB;

		private SpamMessagesDB m_spamMessagesDB;

		private SpellEffectDB m_spellEffectDB;

		private SpellDurationDB m_spellDurationDB;

		private TreasureDB m_treasureDB;

		private UiTextureAtlasDB m_uiTextureAtlasDB;

		private UiTextureAtlasMemberDB m_uiTextureAtlasMemberDB;

		private UiTextureKitDB m_uiTextureKitDB;

		private VW_MobileSpellDB m_vW_MobileSpellDB;

		private WorldMapAreaDB m_worldMapAreaDB;

		private SpellTooltipDB m_SpellTooltipDB;

		private static readonly string bundlePath = "Assets/BundleAssets/StaticDB/";
	}
}
