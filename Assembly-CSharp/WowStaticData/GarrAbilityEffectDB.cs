using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class GarrAbilityEffectDB : MODB<int, GarrAbilityEffectRec>
	{
		public IEnumerable<GarrAbilityEffectRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where (int)rec.GarrAbilityID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrAbilityEffect.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrAbilityEffectRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
