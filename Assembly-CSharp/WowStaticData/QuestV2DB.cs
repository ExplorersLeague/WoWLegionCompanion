using System;
using UnityEngine;

namespace WowStaticData
{
	public class QuestV2DB : MODB<int, QuestV2Rec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/QuestV2_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(QuestV2Rec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
