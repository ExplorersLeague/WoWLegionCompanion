using System;
using UnityEngine;

namespace WowStaticData
{
	public class RewardPackDB : MODB<int, RewardPackRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/RewardPack.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(RewardPackRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
