using System;
using UnityEngine;

namespace WowStaticData
{
	public class UiTextureKitDB : MODB<int, UiTextureKitRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/UiTextureKit.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(UiTextureKitRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
