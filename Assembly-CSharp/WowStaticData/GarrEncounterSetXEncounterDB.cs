using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrEncounterSetXEncounterDB : MODB<int, GarrEncounterSetXEncounterRec>
	{
		public IEnumerable<GarrEncounterSetXEncounterRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.GarrEncounterSetID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrEncounterSetXEncounter.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrEncounterSetXEncounterRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
