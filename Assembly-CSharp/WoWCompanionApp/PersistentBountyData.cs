using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class PersistentBountyData
	{
		private static PersistentBountyData instance
		{
			get
			{
				if (PersistentBountyData.s_instance == null)
				{
					PersistentBountyData.s_instance = new PersistentBountyData();
					PersistentBountyData.s_instance.m_bountiesAreVisible = false;
				}
				return PersistentBountyData.s_instance;
			}
		}

		public static IDictionary<int, WrapperWorldQuestBounty> bountyDictionary
		{
			get
			{
				return PersistentBountyData.instance.m_bountyDictionary;
			}
		}

		public static IDictionary<int, WrapperBountiesByWorldQuest> bountiesByWorldQuestDictionary
		{
			get
			{
				return PersistentBountyData.instance.m_bountiesByWorldQuestDictionary;
			}
		}

		public static void SetBountiesVisible(bool visible)
		{
			PersistentBountyData.s_instance.m_bountiesAreVisible = visible;
		}

		public static bool BountiesAreVisible()
		{
			return PersistentBountyData.s_instance.m_bountiesAreVisible;
		}

		public static void AddOrUpdateBounty(WrapperWorldQuestBounty bounty)
		{
			if (PersistentBountyData.instance.m_bountyDictionary.ContainsKey(bounty.QuestID))
			{
				PersistentBountyData.instance.m_bountyDictionary.Remove(bounty.QuestID);
			}
			PersistentBountyData.instance.m_bountyDictionary.Add(bounty.QuestID, bounty);
		}

		public static void AddOrUpdateBountiesByWorldQuest(WrapperBountiesByWorldQuest bountiesByWorldQuest)
		{
			if (PersistentBountyData.instance.m_bountiesByWorldQuestDictionary.ContainsKey(bountiesByWorldQuest.QuestID))
			{
				PersistentBountyData.instance.m_bountiesByWorldQuestDictionary.Remove(bountiesByWorldQuest.QuestID);
			}
			PersistentBountyData.instance.m_bountiesByWorldQuestDictionary.Add(bountiesByWorldQuest.QuestID, bountiesByWorldQuest);
		}

		public static void ClearData()
		{
			PersistentBountyData.instance.m_bountyDictionary.Clear();
			PersistentBountyData.instance.m_bountiesByWorldQuestDictionary.Clear();
		}

		private static PersistentBountyData s_instance;

		private Dictionary<int, WrapperWorldQuestBounty> m_bountyDictionary = new Dictionary<int, WrapperWorldQuestBounty>();

		private Dictionary<int, WrapperBountiesByWorldQuest> m_bountiesByWorldQuestDictionary = new Dictionary<int, WrapperBountiesByWorldQuest>();

		private bool m_bountiesAreVisible;
	}
}
