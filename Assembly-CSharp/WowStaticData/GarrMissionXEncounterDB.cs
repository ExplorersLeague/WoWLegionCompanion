using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrMissionXEncounterDB : MODB<int, GarrMissionXEncounterRec>
	{
		public IEnumerable<GarrMissionXEncounterRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.GarrMissionID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrMissionXEncounter.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrMissionXEncounterRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
