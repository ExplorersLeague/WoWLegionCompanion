using System;
using UnityEngine;
using WowStatConstants;
using WowStaticData;

public class GarrisonStatus
{
	public static int ArtifactKnowledgeLevel { get; set; }

	public static float ArtifactXpMultiplier { get; set; }

	public static void SetGarrisonServerConnectTime(long serverTime)
	{
		GarrisonStatus.s_serverConnectTime = serverTime;
		GarrisonStatus.s_clientConnectTime = (long)Time.realtimeSinceStartup;
	}

	public static long CurrentTime()
	{
		return GarrisonStatus.s_serverConnectTime + ((long)Time.realtimeSinceStartup - GarrisonStatus.s_clientConnectTime);
	}

	public static void SetFaction(int faction)
	{
		GarrisonStatus.s_faction = faction;
	}

	public static void CheatFastForwardOneHour()
	{
		GarrisonStatus.s_serverConnectTime += 3600L;
	}

	public static void SetCurrencies(int gold, int oil, int resources)
	{
		GarrisonStatus.s_gold = gold;
		GarrisonStatus.s_oil = oil;
		GarrisonStatus.s_resources = resources;
	}

	public static void SetCharacterName(string name)
	{
		GarrisonStatus.s_characterName = name;
	}

	public static void SetCharacterLevel(int level)
	{
		GarrisonStatus.s_characterLevel = level;
	}

	public static void SetCharacterClass(int charClassID)
	{
		GarrisonStatus.s_characterClassID = charClassID;
		ChrClassesRec record = StaticDB.chrClassesDB.GetRecord(charClassID);
		if (record != null)
		{
			GarrisonStatus.s_characterClassName = record.Name;
		}
		else
		{
			GarrisonStatus.s_characterClassName = string.Empty;
		}
	}

	public static int Gold()
	{
		return GarrisonStatus.s_gold;
	}

	public static int Oil()
	{
		return GarrisonStatus.s_oil;
	}

	public static int Resources()
	{
		return GarrisonStatus.s_resources;
	}

	public static string CharacterName()
	{
		return GarrisonStatus.s_characterName;
	}

	public static int CharacterLevel()
	{
		return GarrisonStatus.s_characterLevel;
	}

	public static int CharacterClassID()
	{
		return GarrisonStatus.s_characterClassID;
	}

	public static string CharacterClassName()
	{
		return GarrisonStatus.s_characterClassName;
	}

	public static PVP_FACTION Faction()
	{
		return (PVP_FACTION)GarrisonStatus.s_faction;
	}

	public static void SetFollowerActivationInfo(int remainingActivations, int activationGoldCost)
	{
		GarrisonStatus.s_remainingFollowerActivations = remainingActivations;
		GarrisonStatus.s_followerActivationGoldCost = activationGoldCost;
	}

	public static int GetRemainingFollowerActivations()
	{
		return GarrisonStatus.s_remainingFollowerActivations;
	}

	public static int GetFollowerActivationGoldCost()
	{
		return GarrisonStatus.s_followerActivationGoldCost;
	}

	public static void SetMaxActiveFollowers(int maxActiveFollowers)
	{
		GarrisonStatus.s_maxActiveFollowers = maxActiveFollowers;
	}

	public static int GetMaxActiveFollowers()
	{
		return GarrisonStatus.s_maxActiveFollowers;
	}

	private static long s_serverConnectTime;

	private static long s_clientConnectTime;

	private static int s_faction;

	private static int s_gold;

	private static int s_oil;

	private static int s_resources;

	private static int s_characterLevel;

	private static int s_characterClassID;

	private static string s_characterClassName;

	private static string s_characterName;

	private static int s_remainingFollowerActivations;

	private static int s_followerActivationGoldCost;

	private static int s_maxActiveFollowers;
}
