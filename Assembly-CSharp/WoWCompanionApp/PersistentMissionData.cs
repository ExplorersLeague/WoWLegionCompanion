using System;
using System.Collections.Generic;
using WowStaticData;

namespace WoWCompanionApp
{
	public class PersistentMissionData
	{
		private static PersistentMissionData instance
		{
			get
			{
				if (PersistentMissionData.s_instance == null)
				{
					PersistentMissionData.s_instance = new PersistentMissionData();
				}
				return PersistentMissionData.s_instance;
			}
		}

		public static IDictionary<int, WrapperGarrisonMission> missionDictionary
		{
			get
			{
				return PersistentMissionData.instance.m_missionDictionary;
			}
		}

		public static void AddMission(WrapperGarrisonMission mission)
		{
			if (!PersistentMissionData.instance.m_missionDictionary.ContainsKey(mission.MissionRecID))
			{
				PersistentMissionData.instance.m_missionDictionary.Add(mission.MissionRecID, mission);
			}
		}

		public static void UpdateMission(WrapperGarrisonMission mission)
		{
			PersistentMissionData.instance.m_missionDictionary.Remove(mission.MissionRecID);
			PersistentMissionData.AddMission(mission);
		}

		public static void ClearData()
		{
			PersistentMissionData.instance.m_missionDictionary.Clear();
		}

		public static int GetNumCompletedMissions(bool skipSupportMissions = false)
		{
			int num = 0;
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null)
				{
					if (record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType)
					{
						if (!skipSupportMissions || (record.Flags & 16u) == 0u)
						{
							TimeSpan t = GarrisonStatus.CurrentTime() - wrapperGarrisonMission.StartTime;
							TimeSpan timeSpan = wrapperGarrisonMission.MissionDuration - t;
							if ((wrapperGarrisonMission.MissionState == 1 && timeSpan.TotalSeconds <= 0.0) || wrapperGarrisonMission.MissionState == 2 || wrapperGarrisonMission.MissionState == 3)
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		public static void GetAvailableAndProgressCounts(ref int numAvailable, ref int numInProgress)
		{
			foreach (WrapperGarrisonMission wrapperGarrisonMission in PersistentMissionData.missionDictionary.Values)
			{
				GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(wrapperGarrisonMission.MissionRecID);
				if (record != null && record.GarrFollowerTypeID == (uint)GarrisonStatus.GarrisonFollowerType && (record.Flags & 16u) == 0u)
				{
					if (wrapperGarrisonMission.MissionState == 1 || wrapperGarrisonMission.MissionState == 2 || wrapperGarrisonMission.MissionState == 3)
					{
						numInProgress++;
					}
					else
					{
						numAvailable++;
					}
				}
			}
		}

		private static PersistentMissionData s_instance;

		private Dictionary<int, WrapperGarrisonMission> m_missionDictionary = new Dictionary<int, WrapperGarrisonMission>();
	}
}
