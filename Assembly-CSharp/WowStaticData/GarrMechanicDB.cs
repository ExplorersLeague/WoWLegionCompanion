using System;
using UnityEngine;

namespace WowStaticData
{
	public class GarrMechanicDB : MODB<int, GarrMechanicRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/GarrMechanic.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(GarrMechanicRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
