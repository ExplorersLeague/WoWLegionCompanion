using System;
using UnityEngine;

namespace WowStaticData
{
	public class CreatureDB : MODB<int, CreatureRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/Creature_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(CreatureRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
