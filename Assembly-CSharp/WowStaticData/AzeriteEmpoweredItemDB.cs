using System;
using UnityEngine;

namespace WowStaticData
{
	public class AzeriteEmpoweredItemDB : MODB<int, AzeriteEmpoweredItemRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/AzeriteEmpoweredItem.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(AzeriteEmpoweredItemRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
