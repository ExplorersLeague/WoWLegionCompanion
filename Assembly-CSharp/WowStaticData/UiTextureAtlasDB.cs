using System;
using UnityEngine;

namespace WowStaticData
{
	public class UiTextureAtlasDB : MODB<int, UiTextureAtlasRec>
	{
		public override bool Load(string path, AssetBundle nonLocalizedBundle, AssetBundle localizedBundle, string locale)
		{
			return base.Load(path + "NonLocalized/UiTextureAtlas.txt", nonLocalizedBundle);
		}

		protected override void AddRecord(UiTextureAtlasRec rec)
		{
			this.m_records.Add(rec.ID, rec);
		}
	}
}
