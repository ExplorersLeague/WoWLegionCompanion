using System;
using UnityEngine;

namespace WowStaticData
{
	public class QuestInfoDB : MODB<int, QuestInfoRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/QuestInfo_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(QuestInfoRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
