using System;
using UnityEngine;

namespace WowStaticData
{
	public class CommunityIconDB : MODB<int, CommunityIconRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/CommunityIcon.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(CommunityIconRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
