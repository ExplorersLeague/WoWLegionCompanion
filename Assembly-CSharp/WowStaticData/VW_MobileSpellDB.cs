using System;
using UnityEngine;

namespace WowStaticData
{
	public class VW_MobileSpellDB : MODB<int, VW_MobileSpellRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/VW_MobileSpell_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(VW_MobileSpellRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
