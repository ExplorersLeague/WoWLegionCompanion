using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class LegionfallData
	{
		private static LegionfallData instance
		{
			get
			{
				if (LegionfallData.s_instance == null)
				{
					LegionfallData.s_instance = new LegionfallData();
					LegionfallData.s_instance.m_legionfallWarResources = 0;
				}
				return LegionfallData.s_instance;
			}
		}

		public static IDictionary<int, LegionfallData.ContributionData> legionfallDictionary
		{
			get
			{
				return LegionfallData.instance.m_legionfallDictionary;
			}
		}

		public static void AddOrUpdateLegionfallBuilding(WrapperContribution contribution)
		{
			if (LegionfallData.instance.m_legionfallDictionary.ContainsKey(contribution.ContributionID))
			{
				LegionfallData.instance.m_legionfallDictionary.Remove(contribution.ContributionID);
			}
			LegionfallData.ContributionData value = default(LegionfallData.ContributionData);
			value.contribution = contribution;
			value.underAttackExpireTime = DateTime.MaxValue;
			LegionfallData.instance.m_legionfallDictionary.Add(contribution.ContributionID, value);
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

		public static void SetCurrentInvasionPOI(WrapperAreaPoi? poi)
		{
			LegionfallData.instance.m_currentInvasionPOI = poi;
		}

		public static bool HasCurrentInvasionPOI()
		{
			return LegionfallData.instance.m_currentInvasionPOI != null;
		}

		public static WrapperAreaPoi GetCurrentInvasionPOI()
		{
			return LegionfallData.instance.m_currentInvasionPOI.Value;
		}

		public static void SetCurrentInvasionExpirationTime(TimeSpan secondsUntilExpiration)
		{
			LegionfallData.instance.m_invasionExpirationTime = GarrisonStatus.CurrentTime() + secondsUntilExpiration;
		}

		public static DateTime GetCurrentInvasionExpirationTime()
		{
			return LegionfallData.instance.m_invasionExpirationTime;
		}

		private static LegionfallData s_instance;

		private int m_legionfallWarResources;

		private bool m_hasAccess;

		private Dictionary<int, LegionfallData.ContributionData> m_legionfallDictionary = new Dictionary<int, LegionfallData.ContributionData>();

		private WrapperAreaPoi? m_currentInvasionPOI;

		private DateTime m_invasionExpirationTime;

		public struct ContributionData
		{
			public WrapperContribution contribution;

			public DateTime underAttackExpireTime;
		}
	}
}
