using System;
using UnityEngine;

namespace WowStaticData
{
	public class MobileStringsDB : MODB<string, MobileStringsRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(string.Concat(new string[]
			{
				path,
				locale,
				"/MobileStrings_",
				locale,
				".txt"
			}), localizedBundle);
		}

		protected override void AddRecord(MobileStringsRec rec)
		{
			this.m_records.Add(rec.BaseTag, rec);
		}
	}
}
