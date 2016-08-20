using System;
using System.Collections.Generic;
using UnityEngine;
using WowJamMessages;
using WowJamMessages.MobileClientJSON;
using WowStatConstants;
using WowStaticData;

public class GeneralHelpers : MonoBehaviour
{
	public static Color GetQualityColor(int quality)
	{
		Color gray = Color.gray;
		Color green = Color.green;
		Color result;
		result..ctor(0.34f, 0.49f, 1f, 1f);
		Color result2;
		result2..ctor(0.58f, 0f, 1f, 1f);
		Color result3;
		result3..ctor(1f, 0.56f, 0f, 1f);
		switch (quality)
		{
		case 1:
			return gray;
		case 2:
			return green;
		case 3:
			return result;
		case 4:
			return result2;
		case 5:
			return result3;
		default:
			return Color.red;
		}
	}

	private static int GetMobileAtlasMemberOverride(int iconFileDataID)
	{
		int result = 0;
		switch (iconFileDataID)
		{
		case 1383681:
			result = 6150;
			break;
		case 1383682:
			result = 6149;
			break;
		case 1383683:
			result = 6148;
			break;
		default:
			if (iconFileDataID != 1380306)
			{
				if (iconFileDataID == 1390116)
				{
					result = 0;
				}
			}
			else
			{
				result = 6147;
			}
			break;
		}
		return result;
	}

	public static Sprite LoadIconAsset(AssetBundleType assetBundleType, int iconFileDataID)
	{
		if (iconFileDataID == 894556)
		{
			string locale = Main.instance.GetLocale();
			if (locale != "enUS")
			{
				return Resources.Load<Sprite>("MiscIcons/LocalizedIcons/" + locale + "/XP_Icon");
			}
		}
		if (iconFileDataID == 1380306)
		{
			return Resources.Load<Sprite>("MissionMechanicEffects/MechanicIcon-Curse-Shadow");
		}
		if (iconFileDataID == 1383683)
		{
			return Resources.Load<Sprite>("MissionMechanicEffects/MechanicIcon-Disorienting-Shadow");
		}
		if (iconFileDataID == 1383682)
		{
			return Resources.Load<Sprite>("MissionMechanicEffects/MechanicIcon-Lethal-Shadow");
		}
		if (iconFileDataID == 1390116)
		{
			return Resources.Load<Sprite>("MissionMechanicEffects/MechanicIcon-Powerful-Shadow");
		}
		if (iconFileDataID == 1383681)
		{
			return Resources.Load<Sprite>("MissionMechanicEffects/MechanicIcon-Slowing-Shadow");
		}
		int mobileAtlasMemberOverride = GeneralHelpers.GetMobileAtlasMemberOverride(iconFileDataID);
		if (mobileAtlasMemberOverride > 0)
		{
			return TextureAtlas.instance.GetAtlasSprite(mobileAtlasMemberOverride);
		}
		AssetBundle assetBundle = null;
		string arg = string.Empty;
		if (assetBundleType == AssetBundleType.Icons)
		{
			arg = "Assets/BundleAssets/Icons/";
			assetBundle = AssetBundleManager.Icons;
		}
		if (assetBundleType == AssetBundleType.PortraitIcons)
		{
			arg = "Assets/BundleAssets/PortraitIcons/";
			assetBundle = AssetBundleManager.portraitIcons;
		}
		string text = arg + iconFileDataID + ".tga";
		Sprite sprite = assetBundle.LoadAsset<Sprite>(text);
		if (sprite == null)
		{
			text = arg + iconFileDataID + ".png";
			sprite = assetBundle.LoadAsset<Sprite>(text);
		}
		return sprite;
	}

	public static Sprite LoadCurrencyIcon(int currencyID)
	{
		return AssetBundleManager.Icons.LoadAsset<Sprite>("Currency_" + currencyID.ToString("00000000"));
	}

	public static void GetXpCapInfo(int followerLevel, int followerQuality, out uint xpToNextLevelOrQuality, out bool isQuality, out bool isMaxLevelAndMaxQuality)
	{
		isMaxLevelAndMaxQuality = false;
		isQuality = false;
		GarrFollowerLevelXPRec followerXpRec = null;
		StaticDB.garrFollowerLevelXPDB.EnumRecordsByParentID(followerLevel, delegate(GarrFollowerLevelXPRec rec)
		{
			GarrFollowerTypeRec record = StaticDB.garrFollowerTypeDB.GetRecord((int)rec.GarrFollowerTypeID);
			if (record.GarrTypeID == 3u)
			{
				followerXpRec = rec;
				return false;
			}
			return true;
		});
		if (followerXpRec.XpToNextLevel > 0u)
		{
			xpToNextLevelOrQuality = followerXpRec.XpToNextLevel;
			return;
		}
		isQuality = true;
		GarrFollowerQualityRec qualityRec = null;
		StaticDB.garrFollowerQualityDB.EnumRecordsByParentID(followerQuality, delegate(GarrFollowerQualityRec rec)
		{
			if (rec.GarrFollowerTypeID == 4u)
			{
				qualityRec = rec;
				return false;
			}
			return true;
		});
		xpToNextLevelOrQuality = qualityRec.XpToNextQuality;
		if (qualityRec.XpToNextQuality == 0u)
		{
			isMaxLevelAndMaxQuality = true;
		}
	}

	public static FollowerStatus GetFollowerStatus(JamGarrisonFollower follower)
	{
		bool flag = (follower.Flags & 4) != 0;
		if (flag)
		{
			return FollowerStatus.inactive;
		}
		bool flag2 = (follower.Flags & 2) != 0;
		if (flag2)
		{
			return FollowerStatus.fatigued;
		}
		bool flag3 = follower.CurrentBuildingID != 0;
		if (flag3)
		{
			return FollowerStatus.inBuilding;
		}
		bool flag4 = follower.CurrentMissionID != 0;
		if (flag4)
		{
			return FollowerStatus.onMission;
		}
		return FollowerStatus.available;
	}

	public static string GetColorFromInt(int color)
	{
		int num = color >> 16 & 255;
		int num2 = color >> 8 & 255;
		int num3 = color & 255;
		return string.Format("{0:X2}{1:X2}{2:X2}", num, num2, num3);
	}

	public static string GetMobileStatColorString(MobileStatColor color)
	{
		switch (color)
		{
		case MobileStatColor.MOBILE_STAT_COLOR_TRIVIAL:
			return "808080ff";
		case MobileStatColor.MOBILE_STAT_COLOR_FRIENDLY:
			return "00ff00ff";
		case MobileStatColor.MOBILE_STAT_COLOR_HOSTILE:
			return "ff0000ff";
		case MobileStatColor.MOBILE_STAT_COLOR_INACTIVE:
			return "808080ff";
		case MobileStatColor.MOBILE_STAT_COLOR_ERROR:
			return "ff2020ff";
		}
		return "ffffffff";
	}

	public static string GetItemQualityColorTag(int qualityID)
	{
		return "<color=#" + GeneralHelpers.GetItemQualityColor(qualityID) + ">";
	}

	public static string GetItemQualityColor(int qualityID)
	{
		switch (qualityID)
		{
		case 0:
			return "9d9d9dff";
		case 2:
			return "1eff00ff";
		case 3:
			return "0070ddff";
		case 4:
			return "a335eeff";
		case 5:
			return "ff8000ff";
		case 6:
			return "e6cc80ff";
		case 7:
			return "00ccffff";
		case 8:
			return "00ccffff";
		}
		return "ffffffff";
	}

	public static string GetItemDescription(ItemRec itemRec)
	{
		string text = string.Empty;
		int spellID = 0;
		StaticDB.itemEffectDB.EnumRecordsByParentID(itemRec.ID, delegate(ItemEffectRec effectRec)
		{
			if (effectRec.SpellID == 0)
			{
				return true;
			}
			spellID = effectRec.SpellID;
			return false;
		});
		if (spellID > 0)
		{
			SpellTooltipRec record = StaticDB.spellTooltipDB.GetRecord(spellID);
			if (record != null)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"<color=#",
					GeneralHelpers.s_friendlyColor,
					">",
					WowTextParser.parser.Parse(record.Description, spellID),
					"</color>"
				});
			}
			else
			{
				Debug.Log("GetItemDescription: spellID " + spellID + " not found in spellTooltipDB.");
			}
		}
		if (itemRec.Description != null && itemRec.Description != string.Empty)
		{
			if (text != string.Empty)
			{
				text += "\n\n";
			}
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"<color=#",
				GeneralHelpers.s_defaultColor,
				">\"",
				WowTextParser.parser.Parse(itemRec.Description, 0),
				"\"</color>"
			});
		}
		return text;
	}

	public static bool SpellGrantsArtifactXP(int spellID)
	{
		if (spellID <= 0)
		{
			return false;
		}
		bool artifactXP = false;
		StaticDB.spellEffectDB.EnumRecordsByParentID(spellID, delegate(SpellEffectRec effectRec)
		{
			if (effectRec.Effect == 240)
			{
				artifactXP = true;
				return false;
			}
			return true;
		});
		return artifactXP;
	}

	public static int GetNumActiveChampions()
	{
		int num = 0;
		foreach (JamGarrisonFollower jamGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
		{
			bool flag = (jamGarrisonFollower.Flags & 4) != 0;
			bool flag2 = (jamGarrisonFollower.Flags & 8) != 0;
			if (!flag && !flag2)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumInactiveChampions()
	{
		int num = 0;
		foreach (JamGarrisonFollower jamGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
		{
			bool flag = (jamGarrisonFollower.Flags & 4) != 0;
			bool flag2 = (jamGarrisonFollower.Flags & 8) != 0;
			if (flag && !flag2)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumTroops()
	{
		int num = 0;
		foreach (JamGarrisonFollower jamGarrisonFollower in PersistentFollowerData.followerDictionary.Values)
		{
			bool flag = (jamGarrisonFollower.Flags & 4) != 0;
			bool flag2 = (jamGarrisonFollower.Flags & 8) != 0;
			if (!flag && flag2 && jamGarrisonFollower.Durability > 0)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetMaxActiveChampions()
	{
		GarrFollowerTypeRec record = StaticDB.garrFollowerTypeDB.GetRecord(4);
		return (int)record.MaxFollowers;
	}

	public static string GetInventoryTypeString(INVENTORY_TYPE invType)
	{
		switch (invType)
		{
		case INVENTORY_TYPE.HEAD:
			return StaticDB.GetString("INVTYPE_HEAD", null);
		case INVENTORY_TYPE.NECK:
			return StaticDB.GetString("INVTYPE_NECK", null);
		case INVENTORY_TYPE.SHOULDER:
			return StaticDB.GetString("INVTYPE_SHOULDER", null);
		case INVENTORY_TYPE.BODY:
			return StaticDB.GetString("INVTYPE_BODY", null);
		case INVENTORY_TYPE.CHEST:
			return StaticDB.GetString("INVTYPE_CHEST", null);
		case INVENTORY_TYPE.WAIST:
			return StaticDB.GetString("INVTYPE_WAIST", null);
		case INVENTORY_TYPE.LEGS:
			return StaticDB.GetString("INVTYPE_LEGS", null);
		case INVENTORY_TYPE.FEET:
			return StaticDB.GetString("INVTYPE_FEET", null);
		case INVENTORY_TYPE.WRIST:
			return StaticDB.GetString("INVTYPE_WRIST", null);
		case INVENTORY_TYPE.HAND:
			return StaticDB.GetString("INVTYPE_HAND", null);
		case INVENTORY_TYPE.FINGER:
			return StaticDB.GetString("INVTYPE_FINGER", null);
		case INVENTORY_TYPE.TRINKET:
			return StaticDB.GetString("INVTYPE_TRINKET", null);
		case INVENTORY_TYPE.WEAPON:
			return StaticDB.GetString("INVTYPE_WEAPON", null);
		case INVENTORY_TYPE.SHIELD:
			return StaticDB.GetString("INVTYPE_SHIELD", null);
		case INVENTORY_TYPE.RANGED:
			return StaticDB.GetString("INVTYPE_RANGED", null);
		case INVENTORY_TYPE.CLOAK:
			return StaticDB.GetString("INVTYPE_CLOAK", null);
		case INVENTORY_TYPE.TWO_H_WEAPON:
			return StaticDB.GetString("INVTYPE_2HWEAPON", null);
		case INVENTORY_TYPE.BAG:
			return StaticDB.GetString("INVTYPE_BAG", null);
		case INVENTORY_TYPE.TABARD:
			return StaticDB.GetString("INVTYPE_TABARD", null);
		case INVENTORY_TYPE.ROBE:
			return StaticDB.GetString("INVTYPE_ROBE", null);
		case INVENTORY_TYPE.WEAPONMAINHAND:
			return StaticDB.GetString("INVTYPE_WEAPONMAINHAND", null);
		case INVENTORY_TYPE.WEAPONOFFHAND:
			return StaticDB.GetString("INVTYPE_WEAPONOFFHAND", null);
		case INVENTORY_TYPE.HOLDABLE:
			return StaticDB.GetString("INVTYPE_HOLDABLE", null);
		case INVENTORY_TYPE.AMMO:
			return StaticDB.GetString("INVTYPE_AMMO", null);
		case INVENTORY_TYPE.THROWN:
			return StaticDB.GetString("INVTYPE_THROWN", null);
		case INVENTORY_TYPE.RANGEDRIGHT:
			return StaticDB.GetString("INVTYPE_RANGEDRIGHT", null);
		case INVENTORY_TYPE.QUIVER:
			return StaticDB.GetString("INVTYPE_QUIVER", null);
		case INVENTORY_TYPE.RELIC:
			return StaticDB.GetString("INVTYPE_RELIC", null);
		default:
			return string.Empty;
		}
	}

	public static string GetBonusStatString(BonusStatIndex statIndex)
	{
		switch (statIndex)
		{
		case BonusStatIndex.MANA:
			return StaticDB.GetString("ITEM_MOD_MANA_SHORT", null);
		case BonusStatIndex.HEALTH:
			return StaticDB.GetString("ITEM_MOD_HEALTH_SHORT", null);
		default:
			if (statIndex != BonusStatIndex.MASTERY_RATING)
			{
				return statIndex.ToString();
			}
			return StaticDB.GetString("ITEM_MOD_MASTERY_RATING", null);
		case BonusStatIndex.AGILITY:
			return StaticDB.GetString("ITEM_MOD_AGILITY_SHORT", null);
		case BonusStatIndex.STRENGTH:
			return StaticDB.GetString("ITEM_MOD_STRENGTH_SHORT", null);
		case BonusStatIndex.INTELLECT:
			return StaticDB.GetString("ITEM_MOD_INTELLECT_SHORT", null);
		case BonusStatIndex.STAMINA:
			return StaticDB.GetString("ITEM_MOD_STAMINA_SHORT", null);
		case BonusStatIndex.DEFENSE_SKILL_RATING:
			return StaticDB.GetString("ITEM_MOD_DEFENSE_SKILL_RATING_SHORT", null);
		case BonusStatIndex.DODGE_RATING:
			return StaticDB.GetString("ITEM_MOD_DODGE_RATING_SHORT", null);
		case BonusStatIndex.PARRY_RATING:
			return StaticDB.GetString("ITEM_MOD_PARRY_RATING_SHORT", null);
		case BonusStatIndex.BLOCK_RATING:
			return StaticDB.GetString("ITEM_MOD_BLOCK_RATING_SHORT", null);
		case BonusStatIndex.HIT_MELEE_RATING:
			return StaticDB.GetString("ITEM_MOD_HIT_MELEE_RATING_SHORT", null);
		case BonusStatIndex.HIT_RANGED_RATING:
			return StaticDB.GetString("ITEM_MOD_HIT_RANGED_RATING_SHORT", null);
		case BonusStatIndex.HIT_SPELL_RATING:
			return StaticDB.GetString("ITEM_MOD_HIT_SPELL_RATING_SHORT", null);
		case BonusStatIndex.CRIT_MELEE_RATING:
			return StaticDB.GetString("ITEM_MOD_CRIT_MELEE_RATING_SHORT", null);
		case BonusStatIndex.CRIT_RANGED_RATING:
			return StaticDB.GetString("ITEM_MOD_CRIT_RANGED_RATING_SHORT", null);
		case BonusStatIndex.CRIT_SPELL_RATING:
			return StaticDB.GetString("ITEM_MOD_CRIT_SPELL_RATING_SHORT", null);
		case BonusStatIndex.HIT_RATING:
			return StaticDB.GetString("ITEM_MOD_HIT_RATING_SHORT", null);
		case BonusStatIndex.CRIT_RATING:
			return StaticDB.GetString("ITEM_MOD_CRIT_RATING_SHORT", null);
		case BonusStatIndex.RESILIENCE_RATING:
			return StaticDB.GetString("ITEM_MOD_RESILIENCE_RATING_SHORT", null);
		case BonusStatIndex.HASTE_RATING:
			return StaticDB.GetString("ITEM_MOD_HASTE_RATING_SHORT", null);
		case BonusStatIndex.EXPERTISE_RATING:
			return StaticDB.GetString("ITEM_MOD_EXPERTISE_RATING_SHORT", null);
		case BonusStatIndex.ATTACK_POWER:
			return StaticDB.GetString("ITEM_MOD_ATTACK_POWER_SHORT", null);
		case BonusStatIndex.VERSATILITY:
			return StaticDB.GetString("ITEM_MOD_VERSATILITY", null);
		}
	}

	public static FollowerCanCounterMechanic HasFollowerWhoCanCounter(int garrMechanicTypeID)
	{
		GeneralHelpers.<HasFollowerWhoCanCounter>c__AnonStorey3A <HasFollowerWhoCanCounter>c__AnonStorey3A = new GeneralHelpers.<HasFollowerWhoCanCounter>c__AnonStorey3A();
		<HasFollowerWhoCanCounter>c__AnonStorey3A.garrMechanicTypeID = garrMechanicTypeID;
		<HasFollowerWhoCanCounter>c__AnonStorey3A.canCounterButBusy = false;
		<HasFollowerWhoCanCounter>c__AnonStorey3A.canCounterAndIsAvailable = false;
		JamGarrisonFollower follower;
		foreach (JamGarrisonFollower follower2 in PersistentFollowerData.followerDictionary.Values)
		{
			follower = follower2;
			for (int i = 0; i < follower.AbilityID.Length; i++)
			{
				GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(follower.AbilityID[i]);
				if (record == null)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Invalid Ability ID ",
						follower.AbilityID[i],
						" from follower ",
						follower.GarrFollowerID
					}));
				}
				else if ((record.Flags & 1u) == 0u)
				{
					StaticDB.garrAbilityEffectDB.EnumRecordsByParentID(record.ID, delegate(GarrAbilityEffectRec garrAbilityEffectRec)
					{
						if (garrAbilityEffectRec.GarrMechanicTypeID == 0u)
						{
							return true;
						}
						if (garrAbilityEffectRec.AbilityAction != 0u)
						{
							return true;
						}
						if ((long)<HasFollowerWhoCanCounter>c__AnonStorey3A.garrMechanicTypeID != (long)((ulong)garrAbilityEffectRec.GarrMechanicTypeID))
						{
							return false;
						}
						bool flag = (follower.Flags & 4) != 0;
						bool flag2 = (follower.Flags & 2) != 0;
						bool flag3 = follower.CurrentMissionID != 0;
						bool flag4 = follower.CurrentBuildingID != 0;
						if (flag || flag2 || flag3 || flag4)
						{
							<HasFollowerWhoCanCounter>c__AnonStorey3A.canCounterButBusy = true;
							return false;
						}
						<HasFollowerWhoCanCounter>c__AnonStorey3A.canCounterAndIsAvailable = true;
						return true;
					});
				}
			}
		}
		if (<HasFollowerWhoCanCounter>c__AnonStorey3A.canCounterAndIsAvailable)
		{
			return FollowerCanCounterMechanic.yesAndAvailable;
		}
		if (<HasFollowerWhoCanCounter>c__AnonStorey3A.canCounterButBusy)
		{
			return FollowerCanCounterMechanic.yesButBusy;
		}
		return FollowerCanCounterMechanic.no;
	}

	public static Sprite LoadClassIcon(int classID)
	{
		Sprite result = null;
		switch (classID)
		{
		case 1:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Warrior");
			break;
		case 2:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Paladin");
			break;
		case 3:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Hunter");
			break;
		case 4:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Rogue");
			break;
		case 5:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Priest");
			break;
		case 6:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Death Knight");
			break;
		case 7:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Shaman");
			break;
		case 8:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Mage");
			break;
		case 9:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Warlock");
			break;
		case 10:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Monk");
			break;
		case 11:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Druid");
			break;
		case 12:
			result = Resources.Load<Sprite>("NewLoginPanel/Character-Selection-Demon Hunter");
			break;
		}
		return result;
	}

	public static Font LoadFancyFont()
	{
		string locale = Main.instance.GetLocale();
		Font result;
		if (locale == "koKR")
		{
			result = Resources.Load<Font>("Fonts/Official/K_Pagetext");
		}
		else if (locale == "zhCN")
		{
			result = Resources.Load<Font>("Fonts/Official/ARKai_C");
		}
		else if (locale == "zhTW")
		{
			result = Resources.Load<Font>("Fonts/Official/bLEI00D");
		}
		else if (locale == "ruRU")
		{
			result = Resources.Load<Font>("Fonts/Official/MORPHEUS_CYR");
		}
		else
		{
			result = Resources.Load<Font>("Fonts/Official/MORPHEUS");
		}
		return result;
	}

	public static Font LoadStandardFont()
	{
		string locale = Main.instance.GetLocale();
		Font result;
		if (locale == "koKR")
		{
			result = Resources.Load<Font>("Fonts/Official/2002");
		}
		else if (locale == "zhCN")
		{
			result = Resources.Load<Font>("Fonts/Official/bHEI00M");
		}
		else if (locale == "zhTW")
		{
			result = Resources.Load<Font>("Fonts/Official/ARHei");
		}
		else if (locale == "ruRU")
		{
			result = Resources.Load<Font>("Fonts/Official/FRIZQT___CYR");
		}
		else
		{
			result = Resources.Load<Font>("Fonts/Official/BLIZQUADRATA");
		}
		return result;
	}

	public static int CurrentUnixTime()
	{
		return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
	}

	public static int[] GetBuffsForCurrentMission(int garrFollowerID, int garrMissionID, GameObject missionFollowerSlotGroup)
	{
		List<int> abilityIDList = new List<int>();
		if (!PersistentFollowerData.followerDictionary.ContainsKey(garrFollowerID))
		{
			return abilityIDList.ToArray();
		}
		JamGarrisonFollower jamGarrisonFollower = PersistentFollowerData.followerDictionary[garrFollowerID];
		for (int i = 0; i < jamGarrisonFollower.AbilityID.Length; i++)
		{
			GarrAbilityRec garrAbilityRec = StaticDB.garrAbilityDB.GetRecord(jamGarrisonFollower.AbilityID[i]);
			if (garrAbilityRec == null)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Invalid Ability ID ",
					jamGarrisonFollower.AbilityID[i],
					" from follower ",
					jamGarrisonFollower.GarrFollowerID
				}));
			}
			else
			{
				StaticDB.garrAbilityEffectDB.EnumRecordsByParentID(garrAbilityRec.ID, delegate(GarrAbilityEffectRec garrAbilityEffectRec)
				{
					switch (garrAbilityEffectRec.AbilityAction)
					{
					case 0u:
						return true;
					case 1u:
					{
						MissionFollowerSlot[] componentsInChildren = missionFollowerSlotGroup.GetComponentsInChildren<MissionFollowerSlot>(true);
						int num = 0;
						foreach (MissionFollowerSlot missionFollowerSlot in componentsInChildren)
						{
							if (missionFollowerSlot.GetCurrentGarrFollowerID() > 0)
							{
								num++;
							}
						}
						if (num != 1)
						{
							return true;
						}
						break;
					}
					case 5u:
						return true;
					case 6u:
					{
						GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(garrMissionID);
						if ((float)record.MissionDuration < garrAbilityEffectRec.ActionHours * 3600f)
						{
							return true;
						}
						break;
					}
					case 7u:
					{
						GarrMissionRec record2 = StaticDB.garrMissionDB.GetRecord(garrMissionID);
						if ((float)record2.MissionDuration > garrAbilityEffectRec.ActionHours * 3600f)
						{
							return true;
						}
						break;
					}
					case 9u:
					{
						GarrMissionRec record3 = StaticDB.garrMissionDB.GetRecord(garrMissionID);
						if (record3.TravelDuration < garrAbilityEffectRec.ActionHours * 3600f)
						{
							return true;
						}
						break;
					}
					case 10u:
					{
						GarrMissionRec record4 = StaticDB.garrMissionDB.GetRecord(garrMissionID);
						if (record4.TravelDuration > garrAbilityEffectRec.ActionHours * 3600f)
						{
							return true;
						}
						break;
					}
					case 12u:
						return true;
					}
					if (!abilityIDList.Contains(garrAbilityRec.ID))
					{
						abilityIDList.Add(garrAbilityRec.ID);
					}
					return true;
				});
			}
		}
		return abilityIDList.ToArray();
	}

	public static int ApplyArtifactXPMultiplier(int inputAmount, float multiplier)
	{
		if (multiplier > 1f)
		{
			float num = (float)inputAmount * multiplier;
			int num2;
			if (num < 50f)
			{
				num2 = 1;
			}
			else if (num < 1000f)
			{
				num2 = 5;
			}
			else if (num < 5000f)
			{
				num2 = 25;
			}
			else
			{
				num2 = 50;
			}
			return num2 * (int)Math.Round((double)(num / (float)num2));
		}
		return inputAmount;
	}

	public static bool MissionHasUncounteredDeadly(GameObject enemyPortraitsGroup)
	{
		bool hasUncounteredDeadly = false;
		if (enemyPortraitsGroup != null)
		{
			MissionMechanic[] componentsInChildren = enemyPortraitsGroup.GetComponentsInChildren<MissionMechanic>(true);
			foreach (MissionMechanic missionMechanic in componentsInChildren)
			{
				if (!missionMechanic.IsCountered())
				{
					if (missionMechanic.AbilityID() != 0)
					{
						StaticDB.garrAbilityEffectDB.EnumRecordsByParentID(missionMechanic.AbilityID(), delegate(GarrAbilityEffectRec garrAbilityEffectRec)
						{
							if (garrAbilityEffectRec.AbilityAction == 27u)
							{
								hasUncounteredDeadly = true;
								return false;
							}
							return true;
						});
						if (hasUncounteredDeadly)
						{
							break;
						}
					}
				}
			}
		}
		return hasUncounteredDeadly;
	}

	public static string TextOrderString(string text1, string text2)
	{
		if (Main.instance.GetLocale() == "koKR")
		{
			return string.Format("{0} {1}", text2, text1);
		}
		return string.Format("{0} {1}", text1, text2);
	}

	public static Sprite GetLocalizedFollowerXpIcon()
	{
		string locale = Main.instance.GetLocale();
		if (locale == "enUS")
		{
			return Resources.Load<Sprite>("MiscIcons/XPBonus_Icon");
		}
		return Resources.Load<Sprite>("MiscIcons/LocalizedIcons/" + locale + "/XPBonus_Icon");
	}

	public static string s_defaultColor = "ffd200ff";

	public static string s_normalColor = "ffffffff";

	public static string s_friendlyColor = "00ff00ff";
}
