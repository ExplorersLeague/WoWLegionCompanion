using System;
using UnityEngine;

namespace WowStaticData
{
	public class CharShipmentContainerDB : MODB<int, CharShipmentContainerRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/CharShipmentContainer_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(CharShipmentContainerRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
