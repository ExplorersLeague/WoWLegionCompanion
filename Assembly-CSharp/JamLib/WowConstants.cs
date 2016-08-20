using System;

namespace JamLib
{
	public static class WowConstants
	{
		public const byte NUM_PETBATTLE_MOVE_TYPES = 6;

		public const int NUM_PARTY_INDEX = 2;

		public const int PETITION_NUM_CHOICES = 10;

		public const int MAX_NUM_DECLINED_TYPES = 5;

		public const uint REALM_ADDRESS_NONE = 0u;

		public const string WOWGUID_NULL = "0000000000000000";

		public const int PETBATTLE_SLOT_INVALID = -1;

		public const int PETBATTLE_INPUT_MOVE_MSG_DEBUG_FLAG_NONE = 0;

		public const int PETBATTLE_MSG_IGNORE_ROUND_CHECK = -1;

		public const int NUM_PETBATTLE_PLAYERS = 2;

		public const int PBOID_INVALID = 9;

		public const int PETBATTLE_PET_STATUS_FLAG_NONE = 0;

		public const int PETBATTLE_TRAPSTATUS_INVALID = 0;

		public const int NUM_PETBATTLE_ENVIROS = 3;

		public const int PETBATTLE_STATE_CREATED = 0;

		public const int PETBATTLE_EFFECT_FLAG_NONE = 0;

		public const int PETBATTLE_EFFECT_TYPE_SET_HEALTH = 0;

		public const int STOP_SPLINE_STYLE_NONE = 0;

		public const int LFG_ROLE_MAX = 3;

		public const int MAX_GLYPHS = 6;

		public const int EQUIPPED_LAST = 18;

		public const int NUM_REWARD_FACTIONS = 5;

		public const int MAX_CURRENCY_QUEST_REWARD = 4;

		public const int NUM_CLIENT_CREATURE_FLAG_WORDS = 2;

		public const int MAX_NUM_PLURAL_TYPES = 4;

		public const int NUM_ITEM_ENCHANTMENTS_SAVED = 8;

		public const int MAXIMUM_NUM_QUESTS_LOG = 50;

		public const int BATTLEFIELD_TYPE_NONE = 255;

		public const int NUM_AVG_ITEM_LEVEL_CATEGORIES = 4;

		public const int DEFAULT_TIMEZONE_ID = 1;

		public const int GUILD_NUM_RANKS = 10;

		public const int GUILD_REQUIRED_SIGNATURES = 9;

		public const int ACCEPTED_LICENSES_STORAGE_NUM_BIGINTS = 2;

		public const int MAX_GUILD_BANK_TABS = 8;

		public const int URMS_COUNT = 2;

		public const int NUM_RAID_DIFFICULTY_INDEXES = 2;

		public const int PETBATTLE_SLOT_0 = 0;

		public const int NUM_PETBATTLE_SLOTS = 3;

		public const int MAX_CORPSE_ITEMS = 19;

		public const int MAX_QUESTLOG_COUNT_PROGRESS_BYTES = 100;

		public const int MAX_QUESTLOG_COMPLETED_OBJECTIVE_SPAWNGROUPS_BYTES = 150;

		public const int MAX_PVP_BLACKLIST_MAP = 2;

		public const int RECOVERED_ITEM_OVERFLOW_MAILER_ID = 361;

		public const int ITEM_TOAST_METHOD_DO_NOT_DISPLAY = 0;

		public const int CHAR_BODY_INV_SLOTS = 23;

		public const int WHERE_PACKET_SIZE = 256;

		public const int MAX_WOW_LOCALES = 16;

		public const int NUM_PVP_BRACKETS = 6;

		public const int NUM_CHARACTER_CUSTOM_DISPLAY_OPTIONS = 3;

		public const int WEAPONSLOT_NUMSLOTS = 2;

		public const int NUM_ITEM_ENCHANTMENT_SOCKETS = 3;

		public const int HEAL_PREDICTION_TYPE_SINGLE = 0;

		public const int INVALID_DEST_LOC_SPELL_CAST_INDEX = 0;

		public const int LFG_ROLE_DAMAGE = 2;

		public const int NUM_PET_ACTION_BUTTONS = 10;

		public const int MAX_ITEM_PURCHASE_ITEMS = 5;

		public const int MAX_AWARD_CURRENCY = 5;

		public const int NUM_RUNE_TYPES = 4;

		public const int MAX_RUNES = 6;

		public const int NUM_SAVED_ACTIONBAR_PAGES = 11;

		public const int NUM_ACTIONS_PER_PAGE = 12;

		public const int NUM_SAVED_ACTION_BUTTONS = 132;

		public const int PVP_REPORT_AFK_GENERIC_FAILURE = 4;

		public const int NUM_REQUIRED_TRAINER_ABILITIES = 3;

		public const int NUM_POWER_TYPE_SLOTS = 6;

		public const int NUM_UNITSTATS = 5;

		public const int BATTLEFIELD_NUM_FACTIONS = 2;

		public const int NUM_ACCOUNT_DATA_TYPES = 8;

		public const int MAX_REPUTATION_FACTIONS = 256;

		public const int MD5_INT_LENGTH = 4;

		public const int PERF_SAMPLE_DATA_SIZE = 488;

		public const int MAX_TUTORIAL_BYTES = 32;

		public const int DOS_CHALLENGE_WORDS = 8;

		public const int TRANSFER_REASON_SEAMLESS_TELEPORT = 21;

		public const int DISCONNECT_REASON_FLOOD = 2;

		public const int NUM_SPELLCAST_MISC_FIELDS = 2;

		public const int MAX_CHALLENGE_AFFIXES = 3;

		public const int PARTY_INDEX_PRIVATE = 0;

		public const int PARTY_INDEX_PUBLIC = 1;

		public const int PARTY_INDEX_UNKNOWN = 127;

		public const int LFG_RANDOM_DUNGEON = 6;

		public const int LFG_ROLE_FLAG_DAMAGE = 8;

		public const int NUM_RESEARCH_FIELDS = 1;

		public const uint NUM_SPELL_CLASSES_MASK = 4u;

		public const int PETBATTLE_RESULT_SUCCESS = 21;

		public const int PETBATTLE_RESULT_FAIL_UNKNOWN = 0;

		public const int MAXIMUM_NUM_ACCOUNT_QUEST_COMPLETED_BYTES = 100;

		public const int GARRISON_NUM_AVAILABLE_RECRUITS = 3;

		public const int ITEM_CREATION_CONTEXT_NONE = 0;

		public const int MAX_DISPLAY_SPELL_QUEST_REWARD = 3;

		public const int NUM_COMBAT_RATINGS = 32;

		public const int CURRENCY_SOURCE_PUSHLOOT = 22;

		public const int BI_REWARD_SOURCE_NONE = 0;

		public const int NUM_PVP_FACTIONS = 2;

		public const int MAX_RAID_TARGETS = 28;

		public static readonly int NUM_JAM_DESTINATION_TYPES = Enum.GetNames(typeof(JAM_DESTINATION)).Length;

		public static readonly JamServerSpec SERVER_SPEC_NULL = new JamServerSpec
		{
			RealmAddress = 0u,
			ServerType = (JAM_DESTINATION)WowConstants.NUM_JAM_DESTINATION_TYPES,
			ServerID = 0u
		};
	}
}
