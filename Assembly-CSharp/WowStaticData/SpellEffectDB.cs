using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WowStaticData
{
	public class SpellEffectDB : MODB<int, SpellEffectRec>
	{
		public IEnumerable<SpellEffectRec> GetRecordsByParentID(int parentID)
		{
			return from rec in this.m_records.Values
			where rec.SpellID == parentID
			select rec;
		}

		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/SpellEffect.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(SpellEffectRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
