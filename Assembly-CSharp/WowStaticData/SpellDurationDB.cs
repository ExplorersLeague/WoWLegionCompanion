using System;
using UnityEngine;

namespace WowStaticData
{
	public class SpellDurationDB : MODB<int, SpellDurationRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/SpellDuration.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(SpellDurationRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
