using System;
using UnityEngine;

namespace WowStaticData
{
	public class SpellTooltipDB : MODB<int, SpellTooltipRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/SpellTooltip_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(SpellTooltipRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
