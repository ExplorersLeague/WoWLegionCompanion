using System;
using System.Collections;
using WowJamMessages;
using WowStaticData;

public class PersistentMissionData
{
	private static PersistentMissionData instance
	{
		get
		{
			if (PersistentMissionData.s_instance == null)
			{
				PersistentMissionData.s_instance = new PersistentMissionData();
				PersistentMissionData.s_instance.m_missionDictionary = new Hashtable();
			}
			return PersistentMissionData.s_instance;
		}
	}

	public static Hashtable missionDictionary
	{
		get
		{
			return PersistentMissionData.instance.m_missionDictionary;
		}
	}

	public static void AddMission(JamGarrisonMobileMission mission)
	{
		if (!PersistentMissionData.instance.m_missionDictionary.ContainsKey(mission.MissionRecID))
		{
			PersistentMissionData.instance.m_missionDictionary.Add(mission.MissionRecID, mission);
		}
	}

	public static void UpdateMission(JamGarrisonMobileMission mission)
	{
		PersistentMissionData.instance.m_missionDictionary.Remove(mission.MissionRecID);
		PersistentMissionData.AddMission(mission);
	}

	public static void ClearData()
	{
		PersistentMissionData.instance.m_missionDictionary.Clear();
	}

	public static int GetNumCompletedMissions()
	{
		int num = 0;
		foreach (object obj in PersistentMissionData.missionDictionary.Values)
		{
			JamGarrisonMobileMission jamGarrisonMobileMission = (JamGarrisonMobileMission)obj;
			GarrMissionRec record = StaticDB.garrMissionDB.GetRecord(jamGarrisonMobileMission.MissionRecID);
			if (record != null)
			{
				if (record.GarrFollowerTypeID == 4u)
				{
					long num2 = GarrisonStatus.CurrentTime() - jamGarrisonMobileMission.StartTime;
					long num3 = jamGarrisonMobileMission.MissionDuration - num2;
					if ((jamGarrisonMobileMission.MissionState == 1 && num3 <= 0L) || jamGarrisonMobileMission.MissionState == 2 || jamGarrisonMobileMission.MissionState == 3)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	private static PersistentMissionData s_instance;

	private Hashtable m_missionDictionary;
}
