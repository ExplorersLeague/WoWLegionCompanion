using System;
using System.Collections;
using WowJamMessages.MobileClientJSON;

public class LegionfallData
{
	private static LegionfallData instance
	{
		get
		{
			if (LegionfallData.s_instance == null)
			{
				LegionfallData.s_instance = new LegionfallData();
				LegionfallData.s_instance.m_legionfallDictionary = new Hashtable();
				LegionfallData.s_instance.m_legionfallWarResources = 0;
			}
			return LegionfallData.s_instance;
		}
	}

	public static Hashtable legionfallDictionary
	{
		get
		{
			return LegionfallData.instance.m_legionfallDictionary;
		}
	}

	public static void AddOrUpdateLegionfallBuilding(MobileContribution contribution)
	{
		if (LegionfallData.instance.m_legionfallDictionary.ContainsKey(contribution.ContributionID))
		{
			LegionfallData.instance.m_legionfallDictionary.Remove(contribution.ContributionID);
		}
		LegionfallData.MobileContributionData mobileContributionData = default(LegionfallData.MobileContributionData);
		mobileContributionData.contribution = contribution;
		mobileContributionData.underAttackExpireTime = 0L;
		LegionfallData.instance.m_legionfallDictionary.Add(contribution.ContributionID, mobileContributionData);
	}

	public static void ClearData()
	{
		LegionfallData.instance.m_hasAccess = false;
		LegionfallData.instance.m_legionfallWarResources = 0;
		LegionfallData.instance.m_legionfallDictionary.Clear();
	}

	public static void SetLegionfallWarResources(int legionfallWarResources)
	{
		LegionfallData.instance.m_legionfallWarResources = legionfallWarResources;
	}

	public static int WarResources()
	{
		return LegionfallData.instance.m_legionfallWarResources;
	}

	public static void SetHasAccess(bool hasAccess)
	{
		LegionfallData.instance.m_hasAccess = hasAccess;
	}

	public static bool HasAccess()
	{
		return LegionfallData.instance.m_hasAccess;
	}

	public static void SetCurrentInvasionPOI(JamMobileAreaPOI poi)
	{
		LegionfallData.instance.m_currentInvasionPOI = poi;
	}

	public static JamMobileAreaPOI GetCurrentInvasionPOI()
	{
		return LegionfallData.instance.m_currentInvasionPOI;
	}

	public static void SetCurrentInvasionExpirationTime(int secondsUntilExpiration)
	{
		LegionfallData.instance.m_invasionExpirationTime = GarrisonStatus.CurrentTime() + (long)secondsUntilExpiration;
	}

	public static long GetCurrentInvasionExpirationTime()
	{
		return LegionfallData.instance.m_invasionExpirationTime;
	}

	private static LegionfallData s_instance;

	private int m_legionfallWarResources;

	private bool m_hasAccess;

	private Hashtable m_legionfallDictionary;

	private JamMobileAreaPOI m_currentInvasionPOI;

	private long m_invasionExpirationTime;

	public struct MobileContributionData
	{
		public MobileContribution contribution;

		public long underAttackExpireTime;
	}
}
