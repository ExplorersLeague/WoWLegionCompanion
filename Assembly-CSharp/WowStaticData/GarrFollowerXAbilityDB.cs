using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrFollowerXAbilityDB : MODB<int, GarrFollowerXAbilityRec>
	{
		public IEnumerable<GarrFollowerXAbilityRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.GarrFollowerID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrFollowerXAbility.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrFollowerXAbilityRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
